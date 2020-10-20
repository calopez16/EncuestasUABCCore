const dateFormat = 'DD/MM/YYYY HH:mm:ss';

var encuestaId = 0;

$(document).ready(function () {

    encuestaId = parseInt($("#Id").val());
    $("#btn_EditarNombre").on("click", function () {
        $("#modal_EditarNombreDescripcion").modal("show");
    });

    //#region EDITAR NOMBRE ENCUESTA
    $("#h1_NombreEncuesta").click(function () {
        $(this).hide();
        $("#div_EditarNombreEncuesta").show();
        $("#txt_NombreEncuesta").focus();
        $("#txt_NombreEncuesta").select();
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
        cambiarNombreEncuesta();
    });
    //#endregion

    //#region ELIMINAR SECCION
    var encuestaSeccionId;
    var botonEliminar;
    $("#table_EncuestasSecciones").on("click", ".btn_Eliminar", function () {
        encuestaSeccionId = parseInt($(this).data("id"));
        var descripcion = $(this).data("nombre");
        $("#span_SeccionNombre").text(descripcion);
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


    //#region FUNCIONALIDAD TABLA SECCIONES
    $("#table_EncuestasSecciones tbody").sortable({ handle: '.ordenador' });

    $(".check_seleccionarTodos").click(function () {
        if ($(this).prop("checked")) {
            $(".check_Seccion").prop("checked", true);
        } else {
            $(".check_Seccion").prop("checked", false);
        }
    });
    $("#table_EncuestasSecciones tbody").on("click", ".btn_Editar", function () {
        encuestaId = parseInt($("#Id").val());
        var seccionId = $(this).data("id");
        editarSeccion(seccionId,encuestaId);
    });

    //Editar nombre de sección
    $("#table_EncuestasSecciones tbody").on("click",".span_SeccionNombre",function () {
        $(".span_SeccionNombre").show();
        $(".span_SeccionNombre").prev().hide();
        $(this).closest("tr").find(".span_SeccionNombre").first().hide();
        $(this).closest("tr").find(".span_SeccionNombre").first().prev().show();
        $(this).closest("tr").find(".span_SeccionNombre").first().prev().find(".txt_SeccionNombre").first().focus();
        $(this).closest("tr").find(".span_SeccionNombre").first().prev().find(".txt_SeccionNombre").first().select();
        $('[data-toggle="tooltip"]').tooltip();       
    });

    $("#table_EncuestasSecciones tbody").on("mouseover", ".span_SeccionNombre",function () {
        $(this).find("i").first().show();

    });
    $("#table_EncuestasSecciones tbody").on("mouseout", ".span_SeccionNombre",function () {
        $(this).find("i").first().hide();
    });

    $("#table_EncuestasSecciones tbody").on("click",".btn_CancelarEditarSeccionNombre",function () {
        $(this).closest(".div_EditarSeccionNombre").hide();
        $(this).closest(".div_EditarSeccionNombre").next().show();
        $(this).closest(".div_EditarSeccionNombre").find(".txt_SeccionNombre").first().val($(this).closest(".div_EditarSeccionNombre").next().find("span").first().text());
    });
    $("#table_EncuestasSecciones tbody").on("click", ".btn_GuardarSeccionNombre", function () {
        //Seccion Id
        var id = parseInt($(this).data("id"));
        var encuestaId = parseInt($("#Id").val());
        var nombre = $(this).closest(".div_EditarSeccionNombre").find(".txt_SeccionNombre").first().val();
        cambiarSeccionNombre(id, encuestaId, nombre,this);
    });
    //#endregion
});


function editarSeccion(id,encuestaId) {
    window.location = `${window.urlproyecto}/Encuestas/EditarSeccion?id=${id}&encuestaId=${encuestaId}`;
}

//#region POST
function cambiarNombreEncuesta() {
    var data = `&id=${parseInt($("#Id").val())}&nombre=${$("#txt_NombreEncuesta").val()}`;
    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/CambiarNombre`,
        //Tipo de petición
        type: "POST",
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
        if ($("#table_EncuestasSecciones tbody").find(".fila_Seccion").length == 0) {
            var item = `<tr class="fila_SinSecciones">
                        <td colspan="4" class="text-center">
                            <h6>No se han encontrado secciones</h6>
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
        var fila = ` <tr class="fila_Seccion">
                            <td class="ordenador" style="cursor:pointer;width:40px">
                                <i class="material-icons text-secondary">
                                    drag_indicator
                                </i>
                            </td>
                            <td style="width:40px">
                               <span class="bmd-form-group is-filled"><div class="checkbox" style="margin-top:-5px">
                                    <label>
                                        <input type="checkbox" class="check_Seccion"><span class="checkbox-decorator"><span class="check"></span><div class="ripple-container"></div></span>
                                    </label>
                                </div></span>
                            </td>
                            <td style="cursor:pointer" class="td_Seccion">
                                <div class="row div_EditarSeccionNombre" style="display:none;margin-top:-10px">
                                    <div class="col-8 ml-3">
                                        <input type="text" class="form-control txt_SeccionNombre" value="${nombre}" autocomplete="off" />
                                    </div>
                                    <div class="col-2 form-inline text-center">
                                        <span class="btn-group-sm m-1">
                                            <button type="button" class="btn btn-success bmd-btn-fab btn_GuardarSeccionNombre" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Guardar nombre de sección">
                                                <i class="material-icons">check</i>
                                            </button>
                                        </span>
                                        <span class="btn-group-sm m-1">
                                            <button type="button" class="btn btn-danger bmd-btn-fab btn_CancelarEditarSeccionNombre" data-toggle="tooltip" data-placement="bottom" title="Cancelar edición de nombre">
                                                <i class="material-icons">clear</i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <span class="mt-1 span_SeccionNombre h6">
                                   <span class="ml-3">${nombre}</span>
                                    <i class="material-icons text-primary md-18" style="display:none" title="Editar nombre de sección">edit</i>
                                </span>
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
        if ($("#table_EncuestasSecciones tbody").find(".fila_SinSecciones").length) {
            $("#table_EncuestasSecciones tbody").html(fila);
        } else {
            $("#table_EncuestasSecciones tbody").append(fila);
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

function cambiarSeccionNombre(id, encuestaId, nombre,btn) {
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
        $(btn).closest(".div_EditarSeccionNombre").hide();
        $(btn).closest(".div_EditarSeccionNombre").next().show();
        $(btn).closest(".div_EditarSeccionNombre").next().find("span").first().text(nombre);
        $('[data-toggle="tooltip"]').tooltip("hide");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        GenerarAlerta(enum_MessageAlertType.Danger, "Ocurrió un error al guarda el Nombre de la encuesta.");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

//#endregion