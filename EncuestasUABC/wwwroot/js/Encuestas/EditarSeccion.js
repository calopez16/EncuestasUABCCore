﻿const dateFormat = 'DD/MM/YYYY HH:mm:ss';

var seccionId = 0;
var encuestaId = 0;
var tipoPreguntaId = 0;
var isEditaPregunta = false;

$(document).ready(function () {
    seccionId = parseInt($("#Id").val());
    encuestaId = parseInt($("#IdEncuesta").val());

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
        var encuestaId = parseInt($("#IdEncuesta").val());
        var nombre = $("#txt_SeccionNombre").val();
        cambiarSeccionNombre(id, encuestaId, nombre);
    });
    //#endregion

    $(".btn_PreguntaAgregar").click(function () {
        LimpiarCamposPreguntas();
        $("#div_tiposPregunta").show();
        ocultarFormulariosPreguntas();
        $("#btn_RegresarPreguntaAgregar").hide();
        $("#btn_GuardarPreguntaAgregar").hide();
        $("#modal_PreguntaCrear").modal("show");
        isEditaPregunta = false;
        $("#h5_ModalTituloCrearPregunta").html("Elige el tipo de pregunta");
        $("#btn_CancelarModalPregunta").hide();
        $("#IdPregunta").val(0);
    });

    //#region FUNCIONALIDAD TABLA PREGUNTAS

    $("#table_SeccionPregunta tbody").sortable({
        handle: '.ordenador',
        update: function (event, ui) {
            actualizarPosicionPreguntas();
        }
    });

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
    $("#table_opciones tbody").sortable({ handle: '.ordenador' });

    $(".btn_PreguntaAbierta").click(function () {
        $("#div_tiposPregunta").hide();
        $("#div_PreguntaDescripcion").fadeIn();
        $("#h5_ModalTituloCrearPregunta").text("Crear pregunta abierta");
        $("#btn_RegresarPreguntaAgregar").show();
        $("#btn_GuardarPreguntaAgregar").show();
        $("#txt_DescripcionPregunta").focus();
        tipoPreguntaId = enum_TipoPregunta.Abierta;
    });

    $(".btn_RespuestaUnica").click(function () {
        $("#div_tiposPregunta").hide();
        $("#div_PreguntaDescripcion").fadeIn();
        $("#div_PreguntaRadioCheck").fadeIn();
        $("#h5_ModalTituloCrearPregunta").html("Crear pregunta respuesta única");
        $("#btn_RegresarPreguntaAgregar").show();
        $("#btn_GuardarPreguntaAgregar").show();
        $("#txt_DescripcionPregunta").focus();
        tipoPreguntaId = enum_TipoPregunta.UnicaOpcion;
    });
    $(".btn_RespuestaMultiple").click(function () {
        $("#div_tiposPregunta").hide();
        $("#div_PreguntaDescripcion").fadeIn();
        $("#div_PreguntaRadioCheck").fadeIn();
        $("#h5_ModalTituloCrearPregunta").html("Crear pregunta respuesta múltiple");
        $("#btn_RegresarPreguntaAgregar").show();
        $("#btn_GuardarPreguntaAgregar").show();
        $("#txt_DescripcionPregunta").focus();
        tipoPreguntaId = enum_TipoPregunta.Multiple;
    });
    $(".btn_RespuestaSelectList").click(function () {
        $("#div_tiposPregunta").hide();
        $("#div_PreguntaDescripcion").fadeIn();
        $("#div_PreguntaRadioCheck").fadeIn();
        $("#h5_ModalTituloCrearPregunta").html("Crear pregunta Select List");
        $("#btn_RegresarPreguntaAgregar").show();
        $("#btn_GuardarPreguntaAgregar").show();
        $("#txt_DescripcionPregunta").focus();
        tipoPreguntaId = enum_TipoPregunta.SelectList;
    });
    $("#btn_AgregarOpcion").click(function () {
        var descripcionOpcion = $("#txt_DescripcionOpcion");
        $("#span_DescripcionMsg").text("");
        if (descripcionOpcion.val() == "") {
            $("#span_DescripcionMsg").text("El campo Descripción es requerido");
            return;
        }
        var item = `<tr class="tr_Opcion">
                        <td class="ordenador" style="cursor:pointer;width:40px">
                            <i class="material-icons text-secondary mt-2">
                                drag_indicator
                            </i>
                        </td>
                        <td>
                            <input type="text" class="txt_DescripcionOpcion form-control" placeholder="Descripción" value="${descripcionOpcion.val()}"/>
                            <input hidden class="txt_OpcionId" value="0"/>                                   
                        </td>
                        <td>
                            <span class="btn-group-sm">
                                <button type="button" class="btn btn-danger bmd-btn-fab btn_EliminarOpcion" data-toggle="tooltip" data-placement="bottom" title="Eliminar opción">
                                    <i class="material-icons">remove</i>
                                </button>
                            </span>
                        </td>
                    </tr>`;
        if (!$("#table_opciones").find(".tr_Opcion").length) {
            $("#table_opciones tbody").html(item);
        } else {
            $("#table_opciones tbody").append(item);
        }
        descripcionOpcion.val("");
        $('[data-toggle="tooltip"]').tooltip("hide");
        $('[data-toggle="tooltip"]').tooltip();
        $("#table_opciones tbody").sortable({ handle: '.ordenador' });
        descripcionOpcion.focus();
    });

    $("#table_opciones tbody").on("click", ".btn_EliminarOpcion", function () {
        $('[data-toggle="tooltip"]').tooltip("hide");
        $(this).closest(".tr_Opcion").remove();
        if ($("#table_opciones").find(".tr_Opcion").length < 1) {
            var item = `<tr>
                            <td colspan="3">
                                <h6 class="text-center">No se han agregado opciones</h6>
                            </td>
                        </tr>`;
            $("#table_opciones tbody").html(item);
        }
    });

    $("#btn_RegresarPreguntaAgregar").click(function () {
        $("#div_tiposPregunta").fadeIn();
        ocultarFormulariosPreguntas();
        $("#btn_RegresarPreguntaAgregar").hide();
        $("#btn_GuardarPreguntaAgregar").hide();
        LimpiarCamposPreguntas();
        tipoPreguntaId = 0;
        $("#h5_ModalTituloCrearPregunta").text("Elige el tipo de pregunta");
    });

    $("#btn_GuardarPreguntaAgregar").click(function () {
        if (!$("#form_Pregunta").valid())
            return;
        if (tipoPreguntaId == enum_TipoPregunta.Multiple || tipoPreguntaId == enum_TipoPregunta.UnicaOpcion || tipoPreguntaId == enum_TipoPregunta.SelectList) {
            if (!$("#table_opciones").find(".tr_Opcion").length) {
                GenerarAlerta(enum_MessageAlertType.Information, "Es necesario agregar por lo menos una opción")
                return;
            }
        } else if (tipoPreguntaId == enum_TipoPregunta.Condicional) {
            return;
        } else if (tipoPreguntaId == enum_TipoPregunta.Matriz) {
            return;
        }
        if (isEditaPregunta)
            editarPregunta();
        else
            guardarPregunta();
    });

    $("#table_SeccionPregunta tbody").on("click", ".btn_Editar", function () {
        var preguntaId = parseInt($(this).data("id"));
        cargarPregunta(preguntaId, seccionId, encuestaId);
        $("#btn_CancelarModalPregunta").show();
        $("#modal_PreguntaCrear").modal("show");
    });

    //#endregion


});

function LimpiarCamposPreguntas() {
    $("#txt_DescripcionPregunta").val("");
    $("#check_Obligatoria").prop("checked", false);
    var item = `<tr>
                    <td colspan="3">
                        <h6 class="text-center">No se han agregado opciones</h6>
                    </td>
                </tr>`;
    $("#table_opciones tbody").html(item);
    $("#txt_DescripcionOpcion").val("");
    $("#span_DescripcionMsg").text("");
}

function ocultarFormulariosPreguntas() {
    $("#div_PreguntaDescripcion").hide();
    $("#div_PreguntaRadioCheck").hide();
}

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
            showEstatusLoading();
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#div_EditarSeccionNombre").hide();
        $("#h1_SeccionNombre").show();
        $("#h1_SeccionNombre").find("span").first().text($("#txt_SeccionNombre").val());
        $('[data-toggle="tooltip"]').tooltip("hide");
        finishEstatusLoading("Sección actualizada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al actualizar la sección", false);
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function guardarPregunta() {
    var descripcion = $("#txt_DescripcionPregunta").val();
    var obligatoria = $("#check_Obligatoria").prop("checked");
    var data = new FormData();
    data.append("IdEncuesta", encuestaId);
    data.append("IdEncuestaSeccion", seccionId);
    data.append("Descripcion", descripcion);
    data.append("IdTipoPregunta", tipoPreguntaId);
    data.append("Obligatoria", obligatoria);
    $(".tr_Opcion").each(function (i, item) {
        var descripcionOpcion = $(".txt_DescripcionOpcion").eq(i).val();
        data.append(`Opciones[${i}].Descripcion`, descripcionOpcion);
        data.append(`Opciones[${i}].orden`, i + 1);
    });
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
            showEstatusLoading();
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#modal_PreguntaCrear").modal("hide");
        LimpiarCamposPreguntas();

        var tipoPregunta = getTipoPreguntaDescripcion(tipoPreguntaId);

        var fila = `<tr class="fila_Pregunta pregunta_${data}">
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
                            <td class="pregunta_Descripcion">
                                ${descripcion}
                            </td>
                            <td >${tipoPregunta}</td>
                            <td class="text-center pregunta_Obligatoria">
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
        finishEstatusLoading("Pregunta creada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al crear la pregunta", false);
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function editarPregunta() {
    var descripcion = $("#txt_DescripcionPregunta").val();
    var obligatoria = $("#check_Obligatoria").prop("checked");
    var preguntaId = $("#IdPregunta").val();
    var data = new FormData();
    data.append("Id", preguntaId);
    data.append("IdEncuesta", encuestaId);
    data.append("IdEncuestaSeccion", seccionId);
    data.append("Descripcion", descripcion);
    data.append("IdTipoPregunta", tipoPreguntaId);
    data.append("Obligatoria", obligatoria);
    $(".tr_Opcion").each(function (i, item) {
        var descripcionOpcion = $(".txt_DescripcionOpcion").eq(i).val();
        var opcionId = $(".txt_OpcionId").eq(i).val();
        data.append(`Opciones[${i}].Id`, opcionId);
        data.append(`Opciones[${i}].Descripcion`, descripcionOpcion);
        data.append(`Opciones[${i}].orden`, i + 1);
    });
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/EditarPregunta`,
        //Tipo de petición
        type: "POST",
        //Datos que se enviaran a la llamada
        data: data,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
            showEstatusLoading();
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        $("#modal_PreguntaCrear").modal("hide");
        LimpiarCamposPreguntas();
        $(`.pregunta_${data}`).first().find(".pregunta_Descripcion").first().text(descripcion);
        if (obligatoria)
            $(`.pregunta_${data}`).first().find(".pregunta_Obligatoria").first().html(`<i class="material-icons text-primary">done</i>`);
        else
            $(`.pregunta_${data}`).first().find(".pregunta_Obligatoria").first().html("");
        $('[data-toggle="tooltip"]').tooltip();
        finishEstatusLoading("Pregunta actualizada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al editar la pregunta", false);
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
            showEstatusLoading();
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
        finishEstatusLoading("Pregunta eliminada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al tratar de eliminar la pregunta.");

    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function actualizarPosicionPreguntas() {
    var data = new FormData();
    data.append("encuestaId", encuestaId);
    data.append("seccionId", seccionId);
    $(".txt_PreguntaId").each(function (i, item) {
        var preguntaId = $(this).val();
        data.append(`preguntaId[${i}]`, preguntaId);
    });

    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/ActualizarPosicionPreguntas`,
        //Tipo de petición
        type: "POST",
        processData: false,
        contentType: false,
        //Datos que se enviaran a la llamada
        data: data,
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
            showEstatusLoading();
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        finishEstatusLoading("Sección actualizada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al actualizar la sección");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function cargarPregunta(preguntaId, seccionId, encuestaId) {
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/GetPregunta`,
        //Tipo de petición
        type: "GET",
        //tipo de formato que regresará el servidor.
        dataType: "json",
        //Datos que se enviaran a la llamada
        data: { preguntaId, encuestaId, seccionId },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.

        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        ocultarFormulariosPreguntas();
        $("#IdEncuesta").val(encuestaId);
        $("#IdEncuestaSeccion").val(seccionId);
        $("#IdPregunta").val(preguntaId);
        $("#txt_DescripcionPregunta").val(data.Descripcion);
        $("#txt_DescripcionPregunta").focus();
        $("#check_Obligatoria").prop("checked", data.Obligatoria);
        isEditaPregunta = true;
        if (data.IdTipoPregunta == enum_TipoPregunta.Abierta) {
            $(".btn_PreguntaAbierta").click();
            $("#h5_ModalTituloCrearPregunta").text("Editar pregunta abierta");
        } else if (data.IdTipoPregunta == enum_TipoPregunta.UnicaOpcion) {
            $(".btn_RespuestaUnica").click();
            $("#h5_ModalTituloCrearPregunta").text("Editar pregunta respuesta única");
            cargarPreguntaFormularioOpciones(data);
        } else if (data.IdTipoPregunta == enum_TipoPregunta.Multiple) {
            $(".btn_RespuestaMultiple").click();
            $("#h5_ModalTituloCrearPregunta").text("Editar pregunta respuesta múltiple");
            cargarPreguntaFormularioOpciones(data);
        } else if (data.IdTipoPregunta == enum_TipoPregunta.SelectList) {
            $(".btn_RespuestaSelectList").click();
            $("#h5_ModalTituloCrearPregunta").text("Editar pregunta select list");
            cargarPreguntaFormularioOpciones(data);
        } else if (data.IdTipoPregunta == enum_TipoPregunta.Condicional) {

        } else if (data.IdTipoPregunta == enum_TipoPregunta.Matriz) {

        }
        $("#btn_RegresarPreguntaAgregar").hide();
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al tratar de eliminar la pregunta.");

    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}



function cargarPreguntaFormularioOpciones(pregunta) {
    var item = "";
    $.each(pregunta.Opciones, function (i, opcion) {
        item += `<tr class="tr_Opcion">
                        <td class="ordenador" style="cursor:pointer;width:40px">
                            <i class="material-icons text-secondary mt-2">
                                drag_indicator
                            </i>
                        </td>
                        <td>
                            <input type="text" class="txt_DescripcionOpcion form-control" placeholder="Descripción" value="${opcion.Descripcion}"/>
                            <input hidden class="txt_OpcionId" value="${opcion.Id}"/>                                   
                        </td>
                        <td>
                            <span class="btn-group-sm">
                                <button type="button" class="btn btn-danger bmd-btn-fab btn_EliminarOpcion" data-toggle="tooltip" data-placement="bottom" title="Eliminar opción">
                                    <i class="material-icons">remove</i>
                                </button>
                            </span>
                        </td>
                    </tr>`;
    });
   
    $("#table_opciones tbody").html(item);

}

