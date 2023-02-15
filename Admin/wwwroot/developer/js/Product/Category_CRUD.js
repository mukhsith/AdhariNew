var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    setup();
    loadDataFor();
});
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            nameEn: { required: true },
            nameAr: { required: true },
            imageFile: { required: true },
            imageFileNormalIcon: { required: true },
            imageFileSelectedIcon: { required: true },
            imageFileDesktop: { required: true },
            imageFileMobile: { required: true },
            imageFileDesktopAr: { required: true },
            imageFileMobileAr: { required: true },
        },
        messages: {
            nameEn: { required: '' },
            nameAr: { required: '' },
            imageFile: { required: '', },
            imageFileNormalIcon: { required: '', },
            imageFileSelectedIcon: { required: '', },
            imageFileDesktop: { required: '', },
            imageFileMobile: { required: '', },
            imageFileDesktopAr: { required: '', },
            imageFileMobileAr: { required: '', },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('Category/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    if (r.imageUrl != null) {
        setImage("imagePreview", r.imageUrl);
        $("#imageFile").rules("remove", "required");
    }
    if (r.imageNormalIconUrl != null) {
        setImage("imagePreviewNormalIcon", r.imageNormalIconUrl);
        $("#imageFileNormalIcon").rules("remove", "required");
    }
    if (r.imageSelectedIconUrl != null) {
        setImage("imagePreviewSelectedIcon", r.imageSelectedIconUrl);
        $("#imageFileSelectedIcon").rules("remove", "required");
    }
    if (r.imageDesktopUrl != null) {
        setImage("imagePreviewDesktop", r.imageDesktopUrl);
        $("#imageFileDesktop").rules("remove", "required");
    }
    if (r.imageMobileUrl != null) {
        setImage("imagePreviewMobile", r.imageMobileUrl);
        $("#imageFileMobile").rules("remove", "required");
    }

    if (r.imageDesktopUrlAr != null) {
        setImage("imagePreviewDesktopAr", r.imageDesktopUrlAr);
        $("#imageFileDesktopAr").rules("remove", "required");
    }
    if (r.imageMobileUrlAr != null) {
        setImage("imagePreviewMobileAr", r.imageMobileUrlAr);
        $("#imageFileMobileAr").rules("remove", "required");
    }

    setSelectedItemByValueAndTriggerChangeEvent("productTypeList", r.productTypeId);
    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);

    submitData.append('nameEn', getTextValue('nameEn'));
    submitData.append('nameAr', getTextValue('nameAr'));

    if (imageFile.files[0] != undefined) {
        submitData.append("image", imageFile.files[0]);
    }
     
   
    if (imageFileNormalIcon.files[0] != undefined) {
        submitData.append("imageNormalIcon", imageFileNormalIcon.files[0]);
    }
    if (imageFileSelectedIcon.files[0] != undefined) {
        submitData.append("imageSelectedIcon", imageFileSelectedIcon.files[0]);
    }
    if (imageFileDesktop.files[0] != undefined) {
        submitData.append("imageDesktop", imageFileDesktop.files[0]);
    }

    if (imageFileMobile.files[0] != undefined) {
        submitData.append("imageMobile", imageFileMobile.files[0]);
    }


    if (imageFileDesktopAr.files[0] != undefined) {
        submitData.append("imageDesktopAr", imageFileDesktopAr.files[0]);
    }

    if (imageFileMobileAr.files[0] != undefined) {
        submitData.append("imageMobileAr", imageFileMobileAr.files[0]);
    }


    submitData.append('productTypeId', getSelectedItemValue('productTypeList'));

    submitHiddenData(submitData);
    ajaxPost("Category/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.Category, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/Product/CategoryList", 1500);
    } else {
        showLog(data);
        ToastAlert('error', Resources.Category, Resources.UnableTosave);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', Resources.Category, Resources.UnableTosave);
}

 
       
   
 