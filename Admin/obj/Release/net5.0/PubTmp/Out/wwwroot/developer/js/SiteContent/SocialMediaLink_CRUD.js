
$(document).ready(function () {
    setup();
    loadDataFor();
});
setup = () => {
    $("#dataForm").validate({
        rules: { },
        messages: { },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => {
    ajaxGet('SocialMediaLink/GetDefault', cbGetSuccess);
};

cbGetSuccess = (data) => {

    if (data.data == null) { return; }
    var r = data.data; 
    setTextValue('facebookLink', r.facebookLink);
    setTextValue('instagramLink', r.instagramLink);
    setTextValue('twitterLink', r.twitterLink);
    setTextValue('youtubeLink', r.youtubeLink);
    setTextValue('whatsAppLink', r.whatsAppLink);
    setTextValue('tiktokLink', r.tiktokLink);
    setTextValue('snapchatLink', r.snapchatLink);
    setHiddenData(r);

}


saveData = () => {
    showLoader();
    let submitData = new FormData(); 
    submitData.append("facebookLink", getTextValue("facebookLink"));
    submitData.append("instagramLink", getTextValue("instagramLink"));
    submitData.append("twitterLink", getTextValue("twitterLink"));
    submitData.append("youtubeLink", getTextValue("youtubeLink"));
    submitData.append("whatsAppLink", getTextValue("whatsAppLink"));
    submitData.append("tiktokLink", getTextValue("tiktokLink"));
    submitData.append("snapchatLink", getTextValue("snapchatLink"));
   // submitData.append("active", getCheckValue('active'));
    submitHiddenData(submitData);
    ajaxPost("SocialMediaLink/Edit", submitData, cbPostSuccess, undefined);

};


cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'Social Media Link', 'Saved Successfully'); 
    } else {
        showLog(data);
        ToastAlert('error', 'Social Media Link', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => {
    ToastAlert('error', 'Social Media Link', 'unable to save, please try again or contact to system admin');
}




