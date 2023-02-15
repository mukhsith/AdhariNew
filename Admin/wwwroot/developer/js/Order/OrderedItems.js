var selectedTab = 0;
selectedDataTableName = "#items-datatable-default-";
$(document).ready(function () {

    var d = new Date();
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var localdate = day + "/" + month + "/" + year;


    $("#startDate").val(localdate);
    $("#endDate").val(localdate);


    
    searchDataTable();
  
});


searchOrder = () => {

    var sDate = getFormatedSearchDate($("#startDate").val());
    var eDate = getFormatedSearchDate($("#endDate").val());


    ajaxGetCart("Order/OrderSalesFilterSummary?startDate=" + sDate + "&endDate=" + eDate,
        function (data) {

           
            $("#itemsSoldToday").html(data.formattedItemsSoldToday);
           

        });
    searchDataTable();
}



searchDataTable = () => {

    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }

    $("#datatable-default-").DataTable({
        responsive: true,
        searching: true,
        serverSide: true,
        "ajax": {
            url: getAPIUrl() + "Order/GetSalesOrderedItemsForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                var search = $(":input[type=search]").val();
                if (search.length <= 0) { showLoader(); }
                d.startDate = getDatePickerValue("startDate");
                d.endDate = getDatePickerValue("endDate");


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
            { "data": "title", "name": "title" },
            
            {
                "data": "quantity", render: function (data, type, row) { return row.quantity; }
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






$("#btnclear").click(function () {
    $('#startDate').val('');
    $('#endDate').val('');
});






