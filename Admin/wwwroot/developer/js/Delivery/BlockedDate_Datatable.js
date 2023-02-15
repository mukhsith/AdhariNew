$(document).ready(function () {
    searchDataTable(); 
});

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        searching: true,
        serverSide:true,
        "ajax": {
            url: getAPIUrl() + "DeliveryBlockedDate/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
                var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                hideLoader();
                checkAPIResponse(json);
                return json.data;
            },
            error: function (error) {
                showLog(error);
            },
        }, 
        "columns": [
            {    "data": "id", "name": "id"},
               
            {
                "data": "fromDate", render: function (data, type, row) { return getFormatedDate(row.fromDate);}
            },
            {
                "data": "toDate", render: function (data, type, row) { return getFormatedDate(row.toDate); }
            },
             {
                 "data": "modifiedOn", render: function (data, type, row) { return getFormatedDateTime(row.modifiedOn); }
            },
            {
                "data": "modifiedByUserName", render: function (data, type, row) { return row.modifiedByUserName; }
            },
            
            {
                "data": "note", render: function (data, type, row) {
                    return `<td data-th="Note">${row.note}</td>`; }
            },
            {
                "data": "active", render: function (data, type, row) {
                    return addCheckedAction('DeliveryBlockedDate/ToggleActive', row);
                }
            },
        ],
         
        createdRow: function (row, data, index) { 
            //    Reinitialize ios-switch
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
       
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],
        
    
    });
}

 
