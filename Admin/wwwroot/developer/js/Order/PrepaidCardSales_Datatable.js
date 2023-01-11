 
selectedTab = 0;
selectedDataTableName = "#paid-datatable-default-";
$(document).ready(function () { 
    searchDataTable();  
    configureActions();
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
        responsive: true,
        searching: false,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "walletPackageOrder/GetAllForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                //var search = $(":input[type=search]").val();
                //if (search.length <= 0) { showLoader(); }
                d.selectedTab = selectedTab;
                d.paymentId = getTextValue("paymentId");
                d.startDate = getDatePickerValue("startDate");
                d.endDate = getDatePickerValue("endDate");
                d.customerName = getTextValue("customerName");
                d.customerMobile = getTextValue("mobileNumber");
                d.customerEmail = getTextValue("customerEmail");
                d.prepaidCardId = getSelectedItemValue("prepaidCardList");
                d.paymentMethodId = getSelectedItemValue("paymentMethodList");

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
            { "data": "paymentId" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "customerName" },
            { "data": "mobileNumber" },
            { "data": "walletPackageName" },
            { "data": "walletPackageAmount" },
            { "data": "paymentMethodId", render: function (data, type, row) { return getPaymentMethodHtml(row); }, },
            { "data": "paymentStatusId", render: function (data, type, row) { return getPaymentStatusHtml(row); }, },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
        ],
        columnDefs: [{
            'targets': 7,
            'width': "13.5%"
        }],
    });
}
    getOrderTypeHtml = (row) => {
        if (row.orderTypeId == 1) {
            return `<span class='px-2 rounded-pill fw-bold text-light bg-success'>Online</span>`;
        } else {
            return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>Offline</span>`;
        }
    }
    orderStatus = (itemData) => {
        return "<td><span class='font-weight-bold' style='color:" + itemData.statusColor + "'>" + itemData.status + "</span></td>";
    }
    
     
    getActionsHtml = (row) => {
        var html = `<a href="/order/orderDetails?id=${row.id}&customerId=${row.customerId}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>
                    <a href="" class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Print" data-bs-original-title="Print" aria-label="Print"><i class="fa fa-print"></i></a>
                     `;
  
        return html;
    }


configureDialogModels = () => {

    //_DispatchOrderModalPartial
    $(document).on("click", ".open-order-status", function () {
        $("#order-status .modal-header #dialogOrderId").val($(this).data('id'));
         
    });
      
}


