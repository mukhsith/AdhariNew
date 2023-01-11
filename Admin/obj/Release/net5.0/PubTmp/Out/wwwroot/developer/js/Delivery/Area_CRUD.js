var id = 0;
$(document).ready(function () {
    setup();
    id = getIntegerValue("Id");
    fillDropDownList ("governorateList", 'governorate/GetAll', false, null, "id", "nameEn", loadDataFor);
});
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            nameEn: { required: true },
            nameAr: { required: true },
            deliveryFee: { required: true },
            governorateList: { required: true },
        },
        messages: {
            nameEn: { required: 'Required' },
            nameAr: { required: 'Required' },
            deliveryFee: { required: 'Required' },
            governorateList: { required: 'Required' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('Governorate/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    setTextValue("deliveryFee", r.deliveryFee);
    setSelectedItemByValueAndTriggerChangeEvent("governorateList", r.governorateId);
    
    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id); 
    submitData.append('nameEn', getTextValue('nameEn'));
    submitData.append('nameAr', getTextValue('nameAr'));
    submitData.append('deliveryFee', getFloatValue('deliveryFee'));
    submitData.append('governorateId', getSelectedItemValue('governorateList'));
    submitHiddenData(submitData);
    ajaxPost("Area/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'Area', 'Saved Successfully');
        setTimeout(() => location.href = "/Delivery/AreaList", 100);
    } else {
        //showLog(data);
        ToastAlert('error', 'Area', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => { 
    ToastAlert('error', 'Area', 'unable to save, please try again or contact to system admin');
}

 
       
   
 