<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SRC_ServicioModelo_Bandeja.aspx.cs" Inherits="SRC_Mantenimiento_SRC_ServicioModelo_Bandeja" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckAllA(chk) {
            if (chk.checked) { $("#bandejaAsignados input[type=checkbox]").prop('checked', true); }
            else { $("#bandejaAsignados input[type=checkbox]").prop('checked', false); }
        }

        function CheckAllD(chk) {
            if (chk.checked) { $("#bandejaDisponibles input[type=checkbox]").prop('checked', true); }
            else { $("#bandejaDisponibles input[type=checkbox]").prop('checked', false); }
        }

        function CheckAllB(chk) {
            if (chk.checked) { $("#bandejabuscar input[type=checkbox]").prop('checked', true); }
            else { $("#bandejabuscar input[type=checkbox]").prop('checked', false); }
        }

    </script>

    <style type="text/css">
        th
        {
            cursor: pointer;
        }
        .ctxt
        {
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }
        .cddl
        {
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }
        .TotalRegistros
        {
            color: #555555;
            font-size: 9pt;
            font-weight: bold;
            text-align: left !important;
            padding: 2px;
        }
        .Paginado
        {
            color: #555555;
            font-size: 8pt;
            font-weight: bold;
            text-align: center !important;
            max-height: 15px;
        }
        .images_botones_paginado
        {
            width: 16px;
            cursor: pointer;
            vertical-align: middle !important;
            border-width: 0px;
        }
        .textbox_paginado
        {
            color: #000000;
            font-size: 8pt;
            font-weight: bold;
            border: 1px solid #CCC;
            border-radius: 5px;
            position: relative;
            margin-bottom: 20px;
            margin-left: 0px;
            padding-left: 0px;
            top: 0px;
            left: 0px;
            width: 3%;
            height: 14px;
            text-align: center !important;
        }
        .div.popupnew
        {
            border: 2px solid black;
            opacity: 1;
            filter: alpha(opacity=100);
        }
        .removeOpc
        {
            border: 1px solid rgb(149, 166, 198);
            width: 60px;
            color: rgb(85, 91, 108);
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }
        .text_filtrar
        {
            border: 1px solid rgb(149, 166, 198);
            width: 60px;
            color: rgb(85, 91, 108);
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }
        .text_editar
        {
            border: 1px solid rgb(149, 166, 198);
            width: 130px;
            color: rgb(85, 91, 108);
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }
    </style>
    <table border="0" cellpadding="2" cellspacing="0" width="1000px">
        <tr>
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="98%">
                    <tr>
                        <td valign="bottom" style="width: 238px; padding-right: 3px">
                            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <img alt="" src="../Images/Tabs/tab-izq.gif" />
                                    </td>
                                    <td onmouseout="javascript: return onTabCabeceraOutForm('0');" onmouseover="javascript: return onTabCabeceraOverForm('0');">
                                        SERVICIOS POR MODELO
                                    </td>
                                    <td>
                                        <img alt="" src="../Images/Tabs/tab-der.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" valign="bottom">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <img id="btnBuscar" title="Buscar" style="cursor: pointer" alt="" src="../Images/iconos/b-buscar.gif"
                                            onmouseover="javascript:this.src='../Images/iconos/b-buscar.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"
                                            onclick="fc_Buscar();" />
                                        <img id="btnAgregar" title="Agregar" style="cursor: pointer" alt="" src="../Images/iconos/b-nuevo.gif"
                                            onmouseover="javascript:this.src='../Images/iconos/b-nuevo.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"
                                            onclick="fc_agregar();" />
                                        <img id="bnEliminar" title="Eliminar" style="cursor: pointer" alt="" src="../Images/iconos/b-eliminar.gif"
                                            onmouseover="javascript:this.src='../Images/iconos/b-eliminar.gif'" onmouseout="javascript:this.src='../Images/iconos/b-eliminar.gif'"
                                            onclick="fc_eliminar();" />
                                        <img id="btnLimpiar" title="Limpiar" style="cursor: pointer;" alt="" src="../Images/iconos/b-limpiar.gif"
                                            onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'"
                                            onclick="fc_Limpiar();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div style="width: 997px; height: 465px;">
                    <table cellpadding="0" cellspacing="0" width="980px" border="0">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr style="height: 435px">
                            <td class="c1" style="background-color: #ffffff;" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="950px" style="margin-left: 5px;
                                    margin-right: 5px">
                                    <tr>
                                        <td>
                                            <span style="font-family: Arial; color: #3b83cb; font-size: 11px; font-weight: bold;">
                                                CRITERIOS DE BÚSQUEDA</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" style="width: 950px" cellspacing="1" cellpadding="2" class="cbusqueda">
                                                <tr>
                                                    <td>
                                                        Marca
                                                    </td>
                                                    <td>
                                                        <select id="cboMarca" class="cddl b" style="width: 150px" />
                                                    </td>
                                                    <td>
                                                        Modelo
                                                    </td>
                                                    <td>
                                                        <select id="cboModelo" class="cddl b" style="width: 150px" />
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="0" cellspacing="0" width="100%" style="height: 0px">
                                                <tr>
                                                    <td>
                                                        <img src="../Images/iconos/fbusqueda.gif" width="100%" alt="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td valign="top" style="padding-top: 8px;">
                                            <div id="bandejabuscar" style="width: 997px; height: auto; max-height: 320px;">
                                                <table style="border-color: White; border-width: 0px; width: 950px; border-collapse: collapse;"
                                                    border="0" rules="all" cellspacing="0" cellpadding="0" id="gvServicios">
                                                    <thead>
                                                        <tr class="CabeceraGrilla">
                                                            <th align="center" style="width: 1%;" scope="col">
                                                                <input type="checkbox" onclick="CheckAllB(this)" id="ckHeader" />
                                                            </th>
                                                            <th style="width: 10%;" onclick="fc_Buscar('1','nu_item','D')" scope="col">
                                                                Item
                                                            </th>
                                                            <th style="width: 45%;" onclick="fc_Buscar('1','no_marca','D')" scope="col">
                                                                Marca
                                                            </th>
                                                            <th style="width: 44%;" onclick="fc_Buscar('1','no_modelo','D')" scope="col">
                                                                Modelo
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbgvServicios">
                                                        <tr class="textogrilla">
                                                            <td colspan="5" align="center">
                                                                No existen resultados para esta consulta.
                                                            </td>
                                                        </tr>
                                                        <tr class="Footer">
                                                            <td colspan="4" />
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="background-color: #ffffff;">
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img src="../Images/Tabs/borabajo.gif" alt="" style="width: 1005px;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="pnlBackground" class="modalBackground" style="left: 0px; top: 0px; display: none;
        position: fixed; z-index: 10000;">
    </div>
    <div id="pnlFiltro" style="background-repeat: repeat; border-width: 2px; background-image: url(../Images/fondo.gif);
        display: none; width: 940px; height: 520px; left: 170.5px !important; position: fixed;
        z-index: 99; padding-bottom: 10px;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-top: 10px; margin-left: 10px;">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; margin-left: 0px;">
                        <tr>
                            <td align="left" width="100%">
                                <img src="../Images/cabecera/cabecera_popup.png" width="928px" alt="" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" class="TablaIconosPopupVerContactos">
                        <tr>
                            <td align="right">
                                <img src="../Images/iconos/loading.gif" alt="" id="imgLoading1" style="visibility: hidden;" />
                                <img id="btnCerrar" title="Cerrar" style="cursor: pointer; padding-right: 7px;" alt=""
                                    src="../Images/iconos/b-cerrar.gif" onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'" onclick="fc_ClosePnl('pnlFiltro');fc_Limpiar_Popup();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="display: none">
                            </td>
                            <td>
                                <table id="Table2" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <img id="img3" alt="" src="../Images/Tabs/tab-izq.gif" />
                                        </td>
                                        <td id="tabCab1110" class="TabCabeceraOn">
                                            ASIGNACIÓN DE SERVICIOS POR MODELO
                                        </td>
                                        <td>
                                            <img id="img4" alt="" src="../Images/Tabs/tab-der.gif" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="filtroPopup" width="99%"
                        style="padding-bottom: 10px; padding-top: 10px;">
                        <tr>
                            <td class="td_etiquetaPopupVC" style="padding-left: 40px;">
                                Marca
                            </td>
                            <td class="td_espaciohor">
                                <select id="cboMarcaA" class="cddl b" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_etiquetaPopupVC" style="padding-left: 40px;">
                                Modelo
                            </td>
                            <td class="td_espaciohor">
                                <select id="cboModeloA" class="cddl b" style="width: 150px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="margin-left: 10px;" class="filtroPopup"
            width="97.7%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="filtroPopup" width="99%"
                        style="padding-left: 40px;">
                        <tr>
                            <td>
                                <div id="lblServiciosDisponibles">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="padding-top: 8px;">
                                <div id="headerD">
                                </div>
                                <div id="bandejaDisponibles" style="overflow-y: auto; overflow-x: hidden; width: 380px;
                                    height: 300px;">
                                </div>
                                <div id="footerD">
                                    <table style="border-color: White; border-width: 0px; width: 380px; border-collapse: collapse;"
                                        border="0" rules="all" cellspacing="0" cellpadding="0" id="Table1">
                                        <tfoot>
                                            <tr class="Footer">
                                                <td colspan="4" />
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding-right: 20px;">
                    <table border="0" cellpadding="0" cellspacing="0" class="filtroPopup" width="99%">
                        <tr>
                            <td>
                                <img id="btnAsignar" style="cursor: pointer" alt="" title="Asignar" src="../Images/iconos/sign-right-icon.png"
                                    onclick="fc_Asignar();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img id="btnDesasignar" style="cursor: pointer" alt="" title="Desasignar" src="../Images/iconos/sign-left-icon.png"
                                    onclick="fc_Deasignar();" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="filtroPopup" width="99%">
                        <tr>
                            <td>
                                <div id="lblServiciosAsignados">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="padding-top: 8px;">
                                <div id="headerA">
                                </div>
                                <div id="bandejaAsignados" style="overflow-y: auto; overflow-x: hidden; width: 380px;
                                    height: 300px;">
                                </div>
                                <div id="footerA">
                                    <table style="border-color: White; border-width: 0px; width: 380px; border-collapse: collapse;"
                                        border="0" rules="all" cellspacing="0" cellpadding="0" id="Table3">
                                        <tfoot>
                                            <tr class="Footer">
                                                <td colspan="4" />
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 10px;">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; margin-left: 0px;">
                        <tr>
                            <td align="left" width="100%">
                                <img src="../Images/Tabs/borabajo.gif" alt="" style="width: 918px;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="pnlProgress" style="display: none; border: dimgray 2px solid; width: 150px;
        position: fixed; z-index: 100; height: 40px; padding: 7px; background-color: White;">
        <table>
            <tr>
                <td>
                    <img src="../Images/loading.gif" height="37px" width="46px" alt="Procesando..." />
                </td>
                <td>
                    &nbsp;&nbsp;Procesando...
                </td>
            </tr>
        </table>
    </div>
    <div id="pnlBlocDiv" class="modalBackground" style="left: 0px; top: 0px; display: none;
        position: fixed; z-index: 98; display: none;">
    </div>

    <script type="text/javascript" src="../js/jquery.multiselect.js"></script>

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
        $(document).ready(function() { fc_cargaInicial(); });
        function fc_cargaInicial() {

         
            $("#cboMarca, #cboModelo").append($('<option></option>').val('').html("--Todos--"));
            $("#cboMarcaA, #cboModeloA").append($('<option></option>').val('').html("--Seleccione--"));
            $("#cboMarca").change(function() { fc_select(this.id); });
            $("#cboModelo").change(function() { fc_select(this.id); });
            $("#cboMarcaA").change(function() { fc_select(this.id); });
            $("#cboModeloA").change(function() { fc_select(this.id); });


            var parametros = new Array();
            var usuario = "<%= Profile.Usuario.NID_USUARIO%>";
           
            parametros[0] = usuario;
            sendInfo = { parametros: parametros };
            callAJAX('imgCargandoBandeja', true, 'ObtenerMarcas', sendInfo, function(res) {
                var len = res.d.length;
                for (var i = 0; i < len; i++) {
                    $("#cboMarca").append($("<option></option>").val(res.d[i].codigo).html(res.d[i].nombre));
                    if (len == 1) {
                        $("#cboMarca").val(res.d[i].codigo);
                        fc_select("cboMarca");
                    }
                }
            });

            callAJAX('imgCargandoBandeja', true, 'ObtenerMarcas', sendInfo, function(res) {
                var len = res.d.length;
                for (var i = 0; i < len; i++) {
                    $("#cboMarcaA").append($("<option></option>").val(res.d[i].codigo).html(res.d[i].nombre));
                    if (len == 1) {
                        $("#cboMarcaA").val(res.d[i].codigo);
                        fc_select("cboMarcaA");
                    }
                }
            });



        }
 
    </script>

    <script type="text/javascript">
        function fc_agregar() {
           
            var nid_marca = $("#cboMarca").val();
            var nid_modelo = $("#cboModelo").val();
            $("#cboMarcaA").val(nid_marca);
            fc_cargar_modelo(nid_marca, nid_modelo);
            fc_cargar_bandeja_asignacion(nid_marca, nid_modelo);
            fc_ShowDiv("pnlFiltro");


        }

        function fc_Deasignar() {
           
            var parametros = new Array();
            var seleccion = new Array();
            var idMarca = $("#cboMarcaA").val();
            var idModelo = $("#cboModeloA").val();
            parametros[0] = idMarca;
            parametros[1] = idModelo;
            parametros[2] = "1";

            $('#bandejaAsignados tbody input:checked').each(function() {
                seleccion.push($(this).attr('value'));
            });

            var isValido = true;
            var resultado = "";
            if (idMarca == "") {
                resultado += "Seleccione una marca.\n";
                isValido = false;
            }
            else if (idModelo == "") {
                resultado += "Seleccione un modelo.";
                isValido = false;
            }
            else if (seleccion.length == 0) {

                resultado += "Debe seleccionar al menos un servicio asignado.";
                isValido = false;
            }

            if (!isValido) {
                alert(resultado);

            }
            else {
                sendInfo = { datos: parametros, seleccion: seleccion };

                callAJAX("", true, 'AsignarServicios', sendInfo, function(res) {
                    var data = res.d;
                    if (data == "1") {

                        fc_cargar_bandeja_asignacion(idMarca, idModelo);
                    }
                    else if (data == "0") {
                        alert("Campos obligatorios(*).");
                        return false;
                    }
                    else if (data == "-1") {
                        alert("Error, Consulte con el Administrador.");
                        return false;
                    }
                });
            }
        }

        function fc_Asignar() {
         
            var parametros = new Array();
            var seleccion = new Array();
            var idMarca = $("#cboMarcaA").val();
            var idModelo = $("#cboModeloA").val();
            parametros[0] = idMarca;
            parametros[1] = idModelo;
            parametros[2] = "0";

            $('#bandejaDisponibles tbody input:checked').each(function() {
                seleccion.push($(this).attr('value'));
            });

            var isValido = true;
            var resultado = "";
            if (idMarca == "") {
                resultado += "Seleccione una marca.\n";
                isValido = false;
            }
            else if (idModelo == "") {
                resultado += "Seleccione un modelo.";
                isValido = false;
            }
            else if (seleccion.length == 0) {

                resultado += "Debe seleccionar al menos un servicio disponible.";
                isValido = false;
            }

            if (!isValido) {
                alert(resultado);

            }
            else {
                sendInfo = { datos: parametros, seleccion: seleccion };

                callAJAX("", true, 'AsignarServicios', sendInfo, function(res) {
                    var data = res.d;
                    if (data == "1") {

                        fc_cargar_bandeja_asignacion(idMarca, idModelo);
                    }
                    else if (data == "0") {
                        alert("Campos obligatorios(*).");
                        return false;
                    }
                    else if (data == "-1") {
                        alert("Error, Consulte con el Administrador.");
                        return false;
                    }
                });
            }

        }

        function fc_Limpiar() {
           
            $("#cboMarca").val("");
            $("#cboModelo").val("");
            $("#tbgvServicios").empty();
         

        }

        function fc_Limpiar_Popup() {
            
            $("#cboMarcaA").val("");
            $("#cboModeloA").val("");
            $("#bandejaDisponibles").empty();
            $("#bandejaAsignados").empty();


        }

        function fc_cargar_disponibles(nidMarca, nidModelo, orderby, orderbydirection) {

            if (nidMarca == null)
                nidMarca = $("#cboMarcaA").val();
            if (nidModelo == null)
                nidModelo = $("#cboModeloA").val();
            orderby = orderby || 'nu_item';
            orderbydirection = orderbydirection || 'A';

            sendInfo = { snidMarca: nidMarca, snidModelo: nidModelo, orderby: orderby, orderbydirection: orderbydirection };
            callAJAX("", true, 'ObtenerDisponibles', sendInfo, function(res) {
                var data = res.d;
                if (data[0] == 1) {
                    document.getElementById("headerD").innerHTML = data[1];
                    document.getElementById("bandejaDisponibles").innerHTML = data[2];
                    document.getElementById("lblServiciosDisponibles").innerHTML = data[3];
                }
                else if (data[0] == -1) {
                    alert("Error, Consulte con el Administrador.");
                    return false;
                }
                else if (data[0] == 0) {
                    document.getElementById("headerD").innerHTML = data[1];
                    document.getElementById("bandejaDisponibles").innerHTML = data[2];
                    document.getElementById("lblServiciosDisponibles").innerHTML = data[3];
                }
            });
        }

        function fc_cargar_asignados(nidMarca, nidModelo, orderby, orderbydirection) {

            if (nidMarca == null)
                nidMarca = $("#cboMarcaA").val();
            if (nidModelo == null)
                nidModelo = $("#cboModeloA").val();
            orderby = orderby || 'nu_item';
            orderbydirection = orderbydirection || 'A';
            sendInfo = { snidMarca: nidMarca, snidModelo: nidModelo, orderby: orderby, orderbydirection: orderbydirection };
            callAJAX("", true, 'ObtenerAsignados', sendInfo, function(res) {
                var data = res.d;
                if (data[0] == 1) {
                    document.getElementById("headerA").innerHTML = data[1];
                    document.getElementById("bandejaAsignados").innerHTML = data[2];
                    document.getElementById("lblServiciosAsignados").innerHTML = data[3];
                }
                else if (data[0] == -1) {
                    alert("Error, Consulte con el Administrador.");
                    return false;
                }
                else if (data[0] == 0) {
                    document.getElementById("headerA").innerHTML = data[1];
                    document.getElementById("bandejaAsignados").innerHTML = data[2];
                    document.getElementById("lblServiciosAsignados").innerHTML = data[3];
                }
            });
        }
        function fc_cargar_bandeja_asignacion(nidMarca, nidModelo) {
          
            $("#bandejaDisponibles").empty();
            $("#bandejaAsignados").empty();
            $("#lblServiciosDisponibles").empty();
            $("#lblServiciosAsignados").empty();

            fc_cargar_disponibles(nidMarca, nidModelo);
            fc_cargar_asignados(nidMarca, nidModelo);
        }

        function fc_PnlFiltro() {
            
            var idMarca = $("#cboMarca").val();
            var idModelo = $("#cboModelo").val();
        }
  
        function fc_eliminar() {
            var seleccion = new Array();
            
             $('#bandejabuscar tbody input:checked').each(function() {
                seleccion.push($(this).attr('value'));
            });

            if (seleccion.length == 0) {
                alert("Deberá marca una casilla.");
            }
            else {
                var r = confirm("Ud. Desabilitara los registros seleccionados, ¿Desea continuar?");

                if (r == true) {

                    sendInfo = { parametros: seleccion };
                    callAJAX("", true, 'EliminarServiciosModelo', sendInfo, function(res) {
                        var data = res.d
                        if (data[0] == 1) {
                            fc_Buscar();
                        }
                        else if (data[0] == 0) {
                            fc_Buscar();
                        }
                        else if (data[0] == -1) {
                            alert("Error, Consulte con el Administrador.");
                            return false;
                        }
                          });
                } 
            }
        }
        function fc_Buscar(page, orderby, orderbydirection) {
        
            try {

                var parametros = new Array();

                page = page || 1;
                orderby = orderby || 'nu_item';
                orderbydirection = orderbydirection || 'A';


                parametros[0] = $("#cboMarca").val();
                parametros[1] = $("#cboModelo").val();
                parametros[2] = page;
                parametros[3] = orderby;
                parametros[4] = orderbydirection;

                sendInfo = { parametros: parametros };

                callAJAX("", true, 'ObtenerBandeja', sendInfo, function(res) {
                    var data = res.d
                    if (data[0] == 1) {
                        document.getElementById("bandejabuscar").innerHTML = data[1];
                    }
                    else if (data[0] == 0) {
                        document.getElementById("bandejabuscar").innerHTML = data[1];
                      
                    }
                    else if (data[0] == -1) {
                        alert("Error, Consulte con el Administrador.");
                        return false;
                    }




                });
            }
            catch (e) {
                alert(e);
            }
        }
        function fc_select(id) {
            var parametros = new Array();

            var sendInfo;
            var img = 'imgCargandoBandeja';
            switch (id) {

                case "cboMarca":
                    $("#cboModelo").empty();
                    $("#cboModelo").append($('<option></option>').val('').html("-Todos-"));
                    if ($("#cboMarca").val() == "") {
                        break;
                    }
                    var nidMarca = $("#cboMarca").val();

                    sendInfo = { snidMarca: nidMarca };
                    callAJAX(img, true, 'ObtenerModelos', sendInfo, function(res) {
                        var len = res.d.length;
                        for (var i = 0; i < len; i++) {
                            $("#cboModelo").append($("<option></option>").val(res.d[i].codigo).html(res.d[i].nombre));
                            if (len == 1) {
                                $("#cboModelo").val(res.d[i].codigo);

                            }
                        }
                    });
                    break;

                case "cboMarcaA":

                    var nidMarca = $("#cboMarcaA").val();

                    fc_cargar_modelo(nidMarca);
                    break;

                case "cboModeloA":
                    var nidMarca = $("#cboMarcaA").val();
                    var nidModelo = $("#cboModeloA").val();
                    fc_cargar_bandeja_asignacion(nidMarca, nidModelo);

                    break;
            }

        }

        function fc_cargar_modelo(nidMarca, nidModelo) {

            sendInfo = { snidMarca: nidMarca };
            nidModelo = nidModelo || 0;
            $("#cboModeloA").empty();
            $("#cboModeloA").append($('<option></option>').val('').html("--Seleccione--"));
            if ($("#cboMarcaA").val() == "") {
                return;
            }
            callAJAX("", true, 'ObtenerModelos', sendInfo, function(res) {
                var len = res.d.length;
                for (var i = 0; i < len; i++) {
                    $("#cboModeloA").append($("<option></option>").val(res.d[i].codigo).html(res.d[i].nombre));
                    if (len == 1) {
                        $("#cboModeloA").val(res.d[i].codigo);

                    }
                }
                if (nidModelo != 0)
                    $("#cboModeloA").val(nidModelo);
            });
        }
    </script>

    <script type="text/javascript">

        function callAJAX(img, async, strUrl, strParametros, fn_Callback) {
            $.ajax({
                type: "POST", url: "SGS_ServicioModelo_Bandeja.aspx/" + strUrl, data: JSON.stringify(strParametros), contentType: 'application/json; charset=utf-8', dataType: 'json', async: async,
                beforeSend: function() { fc_DisplayProgress(''); },
                success: function(data, textStatus) { var resultado = data; fn_Callback(resultado); },
                complete: function() { fc_DisplayProgress("none"); },
                error: function(jqXHR, textStatus, errorThrown) {
                    fc_DisplayProgress("none");
                    var errEstado = jqXHR.status;
                    var errMensaje = jqXHR.responseText == "" ? "" : jQuery.parseJSON(jqXHR.responseText).Message;
                    var errTipo = jqXHR.responseText == "" ? "" : jQuery.parseJSON(jqXHR.responseText).ExceptionType;
                    if (errEstado == '401') { location.reload(true); }
                    else if (errEstado == '500') { alert('Error: ' + errTipo + '\nMensaje: ' + errMensaje); }
                }
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
            fc_DisplayProgress('');
        }
        function endReq(sender, args) {
            fc_DisplayProgress('none');

        }
        function fc_DisplayProgress(display) {

            document.getElementById('pnlProgress').style.left = (((screen.width - 350) / 2) + 60) + 'px';
            document.getElementById('pnlProgress').style.top = (screen.height - 150) / 2 + 'px';
            document.getElementById('pnlProgress').style.display = display;
            document.getElementById('pnlBackground').style.width = screen.width + 'px';
            document.getElementById('pnlBackground').style.height = screen.height + 'px';
            document.getElementById('pnlBackground').style.display = display;
        }
        function fc_ClosePnl(pnl) {
            fc_Buscar();
            document.getElementById("pnlBlocDiv").style.display = "none";
            document.getElementById(pnl).style.display = "none";


        }
        function fc_ShowDiv(div) {

            document.getElementById('pnlBlocDiv').style.width = screen.width + 'px';
            document.getElementById('pnlBlocDiv').style.height = screen.height + 'px';
            document.getElementById('pnlBlocDiv').style.display = '';

            document.getElementById(div).style.left = (((screen.width - 1065) / 2) + 60) + 'px';
            document.getElementById(div).style.top = (screen.height - 700) / 2 + 'px';
            document.getElementById(div).style.display = '';
        }

        function selTR(tr, id) {

            var color = "";
            tabla = document.getElementById(id);
            filas = tabla.getElementsByTagName("tr");
            for (i = 0; ele = filas[i]; i++) {

                if ((i % 2) == 0)
                    color = "#E3E7F2";
                else
                    color = "#FFFFFF";
                ele.bgColor = color;
            }
            tr.bgColor = "#C4E4FF";
        }

        function selTRPP(nid_marca, nid_modelo) {

            $("#cboMarcaA").val(nid_marca);
            fc_cargar_modelo(nid_marca, nid_modelo);
            fc_cargar_bandeja_asignacion(nid_marca, nid_modelo);
            fc_ShowDiv("pnlFiltro");

        }



    </script>

</asp:Content>
