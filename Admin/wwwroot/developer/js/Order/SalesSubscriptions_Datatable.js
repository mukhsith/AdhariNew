var selectedTab = 0;
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
    console.log('searchDataTable');
    $(selectedDataTableName).DataTable({
        responsive:true,
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "subscription/GetSubscriptionsForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                console.log('query');
                 var search = $(":input[type=search]").val();
                 if (search.length <= 0) { showLoader(); }
                d.selectedTab = selectedTab;
                d.customerId = getTextValue("customerId"); //from customerList to show specific customer details
                d.subscriptionId = getTextValue("subscriptionId");
                d.startDate = getDatePickerValue("startDate");
                d.endDate = getDatePickerValue("endDate");
                d.customerName = getTextValue("customerName");
                d.customerMobile = getTextValue("customerMobile");
                d.customerEmail = getTextValue("customerEmail");
                d.paymentMethodId = getSelectedItemValue("paymentMethodList");
                d.orderStatusId = getSelectedItemValue("subscriptionStatusList");
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                checkAPIResponse(json);
                hideLoader();
                return json.data;
            },
            error: function (error) {
                console.log('error');
                showLog('error'+error);
            },
        },
        
        "columns": [
             { "data": "subscriptionId" },
             { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } }, 
             { "data": "name" },
             { "data": "mobileNumber" },
             { "data": "formattedTotal" },
             { "data": "paymentMethodId", render: function (data, type, row) { return getPaymentMethodHtml(row); }, },
            { "data": "orderStatusId", "name": "orderStatusId", render: function (data, type, row) { return getSubscriptionStatusHtml(row); }, },
             { "data": null, render: function (data, type, row) { return getActionsHtml(row);  }, },
        ],
        columnDefs: [{
            'targets': 7,
            'width': "13.5%"
        }],
    });

   
  
}
getDeliveredHtml = (row) => {
    if (row.delivered) {
        return `<span class='px-2 fw-bold text-success'>Confirmed</span>`;
    } else {
        return `<span class='px-2 fw-bold text-danger'>Expired</span>`;
    } 
}

getActionsHtml = (row) => {
 
    var html = `<a href ="/order/sales-subscriptionDetails?id=${row.subscriptionId}&customerId=${row.customerId}&subscriptionNumber=${row.subscriptionNumber}" class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Order" data-bs-original-title="View Order" aria-label="View Order"><i class="fa fa-eye"></i></a>`;
    if (row.subscriptionStatusId != 3 || row.subscriptionStatusId != 4) { //if not expired nor cancelled
        html += `<span data-bs-toggle="modal" class="open-subscription-status" data-bs-target="#subscription-status"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-warning"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Change Status" data-bs-original-title="Change Status" aria-label="Change Status" ><i class="fas fa-edit"></i></a> </span>`;
     }
      
    return html;
}

configureDialogModels = () => {
     
    $(document).on("click", ".open-subscription-status", function () {
        $("#subscription-status .modal-header #dialogSubscriptionId").val($(this).data('id'));
         
    }); 
}
 
 
//
//{ "data": "paymentStatusId", render: function (data, type, row) { return getPaymentStatusHtml(row); }, },
