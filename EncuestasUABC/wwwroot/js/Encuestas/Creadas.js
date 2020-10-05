
$(document).ready(function () {
    $('#table_Creadas').DataTable({
        // ServerSide Setups
        serverSide: true,
        //Habilita la paginación
        paging: true,
        // Petición AJAX
        ajax: {
            //URL de donde se obtendrán los datos paginados.
            url: `${window.urlproyecto}/Encuestas/CreadasPaginado`,
            //Tipo de petición http
            type: "POST",
            //Tipo de formato que se enviara al servidor.
            contentType: "application/json",
            //tipo de formato que regresará el servidor.
            dataType: "json",
            //data: Datos que se enviaran al controlador.
            data: function (filtros) {
                //La variable "filtros" contiene los valores necesarios para la paginación.

                var datos = {
                    draw: filtros.draw,
                    columns: filtros.columns,
                    order: filtros.order,
                    start: filtros.start,
                    length: filtros.length,
                    search: filtros.search,
                    OtrosFiltros: []
                };

                //En caso de que se necesiten enviar más parámetros agregar alguna de las siguientes líneas.
                //datos.OtrosFiltros.push(OtroParametro); //Variable
                //datos.OtrosFiltros.push("Valor1"); //string
                //datos.OtrosFiltros.push(2); //Numero                

                //La variable data se convierte a formato JSON y se retorna a "data" para enviarla a la petición.
                return JSON.stringify(datos);
            }
        },
        ordering: true,// Habilita el ordenamiento de la tabla.
        //Se especifica la información de las columnas
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
                                            <input type="checkbox" checked=""><span class="bmd-switch-track"><div class="ripple-container"></div></span>
                                        </label>
                                    </div>
                                </span>`;
                    } else if (data == enum_EstatusEncuesta.Inactiva) {
                        return `<span class="bmd-form-group is-filled">
                                    <div class="switch">
                                        <label>
                                            <input type="checkbox" checked=""><span class="bmd-switch-track"><div class="ripple-container"></div></span>
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
        $("#modal_CrearEncuesta").modal("show");
    });

    $("#table_Creadas").on("click", ".btn_Eliminar", function () {
        var id = $(this).data("id");
        var nombreEncuesta = $(this).data("nombre");
        $("#text_EncuestaEliminar").val(id);
        $("#span_NombreEncuesta").text(nombreEncuesta);
        $("#modal_EliminarEncuesta").modal("show");
    });

    $("#table_Creadas").on("click", ".btn_Restaurar", function () {
        var id = $(this).data("id");
        var nombreEncuesta = $(this).data("nombre");
        $("#text_EncuestaRestaurar").val(id);
        $("#span_NombreEncuestaRestaurar").text(nombreEncuesta);
        $("#modal_RestaurarEncuesta").modal("show");
    });
});