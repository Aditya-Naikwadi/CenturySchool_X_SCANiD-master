$(document).ready(function() {

    // $(".animated-text").typed({
    //     strings: [
    //         "Law and Justice",
    //         "Legal Advice",
    //         "ABCD",
    //         "PQRS"
    //     ],
    //     typeSpeed: 40,
    //     loop: true,
    // });
    $('#sidebarCollapse').on('click', function() {
        $('#sidebar').toggleClass('active');
    });

    $('.sidebar-nav-fixed a').each(function() {
        $(this).removeClass('active');
    })
    $(this).addClass('active');
});