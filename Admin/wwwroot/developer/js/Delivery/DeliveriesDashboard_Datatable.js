var drivers = [];
var selectedTab = 0;
selectedDataTableName = "#notdispatched-datatable-default-";
$(document).ready(function () {

    fillDropDownList("driverList", 'SystemUser/GetAllDriver', false, null, "id", "fullName", cbGetDriver);
    fillDropDownList("areaList", 'Area/ForDropDownList', false, null, "id", "name");
    configureActions();
    configureDialogModels();
    searchForDataTable();
});
cbGetDriver = (data) => {
    drivers = data.data;
}
configureActions = () => {
    $('.tabs ul li a').click(function () {
        var action = $(this).attr("href");
        if (action == "#notdispatched") {
            selectedTab = 0;
            selectedDataTableName = "#notdispatched-datatable-default-";
        } else {
            selectedTab = 1;
            selectedDataTableName = "#dispatched-datatable-default-";
        }
        searchForDataTable();
        showLog(action);
    });
}

searchForDataTable = () => {
    if ($.fn.dataTable.isDataTable(selectedDataTableName)) {
        $(selectedDataTableName).DataTable().destroy();
    }

    $(selectedDataTableName).DataTable({
        responsive: true,
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "Order/GetDeliveriesForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
                d.selectedTab = selectedTab;
                d.orderNumber = getTextValue("orderNumber");
                d.deliveryDate = getDatePickerValue("deliveryDate");

                d.orderModeID = getSelectedItemValue("orderTypeList");
                d.orderTypeId = getSelectedItemValue("orderModeList");
                d.areaId = getSelectedItemValue("areaList");
                d.driverId = getSelectedItemValue("driverList");
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                checkAPIResponse(json);
                hideLoader();
                return json.data;
            },
            error: function (error) {
                showLog('error' + error);
            },
        },

        "columns": [
            { "data": "id" },
            { "data": "orderNumber" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "deliveryDate", render: function (data, type, row) { return getFormatedDate(row.deliveryDate); } },
            /* { "data": "deliveryDate", render: row.deliveryDate.datetime('dd/MM/yyyy') },*/
            /*         { "data": "deliveryDate" },*/
            { "data": "orderModeName" },
            { "data": "orderTypeName" },
            { "data": "areaName" },
            { "data": "formattedDeliveryFee" },
            { "data": "formattedTotal" },
            { "data": "driverName" },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
        ],
        createdRow: function (row, data, dataIndex) {
            if (data['orderTypeName'] == 'Normal') {
                $(row).addClass('body-bg-warning odd');
            }
        }
        //createdRow: function (row, data, index) { 
        //    //change display order value, to avoid page refresh
        //    $(row).attr('data-rowid', data.id);
        //   // $(row).find('td:eq(1)').attr('data-displayorder', row.id);

        //    //    Reinitialize ios-switch
        //    //$(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        //}, 
        //columnDefs: [{ 'targets': 0, 'checkboxes': { 'selectRow': true } }],
        //select: { 'style': 'multi' },
        //order: [ [1, 'asc'] ],
        //initComplete: function (settings, json) {
        //    $(".table").find("input[type='checkbox']").addClass("form-check-input");

        //}

    });

}
getActionsHtml = (row) => {

    var html = ``;
    if (row.orderModeID == 1) {
        html += `<a href="/driver/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                <a href='#' onclick='downloadPDF(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>`;

    }
    else {
        html += `<a href="/order/sales-subscriptionDetails?id=${row.subscriptionID}&customerId=${row.customerId}&subscriptionNumber=${row.subscriptionNumber}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                <a href='#' onclick='downloadPDF(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>`;


    }

    if (selectedTab == 0 && row.driverId == null) {
        html += `<span data-bs-toggle="modal" class="open-dispatch-delivery-modal" data-id="${row.id}" data-bs-target="#dispatch-delivery-modal"><a href="javascript: ; " class="mb-1 mt-1 me-1 btn btn-sm btn-warning dispatch-delivery-btn" data-bs-toggle="tooltip" data-bs-placement="bottom" title="" data-bs-original-title="Dispatch Delivery" aria-label="Dispatch Delivery"><i class="fas fa-truck"></i></a></span>`;

    } else if (selectedTab == 1 && row.driverId != null) {
        html += `<span data-bs-toggle="modal" class="open-dispatch-delivery-modal" data-id="${row.id}" data-bs-target="#dispatch-delivery-modal"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger dispatch-delivery-btn"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Reassign Driver" data-bs-original-title="Reassign Driver" aria-label="Reassign Driver" ><i class="fas fa-exchange "></i></a> </span>`;
    }
    if (row.orderStatusId != 4) {
        if (row.total >0) {
            html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.mobileNumber}" data-bs-target="#send-payment-link-modal"> <a href='#' onclick='SendQpay(${row.id},${row.mobileNumber},${row.customerId},${row.total},${row.orderNumber});' class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>`;
        }
        else {
            html += `<span data-bs-toggle="modal" class="open-reschedule-delivery-modal" data-id="${row.id}" data-bs-target="#reschedule-delivery-modal"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-dark"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Reschedule Order" data-bs-original-title="Reschedule Order" aria-label="Reschedule Order" ><i class="fas fa-calendar-plus"></i></a> </span>
                     `;
        }
    }

    return html;
}


configureDialogModels = () => {
    //dispatch-delivery-modal
    $(document).on("click", ".open-dispatch-delivery-modal", function () {
        $("#dispatch-delivery-modal .modal-header #dialogOrderId").val($(this).data('id'));
        let driverList = $("#dispatch-delivery-modal .modal-body #popupDriverList").empty();
        driverList.append($("<option></option>").val(null).html('--Select Driver--'));
        $.each(drivers, function (a, b) {
            driverList.append($("<option value='" + b.id + "'></option>").val(b.id).html(b.fullName));
        });
    });
    //send-payment-link-modal
    $(document).on("click", ".open-send-payment-link-modal", function () {
        $("#send-payment-link-modal .modal-header #dialogOrderId").val($(this).data('id'));
        
        // $('#payment-link-modal').modal('show');
    });

    //reschedule-delivery-modal
    $(document).on("click", ".open-reschedule-delivery-modal", function () {
        $("#reschedule-delivery-modal .modal-header #dialogOrderId").val($(this).data('id'));
        $("#reschedule-delivery-modal .modal-header #apiUrl").val("order/rescheduleOrder");
        // $('#cancel-order-modal').modal('show');
    });
}

downloadPDF = (orderId) => {
    ajaxGet('Order/GetOrderPDF?id=' + orderId, cbGetPDFSuccess);
}
cbGetPDFSuccess = (data) => {
    window.open(data.data, "OrderPDF");
}


SendQpay = (OrderID, MobileNumber, CustomerId, Total, OrderNumber) => {
    //alert(MobileNumber);
    setTextValue("dialogMobileNumber", MobileNumber);
    
    
    var endpoint = getAPIUrl() + "Order/SendQpay?CustomerID=" + CustomerId +
        "&OrderID=" + OrderID + "&OrderNumber=" + OrderNumber + "&OrderNumber=" + Total ;

    //$('#DisplayOrderModel').modal('toggle');

    //showLoader();
    $.ajax({
        url: endpoint,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            hideLoader();
            if (data.success) {
                updateDataTableDisplayOrder(data);
                ToastAlert('success', 'Order', 'QPay link send Successfully');
            } else {
                showLog(data);
                ToastAlert('danger', 'Order', data.message);
            }

        },
        error: function (xhr) {
            hideLoader();
            ToastAlert('error', 'Display Order', xhr);
        }
    });
}

