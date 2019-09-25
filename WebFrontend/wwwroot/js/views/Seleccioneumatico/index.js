var entity = 'Seleccioneumatico';
var apiurl = 'api/' + entity;
$(document).ready(function () {
    // initialize Css
    $("body").addClass("bindex");

    var dd = new DropDown($('#cboruedas'));

    var dd = new DropDown($('#cboruedas2'));
    var d = new DropDown($('#cbollantas'));
    $(document).click(function () {
        $('.cbomenudesplegable').removeClass('active');
    });
    dd.opts = $("#cboruedas ul.dropdown > li");

    $("#cboruedas").on('click', function () {
        $(".searching").remove();
        $(".notfound").remove();
        searchText = "";
        $.ajax({
            beforeSend: function () {
                var html = "<li class='searching' style='margin: 0px 10px;cursor: initial;'>Buscando ...</li>";
                $("#cboruedas ul").append(html);
            },
            url: apiurl,
            dataType: 'json',
            data: {
                q: searchText
            },
            success: function (data) {
                $(".searching").remove();
                var data = data.searchData;
                var html = "";
                if (data.length > 0) {
                    $("#cboruedas ul.dropdown").addClass("scroll");
                    $.map(data, function (item) {
                        html = html + "<li><a href=''>" + item.literal_marca + "</a></li>";
                    });
                    $("#cboruedas ul").append(html);
                } else {
                    $("#cboruedas ul.dropdown").removeClass("scroll");
                    html = html + "<li style='margin: 0px 10px;cursor: initial;'>Not found</li>";
                    $("#cboruedas ul").append(html);
                }

                dd = new DropDown($('#cboruedas'));
            }
        });
    });
    var latestRequestNumber = 0;
    // llanta model
    $("#llanta_model").keyup(function (event) {
        $(".searching").remove();
        $(".notfound").remove();
        var searchText = $(this).val();
        $("#cboruedas ul li").slice(1).remove();
        $.ajax({
            beforeSend: function () {
                var html = "<li class='searching' style='margin: 0px 10px;cursor: initial;'>Buscando ...</li>";
                $("#cboruedas ul").append(html);
            },
            url: "api/Seleccioneumatico",
            dataType: 'json',
            data: {
                q: searchText,
                requestNumber: ++latestRequestNumber
            },
            success: function (data) {
                if (data.requestNumber < latestRequestNumber) return;
                $(".searching").remove();
                var data = data.searchData;

                if (data.length > 0) {
                    var html = "";
                    $("#cboruedas ul.dropdown").addClass("scroll");
                    $.map(data, function (item) {
                        html = html + "<li><a href=''>" + item.literal_marca + "</a></li>";
                    });
                    $("#cboruedas ul").append(html);
                } else {
                    var html = "";
                    $("#cboruedas ul.dropdown").removeClass("scroll");
                    html = "<li class=notfound style='margin: 0px 10px;cursor: initial;'>Not found</li>";
                    $("#cboruedas ul").append(html);
                }

                dd = new DropDown($('#cboruedas'));
            }
        });
    });
    
    $('form input').keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    });

});

function DropDown(el) {
    this.dd = el;
    this.placeholder = this.dd.children('span');
    this.input = this.dd.children('input');
    this.opts = this.dd.find('ul.dropdown > li');
    this.val = '';
    this.index = -1;
    this.initEvents();
}
DropDown.prototype = {
    initEvents: function () {
        var obj = this;
        obj.dd.on('click', function (event) {
            if ($(this)[0].id == "cboruedas") {
                if (obj.opts.length > 1) {
                    return false;
                }
            }

            $(this).toggleClass('active');
            return false;
        });
        obj.opts.on('click', function () {
            if ($(this)[0].children[0].id == 'llanta_model') {
                return false;
            }
            if ($(this)[0].children[0].id == 'llanta') {
                return false;
            }
            if ($(this)[0].children[0].id == 'neumatico') {
                return false;
            }
            var opt = $(this); obj.val = opt.text(); obj.index = opt.index();
            if (obj.val != "Not found") {
                obj.placeholder.text(obj.val);
                obj.input.val(obj.val);
            }
            isCheck();
            $(obj.dd.selector + " .cboCoche").addClass("cbo14");
            console.log("click select");
        });
    },
    getValue: function () { return this.val; },
    getIndex: function () { return this.index; }
}

// check if select all 3 options 
function isCheck() {
    var llanta_model = $(".llanta_model").html();
    var llanta = $(".llanta").html();
    var neumatico = $(".neumatico").html();
    if (llanta_model != "Modelo de neumático" && llanta != "Dimensión de la llanta" && neumatico != "Número de neumáticos comprados") {
        var form = $("form[id=participa]");
        form.submit();
    }
    return false;
}
