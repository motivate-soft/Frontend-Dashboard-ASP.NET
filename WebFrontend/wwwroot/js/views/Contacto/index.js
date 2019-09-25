var entity = 'Contacto';
var apiurl = 'api/' + entity;
$(document).ready(function () {
    // initialize Css
    $("body").addClass("bindex_det");

    $("#btnCV").on('click', function () {
        window.location.href = "/es";
    });

    // to validate regarding hidden fields
    $.validator.setDefaults({
        ignore: []
    });

    $("input[name=email]").keyup(function (event) {
        $(this).valid();
    });

    $("input[name=movil]").keyup(function (event) {
        $(this).valid();
    });

    $("input[name=chckprivacidad]").click(function (event) {
        $(this).valid();
    });

    $("form[id=frmFormulario]").validate({
        onkeyup: false,
        rules: {
            nombre: {
                required: true
            },
            apellidos: {
                required: true
            },
            email: {
                email: true,
                required: true,
                regex: /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            },
            movil: {
                required: true,
                maxlength: 12,
                digits: true
            },
            escribe: {
                required: true
            },
            chckprivacidad: {
                required: true
            },
            hiddenRecaptcha: {
                required: function () {
                    if (grecaptcha.getResponse() == '') {
                        return true;
                    } else {
                        console.log(grecaptcha.getResponse());
                        return false;
                    }
                },
                minlength: 255
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
            if (element[0].id == "chckprivacidad") {
                $("#labchckprivacidad").addClass("has-error");
            }
            if (element[0].id == "hiddenRecaptcha") {
                console.log($(".g-recaptcha").children("div"));
                $(".g-recaptcha").children("div").addClass("borderLine");
            }
            return false;
        }
    });
    var disable_flag = 1;
    // send email and sms
    $("#btnCE").on('click', function (e) {
        var form = $("form[id=frmFormulario]");
        if (disable_flag == 0) {
            return;
        }
        $.validator.unobtrusive.parse(form);
        if ($(form).valid()) {
            var data = $(form).serializeJSON();
            console.log(data);
            data = JSON.stringify(data);
            $.ajax({
                beforeSend: function () {
                    $("#btnCE").text("Enviar...");
                    $("#btnCE").removeClass("btnFrm");
                    $("#btnCE").addClass("btnFrm_disabled");

                    disable_flag = 0;
                },
                type: 'POST',
                url: apiurl,
                data: data,
                contentType: 'application/json',
                success: function (data) {
                    console.log(data);
                    if (data.success) {
                        window.location.href = "/es/contacto-enviado";
                    } else {
                        alert("Fail");
                    }
                }
            });
        }
    });

    
});

function recaptcha_callback() {
    if (grecaptcha.getresponse().length !== 0) {
        console.log($(".g-recaptcha").children("div"));
        $(".g-recaptcha").children("div").removeclass("borderLine");
        return true;
    } else {
        console.log("callbackl");
        console.log($(".g-recaptcha").children("div"));
        $(".g-recaptcha").children("div").addclass("borderLine");
        return false;
    }
}



