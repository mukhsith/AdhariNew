 
$(document).ready(function () {  
    searchUesrPermissionList();
    // ajaxGet('SystemUser/GetAllPermission', cbGetPermissionSuccess, cbGetPermissionError);
});

searchUesrPermissionList = () => {

    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
   

    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "SystemUser/GetAllPermissionForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                //var search = $(":input[type=search]").val();
                //if (search.length <= 0) { showLoader(); }
            },
            "datatype": "json",
            "dataSrc": function (json) {
                //showLog(json);
               // hideLoader();
                return json.data;
            },             
            error: function (error) {
                showLog(error);
                //alert('error; ' + eval(error));    
            }
        }, 
        "columns": [
            {
                "data": "id", "name": "id", render: function (data, type, row) {
                    return `<td data-rowid='${row.id}'"> ${row.id}</td>`;
                },
            },
            {
                "data": "name", "name": "name", render: function (data, type, row) {
                    return addCheckedAction('SystemUser/ToggleActivePermission', row);
                }
            },
            { "data": "displayOrder", render: function (data, type, row) { return row.displayOrder; } },
            { "data": "title", render: function (data, type, row) { return row.title; } },
            { "data": "navigationUrl", render: function (data, type, row) { return row.navigationUrl; } },
            { "data": "icon", render: function (data, type, row) { return (row.icon != null ? row.icon : ''); } },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDateTime(row.createdOn); } },
            { "data": "modifiedOn", render: function (data, type, row) { return (row.modifiedOn != null ? getFormatedDateTime(row.modifiedOn) : ''); } },
            {
                "data": null, "name": "Actions", render: function (data, type, row) {
                    return addEditAction('User Permission','/SystemUserManagement/UserPermissionAddEdit/', row);
                }
            },
        ],

        createdRow: function (row, data, index) {
            //change display order value, to avoid page refresh
            $(row).attr('rowid', data.id);
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],

    });
}
cbGetPermissionError = (data) => {
}

cbGetPermissionSuccess = (data) => {
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    var table = $("#datatable-default- tbody").empty();

    $.each(data.data, function (a, row) {

        table.append("<tr>" +
            "<td>" + row.id + "</td>" +
            "<td data-th='Active/Inactive' class='align-middle text-center'>" +
            addCheckedAction('SystemUser/ToggleActivePermission', row) +
            "</td>" +
            "<td>" + row.displayOrder + "</td>" +
            "<td>" + row.title + "</td>" +
            "<td>" + row.navigationUrl + "</td>" +
            "<td>" + (row.icon != null ? row.icon :'')  + "</td>" +
            "<td>" + getFormatedDateTime(row.createdOn) + "</td>" +
            "<td>" + (row.modifiedOn != null ? getFormatedDateTime(row.modifiedOn) : '') + "</td>" +
            "<td data-th='Action'>" +
            addEditAction('User Permission','/SystemUserManagement/UserPermissionAddEdit/', row) +
            "</td>" +
            "</tr>");
    });
     

    $('#datatable-default-').DataTable({

         dom: '<"row"<"col-6"B><"col-6"f><"col-lg-12 ResponsiveTable"t><"col-lg-4 mt-lg-3 mt-0 text-lg-start text-center"l><"col-lg-4 mt-lg-3 mt-0 text-center"i><"col-lg-4 text-lg-end text-center"p>>',
        buttons: [{
            extend: 'excelHtml5',
            text: '<i class="fa-regular fa-file-excel text-light"></i> <span class="text-light">Excel</span>',
            titleAttr: 'Excel'
        },
        {
            extend: 'pdfHtml5',
            text: '<i class="fa-regular fa-file-pdf text-light"></i> <span class="text-light">PDF</span>',
            titleAttr: 'PDF'
        }

        ],
         createdRow: function (row, data, index) {
            //    Reinitialize ios-switch
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
    });
}
  
getPermissionHtml=(row,checked) =>{
    var li = "<li class='col-12 col-md-8 list-group-item1 py-1 px-2 d-flex justify-content-between align-items-center mx-auto border-bottom'>" +
        "<p class='mb-0 me-3'>"+ row.title +"</p>" +
        "<div class='switch switch-sm switch-primary'>" +
        "<input type='checkbox' name='switch_"+row.id+"' data-id='"+row.id+"' data-plugin-ios-switch "+ (checked ? 'checked' :'')+" />" +
        "</div>" +
        "</li>";
    return li;
}


