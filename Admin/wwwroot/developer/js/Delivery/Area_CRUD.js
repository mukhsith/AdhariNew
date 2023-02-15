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
            minOrderAmount: { required: true },
            governorateList: { required: true },
        },
        messages: {
            nameEn: { required: '' },
            nameAr: { required: '' },
            deliveryFee: { required: '' },
            minOrderAmount: { required: '' },
            governorateList: { required: '' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('Area/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    setTextValue("deliveryFee", r.deliveryFee);
    setTextValue("minOrderAmount", r.minOrderAmount);
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
    submitData.append('minOrderAmount', getFloatValue('minOrderAmount'));
    submitData.append('governorateId', getSelectedItemValue('governorateList'));
    submitHiddenData(submitData);
    ajaxPost("Area/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.Area, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/Delivery/AreaList", 100);
    } else {
        //showLog(data);
        ToastAlert('error', Resources.Area, Resources.UnableTosave);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', Resources.Area, Resources.UnableTosave);
}

 
       
   
 