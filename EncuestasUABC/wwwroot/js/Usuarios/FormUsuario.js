$(document).ready(function () {
    cargarSelectCarreras();
    $("#btn_Guardar").click(function () {
        var isFormValid = true;
        isFormValid = $("#form_Usuario").valid();
        $("#Email").next().text("");
        if ($("#select_Rol").val() != enum_Roles.Egresado) {
            isFormValid = validarCorreoUABC($("#Email").val());
            if (!isFormValid) {
                $("#Email").next().text("El correo debe ser de uabc (@uabc.edu.mx)");
                $("#Email").next().addClass("field-validation-error");
            }
        }

        if (isFormValid) {
            $("#form_Usuario").submit();
        }
    });


    $("#select_Rol").change(function () {
        $("#div_Maestro").hide();
        $("#div_Alumno").hide();
        $("#div_Egresado").hide();
        var rolId = $(this).val();
        if (rolId == "Administrativo") {
            $("#div_Maestro").show();
        } else if (rolId == "Alumno") {
            $("#div_Alumno").show();

        } else if (rolId == "Egresado") {
            $("#div_Egresado").show();
        }
    });
    $("#select_Rol").change();
    $("#btn_CambiarContrasena").click(function () {
        var email = $(this).data("email");
        $("#txt_EmailContrasena").val(email);
        $("#modal_CambiarContrasena").modal("show");
    });

    setCarreraSeleccionada();
});


function validarCorreoUABC(correo) {
    var re = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;
    if (re.test(correo)) {
        if (correo.indexOf("@uabc.edu.mx", correo.length - "@uabc.edu.mx".length) !== -1) {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}

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
        return `${repo.text} ${repo.unidadacademica ? `/ ${repo.unidadacademica}` : ""} ${repo.campus ? `/ ${repo.campus}` : ""}`;
    }
}

function setCarreraSeleccionada() {
    var carreraId = $("#Alumno_CarreraId").val() != "" ? parseInt($("#Alumno_CarreraId").val()) : 0;
    if (carreraId != 0) {
        var carreraSelect = $('#select_Carrera');
        $.ajax({
            type: 'GET',
            url: `${window.urlproyecto}/Carrera/SelectById`,
            data: { id: carreraId }
        }).then(function (data) {
            // create the option and append to Select2
            var option = new Option(`${data.carrera} / ${data.unidadAcademica} / ${data.campus}`, data.id, true, true);
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
