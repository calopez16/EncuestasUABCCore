
$(document).ready(function () {
    CargarUsuarios();
});

function CargarUsuarios() {
    var table=$('#table_Usuarios').DataTable({
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
        searching: true,
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
                    var item = `<span class="btn-group-sm">
                                  <a class="btn btn-info bmd-btn-fab" href="${window.urlproyecto}/Usuarios/Edit?email=${data}" data-toggle="tooltip" data-placement="bottom" title="Editar usuario">
                                    <i class="material-icons">edit</i>
                                  </a>
                                </span>`;
                    return item;
                }
            },
            {
                data: 'userName',
                sortable: false,
                autoWidth: false,
                className: "text-center",
                render: function (data, type, row) {
                    var item = `<span class="btn-group-sm">
                                  <button class="btn btn-danger bmd-btn-fab btn_Eliminar ml-2 ml-mb-0" data-id="${data}" data-toggle="tooltip" data-placement="bottom" title="Eliminar usuario">
                                    <i class="material-icons">delete</i>
                                  </button>
                                </span>`;
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
        }
    });
  
    $("#table_Usuarios").on("click", ".btn_Eliminar", function () {
        var email = $(this).data("id");
        $("#span_EliminarUsuario").text(email);
        $("#text_emailEliminar").val(email);
        $("#modal_EliminarUsuario").modal("show");
    });
}