var selectedTab = 0;
selectedDataTableName = "#paid-datatable-default-";
$(document).ready(function () { 
    searchDataTable();
    configureActions();
    configureDialogModels();
});
 
configureActions = () => {
    $('.tabs ul li a').click(function () {
        var action = $(this).attr("href");
        if (action == "#paid") {
            selectedTab = 0;
            selectedDataTableName = "#paid-datatable-default-";
        } else if (action == "#unpaid") {
            selectedTab = 1;
            selectedDataTableName = "#unpaid-datatable-default-";
        } else {
            selectedTab = 2;
            selectedDataTableName = "#failed-datatable-default-";
        }
        searchDataTable();
        showLog(action);
    });
}
  

searchDataTable = () => {

    if ($.fn.dataTable.isDataTable(selectedDataTableName)) {
        $(selectedDataTableName).DataTable().destroy();
    } 
   
    $(selectedDataTableName).DataTable({
        responsive:true,
        searching: false,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "Order/GetSalesOrderForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                //var search = $(":input[type=search]").val();
                //if (search.length <= 0) { showLoader(); }
                d.selectedTab = selectedTab;
                d.customerId = getTextValue("customerId"); //from customerList to show specific customer details
                d.orderNumber = getTextValue("orderNumber");
                d.startDate = getDatePickerValue("startDate");
                d.endDate = getDatePickerValue("endDate");
                d.customerName = getTextValue("customerName");
                d.customerMobile = getTextValue("customerMobile");
                d.customerEmail = getTextValue("customerEmail");
                d.paymentMethodId = getSelectedItemValue("paymentMethodList");
                d.orderTypeId = getSelectedItemValue("orderTypeList");
                d.orderStatusId = getSelectedItemValue("orderStatusList");
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                checkAPIResponse(json);
                hideLoader();
                return json.data;
            },
            error: function (error) {
                showLog('error'+error);
            },
        },
        
        "columns": [
            { "data": "id" },
            { "data": "orderNumber" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "orderTypeId", render: function (data, type, row) { return getOrderTypeHtml(row); } },
            { "data": "customer.name" },
            { "data": "customer.mobileNumber" },
            { "data": "total", "name": "total" },
            { "data": "paymentMethodId", render: function (data, type, row) { return getPaymentMethodHtml(row); }, },
            { "data": "paymentStatusId", render: function (data, type, row) { return getPaymentStatusHtml(row); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getOrderStatusHtml(row); }, },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row);  }, },
        ],
        columnDefs: [{
            'targets': 7,
            'width': "13.5%"
        }],
    });

   
    orderStatus = (itemData) => {
        return "<td><span class='font-weight-bold' style='color:" + itemData.statusColor + "'>" + itemData.status + "</span></td>";
    }
      
    getActionsHtml = (row) => {
        var html = `<a href="/order/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                    <a   href='#' onclick='downloadPDF(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
                    `;
        if (row.orderStatusId != 4 && row.orderStatusId != 3)  { //if order is not delivered
            html += `<span data-bs-toggle="modal" class="open-order-cancel-modal" data-id="${row.id}" data-bs-target="#order-cancel"><a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Order Cancel" data-bs-original-title="Order Cancel" aria-label="Order Cancel" ><i class="fa-solid fa-xmark"></i></a> </span>`;
        }
        if (selectedTab != 0 && row.orderStatusId != 4) { //unpaid + failed + not delivered
            html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>
                    `;
        }

        return html;
    }
}

configureDialogModels = () => {
     
    //$(document).on("click", ".open-order-status", function () {
    //    $("#order-status .modal-header #dialogOrderId").val($(this).data('id'));
         
    //});
     
    $(document).on("click", ".open-order-cancel-modal", function () {
        $("#order-cancel .modal-header #dialogOrderId").val($(this).data('id')); 
    });
    //send-payment-link-modal
    $(document).on("click", ".open-send-payment-link-modal", function () {
        $("#send-payment-link-modal .modal-header #dialogOrderId").val($(this).data('id'));

        // $('#payment-link-modal').modal('show');
    });
     
}

downloadPDF = (orderId) => {
    ajaxGet('Order/GetOrderPDF?id=' + orderId, cbGetPDFSuccess); 
}
cbGetPDFSuccess = (data) => {
    window.open(data.data, "_blank");
}
 
 
