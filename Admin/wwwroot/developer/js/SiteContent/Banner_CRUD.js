var id = 0;
$(document).ready(function () {
    setup();
    id = getIntegerValue("Id");
   
    loadDataFor();
    fillDropDownList("productList", 'Product/ForDropDownList', false, null, "id", "name");
  
});
setup = () => {
    
    $("#dataForm").validate({
        errorPlacement:
            function (error, element) { },
        rules: {
            imageFileEn: {
                required: true
            },
            imageFileAr: { required: true},
        },
        messages: {
            imageFileEn: { required: '' },
            imageFileAr: { required: '', },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });
   

};

$("#linkEnabled").change(function () {
    if ($(this)[0].checked) {
        $('.banner-field').show();
    } else {
        $('.banner-field').hide();
        $('.banner-field-url').hide();
        $('.banner-field-product').hide();
    }
});

$('.banner-field select').change(function () {
    if ($(this).val() == 0) {
        $('.banner-field-url').hide();
        $('.banner-field-product').hide();

    } else if ($(this).val() == 1) {
        $('.banner-field-url').show();
        $('.banner-field-product').hide();

    } else if ($(this).val() == 2) {
        $('.banner-field-url').hide();
        $('.banner-field-product').show();
    }
});

loadDataFor = () => {
    //debugger;
    if (id == 0) { return;}
    ajaxGet('Banner/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    if (r.imageUrlEn != null) {
        setImage("imagePreviewEn", r.imageUrlEn);
        $("#imageFileEn").rules("remove", "required");
    }
    if (r.imageUrlAr != null) {
        setImage("imagePreviewAr", r.imageUrlAr);
        $("#imageFileAr").rules("remove", "required");
    }
    setCheckValue("linkEnabled", r.linkEnabled);

    setSelectedItem("linkType", r.linkType);
    setTextValue("linkURL", r.linkUrl);
    setSelectedItem("productList", r.productId);

    $("#linkEnabled").change();
        //LinkTo 1=Url,2=Product
    if (linkEnabled) { 
        $('#linkType').trigger('change');
    
        if (r.linkType == 2) {
            //productList contains list of products
            $("#productList").trigger('change');
        }
    }
    setHiddenData(r);

    

}





isValid = () => {
    if (getCheckValue('linkEnabled')) {
        let linkType = getSelectedItemValue('linkType');
        if (linkType == '0') {
            ToastAlert('error', Resources.Banners, Resources.PleaseselectaLinkType);
            $("#linkType").focus();
            return false;
        }

        let linkUrl = getTextValue('linkURL')
        if (linkType == '1' && linkUrl == '') {
            ToastAlert('error', Resources.Banners, Resources.PleaseEnterANavigationUrl);
            $("#linkURL").focus();
            return false;
        }

        let productId = getSelectedItemValue('productList');
        if (linkType == '2' && productId == '0') {
            ToastAlert('error', Resources.Banners, Resources.SelectProduct);
            $("#productList").focus();
            return false;
        }
    }
}


saveData = () => {
    if (isValid() == false) { return; }

    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);

    if (imageFileEn.files[0] != undefined) {
        submitData.append("imageEn", imageFileEn.files[0]);
    }
    if (imageFileAr.files[0] != undefined) {
        submitData.append("imageAr", imageFileAr.files[0]);
    }

    let linkEnabled = getCheckValue('linkEnabled');

    if (linkEnabled) {
        submitData.append('linkEnabled', getCheckValue('linkEnabled'));

        let linkType = getSelectedItemValue('linkType');
        submitData.append('linkType', linkType);

        if (linkType == '1') {

            
                submitData.append('linkUrl', getTextValue('linkURL'));
        } else {
            var prdID = $('#productList option:selected').val();
            

            submitData.append('productId', prdID);
        }
    }
    
    submitHiddenData(submitData);
    ajaxPost("Banner/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.Banners, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/SiteContent/BannerList", 1500);
    } else {
        showLog(data);
        ToastAlert('error', Resources.Banners, Resources.UnableTosave);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', Resources.Banners, Resources.UnableTosave);
}

 
       
   
 