var contrasenaValida = false;

$(document).ready(function () {
    $("#txt_contrasena").keyup(function () {
        ValidarContrasena();
    });

    $('#txt_confirmarContrasena').keyup(function () {
        ValidarContrasena();
    });
});

function ContrasenaValida(validaContraAnterior = false) {
    $("#contrasenaActual").next().html("");
    $("#txt_contrasena").next().text("");
    $("#txt_confirmarContrasena").next().text("");
    if (validaContraAnterior) {
        if ($("#contrasenaActual").val() == "") {
            $("#contrasenaActual").next().html("Ingresa la contraseña actual");
            contrasenaValida = false;
        }
    }
    if ($("#txt_contrasena").val() == "") {
        $("#txt_contrasena").next().text("El campo es requerido");
        contrasenaValida = false;
    } else if ($("#txt_confirmarContrasena").val() == "") {
        $("#txt_confirmarContrasena").next().text("El campo es requerido");
        contrasenaValida = false;
    }

    if (contrasenaValida)
        return true;
    else
        return false;
}
function ValidarContrasena() {

    var pswd = $("#txt_contrasena").val();
    var pswdConfirm = $('#txt_confirmarContrasena').val();
    contrasenaValida = true;
    //validate the length
    if (pswd.length < 6) {
        $('#Logitud').addClass('d-none');
        contrasenaValida = false;
    } else {
        $('#Logitud').removeClass('d-none');
    }

    //validate letter
    if (pswd.match(/[a-z]/)) {
        $('#Minusculas').removeClass('d-none');
    } else {
        $('#Minusculas').addClass('d-none');
        contrasenaValida = false;
    }

    //validate capital letter
    if (pswd.match(/[A-Z]/)) {
        $('#Mayusculas').removeClass('d-none');
    } else {
        $('#Mayusculas').addClass('d-none');
        contrasenaValida = false;
    }

    //validate CaracterEspecial
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(pswd)) {
        $('#Caract').removeClass('d-none');
    } else {
        $('#Caract').addClass('d-none');
        contrasenaValida = false;
    }

    if (pswd.match(/[0-9]/)) {
        $('#Numeros').removeClass('d-none');
    } else {
        $('#Numeros').addClass('d-none');
        contrasenaValida = false;
    }

    if ((pswd == pswdConfirm) && pswd != "" && pswdConfirm != "") {
        $('#ContrasNoCoinciden').removeClass('d-none');

    } else {
        $('#ContrasNoCoinciden').addClass('d-none');
        contrasenaValida = false;
    }

    if (contrasenaValida) {
        $(".div_CaracteristicasContraAlert").removeClass("alert-warning");
        $(".div_CaracteristicasContraAlert").addClass("alert-success");
    } else {
        $(".div_CaracteristicasContraAlert").addClass("alert-warning");
        $(".div_CaracteristicasContraAlert").removeClass("alert-success");
    }

    return contrasenaValida;
}

