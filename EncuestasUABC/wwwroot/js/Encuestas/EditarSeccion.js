const dateFormat = 'DD/MM/YYYY HH:mm:ss';

var encuestaId = 0;

$(document).ready(function () {

    encuestaId = parseInt($("#Id").val());
    $("#btn_EditarNombre").on("click", function () {
        $("#modal_EditarNombreDescripcion").modal("show");
    });

    //CargarEncuesta();

    //#region EDITAR NOMBRE ENCUESTA
    $("#h1_SeccionNombre").click(function () {
        $(this).hide();
        $("#div_EditarSeccionNombre").show();
        $("#txt_SeccionNombre").focus();
        $("#txt_SeccionNombre").select();
    });

    $("#h1_SeccionNombre").mouseover(function () {
        $(this).find("i").first().show();

    });
    $("#h1_SeccionNombre").mouseout(function () {
        $(this).find("i").first().hide();
    });

    $("#btn_CancelarEditarSeccionNombre").click(function () {
        $("#div_EditarSeccionNombre").hide();
        $("#h1_SeccionNombre").show();
        $("#txt_SeccionNombre").val($("#h1_SeccionNombre").find("span").first().text());
    });
    $("#btn_GuardarSeccionNombre").click(function () {
        var id = parseInt($("#Id").val());
        var encuestaId = parseInt($("#EncuestaId").val());
        var nombre = $("#txt_SeccionNombre").val();
        cambiarSeccionNombre(id, encuestaId, nombre);
    });
    //#endregion

    //#region ELIMINAR SECCION
    var encuestaSeccionId;
    var botonEliminar;
    $("#table_SeccionPreguntas").on("click", ".btn_Eliminar", function () {
        encuestaSeccionId = parseInt($(this).data("id"));
        $("#modal_SeccionEliminar").modal("show");
        botonEliminar = this;
    });

    $("#btn_ConfirmarEliminarSeccion").click(function () {
        encuestaId = parseInt($("#Id").val());
        eliminarSeccion(encuestaSeccionId, encuestaId, botonEliminar);
    });
    //#endregion

    //#region CREAR SECCION
    $(".btn_AgregarSeccion").click(function () {
        $("#txt_CrearNombreSeccion").val("");
        $("#modal_SeccionCrear").modal("show");
        $("#txt_CrearNombreSeccion").next().text("");
    });

    $("#btn_CrearNombreSeccion").click(function () {
        var isFormValid = $("#form_CrearSeccion").valid();
        encuestaId = parseInt($("#Id").val());
        var nombre = $("#txt_CrearNombreSeccion").val();
        $("#txt_CrearNombreSeccion").next().text("");
        if (isFormValid) {
            crearSeccion(encuestaId, nombre);
        }
    });
    //#endregion


    //#region FUNCIONALIDAD TABLA PREGUNTAS
    $("#table_SeccionPreguntas tbody").sortable({ handle: '.ordenador' });

    $(".check_seleccionarTodos").click(function () {
        if ($(this).prop("checked")) {
            $(".check_Pregunta").prop("checked", true);
        } else {
            $(".check_Pregunta").prop("checked", false);
        }
    });

    //#endregion
});


//#region POST

function cambiarSeccionNombre(id, encuestaId, nombre) {
    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/CambiarSeccionNombre`,
        //Tipo de petición
        type: "POST",
        //Datos que se enviaran a la llamada
        data: { id, encuestaId, nombre },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#div_EditarSeccionNombre").hide();
        $("#h1_SeccionNombre").show();
        $("#h1_SeccionNombre").find("span").first().text($("#txt_SeccionNombre").val());
        $('[data-toggle="tooltip"]').tooltip("hide");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al guarda el Nombre de la encuesta.");
        cargarEncuesta();
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function eliminarSeccion(id, encuestaId, boton) {
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
        $("#modal_SeccionEliminar").modal("hide");
        if ($("#table_SeccionPreguntas tbody").find(".fila_Seccion").length == 0) {
            var item = `<tr class="fila_SinSecciones">
                        <td colspan="4" class="text-center">
                            <h4>No se han encontrado secciones</h4>
                        </td>
                    </tr>`;
            $("#table_SeccionPreguntas tbody").html(item);
        }

    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al tratar de eliminar la Sección.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function crearSeccion(encuestaId, nombre) {
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
                        <td class="ordenador" style="cursor:pointer;width:10px">
                            <i class="material-icons text-secondary">
                                drag_indicator
                            </i>
                        </td>
                        <td style="width:40px">
                            <span class="bmd-form-group is-filled"><div class="checkbox">
                            <label>
                                <input type="checkbox" class="check_Pregunta"><span class="checkbox-decorator"><span class="check"></span><div class="ripple-container"></div></span>
                            </label>
                        </div></span>

                        </td>
                        <td>
                            ${nombre}
                        </td>
                        <td style="width:40px">
                            <span class="btn-group-sm">
                                <button class="btn btn-info bmd-btn-fab btn_Editar" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Editar sección">
                                    <i class="material-icons">edit</i>
                                </button>
                            </span>
                        </td>
                        <td style="width:40px">
                            <span class="btn-group-sm">
                                <button class="btn btn-danger bmd-btn-fab btn_Eliminar" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Eliminar sección">
                                    <i class="material-icons">delete</i>
                                </button>
                            </span>
                        </td>
                    </tr>`;
        if ($("#table_SeccionPreguntas tbody").find(".fila_SinSecciones").length) {
            $("#table_SeccionPreguntas tbody").html(fila);
        } else {
            $("#table_SeccionPreguntas tbody").append(fila);
        }
        $("#modal_SeccionCrear").modal("hide");
        $('[data-toggle="tooltip"]').tooltip();

    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al tratar de eliminar la Sección.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}
//#endregion