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
});


