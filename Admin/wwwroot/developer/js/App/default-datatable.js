  
// Disable search and ordering by default
if ($("html").attr("dir") != "rtl") {
    $.extend($.fn.dataTable.defaults, {
        responsive: true,
        pageLength:50,
        searching: true,
        paging: true,
        info: true,
        select: true,
        serverSide: true,
        filter: false, //Search Box
        orderMulti: false,
        stateSave: true,
        ordering: false, //disable ordering
        order: [[0, "desc"]],
        processing: false,
        language: {
            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
        },
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
            {
                extend: 'pdfHtml5',
                text: '<i class="fa-regular fa-file-pdf text-light"></i> <span class="text-light">PDF</span>',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: ':not(:last-child)',
                },
            },
            ],
        lengthMenu: [[100, 5, 10, 15, 25, 50, 200], [100, 5, 10, 15, 25, 50, 200]],
        error: function (xhr, error, code) {
            console.log(xhr, code);
        },
    });
}
else {
    $.extend($.fn.dataTable.defaults, {
        language: {
            url: "/assets/json/ar.json",
            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
        },
        responsive: true,
        pageLength: 50,
        searching: true,
        paging: true,
        info: true,
        select: true,
        serverSide: true,
        filter: false, //Search Box
        orderMulti: false,
        stateSave: true,
        ordering: false, //disable ordering
        order: [[0, "desc"]],
        processing: false,
        dom: '<"row"<"col-6"B><"col-6"f><"col-lg-12 ResponsiveTable"t><"col-lg-4 mt-lg-3 mt-0 text-lg-start text-center"l><"col-lg-4 mt-lg-3 mt-0 text-center"i><"col-lg-4 text-lg-end text-center"p>>',
        buttons:
            [{
                extend: 'excelHtml5',
                text: '<i class="fa-regular fa-file-excel text-light"></i> <span class="text-light">Excel</span>',
                titleAttr:  'Excel',
                exportOptions: {
                    columns: ':not(:last-child)',
                },
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa-regular fa-file-pdf text-light"></i> <span class="text-light">PDF</span>',
                titleAttr: 'PDF',
                exportOptions: {
                    columns: ':not(:last-child)',
                },
            },
            ],
        lengthMenu: [[100, 5, 10, 15, 25, 50, 200], [100, 5, 10, 15, 25, 50, 200]],
        error: function (xhr, error, code) {
            console.log(xhr, code);
        },
    });
}