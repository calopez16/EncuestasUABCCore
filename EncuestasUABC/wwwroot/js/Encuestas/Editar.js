const dateFormat = 'DD/MM/YYYY HH:mm:ss';
var encuestaId = 0;
var carreraId = 0;
var carreraDescripcion="";
$(document).ready(function () {

    encuestaId = parseInt($("#Id").val());
    carreraId =parseInt($("#IdCarrera").val());
    $("#btn_EditarNombre").on("click", function () {
        $("#modal_EditarNombreDescripcion").modal("show");
    });

    cargarSelectCarreras();
    setCarreraSeleccionada();
    //#region EDITAR NOMBRE ENCUESTA
    $(".editarInformacion").click(function () {
        $("#div_informacion_Encuesta").hide();
        $("#div_EditarInformacionEncuesta").show();
        $("#txt_NombreEncuesta").focus();
        $("#txt_NombreEncuesta").select();
        var asdas = $("#div_descripcion").find("div").html();
        $('#textArea_Descripcion').summernote('code', $("#div_descripcion").find("div").html());        
       
    });

    $("#Informacion_Encuesta").mouseover(function () {
        $("#Informacion_Encuesta").find("i").first().show();

    });
    $("#Informacion_Encuesta").mouseout(function () {
        $("#Informacion_Encuesta").find("i").first().hide();
    });

    $("#btn_CancelarEditarInformacionEncuesta").click(function () {
        $("#div_EditarInformacionEncuesta").hide();
        $("#div_informacion_Encuesta").show();
        $("#txt_NombreEncuesta").val($("#h1_NombreEncuesta").text());
        carreraDescripcion = "";
        setCarreraSeleccionada();
    });
    $("#btn_GuardarInformacionEncuesta").click(function () {
        actualizarInformacionEncuesta();
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
    $("#table_EncuestasSecciones tbody").sortable({
        handle: '.ordenador',
        update: function (event, ui) {
            actualizarPosicionSecciones();
        }
    });

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
        editarSeccion(seccionId, encuestaId);
    });

    //Editar nombre de sección
    $("#table_EncuestasSecciones tbody").on("click", ".span_SeccionNombre", function () {
        $(".span_SeccionNombre").show();
        $(".span_SeccionNombre").prev().hide();
        $(this).closest("tr").find(".span_SeccionNombre").first().hide();
        $(this).closest("tr").find(".span_SeccionNombre").first().prev().show();
        $(this).closest("tr").find(".span_SeccionNombre").first().prev().find(".txt_SeccionNombre").first().focus();
        $(this).closest("tr").find(".span_SeccionNombre").first().prev().find(".txt_SeccionNombre").first().select();
        $('[data-toggle="tooltip"]').tooltip();
    });

    $("#table_EncuestasSecciones tbody").on("mouseover", ".span_SeccionNombre", function () {
        $(this).find("i").first().show();

    });
    $("#table_EncuestasSecciones tbody").on("mouseout", ".span_SeccionNombre", function () {
        $(this).find("i").first().hide();
    });

    $("#table_EncuestasSecciones tbody").on("click", ".btn_CancelarEditarSeccionNombre", function () {
        $(this).closest(".div_EditarSeccionNombre").hide();
        $(this).closest(".div_EditarSeccionNombre").next().show();
        $(this).closest(".div_EditarSeccionNombre").find(".txt_SeccionNombre").first().val($(this).closest(".div_EditarSeccionNombre").next().find("span").first().text());
    });
    $("#table_EncuestasSecciones tbody").on("click", ".btn_GuardarSeccionNombre", function () {
        //Seccion Id
        var id = parseInt($(this).data("id"));
        var encuestaId = parseInt($("#Id").val());
        var nombre = $(this).closest(".div_EditarSeccionNombre").find(".txt_SeccionNombre").first().val();
        cambiarSeccionNombre(id, encuestaId, nombre, this);
    });
    //#endregion
});

function actualizarPosicionSecciones() {
    var data = new FormData();
    data.append("encuestaId", encuestaId);
    $(".txt_SeccionId").each(function (i, item) {
        var seccionId = $(this).val();
        data.append(`seccionId[${i}]`, seccionId);
    });

    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/ActualizarPosicionSecciones`,
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
        finishEstatusLoading("Encuesta actualizada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al actualizar la encuesta");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function editarSeccion(id, encuestaId) {
    window.location = `${window.urlproyecto}/Encuestas/EditarSeccion?id=${id}&encuestaId=${encuestaId}`;
}

function actualizarInformacionEncuesta() {
    var data = new FormData();
    data.append("id", parseInt($("#Id").val()));
    data.append("nombre", $("#txt_NombreEncuesta").val());
    data.append("carreraId", $("#select_Carrera").val());
    data.append("descripcion", $("#textArea_Descripcion").val());
    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: `${window.urlproyecto}/Encuestas/CambiarNombre`,
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
        $("#div_EditarInformacionEncuesta").hide();
        $("#div_informacion_Encuesta").show();
        $("#h1_NombreEncuesta").text($("#txt_NombreEncuesta").val());
        $('[data-toggle="tooltip"]').tooltip("hide");
        $("#span_CarreraDescripcion").text(carreraDescripcion);
        $("#div_descripcion").find("div").html($("#textArea_Descripcion").val());
        finishEstatusLoading("Encuesta actualizada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al guardar el Nombre de la encuesta");
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
            showEstatusLoading();
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
        finishEstatusLoading("Sección eliminada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al tratar de eliminar la Sección",false);
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
            showEstatusLoading();
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
        var fila = ` <tr class="fila_Seccion">
                            <td class="ordenador" style="cursor:pointer;width:40px">
                                <i class="material-icons text-secondary">
                                    drag_indicator
                                </i>
                                <input hidden value="${data}" class="txt_SeccionId" />
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
        finishEstatusLoading("Sección creada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al tratar de eliminar la Sección",false);
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

function cambiarSeccionNombre(id, encuestaId, nombre, btn) {
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
        $(btn).closest(".div_EditarSeccionNombre").hide();
        $(btn).closest(".div_EditarSeccionNombre").next().show();
        $(btn).closest(".div_EditarSeccionNombre").next().find("span").first().text(nombre);
        $('[data-toggle="tooltip"]').tooltip("hide");
        finishEstatusLoading("Sección actualizada");
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
        finishEstatusLoading("Ocurrió un error al guarda el Nombre de la encuesta");
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}

//CARGAS DE COMPONENTES

function cargarSelectCarreras() {
    $('#select_Carrera').select2({
        delay: 250,
        language: "es",
        ajax: {
            url: `${window.urlproyecto}/Carrera/Select`,
            delay: 1000,
            //Tipo de petición http
            type: "GET",
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page || 1,
                    perPage: 10,
                    id: $(this).data("id")
                }
                // Query parameters will be ?search=[term]&page=[page]
                return query;
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                return {
                    results: data.results,
                    pagination: {
                        more: (params.page * 10) < data.totalRegistros
                    }
                };
            }
        },
        placeholder: 'Seleccione',
        templateResult: formatRepo,
        templateSelection: formatRepoSelection
    });

    $('#select_Carrera').one('select2:open', function (e) {
        $('input.select2-search__field').prop('placeholder', 'Buscar Campus / Unidad Académica / Carrera');
    });

    function formatRepo(repo) {
        if (repo.loading) {
            return repo.text;
        }
        var $container = $(`<div>
                            <h5>${repo.text}</h5>
                            <h6><strong>Unidad acad&eacute;mica: </strong>${repo.unidadacademica}</h6>
                            <h6><strong>Campus: </strong>${repo.campus}</h6>
                        </div>`);
        return $container;
    }

    function formatRepoSelection(repo) {
        if (repo.carrera) {
            carreraDescripcion = ` ${repo.unidadacademica ? `${repo.unidadacademica}` : ""}, ${repo.carrera}, ${repo.campus ? `${repo.campus}` : ""}`;
            return `${repo.unidadacademica ? `${repo.unidadacademica}` : ""} / ${repo.text} / ${repo.campus ? `${repo.campus}` : ""}`;
        }
        carreraDescripcion = `${repo.text}`;
        return `${repo.text}`;
    }
}

function setCarreraSeleccionada() {
    if (carreraId != 0) {
        var carreraSelect = $('#select_Carrera');
        $.ajax({
            type: 'GET',
            url: `${window.urlproyecto}/Carrera/SelectById`,
            data: { id: carreraId }
        }).then(function (data) {
            // create the option and append to Select2
            var option = new Option(`${data.unidadAcademica} / ${data.carrera} / ${data.campus}`, data.id, true, true);
            carreraSelect.append(option).trigger('change');

            // manually trigger the `select2:select` event
            carreraSelect.trigger({
                type: 'select2:select',
                params: {
                    data: data
                }
            });
        });
    }

}