
$(document).ready(function () {
    searchDataTable();
    configureDialogModels();
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
                url: getAPIUrl() + "customer/GetAllForDataTable",
                type: "POST",
                headers: { "Authorization": 'Bearer ' + getToken() },
                data: function (d) {
                    var search = $(":input[type=search]").val();
                    d.customerName = getTextValue("customerName");
                    d.customerMobile = getTextValue("customerMobile");
                    d.customerEmail = getTextValue("customerEmail");
                    d.customerType = getSelectedItemValue("customerTypeList");
                    if (search.length <= 0) { showLoader(); }
                },
                "datatype": "json",
                "dataSrc": function (json) {
                    checkAPIResponse(json);
                    hideLoader();
                    return json.data;
                }
            },

            "columns": [
                { "data": "id", "name": "id" },
                { "data": "active", render: function (data, type, row) { return addCheckedAction('customer/ToggleActive', row); } },
                { "data": "name", "name": "name" },
                { "data": "mobileNumber" },
                { "data": "emailAddress" },
                { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
                { "data": "totalOrders" },
                { "data": "b2B", "name": "b2B", render: function (data, type, row) { return `${getCustomerType(row)}`; } },
                { "data": null, "name": "Actions", render: function (data, type, row) { return `${getActions(row)}`; } },
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

getCustomerType = (row) => {
    //px - 2 rounded - pill fw - bold bg - primary text - light
    //px - 2 rounded - pill fw - bold bg - secondary text - light
    return `<td class="" data-th="Customer Type"><span class="px-2 rounded-pill fw-bold ${(row.b2B ? "bg-secondary" : "bg-primary")} text-light">${(row.b2B ? "B2B": "B2C")}</span></td>`;
}
//  <a href="/sales/subscriptions?id=${row.id}" class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-light" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Subscriptions" data-bs-original-title="View Subscriptions" aria-label="View Subscriptions"><i class="fa fa-boxes"></i></a>
//<span data-bs-toggle="modal" data-id="${row.id}" class="open-display-addresses" data-bs-target="#display-addresses"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-info" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Addresses" data-bs-original-title="View Addresses" aria-label="View Addresses"><i class="fa fa-map-location-dot"></i></a> </span>

getActions = (row) => { 
    var html = `
    <a href="/order/salesOrders?customerId=${row.id}"  class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Orders" data-bs-original-title="View Orders" aria-label="View Orders"><i class="fa fa-box"></i></a>
    <a href="/customer/wallet-history?customerId=${row.id}" class="mb-1 mt-1 me-1 btn btn-sm btn-success" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Wallet History" data-bs-original-title="View Wallet History" aria-label="View Wallet History" aria-describedby="tooltip832581"><i class="fa fa-wallet"></i></a>
    <a href="/customer/cashback-history?customerId=${row.id}" class="mb-1 mt-1 me-1 btn btn-sm btn-danger" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Cashback History" data-bs-original-title="View Cashback History" aria-label="View Cashback History" aria-describedby="tooltip832581"><i class="fa-solid fa-money-bill"></i></a>
<a href="/customer/customerAddress?customerId=${row.id}" class="mb-1 mt-1 me-1 btn btn-sm btn-info" data-bs-toggle="tooltip" data-bs-placement="bottom" title="View Customer Address" data-bs-original-title="View Customer Address" aria-label="View Customer Address" aria-describedby="tooltip832581"><i class="fa fa-map-location-dot"></i></a>
    <span data-bs-toggle="modal" data-id="${row.id}" class="open-change-customer-type" data-bs-target="#change-customer-type"> <a href="javascript:;" class="mb-1 mt-1 me-1 btn btn-sm btn-warning" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Change Customer Type" data-bs-original-title="Change Customer Type" aria-label="Change Customer Type"><i class="fa fa-user-pen"></i></a> </span>
<span data-bs-toggle="modal" data-id="${row.id}" data-customerId="${row.id}" class="open-display-CustomerEdit" data-bs-target="#display-CustomerEdit"> <a href="#" onclick='EditCustomer(${row.id});' class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="Edit Customer" data-bs-original-title="Edit Customer" aria-label="Edit Customer"><i class="fa fa-pen"></i></a> </span>
   `;
    return html;
}
configureDialogModels = () => {
    //href=
    SalesOrdersByCustomer = (customerId) => {
        window.location.href = `/order/salesOrders?customerId=${customerId}`;
    }
    $(document).on("click", ".open-display-wallet", function () {
        //show detail func in DisplayWalletModalPartial
        ajaxGet("customer/GetWalletBalanceByCustomerId?id=" + $(this).data('id'), showWalletSuccess);
    });

    $(document).on("click", ".open-display-addresses", function () { 
        $("#display-addresses .modal-header #dialogAddressCustomerId").val($(this).data('id'));
        ajaxGet("customer/getAllAddress/?id=" + $(this).data('id'), showAddressListSuccess);
    });

    $(document).on("click", ".open-change-customer-type", function () {
       // var customerId = $(this).data('id');
        $("#change-customer-type .modal-header #dialogCustomerTypeId").val($(this).data('id'));
        // $('#addBookDialog').modal('show');
    });
}



EditCustomer = (ID) => {
    //alert(MobileNumber);
    setTextValue("dialogCustomerId", ID);
   
    getCust(ID);
}


 

