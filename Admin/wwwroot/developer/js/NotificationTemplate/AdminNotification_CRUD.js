var id = 0;
$(document).ready(function () {
    setup();
    //id = getIntegerValue("Id");
    loadDataFor();
});
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            lowStockThreshold: { required: true },
            lowStockToEmail: { required: true },
            lowStockCCEmail: { required: true },
            newOrderToEmail: { required: true },
            newOrderCCEmail: { required: true },
            
        },
        messages: {
            lowStockThreshold: { required: 'Required' },
            lowStockToEmail: { required: 'Required' },
            lowStockCCEmail: { required: 'Required' },
            newOrderToEmail: { required: 'Required' },
            newOrderCCEmail: { required: 'Required' },
           
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => { 
    //if (id == 0) { return;}
    ajaxGet('Notification/GetAdminNotificationDefault', cbGetSuccess);
};
cbGetSuccess = (data) => {
    console.log(data.data);
    if (data.data == null) {  return; }
    var r = data.data;
    setTextValue("Id", r.id);
    setTextValue("LowStockThreshold", r.lowStockThresholdQuantity);
    setTextValue("LowStockToEmail", r.lowStockToEmailAddress);
    setTextValue("LowStockCCEmail", r.lowStockCCEmailAddress);
    setTextValue("NewOrderToEmail", r.newOrderNotificationToEmailAddress);
    setTextValue("NewOrderCCEmail", r.newOrderNotificationCCEmailAddress);

    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
   // alert(getCheckValue('chkNewOrder'));
    submitData.append("Id", $('#Id').val());
   
    submitData.append('lowStockEnabled', getCheckValue('chkLowStockThreshold'));
    submitData.append('lowStockThresholdQuantity', getTextValue('LowStockThreshold'));
    submitData.append('lowStockToEmailAddress', getTextValue('LowStockToEmail'));
    submitData.append('lowStockCCEmailAddress', getTextValue('LowStockCCEmail'));
    submitData.append('newOrderNotificationEnabled', getCheckValue('chkNewOrder'));
    submitData.append('newOrderNotificationToEmailAddress', getTextValue('NewOrderToEmail'));
    submitData.append('newOrderNotificationCCEmailAddress', getTextValue('NewOrderCCEmail'));

    //submitData.append('orderConfirmationEnabled', 1);
    //submitData.append('orderConfirmationToEmailAddress', '');
    //submitData.append('orderConfirmationCCEmailAddress', '');

    submitHiddenData(submitData);
    ajaxPost("Notification/UpdateAdminNotificationnew", submitData, cbPostSuccess, cbPostError);

};



cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'Notification', 'Saved Successfully');
        cbGetSuccess(data);
    } else {
        //showLog(data);
        ToastAlert('error', 'Notification', data.message);
    }
}

cbPostError = (error) => {
    ToastAlert('error', 'Notification', 'unable to save, please try again or contact to system admin');
}


       
   
 