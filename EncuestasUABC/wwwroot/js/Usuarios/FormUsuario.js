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

            "UsuarioAlumno.Alumno.Matricula": {
                number: true
            },
            "UsuarioAlumno.Alumno.Semestre": {
                range: [0, 10],
                number: true
            },
            "UsuarioAlumno.Alumno.CorreoAlterno": {
                email: true,
            },
            "UsuarioMaestro.Maestro.Correo": {
                email: true,
            },
            "UsuarioMaestro.Maestro.CorreoAlterno": {
                email: true,
            },
            "UsuarioEgresado.Egresado.Correo": {
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
            "UsuarioAlumno.Alumno.Semestre": {
                range: "El rango permitido es de 1 a 99"
            },
            "UsuarioAlumno.Alumno.CorreoAlterno": {
                email: "Ingresa un Email valido",
            },
            "UsuarioAlumno.Alumno.CorreoAlterno": {
                email: "Ingresa un Email valido",
            },
            "UsuarioMaestro.Maestro.Correo": {
                email: "Ingresa un Email valido",
            },
            "UsuarioMaestro.Maestro.CorreoAlterno": {
                email: "Ingresa un Email valido",
            },
            "UsuarioEgresado.Egresado.Correo": {
                email: "Ingresa un Email valido",
            }
        }
    });

    var campusId = $("#txt_CampusId").val();
    var unidadAcademicaId = parseInt($("#txt_UnidadAcademicaId").val());
    var carreraId = parseInt($("#txt_CarreraId").val());
    CargarUnidadesAcademicas(campusId, unidadAcademicaId);
    CargarCarreras(unidadAcademicaId, carreraId);
    $("#select_Campus").change(function () {
        var campusId = $(this).val();
        $("#select_UnidadAcademica").empty();
        $("#select_Carrera").empty();
        CargarUnidadesAcademicas(campusId);
        CargarCarreras(0);
    });

    $("#select_UnidadAcademica").change(function () {
        var unidadAcademicaId = $(this).val();
        $("#select_Carrera").empty();
        CargarCarreras(unidadAcademicaId);
    });

    $("#btn_CambiarContrasena").click(function () {
        var email = $(this).data("email");
        $("#txt_EmailContrasena").val(email);
        $("#modal_CambiarContrasena").modal("show");
    })
});

function CargarUnidadesAcademicas(campusId, unidadAcademicaId) {
    $.ajax({
        //Url de la peticion
        url: "../Usuarios/UnidadesAcademicas",
        //Tipo de petición
        type: "POST",
        //Datos que se enviaran a la llamada
        data: { campusId: campusId },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        data.unshift({ id: "", text: "Seleccionar" });
        $("#select_UnidadAcademica").select2({
            data: data,
        });
        if (unidadAcademicaId != undefined) {
            $("#select_UnidadAcademica").val(unidadAcademicaId);
            $("#select_UnidadAcademica").trigger('change');
        }

    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function CargarCarreras(unidadAcademicaId, carreraId) {
    $.ajax({
        //Url de la peticion
        url: "../Usuarios/Carreras",
        //Tipo de petición
        type: "POST",
        //Datos que se enviaran a la llamada
        data: { unidadAcademicaId: unidadAcademicaId },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        data.unshift({ id:"", text: "Seleccionar" });
        $("#select_Carrera").select2({ data: data });
        if (carreraId != undefined) {
            $("#select_Carrera").val(carreraId);
            $("#select_Carrera").trigger('change');
        }
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}
