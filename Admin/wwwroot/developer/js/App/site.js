// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
 
//summernote for code paste
$(document).ready(function () {

    //date format initialization
    $.extend(theme.PluginDatePicker.defaults, {
        format: "dd/mm/yyyy"
    });

    //initialized

    if ($('.summernote').length > 0) {
         $('.summernote').summernote({
            height: "180",
             disableDragAndDrop: true,
             styleTags: ['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'],
             toolbar: [
                 ['para', ['style']],
                 ['style', ['bold', 'italic', 'underline']],
                 ['para', ['ul', 'ol']]
             ]
        });

        // Sanitization being done OnPaste
        $(".summernote").on("summernote.paste", function (e, ne) {
            var bufferText = ((ne.originalEvent || ne).clipboardData || window.clipboardData).getData('Text');
            ne.preventDefault();
            document.execCommand('insertText', false, bufferText);
            $(ne.currentTarget).find("*").removeAttributes();
        });
        // Sanitization being done OnChange
        $('.summernote').on('summernote.change', function (we, contents, $editable) {
            $editable.find("*").removeAttributes();
        });

        // Helper Function to Remove Attributes
        jQuery.fn.removeAttributes = function () {
            return this.each(function () {
                var attributes = $.map(this.attributes, function (item) {
                    return item.name;
                });
                var obj = $(this);
                $.each(attributes, function (i, item) {
                    obj.removeAttr(item);
                });
            });
        }

        sanitizeSummernoteAndGetContent = (summernoteID) => {
            // Sanitizing of All Attributes
            $("#" + summernoteID + " + .note-editor .note-editable *").removeAttributes();
            // Returning Clean Code
            return $("#" + summernoteID).summernote("code");
        }
    }

});
 
  
showLog = (text)=> {
    if (isDebug) { 
        console.log(text);
    }
}
showFormData = (submitData) => {

    for (var pair of submitData.entries()) {
        console.log(pair[0] + ':' + pair[1]);
    }
}
 
setHiddenData = (r) => {
    setTextValue('createdBy', r.createdBy);
    setTextValue('createdOn', r.createdOn);
    setTextValue('modifiedBy', r.modifiedBy);
    setTextValue('modifiedOn', r.modifiedOn);
}
submitHiddenData = (submitData) => {
   
    if (getIntegerValue('Id')>0) { 
        submitData.append('createdBy', getTextValue('createdBy'));
        submitData.append('createdOn', getTextValue('createdOn'));
        submitData.append('modifiedBy', getTextValue('modifiedBy'));
        submitData.append('modifiedOn', getTextValue('modifiedOn'));
    }
}
reloadPage = () => {
    window.location.reload();
}

disableAllElement =(element)=> {
    $(element).attr('disabled', true);
}
enableAllElement = (element) => {
    $(element).attr('disabled', false);
}

clearInputText = (inputId) => {
    $("#" + inputId).val('');
}

isValidURL = (inputId) => {
    var url = inputId.value;
    var pattern = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
    if (pattern.test(url)) {
        //alert("Url is valid");
        return true;
    } else {
        alert("Url is Invalid");
    }
    return false;

}
ajaxGetCart = (url, callbackGetSuccess = undefined, callbackGetError = undefined) => {
    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
    }
    showLoader();
    $.ajax({
        url: getAPIUrl() + url,
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            showLog(data);
             if (callbackGetSuccess) {
                callbackGetSuccess(data);
            }   
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            hideLoader();
            showLog(jqXHR);
        }
    });
}
ajaxPostCart = (url, formBody, callBackPostSuccess = undefined, callBackPostError = undefined) => {
   
    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
    }
    showLoader();
    $.ajax({
        url: getAPIUrl() + url,
        type: 'POST',
        contentType: 'application/json; charset=utf-8', 
        crossDomain: false,
        headers: { "Authorization": 'Bearer ' + getToken() }, 
        data: JSON.stringify(formBody),
        success: function (data) {
            showLog(data); 
            if (callBackPostSuccess) {  
                    callBackPostSuccess(data);
            }; 
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) { 
            hideLoader();
            showLog(jqXHR);
        }
    });

}
ajaxDeleteCart = (url, callBackPostSuccess = undefined, callBackPostError = undefined) => {

    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
    }
    showLoader();
    $.ajax({
        url: getAPIUrl() + url,
        type: 'DELETE',
        contentType: 'application/json; charset=utf-8',
        crossDomain: false,
        headers: { "Authorization": 'Bearer ' + getToken() }, 
        success: function (data) {
            showLog(data); 
            if (callBackPostSuccess) { //so call callback action
                callBackPostSuccess(data);
            }; 
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            hideLoader();
            showLog(jqXHR);
        }
    });

}
ajaxGet = (url, callbackGetSuccess = undefined, callbackGetError = undefined) => {
    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
    }
    showLoader();
    $.ajax({
        url: getAPIUrl() + url,
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            showLog(data);
            validateAPIResponse(data.statusCode, data.success, data.message, url);
            if (callbackGetSuccess) {
                callbackGetSuccess(data);
            } else {
                callbackGetError(data);
            }
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {

            if (callbackGetError) {
                showLog(jqXHR);
                callbackGetError(jqXHR);
            };
            hideLoader();
        }
    });
}
ajaxPost = (url, submitData, callBackPostSuccess = undefined, callBackPostError = undefined) => {
   
    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
        if (submitData != null) { showFormData(submitData) };
    }
    $.ajax({
        url: getAPIUrl() + url,
        type: 'POST',
        dataType: "json",
        crossDomain: false,
        headers: {
            "Authorization": 'Bearer ' + getToken()
        },
        processData: false,
        contentType: false,
        data: submitData,
        success: function (data) {
            showLog(data);
            var action = validateAPIResponse(data.statusCode, data.success, data.message,url);
            if (action == false) { //no action has been taken
                if (callBackPostSuccess) { //so call callback action
                    callBackPostSuccess(data);
                };
            }
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 404) {
                ToastAlert('danger', 'API 404', 'Bad Request, please check Method Name');
            } else {
                showLog(jqXHR);
                if (callBackPostError) {
                    callBackPostError(jqXHR);
                }
            }
            hideLoader();
        }
    });

}
ajaxPostJSON = (url, parameters, callBackPostSuccess = undefined, callBackPostError = undefined) => {
    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
        if (parameters != null) { showLog(parameters) };
    }
    $.ajax({
        url: getAPIUrl() + url,
        type: 'POST',
        crossDomain: false,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parameters),
        headers: {
            "Authorization": 'Bearer ' + getToken()
        },

        success: function (data) {
            showLog(data);
            var action = validateAPIResponse(data.statusCode, data.success, data.message,url);
            if (action == false) { //no action has been taken
                if (callBackPostSuccess) { //so call callback action
                    callBackPostSuccess(data);
                };
            }
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {

            if (callBackPostError) {
                showLog(jqXHR);
                callBackPostError(jqXHR);
            };
            hideLoader();
        }
    });

}

validateAPIResponse = (statusCode, success, message,url) => {
    showLog(statusCode + " , " + success);
    if (statusCode == 0) { // custom message from backend
        ToastAlert('danger', 'API 0', message + "<br> Please check the admin for below url<br>"+url);
        return true;
    } else if (statusCode == 250) { // custom message from backend
        removeToken();
        ToastAlert('danger', 'Session Timeout', message);
        //window.location.href = '/Account/Index';
        window.location.href = '/Account/SessionExpired';
        return true;
    } else if (statusCode == 300) { // custom message from backend
        ToastAlert('danger', 'API 300', message);
        return true;
    } else if (statusCode == 401) {
        ToastAlert('danger', 'API Permission', 'Sorry!, You are not authorized, please contact your System Administrator');
        setTimeout(window.location.href = "/account/index", 500);
        return true;
    } else if (statusCode == 400) {
        ToastAlert('danger', 'API 400', 'Bad Request, please check \r\nMethod type GET/POST or Method Name');
        return true;
    } else if (statusCode == 404) {
        ToastAlert('danger', 'API 404', 'The requested page is not available');
        return true;
    } else if (statusCode == 405) {
        ToastAlert('danger', 'New User', message);
        return true;
    }
    if (success ==  false && statusCode == 401) {
        removeToken();
        ToastAlert('danger', 'API 401', message);
        window.location.href = '/Account/Index';
        return;
    } else {
        return false;
    }
};
//check for logout
checkAPIResponse = (json) => {
    if (json.success == false && json.statusCode==250) {
        showLog(json);
        removeToken();
        //ToastAlert('danger', 'API ' + json.statusCode, json.message);
        window.location.href = '/Account/SessionExpired';
    }
    return true;
}
//add change event on imageFile, if the file is selected, show it in the preview 
addEventForImageFileAndPreview = (imageFileId, imagePreviewId) => {

    $("#" + imageFileId).change(function (e) {
        var ext = this.value.match(/\.([^\.]+)$/)[1];
        switch (ext) {
            case 'jpg':
            case 'png':
                // alert('Allowed');
                const [file] = this.files
                if (file) {
                    $("#" + imagePreviewId).attr('src', window.URL.createObjectURL(this.files[0]));

                }
                break;
            default:
                alert('Not allowed');
                this.value = '';
        }
    });
     

}

addCheckedAction = (apibase, row) => {
    var html = `<div class="switch switch-sm switch-primary my-0  " onclick="doToggleActivate('${apibase}',${row.id});">
        <input ${(row.active ? "checked=true" : "")} type="checkbox"   data-plugin-ios-switch />
    </div>`;
    return html;
}

//all api must have a ToggleActive method, callback to the datatable for displayOrder Update
doToggleActivate = (apibase, id) => {
    $.ajax({
        url: getAPIUrl() + apibase + "?id=" + id,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            hideLoader();
            if (data.success) {
                ToastAlert('success', 'Active', data.message);
            } else {
                debugger
                ToastAlert('danger', 'Active', data.message);
            }
            return false;
        },
        error: function (xhr) {
            //hideLoader();
            ToastAlert('danger', 'Active', xhr);
        }
    });
}


addEditAction = (title, apibase, row) => {
    var html =
        `<a href='${apibase + row.id}' class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip"
            data-bs-placement="bottom" title="Edit ${title}" data-bs-original-title="Edit Banner" aria-label="Edit Banner">
            <i class="fa fa-pen"></i>
        </a>`;
    return html;
}
doDelete = (apibase,id) => {
        showLoader();
        $.ajax({
            url: getAPIUrl() + apibase + "/Delete?id=" + id,
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + getToken()
            },
            success: function (data) {
                hideLoader();
                if (data.success) {
                    ToastAlert('success', 'Active', data.message);
                    reloadPage();

                } else {
                    showLog(data);
                    ToastAlert('danger', 'Delete', data.message);
                }

            },
            error: function (xhr) {
                hideLoader();
                alert('error', xhr);
            }
        });
}
//selecte dropdown item with value
//dataType: "json",'headers': { "Content-Type": "application/json" },
fillDropDownList = (divId, apiName, keyToMatch, KeyId, optionId, optionName, callbackFunction = undefined) => {
    var myDropDownList = $('#' + divId).empty();
    $.ajax({
        url: getAPIUrl() + apiName,
        type: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken(),
            "lang": $('html')[0].lang
        },
        success: function (data) {
            showLog(data);
            myDropDownList.append($("<option></option>").val(null).html('--Select--'));

            $.each(data.data, function (a, b) {
                if (keyToMatch) {
                    if (b.id == KeyId) {
                        myDropDownList.append($("<option selected></option>").val(b[optionId]).html(b[optionName]));
                    } else {
                        myDropDownList.append($("<option></option>").val(b[optionId]).html(b[optionName]));
                    }
                } else {
                    myDropDownList.append($("<option></option>").val(b[optionId]).html(b[optionName]));
                }

            });
            //after execution, call callbackfunction
            if (callbackFunction) {
                callbackFunction(data);
            };
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}




fillDropDownMultiList = (divId, apiName, keyToMatch, KeyId, optionId, optionName, callbackFunction = undefined) => {
    var myDropDownList = $('#' + divId).empty();
    $.ajax({
        url: getAPIUrl() + apiName,
        type: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken(),
            "lang": $('html')[0].lang
        },
        success: function (data) {
            showLog(data);
           // myDropDownList.append($("<option></option>").val(null).html('--Select--'));

            $.each(data.data, function (a, b) {
                if (keyToMatch) {
                    if (b.id == KeyId) {
                        myDropDownList.append($("<option selected></option>").val(b[optionId]).html(b[optionName]));
                    } else {
                        myDropDownList.append($("<option></option>").val(b[optionId]).html(b[optionName]));
                    }
                } else {
                    myDropDownList.append($("<option></option>").val(b[optionId]).html(b[optionName]));
                }

            });
            //after execution, call callbackfunction
            if (callbackFunction) {
                callbackFunction(data);
            };
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

//parameters
//1-element Id
//2-list data
//3-KeyToMatch for item.id == with user provided Id for selected Item
//4-Key Value to be Matched
//5-Item Field Value
//6-Item Field Name for display
//7-Callback function
fillDropDownListData = (divId, data, keyToMatch, KeyId, optionId, optionName, callbackFunction = undefined) => {
    var myDropDownList = $('#' + divId).empty();
   
            myDropDownList.append($("<option></option>").val(null).html('--Select--'));

            $.each(data, function (a, b) {
                if (keyToMatch) {
                    if (b.id == KeyId) {
                        myDropDownList.append($("<option selected></option>").val(b[optionId]).html(b[optionName]));
                    } else {
                        myDropDownList.append($("<option value='" + b[optionId]+"'></option>").val(b[optionId]).html(b[optionName]));
                    }
                } else {
                    myDropDownList.append($("<option value='" + b[optionId] +"'></option>").val(b[optionId]).html(b[optionName]));
                }

            });
            //after execution, call callbackfunction
            if (callbackFunction) {
                callbackFunction();
            };
        
     
}
setTextValue = (inputId, text) => {
    
    try {
        return $("#" + inputId).val(text);
    }
    catch (error) {
        showLog('setTextValue:' + inputId + ' is ' + error);
    }
}
getTextValue = (inputId) => {
    try {
        return $("#" + inputId).val();
    }
    catch (error) {
        showLog('getTextValue:' + inputId + ' is '+ error);
    }
}

getFloatValue = (inputId) => {
    try { 
        return (isNaN(parseFloat($("#" + inputId).val())) ? 0 : parseFloat($("#" + inputId).val()));
    }
    catch (error) {
        showLog('getFloatValue:' + inputId + ' is ' + error);
    }
    
}
setFloatValue = (inputId, value) => {
     
    try {
        return $("#" + inputId).val(value);
    }
    catch (error) {
        showLog('setIntegerValue:' + inputId + ' is ' + error);
    }

}

getIntegerValue = (inputId) => {
    
    try {
        return (isNaN(parseInt($("#" + inputId).val())) ? 0 : parseInt($("#" + inputId).val()));
    }
    catch (error) {
        showLog('setIntegerValue:' + inputId + ' is ' + error);
    }
}
setIntegerValue = (inputId, value) => {
    
    try {
        return $("#" + inputId).val(value);
    }
    catch (error) {
        showLog('setIntegerValue:' + inputId + ' is ' + error);
    }
}

setCheckValue = (checkBoxId, active) => {
    try {

        var box = $('#' + checkBoxId);
        if (active) {

            box.prop('checked', "checked");
            box.prev().addClass('on');
            box.prev().removeClass('off');
        }
        else {
            box.prop('checked', false);
            box.prev().addClass('off');
            box.prev().removeClass('on');
        }

    }
    catch (error) {
        showLog('setTextValue:' + inputId + ' is ' + error);
    }
}
getCheckValue = (inputId) => {
   
    try {
        return $("#" + inputId)[0].checked
    }
    catch (error) {
        showLog('getCheckedValue:' + inputId + ' is ' + error);
    }
}

setLabelValue = (labelId, text) => {

    try {
        return $("#" + labelId).text(text);
    }
    catch (error) {
        showLog('setLabelValue:' + labelId + ' is ' + error);
    }
}
setLink = (hrefId, text) => {

    try {
        return $("#" + hrefId).attr("href", text);
    }
    catch (error) {
        showLog('setLink:' + hrefId + ' is ' + error);
    }
}

setSelectedItem = (inputId, data) => {
    $("#" + inputId + " option:eq(" + data + ")").prop('selected', true);
}
setSelectedItemByValue = (inputId, id) => {
    $("#" + inputId + " option[value=" + id + "]").prop('selected', true); 
}
setSelectedItemByValueAndTriggerChangeEvent = (inputId, id) => {
    $("#" + inputId + " option[value=" + id + "]").prop('selected', true);
    $("#" + inputId).trigger('change'); //after set list item,fire change event to display the selected item in list
}
getSelectedItemValue = (inputId) => {
    var selectedValue = $('#' + inputId + ' option:selected').val();
    return selectedValue;
}
getSelectedItemText = (inputId) => {
    var selectedText = $('#' + inputId + ' option:selected').text();
    return selectedText;
}

setImage = (inputId, imageUrl) => {
    try {
        $("#" + inputId).attr('src', imageUrl);
    }
    catch (error) {
        showLog('setImage:' + inputId + ' for ' + imageUrl + '-' + error);
    }
}
getImage = (inputId) => { 
    try {
      return  $("#" + inputId).file;
    }
    catch (error) {
        showLog('setImage:' + inputId + ' for ' + imageUrl + '-' + error);
    }
}

ToastAlert = (type, title, message, imgUrl) => {
    var imgTag = "";
    if (imgUrl != null) {
        imgTag = `<img src="${imgUrl}" class="rounded me-2" alt="..." >`;
    }

    if (type == 'error') {
        type = 'danger';
        // color = 'light'; 
    } 
     
    var color = 'light';  
    const check_time = (i) => {
        return (i < 10) ? "0" + i : i;
    }

    var today = new Date(),
        h = check_time(today.getHours()),
        m = check_time(today.getMinutes()),
        s = check_time(today.getSeconds());
    var time = h + ':' + m + ':' + s;

    var toast_template_html =
        `<div aria-atomic="true" aria-live="assertive" class="toast position-fixed top-0 end-0 m-3" role="alert" id="toast_message-${today}">
            <div class="toast-header bg-${type}"> 
                ${imgTag}
                <strong class="me-auto text-${color}">${title}</strong>
                <small class="text-${color}">${time}</small>
                <button aria-label="Close" class="btn-close" data-bs-dismiss="toast" type="button"></button>
            </div>
            <div class="toast-body">${message}</div>
        </div>`;

    var toast_wrapper = document.createElement('template');
    toast_wrapper.innerHTML = toast_template_html.trim();
    var awesome_toast = toast_wrapper.content.firstChild;
    document.querySelector('.toast-container').appendChild(awesome_toast);

    new bootstrap.Toast(
        awesome_toast,
        {
            autohide: true, /* set false for demonstration */
            delay: 6000
        }
    ).show(); 
}
 
//set text field date value
setDatePickerValue = (inputId, dateFromDb) => {
    //1.    datepicker should be added in layout.cshtml because 
    //      it is initialized with date display format 'theme.init.js'
    //2. below script file should be added in the View because ?
    //<script src="~/plugin/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    $('#' + inputId).bootstrapDP('setDates', [getFormatedDate(dateFromDb)]);

}
//for saving in database date column
getDatePickerValue = (inputId) => {
    var _day = $('#' + inputId).val();
    if (_day == null || _day === '') return null;
    //split date based on '-' or '/'
    var _split = (_day.indexOf('-') > 0 ? _day.split('-') : _day.split('/'));
    //return day format (year/month/day)
    var _date = _split[2] + '/' + _split[1] + '/' + _split[0];
    return _date;
}

//display date value in datatable row column
getFormatedDate = (_date) => {
    if (_date == null) return null;
    //remove time from the date if it contains
    var _day = (_date.indexOf('T') > 0 ? _date.split('T')[0] : _date);
    //split date based on '-' or '/'
    var _split = (_day.indexOf('-') ? _day.split('-') : _day.split('/'));
    var _date = _split[2] + '/' + _split[1] + '/' + _split[0];
    return _date;
}


//for saving in database date column
getDatePickerValueN1 = (inputId) => {
    var _day = $('#' + inputId).val();
    if (_day == null || _day === '') return null;
    //split date based on '-' or '/'
    var _split = (_day.indexOf('-') > 0 ? _day.split('-') : _day.split('/'));
    //return day format (year/month/day)
    var _date = _split[2] + '/' + _split[1] + '/' + _split[0] + 'T00:00:00';
    return _date;
}





//display date format for datatable column (month/day/year) (6/6/2022, 1:01:47 PM)
getFormatedDateTime = (_date) => {
    if (_date == null) return null;
    var datetime = new Date(_date).toLocaleString();
    return datetime;
}

validateMobileNumber=(evt)=> {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}
 
maxlength =(event) => {
    const ele = event.target;
    const maxlength = ele.maxLength;
    const value = ele.value;
    if (event.type == 'keypress') {
        if (value.length >= maxlength) {
            event.preventDefault();
        }
    }
    else if (event.type == 'keyup') {
        if (value.length > maxlength) {
            ele.value = value.substring(0, maxlength);
        }
    }
}

getOrderTypeHtml = (row) => {
        if (row.orderTypeId == 1) {
            return `<span class='px-2 rounded-pill fw-bold text-light bg-success'>` + Resources.Online+`</span>`;
        } else {
            return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>` + Resources.Offline+ `</span>`;
        }
}
getOrderStatusHtml = (row) => {
    if (row.orderStatusId == 1) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Pending +`</span>`;
    } else if (row.orderStatusId == 2) {
        return `<span class='px-2 fw-bold text-primary'>` + Resources.Confirmed +`</span>`;
    } else if (row.orderStatusId == 3) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Cancelled +`</span>`;
    } else if (row.orderStatusId == 4) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Confirmed + `</span>`;
      //  return `<span class='px-2 fw-bold text-secondary'>` + Resources.Delivered +`</span>`;
    } else if (row.orderStatusId == 5) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Received +`</span>`;
    } else if (row.orderStatusId == 6) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.OnTheWay +`</span>`;
    } else if (row.orderStatusId == 7) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Returned +`</span>`;
    } else if (row.orderStatusId == 8) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Discarded +`</span>`;
    } else if (row.orderStatusId == 9) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Failed +`</span>`;
    }
else if (row.orderStatusId == 10) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.CancelledByCustomer +`</span>`;
    } else if (row.orderStatusId == 11) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.ReturnedByDriver +`</span>`;
    }   else {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning' title='` + Resources.CancelledByCustomer +`'>` + Resources.Cancelled +`</span>`;
    }

   
}


getOrderDeliveryStatusHtml = (row) => {
    if (row.orderStatusId == 1) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Pending + `</span>`;
    } else if (row.orderStatusId == 2) {
        return `<span class='px-2 fw-bold text-primary'>` + Resources.Confirmed + `</span>`;
    } else if (row.orderStatusId == 3) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Cancelled + `</span>`;
    } else if (row.orderStatusId == 4) {
       return `<span class='px-2 fw-bold text-secondary'>` + Resources.Delivered +`</span>`;
    } else if (row.orderStatusId == 5) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Received + `</span>`;
    } else if (row.orderStatusId == 6) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.OnTheWay + `</span>`;
    } else if (row.orderStatusId == 7) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Returned + `</span>`;
    } else if (row.orderStatusId == 8) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Discarded + `</span>`;
    } else if (row.orderStatusId == 9) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Failed + `</span>`;
    }
    else if (row.orderStatusId == 10) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.CancelledByCustomer + `</span>`;
    } else if (row.orderStatusId == 11) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.ReturnedByDriver + `</span>`;
    } else {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning' title='` + Resources.CancelledByCustomer + `'>` + Resources.Cancelled + `</span>`;
    }


}


getDeliveryStatusHtml = (row) => {
    if (row.orderStatusId == 4) /*Pending*/ {
        return `<span class='px-2 rounded-pill fw-bold text-success'>` + Resources.Delivered + `</span>`;
    } else {
        return `<span class='px-2 rounded-pill fw-bold text-danger'>` + Resources.Pending + `</span>`;
    }
}




getDeviceTypeHtml = (row) => {
    if (row.deviceTypeId == 0) {
        return `<span class='px-2 fw-bold text-warning'>WEB</span>`;
    } else if (row.deviceTypeId == 1) {
        return `<span class='px-2 fw-bold text-secondary'>Android</span>`;
    } else if (row.deviceTypeId == 2) {
        return `<span class='px-2 fw-bold text-primary'>IOS</span>`;
    } else if (row.deviceTypeId == 3) {
        return `<span class='px-2 fw-bold text-warning'>WEB</span>`;
    }
}



getOrderPaidStatusHtml = (row) => {
    if (row.paymentStatusId == 1) /*Pending*/ {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>` + Resources.Unpaid +`</span>`;
    } else if (row.paymentStatusId == 2) /*Captured*/ {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-success'>` + Resources.Paid +`</span>`;
    } else if (row.paymentStatusId == 3) /*NotCaptured*/ {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.NotCaptured +`</span>`;
    } else if (row.paymentStatusId == 4) /*Canceled*/ {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Cancelled +`</span>`;
    } else if (row.paymentStatusId == 5) /*PendingCash*/ {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>` + Resources.Unpaid +`</span>`;
    }
}


getOrderDeliveryStatusHtml = (row) => {
    if (row.delivered == true) /*Pending*/ {
        return `<span class='px-2 rounded-pill bg-primary fw-bold text-light'>` + row.deliveryStatus +`</span>`;
    } else
    {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>` + row.deliveryStatus +`</span>`;
    }
}


getPaymentMethodHtml = (row) => {
    if (row.paymentMethodId == 1) {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-info'>` + Resources.KNET +`</span>`;
    } else if (row.paymentMethodId == 2) {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>` + Resources.VISA_Master +`</span>`;
    } else if (row.paymentMethodId == 3) {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-danger'>` + Resources.Tabby +`</span>`;
    } else if (row.paymentMethodId == 4) {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-primary'>` + Resources.COD +`</span>`;
    }  else if (row.paymentMethodId == 5) {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-success'>` + Resources.Wallet +`</span>`;
    } else {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-info'>` + Resources.QPAY +`</span>`;
    }
}

getOrderDeliveryHtml = (row) => {
    if (row.orderTypeId == 1) {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-success'>` + Resources.Online +`</span>`;
    } else {
        return `<span class='px-2 rounded-pill fw-bold text-light bg-warning'>` + Resources.Offline + `</span>`;
    }
}

getPaymentStatusHtml = (row) => {
    if (row.paymentStatusId == 1) /*Pending*/ {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Pending +`</span>`;
    } else if (row.paymentStatusId == 2) /*Captured*/ {
        return `<span class='px-2 fw-bold text-primary'>` + Resources.Captured +`</span>`;
    } else if (row.paymentStatusId == 3) /*NotCaptured*/ {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.NotCaptured +`</span>`;
    } else if (row.paymentStatusId == 4) /*Canceled*/ {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Cancelled +`</span>`;
    } else if (row.paymentStatusId == 5) /*PendingCash*/ {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.PendingCash +`</span>`;
    }
}

getSubscriptionStatusHtml = (row) => {
    if (row.subscriptionStatusId == 1) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Pending +`</span>`;
    } else if (row.subscriptionStatusId == 2) {
        return `<span class='px-2 fw-bold text-primary'>` + Resources.Confirmed +`</span>`;
    } else if (row.subscriptionStatusId == 3) {
        return `<span class='px-2 fw-bold text-danger'>` + Resources.Expired +`</span>`;
    } else if (row.subscriptionStatusId == 4) {
        return `<span class='px-2 fw-bold text-secondary'>` + Resources.Cancelled +`</span>`;
    }
}


function getFormatedSearchDate(_date) {
    var _split = _date.split('/');
    var _date = _split[1] + '/' + _split[0] + '/' + _split[2];
    return _date;

}
