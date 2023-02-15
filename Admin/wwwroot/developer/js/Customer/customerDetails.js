var selectedTab = 0;
selectedDataTableName = "#paid-datatable-default-";
$(document).ready(function () {

    //  fillDropDownList("governorateList", "Governorate/ForDropDownList", "id", "name", getGovernorateList);
    //fillDropDownList("areaList", "area/ForDropDownList", "id", "name");
    getCustomer();
  //  alert(Resources.AreYouSureTheOrderIsDelivered);
    configureDialogModels();
 
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
                    `; */
        var html = `<a href="/order/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                    <a href='#' onclick='downloadNewPDF(${row.id},${row.customerId});' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
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



EditAddress = () => {

    var selectedAddressId = $('input[type=radio][name=address]:checked').val();


    setTextValue("dialogCustomerId", getTextValue('customerId'));
    setTextValue("dialogAddressId", selectedAddressId);


    ajaxGet('Governorate/ForDropDownList', geteditGovernorateList);

}



getCustomer = () => {
    var Id = $("#customerId").val();
    ajaxGet('customer/GetById?customerId=' + getTextValue('customerId'), getCustomerSuccess)
}


getGovernorateList = (data) => {
    console.log(data);
    fillDropDownListData("governorateList", data.data, false, null, "id", "name");
}


GetCityDropDownData = (id) => {
    ajaxGet('Area/ForDropDownListByCity?governorateId=' + id, getAreaList);

}
getAreaList = (data) => {
    console.log(data);
    fillDropDownListData("areaList", data.data, false, null, "id", "name");
}

$(document).on("change", "input[type=radio][name=address]", function () {
    var selectedTab = $(this).attr('id');
    console.log(selectedTab);
    $(this).closest('.addresses').find('.inputGroup').removeClass('active');
    $(this).closest('.inputGroup').addClass('active');
    $('.continue-btn').removeClass('disabled');
});


addCustomerAddress = () => {
    var addressType = $(".delivery-addresses .active a").attr('id');
    console.log(addressType);
}
 
getCustomerSuccess = (data) => {

    if (data.data.name != null) {
        var d = data.data;
        $("#customerId").val(d.id);
        $("#customerName").html(d.name);
        $("#customerEmail").html(d.emailAddress);
        $("#customerMobile").html(d.mobileNumber);

        loadCustomerAddress();

    } 
}


loadCustomerAddress = () => {
    ajaxGet('customer/getAllAddress?id=' + getTextValue("customerId"), showAddressListSuccess);

}
showAddressListSuccess = (data) => {

    if (data == null) { return; }
    var address = $("#display-addresses .row .addresses").html('');
    if (data.data.length > 0) {
        $("#noSavedAddress").hide();

        $(data.data).each(function (index, item) {
            var html = `<div class="col-12 col-md-4 mb-3">
                        <div class="inputGroup ${index === 0 ? "active" : ""} h-100 border border-secondary rounded-3 body-bg-secondary d-flex justify-content-between align-items-center p-3">
                            <label for="address-0" class="w-100">
                                <p class="address-name card-text mb-0 fw-bold">${item.name}</p>
                                <p class="address-content card-text mb-0"> ${item.addressText}</p>
                            </label>
                                <input data-id="${item.id}" id="${item.id}" name="address" value="${item.id}" onclick="saveAttributes()" type="radio" ${index === 0 ? "checked" : ""} >
                        </div>
                    </div>`;
            address.append(html);
        });
    }
    else {
        
        $("#noSavedAddress").show();
        $("#editAdd").hide();
    }

    //for (const r of data.data) { //no of rows
    //    var html = `<div class="col-12 col-md-4 mb-3">
    //                    <div class="inputGroup h-100 border border-secondary rounded-3 body-bg-secondary d-flex justify-content-between align-items-center p-3">
    //                        <label for="address-0" class="w-100">
    //                            <p class="address-name card-text mb-0 fw-bold">${r.name}</p>
    //                            <p class="address-content card-text mb-0"> ${r.addressText}</p>
    //                        </label>
    //                            <input data-id="${r.id}" id="${r.id}" name="address" type="radio" >
    //                    </div>
    //                </div>`;
    //    address.append(html);
    //}
    //ajaxGet('Product/GetAllProductAndCategoryForOfflineOrder?customerId=' + getTextValue("customerId"), cbGetProductAndCategoryList);
}
 

disableFields = (enable) => {
    clearFields();
    $("#customerName").attr("disabled", enable);
    $("#customerEmail").attr("disabled", enable)
    $("#customerTypeList").attr("disabled", enable);

}


$(".edit-btn").click(function () {
    $(this).hide();
    $(".update-btn").show();
    $(".customer-details").removeAttr("disabled");
    $("select#CustomerType").select2('enable');
});

$(".update-btn").click(function () {
    $(this).hide();
    $(".edit-btn").show();
    $(".customer-details").attr("disabled", true);
    $("select#CustomerType").select2('enable', false);
});
