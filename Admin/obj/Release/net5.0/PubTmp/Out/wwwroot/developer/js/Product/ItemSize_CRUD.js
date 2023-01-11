﻿var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    setup();
    loadDataFor();
});
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            nameEn: { required: true },
            nameAr: { required: true },
        },
        messages: {
            nameEn: { required: 'Required' },
            nameAr: { required: 'Required' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('ItemSize/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);

    submitData.append('nameEn', getTextValue('nameEn'));
    submitData.append('nameAr', getTextValue('nameAr'));

    submitHiddenData(submitData);
    ajaxPost("ItemSize/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'Item Size', 'Saved Successfully');
        setTimeout(() => location.href = "/Product/ItemSizeList", 100);
    } else {
        //showLog(data);
        ToastAlert('error', 'Item Size', 'unable to save, please try again or contact to system admin');
    }
}

cbPostError = (error) => { 
    ToastAlert('error', 'Item Size', 'unable to save, please try again or contact to system admin');
}

 
       
   
 