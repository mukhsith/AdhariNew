// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
ToastAlert = (type="error", title="Alert", message, imgUrl) => {
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
                <small class="text-${color} d-none">${time}</small>
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

///// Will continue to make it dynamic
ProductToastAlert = (type, icon, headerTitle, message, imgUrl, title, price, prouctUrl) => {
    var imgTag = "";
    if (imgUrl != null) {
        imgTag = `<img src="${imgUrl}" alt="Product Name" height="50px" class="product-image me-2">`;
    }
    var msgTag = "";
    if (message != null) {
        msgTag = `<p class="mb-1">${message}</p>`;
    }
    var toast_template_html = `<div class="toast rounded-3" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header align-items-center rounded-3 rounded-bottom bg-${type} border-0 text-white fw-bold">
                <i class="${icon} me-2"></i>
                <strong class="me-auto">${headerTitle}</strong>
                <a href="javascript:;" class="text-white d-flex" data-bs-dismiss="toast" aria-label="Close"><i class="fa-solid fa-xmark fs-5"></i></a>
            </div>
            <div class="toast-body rounded-3 rounded-top bg-white">
                ${msgTag}
                <div class="d-flex align-items-center">
                    ${imgTag}
                    <div class="d-flex flex-column product-title">
                        <a href="${prouctUrl}" class="text-primary fw-bold product-url">${title}</a>
                        <p class="m-0 product-price">${price}</p>
                    </div>
                </div>
            </div>
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

disableAllElement = (element) => {
    $(element).attr('disabled', true);
}

enableAllElement = (element) => {
    $(element).attr('disabled', false);
}

setSelectedItemByValue = (inputId, id) => {
    console.log(id);
    console.log(inputId);
    $("#" + inputId + " option[value=1" + id + "]").prop('selected', true);
}

setSelectedItemByValueAndTriggerChangeEvent = (inputId, id) => {
    $("#" + inputId + " option[value=" + id + "]").prop('selected', true);
    $("#" + inputId).trigger('change'); //after set list item,fire change event to display the selected item in list
}

setTextValue = (inputId, text) => {
    try {
        return $("#" + inputId).val(text);
    }
    catch (error) {
        showLog('setTextValue:' + inputId + ' is ' + error);
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

setLabelValue = (labelId, text) => {
    try {
        return $("#" + labelId).text(text);
    }
    catch (error) {
        showLog('setLabelValue:' + labelId + ' is ' + error);
    }
}