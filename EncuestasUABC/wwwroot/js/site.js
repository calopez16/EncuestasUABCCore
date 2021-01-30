
var estatusInProgress = 0;
function showEstatusLoading() {
    $("#estatusAlertLoading").slideDown();
    estatusInProgress++;
}

function finishEstatusLoading(message, success = true) {

    if (estatusInProgress != 0) estatusInProgress--;
    if (estatusInProgress <= 0) {
        $("#estatusAlertLoading").hide();
    }
    var timestamp = new Date().getUTCMilliseconds();
    if (success) {
        icon = "check";
    } else {
        icon = "clear";
    }
    var item = `<li class="p-2" id="estatusAlertMessage_${timestamp}">
                    <i class="material-icons float-left mr-2">${icon}</i>
                    ${message}
                </li>`;
    $("#estatusAlertList").append(item);

    setTimeout(function () {
        $(`#estatusAlertMessage_${timestamp}`).slideUp();
    }, 1000);
}


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
    $('.editor').summernote({
        lang: 'es-ES',
        height:300,
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'underline','clear']],
            ['fontname', ['fontname']],
            ['color', ['']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link']],
            ['view', ['fullscreen', 'help']],
        ],
    });

    
});

$(window).on("load", function () {
    $(".loader-container").fadeOut();
});

$(document).on("click", ".control",function () {
    var buttonText = $(this).children("i").html();
    if (buttonText == "expand_more") {
        $(this).children("i").html("expand_less");
    } else {
        $(this).children("i").html("expand_more");
    }
});

