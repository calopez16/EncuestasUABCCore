const dateFormat = 'DD/MM/YYYY HH:mm:ss';

$(document).ready(function () {
    $("#btn_EditarNombre").on("click", function () {
        $("#modal_EditarNombreDescripcion").modal("show");
    });

    CargarEncuesta();

    //#region EDITAR NOMBRE ENCUESTA
    $("#h1_NombreEncuesta").click(function () {
        $(this).hide();
        $("#div_EditarNombreEncuesta").show();
    });

    $("#h1_NombreEncuesta").mouseover(function () {
        $(this).find("i").first().show();

    });
    $("#h1_NombreEncuesta").mouseout(function () {
        $(this).find("i").first().hide();
    });

    $("#btn_CancelarEditarNombreEncuesta").click(function () {
        $("#div_EditarNombreEncuesta").hide();
        $("#h1_NombreEncuesta").show();
        $("#txt_NombreEncuesta").val($("#h1_NombreEncuesta").find("span").first().text());
    });
    $("#btn_GuardarNombreEncuesta").click(function () {
        CambiarNombreEncuesta();
    });
    //#endregion

    MostrarMensajeEstado(enum_MessageAlertType.Success, "Mensajeeeeeeeeee");
});


function CargarEncuesta() {
    var data = `&id=${parseInt($("#Id").val())}`;
    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/GetEncuesta`,
        //Tipo de petición
        type: "GET",
        //Datos que se enviaran a la llamada
        data: data,
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
            $(".loader-container").fadeIn();
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#h1_NombreEncuesta").find("span").first().text(data.nombre);
        $("#txt_NombreEncuesta").val(data.nombre);
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "No se pudo cargar la encuesta.");

    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
        $(".loader-container").fadeOut();
    });
}


//#region POST

function CambiarNombreEncuesta() {
    var data = `&id=${parseInt($("#Id").val())}&nombre=${$("#txt_NombreEncuesta").val()}`;
    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/CambiarNombre`,
        //Tipo de petición
        type: "GET",
        //Datos que se enviaran a la llamada
        data: data,
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#div_EditarNombreEncuesta").hide();
        $("#h1_NombreEncuesta").show();
        $("#h1_NombreEncuesta").find("span").first().text($("#txt_NombreEncuesta").val());
        $('[data-toggle="tooltip"]').tooltip("hide");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al guarda el Nombre de la encuesta.");
        CargarEncuesta();
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}


//#endregion 