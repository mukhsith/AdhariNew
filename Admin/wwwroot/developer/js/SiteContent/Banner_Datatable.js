

$(document).ready(function () {

  
    
searchDataTable();
    
   // ajaxGet('Banner/GetAll', , undefined);
});

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
       
        "ajax": {
            url: getAPIUrl() + "Banner/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
                showLoader();
                //d.customerName = getTextValue("customerName");
                //d.customerEmail = getTextValue("customerEmail");
                //d.mobileNumber = getTextValue("mobileNumber");
                //d.createdOn = getTextValue("createdOn");
            },
            "datatype": "json",
            "dataSrc": function (json) {
               // showLog(json);
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
                    return addCheckedAction('Banner/ToggleActive', row);
                }
            }, 
            {
                "data": "imageUrlEn", render: function (data, type, row) {
                    return `<img src='${row.imageUrlEn}' height='50'>`;
                }
            },
            {
                "data": "imageUrlAr", render: function (data, type, row) {
                    return `<img src='${row.imageUrlAr}' height='50'>`;
                }
            }, 
            {
                "data": "linkType", render: function (data, type, row) {
                    return `${(row.linkType == 0 ? 'None' : row.linkType == 1 ? 'Link' : 'Product')}`;
                }
            }, 
            {
                "data": "linkUrl", render: function (data, type, row) {
                    return `${(row.linkType == 0 ? 'None' : row.linkType == 1 ? row.linkUrl : row.product.nameEn)}`;
                }},
            {
                "data": null, "name":"Actions", render: function (data, type, row) {
                    return `${addEditAction('Banner','/SiteContent/BannerAddEdit/', row) } ${ addPopupAction('Banner', row) }`;
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
            { 'width': '10%', 'targets': [0, 1] },
            { 'width': '10%', 'targets': [2, 3, 4] },
            { "className": "text-wrap", "targets": "_all" },
        ],

    });

 
}

 
