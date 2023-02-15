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
            MinCartQuantity: { required: true },
            MaxCartQuantity: { required: true },
        },
        messages: { 
            nameEn: { required: '' },
            nameAr: { required: '' },
            descriptionEn: { required: '' },
            descriptionAr: { required: '' },
            piecePerPacking: { required: '' },
            itemSizeList: { required: '' },
            stock: { required: '' },
            categoryList: { required: '' },
            imageFile: { required: '' },
            price: { required: '' },
            MinCartQuantity: { required: '' },
            MaxCartQuantity: { required: '' },
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

    setTextValue("MinCartQuantity", r.minCartQuantity);
    setTextValue("MaxCartQuantity", r.maxCartQuantity);
      
    if (r.b2BPriceEnabled) {
        $('.enablePricingB2B').show(500);
    
        setCheckValue("b2BPriceEnabled", r.b2BPriceEnabled); 
        setTextValue("b2BPrice", r.b2BPrice);
        setTextValue("b2BDiscountedPrice", r.b2BDiscountedPrice);
        setDatePickerValue("b2BDiscountFromDate",  r.b2BDiscountFromDate);
        setDatePickerValue("b2BDiscountToDate", r.b2BDiscountToDate);


        setTextValue("B2BMinCartQuantity", r.b2BMinCartQuantity);
        setTextValue("B2BMaxCartQuantity", r.b2BMaxCartQuantity);
    }
    setHiddenData(r);
   searchDataTableForStockHistory(r.id);
}

 
isValidData = () => {
    if (getFloatValue('price') == 0) {
        ToastAlert('error', Resources.Product, Resources.PleaseEnterPrice);
        $("#price").focus();
        return false;
    }
    else if (getIntegerValue('MinCartQuantity') == 0) {
        ToastAlert('error', Resources.Product, Resources.PleaseMinmumCartQuantity);
        $("#MinCartQuantity").focus();
        return false;
    }
    else if (getIntegerValue('MaxCartQuantity') == 0) {
        ToastAlert('error', Resources.Product, Resources.PleaseMaximumCartQuantity);
        $("#MaxCartQuantity").focus();
        return false;
    }
    else if (getIntegerValue('MaxCartQuantity') <= getIntegerValue('MinCartQuantity')) {
        ToastAlert('error', Resources.Product, Resources.PleaseValidMaximumCartQuantity);
        $("#MaxCartQuantity").focus();
        return false;
    }
    else {
        if (getDatePickerValue('discountFromDate') != null || getDatePickerValue('discountToDate') != null) {
            if (getFloatValue('discountedPrice') == 0) {
                ToastAlert('error', Resources.Product, Resources.PleaseDiscountedPrice);
                $("#discountedPrice").focus();
                return false;
            }
        }
    }
    if (getCheckValue('b2BPriceEnabled')) {

        if (getFloatValue('b2BPrice') == 0) {
            ToastAlert('error', Resources.Product, Resources.PleaseB2BPrice);
            $("#b2BPrice").focus();
            return false;
        } else if (getIntegerValue('B2BMinCartQuantity') == 0) {
            ToastAlert('error', Resources.Product, Resources.PleaseB2BMinmumCartQuantity);
            $("#B2BMinCartQuantity").focus();
            return false;
        }
        else if (getIntegerValue('B2BMaxCartQuantity') == 0) {
            ToastAlert('error', Resources.Product, Resources.PleaseB2BMaximumCartQuantity);
            $("#B2BMaxCartQuantity").focus();
            return false;
        }
        else if (getIntegerValue('B2BMaxCartQuantity') <= getIntegerValue('B2BMinCartQuantity')) {
            ToastAlert('error', Resources.Product, Resources.PleaseValidMaximumCartQuantity);
            $("#B2BMaxCartQuantity").focus();
            return false;
        }
        else {
            if (getDatePickerValue('b2BDiscountFromDate') != null || getDatePickerValue('b2BDiscountToDate') != null) {
                if (getFloatValue('b2BDiscountedPrice') == 0) {
                    ToastAlert('error', Resources.Product, Resources.PleaseB2BDiscountPrice);
                    $("#b2BDiscountedPrice").focus();
                    return false;
                }
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
    submitData.append('minCartQuantity', getIntegerValue('MinCartQuantity'));
    submitData.append('maxCartQuantity', getIntegerValue('MaxCartQuantity'));


    if (getCheckValue('b2BPriceEnabled')) {
        submitData.append('b2BPriceEnabled', getCheckValue('b2BPriceEnabled'));
        submitData.append('b2BPrice', getFloatValue('b2BPrice'));
        submitData.append('b2BDiscountedPrice', getFloatValue('b2BDiscountedPrice'));
        if (getDatePickerValue('b2BDiscountFromDate') != null || getDatePickerValue('b2BDiscountToDate') != null) {
            submitData.append('b2BDiscountFromDate', getDatePickerValue('b2BDiscountFromDate'));
            submitData.append('b2BDiscountToDate', getDatePickerValue('b2BDiscountToDate'));
        }

        submitData.append('b2BMinCartQuantity', getIntegerValue('B2BMinCartQuantity'));
        submitData.append('b2BMaxCartQuantity', getIntegerValue('B2BMaxCartQuantity'));

    }

    if (imageFile.files[0] != undefined) {
        submitData.append("image", imageFile.files[0]);
    }
    
    submitHiddenData(submitData);
    ajaxPost("Product/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.Product, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/Product/ProductList", 10);
    } else {
        showLog(data);
        ToastAlert('error', Resources.Product, Resources.UnableTosave);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', Resources.Product, Resources.UnableTosave);
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

   
 