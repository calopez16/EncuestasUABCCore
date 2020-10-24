// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(() => {
    $('[data-toggle="tooltip"]').tooltip();

    $('select').on('select2:open', function (e) {
        $('.select2-dropdown').hide();
        setTimeout(function () {
            $('.select2-dropdown').slideDown(50, "easeInOutQuint");
            $('input.select2-search__field').focus();
        }, 50);
    });

   //#region SELECT DE CARRERAS

    //#endregion
});

$(window).on("load", function () {
    $(".loader-container").fadeOut();
});

