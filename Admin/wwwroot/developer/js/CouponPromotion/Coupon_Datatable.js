
$(document).ready(function () {
    searchDataTable(); 
});

searchDataTable = () => {
   
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    
    $("#datatable-default-").DataTable({
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "Coupon/GetAllForDataTable",
            type: "POST",
            headers: {"Authorization": 'Bearer ' + getToken()},
            data: function (d) {
                d.couponCode = $("#couponCode").val();
                d.active = getSelectedItemValue('statusList');
                d.createdOn = getTextValue('createdOn');
                showLoader();

            },
            "datatype": "json",
            "dataSrc": function (json) {
               showLog(json);
                hideLoader();
                return json.data;
            }
        }, 
                              
                            
                             
        "columns": [
            {    "data": "id", "name": "id"},
             
            {
                "data": "active", render: function (data, type, row) {
                    return addCheckedAction('Coupon/ToggleActive', row);
                }
            },
            {
                "data": "couponCode", render: function (data, type, row) { return row.couponCode; }
            },
            {
                "data": "startDate", render: function (data, type, row) { return getFormatedDate(row.startDate); }
            },
            {
                "data": "endDate", render: function (data, type, row) { return getFormatedDate(row.endDate); }
            },
            {
                "data": "limitUsageEnabled", render: function (data, type, row) { return (row.limitUsageEnabled ? 'Yes':'No'); }
            },
            {
                "data": "quantity", render: function (data, type, row) { return row.quantity; }
            },
            {
                "data": "quantityUsed", render: function (data, type, row) { return row.quantityUsed; }
            },
            {
                "data": "validity", render: function (data, type, row) { return getValidity(row); }
            },
            {
                "data": null, "name":"Actions", render: function (data, type, row) {
                    return `${addEditAction('Coupon','/CouponPromotion/CouponAddEdit/', row)}`;
                }
            },  
        ],
         
        createdRow: function (row, data, index) {
            //change display order value, to avoid page refresh
            $(row).attr('data-rowid', data.id);
            $(row).find('td:eq(1)').attr('data-displayorder', row.id);

            //    Reinitialize ios-switch
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
       
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],
        
    });
}
getValidity = (row) => {

    if (row.validity) {
        return `<td data-th="Validity"><span class="px-2 rounded-pill fw-bold bg-danger text-light">` + Resources.Expired +`</span></td>`;
    } else {
        return `<td data-th="Validity"><span class="px-2 rounded-pill fw-bold bg-success text-light">` + Resources.Active+`</span></td>`;
    }
}

