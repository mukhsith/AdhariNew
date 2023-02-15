
$(document).ready(function () {
    setup();
    loadDataFor();
});
setup = () => {
    //$.validator.addMethod("phoneNumber", function (value, element) {
    //    return value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1').replace(/^0[^.]/, '0');
    //    return true;
    //}

    $("#dataForm").validate({
        errorPlacement:
            function (error, element) { },
        rules: {
            addressEn: { required: true },
            addressAr: { required: true },
            mobileNumber: { required: true },
            emailAddress: { required: true },
            whatsAppNumber: { required: true },
            
        },
        messages: {
            addressEn: { required: '' },
            addressAr: { required: '' },
            mobileNumber: { required: '' },
            emailAddress: { required: '' },
            whatsAppNumber: { required: '' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => {
    ajaxGet('ContactDetail/GetDefault', cbGetSuccess);
};

cbGetSuccess = (data) => {

    if (data.data == null) { return; }
    var r = data.data; 
    setTextValue('addressEn', r.addressEn);
    setTextValue('addressAr', r.addressAr);
    setTextValue('mobileNumber', r.mobileNumber);
    setTextValue('emailAddress', r.emailAddress);
    setTextValue('whatsAppNumber', r.whatsAppNumber);
    setHiddenData(r);

}


saveData = () => {
    showLoader();
    let submitData = new FormData(); 
    submitData.append("addressEn", getTextValue("addressEn"));
    submitData.append("addressAr", getTextValue("addressAr"));
    submitData.append("mobileNumber", getTextValue("mobileNumber"));
    submitData.append("emailAddress", getTextValue("emailAddress"));
    submitData.append("whatsAppNumber", getTextValue("whatsAppNumber"));
   // submitData.append("active", getCheckValue('active'));
    submitHiddenData(submitData);
    ajaxPost("ContactDetail/Edit", submitData, cbPostSuccess, undefined);

};


cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', Resources.ContactDetails, Resources.SavedSuccessfully);
    } else {
        showLog(data);
        ToastAlert('error', Resources.ContactDetails, Resources.UnableTosave);
    }
}

cbPostError = (error) => {
    ToastAlert('error', Resources.ContactDetails, Resources.UnableTosave);
}




