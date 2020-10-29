$(document).ready(function () {
  $(".btn_CambiarPanel").click(function () {
        if ($("#check_MenuDisplay").prop("checked")) {
            $("#check_MenuDisplay").prop("checked",false);
            $("#div_Panel1").animate({ opacity: 0 }, 200);
            $("#div_Panel2").animate({ opacity: 1 }, 200);
            $(".white-login").removeClass("white-login-right");
            $("#form_login").show();
            $("#form_comentarios").hide();
        } else {
            $("#check_MenuDisplay").prop("checked", true);
            $("#div_Panel2").animate({ opacity: 0 }, 200);
            $("#div_Panel1").animate({ opacity: 1 }, 200);
            $(".white-login").addClass("white-login-right");
            $("#form_login").hide();
            $("#form_comentarios").show();
        }       
    });
});

