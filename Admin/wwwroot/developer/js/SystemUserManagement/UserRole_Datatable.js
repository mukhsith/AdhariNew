 
$(document).ready(function () {
    searchList();
    //ajaxGet('SystemUser/GetAllRoles', cbGetRolesSuccess, cbGetRolesError);
    ajaxGet('SystemUser/GetAllPermission', callbackGetAllPermissionSuccess, callbackGetAllPermissionError);
});

searchList = () => {

    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }


    $("#datatable-default-").DataTable({
         
        "ajax": {
            url: getAPIUrl() + "SystemUser/GetAllRolesForDataTable",
            type: "POST",
            headers: { "Authorization": 'Bearer ' + getToken() },
            data: function (d) {
                showLoader();
            },
            "datatype": "json",
            "dataSrc": function (json) {
                showLog(json);
                hideLoader();
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
                "data": "active", "name": "active", render: function (data, type, row) {
                    return addCheckedAction('SystemUser/ToggleActiveRole', row);
                }
            },
            { "data": "name", render: function (data, type, row) { return row.name; } },  
            { "data": "createdOn", render: function (data, type, row) { return getFormatedDateTime(row.createdOn); } },
            { "data": "modifiedOn", render: function (data, type, row) { return (row.modifiedOn != null ? getFormatedDateTime(row.modifiedOn) : ''); } },
            {
                "data": null, "name": "Actions", render: function (data, type, row) {
                    return addEditAction('User Role','/SystemUserManagement/UserRoleAddEdit/', row) + addEditPermissionAction(row);
                }
            },
        ],

        createdRow: function (row, data, index) {
            //change display order value, to avoid page refresh
            $(row).attr('rowid', data.id);
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
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
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],

    });
}
callbackGetAllPermissionSuccess = (data) => {
    addStorage("permissions", data.data); //for reuse 
}
callbackGetAllPermissionError = (data) => {
}

cbGetRolesSuccess = (data) => {
    showLog(data);
    var table = $("#datatable-default- tbody").empty();
   
    $.each(data.data, function (a, row) {

        table.append(`<tr> 
            <td data-th="ID">${row.id}</td>
            <td data-th="Status">${addCheckedAction('SystemUser/ToggleActiveRole', row)}</td>
            <td>${row.name}</td>
            <td>${getFormatedDateTime(row.createdOn)}</td>
            <td>${(row.modifiedOn != null ? getFormatedDateTime(row.modifiedOn) : '')}</td>
            <td data-th='Action'>
                ${addEditAction('User Role','/SystemUserManagement/UserRoleAddEdit/', row)}
                ${addEditPermissionAction(row)}
             </td>
             </tr>`);
    });
     

    $('.datatable-default-').DataTable({
        dom: '<"row"<"col-6"B><"col-6"f><"col-lg-12 ResponsiveTable"t><"col-lg-4 mt-lg-3 mt-0 text-lg-start text-center"l><"col-lg-4 mt-lg-3 mt-0 text-center"i><"col-lg-4 text-lg-end text-center"p>>',
        buttons:
            [{
                extend: 'excelHtml5',
                text: '<i class="fa-regular fa-file-excel text-light"></i> <span class="text-light">Excel</span>',
                titleAttr: 'Excel'
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa-regular fa-file-pdf text-light"></i> <span class="text-light">PDF</span>',
                titleAttr: 'PDF'
            },
            ],
        createdRow: function (row, data, index) {
            //    Reinitialize ios-switch
            $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
    });
}

cbGetRolesError = (data) => {
    ToastAlert('error', 'System User Role', 'unable to save, please try again or contact to system admin');

}

addEditPermissionAction = (row) => {
   return "<a  href='#' onclick=dialogPermission(" + row.id + ") class='mb-1 mt-1 me-1 btn btn-success btn-sm' data-bs-placement='bottom' title='Edit Permission'><i class='fas fa-cogs'></i></a>";
}
 

dialogPermission = (userRoleId) => {
    //current role 
    $(".permissions-list").data("roleId", userRoleId);
    ajaxGet('SystemUser/GetPermissionByRoleId?id=' + userRoleId, callbackGetRolePermissionSuccess, callbackGetAllPermissionError);

}
 
callbackGetRolePermissionSuccess = (data) => {
    $('#edit_permissions').modal('toggle'); //show popup window for permissions
    var permissions = getStorage("permissions"); //get all permissions
    var divPermission = $(".permissions-list").empty(); //empty permission <li> list,if exists
    $.each(permissions, function (a, row) {
        if (data.data.length > 0) {

            const results = data.data.filter(obj => { return obj.permissionId === row.id; });
            if (results.length > 0) {
                divPermission.append(getPermissionHtml(row, results[0].allowed));
            } else {
                divPermission.append(getPermissionHtml(row, false));
            }
         
        } else {
            divPermission.append(getPermissionHtml(row,false));
        }
    });

    $('#edit_permissions').find('[data-plugin-ios-switch]').themePluginIOS7Switch();

     
}
callbackGetRolePermissionError = (data) => {

}

 
saveRolePermission = () => { 
    var index = 0;
    let submitData = new FormData();
    submitData.append('roleId', $(".permissions-list").data("roleId"));
    $(".permissions-list >li input:checkbox").each(function () {
        submitData.append(`permissions[][${index}][id]`, $(this).data("id"));
        submitData.append(`permissions[][${index}][allowed]`, $(this)[0].checked);
        index += 1;
    });

    ajaxPost("SystemUser/UpdateRolePermission", submitData, cbUpdateRole, cbUpdateRoleError);

}
cbUpdateRole = (data) => {
    ToastAlert('success', 'Role Permission', 'Role Permissions are updated successfully');
     
    $('#edit_permissions').modal('toggle');
}
cbUpdateRoleError = (data) => {
    ToastAlert('error', 'Role Permission', 'unable to save, please try again or contact to system admin');
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


