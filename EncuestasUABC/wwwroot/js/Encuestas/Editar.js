const dateFormat = 'DD/MM/YYYY HH:mm:ss';

$(document).ready(function () {
    $("#btn_EditarNombre").on("click", function () {
        $("#modal_EditarNombreDescripcion").modal("show");
    });

    $("#form_EditarNombreDescripcion").validate({
        rules: {
            Nombre: "required",
            Descripcion: "required"
        },
        messages: {
            Nombre: "El campo Nombre es obligatorio",
            Descripcion: "El campo Apellido Paterno es obligatorio",
        }
    });
    $("#lista_secciones").sortable();
    var baseUrl = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
    alert(baseUrl);
});



function CargarEncuesta() {
    //Llamada generica de petición AJAX  
    $.ajax({
        //Url de la peticion
        url: "",
        //Tipo de petición
        type: "POST",
        //Tipo de formato que se enviara al servidor.
        contentType: "application/json",
        //tipo de formato que regresará el servidor.
        dataType: "json",
        //Datos que se enviaran a la llamada
        data: { buscar: txt_Search },
        //Accion al comenzar la carga de la peticion AJAX.
        beforeSend: function () {
            //Aqui regularmente se implementa un loading.
        }
    }).done(function (data) {
        //Se ejecuta cuando la peticion ha sido exitosa. 
        //data es la respuesta que se recibe.
    }).fail(function () {
        //Se ejecuta cuando la peticion ha regresado algun error.
    }).always(function () {
        //Se ejecuta al final de la peticion sea exitosa o no.
    });
}