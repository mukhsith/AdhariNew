
$(document).ready(function () {
    searchDataTable(); 
});
 
searchDataTable = () => {

    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }

    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "customer/GetWalletHistoryForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) { 
                d.customerId = getTextValue("customerId");
                showLoader();
            },
            "datatype": "json",
            "dataSrc": function (json) {
                checkAPIResponse(json);
                hideLoader();
                return json.data;
            }
        },

        "columns": [
            { "data": "id",  }, 
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDate(row.createdOn); } },
            { "data": "transactionNo", },
            { "data": "description",  },
            { "data": "credit",  },
            { "data": "debit",  },
            { "data": "balance", },
             
        ], 
          
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],
       

    });
}

 

