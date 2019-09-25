var adjunto1_par = "";
var adjunto2_par = "";
var adjunto3_par = "";
var adjunto4_par = "";
var adjunto5_par = "";

var entity = 'EnviarResendParticipa';
var apiurl = 'api/' + entity;
$(document).ready(function () {
    $("body").addClass("bindex_det");

    $(".upload_area").on('click', '.adjunto-file', function () {
        $(this).parent().find("input[class=adjunto]").click();
        $(this).removeClass("borderdotLine");
        $(".titError2").hide();
    });

    // file selected
    $(".upload_area").on('change', '.adjunto', function () {
        var fd = new FormData();
        var file = $(this)[0].files[0];
        var archivo = this.files[0];
        var extension = file.name.split('.').pop();
        if ((extension.toLowerCase() == "pdf" || extension.toLowerCase() == "jpg" || extension.toLowerCase() == "png") && (file.size < 4000000)) {
            $("#msg_vista_configuracion_archivos").html('');
            var p_tag = $(this).parent().find("p");
            p_tag.html('<em>Subiendo adjunto...</em><br />El proceso puede tardar unos minutos.');
            fd.append('file', file);
          
            uploadFile(fd, archivo, p_tag);
            $(".first-file").removeClass("borderdotLine");
        } else {
            var p_tag = $(this).parent().find("p");
            p_tag.parent().addClass("borderdotLine");

            p_tag.html('<u>Seleccione</u> o arrastre un archivo<br />(Máx 4MB, JPG/PNG/PDF)');
            var msg = "El archivo no cumple con los requisitos de tamaño o extensión...";
            $(".titError2").html(msg);
            $(".titError2").show();

            switch (p_tag.attr('class')) {
                case "p1":
                    adjunto1_par = "";
                    break;
                case "p2":
                    adjunto2_par = "";
                    break;
                case "p3":
                    adjunto3_par = "";
                    break;
                case "p4":
                    adjunto4_par = "";
                    break;
                case "p5":
                    adjunto5_par = "";
                    break;
                default:
                    adjunto1_par = "";
                    break;
            }
        }
    });
    

    $(".adduploadbtn").on('click', function () {
        var count = $(".upload_area > div").length;
        if (count > 5) {
            return false;
        }
        var uploadbtn = '<div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-xs-12 frmCol">'+
            '<label class="control-label etFrm" > Subir imagen '+count+'</label >' +
            '<div class="form-group">' +
            '<input type="file" name="ImageFiles" class="adjunto" >' +
            //'<input type="hidden" name="adjunto'+count+'_par"/>' + 
            '<div class="upload-area adjunto-file">' +
            '<p class="p'+count+'"><u>Selecciona</u> o arrastra un archivo<br />(M&aacute;x 4MB, JPG/PNG/PDF)</p>' +
            '</div>' +
            '</div>' +
                '</div>';
        $(".adduploadbtn-area").before(uploadbtn);
        if (count > 4) {
            $(".adduploadbtn").css({ "opacity": 0.4, "cursor": "default" });
            return false;
        }
    });


    // to validate regarding hidden fields
    $.validator.setDefaults({
        ignore: []
    });
    
    $("form[id=frmFormulario]").validate({
        onkeyup: false,
        rules: {
            adjunto_par: {
                required: true
            }
        },
        highlight: function (element) {
            $(element).parent().addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).parent().removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'validation-error-message help-block form-helper bold',
        errorPlacement: function (error, element) {
            if (element[0].id == "adjunto_par") {
                $(".first-file").addClass("borderdotLine");
            }
            return false;
        }
    });
    
    var disable_flag = 1;
    $(".btnFrm").on('click', function () {
        var adjunto_par = "";
        adjunto_par = adjunto1_par + "|" + adjunto2_par + "|" + adjunto3_par + "|" + adjunto4_par + "|" + adjunto5_par;
        console.log(adjunto_par);
        adjunto_par = adjunto_par.replace("||||", "|");
        adjunto_par = adjunto_par.replace("|||", "|");
        adjunto_par = adjunto_par.replace("||", "|");
        adjunto_par = adjunto_par.replace("||", "|");
        if (adjunto_par[adjunto_par.length - 1] == "|") {
            adjunto_par = adjunto_par.slice(0, -1);
        }
        if (adjunto_par.charAt(0) == "|") {
            adjunto_par = adjunto_par.substr(1);
        }

        $("#adjunto_par").val(adjunto_par);

        var form = $("form[id=frmFormulario]");
        if (disable_flag == 0) {
            return;
        }
        
        $.validator.unobtrusive.parse(form);
        var data = $(form).serializeJSON();
        if ($(form).valid()) {

            data = JSON.stringify(data);
            console.log("submit");
            $.ajax({
                beforeSend: function () {
                    $("#btnEnivar").text("Enviar...");
                    $("#btnEnivar").removeClass("btnFrm");
                    $("#btnEnivar").addClass("btnFrm_disabled");

                    disable_flag = 0;
                },
                type: 'POST',
                url: apiurl,
                data: data,
                contentType: 'application/json',
                success: function (data) {
                    console.log(data);
                    if (data.success) {
                        window.location.href = "/es/mod-adjuntar-ok";
                    }
                }
            });
        }
    });
});

function uploadFile(formdata, archivo, p_tag) {
    $.ajax(
        {
            url: "api/Upload",
            data: formdata,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (response) {
                if (response.success) {
                    p_tag.html("<strong>" + archivo.name + "</strong>");

                    switch (p_tag.attr('class')) {
                        case "p1":
                            adjunto1_par = response.tempfileName + ":" + archivo.name;
                            break;
                        case "p2":
                            adjunto2_par = response.tempfileName + ":" + archivo.name;
                            break;
                        case "p3":
                            adjunto3_par = response.tempfileName + ":" + archivo.name;
                            break;
                        case "p4":
                            adjunto4_par = response.tempfileName + ":" + archivo.name;
                            break;
                        case "p5":
                            adjunto5_par = response.tempfileName + ":" + archivo.name;
                            break;
                        default:
                            adjunto1_par = response.tempfileName + ":" + archivo.name;
                            break;
                    }
                    
                }
                else {
                    adjunto_par = "";
                }
            }
        }
    );
};

