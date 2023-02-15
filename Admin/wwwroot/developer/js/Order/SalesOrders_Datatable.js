var selectedTab = 0;
selectedDataTableName = "#paid-datatable-default-";
$(document).ready(function () {

    var d = new Date();
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var localdate = day + "/" + month + "/" + year;


    $("#startDate").val(localdate);
    $("#endDate").val(localdate);


    configureActions();
    configureDialogModels();
    searchDataTable();
    searchDataTableSummary();
});

configureActions = () => {
    $('.tabs ul li a').click(function () {
        var action = $(this).attr("href");
        if (action == "#paid") {
            selectedTab = 0;
            selectedDataTableName = "#paid-datatable-default-";
            searchDataTable();
        }
        else if (action == "#unpaid") {
            selectedTab = 1;
            selectedDataTableName = "#unpaid-datatable-default-";
            searchUnpaidDataTable();
        }
        else {
            selectedTab = 2;
            selectedDataTableName = "#failed-datatable-default-";
            searchFailedDataTable();
        }
        // searchDataTable();
        showLog(action);
    });



}


searchOrder = () => {

    var sDate = getFormatedSearchDate($("#startDate").val());
    var eDate = getFormatedSearchDate($("#endDate").val());


    ajaxGetCart("Order/OrderSalesFilterSummary?startDate=" + sDate + "&endDate=" + eDate,
        function (data) {

            $("#KNET").html(data.knet);
            $("#VISA_Master").html(data.visA_Master);
            $("#Tabby").html(data.tabby);
            $("#COD").html(data.cod);
            $("#Wallet").html(data.wallet);
            $("#QPay").html(data.qPay);

            $("#orderReceivedToday").html(data.formattedOrderReceivedToday);
            $("#salesAmountToday").html(data.formattedSalesAmountToday);
            $("#itemsSoldToday").html(data.formattedItemsSoldToday);
            $("#FailedCount").html(data.failedOrderReceived);

        });
    searchDataTable();
}



searchDataTable = () => {





    if ($.fn.dataTable.isDataTable(selectedDataTableName)) {
        $(selectedDataTableName).DataTable().destroy();
    }

    $(selectedDataTableName).DataTable({
        responsive: true,
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
                showLog('error' + error);
            },
        },

        "columns": [
            { "data": "id" },
            { "data": "orderNumber" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "orderTypeId", render: function (data, type, row) { return getOrderTypeHtml(row); } },
            { "data": "customer.name" },
            { "data": "customer.mobileNumber" },
            { "data": "deviceTypeId", render: function (data, type, row) { return getDeviceTypeHtml(row); }, },
            { "data": "total", "name": "total" },
            { "data": "paymentMethodId", render: function (data, type, row) { return getPaymentMethodHtml(row); }, },
            { "data": "paymentStatusId", render: function (data, type, row) { return getPaymentStatusHtml(row); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getOrderStatusHtml(row); }, },
            { "data": "deliveryDate", render: function (data, type, row) { return getFormatedDate(row.deliveryDate); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getDeliveryStatusHtml(row); }, },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
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
        /*var html = `<a href="/order/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                    <a   href='#' onclick='downloadPDF(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
                    <a href='#' onclick='printDotMatrixOrder(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print dot" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
                    `; */
        var html = `<a href="/order/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                    <a href='#' onclick='downloadNewPDF(${row.id},${row.customerId});' class="mb-1 mt-1 me-1 btn btn-sm btn-warning text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="PDF" data-bs-original-title="PDF" aria-label="Print"><i class="fa fa-file-pdf"></i></a>
  <a href='#' onclick='printDotMatrixOrder(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print dot matrix" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
                    `;
        if (row.orderStatusId != 4 && row.orderStatusId != 3) { //if order is not delivered
            html += `<span data-bs-toggle="modal" class="open-order-cancel-modal" data-id="${row.id}" data-bs-target="#order-cancel"><a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Order Cancel" data-bs-original-title="Order Cancel" aria-label="Order Cancel" ><i class="fa-solid fa-xmark"></i></a> </span>`;
        }
        if (row.orderStatusId == 2 && (row.paymentStatusId == 1 || row.paymentStatusId == 5)) { //unpaid + failed + not delivered
            /*html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>*/
            html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href='#' onclick='SendQpay(${row.id},${row.customer.mobileNumber},${row.customerId},${row.total},${row.orderNumber},1);' class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>`;

        }






        return html;
    }

    //  searchDataTableSummary();
}
searchUnpaidDataTable = () => {

    if ($.fn.dataTable.isDataTable(selectedDataTableName)) {
        $(selectedDataTableName).DataTable().destroy();
    }

    $(selectedDataTableName).DataTable({
        responsive: true,
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
                showLog('error' + error);
            },
        },

        "columns": [
            { "data": "id" },
            { "data": "orderNumber" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "orderTypeId", render: function (data, type, row) { return getOrderTypeHtml(row); } },
            { "data": "customer.name" },
            { "data": "customer.mobileNumber" },
            { "data": "deviceTypeId", render: function (data, type, row) { return getDeviceTypeHtml(row); }, },
            { "data": "total", "name": "total" },
            { "data": "paymentMethodId", render: function (data, type, row) { return getPaymentMethodHtml(row); }, },
            { "data": "paymentStatusId", render: function (data, type, row) { return getPaymentStatusHtml(row); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getOrderStatusHtml(row); }, },
            { "data": "deliveryDate", render: function (data, type, row) { return getFormatedDate(row.deliveryDate); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getDeliveryStatusHtml(row); }, },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
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
                    <a href='#' onclick='downloadNewPDF(${row.id},${row.customerId});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
                    `;
        if (row.orderStatusId != 4 && row.orderStatusId != 3) { //if order is not delivered
            html += `<span data-bs-toggle="modal" class="open-order-cancel-modal" data-id="${row.id}" data-bs-target="#order-cancel"><a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Order Cancel" data-bs-original-title="Order Cancel" aria-label="Order Cancel" ><i class="fa-solid fa-xmark"></i></a> </span>`;
        }
        if (selectedTab != 0 && row.orderStatusId != 4) { //unpaid + failed + not delivered
            /*html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>*/
            html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href='#' onclick='SendQpay(${row.id},${row.customer.mobileNumber},${row.customerId},${row.total},${row.orderNumber},1);' class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>`;

        }

        return html;
    }

    //  searchDataTableSummary();
}
searchFailedDataTable = () => {

    if ($.fn.dataTable.isDataTable(selectedDataTableName)) {
        $(selectedDataTableName).DataTable().destroy();
    }

    $(selectedDataTableName).DataTable({
        responsive: true,
        searching: true,
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
                showLog('error' + error);
            },
        },

        "columns": [
            { "data": "id" },
            { "data": "orderNumber" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "orderTypeId", render: function (data, type, row) { return getOrderTypeHtml(row); } },
            { "data": "customer.name" },
            { "data": "customer.mobileNumber" },
            { "data": "deviceTypeId", render: function (data, type, row) { return getDeviceTypeHtml(row); }, },
            { "data": "total", "name": "total" },
            { "data": "paymentMethodId", render: function (data, type, row) { return getPaymentMethodHtml(row); }, },
            { "data": "paymentStatusId", render: function (data, type, row) { return getPaymentStatusHtml(row); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getOrderStatusHtml(row); }, },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
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
        var html = `<a href="/order/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                    `;

        // <a href='#' onclick='downloadNewPDF(${row.id},${row.customerId});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>

        //if (row.orderStatusId != 4 && row.orderStatusId != 3) { //if order is not delivered
        //    html += `<span data-bs-toggle="modal" class="open-order-cancel-modal" data-id="${row.id}" data-bs-target="#order-cancel"><a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Order Cancel" data-bs-original-title="Order Cancel" aria-label="Order Cancel" ><i class="fa-solid fa-xmark"></i></a> </span>`;
        //}
        //if (selectedTab != 0 && row.orderStatusId != 4) { //unpaid + failed + not delivered
        //    /*html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>*/
        //    html += `<span data-bs-toggle="modal" class="open-send-payment-link-modal" data-id="${row.id}" data-customerId="${row.customerId}" data-bs-target="#send-payment-link-modal"> <a href='#' onclick='SendQpay(${row.id},${row.customer.mobileNumber},${row.customerId},${row.total},${row.orderNumber},1);' class="mb-1 mt-1 me-1 btn btn-sm btn-info"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Send Payment Link" data-bs-original-title="Send Payment Link" aria-label="Send Payment Link" ><i class="fas fa-money-bill-wave"></i></a> </span>`;

        //}

        return html;
    }

    //  searchDataTableSummary();
}


searchDataTableSummary = () => {

    ajaxGetCart("Order/OrderSalesSummary?customerId=" + $("#customerId").val(),
        function (data) {
            if (data.success) {

            }
        });

}


$("#btnclear").click(function () {
    $('#startDate').val('');
    $('#endDate').val('');
});


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


downloadNewPDF = (orderId, customerId) => {
    //ajaxGet('Order/getorderpdfNew?id=' + orderId + '&customerId=' + customerId, cbGetPDFSuccess);

    var lang = (document.documentElement.lang == "en" ? true : false);
    showLoader();
    //url: "https://localhost:44380/webapi/order/getorderpdfadmin?id=" + orderId + "&customerId=" + customerId + "&isEnglish=" + lang,
    $.ajax({
        url: "https://apiad.adhari.com.kw/webapi/order/getorderpdfadmin?id=" + orderId + "&customerId=" + customerId + "&isEnglish=" + lang,
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            showLog(data);
            cbGetPDFSuccess(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {

            if (callbackGetError) {
                showLog(jqXHR);
                callbackGetError(jqXHR);
            };
            hideLoader();
        }
    });
}

cbGetPDFSuccess = (data) => {
    hideLoader();
    window.open(data.data, "_blank");
}


SendQpay = (OrderID, MobileNumber, CustomerId, Total, OrderNumber, OrderModeID) => {
    //alert(MobileNumber);
    setTextValue("dialogMobileNumber", MobileNumber);
    setTextValue("dialogOrderId", OrderID);
    setTextValue("dialogCustomerId", CustomerId);
    setTextValue("dialogTotal", Total);
    setTextValue("dialogOrderNumber", OrderNumber);
    setTextValue("dialogOrderTypeID", OrderModeID);


}


sendPaymentLink = () => {


    var OrderID = getTextValue("dialogOrderId");
    var CustomerId = getTextValue("dialogCustomerId");
    var Total = getTextValue("dialogTotal");
    var OrderNumber = getTextValue("dialogOrderNumber");
    var OrderTypeID = getTextValue("dialogOrderTypeID");

    var endpoint = getAPIUrl() + "Order/SendQpay?CustomerID=" + CustomerId +
        "&OrderID=" + OrderID + "&OrderNumber=" + OrderNumber + "&Ordertotal=" + Total + "&OrderType=" + OrderTypeID;

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





printDotMatrixOrder = (orderId) => {
    ajaxGet('Order/GetDotMatrixOrderPDF?id=' + orderId, cbGetDotMatrixSuccess);
    //ajaxGet('Order/downloadNewPDF?id=' + orderId , cbGetPDFSuccess);
}
cbGetDotMatrixSuccess = (data) => {
    /*window.open(data.data, "_blank");*/
    CallPrint(data.data);
}
CallPrint = (strid) => {
    //debugger;


    //$.get(strid,

    //    { '_': $.now() } // Prevents caching

    //).done(function (data) {

    //    // Here's the HTML
    //    var html = data;


    var prtContent = strid;
        var WinPrint = window.open('', '', 'letf=0,top=0,width=600,height=600,toolbar=0,scrollbars=0,status=0');
        WinPrint.document.write(prtContent);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();

    //}).fail(function (jqXHR, textStatus) {

    //    // Handle errors here

    //});
   

    //alert(strid);
    //var obj = "#" + strid;


}