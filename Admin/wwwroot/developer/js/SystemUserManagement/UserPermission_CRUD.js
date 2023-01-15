var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    loadDataFor();
    setUpFormValidation();

});

setUpFormValidation = () => { 
    $("#dataForm").validate({
        rules: {
            title: { required: true, minlength: 5, maxlength: 50, },
            navigationUrl: { required: true }, 
        },
        messages: {
            title: { required: 'Required' },
            navigationUrl: { required: 'Required' }  
        },

        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });

};




loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('SystemUser/GetPermissionById?Id=' + id, callbackGetDataSuccess, callbackGetDataError);
};
callbackGetDataSuccess = (data) => {
    if (data.data == null) return;
    var r = data.data;
    setTextValue('title', r.title);
    setTextValue('titleAr', r.titleAr);

    setTextValue('navigationUrl', r.navigationUrl);
    setTextValue('icon', r.icon);
    setTextValue('permissionId', r.parentPermissionId);
    setTextValue("displayOrder").val(r.displayOrder);
//    setTextValue('displayOrder',33);
    setCheckValue('isActive', r.active); 
    setHiddenData(r);
}

callbackGetDataError = (error) => {
    ToastAlert('error', 'System Permission', 'unable to get data, please try again or contact to system admin');
}

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);
    submitData.append('title', getTextValue('title'));
    submitData.append('titleAr', getTextValue('titleAr'));
    submitData.append('navigationUrl', getTextValue('navigationUrl'));
    submitData.append('icon', getTextValue('icon'));
    submitData.append('parentPermissionId', getTextValue('permissionId'));
    submitData.append('displayOrder', getIntegerValue('displayOrder'));
    submitData.append('active', getCheckValue('isActive'));
    submitHiddenData(submitData); 
    ajaxPost("SystemUser/AddEditPermission", submitData, callbackPostSuccess, callbackPostError);

};


callbackPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'System User Permission', 'Saved Successfully');
        setTimeout(() => location.href = "/SystemUserManagement/UserPermissionList", 500);
    } else {
        showLog(data);
        ToastAlert('error', 'System Permission', 'unable to save, please try again or contact to system admin');
    }
}

callbackPostError = (error) => {
    ToastAlert('error', 'System Permission', 'unable to save, please try again or contact to system admin');
}

