var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    ajaxGet('Product/GetAllCategoryAndItemSize', cbGetList);
    setup();
    
});

cbGetList = (data) => { 
    fillDropDownListData("itemSizeList", data.data.itemSize, false, null, "id", "nameEn");
    fillDropDownListData("categoryList", data.data.category, false, null, "id", "nameEn");
    loadDataFor();
}
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            nameEn: { required: true },
            nameAr: { required: true },
            descriptionEn: { required: true },
            descriptionAr: { required: true },
            piecePerPacking: { required: true },
            itemSizeList: { required: true },
            stock: { required: true },
            categoryList: { required: true },
            imageFile: { required: true },
            price: { required: true },
        },
        messages: { 
            nameEn: { required: 'Required' },
            nameAr: { required: 'Required' },
            descriptionEn: { required: 'Required' },
            descriptionAr: { required: 'Required' },
            piecePerPacking: { required: 'Required' },
            itemSizeList: { required: 'Required' },
            stock: { required: 'Required' },
            categoryList: { required: 'Required' },
            imageFile: { required: 'Required' },
            price: { required: 'Required' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });
     
     $(document).on('click', '.enablePricingB2B-switch .ios-switch', function () {
          $('.enablePricingB2B').toggle();
     });
     if ($(document).find('.enablePricingB2B-switch [data-plugin-ios-switch]').prop('checked')) {
            $('.enablePricingB2B').show();
     } else {
            $('.enablePricingB2B').hide();
    }

    $("#imageFile").change(function (event) {
        var image = document.getElementById('imagePreview');
        image.src = URL.createObjectURL(event.target.files[0]);
    });
       
};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('Product/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;
     
    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    setTextValue("descriptionEn", r.descriptionEn);
    setTextValue("descriptionAr", r.descriptionAr);
    setTextValue("piecesPerPacking", r.piecesPerPacking);

    setSelectedItemByValueAndTriggerChangeEvent("itemSizeList", r.itemSizeId);

    setTextValue("stock", r.stock);
    $("#stock").prop('disabled', true);

    setSelectedItemByValueAndTriggerChangeEvent("categoryList", r.categoryId); 

    if (r.imageUrl != null) {
        setImage("imagePreview", r.imageUrl);
        $("#imageFile").rules("remove", "required");
    }
    setTextValue("price", r.price);
    setTextValue("discountedPrice", r.discountedPrice);
    setDatePickerValue("discountFromDate", r.discountFromDate);
    setDatePickerValue("discountToDate", r.discountToDate);
      
    if (r.b2BPriceEnabled) {
        $('.enablePricingB2B').show(500);
    
        setCheckValue("b2BPriceEnabled", r.b2BPriceEnabled); 
        setTextValue("b2BPrice", r.b2BPrice);
        setTextValue("b2BDiscountedPrice", r.b2BDiscountedPrice);
        setDatePickerValue("b2BDiscountFromDate",  r.b2BDiscountFromDate);
        setDatePickerValue("b2BDiscountToDate",  r.b2BDiscountToDate);
    }
    setHiddenData(r);
   searchDataTableForStockHistory(r.id);
}

 
isValidData = () => {
    if (getFloatValue('price') == 0) {
        ToastAlert('error', 'Product', 'Please enter Price');
        $("#price").focus();
        return false;
    } else {
        if (getDatePickerValue('discountFromDate') != null || getDatePickerValue('discountToDate') != null) {
            if (getFloatValue('discountedPrice') == 0) {
                ToastAlert('error', 'Product', 'Please enter Discounted Price');
                $("#discountedPrice").focus();
                return false;
            }
        }
    }
    if (getCheckValue('b2BPriceEnabled')) {

        if (getFloatValue('b2BPrice') == 0) {
            ToastAlert('error', 'Product', 'Please enter B2B Price');
            $("#b2BPrice").focus();
            return false;
        }
        if (getDatePickerValue('b2BDiscountFromDate') != null || getDatePickerValue('b2BDiscountToDate') != null) {
            if (getFloatValue('b2BDiscountedPrice') == 0) {
                ToastAlert('error', 'Product', 'Please enter B2B Discount Price');
                $("#b2BDiscountedPrice").focus();
                return false;
            }
        }
    }
    return true;
}
saveData = () => {
    if (isValidData() == false) { return; }
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);

    submitData.append('nameEn', getTextValue('nameEn'));
    submitData.append('nameAr', getTextValue('nameAr'));
    submitData.append('descriptionEn', getTextValue('descriptionEn'));
    submitData.append('descriptionAr', getTextValue('descriptionAr'));
    submitData.append('piecesPerPacking', getIntegerValue('piecesPerPacking'));
    submitData.append('itemSizeId', getSelectedItemValue('itemSizeList'));
    submitData.append('stock', getIntegerValue('stock'));
    submitData.append('categoryId', getSelectedItemValue('categoryList'));
    
    
    submitData.append('price', getFloatValue('price'));  
    submitData.append('discountedPrice', getFloatValue('discountedPrice'));
    if (getDatePickerValue('discountFromDate') != null || getDatePickerValue('discountToDate') != null) {
        submitData.append('discountFromDate', getDatePickerValue('discountFromDate'));
        submitData.append('discountToDate', getDatePickerValue('discountToDate'));
    }
     

    if (getCheckValue('b2BPriceEnabled')) {
        submitData.append('b2BPriceEnabled', getCheckValue('b2BPriceEnabled'));
        submitData.append('b2BPrice', getFloatValue('b2BPrice'));
        submitData.append('b2BDiscountedPrice', getFloatValue('b2BDiscountedPrice'));
        if (getDatePickerValue('b2BDiscountFromDate') != null || getDatePickerValue('b2BDiscountToDate') != null) {
            submitData.append('b2BDiscountFromDate', getDatePickerValue('b2BDiscountFromDate'));
            submitData.append('b2BDiscountToDate', getDatePickerValue('b2BDiscountToDate'));
        }
    }

    if (imageFile.files[0] != undefined) {
        submitData.append("image", imageFile.files[0]);
    }
    
    submitHiddenData(submitData);
    ajaxPost("Product/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'Product', 'Saved Successfully');
        setTimeout(() => location.href = "/Product/ProductList", 10);
    } else {
        showLog(data);
        ToastAlert('error', 'Product', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => { 
    ToastAlert('error', 'Product', 'unable to save, please try again or contact to system admin');
}

 
/* product stock history */
searchDataTableForStockHistory = (productId) => {
   // console.log('row:', productId);
    if ($.fn.dataTable.isDataTable("#datatable-default-productEdit")) {
        $("#datatable-default-productEdit").DataTable().destroy();
    }

    $("#datatable-default-productEdit").DataTable({
        searching: true, 
        serverSide: true,
        stateSave: false,
        "ajax": {
            url: getAPIUrl() + "Product/ProductHistoryGetAllForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                d.productId = productId;
                var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                hideLoader();
                return json.data;
            },
            error: function (error) {
                showLog(error);
            },
        }, 
        "columns": [
            { "data": "productUpdateType",  "name": "productUpdateType" },
            { "data": "oldStock",           "name": "oldStock" },
            { "data": "productActionType",  "name": "productActionType" },
            { "data": "inputStock",         "name": "inputStock" },
            { "data": "updatedStock",       "name": "updatedStock" },
            { "data": "createdBy",          "name": "createdBy" },
            { "data": "createdOn",          "name": "createdOn"},
            { "data": "note",               "name": "note" }, 
        ],
         
        columnDefs: [ 
            { 'width': '10%', 'targets': [0, 2, 5, 6, 7] },
            { "className": "text-wrap", "targets": "_all" },
        ],

    });
     
}

   
 