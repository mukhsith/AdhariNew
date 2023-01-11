1. Banner is link to Constant ProductId, Pending due to product structure not yet created


  $.validator.addMethod('lettersonlyRule', function (value, element) {
        return this.optional(element) || /^[^-\s][a-zA-Z\s-]+$/.test(value);
    });


    //Dynamic Toast 
const show_toast = (toast_header, toast_header_bg, toast_header_text_color, toast_message, delay) => {

    const check_time = (i) => {
        return (i < 10) ? "0" + i : i;
    }

    let today = new Date(),
        h = check_time(today.getHours()),
        m = check_time(today.getMinutes()),
        s = check_time(today.getSeconds());

    let toast_time = h + ':' + m + ':' + s;

    let toast_template_html = `
            <div aria-atomic="true" aria-live="assertive"
              class="toast position-fixed top-0 end-0 m-3"
              role="alert" id="toast_message-${today}"
            >
              <div class="toast-header bg-${toast_header_bg}">
                 
                <strong class="me-auto text-${toast_header_text_color}">${toast_header}</strong>
                <small class="text-${toast_header_text_color}">${toast_time}</small>
                <button aria-label="Close" class="btn-close"
                  data-bs-dismiss="toast" type="button"></button>
              </div>
              <div class="toast-body">${toast_message}</div>
            </div>
          `;

    const toast_wrapper = document.createElement('template');
    toast_wrapper.innerHTML = toast_template_html.trim();
    const awesome_toast = toast_wrapper.content.firstChild;
    document.querySelector('.toast-container').appendChild(awesome_toast);

    new bootstrap.Toast(
        awesome_toast, {
        autohide: false,
        /* set false for demonstration */
        delay: delay
    }
    ).show();
}


ajaxGetForValidation = (url, callbackGetSuccess = undefined, callbackGetError = undefined) => {
    showLoader();
    $.ajax({
        url: getAPIUrl() + url,
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            showLog(data);
            if (callbackGetSuccess) {
                callbackGetSuccess(data);
            }
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {

            if (callbackGetError) {
                showLog(jqXHR);
                callbackGetError(jqXHR);
            };
            hideLoader();
        }
    });
}

ajaxPUT = (url, submitData, callbackPutSuccess = undefined, callbackPutError = undefined) => {
    if (isDebug) {
        showLog('dev Url:' + getAPIUrl() + url);
    }
    $.ajax({
        url: getAPIUrl() + url,
        type: 'PUT',
        dataType: "json",
        crossDomain: false,
        headers: {
            "Authorization": 'Bearer ' + getToken()
        },
        processData: false,
        contentType: false,
        data: submitData,
        success: function (data) {
            showLog(data);
            validateAPIResponse(data.statusCode, data.success, data.message,url);
            if (callbackPutSuccess) {
                callbackPutSuccess(data);
            };
            hideLoader();
        },
        error: function (jqXHR, textStatus, errorThrown) {

            if (callbackPutError) {
                showLog(jqXHR);
                callbackPutError(jqXHR);
            };
            hideLoader();
        }
    });

}


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


{ "data": "createdOn", "name": "createdOn", orderable: true, render: function (data, type, row) {
                    return getFormatedDateTime(row.createdOn);
                }
            },
            { "data": "modifiedOn", "name": "modifiedOn", orderable: true, render: function (data, type, row) {
                    return getFormatedDateTime(row.modifiedOn);
                }
            },




cbGetAllSuccess = (data) => {
    var table = $("#datatable-default- tbody").empty();
     $.each(data.data, function (a, row) {
         table.append(
            `<tr data-rowid='${row.id}'>
                <td>${row.id}</td>
                <td data-displayorder='${row.id}'> ${row.displayOrder}</td>
                <td data-th='Active/Inactive' class='align-middle text-center'>
                    ${addCheckedAction('Banner/ToggleActive', row)}
                </td> 
                <td><img src='${row.imageUrlEn}' width='50' height='50'></td>
                <td><img src='${row.imageUrlAr}' width='50' height='50'></td>
                <td> ${(row.linkType == 1 ? 'Link' : 'Product')}</td>
                <td> ${(row.linkType == 1 ? row.linkUrl : row.productId)}</td>
                <td data-th='Action'>
                    ${addEditAction('Banner','/Content/BannerAddEdit/', row)}
                    ${addPopupAction('Banner', row,'datatable-default-')}
                </td>
            </tr>`
         );
     });


            
    $('.datatable-default-').DataTable({
        stateSave: true,
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
            ],
        createdRow: function (row, data, index) {
                    //    Reinitialize ios-switch
              $(row).find('[data-plugin-ios-switch]').themePluginIOS7Switch();
        },
    });
    
}
 
cbGetAllError = (data) => {
    alert('error', data);
}
