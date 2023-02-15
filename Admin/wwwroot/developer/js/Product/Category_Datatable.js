
$(document).ready(function () {

    configureDialogModels();
    searchDataTable();
   
});


configureDialogModels = () => {

    //$(document).on("click", ".open-order-status", function () {
    //    $("#order-status .modal-header #dialogOrderId").val($(this).data('id'));

    //});

    $(document).on("click", ".open-category-cancel-modal", function () {
        $("#category-cancel .modal-header #dialogCategoryId").val($(this).data('id'));
    });
    

}


searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "Category/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
               // showLoader(); 
            },
            "datatype": "json",
            "dataSrc": function (json) {
               // showLog(json);
              //  hideLoader();
                checkAPIResponse(json);
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
                    return addCheckedAction('Category/ToggleActive', row);
                }
            },
            {
                "data": "nameEn", render: function (data, type, row) {return row.nameEn;}
            },
            {
                "data": "nameAr", render: function (data, type, row) { return row.nameAr; }
            },
            {
                "data": "imageUrl", render: function (data, type, row) {
                    return `<img src='${row.imageUrl}' width='50' height='50'>`;
                }
            },
            {
                "data": "Products", render: function (data, type, row) { return row.id; }
            },
           
                //"data": null, "name":"Actions", render: function (data, type, row) {
                //    return `${addEditAction('Category','/Product/CategoryAddEdit/', row)} ${addPopupAction('Category', row) }`;
                //}
                 { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
             
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

    getActionsHtml = (row) => {
        var html = `${addEditAction('Category', '/Product/CategoryAddEdit/', row)} ${addPopupAction('Category', row)}`;


        html += `<span data-bs-toggle="modal" class="open-category-cancel-modal" data-id="${row.id}" data-bs-target="#category-cancel"><a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Category Delete" data-bs-original-title="Category Delete" aria-label="Category Delete" ><i class="fa-solid fa-xmark"></i></a> </span>`;
       






        return html;
    }



}

 
