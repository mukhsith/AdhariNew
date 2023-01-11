var id = 0;
$(document).ready(function () {
    setup();
    id = getIntegerValue("Id"); 
});
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            fromDate: { required: true },
            toDate: { required: true },
            note: { required: true }, 
        },
        messages: {
            fromDate: { required: 'Required' },
            toDate: { required: 'Required' },
            note: { required: 'Required' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    }); 

};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('DeliveryBlockedDate/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    setDatePickerValue("fromDate", r.fromDate);
    setDatePickerValue("toDate", r.toDate); 
    setTextValue("note", r.note);  
    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id); 
    submitData.append('fromDate', getDatePickerValue('fromDate'));
    submitData.append('toDate', getDatePickerValue('toDate'));
    submitData.append('note', getTextValue('note')); 
    submitHiddenData(submitData);
    ajaxPost("DeliveryBlockedDate/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'Delivery Blocked Date', 'Saved Successfully');
        setTimeout(() => location.href = "/Delivery/BlockedDateList", 500);
    } else {
        //showLog(data);
        ToastAlert('error', 'Delivery Blocked Date', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => { 
    ToastAlert('error', 'Delivery Blocked Date', 'unable to save, please try again or contact to system admin');
}

 
       
   
 