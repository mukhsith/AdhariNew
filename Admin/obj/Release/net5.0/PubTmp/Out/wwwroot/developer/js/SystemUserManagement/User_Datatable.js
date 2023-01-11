
$(document).ready(function () {
    //ajaxGet('SystemUser/GetAllUser', cbGetAllSuccess, cbGetAllError);
    searchUesrList();
});
 
 
searchUesrList = () => {

    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }

    $("#datatable-default-").DataTable({
        searching: true,
        "ajax": {
            url: getAPIUrl() + "SystemUser/GetAllUserForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
               // showLoader();
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
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
                    return addCheckedAction('SystemUser/ToggleActiveUser', row);
                }
            },
            { "data": "fullName", render: function (data, type, row) { return row.fullName; } },
            { "data": "mobileNumber", render: function (data, type, row) { return row.mobileNumber; } },
            { "data": "emailAddress", render: function (data, type, row) { return row.emailAddress; } },
            { "data": "gender", render: function (data, type, row) { return getGenderName(row); } },
            { "data": "role.name", "name": "Role Name" },
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDateTime(row.createdOn); } },
            { "data": "modifiedOn", render: function (data, type, row) { return (row.modifiedOn != null ? getFormatedDateTime(row.modifiedOn) : ''); } },

            {
                "data": null, "name": "Actions", render: function (data, type, row) {
                    return addEditAction('User','/SystemUserManagement/UserAddEdit/', row);
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
cbGetAllSuccess = (data) => {
    var table = $("#datatable-default- tbody");
     table.empty();
     $.each(data.data, function (a, row) {
        table.append("<tr>" +
            "<td>" + row.id + "</td>" +
            "<td data-th='Active/Inactive' class='align-middle text-center'>" +
            addCheckedAction('SystemUser/ToggleActiveUser',row) +
            "</td>" +
            "<td>" + row.fullName + "</td>" +
            "<td>" + row.mobileNumber + "</td>" +
            "<td>" + row.emailAddress + "</td>" +
            "<td>" + getGenderName(row) + "</td>" +
            "<td>" + getFormatedDateTime(row.createdOn) + "</td>" +
            "<td>" + row.role.name + "</td>" +
            "<td data-th='Action'>" +
            addEditAction('User','/SystemUserManagement/UserAddEdit/', row) +
            "</td>" +
        "</tr>");
     });


            
    $('.datatable-default-').DataTable({
        dom: '<"row"<"col-6"B><"col-6"f><"col-lg-12 ResponsiveTable"t><"col-lg-4 mt-lg-3 mt-0 text-lg-start text-center"l><"col-lg-4 mt-lg-3 mt-0 text-center"i><"col-lg-4 text-lg-end text-center"p>>',
        buttons: 
        [{
            extend: 'excelHtml5',
            text: '<i class="fa-regular fa-file-excel text-light"></i> <span class="text-light">Excel</span>',
            titleAttr: 'Excel',
               exportOptions: {
                  columns: ':not(:last-child)',
               },
         },
         {  extend: 'pdfHtml5',
            text: '<i class="fa-regular fa-file-pdf text-light"></i> <span class="text-light">PDF</span>',
             titleAttr: 'PDF',
             exportOptions: {
                 columns: ':not(:last-child)',
             },
         },
         //{
         //   extend: 'csvHtml5',
         //   text: '<i class="fa-regular fa-file-text-o text-light"></i> <span class="text-light">CSV</span>',
         //   titleAttr: 'CSV',
         //   title: 'User List',
         //      exportOptions: {
         //         columns: ':not(:last-child)',
         //      }
         //},
         //{
         //    extend: 'print',
         //    titleAttr: 'Print',
         //    exportOptions: {
         //        columns: [0,  2, 3, 4, 5, 6, 7],
         //    },
         //       }
            ],
        createdRow: function (row, data, index) {
                    //    Reinitialize ios-switch
              $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
    });
    
}
 
cbGetAllError = (data) => {
    alert('error')
}
getGenderName = (row) => {
    if (row.genderTypeId == 0) {
        return "None";
    } else if (row.genderTypeId == 1) {
        return "Male";
    } else if (row.genderTypeId == 2) {
        return "Female";
    }
}