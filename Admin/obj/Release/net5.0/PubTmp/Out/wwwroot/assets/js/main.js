"use strict";

$(document).ready(function () {
  // showLoader();
  advancedExpandMenuItem();
  sidebarLeftCollapse(); // To be removed by the developers!

  setTimeout(function () {
    hideLoader();
  }, 1000);
  console.log("document ready");
});
$(document).on('AdvancedExpandMenuItemDone', function () {
  generateBreadcrumbs();
}); // Gets Browser URL and Compares to the Side Menu and assigns Active Class.

function advancedExpandMenuItem() {
  // Temporary Code. Should be removed by the developers
  var trimcount = 0;

  switch (document.location.hostname) {
    case 'designs.mpp.com.kw':
      var rootFolder = '/adhari/admin/';
      trimcount = rootFolder.length;
      break;

    case 'localhost':
      trimcount = 0;
      break;

    default: // set whatever you want

  }

  if (document.location.pathname != "/" && document.location.pathname != null && document.location.pathname.match(/[^\/]+$/) != null) {
    var htmlname = document.location.pathname;
    $('#menu').find("a[href='" + htmlname + "'],a[href='" + htmlname.substring(1) + "'],a[href='" + htmlname.substring(trimcount) + "']").parent().addClass('nav-active');
    $('#menu').find("a[href='" + htmlname + "'],a[href='" + htmlname.substring(1) + "'],a[href='" + htmlname.substring(trimcount) + "']").parents('.nav-parent').addClass('nav-expanded nav-active');
  } else {
    var htmlname = "/";
    $('#menu').find("a[href='" + htmlname + "'],a[href='" + htmlname.substring(1) + "'],a[href='" + htmlname.substring(trimcount) + "']").parent().addClass('nav-active');
    $('#menu').find("a[href='" + htmlname + "'],a[href='" + htmlname.substring(1) + "'],a[href='" + htmlname.substring(trimcount) + "']").parents('.nav-parent').addClass('nav-expanded nav-active');
  }

  $(document).trigger("AdvancedExpandMenuItemDone");
}

function sidebarLeftCollapse() {
  if (localStorage.getItem("sidebar-left-collapsed") == "true") {
    if (!$('html').hasClass('sidebar-left-collapsed')) {
      $('html').addClass('sidebar-left-collapsed');
    }
  } else {
    if ($('html').hasClass('sidebar-left-collapsed')) {
      $('html').removeClass('sidebar-left-collapsed');
    }
  }
}

$('[data-fire-event="sidebar-left-toggle"]').click(function () {
  setTimeout(function () {
    if ($('html').hasClass('sidebar-left-collapsed')) {
      localStorage.setItem("sidebar-left-collapsed", true);
    } else {
      localStorage.setItem("sidebar-left-collapsed", false);
    }
  }, 500);
});

function generateBreadcrumbs() {
  $('#menu').find('.nav-active').each(function () {
    var href = $(this).children().first().attr('href');
    var name = $(this).children().first().find('span').first().text();

    if (href != "#") {
      name = $(this).children().first().text().trim();
    }

    $('ol.breadcrumbs').append('<li><a href="' + href + '" class="fs-6 fw-bold text-decoration-none">' + name + '</a></li>');
  });
}

function showLoader() {
  $('.loader-container').show();
}

function hideLoader() {
  $('.loader-container').hide();
}

function showAjaxLoader() {
  $('.ajax-loader-container').show();
}

function hideAjaxLoader() {
  $('.ajax-loader-container').hide();
}