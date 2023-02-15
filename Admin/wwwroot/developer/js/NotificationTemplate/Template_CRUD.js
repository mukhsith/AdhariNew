var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    setup();
    loadDataFor();
});
setup = () => {
    $("#dataForm").validate({
        rules: {},
        messages: {},
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });

    $(document).on('click', '.EnableOTPNotifications-switch .ios-switch', function () {
        $('.EnableOTPNotifications').toggle();
    });

    $(document).on('click', '.EnablePaymentFailedNotifications-switch .ios-switch', function () {
        $('.EnablePaymentFailedNotifications').toggle();
    });

}

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('NotificationTemplate/ById?Id=' + id, cbGetSuccess);
};


cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;
    $("#notificationTitle").text(r.title);
    setCheckValue("smsEnabled", r.smsEnabled);
    if (r.smsEnabled) {
        $(".EnableOTPNotifications").toggle();
    }

    setTextValue("smsMessageEn", r.smsMessageEn);
    setTextValue("smsMessageAr", r.smsMessageAr);

   
    setCheckValue("pushEnabled", r.pushEnabled);
    if (r.pushEnabled) {
        $(".EnablePaymentFailedNotifications").toggle();
    }
    setTextValue("pushMessageEn", r.pushMessageEn);
    setTextValue("pushMessageAr", r.pushMessageAr);
  
    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);

  /*  submitData.append('smsEnabled', getCheckValue('smsEnabled'));*/
    submitData.append('smsMessageEn', getTextValue('smsMessageEn'));
    submitData.append('smsMessageAr', getTextValue('smsMessageAr'));
     
    //submitData.append('pushEnabled', getCheckValue('pushEnabled'));
    //submitData.append('pushMessageEn', getTextValue('pushMessageEn'));
    //submitData.append('pushMessageAr', getTextValue('pushMessageAr'));
       
    submitHiddenData(submitData);
    ajaxPost("NotificationTemplate/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.NotificationTemplates, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/Notification/NotificationTemplateList", 1500);
    } else { 
        ToastAlert('error', Resources.NotificationTemplates, Resources.UnableTosave);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', Resources.NotificationTemplates, Resources.UnableTosave);
}

