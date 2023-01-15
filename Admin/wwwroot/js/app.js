$(document).ready(function () {   

        // Added by Adil
        advancedExpandMenuItem();
        sidebarLeftCollapse();

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

    //if ($("input[type='number']").length > 0) {
    //    $("input[type='number']").inputSpinner();
    //}
    


});

// Added by Adil
// Gets Browser URL and Compares to the Side Menu and assigns Active Class.
function advancedExpandMenuItem_OFF(){
    if (document.location.pathname != "/"
        && document.location.pathname != null
        && document.location.pathname.match(/[^\/]+$/) != null) {
      var htmlname = document.location.pathname.match(/[^\/]+$/)[0];
      $('#menu').find("a[href='" + htmlname + "']").parent().addClass('nav-active');
      $('#menu').find("a[href='" + htmlname + "']").closest('.nav-parent').addClass('nav-expanded nav-active');
    }
    else{
      var htmlname = "index.html";
      $('#menu').find("a[href='" + htmlname + "']").parent().addClass('nav-active');
      $('#menu').find("a[href='" + htmlname + "']").closest('.nav-parent').addClass('nav-expanded nav-active');
    }
}

function advancedExpandMenuItem() {
    var t = document.location.pathname;
    $("#menu").find("a[href='" + t + "']").parent().addClass("nav-active");
    $("#menu").find("a[href='" + t + "']").closest(".nav-parent").addClass("nav-expanded nav-active");
}
//  Collapses and Expands Sidemenu
function sidebarLeftCollapse(){
    if(localStorage.getItem("sidebar-left-collapsed") == "true"){
        if(!$('html').hasClass('sidebar-left-collapsed')){
            $('html').addClass('sidebar-left-collapsed');
        }
    }
    else{
        if($('html').hasClass('sidebar-left-collapsed')){
            $('html').removeClass('sidebar-left-collapsed');
        }
    }
}
// Custom Event to Expand Sidemenu Once advancedExpandMenuItem has been executed to Completion
$('[data-fire-event="sidebar-left-toggle"]').click(function(){
    setTimeout(function(){
        if($('html').hasClass('sidebar-left-collapsed')){
            localStorage.setItem("sidebar-left-collapsed", true);
        }
        else{
            localStorage.setItem("sidebar-left-collapsed", false);
        }
        console.log(localStorage.getItem("sidebar-left-collapsed"));
    },500);
});