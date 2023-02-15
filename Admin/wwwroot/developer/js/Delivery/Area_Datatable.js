
$(document).ready(function () {
    searchDataTable(); 
    /* fillDropDownList("governorateList", 'governorate/GetAll', false, null, "id", "nameEn");*/
    fillDropDownList("governorateList", 'Governorate/ForDropDownList', false, null, "id", "name");
});
 
    
searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        responsive: true,
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "Area/GetAllForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() }, 
            data: function (d) {
                 var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
              
                d.governorateId = getSelectedItemValue("governorateList");
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
              /*  checkAPIResponse(json);*/
                hideLoader();
                return json.data;
            },
            error: function (error) {
                showLog('error' + error);
            },
        },
                   
        "columns": [
            { "data": "id", "name": "id" },
            {
                "data": "displayOrder", "name": "displayOrder", render: function (data, type, row) {
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
                "data": "governorate.nameEn", render: function (data, type, row) {
                    return (row.governorate == null ? "None" : row.governorate.nameEn);
                }
            },
            {
                "data": "governorate.nameAr", render: function (data, type, row) {
                    return (row.governorate == null ? "None" : row.governorate.nameAr);
                }
            },
            {
                "data": "deliveryFee", render: function (data, type, row) { return row.deliveryFee; }
            },
            {
                "data": "minOrderAmount", render: function (data, type, row) { return row.minOrderAmount; }
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

 
