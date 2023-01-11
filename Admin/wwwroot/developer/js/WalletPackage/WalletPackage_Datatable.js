
$(document).ready(function () {
    searchDataTable();  
});
 
    
searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        responsive:true,
        searching: true,
        serverSide:true,
        "ajax": {
            url: getAPIUrl() + "WalletPackage/GetAllForDataTable",
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
            }
        },
         
        "columns": [
            {    "data": "id", "name": "id"}, 
            {
                "data": "active", render: function (data, type, row) {
                    return addCheckedAction('WalletPackage/ToggleActive', row);
                }
            },
            {
                "data": "nameEn", render: function (data, type, row) {return row.nameEn;}
            },
            {
                "data": "nameAr", render: function (data, type, row) { return row.nameAr; }
            },
            {
                "data": "descriptionEn", render: function (data, type, row) { return row.descriptionEn; }
            },
            {
                "data": "descriptionAr", render: function (data, type, row) { return row.descriptionAr; }
            },
            {
                "data": "amount", render: function (data, type, row) { return row.amount; }
            },
            {
                "data": "walletAmount", render: function (data, type, row) { return row.walletAmount; }
            },
            {
                "data": null, "name":"Actions", render: function (data, type, row) {
                    return `${addEditAction('WalletPackage','/WalletPackage/AddEdit/', row)}`;
                }
            },  
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

 
