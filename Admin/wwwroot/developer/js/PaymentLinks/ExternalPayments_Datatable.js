var selectedTab = 0;
selectedDataTableName = "#paid-datatable-default-";
selectedunpaidDataTableName = "#unpaid-datatable-default-";

$(document).ready(function () {
    configureActions(); 
    searchForDataTable();
    searchForunpaidDataTable();
}); 
configureActions = () => {
    $('.tabs ul li a').click(function () {
        var action = $(this).attr("href");
        if (action == "#paid") {
            selectedTab = 0;
            selectedDataTableName = "#paid-datatable-default-";
            searchForDataTable();
         } else { 
            selectedTab = 1;
            selectedunpaidDataTableName = "#unpaid-datatable-default-";
            searchForunpaidDataTable();
        }
       
        showLog(action);
    });
}
 
searchForDataTable = () => {
    if ($.fn.dataTable.isDataTable(selectedDataTableName)) {
        $(selectedDataTableName).DataTable().destroy();
    } 
   
    $(selectedDataTableName).DataTable({
        responsive: true,
        searching: false,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "PaymentLinks/GetAllForCustomDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                 //var search = $(":input[type=search]").val();
                 //if (search.length <= 0) { showLoader(); }
                d.selectedTab = selectedTab; 
                d.paymentLinkID = getTextValue("paymentLinkID");
                d.customerMobile = getTextValue("customerMobile");
                d.startDate = getDatePickerValue("startDate");
                d.endDate = getDatePickerValue("endDate");
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
                showLog('error'+error);
            },
        }, 
        "columns": [
            { "data": "id" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "mobileNumber" },
            { "data": "amount" },
            { "data": "paymentResult" },
            { "data": "paymentTrackId" },
            { "data": "paymentAuth"},
            { "data": "paymentId" },
            { "data": "paymentMethodName" },
            { "data": null, render: function (data, type, row) { return getPaymentStatusHtml(row);  }, },
        ],
  
    }); 
      
} 
 
  

searchForunpaidDataTable = () => {
    if ($.fn.dataTable.isDataTable(selectedunpaidDataTableName)) {
        $(selectedunpaidDataTableName).DataTable().destroy();
    }

    $(selectedunpaidDataTableName).DataTable({
        responsive: true,
        searching: false,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "PaymentLinks/GetAllForCustomDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                //var search = $(":input[type=search]").val();
                //if (search.length <= 0) { showLoader(); }
                d.selectedTab = selectedTab;
                d.paymentLinkID = getTextValue("paymentLinkID");
                d.customerMobile = getTextValue("customerMobile");
                d.startDate = getDatePickerValue("startDate");
                d.endDate = getDatePickerValue("endDate");
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
            { "data": "id" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "mobileNumber" },
            { "data": "amount" },
            { "data": "paymentResult" },
            { "data": "paymentTrackId" },
            { "data": "paymentAuth" },
            { "data": "paymentId" },
            { "data": "paymentMethodName" },
           /* { "data": null, render: function (data, type, row) { return getPaymentStatusHtml(row); }, },*/
        ],

    });

}