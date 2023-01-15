
$(document).ready(function () {
    searchDataTable(); 
});

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        responsive: true,
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "PaymentMethod/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) { 
                showLoader(); 
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                checkAPIResponse(json);
                hideLoader();
                return json.data;
            }
        }, 
                              
                            
                             
        "columns": [
            {"data": "id", "name": "id"},
            { "data": "nameEn" },

            {
                "data": "imageUrl", render: function (data, type, row) {
                    return `<img src='${row.imageUrl}' width='50' height='50'>`;
                }
            },
            {
                "data": "normalCheckoutRegisteredCustomer", render: function (data, type, row) {
                    return addNormalCheckedBox('PaymentMethod/ToggleActiveNormalRegistered', row);
                }
            },
            {
                "data": "subscriptionCheckoutRegisteredCustomer", render: function (data, type, row) {
                    return addSubscriptionCheckedBox('PaymentMethod/ToggleActiveSubscriptionRegistered', row);
                }
            },
             
        ],
          
        createdRow: function (row, data, index) {
            //change display order value, to avoid page refresh
            $(row).attr('data-rowid', data.id);
            // $(row).find('td:eq(1)').attr('data-displayorder', row.id);

            //    Reinitialize ios-switch
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        } 
        
    });
}
 
addNormalCheckedBox = (apibase, row) => {
    var html = `<div class="switch switch-sm switch-primary my-0  " onclick="doTogglePayment('${apibase}',${row.id});">
        <input ${(row.normalCheckoutRegisteredCustomer ? "checked=true" : "")} type="checkbox"   data-plugin-ios-switch />
    </div>`;
    return html;
}
addSubscriptionCheckedBox = (apibase, row) => {
    var html = `<div class="switch switch-sm switch-primary my-0  " onclick="doTogglePayment('${apibase}',${row.id});">
        <input ${(row.subscriptionCheckoutRegisteredCustomer ? "checked=true" : "")} type="checkbox"   data-plugin-ios-switch />
    </div>`;
    return html;
}

//all api must have a ToggleActive method, callback to the datatable for displayOrder Update
doTogglePayment = (apibase, id) => {
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