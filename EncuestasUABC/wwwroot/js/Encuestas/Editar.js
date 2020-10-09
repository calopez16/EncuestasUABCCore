const dateFormat = 'DD/MM/YYYY HH:mm:ss';

var idEncuesta = 0;

$(document).ready(function () {

    idEncuesta = parseInt($("#Id").val());
    $("#btn_EditarNombre").on("click", function () {
        $("#modal_EditarNombreDescripcion").modal("show");
    });

    //CargarEncuesta();

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

    //#region ELIMINAR SECCION
    var encuestaSeccionId;
    var botonEliminar;
    $("#table_EncuestasSecciones").on("click", ".btn_Eliminar", function () {
        encuestaSeccionId = parseInt($(this).data("id"));
        $("#modal_EliminarSeccion").modal("show");
        botonEliminar = this;
    });

    $("#btn_ConfirmarEliminarSeccion").click(function () {
        idEncuesta = parseInt($("#Id").val());
        EliminarSeccion(encuestaSeccionId, idEncuesta, botonEliminar);
    });
    //#endregion

    //#region CREAR SECCION
    $(".btn_AgregarSeccion").click(function () {
        $("#txt_CrearNombreSeccion").val("");
        $("#modal_CrearSeccion").modal("show");
        $("#txt_CrearNombreSeccion").next().text("");
    });

    $("#btn_CrearNombreSeccion").click(function () {
        var isFormValid = $("#form_CrearSeccion").valid();
        idEncuesta = parseInt($("#Id").val());
        var nombre = $("#txt_CrearNombreSeccion").val();
        $("#txt_CrearNombreSeccion").next().text("");
        if (isFormValid) {
            CrearSeccion(idEncuesta, nombre);
        }      
    });
    //#endregion
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

function EliminarSeccion(id, encuestaId, boton) {
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/DeleteSeccion`,
        //Tipo de petición
        type: "PUT",
        //Datos que se enviaran a la llamada
        data: { id, encuestaId },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $(boton).closest("tr").remove();
        $("#modal_EliminarSeccion").modal("hide");
        if ($("#table_EncuestasSecciones tbody").find(".fila_Seccion").length == 0) {
            var item = `<tr class="fila_SinSecciones">
                        <td colspan="4" class="text-center">
                            <h4>No se han encontrado secciones</h4>
                        </td>
                    </tr>`;
            $("#table_EncuestasSecciones tbody").html(item);
        }
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al tratar de eliminar la Sección.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function CrearSeccion(encuestaId, nombre) {
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/CrearSeccion`,
        //Tipo de petición
        type: "POST",
        //Datos que se enviaran a la llamada
        data: { encuestaId, nombre },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        var fila = ` <tr>
                        <td>
                            <i class="material-icons">
                                height
                            </i>
                        </td>
                        <td>
                            ${nombre}
                        </td>
                        <td>
                            <span class="btn-group-sm">
                                <button class="btn btn-info bmd-btn-fab btn_Editar" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Editar sección">
                                    <i class="material-icons">edit</i>
                                </button>
                            </span>
                        </td>
                        <td>
                            <span class="btn-group-sm">
                                <button class="btn btn-danger bmd-btn-fab btn_Eliminar" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Eliminar sección">
                                    <i class="material-icons">delete</i>
                                </button>
                            </span>
                        </td>
                    </tr>`;
        if ($("#table_EncuestasSecciones tbody").find(".fila_SinSecciones").length) {
            $("#table_EncuestasSecciones tbody").html(fila);
        } else {
            $("#table_EncuestasSecciones tbody").append(fila);
        }
        $("#modal_CrearSeccion").modal("hide");

    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al tratar de eliminar la Sección.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}
//#endregion