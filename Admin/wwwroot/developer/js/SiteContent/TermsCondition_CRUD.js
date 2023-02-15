
$(document).ready(function () {
    setup();
    loadDataFor();
});
setup = () => {
    $("#dataForm").validate({
        rules: {
            contentEn: { required: true },
            contentAr: { required: true },

        },
        messages: {
            contentEn: { required: '' },
            contentAr: { required: '', },
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

    if (data.data == null) { return; }
    var r = data.data; 
    $("#contentEn").summernote('code', r.contentEn);
    $("#contentAr").summernote('code', r.contentAr);

   /* setCheckValue('active', r.active);*/
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

   /* submitData.append("active", getCheckValue('active'));*/
    submitHiddenData(submitData);
    ajaxPost("SiteContent/Edit", submitData, cbPostSuccess, undefined);

};


cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', Resources.TermsAndConditions , Resources.SavedSuccessfully);
    } else {
        showLog(data);
        ToastAlert('error', Resources.TermsAndConditions, Resources.UnableTosave);
    }
}

cbPostError = (error) => {
    ToastAlert('error', Resources.TermsAndConditions, Resources.UnableTosave);
}




