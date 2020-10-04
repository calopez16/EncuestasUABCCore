$(document).ready(function () {

    $("#btn_Guardar").click(function () {
        var isFormValid = true;
        isFormValid = $("#form_Usuario").valid();
        $("#Email").next().text("");
        if ($("#select_Rol").val() != enum_Roles.Egresado) {
            isFormValid = validarCorreoUABC($("#Email").val());
            if (!isFormValid) {
                $("#Email").next().text("El correo debe ser de uabc (@uabc.edu.mx)");
                $("#Email").next().addClass("field-validation-error");
            }
        }     

        if (isFormValid) {
            $("#form_Usuario").submit();
        }
    });
   

    $("#select_Rol").change(function () {
        $("#div_Maestro").hide();
        $("#div_Alumno").hide();
        $("#div_Egresado").hide();
        var rolId = $(this).val();
        if (rolId == enum_Roles.Maestro || rolId == enum_Roles.Coordinador || rolId == enum_Roles.Tutor) {
            $("#div_Maestro").show();
        } else if (rolId == enum_Roles.Alumno) {
            $("#div_Alumno").show();

        } else if (rolId == enum_Roles.Egresado) {
            $("#div_Egresado").show();
        }
    });
    $("#select_Rol").change();
    $("#btn_CambiarContrasena").click(function () {
        var email = $(this).data("email");
        $("#txt_EmailContrasena").val(email);
        $("#modal_CambiarContrasena").modal("show");
    });
});


function validarCorreoUABC(correo) {
    var re = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;
    if (re.test(correo)) {
        if (correo.indexOf("@uabc.edu.mx", correo.length - "@uabc.edu.mx".length) !== -1) {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}
