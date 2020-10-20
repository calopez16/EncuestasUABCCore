const dateFormat = 'DD/MM/YYYY HH:mm:ss';

var seccionId = 0;
var encuestaId = 0;
var tipoPreguntaId = 0;

$(document).ready(function () {
    seccionId = parseInt($("#Id").val());
    encuestaId = parseInt($("#EncuestaId").val());

    //#region EDITAR NOMBRE SECCION
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

    $(".btn_PreguntaAgregar").click(function () {
        LimpiarCamposPreguntas();
        $("#div_tiposPregunta").show();
        $("#div_PreguntaDescripcion").hide();
        $("#btn_RegresarPreguntaAgregar").hide();
        $("#btn_GuardarPreguntaAgregar").hide();
        $("#modal_PreguntaCrear").modal("show");
    });

    //#region FUNCIONALIDAD TABLA PREGUNTAS

    $("#table_SeccionPregunta tbody").sortable({ handle: '.ordenador' });

    $(".check_seleccionarTodos").click(function () {
        if ($(this).prop("checked")) {
            $(".check_Pregunta").prop("checked", true);
        } else {
            $(".check_Pregunta").prop("checked", false);
        }
    });

    //Eliminar Pregunta
    var encuestaPreguntaId;
    var botonEliminar;
    $("#table_SeccionPregunta tbody").on("click", ".btn_Eliminar", function () {
        encuestaPreguntaId = parseInt($(this).data("id"));
        var descripcion = $(this).data("descripcion");
        $("#span_PreguntaNombre").text(descripcion);
        $("#modal_PreguntaEliminar").modal("show");
        botonEliminar = this;
    });

    $("#btn_ConfirmarEliminarPregunta").click(function () {
        eliminarPregunta(encuestaPreguntaId, encuestaId, seccionId, botonEliminar);
    });

    //#endregion

    //#region MODAL AGREGAR PREGUNTAS

    $(".btn_PreguntaAbierta").click(function () {
        $("#div_tiposPregunta").hide();
        $("#div_PreguntaDescripcion").show();
        $("#h5_ModalTituloCrearPregunta").text("Crear pregunta abierta");
        $("#btn_RegresarPreguntaAgregar").show();
        $("#btn_GuardarPreguntaAgregar").show();
        $("#txt_DescripcionPregunta").focus();
        tipoPreguntaId = enum_TipoPregunta.Abierta;
    });
    $("#btn_RegresarPreguntaAgregar").click(function () {
        $("#div_tiposPregunta").show();
        $("#div_PreguntaDescripcion").hide();
        $("#btn_RegresarPreguntaAgregar").hide();
        $("#btn_GuardarPreguntaAgregar").hide();
        LimpiarCamposPreguntas();
        tipoPreguntaId = 0;
        $("#h5_ModalTituloCrearPregunta").text("Elige el tipo de pregunta");
    });


    $("#btn_GuardarPreguntaAgregar").click(function () {
        var isFormValid = $("#form_Pregunta").valid();
        if (isFormValid) {
            guardarPregunta();
        }
    });

    //#endregion
});

function LimpiarCamposPreguntas() {
    $("#txt_DescripcionPregunta").val("");
    $("#check_Obligatoria").prop("checked", false);
}
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

function guardarPregunta() {
    var descripcion = $("#txt_DescripcionPregunta").val();
    var obligatoria = $("#check_Obligatoria").prop("checked");
    var data = new FormData();
    data.append("EncuestaId", encuestaId);
    data.append("EncuestaSeccionId", seccionId);
    data.append("Descripcion", descripcion);
    data.append("TipoPreguntaId", tipoPreguntaId);
    data.append("Obligatoria", obligatoria);

    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/CrearPregunta`,
        //Tipo de petición
        type: "POST",
        //Datos que se enviaran a la llamada
        data: data,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#modal_PreguntaCrear").modal("hide");
        LimpiarCamposPreguntas();

        var tipoPregunta = getTipoPreguntaDescripcion(tipoPreguntaId);

        var fila = `<tr class="fila_Pregunta">
                            <td class="ordenador" style="cursor:pointer;width:40px">
                                <i class="material-icons text-secondary">
                                    drag_indicator
                                </i>
                            </td>
                            <td style="width:40px">
                               <span class="bmd-form-group is-filled"><div class="checkbox" style="margin-top:-5px">
                                    <label>
                                        <input type="checkbox" class="check_Pregunta"><span class="checkbox-decorator"><span class="check"></span><div class="ripple-container"></div></span>
                                    </label>
                                </div></span>
                            </td>
                            <td>
                                ${descripcion}
                            </td>
                            <td>${tipoPregunta}</td>
                            <td class="text-center">
                                ${obligatoria ? '<i class="material-icons text-primary">done</i>' : ""}
                            </td>
                        <td style="width:40px">
                            <span class="btn-group-sm">
                                <button class="btn btn-info bmd-btn-fab btn_Editar" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Editar pregunta">
                                    <i class="material-icons">edit</i>
                                </button>
                            </span>
                        </td>
                        <td style="width:40px">
                            <span class="btn-group-sm">
                                <button class="btn btn-danger bmd-btn-fab btn_Eliminar" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Eliminar pregunta">
                                    <i class="material-icons">delete</i>
                                </button>
                            </span>
                        </td>
                    </tr>`;
        if ($(".fila_SinPregunta").length) {
            $("#table_SeccionPregunta tbody").html(fila);
        } else {
            $("#table_SeccionPregunta tbody").append(fila);
        }
        $('[data-toggle="tooltip"]').tooltip();

    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al crear la pregunta.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function getTipoPreguntaDescripcion(tipoPreguntaId) {
    if (tipoPreguntaId == enum_TipoPregunta.Abierta) {
        return "Abierta";
    } else if (tipoPreguntaId == enum_TipoPregunta.Multiple) {
        return "Múltiple";
    } else if (tipoPreguntaId == enum_TipoPregunta.UnicaOpcion) {
        return "Única Opción";
    } else if (tipoPreguntaId == enum_TipoPregunta.Condicional) {
        return "Condicional";
    } else if (tipoPreguntaId == enum_TipoPregunta.Matriz) {
        return "Matriz";
    } else if (tipoPreguntaId == enum_TipoPregunta.SelectList) {
        return "Select List";
    }
}

//#endregion

//#region PUT

function eliminarPregunta(id, encuestaId, seccionId, boton) {
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/DeletePregunta`,
        //Tipo de petición
        type: "PUT",
        //Datos que se enviaran a la llamada
        data: { id, encuestaId, seccionId },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $(boton).closest("tr").remove();
        $("#modal_PreguntaEliminar").modal("hide");
        if ($("#table_SeccionPregunta tbody").find(".fila_Pregunta").length == 0) {
            var item = ` <tr class="fila_SinPregunta">
                        <td colspan="7" class="text-center">
                            <h6>No se han encontrado Preguntas</h6>
                        </td>
                    </tr>`;
            $("#table_SeccionPregunta tbody").html(item);
        }

    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al tratar de eliminar la pregunta.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}


//#endregion