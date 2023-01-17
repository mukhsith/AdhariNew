 
$(document).ready(function () { 
    configureDialogModels();
    searchNormalDataTable();
});
 

searchNormalDataTable = () => {
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }

    $("#datatable-default-").DataTable({
        responsive: true,
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "driver/GetDeliveryDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
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
            { "data": "customerName" },
            { "data": "mobileNumber" },
            { "data": "formattedDeliveryFee" },
            { "data": "formattedTotal" },
            { "data": "driverName" },
            { "data": "paymentStatus", "name": "paymentStatusId", render: function (data, type, row) { return getOrderPaidStatusHtml(row); }, },
            { "data": "deliveryStatus", "name": "orderStatusId", render: function (data, type, row) { return getOrderDeliveryStatusHtml(row); }, },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
        ],
        createdRow: function (row, data, dataIndex) {
            if (data['orderTypeName'] == 'Normal') {
                $(row).addClass('body-bg-warning odd');
            }
        }

    });

}
    
getActionsHtml = (row) => {

    var html = `<a href="/driver/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>`;

        if (row.orderStatusId != 4) {
             
            html += `<span data-bs-toggle="modal" class="open-confirm-delivery-modal" data-id="${row.id}" data-bs-target="#confirm-delivery-modal"> <a href="#" onclick='Updatedelivery(${row.id},${row.customerId},${row.orderNumber},${row.orderModeID});' class="mb-1 mt-1 me-1 btn btn-sm btn-success"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Delivered" data-bs-original-title="Delivered" aria-label="Delivered" ><i class="fas fa-check"></i></a> </span>
                    `;

            if (row.total > 0 && row.paymentStatusId != 2) {
                html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.mobileNumber}" data-bs-target="#send-payment-link-modal"> <a href='#' onclick='SendQpay(${row.id},${row.mobileNumber},${row.customerId},${row.total},${row.orderNumber});' class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>`;
            }
     
            html += `<span data-bs-toggle="modal" class="open-reschedule-delivery-modal" data-id="${row.id}" data-bs-target="#reschedule-delivery-modal"> <a href='#' onclick='RemoveDriver(${row.id},${row.customerId},${row.orderNumber},${row.orderModeID});' class="mb-1 mt-1 me-1 btn btn-sm btn-dark"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Reschedule Order" data-bs-original-title="Reschedule Order" aria-label="Remove Driver" ><i class="fas fa-undo"></i></a> </span>
                     `;
        }

    return html;
}
    
    configureDialogModels = () => {
  
      
    $(document).on("click", ".open-send-payment-link-modal", function () {
        //var orderId = $(this).data('id');
        $("#send-payment-link-modal .modal-header #dialogOrderId").val($(this).data('id'));

        // $('#payment-link-modal').modal('show');
    });

     
    $(document).on("click", ".open-confirm-delivery-modal", function () {
        //var orderId = $(this).data('id');
        $("#confirm-delivery-modal .modal-header #dialogOrderId").val($(this).data('id'));
        // $('#confirm-delivery-modal').modal('show');
    });

    //reschedule is based on login userid
    $(document).on("click", ".open-reschedule-delivery-modal", function () {
        //var orderId = $(this).data('id');
        $("#reschedule-delivery-modal .modal-header #dialogOrderId").val($(this).data('id'));
        $("#reschedule-delivery-modal .modal-header #apiUrl").val("driver/rescheduleOrder");
            // $('#cancel-order-modal').modal('show');
    });
          

}


SendQpay = (OrderID, MobileNumber, CustomerId, Total, OrderNumber) => {
    //alert(MobileNumber);
    setTextValue("dialogMobileNumber", MobileNumber);
    setTextValue("dialogOrderId", OrderID);
    setTextValue("dialogCustomerId", CustomerId);
    setTextValue("dialogTotal", Total);
    setTextValue("dialogOrderNumber", OrderNumber);


    //var endpoint = getAPIUrl() + "Order/SendQpay?CustomerID=" + CustomerId +
    //    "&OrderID=" + OrderID + "&OrderNumber=" + OrderNumber + "&Ordertotal=" + Total ;

    ////$('#DisplayOrderModel').modal('toggle');

    ////showLoader();
    //$.ajax({
    //    url: endpoint,
    //    method: "POST",
    //    headers: {
    //        "Content-Type": "application/json",
    //        "Authorization": 'Bearer ' + getToken()
    //    },
    //    success: function (data) {
    //        hideLoader();
    //        if (data.success) {
    //           // updateDataTableDisplayOrder(data);
    //            ToastAlert('success', 'Order', 'QPay link send Successfully');
    //            hideLoader();
    //        }
    //        //else {
    //        //    showLog(data);
    //        //    ToastAlert('danger', 'Order', data.message);
    //        //}

    //    },
    //    error: function (xhr) {
    //        hideLoader();
    //        ToastAlert('error', 'Display Order', xhr);
    //    }
    //});
}


sendPaymentLink = () => {


    var OrderID = getTextValue("dialogOrderId");
    var CustomerId = getTextValue("dialogCustomerId");
    var Total = getTextValue("dialogTotal");
    var OrderNumber = getTextValue("dialogOrderNumber");

    var endpoint = getAPIUrl() + "Order/SendQpay?CustomerID=" + CustomerId +
        "&OrderID=" + OrderID + "&OrderNumber=" + OrderNumber + "&Ordertotal=" + Total;

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
            /*if (data.success) {*/
            // updateDataTableDisplayOrder(data);
            ToastAlert('success', 'Order', 'QPay link send Successfully');
            hideLoader();
            //}
            //else {
            //    showLog(data);
            //    ToastAlert('danger', 'Order', data.message);
            //}
            $("#send-payment-link-modal").modal('hide');

        },
        error: function (xhr) {
            hideLoader();
            //ToastAlert('error', 'Display Order', xhr);
        }
    });

}




Updatedelivery = (OrderID, CustomerId, OrderNumber, OrderModeID) => {
    //alert(MobileNumber);
    setTextValue("dialogOrderId", OrderID);
    setTextValue("dialogCustomerId", CustomerId);
    setTextValue("dialogOrderTypeId", OrderModeID);
    setTextValue("dialogOrderNumber", OrderNumber);


    //var endpoint = getAPIUrl() + "Order/SendQpay?CustomerID=" + CustomerId +
    //    "&OrderID=" + OrderID + "&OrderNumber=" + OrderNumber + "&Ordertotal=" + Total ;

    ////$('#DisplayOrderModel').modal('toggle');

    ////showLoader();
    //$.ajax({
    //    url: endpoint,
    //    method: "POST",
    //    headers: {
    //        "Content-Type": "application/json",
    //        "Authorization": 'Bearer ' + getToken()
    //    },
    //    success: function (data) {
    //        hideLoader();
    //        if (data.success) {
    //           // updateDataTableDisplayOrder(data);
    //            ToastAlert('success', 'Order', 'QPay link send Successfully');
    //            hideLoader();
    //        }
    //        //else {
    //        //    showLog(data);
    //        //    ToastAlert('danger', 'Order', data.message);
    //        //}

    //    },
    //    error: function (xhr) {
    //        hideLoader();
    //        ToastAlert('error', 'Display Order', xhr);
    //    }
    //});
}

 
 
