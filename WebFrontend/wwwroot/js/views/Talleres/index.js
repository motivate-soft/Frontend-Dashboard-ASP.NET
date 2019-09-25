var entity = 'Talleres';
var apiurl = 'api/' + entity;
$(document).ready(function () {
    // initialize Css
    $("body").addClass("bindex_det");

    var nombreBuscar = $("#abuscar").val();
    var pais = "ES";
    var data = { Razon: nombreBuscar, Pais: pais };

    // action
    $("#abuscar").on("keypress", function (e) {

        if (e.keyCode === 13 && !e.shiftKey) {
            e.preventDefault();
            buscar_taller();
        }
    });

    $(".btnFrmEnviado").on('click', function () {
        buscar_taller();
    });
});

function buscar_taller() {
    console.log("click search button ...");
    $(".resTaller>ul").empty();
    $(".resTaller").css("overflow-y", "hidden");

    var form = $("form[id=frmFormulario]");
    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        $.ajax({
            beforeSend: function () {
                $(".spinner-grow").show();
            },
            type: 'POST',
            url: apiurl,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                $(".spinner-grow").hide();
                if (data.success) {
                    console.log("Success");
                    var tabla = data.data;
                    if (tabla.length < 1) {
                        $(".titError3").show();
                    } else {
                        if (tabla.length > 4) {
                            $(".resTaller").css("overflow-y", "scroll");
                        }
                        $(".titError3").hide();
                        for (i = 0; i < tabla.length; i++) {
                            $(".resTaller>ul").append("<li><strong>" + tabla[i].alias_tall + "</strong><br/>" + tabla[i].direccion_tall + "<br/>" + tabla[i].poblacion_tall + ", " + tabla[i].provincia_tall + "<br/>" + tabla[i].cp_tall + "</li>");
                        }
                    }
                } else {
                    console.log("Fail");
                }
            }
        });
    }
}