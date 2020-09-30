let _menu_Estado = false;
$(document).ready(function () {
    //#region MenuPrincipal
    $(".btn_Menu").on("click", function () {
        if (_menu_Estado) {
            OcultarMenu();
        } else {
            MostrarMenu();
        }
    });

    $(".fondo-negro").on("click", function (e) {
        var container = $(".menu-opciones");
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            OcultarMenu();
        }
    });

    $(window).resize(function () {
        AdaptarMenu();
    });
    var menuEstado = Cookies.get('menu_Estado');
    if (menuEstado != undefined) {
        if (menuEstado == 'true') {
            MostrarMenuSinAnimacion();
        } else {
            OcultarMenu();
        }
    }
    //#endregion

    //#region Opciones Menu
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


    //#endregion
});

function MostrarMenu() {
    var widthWindow = $(window).width();
    $(".menu-principal").show();
    $(".menu-principal").animate({ left: '0px' });
    $(".btn_Menu i").html("menu_open");
    if (widthWindow < 1024) {
        $("body").animate({ marginLeft: '0px' });
        $(".fondo-negro").show();
        $(".btn_MenuCerrar").css("visibility", "visible");
    } else {
        var widthMenuPrincipalNav = $(".menu-principal-nav").width() - 300;
        $(".menu-principal-nav").animate({ width: `${widthMenuPrincipalNav}px` });
        $("body").animate({ marginLeft: '350px' });
        $(".btn_MenuCerrar").css("visibility", "hidden");
        $(".menu-principal").css("width", "initial");

    }
    _menu_Estado = true;
    Cookies.set('menu_Estado', _menu_Estado);
}

function MostrarMenuSinAnimacion() {
    var widthWindow = $(window).width();
    $(".menu-principal").show();
    $(".menu-principal").animate({ left: '0px' }, 0);
    $(".btn_Menu i").html("menu_open");
    if (widthWindow < 1024) {
        $(".menu-principal-nav").animate({ width: `100%` }, 0);
        $("body").animate({ marginLeft: '0px' }, 0);
        $(".fondo-negro").show();
        $(".btn_MenuCerrar").css("visibility", "visible");
    } else {
        var widthMenuPrincipalNav = $(".menu-principal-nav").width() - 300;
        $(".menu-principal-nav").animate({ width: `${widthMenuPrincipalNav}px` }, 0);
        $("body").animate({ marginLeft: '350px' }, 0);
        $(".btn_MenuCerrar").css("visibility", "hidden");
        $(".menu-principal").css("width", "initial");

    }
    _menu_Estado = true;
    Cookies.set('menu_Estado', _menu_Estado);
}

function OcultarMenu() {
    var widthWindow = $(window).width();
    if (widthWindow >= 1024) {
        $("body").animate({ marginLeft: '0px' });
    }
    $(".btn_Menu i").html("menu");
    $(".menu-principal-nav").animate({ width: `100%` });
    $(".menu-principal").animate({ left: '-350px' }, function () {
        $(".menu-principal").hide();
        if (widthWindow < 1024) {
            $(".fondo-negro").hide();
        }
    });
    _menu_Estado = false;
    Cookies.set("menu_Estado", _menu_Estado);
}

function AdaptarMenu() {
    var widthWindow = $(window).width();
    if (widthWindow >= 1024) {
        if (_menu_Estado) {
            var widthMenuPrincipalNav = $(".menu-principal-nav").width() - 300;
            $(".menu-principal-nav").animate({ width: `${widthMenuPrincipalNav}px` });
            $("body").animate({ marginLeft: '350px' }, 0);
            $(".fondo-negro").hide();
            $(".btn_MenuCerrar").css("visibility", "hidden");
            $(".btn_MenuAbrir").css("visibility", "visible");
        } else {
            $(".menu-principal-nav").animate({ width: `100%` }, 0);
        }
    } else {
        if (_menu_Estado) {
            $(".menu-principal-nav").animate({ width: `100%` }, 0);
            $("body").animate({ marginLeft: '0px' }, 0);
            $(".fondo-negro").show();
            $(".btn_MenuCerrar").css("visibility", "visible");
        }
    }
}


