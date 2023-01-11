 
$(document).ready(function () {
     setup();
    loadDataFor();
});
setup = () => { 
    $("#dataForm").validate({
        rules: {
            contentEn: { required: true},
            contentAr: { required: true },
            imageFile: { required: true },
            
        },
        messages: {
            contentEn: { required: 'Required' },
            contentAr: { required: 'Required', },
            imageFile: { required: 'Required', }, 
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });
     

};

loadDataFor = () => { 
    ajaxGet('SiteContent/' + getIntegerValue('typeId'), cbGetSuccess);
};

cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;
    $("#contentEn").summernote('code', r.contentEn);
    $("#contentAr").summernote('code', r.contentAr);
    
    if (r.imageUrl != null) {
        setImage("imagePreview", r.imageUrl);
        $("#imageFile").rules("remove", "required");
    }
    setCheckValue('active', r.active);
    setHiddenData(r);
   
}


saveData = () => {
    showLoader();
    let submitData = new FormData();

    var contentEn = sanitizeSummernoteAndGetContent("contentEn");
    var contentAr = sanitizeSummernoteAndGetContent("contentAr");
    submitData.append("appContentType", getIntegerValue("typeId"));
    submitData.append("contentEn", contentEn);
    submitData.append("contentAr", contentAr);
      
    if (imageFile.files[0] != undefined) {
        submitData.append("image", imageFile.files[0]);
    }
     
    submitData.append("active", getCheckValue('active'));
    submitHiddenData(submitData);
    ajaxPost("SiteContent/Edit", submitData, cbPostSuccess, undefined);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'About Us', 'Saved Successfully'); 
    } else {
        showLog(data);
        ToastAlert('error', 'About Us', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => { 
    ToastAlert('error', 'About Us', 'unable to save, please try again or contact to system admin');
}

 
       
   
 