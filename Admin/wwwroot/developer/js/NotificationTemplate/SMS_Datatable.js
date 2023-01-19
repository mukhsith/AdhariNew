
$(document).ready(function () {
    searchDataTable(); 
});

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({ 
        searching: false,
        serverSide:true,
        "ajax": {
            url: getAPIUrl() + "Notification/GetAllForSMSDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
                 showLoader(); 
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                hideLoader();
                checkAPIResponse(json);
                return json.data;
            }
        }, 
                              
                            
                             
        "columns": [
            { "data": "id"},
            { "data": "message" },
            { "data": "mobileNumber" },
            { "data": "scheduleDate", render: function (data, type, row) { return getFormatedDate(row.scheduleDate); } },
            //{
            //    "data": null, "name":"Actions", render: function (data, type, row) {
            //        return `${addEditAction('Notification Template','/Notification/NotificationTemplateEdit/', row)} `;
            //    }
            //},  
        ],
         
        createdRow: function (row, data, index) {
            //change display order value, to avoid page refresh
            $(row).attr('data-rowid', data.id);
            $(row).find('td:eq(1)').attr('data-displayorder', row.id);

            //    Reinitialize ios-switch
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
       
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],
        
    });
}

 
