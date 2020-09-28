// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const _dateFormat = 'DD/MM/YYYY HH:mm:ss';

$(document).ready(function () {
    $('.mask-Date').mask('99/99/9999', {
        placeholder: 'dd/mm/yyyy'
    });
    $('.mask-phone').mask('(999) 999-9999');
    $('.mask-phone2').mask('+186 999 999-9999');
    $('.mask-periodo').mask('9999-9');
    $('.mask-ext').mask('(999) 999-9999? x9999');
    $('.mask-credit').mask('****-****-****-****', {
        placeholder: '*'
    });
    $('.mask-tax').mask('99-9999999');
    $('.mask-currency').mask('$ 99.99');
    $('.mask-product').mask('a*-999-a999', {
        placeholder: 'a*-999-a999'
    });

    $.mask.definitions['~'] = '[+-]';
    $('.mask-eye').mask('~9.99 ~9.99 999');

    $(".select2").select2({ language: "es" });

    $('select:not(.normal)').each(function () {
        $(this).select2({
            dropdownParent: $(this).parent()
        });
    });

});

