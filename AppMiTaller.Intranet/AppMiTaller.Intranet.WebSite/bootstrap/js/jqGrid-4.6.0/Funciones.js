/*************************************************************************
Modificaciones
*************************************************************************/
/*************************************************************************
@001 ... 00/00/0000 MOTIVO DE CAMBIO: Se cambia la ruta del icono calendario
@002 ACP 16/04/2017 MOTIVO DE CAMBIO: Se agrega opciones a la carga del JQGrid
@003 GMC 24/05/2017 MOTIVO DE CAMBIO: Se agrega funcion para seleccionar datepicker
@004 RNT 29/06/2017 MOTIVO DE CAMBIO: Ajuste
@005 ACP 05/07/2017 MOTIVO DE CAMBIO: Migración Bootstrap
@006 FPS 20/07/2017 MOTIVO DE CAMBIO: Migración Bootstrap - Implementación control FileUpload
@007 FPS 14/09/2017 MOTIVO DE CAMBIO: Se agrega parametro al FormatFecha
@008 FPS 25/09/2017 MOTIVO DE CAMBIO: Se corrige función de valida ruc
@009 FPS 26/09/2017 MOTIVO DE CAMBIO: Se cambia alert por funciones de alerta
@010 FPS 17/10/2017 MOTIVO DE CAMBIO: Ajuste para corregir en Safari del Ipad el control DatePicker dentro de Modal
@011 FPS 11/12/2017 MOTIVO DE CAMBIO: FileUpload Redirect a logeo cuando expira la sesión.
@012 FPS 15/12/2017 MOTIVO DE CAMBIO: Función para DatePicker Bootstrap y control de sesión en fileupload
*************************************************************************/

var RE_ALAFANUMERICO = /^[\w\d áéíóúAÉÍÓÚÑñüÜ_’.,¿?:&()#º\+\[\]/ \\ \-]+$/i;
var RE_ALAFANUMERICONOESP = /^[\w\dáéíóúAÉÍÓÚÑñ]+$/i;
var RE_SOLONRO = /^\d+$/i;
var RE_EMAIL = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/i;
var RE_COLORWEB = /^#?([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$/i;
var RE_PATH = /([A-Z]:\\[^/:\*\?<>\|]+\.\w{2,6})|(\\{2}[^/:\*\?<>\|]+\.\w{2,6})/i;
var RE_IP = /^(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)(?:[.](?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)){3}$/i;
var RE_PRECIO = /^(-)?\d+(\.\d?\d?)?$/i;
var RE_NRO_DECIMAL = /^[\- \d ,.]+$/i;
var MASCARA_FECHA = '__/__/____';
var RE_CUENTA_CORRIENTE = /^[\d \- /]*$/i;
var RE_NUMERO_TELEFONO = /^[\d () \- /]*$/i;
var RE_PLACA = /^[\w\d\ñÑ\-]*$/i;
var RE_CODIGO = /^[\w\d\-_.]*$/i;
var RE_NRO_VERSION = /^[\d.]+$/i;

//@006 I
$(document).ready(function () {
    $(".panel-heading[data-spy=affix]").width($(".panel-default").width() - 10);
    //@010 I
    $(".modal").on("show.bs.modal", function (e) {
        try {
            var datePicker = document.getElementById("ui-datepicker-div");
            if (datePicker) {
                e.delegateTarget.appendChild(datePicker);
            }
        }
        catch (ex) { console.log("Error: show.bs.modal: " + ex); }
    });
    $(".modal").on("hide.bs.modal", function (e) {
        try {
            var datePicker = document.getElementById("ui-datepicker-div");
            if (datePicker) {
                $("body").append(datePicker);
            }
        }
        catch (ex) { console.log("Error: hide.bs.modal: " + ex); }
    });
    //@010 F
});

function fc_UpdatePanelHeaderBotones() {
    try {
        $(".panel-heading[data-spy=affix]").width($(".panel-default").width() - 10);

        $("#btnVerBotones").hide();
        $("#divBotones").removeClass("dropdown-menu dropdown-menu-right list-group");
        $("#divBotones button").removeClass("list-group-item");

        var widthTituloPagina = $("#divTituloPagina").width() + 50;
        var widthDivBotones = $("#divBotones").width();
        var widthPanelDefault = $(".panel-default").width();
        var heightPanelHeader = $(".panel-heading.clearfix[data-spy=affix]").height();

        if (((widthDivBotones >= (widthPanelDefault - widthTituloPagina)) && !fc_ExistDisplayControl("btnVerBotones")) || heightPanelHeader > 50) {
            $("#btnVerBotones").show();
            $("#divBotones").addClass("dropdown-menu dropdown-menu-right list-group");
            $("#divBotones button").addClass("list-group-item");
        }
    }
    catch (ex) {
        fc_AlertError("Error: " + ex);
    }
}

$(window).resize(function () {
    fc_UpdatePanelHeaderBotones();
});

function fc_ExistDisplayControl(Id_control) {
    if (Id_control.indexOf("#") >= 0) Id_control = Id_control.replace("#", "");

    if (document.getElementById(Id_control) != null && $("#" + Id_control).is(':visible')) {
        return true;
    }
    else return false;
}
//@006 F

//@005 F
function fc_ValidarRuc(strNomRuc) {
    var suma = "0";
    
    var Ruc = "";
    if (document.getElementById(strNomRuc) != null) Ruc = new String(document.getElementById(strNomRuc).value);
    else Ruc = strNomRuc;
    
    var result = false;
    var cadena = "";
    if (Ruc != "99999999999") {
        if (Ruc.length == 11) {
            Ruc = Ruc.split("");
            var strPar = new String("5,4,3,2,7,6,5,4,3,2,");
            var arrPar = new Array(10);
            arrPar = strPar.split(",");

            var caracter = parseFloat(Ruc[10]);
            for (var i = 0; i < 10; i++) {
                suma = parseFloat(suma) + parseFloat(arrPar[i]) * parseFloat(Ruc[i]);
            }

            var resto = suma % 11;
            var verificador = 11 - resto;
            if (verificador == 11) {
                verificador = 1;
            }
            else if (verificador == 10) {
                verificador = 0;
            }
            if (verificador != caracter) {
                cadena = mstrRUCInvalido;
                result = false;
            }
            else { result = true; }
        }
        else {
            result = false;
            cadena = mstrLongitudRUC;
        }
    }
    else {
        result = true;
    }

    return cadena;
}
//@005 I

function fc_ValidarEmail(e) {
    return expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/, expr.test(e) ? !0 : !1
}

function fc_SoloLetras() {
    var e = window.event ? event.keyCode : event.which;
    if ((e == 32) || (e >= 65 && e <= 90) || (e >= 97 && e <= 122) || (e == 209) || (e == 241)) {
    }
    else {
        event.keyCode = 0;
        //alert('Solo se permite ingresar letras');
        return false;
    }
    return true;
}

function fc_SoloNumeros(e) {
    var key;
    if (window.event) // IE
    {
        key = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        key = e.which;
    }
    if (key == 13) return true;

    if (key < 48 || key > 57) {
        e.keyCode = 0;
        //alert('Solo se permite ingresar números');
        return false;
    }
    return true;
}

//@006 I
function fc_RefreshJQGrid_Responsive(IdTabla) {
    if (IdTabla.indexOf("#") < 0) IdTabla = ("#" + IdTabla);
    var $grid = $(IdTabla),
    newWidth = $("#gbox_" + IdTabla.replace("#", "")).parent().width();
    $grid.jqGrid("setGridWidth", newWidth);
}
//@006 F

function fc_ValidarFecha(e) {
    var t = e,
        r = 1900,
        o = 2100,
        a = "",
        n = "",
        i = "",
        l = 0,
        s = t.substr(6, 4) + "" + t.substr(3, 2) + t.substr(0, 2);
    if ("" == s.trim()) return !1;
    for (var c = "1234567890", d = 0; d < s.length; d++)
        if (-1 == c.indexOf(s.charAt(d))) return !1;
    if (a = t.substr(6, 4).trim(), n = t.substr(3, 2).trim(), i = t.substr(0, 2).trim(), 1 == n || 3 == n || 5 == n || 7 == n || 8 == n || 10 == n || 12 == n) (isNaN(i) || parseInt(i, 10) < 1 || parseInt(i, 10) > 31) && (l = 1);
    else if (4 == n || 6 == n || 9 == n || 11 == n) (isNaN(i) || parseInt(i, 10) < 1 || parseInt(i, 10) > 30) && (l = 1);
    else {
        var u = a % 4 != 0 || a % 100 == 0 && a % 400 != 0 ? 28 : 29;
        (isNaN(i) || parseInt(i, 10) < 1 || parseInt(i, 10) > u) && (l = 1)
    }
    return 1 == l ? !1 : parseFloat(n) > 12 ? !1 : parseFloat(a) < r || parseFloat(a) > o ? !1 : !0
}

function fc_ValidarRangofechas(e, t) {
    var r = e.substr(6, 4) + "" + e.substr(3, 2) + e.substr(0, 2),
        o = t.substr(6, 4) + "" + t.substr(3, 2) + t.substr(0, 2);
    return r > o ? !1 : !0
}

function fc_ClearGridView(e, t) {
    null != document.getElementById(e) && (1 == t ? $("#" + e).remove() : $("#" + e + " tr:not(:first-child)").remove())
}

//@012 I
function fc_FormatFechaBS(idTextBox, DatePicker_Opciones) {
    if (idTextBox.indexOf("#") < 0) idTextBox = "#" + idTextBox;
    var idDpkDiv = "dpk" + idTextBox.replace("#", "").replace("txt", "");
    if (fc_ExistDisplayControl(idDpkDiv) == false) {
        $(idTextBox).wrap("<div class='input-group date' id=" + idDpkDiv + " style='max-width:140px;'></div>");
        $(idTextBox).after("<span class='input-group-addon add-on' title='Ver Calendario'><i class='fa fa-calendar'></i></span>");
    }

    $("#" + idDpkDiv).bootstrapDatePicker({
        format: "dd/mm/yyyy",
        language: "es",
        todayHighlight: true,
        todayBtn: true,
        autoclose: true
        , startDate: null //"-1d"
        , endDate: null //"+2d"
        , showOnFocus: false
        , weekStart: 0
    });
}

function fc_FormatFechaRangoBS(idDivFechaRango, DatePicker_Opciones) {
    if (idDivFechaRango.indexOf("#") < 0) idDivFechaRango = "#" + idDivFechaRango;
    $(idDivFechaRango).addClass("input-group input-daterange");

    $(idDivFechaRango).bootstrapDatePicker({
        format: "dd/mm/yyyy",
        language: "es",
        todayHighlight: true,
        todayBtn: true,
        autoclose: true
        , weekStart: 0
    });
}
//@012 F

function fc_FormatFecha(e, t, r, o) {
    e.indexOf("#") < 0 && (e = "#" + e), $.datepicker.regional.es = {
        closeText: "Cerrar",
        prevText: "Previo",
        nextText: "Próximo",
        currentText: "Hoy",
        monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        monthNamesShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
        monthStatus: "Ver otro mes",
        yearStatus: "Ver otro año",
        dayNames: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
        dayNamesShort: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sáb"],
        dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
        dateFormat: "dd/mm/yy",
        firstDay: 0,
        initStatus: "Selecciona la fecha",
        isRTL: !1
    }, $.datepicker.setDefaults($.datepicker.regional.es);
    var a = e;
    $(a).width(70), $(a).attr("maxlength", "10"), 0 == t.fl_enabled_textbox && $(a).prop("disabled", !0), $(a).datepicker({
        showButtonPanel: !0,
        showOn: "button",
        buttonImage: /*"img/calendario.gif"*/"../Images/iconos/calendario.gif" /*@001 I/F*/,
        buttonImageOnly: !0,
        buttonText: "Click para mostrar el Calendario",
        changeMonth: t.fl_changeMonth,
        changeYear: t.fl_changeYear,
        minDate: t.minDate,
        maxDate: t.maxDate,
        onClose: function (e) {
            "" != r && null != r && void 0 != r && $("#" + o).datepicker("option", "MIN" == r ? "minDate" : "maxDate", e)
        }
        //@007 I
        , beforeShow: function (e) {
            setTimeout(function () {
                var txt = $("select.ui-datepicker-year option:first").text();
                $("select.ui-datepicker-year option:first").text(txt + " ▲");
                txt = $("select.ui-datepicker-year option:last").text();
                $("select.ui-datepicker-year option:last").text(txt + " ▼");
            }, 1);
        }
        , onChangeMonthYear: function () {
            setTimeout(function () {
                var txt = $("select.ui-datepicker-year option:first").text();
                $("select.ui-datepicker-year option:first").text(txt + " ▲");
                txt = $("select.ui-datepicker-year option:last").text();
                $("select.ui-datepicker-year option:last").text(txt + " ▼");
            }, 1);
        }
        //@007 F
    })
}

function fc_FormatNumeros(e) {
    e.indexOf("#") < 0 && (e = "#" + e), $(e).keypress(function (e) {
        return fc_SoloNumeros(e)
    })
}

//@006 I
function fc_FormatImporte(e) {
    var t = e.replace("#", ""),
        r = document.getElementById(t);
    e.indexOf("#") < 0 && (e = "#" + e), $(e).keypress(function (e) {
        return fc_KeyPressTxtImporte(e, r, mascara_importe)
    }), $(e).bind("paste", function () {
        return !1
    }), $(e).focus(function (e) {
        return fc_OnFocusTxtImporte(e, r, mascara_importe)
    }), $(e).blur(function (e) {
        return fc_OnBlurTxtImporte(e, r, mascara_importe)
    })
}

function fc_KeyPressTxtImporte(e, t, r) {
    r = fc_Replace(r, chr_miles, "");
    var o, a = r.split(chr_decimales);
    if (o = "." == chr_decimales ? 46 : 44, null == e && (e = window.event), null != e) {
        var n = 0;
        if (n = e.which ? e.which : e.keyCode, 35 == n || 36 == n || 37 == n || 38 == n || 39 == n || 40 == n || n == o || 45 == n || 9 == n || 8 == n) return !0;
        if (t.value.length >= r.length) return !1;
        if (n != o && (48 > n || n > 57)) return !1;
        if (n == o && (a.length > 1 && t.value.indexOf(chr_decimales) > -1 || a.length <= 1)) return !1
    }
    return !0
}

function fc_OnFocusTxtImporte(e, t, r) {
    return t.value = fc_Replace(t.value, chr_miles, ""), !0
}

function fc_OnBlurTxtImporte(e, t, r) {
    var o;
    o = "." == chr_decimales ? /^[\- \d ,.]+$/i : /^[\- \d .,]+$/i;
    var a = /^\d+$/i;
    if (t.value = fc_Replace(t.value, chr_miles, ""), "" != fc_Trim(t.value)) {
        var n = r.split(chr_decimales);
        if (n.length > 1) {
            if (!fc_Trim(t.value).match(o)) return fc_Alert("El valor ingresado" + mstrFormatoIncorrecto + "El formato correcto es: " + r), t.value = "", t.focus(), !1
        } else if (!fc_Trim(t.value).match(a)) return fc_Alert("El valor ingresado" + mstrFormatoIncorrecto + "El formato correcto es: " + r), t.value = "", t.focus(), !1;
        var i = "";
        n.length > 1 ? (isNaN(t.value.replace(",", ".")) && (t.value = 0), t.value = roundNumber(t.value, n[1].length)) : isNaN(t.value.replace(",", ".")) && (t.value = 0), t.value = t.value, arrTextBox = t.value.split(chr_decimales);
        for (var l = arrTextBox[0].split(""), s = l.length - 1; s >= 0; s--) i += l[s], l.length - s != 0 && 0 != s && (l.length - s) % 3 == 0 && (i += chr_miles);
        l = i.split(""), i = "";
        for (var s = l.length - 1; s >= 0; s--) i += l[s];
        arrTextBox.length > 1 && (i = i + chr_decimales + arrTextBox[1]), t.value = i
    }
}

function roundNumber(e, t) {
    var r;
    if (t = Number(t), 1 > t) r = Math.round(e).toString();
    else {
        var o = e.toString(); -1 == o.lastIndexOf(chr_decimales) && (o += chr_decimales);
        var a = o.lastIndexOf(chr_decimales) + t,
            n = Number(o.substring(a, a + 1)),
            i = Number(o.substring(a + 1, a + 2));
        if (i >= 5) {
            if (9 == n && a > 0)
                for (; a > 0 && (9 == n || isNaN(n)); ) n != chr_decimales ? (a -= 1, n = Number(o.substring(a, a + 1))) : a -= 1;
            n += 1
        }
        r = o.substring(0, a) + n.toString()
    } -1 == r.lastIndexOf(chr_decimales) && (r += chr_decimales);
    for (var l = r.substring(r.lastIndexOf(chr_decimales) + 1).length, s = 0; t - l > s; s++) r += "0";
    return r
}
//@006 F
function fc_GetJQGrid_Local(e, t, r, o, a, n, i, l, s, b) {

    try {
        var c = e.replace("#", "");
        null != document.getElementById("gbox_" + c) && $(e).jqGrid("GridUnload"), e.indexOf("#") < 0 && (e = "#" + e), t.indexOf("#") < 0 && (t = "#" + t), 0 == a.fl_paginar && (a.pageSize = n.length);
        var d = n,
            u = $(e);
        numberTemplate = {
            formatter: "number",
            align: "right",
            sorttype: "number"
        }, u.jqGrid({
            datatype: "local",
            data: d,
            colNames: r,
            colModel: o,
            pager: t,
            rownumbers: !1,
            loadtext: "Cargando datos...",
            loadonce: !0,
            viewrecords: !0,
            recordtext: "{0} - {1} de {2} registros",
            emptyrecords: "No existen resultados",
            pgtext: "Pág: {0} de {1}",
            rowNum: a.pageSize,
            height: a.height,
            scroll: a.fl_paginar_scroll,
            pgbuttons: a.fl_paginar,
            pginput: a.fl_paginar,
            multiboxonly: !0,
            multiselect: a.multiselect,
            multiselectWidth: a.multiselectWidth,
            sortname: 0,
            sortorder: "asc",
            autowidth: !0,
            cellsubmit: a.cellsubmit,
            shrinkToFit: a.shrinkToFit,
            forceFit: !0,
            gridview: !0,
            altRows: a.altRows,
            hoverrows: !1,
            cellEdit: a.cellEdit,


            gridComplete: function () {
                "" != s && void 0 != s && s()
            },
            //@002 I
            /*onSelectRow: function (e) {
            onSelectRow: function (e) {
            
                "" != i && void 0 != i && i(e)
            },*/
            onSelectRow: (typeof (a.onSelectRow) === "undefined") ? function (e) {

                "" != i && void 0 != i && i(e)
            } : a.onSelectRow,
            //@002 F
            beforeSelectRow: function (rowid, event) {

                if (fc_Trim(String(b)) == "undefined") { return true; }
                else {
                    "" != b && void 0 != b && b(rowid, event)
                }
            },
            //@002 I
            /*ondblClickRow: function (e) {

                "" != l && void 0 != l && l(e)
            },*/
            ondblClickRow: (typeof (a.ondblClickRow) === "undefined") ? function (e) {

                "" != l && void 0 != l && l(e);
            } : a.ondblClickRow,
            subGrid: a.subGrid,
            subGridModel: a.subGridModel,
            subGridRowExpanded: a.subGridRowExpanded,
            subGridOptions: a.subGridOptions,
            //@002 I
            onSortCol: function (e, t, r) { },
            onPaging: function (e) { }
        })
    } catch (f) {
        fc_AlertError("CATCH: " + f)
    }
}

function fc_GetJQGrid_Ajax(e, t, r, o, a, n, i, l, s, c) {
    var d = r.replace("#", "");
    null != document.getElementById("gbox_" + d) && $(r).jqGrid("GridUnload"), r.indexOf("#") < 0 && (r = "#" + r), o.indexOf("#") < 0 && (o = "#" + o), 0 == i.fl_paginar && (i.pageSize = 1e4), $(r).jqGrid({
        datatype: function () {
            $.ajax({
                type: "POST",
                data: "{'strFiltros':" + JSON.stringify(e) + ",'pPageSize':'" + $(r).getGridParam("rowNum") + "','pCurrentPage':'" + $(r).getGridParam("page") + "','pSortColumn':'" + $(r).getGridParam("sortname") + "','pSortOrder':'" + $(r).getGridParam("sortorder") + "'}",
                dataType: "json",
                url: t,
                contentType: "application/json; charset=utf-8",
                async: !0,
                beforeSend: function () {
                    fc_show_progress(!0)
                },
                complete: function () {
                    fc_show_progress(!1)
                },
                success: function (e, t) {
                    "success" == t ? $(r)[0].addJSONData(JSON.parse(e.d)) : fc_AlertError(JSON.parse(e.responseText).Message)
                },
                error: function (e, t, r) {
                    fc_errorAjax(e, t, r)
                }
            })
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: !0,
            cell: "Row",
            id: "ID"
        },
        colNames: a,
        colModel: n,
        pager: o,
        rownumbers: !1,
        loadtext: "Cargando datos...",
        viewrecords: !0,
        recordtext: "{0} - {1} de {2} registros",
        emptyrecords: "No existen resultados",
        pgtext: "Pág: {0} de {1}",
        rowNum: i.pageSize,
        height: i.height,
        scroll: i.fl_paginar_scroll,
        pgbuttons: i.fl_paginar,
        pginput: i.fl_paginar,
        multiboxonly: !0,
        /*@004 I*/
        /*multiselect: i.fl_multiselect,*/
        multiselect: i.multiselect,
        multiselectWidth: i.multiselectWidth,
        /*@004 F*/
        sortname: 0,
        sortorder: "asc",
        autowidth: !0,
        shrinkToFit: !1,
        forceFit: !0,
        gridview: !0,
        altRows: i.altRows,
        hoverrows: !1,
        gridComplete: function () {
            "" != c && void 0 != c && c()
        },
        onSelectRow: function (e) {
            "" != l && void 0 != l && l(e)
        },
        ondblClickRow: function (e) {
            "" != s && void 0 != s && s(e)
        }
    }).navGrid(o, {
        edit: !1,
        add: !1,
        search: !1,
        del: !1,
        refresh: !1
    })
}

function fc_Modal(e, t, r) {
    e.indexOf("#") < 0 && (e = "#" + e), $(function () {
        $(e).dialog({
            autoOpen: !1,
            modal: t,
            width: "auto",
            height: "auto",
            close: function () {
                r()
            },
            open: function () { }
        })
    })
}

function fc_Alert(e) {
    //@006 I
    $("body").append("<div id='dialog-message'></div>"), e = e.replace(/\n/g, "<br>"), e = "<div style='margin-top: 10px;'><img src='../bootstrap/img/exclamation.png' style='float: left;margin-right:5px;' /><p style='text-align:justify;'>" + e + "</p></div>", $("#dialog-message").html(e), $("#dialog-message").dialog({
    //@006 F
        resizable: !1,
        modal: !0,
        title: "Sistema de Reserva de Citas",
        closeOnEscape: !1,
        buttons: {
            Aceptar: function () {
                $(this).dialog("close").remove()
            }
        },
        open: function (e, t) {
            $(this).parent().children().children(".ui-dialog-titlebar-close").hide(), $(".ui-dialog :button").blur()
        }
    })
}

//@006 I
function fc_AlertError(e) {
    $("body").append("<div id='dialog-message'></div>"), e = e.replace(/\n/g, "<br>"), e = "<div style='margin-top: 10px;'><img src='../bootstrap/img/error.png' style='float: left;margin-right:5px;' /><p style='text-align:justify;'>" + e + "</p></div>", $("#dialog-message").html(e), $("#dialog-message").dialog({
        resizable: !1,
        modal: !0,
        title: "Sistema de Reserva de Citas",
        closeOnEscape: !1,
        buttons: {
            Aceptar: function () {
                $(this).dialog("close").remove()
            }
        },
        open: function (e, t) {
            $(this).parent().children().children(".ui-dialog-titlebar-close").hide(), $(".ui-dialog :button").blur()
        }
    })
}
function fc_AlertSuccess(e) {
    $("body").append("<div id='dialog-message'></div>"), e = e.replace(/\n/g, "<br>"), e = "<div style='margin-top: 10px;'><img src='../bootstrap/img/success.png' style='float: left;margin-right:5px;' /><p style='text-align:justify;'>" + e + "</p></div>", $("#dialog-message").html(e), $("#dialog-message").dialog({
        resizable: !1,
        modal: !0,
        title: "Sistema de Reserva de Citas",
        closeOnEscape: !1,
        buttons: {
            Aceptar: function () {
                $(this).dialog("close").remove()
            }
        },
        open: function (e, t) {
            $(this).parent().children().children(".ui-dialog-titlebar-close").hide(), $(".ui-dialog :button").blur()
        }
    })
}
//@006 F

function fc_Confirm(e, t) {
    $("body").append("<div id='dialog-confirm'></div>"), e = e.replace(/\n/g, "<br>"), e = "<div style='margin-top: 10px;'><img src='../bootstrap/img/question.png' style='float: left;margin-right:5px;' /><p style='text-align:justify;'>" + e + "</p></div>", $("#dialog-confirm").html(e), $("#dialog-confirm").dialog({
        resizable: !1,
        modal: !0,
        title: "Sistema de Reserva de Citas",
        closeOnEscape: !1,
        buttons: {
            Aceptar: function () {
                $(this).dialog("close").remove(), t(!0)
            },
            Cancelar: function () {
                $(this).dialog("close").remove(), t(!1)
            }
        },
        open: function (e, t) {
            $(this).parent().children().children(".ui-dialog-titlebar-close").hide(), $(".ui-dialog :button").blur()
        }
    })
}

function fc_Hide_Check_All(e) {
    var t = $(e);
    $("#cb_" + t[0].id).hide()
}

function fc_CrearTreeView(e, t, r, o, a) {
    $("#" + e).jstree("destroy"), $(function () {
        $("#" + e).jstree({
            plugins: ["", t],
            core: {
                multiple: r,
                data: o
            }
        }).on("changed.jstree", function (e, t) {
            var r, o, n = [];
            for (r = 0, o = t.selected.length; o > r; r++) n.push(t.instance.get_node(t.selected[r]).id);
            a(n.join(", "))
        }).jstree()
    })
}

function fc_FillCombo(e, t, r) {
    e.indexOf("#") < 0 && (e = "#" + e), $(e).html(""), 0 == t.length ? $(e).append("<option value=''>" + r + "</option>") : "" != r && $(e).append("<option value=''>" + r + "</option>"), $.each(t, function (t, r) {
        $(e).append("<option value='" + r.value + "'>" + r.nombre + "</option>")
    })
}

function fc_FillComboSelectpicker(e, t, r) {
    $(e).html('');
    e.indexOf("#") < 0 && (e = "#" + e), $(e).html(""), 0 == t.length ? $(e).append("<option value=''>" + r + "</option>") : "" != r && $(e).append("<option value=''>" + r + "</option>"), $.each(t, function (t, r) {
        $(e).append("<option value='" + r.value + "'>" + r.nombre + "</option>")
    })
    $(e).selectpicker('refresh');
}

function fc_getJsonAjax(jsonParametros, jsonUrl, jsonResponse, jsonAsync) {

    if (jsonAsync == 'undefined') {
        jsonAsync = true;
    }

    fc_show_progress(!0), $.ajax({
        type: "POST",
        data: jsonParametros,
        dataType: "json",
        url: jsonUrl,
        contentType: "application/json; charset=utf-8",
        async: jsonAsync,
        beforeSend: function () { },
        complete: function () {
            fc_show_progress(!1)
        },
        success: function (jsonParametros, jsonUrl) {
            "success" == jsonUrl ? jsonResponse(JSON.parse(jsonParametros.d)) : fc_AlertError("ERROR SUCCESS: " + JSON.parse(jsondata.responseText).Message)
        },
        error: function (jsonParametros, jsonUrl, jsonResponse) {
            fc_errorAjax(jsonParametros, jsonUrl, jsonResponse)
        }
    })
}


// This is the function.
String.prototype.format = function () {
    var content = this;
    for (var i = 0; i < arguments.length; i++) {
        var replacement = '{' + i + '}';
        content = content.replace(replacement, arguments[i]);
    }
    return content;
};

function fc_Msj_Notify(strMensaje, strTipo) {

    var strImage;

    switch (strTipo) {
        case '1':
            strImage = "../Bootstrap/img/plugins/gritter/success.png";
            break;
        case '2':
            strImage = "../Bootstrap/img/plugins/gritter/warning.png";
            break;
        case '3':
            strImage = "../Bootstrap/img/plugins/gritter/information.png";
            break;
        default:
            strImage = "../Bootstrap/img/plugins/gritter/error.png";
    }
    /*@004 I*/
    $('.gritter-item-wrapper').remove();
    /*@004 F*/
    $.gritter.add({
        title: 'Sistema de Reserva de Citas',
        text: strMensaje,
        image: strImage,
        sticky: false,
        time: ''
    });
}


function fc_CalculoDiaNombreSemana(fecha) {
    fecha = fecha.split('/');
    if (fecha.length != 3) {
        return null;
    }
    //Vector para calcular día de la semana de un año regular.  
    var regular = [0, 3, 3, 6, 1, 4, 6, 2, 5, 0, 3, 5];
    //Vector para calcular día de la semana de un año bisiesto.  
    var bisiesto = [0, 3, 4, 0, 2, 5, 0, 3, 6, 1, 4, 6];
    //Vector para hacer la traducción de resultado en día de la semana.  
    var semana = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];
    //Día especificado en la fecha recibida por parametro.  
    var dia = fecha[0];
    //Módulo acumulado del mes especificado en la fecha recibida por parametro.  
    var mes = fecha[1] - 1;
    //Año especificado por la fecha recibida por parametros.  
    var anno = fecha[2];
    //Comparación para saber si el año recibido es bisiesto.  
    if ((anno % 4 == 0) && !(anno % 100 == 0 && anno % 400 != 0))
        mes = bisiesto[mes];
    else
        mes = regular[mes];
    //Se retorna el resultado del calculo del día de la semana.  
    return semana[Math.ceil(Math.ceil(Math.ceil((anno - 1) % 7) + Math.ceil((Math.floor((anno - 1) / 4) - Math.floor((3 * (Math.floor((anno - 1) / 100) + 1)) / 4)) % 7) + mes + dia % 7) % 7)];
}

function fc_errorAjax(e, t, r) {
    //if (401 == e.status) alert("Acceso no Autorizado: " + r), location.reload();
    if (401 == e.status) location.reload();
    else {
        var o = e.responseText.indexOf("<title>") + 7,
            a = e.responseText.indexOf("</title>") - o,
            n = e.responseText.substr(o, a);
        if ("" == n) try {
            n = jQuery.parseJSON(e.responseText).Message
        } catch (i) { }
        fc_AlertError("Error: (" + e.status + "): " + n)
    }
}

function fc_show_progress(e) {
    e ? $("#divProgress").show() : $("#divProgress").hide()
}

function fc_PressKey(e, t) {
    event.keyCode == e && document.getElementById(t).click()
}

function fc_UploadFile(e, t, r, o) {
    if (-1 != navigator.appName.indexOf("Explorer") && -1 == navigator.appVersion.indexOf("MSIE 1")) return void fc_Alert("- Debe usar Internet Explorer superior a 9 o otro navegador.");
    for (var a = document.getElementById(e), n = a.files, i = 1, l = "", s = new Array(".gif", ".jpg", ".png", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt", ".pdf"), c = document.getElementById(e).value, d = c.substring(c.lastIndexOf(".")).toLowerCase(), u = !1, f = 0; f < s.length; f++)
        if (s[f] == d) {
            u = !0;
            break
        }
    if (!u) return fc_Alert("Comprueba la extensión de los archivos a subir. \n  Sólo se pueden subir archivos con extensiones: " + s.join() + "\n"), !1;
    var p = 0;
    u = !1;
    for (var f = 0; f < n.length; f++)
        if (p = n[f].size / 1024, p > r) {
            u = !0;
            break
        }
    if (p = parseInt(p), u) return fc_Alert("El tamaño del archivo no puede ser mayor a " + r.toString() + " KB"), !1;
    fc_show_progress(!0);
    try {
        for (var f = 0; f < n.length; f++) {
            l = "0";
            var m = new XMLHttpRequest,
                g = new FormData;
            g.append("file", n[f]), g.append("name", ""), g.append("ruta", t), m.open("POST", "../FileUpload.ashx/ProcessRequest", !0), m.onload = function (e) {
                if (200 == this.status) {
                    var t = this.responseText.split("|");
                    o(t, p), fc_show_progress(!1)
                } else fc_Alert(e)
            }, m.send(g), i += 1
        }
        document.getElementById(e).value = null
    } catch (h) {
        fc_AlertError(h)
    }
}

function fc_Trim(e) {
    var t, r = e;
    for (t = 0; t < r.length && " " == r.charAt(t) ;) r = r.substring(t + 1, r.length);
    for (t = r.length - 1; t >= 0 && " " == r.charAt(t) ; t = r.length - 1) r = r.substring(0, t);
    return r
}




function fc_Replace(e, t, r) {
    return e.split(t).join(r)
}
var DatePicker_Opciones_Default = {
    fl_changeMonth: !1,
    fl_changeYear: !1,
    minDate: "",
    maxDate: "",
    fl_enabled_textbox: !0
},
    chr_miles = ",", 
    chr_decimales = ".", 
    mascara_importe = "999,999,999.99", 
    mstrFormatoIncorrecto = " no tiene el formato correcto.\n",
    JQGrid_Opciones_Default = {
        pageSize: 10,
        height: "auto",
        fl_paginar: !0,
        fl_paginar_scroll: !1,
        fl_multiselect: !1,
        altRows: !0
        //@002 I
        , subGrid: false
        , subGridModel: []
        , subGridRowExpanded: function () { }
        , subGridOptions: {}
        //@002 F
    },
    JQGrid_Util = new Object;
JQGrid_Util.GetTabla_Local = function (e, t, r, o, a, n, i, l, s, b) {
    fc_GetJQGrid_Local(e, t, r, o, a, n, i, l, s, b)
}, JQGrid_Util.GetTabla_Ajax = function (e, t, r, o, a, n, i, l, s, c) {
    fc_GetJQGrid_Ajax(e, t, r, o, a, n, i, l, s, c)
}, JQGrid_Util.getIDs = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e);
    var t = $(e).jqGrid("getDataIDs");
    return t
}, JQGrid_Util.getRowData = function (e, t) {
    return e.indexOf("#") < 0 && (e = "#" + e), $(e).getRowData(t)
}, JQGrid_Util.getRowDataSelected = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e);
    //@006 I
    var t = $(e).jqGrid("getGridParam", "selrow");
    if (t == null) return null;
    else return $(e).getRowData(t);
    //@006 F
}, JQGrid_Util.getRowIDSelected = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e);
    var t = $(e).jqGrid("getGridParam", "selrow");
    return t
}, JQGrid_Util.getRowIDsSelected = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e);
    var t = $(e).jqGrid("getGridParam", "selarrrow");
    return t
}, JQGrid_Util.getCountRegistros = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e);
    var t = $(e).jqGrid("getGridParam", "records");
    return t
}, JQGrid_Util.resetSelection = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e), $(e).resetSelection()
}, JQGrid_Util.clearData = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e), $(e).clearGridData(!0)
}, JQGrid_Util.AutoWidthResponsive = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e), $(window).resize(function () {
        gridParentWidth = $("#gbox_" + e.replace("#", "")).parent().width(), $(e).jqGrid("setGridWidth", gridParentWidth)
    })
};
var Modal_Util = new Object;
Modal_Util.Open = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e), $(e).dialog("open")
}, Modal_Util.Close = function (e) {
    e.indexOf("#") < 0 && (e = "#" + e), $(e).dialog("close")
}, Modal_Util.FormatModal = function (e, t, r) {
    fc_Modal(e, t, r)
}, Modal_Util.Alert = function (e) {
    fc_Alert(e)
}, Modal_Util.AlertError = function (e) {
    fc_AlertError(e)
}, Modal_Util.AlertSuccess = function (e) {
    fc_AlertSuccess(e)
}, Modal_Util.Confirm = function (e, t) {
    fc_Confirm(e, t)
};

//@005 I
function fc_getNavigator() {
    var ua = navigator.userAgent, tem,
    M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return 'IE ' + (tem[1] || '');
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
        if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
    return M.join(' ');
};
//@005 F

//@006 I
//#region - Funciones jqFileUpload
function fc_formatFileSize(bytes) {
    if (typeof bytes !== 'number') {
        return '';
    }
    if (bytes >= 1073741824) {
        return (bytes / 1073741824).toFixed(2) + ' GB';
    }
    if (bytes >= 1048576) {
        return (bytes / 1048576).toFixed(2) + ' MB';
    }
    return (bytes / 1024).toFixed(2) + ' KB';
}
var JQFileUpload_Opciones_Default = { maxNumberOfFiles: 1, autoUpload: false, maxFileSize: 5242880 /*5MB*/, acceptFileTypes: undefined /*'jpg|jpeg|jpe|png|gif'*/, paramJson: {} };
var JQFileUpload_Util = new Object();
JQFileUpload_Util.FormatFileUpload = function (IdFileUpload, JQFileUpload_Opciones, fn_Complete) {
    fc_FormatFileUpload(IdFileUpload, IdPieTabla, Cabecera, ModelCol, JQFileUpload_Opciones, objDatos, fn_Click, fn_dblClick, fn_Complete);
};
JQFileUpload_Util.getNomFilesContentFileUpload = function (IdFileUpload) {
    return fc_GetNomFilesContentFileUpload(IdFileUpload);
};
JQFileUpload_Util.cleanFileUpload = function (IdFileUpload) {
    fc_CleanFileUpload(IdFileUpload);
};
JQFileUpload_Util.getNumFilesPendienteProceso = function (IdFileUpload) {
    return fc_GetNumFilesPendienteProceso(IdFileUpload);
};

function fc_FormatFileUpload(IdFileUpload, JQFileUpload_Opciones, fn_Complete) {
    try {
        if (IdFileUpload.indexOf("#") < 0) IdFileUpload = ("#" + IdFileUpload);

        var idContentFileUpload = "cont_" + IdFileUpload.replace('#', '');
        $(IdFileUpload).after("<div id=" + idContentFileUpload + "></div>");
        idContentFileUpload = ("#" + idContentFileUpload);

        JQFileUpload_Opciones.paramJson.maxFileSize = JQFileUpload_Opciones.maxFileSize; //Para validar en el Handler
        JQFileUpload_Opciones.paramJson.acceptFileTypes = JQFileUpload_Opciones.acceptFileTypes; //Para validar en el Handler

        $(IdFileUpload).fileupload({
            url: '../HandlerFileUpload/FileTransferHandler.ashx',
            autoUpload: JQFileUpload_Opciones.autoUpload
            , dataType: 'json'
            //, replaceFileInput: false
            , maxNumberOfFiles: JQFileUpload_Opciones.maxNumberOfFiles
            , nu_files: 0 //no se mueve
            , nu_files_pendiente_proceso: 0 //no se mueve
            , maxFileSize: JQFileUpload_Opciones.maxFileSize //5242880 //5MB = 5242880 Bytes
            , acceptFileTypes: JQFileUpload_Opciones.acceptFileTypes //    /(\.|\/)(jpe?g|png)$/i
            , formData: JQFileUpload_Opciones.paramJson
            , add: function (e, data) {
                var options = $(this).fileupload("option");
                var nu_files = options.nu_files + 1;
                //#region - Validaciones
                if (data.files[0].name.length > 100) { fc_Alert("El nombre del archivo no debe exceder los 100 caracteres."); return; }
                if (nu_files > options.maxNumberOfFiles) { return; }

                if (options.acceptFileTypes != undefined) {
                    var acceptFileTypes = "(\.|\/)(" + options.acceptFileTypes + ")$";
                    var RE_acceptFileTypes = new RegExp(acceptFileTypes, "i");
                    if (!(RE_acceptFileTypes.test(data.files[0].type) ||
                    RE_acceptFileTypes.test(data.files[0].name))) {
                        fc_Alert("Tipo de archivo no permitido (Permitidos: [" + options.acceptFileTypes + "]).");
                        return;
                    }
                }
                var ext_validos = "jpg|jpeg|png|gif|pdf|doc|docx|xls|xlsx|ppt|pptx|txt|rtf|msg";
                ext_validos = "(\.|\/)(" + ext_validos + ")$";
                var RE_acceptFileTypes_validos = new RegExp(ext_validos, "i");
                if (!(RE_acceptFileTypes_validos.test(data.files[0].type) ||
                    RE_acceptFileTypes_validos.test(data.files[0].name))) {
                    fc_Alert("Tipo de archivo no permitido por el sistema.");
                    return;
                }

                if (options.maxFileSize &&
                    data.files[0].size > options.maxFileSize) {
                    fc_Alert("El archivo es demasiado grande. (Peso máximo: " + fc_formatFileSize(options.maxFileSize) + ")");
                    return;
                }
                //#endregion - Validaciones
                $(this).fileupload("option", "nu_files", nu_files);
                var nu_files_pendiente_proceso = options.nu_files_pendiente_proceso + 1;
                $(this).fileupload("option", "nu_files_pendiente_proceso", nu_files_pendiente_proceso);
                if (nu_files >= options.maxNumberOfFiles) { $(IdFileUpload).hide(); }

                //var divProgress = $("<div id=\"progressbar\"></div>").progressbar({ value: 0 }).css("width", "100px");
                var divProgress = $("<img src=../bootstrap/img/progress_snake.gif style='display:none;'></img>");
                var btnSubmit = "";
                if (options.autoUpload == false) {
                    //btnSubmit = $('<button type="button" class="start">Subir</button>')
                    btnSubmit = $('<button type="button" class="btn btn-default" title="Subir"><i class="fa fa-upload" style="color:#10ab10;"></i></button>')
                        //.button({ icons: { primary: "ui-icon-circle-arrow-e" }, text: false })
                        .click(function () {
                            data.submit();
                            $(this).hide();
                            data.context.find("img").show();
                        });
                }
                //var btnCancelar = $('<button type="button" class="cancel">Cancelar</button>')
                var btnCancelar = $('<button type="button" class="btn btn-default" title="Cancelar"><i class="fa fa-ban" style="color:#ff0000;"></i></button>')
                    //.button({ icons: { primary: "ui-icon-cancel" }, text: false })
                    .click(function () {
                        var nu_files = $(IdFileUpload).fileupload("option", "nu_files");
                        nu_files = nu_files - 1;
                        $(IdFileUpload).fileupload("option", "nu_files", nu_files);

                        var nu_files_pendiente_proceso = $(IdFileUpload).fileupload("option", "nu_files_pendiente_proceso");
                        nu_files_pendiente_proceso = nu_files_pendiente_proceso - 1;
                        $(IdFileUpload).fileupload("option", "nu_files_pendiente_proceso", nu_files_pendiente_proceso);

                        data.abort();
                        data.context.fadeOut(function () {
                            $(this).remove();
                            $(IdFileUpload).show();
                        });
                    });

                var ctrlArchivo = $("<label>" + data.files[0].name + "</label>"
                    + "<a></a>"
                    + " <label>" + fc_formatFileSize(data.files[0].size) + "</label>")

                data.context = $('<div/>');
                data.context.append(ctrlArchivo)
                .append(divProgress)
                .append(btnSubmit)
                .append("&nbsp;").append(btnCancelar)
                .append("<div id='line_progress' style='width:0%;background-color:#005cab;height:1px;'></div>")
                .appendTo(idContentFileUpload);

                if (options.autoUpload == true) {
                    data.submit();
                    data.context.find("img").show();
                }
            }
            , progress: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                //data.context.find(".ui-progressbar-value").css('width', progress + '%').css("display", "block");
                data.context.find("#line_progress").css('width', progress + '%');
            }
            , fail: function (e, data) {
                if (data.jqXHR) {
                    //@011 I
                    if (data.jqXHR.responseText.indexOf("Logeo.aspx?ReturnUrl") > 0) {
                        location.reload();
                        return;
                    }
                    //@011 F
                    fc_AlertError('Server-error:\n\n' + data.jqXHR.responseText);
                }
            }
            , done: function (e, data) {
                $.each(data.result, function (index, file) {

                    var nu_files_pendiente_proceso = $(IdFileUpload).fileupload("option", "nu_files_pendiente_proceso");
                    nu_files_pendiente_proceso = nu_files_pendiente_proceso - 1;
                    $(IdFileUpload).fileupload("option", "nu_files_pendiente_proceso", nu_files_pendiente_proceso);

                    if (file.error != null && file.error != "") {
                        fc_AlertError(file.error);
                        var nu_files = $(IdFileUpload).fileupload("option", "nu_files");
                        nu_files = nu_files - 1;
                        $(IdFileUpload).fileupload("option", "nu_files", nu_files);

                        data.context.fadeOut(function () {
                            $(this).remove();
                            $(IdFileUpload).show();
                        });
                    }
                    else {
                        //data.context.find(".ui-progressbar-value").css('width', 100 + '%').css("display", "block");
                        data.context.find("#line_progress").css('width', '100%');

                        //var btnEliminar = $('<button type="button" class="delete">Eliminar</button>')
                        var btnEliminar = $('<button type="button" class="btn btn-default" title="Eliminar"><i class="fa fa-trash-o" style="color:#696969;"></i></button>')
                            //.button({ icons: { primary: "ui-icon-trash" }, text: false })
                            .attr("data-type", file.delete_type).attr("data-url", file.delete_url)
                            .click(function () {
                                var req = $.ajax({
                                    dataType: 'json',
                                    url: $(this).attr('data-url'),
                                    type: 'POST'
                                    , data: { 'data-type': $(this).attr('data-type') }
                                    , complete: function () {
                                        var nu_files = $(IdFileUpload).fileupload("option", "nu_files");
                                        nu_files = nu_files - 1;
                                        $(IdFileUpload).fileupload("option", "nu_files", nu_files);

                                        data.context.fadeOut(function () {
                                            $(this).remove();
                                            $(IdFileUpload).show();
                                        });
                                    }
                                });
                            });

                        data.context.find("img").fadeOut(function () {
                            data.context.find("#line_progress").remove();
                            data.context.find("label").first().hide();
                            data.context.find("a").text(file.name).attr("href", file.url).attr("target", "_blank");
                            //data.context.find("button.cancel").hide();
                            data.context.find("button[title='Cancelar']").hide();
                            data.context.append(btnEliminar);
                        });

                        if (fn_Complete != null && fn_Complete != undefined) {
                            fn_Complete(IdFileUpload, file.name, file.url, file.size, file.name_ori);
                        }
                    }
                });
            }
        });
    } catch (ex) {
        fc_AlertError("CATCH: " + ex);
    }
}
function fc_GetNomFilesContentFileUpload(IdFileUpload) {
    if (IdFileUpload.indexOf("#") < 0) IdFileUpload = ("#" + IdFileUpload);

    var IdContentFileUpload = "cont_" + IdFileUpload.replace('#', '');
    IdContentFileUpload = ("#" + IdContentFileUpload);

    var nomfiles = "";
    var delimitador = "";
    $(IdContentFileUpload).find("a").each(function (index) {
        if (index > 0) delimitador = "|";
        nomfiles += (delimitador + $(this).text());
    });

    return nomfiles;
}
function fc_CleanFileUpload(IdFileUpload) {
    if (IdFileUpload.indexOf("#") < 0) IdFileUpload = ("#" + IdFileUpload);

    var IdContentFileUpload = "cont_" + IdFileUpload.replace('#', '');
    IdContentFileUpload = ("#" + IdContentFileUpload);

    $(IdContentFileUpload).empty();
    $(IdFileUpload).show();

    $(IdFileUpload).fileupload("option", "nu_files", 0);
    $(IdFileUpload).fileupload("option", "nu_files_pendiente_proceso", 0);
}
function fc_GetNumFilesPendienteProceso(IdFileUpload) {
    if (IdFileUpload.indexOf("#") < 0) IdFileUpload = ("#" + IdFileUpload);
    return $(IdFileUpload).fileupload("option", "nu_files_pendiente_proceso");
}
//#endregion - Funciones jqFileUpload
//@006 F