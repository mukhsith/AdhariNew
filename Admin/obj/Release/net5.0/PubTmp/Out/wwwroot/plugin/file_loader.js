
$.get("inc_header.html", function(data) {
    $(".header").html(data);
});
$.get("inc_side_menu.html", function(data) {
    $(".sidebar-left").html(data, function(){
        
    });
    
    // var FooterDate = new Date();
    // var FooterYear = FooterDate.getFullYear();
    // document.getElementById("year").innerHTML = FooterYear;
    
});



// $(document).ready(function () {

// });
