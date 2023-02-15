

var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    fillDropDownList('roleList', 'SystemUser/GetAllRoles', false, '', 'id', 'name', callbackSuccess);
    
    setUpFormValidation(); 
    //fillDropDownListData('roleList', 'SystemUser/GetAllRoles', true, r.roleId, 'id', 'name');
});


//mobileNumber: { required: true, maxlength: 8 },
//emailAddress: { required: true, },
//mobileNumber: { required: '', },
//emailAddress: { required: '', },


setUpFormValidation = () => {
    $.validator.addMethod('lettersonlyRule', function (value, element) {
        return this.optional(element) || /^[^-\s][a-zA-Z\s-]+$/.test(value);
    });

    $("#dataForm").validate({
        errorPlacement:
            function (error, element) { },
        rules: {
            userName: { required: true, minlength: 3, maxlength: 150, },
            fullName: { required: true, minlength: 3, maxlength: 50, },
            password: { required: true, minlength: 5, maxlength: 10, },
            confirmPassword: { required: true, minlength: 5, maxlength: 10, equalTo: "#password", },
            roleList: { required: true,  }
        },
        messages: {
            userName: { required: '' },
            fullName: { required: '' },
            password: { required: '', },
            confirmPassword: { required: '', },
            roleList: { required: 'Please select a Role' }
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });

};

callbackSuccess = () => {
    if (id > 0) {
        ajaxGet('SystemUser/ByIdUser?Id=' + id, callbackGetDataSuccess, callbackGetDataError);
    }
};

callbackGetDataSuccess = (data) => {
     
    if (data.data == null) { 
        return;
    }
    var r = data.data;
    setTextValue('userName', r.userName);
    setTextValue('fullName', r.fullName);
    setTextValue('mobileNumber', r.mobileNumber);

    setTextValue('emailAddress', r.emailAddress);
    $('#emailAddress').prop('readonly', true);

  //  setSelectedItemByValueAndTriggerChangeEvent('genderList', r.genderTypeId); 
    setTextValue('password', r.password);
    setTextValue('confirmPassword', r.password);
    setSelectedItemByValueAndTriggerChangeEvent('roleList', r.roleId);
    setHiddenData(r);
    
}

callbackGetDataError = (error) => {
    ToastAlert('error', 'System User', 'unable to get data, please try again or contact to system admin');
}

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);
    submitData.append('userName', getTextValue('userName'));
    submitData.append('fullName', getTextValue('fullName'));
    submitData.append('mobileNumber', getTextValue('mobileNumber'));
    submitData.append('emailAddress', getTextValue('emailAddress'));
    submitData.append('password', getTextValue('password'));
    submitData.append('genderTypeId', 1);
    submitData.append('RoleId', getSelectedItemValue('roleList'));
    submitHiddenData(submitData);
    //showFormData(submitData); 
    ajaxPost("SystemUser/AddEditUser", submitData, callbackPostSuccess, callbackPostError);

};


callbackPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'System user', 'Saved Successfully');
        setTimeout(() => location.href = "/SystemUserManagement/UserList", 1500);
    } else {
        showLog(data);
         ToastAlert('error', 'System User', 'unable to save, please try again or contact to system admin');
    }
}

callbackPostError = (error) => { 
     ToastAlert('error', 'System User', 'unable to save, please try again or contact to system admin');
}

 
       
   
 