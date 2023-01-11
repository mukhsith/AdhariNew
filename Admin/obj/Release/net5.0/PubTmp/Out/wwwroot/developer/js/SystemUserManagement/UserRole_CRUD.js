

var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    loadDataFor();
    setUpFormValidation();

});

setUpFormValidation = () => { 
    $("#dataForm").validate({
        rules: {
            name: { required: true, minlength: 3, maxlength: 15, }, 
        },
        messages: {
            name: { required: 'Required' }, 
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });

};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('SystemUser/GetRoleById?Id=' + id, callbackGetDataSuccess, callbackGetDataError);
};
callbackGetDataSuccess = (data) => {
    if (data.data == null) return;
    var r = data.data;
    setTextValue('name', r.name);  
    setHiddenData(r);
}

callbackGetDataError = (error) => {
    ToastAlert('error', 'System User Role', 'unable to get data, please try again or contact to system admin');
}

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);
    submitData.append('name', getTextValue('name'));  
    submitHiddenData(submitData); 
    ajaxPost("SystemUser/AddEditRole", submitData, callbackPostSuccess, callbackPostError);

};


callbackPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'System User Role', 'Saved Successfully');
        setTimeout(() => location.href = "/SystemUserManagement/UserRoleList", 500);
    } else {
        showLog(data);
        ToastAlert('error', 'System User Role', 'unable to save, please try again or contact to system admin');
    }
}

callbackPostError = (error) => {
    ToastAlert('error', 'System User Role', 'unable to save, please try again or contact to system admin');
}

