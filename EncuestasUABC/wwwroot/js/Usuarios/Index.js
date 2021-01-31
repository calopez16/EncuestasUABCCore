
$(document).ready(function () {
    cargarPaginado();
    cargarFiltros();
});



function cargarPaginado() {
    var correo = $("#txt-filtro-correo").val();
    var nombre = $("#txt-filtro-nombre").val();
    var roles = $("#select-filtro-roles").val();
    var eliminado = $("#check-filtro-estatus").prop("checked");
    $('#table_Usuarios').DataTable().destroy();
    $('#table_Usuarios').DataTable({
        // ServerSide Setups
        serverSide: true,
        //Habilita la paginación
        paging: true,
        responsive: {
            details: {
                type: 'column',
                target: -1,
                renderer: function (api, rowIdx, columns) {
                    var datos = '';
                    var botones = '';
                    for (var i = 0; i < columns.length; i++) {
                        if (columns[i].hidden) {
                            if (columns[i].title == "") {
                                botones += columns[i].data;
                            } else {
                                datos += `<tr><td><label class="font-weight-bold">${columns[i].title}</label></td>
                                        <td><span>${columns[i].data}</span></tr></td>`;
                            }
                        }

                    }

                    var data = `${datos}
                                    <tr><td colspan="2"><div class="d-flex justify-content-end w-100">${botones}</div></td></tr>`;
                    return data ?
                        $('<table/>').append(data) :
                        false;
                }
            },

        },
        columnDefs: [
            {
                className: 'control',
                targets: -1,
                orderable: false
            }
        ],
        // Petición AJAX
        ajax: {
            //URL de donde se obtendrán los datos paginados.
            url: `${window.urlproyecto}/Usuarios/Paginado`,
            //Tipo de petición http
            type: "POST",
            //Tipo de formato que se enviara al servidor.
            contentType: "application/json",
            //tipo de formato que regresará el servidor.
            dataType: "json",
            beforeSend: function () {
                showEstatusLoading();
            },
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

                datos.OtrosFiltros.push(correo);
                datos.OtrosFiltros.push(nombre);
                datos.OtrosFiltros.push(roles);
                datos.OtrosFiltros.push(eliminado);
                //La variable data se convierte a formato JSON y se retorna a "data" para enviarla a la petición.
                return JSON.stringify(datos);
            }
        },        
        ordering: true,// Habilita el ordenamiento de la tabla.
        searching: false,
        dom: `<'row'<'col-sm-12 col-lg-6 col-lg-4'f>><'row'<'col-sm-12'tr>><'row d-flex justify-content-between'<'col-md-6 col-lg-4'l><'col-md-6 col-lg-4 text-center'i><'col-md-12 col-lg-4'p>>`,
        //Se especifica la información de las columnas
        columns: [
            {
                data: 'userName',
                sortable: true,
                autoWidth: true,
                visible: true
            },

            {
                data: 'nombre',
                sortable: true,
                autoWidth: true,
                visible: true
            },
            {
                data: 'apellidoPaterno',
                sortable: true,
                autoWidth: true,
                visible: true
            },
            {
                data: 'apellidoMaterno',
                sortable: true,
                autoWidth: true,
                visible: true
            },
            {
                data: 'rol',
                sortable: true,
                autoWidth: true,
                visible: true
            },
            {
                data: 'userName',
                sortable: false,
                autoWidth: false,
                className: "text-center",
                render: function (data, type, row) {
                    var item = "";
                    if (row.activo) {
                        item = `<span class="btn-group-sm">
                                  <a class="btn btn-info bmd-btn-fab page-loader" href="${window.urlproyecto}/Usuarios/Edit?email=${data}" data-toggle="tooltip" data-placement="bottom" title="Editar usuario">
                                    <i class="material-icons">edit</i>
                                  </a>
                                </span>`;
                    }
                    return item;
                }
            },
            {
                data: 'userName',
                sortable: false,
                autoWidth: false,
                className: "text-center",
                render: function (data, type, row) {
                    var item = "";
                    if (row.activo) {
                        item = `<span class="btn-group-sm">
                                  <button class="btn btn-danger bmd-btn-fab btn_Eliminar ml-2 ml-mb-0" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Eliminar Usuario">
                                    <i class="material-icons">delete</i>
                                  </button>
                                </span>`;
                    } else {
                        item = `<span class="btn-group-sm">
                                  <button class="btn btn-info bmd-btn-fab btn_Restaurar ml-2 ml-mb-0" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Restaurar Usuario">
                                    <i class="material-icons">restore</i>
                                  </button>
                                </span>`;
                    }
                    return item;
                }
            },
            {
                data: null,
                sortable: false,
                autoWidth: false,
                visible: true,
                render: function (data, type, row) {
                    var item = `<span class="btn-group-sm">
                                  <button class="btn bmd-btn-fab control" data-toggle="tooltip" data-placement="bottom" title="Más información">
                                    <i class="material-icons">expand_more</i>
                                  </button>
                                </span>`;
                    return item;
                }
            },
        ],
        //Se traducen algunos de los textos de la tabla que se genera.
        language: {
            processing: 'Cargando',
            search: "Buscar _INPUT_",
            lengthMenu: "Elementos por página:  _MENU_",
            info: "Mostrando _START_ - _END_ de _TOTAL_ elementos",
            infoEmpty: "Mostrando _START_ - _END_ de _TOTAL_ elementos",
            emptyTable: "No hay información",
            paginate: {
                previous: "Anterior",
                next: "Siguiente"
            },
            infoFiltered: " - filtrado de _MAX_ registros en total"
        },
        initComplete: function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip();
            finishEstatusLoading("Usuarios cargados correctamente", true);
        },
    });

    $("#table_Usuarios").on("click", ".btn_Eliminar", function () {
        var email = $(this).data("id");
        $("#span_EliminarUsuario").text(email);
        $("#text_emailEliminar").val(email);
        $("#modal_EliminarUsuario").modal("show");
    });
    $("#table_Usuarios").on("click", ".btn_Restaurar", function () {
        var email = $(this).data("id");
        $("#span_RestaurarUsuario").text(email);
        $("#text_emailRestaurar").val(email);
        $("#modal_RestaurarUsuario").modal("show");
    });
}

function cargarFiltros() {
    $("#btn-limpiarFiltro").click(function () {
        $("#txt-filtro-correo").val("");
        $("#txt-filtro-nombre").val("");
        $("#select-filtro-roles").val("");
        $("#check-filtro-estatus").prop("checked", false);
        cargarPaginado();
    });
    $("#btn-AplicarFiltros").click(function () {
        cargarPaginado();
    });
}