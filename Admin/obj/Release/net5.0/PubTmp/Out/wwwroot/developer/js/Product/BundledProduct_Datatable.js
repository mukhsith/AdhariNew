
$(document).ready(function () {
    searchDataTable();
});

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "BundledProduct/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
                var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
            },
            "datatype": "json",
            "dataSrc": function (json) {
                //showLog(json);
                //hideLoader();
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
                "data": "displayOrder", "name": "displayOrder",  render: function (data, type, row) {
                    return `<td data-displayorder='${row.id}'> ${row.displayOrder}</td>`;
                }
            },
            {
                "data": "active", render: function (data, type, row) {
                    return addCheckedAction('BundledProduct/ToggleActive', row);
                }
            },
            {
                "data": "imageUrl", render: function (data, type, row) {
                    return `<img src='${row.imageUrl}' width='50' height='50'>`;
                }
            },
            {
                "data": "nameEn", render: function (data, type, row) {return row.nameEn;}
            },
            {
                "data": "nameAr", render: function (data, type, row) { return row.nameAr; }
            },
            
            {
                "data": "data.packages", render: function (data, type, row)
                {
                    return GetProductDetails(row);
                }
            },
            {
                "data": null, "name":"Actions", render: function (data, type, row) {
                    return `${addEditAction('Bundled Product','/Product/BundledProductAddEdit/', row)} ${addPopupAction('Product', row) }`;
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

    GetProductDetails = (row) => {
        var html = `<td data-th="Contents">
                    <ul class="body-bg-secondary border border-secondary rounded-3 text-muted p-2 m-0">`;
        for (var index = 0; index < row.productDetails.length; index++) {
            var item = row.productDetails[index];
            html += `<li class="d-flex justify-content-between mb-1">
                        <p class="m-0"><a href="#">${item.productName} ${item.piecesPerPacking} (${item.itemSize})</a></p> <span>x${item.quantity}</span>
                    </li>`;            
        }
        html += `</ul></td>`;
        return html;
    }
}



    
 
 
