
$(document).ready(function () {
    $('#table_Creadas').DataTable({
        // ServerSide Setups
        serverSide: true,
        //Habilita la paginaci�n
        paging: true,
        // Petici�n AJAX
        ajax: {
            //URL de donde se obtendr�n los datos paginados.
            url: `${window.urlproyecto}/Encuestas/CreadasPaginado`,
            //Tipo de petici�n http
            type: "POST",
            //Tipo de formato que se enviara al servidor.
            contentType: "application/json",
            //tipo de formato que regresar� el servidor.
            dataType: "json",
            //data: Datos que se enviaran al controlador.
            data: function (filtros) {
                //La variable "filtros" contiene los valores necesarios para la paginaci�n.

                var datos = {
                    draw: filtros.draw,
                    columns: filtros.columns,
                    order: filtros.order,
                    start: filtros.start,
                    length: filtros.length,
                    search: filtros.search,
                    OtrosFiltros: []
                };

                //En caso de que se necesiten enviar m�s par�metros agregar alguna de las siguientes l�neas.
                //datos.OtrosFiltros.push(OtroParametro); //Variable
                //datos.OtrosFiltros.push("Valor1"); //string
                //datos.OtrosFiltros.push(2); //Numero                

                //La variable data se convierte a formato JSON y se retorna a "data" para enviarla a la petici�n.
                return JSON.stringify(datos);
            }
        },
        ordering: true,// Habilita el ordenamiento de la tabla.
        //Se especifica la informaci�n de las columnas
        columns: [
            {
                data: 'estatusEncuestaId',
                sortable: true,
                visible: true,
                className: "text-center",
                render: function (data, x, row) {
                    if (data == enum_EstatusEncuesta.Activa) {
                        return `<span class="bmd-form-group is-filled">
                                    <div class="switch" >
                                        <label>
                                            <input class="check_Activo" type="checkbox" checked data-id="${row.id}"><span class="bmd-switch-track"><div class="ripple-container"></div></span>
                                        </label>
                                    </div>
                                </span>`;
                    } else if (data == enum_EstatusEncuesta.Inactiva) {
                        return `<span class="bmd-form-group is-filled">
                                    <div class="switch">
                                        <label>
                                            <input class="check_Activo" type="checkbox" data-id="${row.id}"><span class="bmd-switch-track"><div class="ripple-container"></div></span>
                                        </label>
                                    </div>
                                </span>`;
                    } else {
                        return 'Eliminada';
                    }
                }
            },
            {
                data: 'fecha',
                searchable: false,
                sortable: true,
                visible: true
            },
            {
                data: 'nombre',
                searchable: false,
                sortable: true,
                autoWidth: true,
                visible: true
            },
            {
                data: 'carreraIdNavigation.nombre',
                searchable: false,
                sortable: true,
                autoWidth: true,
                visible: true
            },
            {
                data: 'id',
                sortable: false,
                visible: true,
                className: "text-center",
                render: function (data, x, row) {
                    return `<span class="btn-group-sm">
                                  <a class="btn btn-info bmd-btn-fab" href="${window.urlproyecto}/Encuestas/Editar/${data}" data-toggle="tooltip" data-placement="bottom" title="Editar encuesta">
                                    <i class="material-icons">edit</i>
                                  </a>
                                </span>`;
                }
            },
            {
                data: 'id',
                sortable: false,
                visible: true,
                className: "text-center",
                render: function (data, x, row) {
                    if (row.estatusEncuestaId != enum_EstatusEncuesta.Eliminada) {
                        return `<span class="btn-group-sm">
                                  <button class="btn btn-danger bmd-btn-fab btn_Eliminar" data-id="${data}" data-nombre="${row.nombre}" data-toggle="tooltip" data-placement="bottom" title="Eliminar encuesta">
                                    <i class="material-icons">delete</i>
                                  </button>
                                </span>`;
                    } else {
                        return `<span class="btn-group-sm">
                                  <button class="btn btn-secondary bmd-btn-fab btn_Restaurar" data-id="${data}" data-nombre="${row.nombre}" data-toggle="tooltip" data-placement="bottom" title="Restaurar encuesta">
                                    <i class="material-icons">restore</i>
                                  </button>
                                </span>`;
                    }

                }
            }
        ],
        //Se traducen algunos de los textos de la tabla que se genera.
        language: {
            processing: 'Cargando',
            search: "Buscar _INPUT_",
            lengthMenu: "Elementos por p&#225;gina:  _MENU_",
            info: "Mostrando _START_ - _END_ de _TOTAL_ elementos",
            infoEmpty: "Mostrando _START_ - _END_ de _TOTAL_ elementos",
            emptyTable: "No hay informaci&#243;n",
            paginate: {
                previous: "Anterior",
                next: "Siguiente"
            },
            infoFiltered: " - filtrado de _MAX_ registros en total"
        },
        initComplete: function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
    $("#btn_CrearEncuesta").click(function () {
        $("#modal_EncuestaCrear").modal("show");
    });

    $("#table_Creadas").on("click", ".btn_Eliminar", function () {
        var id = $(this).data("id");
        var nombreEncuesta = $(this).data("nombre");
        $("#text_EncuestaEliminar").val(id);
        $("#span_NombreEncuesta").text(nombreEncuesta);
        $("#modal_EncuestaEliminar").modal("show");
    });

    $("#table_Creadas").on("click", ".btn_Restaurar", function () {
        var id = $(this).data("id");
        var nombreEncuesta = $(this).data("nombre");
        $("#text_EncuestaRestaurar").val(id);
        $("#span_NombreEncuestaRestaurar").text(nombreEncuesta);
        $("#modal_EncuestaRestaurar").modal("show");
    });

    $("#table_Creadas").on("change", ".check_Activo", function () {
        var id = parseInt($(this).data("id"));
        var activo = $(this).prop("checked");
        var data = `&id=${id}&activo=${activo}`;
        //Llamada generica de petici�n AJAX  
        $.ajax({
            //Url de la peticion
            url: `${window.urlproyecto}/Encuestas/CambiarActivo`,
            //Tipo de petici�n
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
        }).fail(function () {
            //Se ejecuta cuando la peticion ha regresado algun error.
            GenerarAlerta(enum_MessageAlertType.Danger, "No se pudo actualizar el estatus de la encuesta.");
        }).always(function () {
            //Se ejecuta al final de la peticion sea exitosa o no.
        });
    });

    $('#CarreraId').select2({
        delay: 250,
        language: "es",
        ajax: {
            url: `${window.urlproyecto}/Encuestas/Carreras`,
            delay: 1000,
            //Tipo de petici�n http
            type: "GET",
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page || 1,
                    perPage: 10
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

    $('#CarreraId').one('select2:open', function (e) {
        $('input.select2-search__field').prop('placeholder', 'Buscar Campus / Unidad Academica / Carrera');
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

        return repo.text;
    }

});