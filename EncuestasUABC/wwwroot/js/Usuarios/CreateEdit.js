
$(document).ready(function () {

    $.validator.addMethod("correoUABC", function (value, element) {
        var re = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;
        if (re.test(value)) {
            if (value.indexOf("@uabc.edu.mx", value.length - "@uabc.edu.mx".length) !== -1) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }, "El correo debe tener el dominio de UABC(@uabc.edu.mx)");

    $("#select_Rol").change(function () {
        $("#div_Maestro").hide();
        $("#div_Alumno").hide();
        $("#div_Egresado").hide();
        var rol = $(this).val();
        if (rol == "MAESTRO" || rol == "COORDINADOR" || rol == "TUTOR") {
            $("#div_Maestro").show();
        } else if (rol == "ALUMNO") {
            $("#div_Alumno").show();

        } else if (rol == "EGRESADO") {
            $("#div_Egresado").show();
        }
    });
    $("#select_Rol").change();

    $("#form_Usuario").validate({
        rules: {
            Nombre: "required",
            ApellidoPaterno: "required",
            ApellidoMaterno: "required",
            UserName: "required",
            Email: {
                required: true,
                email: true,
                correoUABC: true
            },
            Rol: "required",

            "Alumno.Matricula": {
                number: true
            },
            "Alumno.Semestre": {
                range: [1, 10],
                number: true
            },
            "Alumno.CorreoAlterno": {
                email: true,
            },
            "Maestro.Correo": {
                email: true,
            },
            "Maestro.CorreoAlterno": {
                email: true,
            },
            "Egresado.Correo": {
                email: true,
            }

        },
        messages: {
            Nombre: "El campo Nombre es obligatorio",
            ApellidoPaterno: "El campo Apellido Paterno es obligatorio",
            ApellidoMaterno: "El campo Apellido Materno es obligatorio",
            Email: {
                required: "El correo el obligatorio",
                email: "Ingresa un Email valido",
                matches: "Ingresa un email de UABC @uabc.edu.mx",
            },
            Rol: "El campo de Rol es obligatorio",
            "Alumno.Semestre": {
                range: "El rango permitido es de 1 a 99"
            },
            "Alumno.CorreoAlterno": {
                email: "Ingresa un Email valido",
            },
            "Alumno.CorreoAlterno": {
                email: "Ingresa un Email valido",
            },
            "Maestro.Correo": {
                email: "Ingresa un Email valido",
            },
            "Maestro.CorreoAlterno": {
                email: "Ingresa un Email valido",
            },
            "Egresado.Correo": {
                email: "Ingresa un Email valido",
            }
        }
    });

    $('.date-Month-Year').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
        alert("You are " + years + " years old!");
    });

});

