
$(document).ready(function () {
    configureDialogModels();
    popupStockSettings();
    ajaxGet('Product/GetAllCategoryAndItemSize', cbGetList);
});


cbGetList = (data) => {
   
    fillDropDownListData("categoryList", data.data.category, false, null, "id", "nameEn");
    searchDataTable();
}


configureDialogModels = () => {

    //$(document).on("click", ".open-order-status", function () {
    //    $("#order-status .modal-header #dialogOrderId").val($(this).data('id'));

    //});

    $(document).on("click", ".open-product-cancel-modal", function () {
        $("#product-cancel .modal-header #dialogProductId").val($(this).data('id'));
    });


}


cbCancelOrderSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'product', 'product deleted successfully');
        setTimeout(() => location.href = "/product/productList", 2500);

    } else {
        ToastAlert('error', 'product', data.message);
    }
}

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "Product/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
                var search = $(":input[type=search]").val();
                d.productName = getTextValue("productName");
                d.categoryID = getSelectedItemValue("categoryList");

               // if (search.length <= 0) { showLoader(); }
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
                "data": "displayOrder", "name": "displayOrder",  render: function (data, type, row) {
                    return `<td data-displayorder='${row.id}'> ${row.displayOrder}</td>`;
                }
            },
            {
                "data": "active", render: function (data, type, row) {
                    return addCheckedAction('Product/ToggleActive', row);
                }
            },
            {
                "data": "imageUrl", render: function (data, type, row) {
                    return `<img src='${row.imageUrl}' width='50' height='50'>`;
                }
            },
            {
                "data": "category.nameEn", render: function (data, type, row) { return row.category.nameEn; }
            },
            {
                "data": "category.nameAr", render: function (data, type, row) { return row.category.nameAr; }
            },
            {
                "data": "nameEn", render: function (data, type, row) {return row.nameEn;}
            },
            {
                "data": "nameAr", render: function (data, type, row) { return row.nameAr; }
            },
            
            {
                "data": "stock", render: function (data, type, row) { return row.stock; }
            },
            { "data": null, render: function (data, type, row) { return getActionsHtml(row); }, },
            //{
            //    "data": null, "name":"Actions", render: function (data, type, row) {
            //        return `${addEditAction('Product', '/Product/ProductAddEdit/', row)} ${addPopupAction('Product', row)}   ${restock(row)}`;
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
            
            { 'width': '10%', 'targets': [0, 1] },
            { 'width': '10%', 'targets': [2, 3, 4] },
            { "className": "text-wrap", "targets": "_all" },
        ],
        
    });


    getActionsHtml = (row) => {
        var html = `${addEditAction('Product', '/Product/ProductAddEdit/', row)} ${addPopupAction('Product', row)} ${restock(row)}`;

        html += `<span data-bs-toggle="modal" class="open-product-cancel-modal" data-id="${row.id}" data-bs-target="#product-cancel"><a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-danger"  data-bs-toggle="tooltip" data-bs-placement="bottom" title="Product Delete" data-bs-original-title="Product Delete" aria-label="Product Delete" ><i class="fa-solid fa-xmark"></i></a> </span>`;

        return html;
    }

}

restock = (row) => {
    return `<span> 
                <a href="javascript:;" onclick="javascript:doUpdateStock(${row.id},${row.productType},${row.stock});" class="mb-1 mt-1 me-1 btn btn-sm btn-success" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Update Stock"
                    data-bs-original-title="Restock" aria-label="Restock" >
                    <i class="fa-solid fa-boxes-stacked"></i>
                </a>
            </span>`;
} 
doUpdateStock = (productId, productType, stock) => {
    $('#productId').val(productId);
    $('#productType').val(productType);
    $('.source-stock').html(stock);
    var myModal = new bootstrap.Modal(document.getElementById('stock_modal'), {
        keyboard: false
    }).show();
}

popupStockSettings = () => {
    $('.stock-action-option li').click(function () {
        $('.stock-action-option li').removeClass('active');
        $(this).addClass('active');
        $('.stock-action-button').html($('.stock-action-option li.active').find('a').html());
        performCalculationAndSendToUpdatedStock();
    });
    $('#stockInput').change(function () {
        performCalculationAndSendToUpdatedStock();
    });
    performCalculationAndSendToUpdatedStock = () => {
        var sourceStock = parseInt($('.source-stock').html());
        var updatedStock = sourceStock;

        var action = $('.stock-action-option li.active').find('a').attr("data-action");
        switch (action) {
            case "1":
                updatedStock = updatedStock + parseInt($('#stockInput').val());
                break;
            case "2":
                updatedStock = updatedStock - parseInt($('#stockInput').val());
                break;
            case "3":
                updatedStock = parseInt($('#stockInput').val());
                break;
            default:
            // code block
        }
        $('.updated-stock').html(updatedStock);
    }
}

updateStock = () => { 
    ajaxPost("Product/UpdateStock", GetSubmitFormObject(), cbPostSuccess);
 }
cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'Product Stock', "Product Stock Updated Successfully");
        setTimeout(reloadPage(), 1000);
      
    }
}
GetSubmitFormObject = () => {
    let submitData = new FormData();
    submitData.append('productId', $("#productId").val());
    submitData.append('productType', $("#productType").val());
    submitData.append('productUpdateType', 2); //Manual Adjustment
    var actionType = $('.stock-action-option li.active').find('a').attr("data-action");
    
    submitData.append('productActionType', actionType);
    submitData.append('oldStock', parseInt($(".source-stock").text()));
    submitData.append('updatedStock', parseInt($(".updated-stock").text()));
    submitData.append('inputStock', parseInt($('#stockInput').val()));
    submitData.append('note', $("#stockNote").val());
    return submitData;
}

