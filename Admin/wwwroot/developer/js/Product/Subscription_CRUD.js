var parentId = 0;
var products = null;
var categories = null;
var Table = null;
var productItems = [];
$(document).ready(function () {
    parentId = getIntegerValue("Id");
  fillDropDownList("subscriptionDurationsList", "Subscription/GetSubscriptionDurations", false, null, "id", "nameEn", loadsubscriptionDuration );
    ajaxGet('Product/GetAllProductAndCategory', cbGetList);
    //fillDropDownMultiList("subscriptionDurationsList", 'Subscription/ForDropDownList', false, null, "id", "name", loadsubscriptionDuration);
    ////loadsubscriptionDuration();
    //fillDropDownList("productList", 'Product/ForDropDownList', false, null, "id", "nameEn");
    //prepareDatatable();
    //loadDataFor();
    setup();


 /*   $("#subscriptionDurationsList").multiselect('rebuild');*/

});

loadsubscriptionDuration = () => {
   
    $("#subscriptionDurationsList").multiselect('rebuild');
}


cbGetList = (data) => {
    products = data.data.products; //temporary save for filter of selected item from dropdown list
    categories = data.data.categories;
    fillDropDownListData("productList", data.data.products, false, null, "id", "nameEn");
    prepareDatatable();
    loadDataFor();
}
setup = () => {

    $("#dataForm").validate({
        rules: {
            nameEn: { required: true },
            nameAr: { required: true },
            descriptionEn: { required: true },
            descriptionAr: { required: true },
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

    $(document).on('click', '.specialPackage-switch .ios-switch', function () {
         $('.specialPackage').toggle();
       //  $('.subscriptionDuration').toggle();
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
    if (parentId == 0) { // if no record found, build the datatable for existing BundledProdcutDetails List
        return;
    }
    ajaxGet('Subscription/ById?Id=' + parentId, cbGetSuccess);
};
cbGetSuccess = (data) => {

    if (data.data == null) { return; }
    var r = data.data;

    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    setTextValue("descriptionEn", r.descriptionEn);
    setTextValue("descriptionAr", r.descriptionAr);

    setSelectedItemByValueAndTriggerChangeEvent("productList", r.productId);
    
    setSelectedItemByValueAndTriggerChangeEvent("subscriptionDurationsList", r.subscriptionDurationId);

    if (r.imageUrl != null) {
        setImage("imagePreview", r.imageUrl);
        $("#imageFile").rules("remove", "required");
    }

    
    setCheckValue("specialPackage", r.specialPackage);
    //if (r.specialPackage) {
    //    $('.subscriptionDuration').show();
    //}


    var subscriptionDuration = new Array();
    if (r.subscriptionDurationIds != null) {

        subscriptionDuration = r.subscriptionDurationIds.split(",");
       
    }
    if (subscriptionDuration.length > 0) {

        for (var i = 0; i < subscriptionDuration.length; i++) {
            var optionVal = subscriptionDuration[i];
            console.log(optionVal);
            if (subscriptionDuration[i] != "") {
                $("#subscriptionDurationsList").find("option[value=" + optionVal + "]").prop("selected", "selected");
            }
        }
        $("#subscriptionDurationsList").multiselect('refresh');

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
        setDatePickerValue("b2BDiscountFromDate", r.b2BDiscountFromDate);
        setDatePickerValue("b2BDiscountToDate", r.b2BDiscountToDate);

        setTextValue("B2BMinCartQuantity", r.b2BMinCartQuantity);
        setTextValue("B2BMaxCartQuantity", r.b2BMaxCartQuantity);

    }
    if (r.productDetails.length > 0) {
        for (var index = 0; index < r.productDetails.length; index++) {
            var bp = r.productDetails[index];
            //filter productdetail items in product table (el.id is a product table primary id) and (pb.productId is a saved productDetails items)
            var product = products.filter(function (el) { return el.id == bp.childProductId; })[0];
            if (product != undefined) {

                var item = {
                    id: bp.id, //new item will be with zero id
                    productId: bp.productId,
                    childProductId: bp.childProductId,
                    imageUrl: product.imageUrl,
                    nameEn: product.nameEn + ' ' + product.itemSizeNameEn,
                    price: product.price,
                    quantity: bp.quantity
                };

                addRow(item);
            }
        }
        $('.datatable-default-bundledProduct').find('tfoot').show();
        calculate();


    }
    setHiddenData(r);
}


isValidData = () => {

    if (getCheckValue('specialPackage')) {
        var durationId = getSelectedItemValue("subscriptionDurationsList");
        if (durationId == null || durationId == undefined) {
            ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseSpecialPackageDuration);
            $("#subscriptionDurationsList").focus();
            return false;
        }
    }

    if (getCheckValue('specialPackage')) {
        var subscriptionDuration = "";
        var subDuration = new Array();
        subscriptionDuration = multiselect_selected(subscriptionDurationsList);
        subDuration = subscriptionDuration.split(",");
        if (subDuration.length != 1) {
            ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseSingleSubscriptionDurations);
            return false;
        }
    }

    if (getFloatValue('price') == 0) {
        ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseEnterPrice);
        $("#price").focus();
        return false;
    }
    else if (getIntegerValue('MinCartQuantity') == 0) {
        ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseMinmumCartQuantity);
        $("#MinCartQuantity").focus();
        return false;
    }
    else if (getIntegerValue('MaxCartQuantity') == 0) {
        ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseMaximumCartQuantity);
        $("#MaxCartQuantity").focus();
        return false;
    }
    else if (getIntegerValue('MaxCartQuantity') <= getIntegerValue('MinCartQuantity')) {
        ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseValidMaximumCartQuantity);
        $("#MaxCartQuantity").focus();
        return false;
    }

    else {
        if (getDatePickerValue('discountFromDate') != null || getDatePickerValue('discountToDate') != null) {
            if (getFloatValue('discountedPrice') == 0) {
                ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseDiscountedPrice);
                $("#discountedPrice").focus();
                return false;
            }
        }
    }
    if (getCheckValue('b2BPriceEnabled')) {

        if (getFloatValue('b2BPrice') == 0) {
            ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseB2BPrice);
            $("#b2BPrice").focus();
            return false;
        }
        else if (getIntegerValue('B2BMinCartQuantity') == 0) {
            ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseB2BMinmumCartQuantity);
            $("#B2BMinCartQuantity").focus();
            return false;
        }
        else if (getIntegerValue('B2BMaxCartQuantity') == 0) {
            ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseB2BMaximumCartQuantity);
            $("#B2BMaxCartQuantity").focus();
            return false;
        }
        else if (getIntegerValue('B2BMaxCartQuantity') <= getIntegerValue('B2BMinCartQuantity')) {
            ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseValidMaximumCartQuantity);
            $("#B2BMaxCartQuantity").focus();
            return false;
        }
        else {
            if (getDatePickerValue('b2BDiscountFromDate') != null || getDatePickerValue('b2BDiscountToDate') != null) {
                if (getFloatValue('b2BDiscountedPrice') == 0) {
                    ToastAlert('error', Resources.SubscriptionProduct, Resources.PleaseB2BDiscountPrice);
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
    submitData.append("Id", parentId);

    submitData.append('nameEn', getTextValue('nameEn'));
    submitData.append('nameAr', getTextValue('nameAr'));
    submitData.append('descriptionEn', getTextValue('descriptionEn'));
    submitData.append('descriptionAr', getTextValue('descriptionAr'));
    submitData.append('categoryId', 5);

    var specialPackage = getCheckValue('specialPackage');
    submitData.append('specialPackage', getCheckValue('specialPackage'));
    var subscriptionDurationId = getSelectedItemValue("subscriptionDurationsList");
    if (subscriptionDurationId != null) {
        submitData.append('subscriptionDurationId', subscriptionDurationId);
    }

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


    var subscriptionDuration = "";

    subscriptionDuration = multiselect_selected(subscriptionDurationsList);


    submitData.append('subscriptionDurationIds', subscriptionDuration);

    var productDetails = getBundledProducts();
    for (var index = 0; index < productDetails.length; index++) {
        var p = productDetails[index];
        submitData.append(`ProductDetails[][${index}][Id]`, p.id);
        submitData.append(`ProductDetails[][${index}][productId]`, p.productId);
        submitData.append(`ProductDetails[][${index}][childProductId]`, p.childProductId);
        submitData.append(`ProductDetails[][${index}][quantity]`, p.quantity);
    }
    submitHiddenData(submitData);
    ajaxPost("Subscription/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', Resources.SubscriptionProduct, Resources.SavedSuccessfully);
        //setTimeout(() => location.href = "/Product/SubscriptionList", 10);
    } else {
        showLog(data);
        ToastAlert('error', Resources.SubscriptionProduct, Resources.UnableTosave);
    }
}

cbPostError = (error) => {
    ToastAlert('error', Resources.SubscriptionProduct, Resources.UnableTosave);
}


getBundledProducts = () => {
    const allTables = document.querySelectorAll(".datatable-default-bundledProduct");
    var items = [];

    for (const table of allTables) {
        var primaryId = 0;
        var productId = 0;
        var childProductId = 0;
        var qty = 0;
        const tBody = table.tBodies[0];
        const rows = Array.from(tBody.rows);
        const headerCells = table.tHead.rows[0].cells; //get table header

        for (const r of rows) { //no of rows
            for (const th of headerCells) { //loop through number of cells
                const cellIndex = th.cellIndex;
                const cell = r.cells[cellIndex]; //current cell
                if (cellIndex == 0) { //product Id
                    const productCell = $(cell).find("span")[0];
                    //productId = parseInt(productCell.innerText); 
                    primaryId = parseInt($(productCell).data('id'));
                    productId = parseInt($(productCell).data('productid'));
                    childProductId = parseInt($(productCell).data('childproductid'));
                    // productId = parseInt(cell.textContent);
                } else if (cellIndex == 4) { //qty column 4 <td><input type=number min=1/></td>
                    const qtyCell = $(cell).find("input[type=number]")[0];
                    qty = parseInt(qtyCell.value);

                }
                //for debug
                //showLog(cell.textContent);
            }
            //for new item id will be zero
            items.push({ id: primaryId, productId: productId, childProductId: childProductId, quantity: qty })
        }

        return items;

    }
}

prepareDatatable = () => {
    //console.log("Ready");
    // $('.table').DataTable();
    Table = $('.datatable-default-bundledProduct').DataTable({
        responsive: true,
        searching: false,
        paging: false,
        info: false,
        ordering: false,
        serverSide: false,
        buttons:[],
        language: { emptyTable: "No Subscription Product Added." },
        columnDefs: [{ "width": "5%", "targets": [0, 5] }],
        columnDefs: [{ "width": "20%", "targets": [1, 4] }]
    });


    // Temporary Code. Needs to be made generic by the developer
    $('.add-product-btn').click(function () {

        var selectedProductId = getSelectedItemValue("productList"); //dropdownlist selectedItem id
        if (selectedProductId) {
            //existing all products
            var product = products.filter(function (el) { return el.id == selectedProductId; })[0];
            if (product != undefined) {
                var detail = {
                    id: 0, //new item will be with zero id  
                    productId: parentId,//addEdit current id zero(0) or > 0
                    childProductId: product.id,
                    imageUrl: product.imageUrl,
                    nameEn: product.nameEn + ' ' + product.itemSizeNameEn,
                    price: product.price,
                    quantity: 1 //default value
                };
                addRow(detail);

            }
        }
    });

    addRow = (item) => {
        if (!alreadyExistsInTable(item)) {

            Table.row.add(
                [
                    `<span data-id='${item.id}' data-productid='${item.productId}' data-childproductid='${item.childProductId}'>${item.id}</span>`,
                    `<img src='${item.imageUrl}' height='100px'>`,
                    item.nameEn,
                    `<span data-price='${item.price}'>KWD ${parseFloat(item.price).toFixed(3)}</span>`,
                    `<input type='number' id='row${item.childProductId}' class='form-control form-control-lg border-secondary' min=1
                    onkeypress='return event.charCode >= 48 && event.charCode <= 57' value=${item.quantity} onchange='calculate()'>`,
                    `<a href='javascript:;' class='mb-1 mt-1 me-1 btn btn-sm btn-danger btn-remove-product' 
                        data-bs-toggle='tooltip' data-bs-placement='bottom' title=''
                        data-bs-original-title='Remove Product' aria-label='Remove Product'>
                            <i class='fa fa-xmark'></i>
                    </a>`
                ]).draw(false);
            $('.datatable-default-bundledProduct').find('tfoot').show();
            calculate();
        }
        else {
            ToastAlert("error", "Item Exists", item.nameEn + ' is already exists ');
        }
    }

    alreadyExistsInTable = (item) => {
        return $('.datatable-default-bundledProduct').find(`[data-childproductid=${item.childProductId}]`).length > 0;
    }

    //calculate Price
    $('.calculate-pricing').click(function () {
        $(this).siblings('input').val($('.total-price').attr('data-price'))
    });

    $(document).on('click', '.btn-remove-product', function () {
        Table.row($(this).parents('tr')).remove().draw();
        calculate();
    });



}

calculate = () => {
    const allTables = document.querySelectorAll(".datatable-default-bundledProduct");
    var totalPrice = 0.0;
    var totalQty = 0;
    var _rowPrice = 0;

    for (const table of allTables) {
        const tBody = table.tBodies[0];
        const rows = Array.from(tBody.rows);
        const headerCells = table.tHead.rows[0].cells; //get table header

        for (const r of rows) { //no of rows
            for (const th of headerCells) { //loop through number of cells
                const cellIndex = th.cellIndex;
                const cell = r.cells[cellIndex]; //current cell
                if (cellIndex == 3) { //price column 3 text inside <td>45</td>
                    const price = $(cell).find("span")[0];//.data("price");
                    if (price != undefined) {
                        const PriceCell = parseFloat(price.attributes["data-price"].value); // parseFloat(cell.textContent);
                        //totalPrice += PriceCell;
                        _rowPrice = PriceCell;
                    }
                } else if (cellIndex == 4) { //qty column 4 <td><input type=number min=1/></td>

                    const QtyInput = $(cell).find("input[type=number]")[0];
                    if (QtyInput != undefined) {
                        const QtyCell = parseInt(QtyInput.value);
                        totalQty += QtyCell; 
                        totalPrice += _rowPrice * QtyCell;
                    }
                }
                //for debug
                //console.log(cell.textContent);
            }
        }
        setTotalPrice(totalQty, totalPrice);

    }
}
setTotalPrice = (qty, totalPrice) => {
    $('.total-price').html("KWD " + totalPrice);
    $('.total-price').attr("data-price", totalPrice);
    $('.total-qty').html(qty);
}


function multiselect_selected($el) {
    var selectedVal = "";
    var ret = true;
    $('option', $el).each(function (element) {
        if ($(this).prop('selected')) {

            if (selectedVal != "") {
                selectedVal = selectedVal + "," + $(this).val();
            } else {
                selectedVal =  $(this).val();

            }

            ret = false;
        }
    });
    return selectedVal;
}
