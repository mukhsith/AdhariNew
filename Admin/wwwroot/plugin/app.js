$(document).ready(function () {   




    jQuery('#preloader').fadeOut(1000, function () {
        $(this).remove();
    });
   

    $('.gototop').click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, "slow");
    });

    var FileName = $('.FileName');
    FileName.each(function () {
        var strFileName = $(this).text();
        if (strFileName.length > 10) strFileName = strFileName.substring(0, 7) + "..." + strFileName.substr(strFileName.length - 7);
        $(this).text(strFileName);
    });
    

    var windowWidth = $(document).width();
    // $('[data-toggle="tooltip"]').tooltip();
    //    $('#main-menu').smartmenus();
    //    $('#main-menu1').smartmenus();
    //    $('#main-menu').smartmenus({
    //        subMenusSubOffsetX: 1,
    //        subMenusSubOffsetY: -8
    //    });
    
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $($.fn.dataTable.tables(true)).DataTable()
            .columns.adjust()
            .responsive.recalc();
    });

    $('#collapse-myTab').on('shown.bs.collapse', function (e) {
        $($.fn.dataTable.tables(true)).DataTable()
            .columns.adjust()
            .responsive.recalc();
    });
    



    if ($("input[type='number']").length > 0) {
        $("input[type='number']").inputSpinner();
    }
    
        

   
    

    








    // fakewaffle.responsiveTabs(['xs', 'sm']);



    // var FooterDate = new Date();
    // var FooterYear = FooterDate.getFullYear();
    // document.getElementById("year").innerHTML = FooterYear;
});