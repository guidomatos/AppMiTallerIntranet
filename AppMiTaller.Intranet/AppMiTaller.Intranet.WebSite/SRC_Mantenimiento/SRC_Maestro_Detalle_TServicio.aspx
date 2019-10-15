<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SRC_Maestro_Detalle_TServicio.aspx.cs" Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_TServicio" Theme="Default" Title="Detalle Tipo Servicio" EnableEventValidation="true" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/nicEdit.js" type="text/javascript"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () {
            new nicEditor({ iconsPath: './Scripts/nicEditorIcons.gif', fullPanel: true, maxHeight: 320, uploadURI: './nicUpload.ashx' }).panelInstance('txtContenido');
        });
    </script>
    <script type="text/javascript">
        function CheckAllB(chk) {
            if (chk.checked) { $("#bandejabuscar input[type=checkbox]").prop('checked', true); }
            else { $("#bandejabuscar input[type=checkbox]").prop('checked', false); }
        }
        function Validar_Datos() {

            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var txtNom = document.getElementById('<%=txtNom.ClientID%>');

            if (txtCodigo.value.trim() == "") {
                alert('Ingrese Codigo de Tipo de Servicio.');
                txtCodigo.focus();
                return false;
            }
            if (txtNom.value.trim() == "") {
                alert('Ingrese Nombre de Tipo de Servicio.');
                txtNom.focus();
                return false;
            }

            return true;
        }

    </script>



    <table cellpadding="2" cellspacing="0" width="1000" border="0" style="height: 47px">
        <tr>
            <td>
                <!--INICIO ICONOS DE ACCCION -->
                <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
                    <tr>
                        <td style="width: 100%" align="right">
                            <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/iconos/b-registrofecha.gif"
                                OnClick="btnEditar_Click" onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'"
                                onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'" ToolTip="Editar"
                                Visible="False" />
                            <asp:ImageButton ID="btnGrabar" runat="server" ToolTip="Grabar" ImageUrl="~/Images/iconos/b-guardar.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"
                                OnClick="btnGrabar_Click" OnClientClick="javascript:return Validar_Datos();" />
                            <asp:ImageButton ID="btnRegresar" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'"
                                OnClick="btnRegresar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <!--FIN ICONOS DE ACCCION -->
        </tr>
        <tr>
            <td valign="top">
                <cc1:TabContainer ID="TabDetTServicio" runat="server" CssClass="" ActiveTabIndex="0">
                    <cc1:TabPanel runat="server" ID="Tab1">
                        <HeaderTemplate>
                            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" style="height: 23px; vertical-align: bottom;" />
                                    </td>
                                    <td class="TabCabeceraOffForm" onmouseover="javascript: return onTabCabeceraOverForm('0');"
                                        onmouseout="javascript: return onTabCabeceraOutForm('0');">Detalle Tipo de Servicio
                                    </td>
                                    <!-- AGREGAR TITULO DEL TAB-->
                                    <td>
                                        <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der_off_plom.gif" style="height: 23px; vertical-align: bottom;" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="DivCuerpoTab">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff;" cellspacing="1"
                                                    cellpadding="1" border="0" width="966">
                                                    <tbody>
                                                        <tr>
                                                            <td class="lineadatos">
                                                                <asp:Label ID="lbl" runat="server" SkinID="lblcb">Tipo de Servicio</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdDTServicio" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel ID="p_items" runat="server">
                                                                            <table cellspacing="1" cellpadding="2" border="0" style="width: 960px" class="textotab">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 15%">Codigo
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" SkinID="txtob"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Nombre Tipo de Servicio
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtNom" runat="server" MaxLength="50" Width="214px" SkinID="txtob"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Estado
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="cboEstado" runat="server" Width="155px" SkinID="cboob">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Observación (Web)
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkObservacion" runat="server" Text="Visible?"></asp:CheckBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <!--
                                                                                    <tr>
                                                                                        <td>Validar Kilometraje?
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkKm" runat="server"></asp:CheckBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    -->
                                                                                    <tr>
                                                                                        <td colspan="2"></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table id="tblTextoInformativo">
                                                                    <tr>
                                                                        <td class="lineadatos" colspan="6">
                                                                            <asp:Label ID="Label1" runat="server" SkinID="lblcb">Texto Informativo</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" SkinID="lblcb">Agregar/Quitar Marca</asp:Label>
                                                                        </td>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                        <td>&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btnAgregar" title="Agregar" style="cursor: pointer" alt="" src="../Images/iconos/b-nuevodetalle.gif"
                                                                                onmouseover="javascript:this.src='../Images/iconos/b-nuevodetalle.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevodetalle.gif'"
                                                                                onclick="fc_agregar();" />
                                                                        </td>
                                                                        <td style="width: 782px">
                                                                            <img id="bnEliminar" title="Eliminar" style="cursor: pointer" alt="" src="../Images/iconos/b-eliminardet.gif"
                                                                                onmouseover="javascript:this.src='../Images/iconos/b-eliminardet.gif'" onmouseout="javascript:this.src='../Images/iconos/b-eliminardet.gif'"
                                                                                onclick="fc_eliminar();" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6">
                                                                            <table border="0" cellpadding="0" cellspacing="0" class="filtroPopup" width="99%"
                                                                                style="padding-left: 40px;">
                                                                                <tr>
                                                                                    <td valign="top" style="padding-top: 8px;">
                                                                                        <asp:HiddenField ID="txth_nid_tipo_servicio" runat="server" />
                                                                                        <div id="header">
                                                                                        </div>
                                                                                        <div id="bandejabuscar" style="overflow-y: auto; overflow-x: hidden; width: 850px; height: auto; max-height: 200px;">
                                                                                        </div>
                                                                                        <div id="footer">
                                                                                            <table style="border-color: White; border-width: 0px; width: 850px; border-collapse: collapse;"
                                                                                                border="0" rules="all" cellspacing="0" cellpadding="0" id="Table1">
                                                                                                <tfoot>
                                                                                                    <tr class="Footer">
                                                                                                        <td colspan="5" />
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
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>

    <div id="pnlBackground" class="modalBackground" style="left: 0px; top: 0px; display: none; position: fixed; z-index: 10000;">
    </div>
    <div id="pnlFiltro" style="background-repeat: repeat; border-width: 2px; background-image: url(../Images/fondo.gif); display: none; width: 940px; height: 590px; left: 170.5px !important; position: fixed; z-index: 99; padding-bottom: 10px;">
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
                                <input type="hidden" id="nid_tipo_servicio_marca">
                                <img src="../Images/iconos/loading.gif" alt="" id="imgLoading1" style="visibility: hidden;" />
                                <img id="btnGuardar" title="Guardar" style="cursor: pointer; padding-right: 7px;"
                                    alt="" src="../Images/iconos/b-guardar.gif" onmouseover="javascript:this.src='../Images/iconos/b-guardar.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" onclick="fc_guardar();" />
                                <img id="btnCerrar" title="Cerrar" style="cursor: pointer; padding-right: 7px;" alt=""
                                    src="../Images/iconos/b-cerrar.gif" onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'" onclick="fc_ClosePnl('pnlFiltro');" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="display: none"></td>
                            <td>
                                <table id="Table2" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <img id="img3" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" style="height: 23px; vertical-align: bottom;" />

                                        </td>
                                        <td id="tabCab1110" class="TabCabeceraOn">AGREGAR/MODIFICAR
                                        </td>
                                        <td>
                                            <img id="img4" alt="" src="../Images/Tabs/tab-der_off_plom.gif" style="height: 23px; vertical-align: bottom;" />

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
                            <td class="td_etiquetaPopupVC" style="padding-left: 20px;">Tipo de Servicio
                            </td>
                            <td class="td_espaciohor">
                                <select id="cboTipoServicio" class="cboob" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_etiquetaPopupVC" style="padding-left: 20px; padding-right: 70px">Marca
                            </td>
                            <td class="td_espaciohor">
                                <select id="cboMarca" class="cboob" style="width: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_etiquetaPopupVC" style="padding-left: 20px;">Texto Visible ?
                            </td>
                            <td class="td_espaciohor">
                                <input type="checkbox" id="ckVisible" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_etiquetaPopupVC" style="padding-left: 20px; vertical-align: text-top">Texto Informativo
                            </td>
                            <td class="td_espaciohor">
                                <div style="width: 97%;">
                                    <textarea style="width: 705px; height: 520px;" id="txtContenido" rows="2"></textarea>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" style="margin-left: 10px;">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; margin-left: 0px;">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="pnlBlocDiv" class="modalBackground" style="left: 0px; top: 0px; display: none; position: fixed; z-index: 98; display: none;">
    </div>
    <div id="pnlProgress" style="display: none; border: dimgray 2px solid; width: 150px; position: fixed; z-index: 100; height: 40px; padding: 7px; background-color: White;">
        <table>
            <tr>
                <td>
                    <img src="../Images/loading.gif" height="37px" width="46px" alt="Procesando..." />
                </td>
                <td>&nbsp;&nbsp;Procesando...
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        setTabCabeceraOnForm('0');

        $(document).ready(function () { fc_cargaInicial(); });
        function fc_cargaInicial() {
            fc_Buscar();

            $("#cboTipoServicio, #cboMarca").append($('<option></option>').val('').html("--Seleccione--"));


            var parametros = new Array();
            var usuario = "<%= Profile.Usuario.Nid_usuario%>";

            parametros[0] = usuario;
            sendInfo = { snidUsuario: usuario };
            callAJAX('imgCargandoBandeja', true, 'ObtenerTipoServicios', sendInfo, function (res) {
                var len = res.d.length;
                for (var i = 0; i < len; i++) {
                    $("#cboTipoServicio").append($("<option></option>").val(res.d[i].codigo).html(res.d[i].nombre));

                }
            });



            callAJAX('imgCargandoBandeja', true, 'ObtenerMarcas', sendInfo, function (res) {
                var len = res.d.length;
                for (var i = 0; i < len; i++) {
                    $("#cboMarca").append($("<option></option>").val(res.d[i].codigo).html(res.d[i].nombre));
                    if (len == 1) {
                        $("#cboMarca").val(res.d[i].codigo);
                    }
                }
            });



        }

        function fc_limpiar() {
            $("#nid_tipo_servicio_marca").val("");
            $("#cboTipoServicio").val("");
            $("#cboMarca").val("");
            $("#ckVisible").prop('checked', false);
            nicEditors.findEditor('txtContenido').setContent('');
        }


        function fc_eliminar() {
            var parametros = new Array();
            parametros[0] = "<%= Profile.Usuario.Nid_usuario%>";
            parametros[1] = "<%= Profile.UsuarioRed%>";
            parametros[2] = "<%= Profile.Estacion%>";
            var seleccion = new Array();


            $('#bandejabuscar tbody input:checked').each(function () {
                seleccion.push($(this).attr('value'));
            });

            if (seleccion.length == 0) {
                alert("Deberá marca una casilla.");
            }
            else {

                var r = confirm("Ud. Desabilitara los registros seleccionados, ¿Desea continuar?");
                if (r == true) {
                    sendInfo = { seleccion: seleccion, parametros: parametros };
                    callAJAX("", true, 'EliminarTipoServicioMarca', sendInfo, function (res) {
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

        function fc_Buscar(orderby, orderbydirection) {
            var parametros = new Array();

            var nid_tipo_servicio = $("#<%=txth_nid_tipo_servicio.ClientID%>").val();
            if (nid_tipo_servicio == "") {
                document.getElementById("tblTextoInformativo").style.display = "none";
            }
            orderby = orderby || 'nu_item';
            orderbydirection = orderbydirection || 'A';

            parametros[0] = nid_tipo_servicio;
            parametros[1] = orderby;
            parametros[2] = orderbydirection;

            sendInfo = { parametros: parametros };
            callAJAX('imgCargandoBandeja', true, 'ObtenerBandeja', sendInfo, function (res) {
                var data = res.d
                if (data[0] == 1) {
                    document.getElementById("header").innerHTML = data[1];
                    document.getElementById("bandejabuscar").innerHTML = data[2];

                }
                else if (data[0] == 0) {
                    document.getElementById("header").innerHTML = data[1];
                    document.getElementById("bandejabuscar").innerHTML = data[2];
                    return false;
                }
                else if (data[0] == -1) {
                    alert("Error, Consulte con el Administrador.");
                    return false;
                }

            });

        }
    </script>

    <script type="text/javascript">


        function fc_agregar(nidTipoServicio) {

            var nid_tipo_servicio = $("#<%=txth_nid_tipo_servicio.ClientID%>").val();
            nidTipoServicio = nidTipoServicio || nid_tipo_servicio;
            $("#cboTipoServicio").val(nidTipoServicio);
            $('#cboTipoServicio').attr("disabled", true);
            $('#cboMarca').attr("disabled", false);
            fc_ShowDiv("pnlFiltro");


        }

        function fc_ver_detalle(nid_tipo_servicio_marca) {

            $('#cboTipoServicio').attr("disabled", true);
            $('#cboMarca').attr("disabled", true);
            $("#nid_tipo_servicio_marca").val(nid_tipo_servicio_marca);
            var parametros = new Array();


            parametros[0] = nid_tipo_servicio_marca;

            sendInfo = { parametros: parametros };
            callAJAX('imgCargandoBandeja', true, 'ObtenerTextoInformativo', sendInfo, function (res) {
                var data = res.d
                if (data[0] == 1) {
                    $("#cboTipoServicio").val(data[1]);
                    $("#cboMarca").val(data[2]);
                    if (data[3] == "1")
                        $("#ckVisible").prop('checked', true);
                    else
                        $("#ckVisible").prop('checked', false);

                    nicEditors.findEditor('txtContenido').setContent(data[4]);
                    fc_ShowDiv("pnlFiltro");
                }
                else if (data[0] == -1) {
                    alert("Error, Consulte con el Administrador.");
                }

            });
        }

        function fc_guardar() {
            var parametros = new Array();

            parametros[0] = $("#cboTipoServicio").val();
            parametros[1] = $("#cboMarca").val();
            parametros[2] = $("#ckVisible").is(":checked");
            parametros[3] = nicEditors.findEditor('txtContenido').getContent();
            parametros[4] = "<%= Profile.Usuario.Nid_usuario%>";
            parametros[5] = "<%= Profile.UsuarioRed%>";
            parametros[6] = "<%= Profile.Estacion%>";
            parametros[7] = $("#nid_tipo_servicio_marca").val();
            sendInfo = { parametros: parametros };


            if (parametros[1] == "") {
                alert("Seleccione marca.");
                return false;
            }
            else if (parametros[3] == "" || parametros[3] == "<br>") {
                alert("Ingrese texto informativo.");
                return false;
            }
            else {
                callAJAX('imgCargandoBandeja', true, 'AgregarTextoInformativo', sendInfo, function (res) {
                    var data = res.d;
                    if (data == 1) {
                        alert("Datos guardados exitosamente.");
                        fc_ClosePnl('pnlFiltro');
                        fc_Buscar();
                    }
                    else if (data == 0) {
                        alert("Esta marca ya fue asignada a un tipo de servicio.");
                        false;
                    }
                    else if (data == -1) {
                        alert("Error, Consulte con el Administrador.");
                        false;
                    }
                });
            }
        }

    </script>

    <script type="text/javascript">

        function callAJAX(img, async, strUrl, strParametros, fn_Callback) {
            $.ajax({
                type: "POST", url: "SRC_Maestro_Detalle_TServicio.aspx/" + strUrl, data: JSON.stringify(strParametros), contentType: 'application/json; charset=utf-8', dataType: 'json', async: async,
                beforeSend: function () { fc_DisplayProgress(''); },
                success: function (data, textStatus) { var resultado = data; fn_Callback(resultado); },
                complete: function () { fc_DisplayProgress("none"); },
                error: function (jqXHR, textStatus, errorThrown) {
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
            fc_limpiar();
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
