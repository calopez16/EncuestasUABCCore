
$(document).ready(() => {
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
    $(".opciones-item").on("click", function () {
        if ($(this).next().length) {
            $(this).next().slideToggle(200);
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
                $(this).find("i").last().html("keyboard_arrow_down");
            } else {
                $(this).addClass("active");
                $(this).find("i").last().html("keyboard_arrow_up");
            }
        }

    });
});