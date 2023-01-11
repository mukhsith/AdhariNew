
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
            url: getAPIUrl() + "CustomerFeedback/GetAllForDataTable",
            type: "POST",
            headers: { "Authorization" : 'Bearer ' + getToken()},
            data: function (d) {
                //showLoader();
            },
            "datatype": "json",
            "dataSrc": function (json) {
               showLog(json);
               // hideLoader();
                return json.data;
            },
            //success: function (html) {
            //    alert('successful : ' + html);
            //    //$("#result").html("Successful");
            //},
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
            {"data": "name", "name": "name",  render: function (data, type, row) {return row.name;}},
            {"data": "emailAddress", render: function (data, type, row) { return row.emailAddress;}}, 
            {"data": "mobileNumber", render: function (data, type, row) {return row.mobileNumber;}},
            {"data": "subject", render: function (data, type, row) {return row.subject;}}, 
            {"data": "message", render: function (data, type, row) {return row.message;}},
            {"data": "createdOn", render: function (data, type, row) {return getFormatedDateTime(row.createdOn);}},
            {"data": "status", render: function (data, type, row) {return `<td data-status='${row.id}'> ${feedbackStatus(row)}</td>`;}},
            {"data": null, "name":"Actions", render: function (data, type, row) {return feedbackActions(row);}},  
        ],
         
        createdRow: function (row, data, index) {
            //change display order value, to avoid page refresh
             $(row).attr('rowid', data.id);
          //  $(row).find('td:eq(1)').attr('data-displayorder', row.id);  
        },
       
        columnDefs: [
            { "targets": -1, "orderable": false },
            { "className": "text-wrap", "targets": "_all" },
        ],
        
    });
    
}

feedbackStatus = (row) => {
    if (row.status == 0) {
        return `<span class="px-2 rounded-pill fw-bold text-light bg-warning" >Unread</span >`;
    } else {
        return `<span class="px-2 rounded-pill fw-bold text-light bg-success">Completed</span>`;
    }
    
}

//
feedbackActions = (row) => {
    var html = `<td class="text-center">`;
    html += (row.status == 0 ? `<a href='javascript:changeStatus(${row.id})'  class="mb-1 mt-1 me-1 btn btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-placement="bottom" title="" data-bs-original-title="Read" aria-label="Read"><i class="fa fa-flag"></i></a>` : '');
    html += `<a href='mailto:bkdurrani@mediaphonekwt.com' class="mb-1 mt-1 me-1 btn btn-sm btn-secondary text-white" data-bs-toggle="tooltip" data-bs-placement="bottom" title="" data-bs-original-title="Email" aria-label="Email"><i class="fa fa-envelope"></i></a> 
          <a href='javascript:openWhatsApp(${row.mobileNumber})' class="mb-1 mt-1 me-1 btn btn-sm btn-success" data-bs-toggle="tooltip" data-bs-placement="bottom" title="" data-bs-original-title="Message" aria-label="Message"><i class="fa fa-message"></i></a>
           </td>`;
    return html;
}

openWhatsApp = (mobile) => {
    newMobile = mobile;
    if (mobile.length > 8) {
       newMobile = mobile.substring(mobile.length - 8);
    }
    window.open("https://wa.me/+965" + mobile);
}

changeStatus = (id) => {
    let submitData = new FormData();
    submitData.append("id", id);
    submitData.append("status", 1);
    ajaxPost("CustomerFeedback/UpdateStatus", submitData, cbUpdateSuccess, undefined);
}

cbUpdateSuccess = (data) => {
    if (data.data == null) { return; }
    if (data.statusCode == 200) {
        $("nav#menu .customerNotificationCount").html(data.data.pendingNotificationBadgeCount)
        searchDataTable(); //recall datatable

    }
}