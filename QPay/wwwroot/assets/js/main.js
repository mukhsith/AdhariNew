"use strict";

$(document).ready(function () {
    // Set direction = rtl attribute to html tag when switched to RTL
    if ($('body').css('direction') == 'rtl') {
        $('html').attr('dir', 'rtl');
    } // showLoader();


    setTimeout(function () {
        hideLoader();
    }, 2000);

});
$(document).ready(function () {
    if ($('body').css('direction') == 'rtl') {
        $('select:not(.no-search)').each(function () {
            if ($(this).closest(".modal").length) {
                var modalID = "#" + $(this).closest(".modal").attr("id");
                $(this).select2({
                    dir: "rtl",
                    theme: 'bootstrap',
                    dropdownParent: modalID
                });
            }
            else {
                $(this).select2({
                    dir: "rtl",
                    theme: 'bootstrap'
                });
            }
        });

        $('select.no-search').each(function () {
            if ($(this).closest(".modal").length) {
                var modalID = "#" + $(this).closest(".modal").attr("id");
                $(this).select2({
                    dir: "rtl",
                    theme: 'bootstrap',
                    dropdownParent: modalID,
                    minimumResultsForSearch: Infinity
                });
            }
            else {
                $(this).select2({
                    dir: "rtl",
                    theme: 'bootstrap',
                    minimumResultsForSearch: Infinity
                });
            }
        });
    } else {
        $('select:not(.no-search)').each(function () {
            if ($(this).closest(".modal").length) {
                var modalID = "#" + $(this).closest(".modal").attr("id");
                $(this).select2({
                    theme: 'bootstrap',
                    dropdownParent: modalID
                });
            }
            else {
                $(this).select2({
                    theme: 'bootstrap'
                });
            }
        });

        $('select.no-search').each(function () {
            if ($(this).closest(".modal").length) {
                var modalID = "#" + $(this).closest(".modal").attr("id");
                $(this).select2({
                    theme: 'bootstrap',
                    dropdownParent: modalID,
                    minimumResultsForSearch: Infinity
                });
            }
            else {
                $(this).select2({
                    theme: 'bootstrap',
                    minimumResultsForSearch: Infinity
                });
            }
        });
    }
}); // (function ($) {
//   "use strict";
//   // Initiate the wowjs
//   new WOW().init();  
// })(jQuery);

/*=============================================
=            lax.js Script            =
=============================================*/

window.onload = function () {
    lax.init(); // Add a driver that we use to control our animations

    lax.addDriver('scrollY', function () {
        return window.scrollY;
    }); // Add animation bindings to elements

    lax.addElements('.selector', {
        scrollY: {
            translateX: [["elInY", "elCenterY", "elOutY"], [0, 'screenWidth/2', 'screenWidth']]
        }
    });
};
/*=====  End of lax.js Script  ======*/


function showLoader() {
    $('.loader-container').show(); // 
}

function hideLoader() {
    $('.loader-container').hide("fast");
}