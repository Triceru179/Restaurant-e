$(document).ready(function ()
{
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#dismiss, .overlay').on('click', function () {
        $('#sidebar').removeClass('active')
        $('.overlay').removeClass('active')
        $('.navbar, .container').removeClass('blur')
    });

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').addClass('active')
        $('.overlay').addClass('active')
        $('.navbar, .container').addClass('blur')
        $('.collapse.in').toggleClass('in')
        $('a[aria-expanded=true]').attr('aria-expanded', 'false')
    });
});