
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
        rules: {
            addressEn: { required: true },
            addressAr: { required: true },
            mobileNumber: { required: true },
            emailAddress: { required: true },
            
            
        },
        messages: {
            addressEn: { required: 'Required' },
            addressAr: { required: 'Required' },
            mobileNumber: { required: 'Required' },
            emailAddress: { required: 'Required' }, 
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
    setHiddenData(r);

}


saveData = () => {
    showLoader();
    let submitData = new FormData(); 
    submitData.append("addressEn", getTextValue("addressEn"));
    submitData.append("addressAr", getTextValue("addressAr"));
    submitData.append("mobileNumber", getTextValue("mobileNumber"));
    submitData.append("emailAddress", getTextValue("emailAddress"));
   // submitData.append("active", getCheckValue('active'));
    submitHiddenData(submitData);
    ajaxPost("ContactDetail/Edit", submitData, cbPostSuccess, undefined);

};


cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'Contact Detail', 'Saved Successfully'); 
    } else {
        showLog(data);
        ToastAlert('error', 'Contact Detail', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => {
    ToastAlert('error', 'Contact Detail', 'unable to save, please try again or contact to system admin');
}




