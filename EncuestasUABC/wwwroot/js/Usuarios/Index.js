
$(document).ready(function () {
    CargarUsuarios();
});

function CargarUsuarios() {
    $('#table_Usuarios').DataTable({
        // ServerSide Setups
        serverSide: true,
        //Habilita la paginación
        paging: true,
        // Petición AJAX
        ajax: {
            //URL de donde se obtendrán los datos paginados.
            url: "/Usuarios/Paginado",
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
                sortable: false,
                autoWidth: true,
                visible: true
            },
            {
                data: 'userName',
                sortable: false,
                autoWidth: true,
                className: "text-center",
                render: function (data, type, row) {
                    var item = `<a href="/Usuarios/Edit?email=${data}" class="btn btn-primary m-1" title="Editar"><i class="fa fa-edit"></i></a>`;
                    item += `<button class="btn btn-danger m-1 btn_Eliminar" title="Editar" data-id="${data}"><i class="fa fa-trash"></i></button>`;
                    return item;
                }
            },

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
    });

    $("#table_Usuarios").on("click", ".btn_Eliminar", function () {
        var email = $(this).data("id");
        $("#span_EliminarUsuario").text(email);
        $("#text_emailEliminar").val(email);
        $("#modal_EliminarUsuario").modal("show");
    });
}