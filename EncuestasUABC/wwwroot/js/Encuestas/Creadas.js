
$(document).ready(function() {
    $('#table_paginado').DataTable({
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
                data: 'Fecha',
                searchable: false,
                sortable: true,
                autoWidth: true,
                visible: true
            },

            {
                data: 'Nombre',
                searchable: false,
                sortable: true,
                autoWidth: true,
                visible: true
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

    $("#select_Carrera").select2({
        placeholder: "Select Multiple Values",
        theme: "bootstrap4",
        allowClear: true,
        ajax: {
            url: `${window.urlproyecto}/Encuestas/CarreraAutoComplete`,
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query =
                {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            id: item.IdCarrera,
                            text: item.Nombre
                        };
                    }),
                };
            }
        }
    });

});