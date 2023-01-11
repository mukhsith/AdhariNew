
$(document).ready(function () {
    searchDataTable(); 
    fillDropDownList("governorateList", 'governorate/GetAll', false, null, "id", "nameEn");
});
 
    
searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "Area/GetAllForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() }, 
            data: function (d) {
                var search = $(":input[type=search]").val();
                d.governorateId  = getSelectedItemValue("governorateList");
                if (search.length <= 0) { showLoader(); }
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                hideLoader();
                return json.data;
            }
        },
                   
        "columns": [
            {    "data": "id", "name": "id"},
            {
                "data": "displayOrder", "name": "displayOrder",  render: function (data, type, row) {
                    return `<td data-displayorder='${row.id}'> ${row.displayOrder}</td>`;
                }
            },
            {
                "data": "active", render: function (data, type, row) {
                    return addCheckedAction('Area/ToggleActive', row);
                }
            },
            {
                "data": "nameEn", render: function (data, type, row) {return row.nameEn;}
            },
            {
                "data": "nameAr", render: function (data, type, row) { return row.nameAr; }
            },
            {
                "data": "deliveryFee", render: function (data, type, row) { return row.deliveryFee; }
            },
            {
                "data": "governorate.nameEn", render: function (data, type, row) {
                    return (row.governorate == null ? "None" : row.governorate.nameEn);
                }
            },
            {
                "data": null, "name":"Actions", render: function (data, type, row) {
                    return `${addEditAction('Area','/Delivery/AreaAddEdit/', row)} ${addPopupAction('Governorate', row) }`;
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

 
