﻿<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_AdmCitas_VerDetalle.aspx.cs"
    Inherits="SRC_Operaciones_SRC_AdmCitas_VerDetalle"
    Theme="Default"
    EnableEventValidation="true"
    Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboPaisTelefono.ascx" TagName="ComboPaisTelefono" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/SRC_Validacion.js"></script>

    <style type="text/css">
        .ctxt {
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }

        .cdll {
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }

        .overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #ffffff;
            filter: alpha(opacity=0.7);
            opacity: 0.5;
        }

        .overlayContent {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #ffffff;
            }

            .overlayContent img {
                width: 100px;
                height: 100px;
            }

        .cls_combo_pais {
            font-family: Verdana;
            font-size: 10px;
            color: #555B6C;
            width: 60px;
            border-style: solid;
            border-width: 1px;
            border-color: #95A6C6;
        }

        .cls_caja_telefono {
            width: 60px;
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }

        .cls_caja_anexo {
            width: 33px;
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }
    </style>

    <script type="text/javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';

        //Código JavaScript incluido en un archivo denominado jsUpdateProgress.js 
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args) {
            saveCombos();
            // muestra el popup 
            $find(ModalProgress).show();
        }
        function endReq(sender, args) {
            // esconde el popup 
            $find(ModalProgress).hide();
            recoverCombos();
        }

    </script>

    <script type="text/javascript">

        var combo_PaisTelefono = "";
        var combo_PaisCelular = "";

        function saveCombos() {
            combo_PaisTelefono = document.getElementById("cboPaisTelefonoFijo");
            combo_PaisCelular = document.getElementById("cboPaisTelefonoCel");
        }
        function recoverCombos() {
            if (document.getElementById("cboPaisTelefono") != null) {
                document.getElementById("cboPaisTelefono").id = "cboPaisTelefonoFijo";
                document.getElementById("cboPaisTelefono").id = "cboPaisTelefonoCel";
                for (var i = 0; i < combo_PaisTelefono.length; i++) {
                    $("#cboPaisTelefonoFijo").append("<option value='" + combo_PaisTelefono.options[i].value + "'>" + combo_PaisTelefono.options[i].text + "</option>");
                }
                for (var i = 0; i < combo_PaisCelular.length; i++) {
                    $("#cboPaisTelefonoCel").append("<option value='" + combo_PaisCelular.options[i].value + "'>" + combo_PaisCelular.options[i].text + "</option>");
                }
                setValuesCombo();
            }

        }
        function loadComboPaises() {
            fc_UserControlComboPaisTelefono("S", "0", false, 'cboPaisTelefonoFijo');
            fc_UserControlComboPaisTelefono("S", "0", false, 'cboPaisTelefonoCel');
            setValuesCombo();
        }
        function setValuesCombo() {
            $('#cboPaisTelefonoFijo').val(document.getElementById('<%=this.hfPaisTelefonoFijo.ClientID %>').value);
            $('#cboPaisTelefonoCel').val(document.getElementById('<%=this.hfPaisTelefonoCelular.ClientID %>').value);
            $("#cboPaisTelefonoFijo").removeClass("form-control").addClass("cls_combo_pais");
            $("#cboPaisTelefonoCel").removeClass("form-control").addClass("cls_combo_pais");
            $('#cboPaisTelefonoFijo').on('change', function () {
                document.getElementById("<%=this.hfPaisTelefonoFijo.ClientID %>").value = $("#cboPaisTelefonoFijo").val();
            });

            $('#cboPaisTelefonoCel').on('change', function () {
                document.getElementById("<%=this.hfPaisTelefonoCelular.ClientID %>").value = $("#cboPaisTelefonoCel").val();
            });

            var deshabilitado = document.getElementById('<%=this.txt_det_clitelffijo.ClientID %>').disabled;
            $("#cboPaisTelefonoFijo").prop("disabled", deshabilitado);
            $("#cboPaisTelefonoCel").prop("disabled", deshabilitado);
        }

        function Fc_Imprimir() {
            return true;
        }

        var printWindow;
        function FC_PopupImprimir(strHTML) {
            printWindow = window.open("", "mywindow", "location=0,status=0,scrollbars=0,resizable=0,width=600px,height=430px");
            var strContent = "<html><body>";
            strContent = strContent + "<title>Impresión de Resúmen de Cita</title>";
            strContent = strContent + "<script type='text/javascript' language='Javascript'>"
            strContent = strContent + "function imprimir() {window.print();}" //Print
            strContent = strContent + "</" + "script>"
            strContent = strContent + "</head><body>";
            strContent = strContent + strHTML;
            strContent = strContent + "</body>";
            strContent = strContent + "<script language='javascript' type='text/javascript'> imprimir(); </" + "script>";
            strContent = strContent + "</html>";
            //printWindow.moveTo((screen.width-this.width +12)/2,(screen.height-this.height + 28)/2);   
            printWindow.document.write(strContent);
            printWindow.document.close();
            printWindow.focus();
        }

        function FC_PopupImprimirQR(strHTML) {
            printWindow = window.open("", "mywindow", "location=0,status=0,scrollbars=0,resizable=0,width=600px,height=430px");
            var strContent = "<html><body>";
            strContent = strContent + "<title>Modelo de QR</title>";
            strContent = strContent + "</head><body>";
            strContent = strContent + strHTML;
            strContent = strContent + "</body>";
            strContent = strContent + "<script language='javascript' type='text/javascript'> setInterval(function(){window.print();},1500); </" + "script>";
            strContent = strContent + "</html>";
            printWindow.document.write(strContent);
            printWindow.document.close();
            printWindow.focus();
        }

        function Fc_OpenAtenderCita() {
            document.getElementById("<%=this.btnOpenCA.ClientID %>").click();
            document.getElementById("<%=this.lblTextoPlacaCA.ClientID %>").innerText = document.getElementById("<%=this.lblTextoPlaca.ClientID %>").innerText;
            document.getElementById("<%=this.txtnroplaca.ClientID %>").value = document.getElementById("<%=this.txt_det_vehplaca.ClientID %>").value;
            document.getElementById("<%=this.txtkminicial.ClientID %>").value = '0';
            document.getElementById("<%=this.txtglosa.ClientID %>").value = '';
            document.getElementById("<%=this.chkpuntualidad.ClientID %>").cheked = false;

            return false;
        }

        function Fc_CloseAtenderCita() {
            document.getElementById("<%=this.btnCloseCA.ClientID %>").click();
            return false;
        }

        function Fc_GuardarCitaAtendida() {
            var txtnroplaca = document.getElementById('<%=txtnroplaca.ClientID%>');
            var txtkminicial = document.getElementById('<%=txtkminicial.ClientID%>');

            if (fc_Trim(document.getElementById('<%=txtnroplaca.ClientID%>').value) == "") {
                alert('Ingrese el número de  de Placa.');
                return false;
            }
            if ((fc_Trim(txtkminicial.value.trim()) == '') || (fc_Trim(txtkminicial.value.trim()) == '0')) {
                alert('Debe ingresar un kilometraje inicial mayor a 0.');
                return false;
            }

            return confirm('¿Está seguro de Atender la Cita?.');
        }

        //----------------------------------------------------------------------
        function Fc_BuscarContacto() {
            if (fc_Trim(document.getElementById('<%=txt_det_clidnirut.ClientID%>').value) != '')
                document.getElementById('<%=btn_BuscarContacto.ClientID%>').click();
            else
                return false;
        }
        function llama1() {
            var ddlMarca = document.getElementById('<%=Button1.ClientID%>'); ddlMarca.click();
        }
        function llama2() {
            var btnHorario2 = document.getElementById('<%=Button2.ClientID%>'); btnHorario2.click();
        }
        var ventana
        var cont = 0
        var titulopordefecto = "Defecto"
        function foto(mapa, titulo) {
            if (cont == 1) { ventana.close(); ventana = null }
            if (titulo == null) { titulo = titulopordefecto }
            ventana = window.open('', 'ventana', 'toolbar=no,status=no,location=no,directories=0,menubar=no,scrollbars=no,resizable=0,width=50%,height=50%')
            ventana.document.write('<html><head><title>' + titulo + '</title></head><body style="overflow:hidden" marginwidth="0" marginheight="0" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" scroll="no" onUnload="opener.cont=0"><img src="' + mapa + '" onLoad="opener.redimensionar(this.width, this.height)">')
            ventana.document.close()
            cont++
        }
        function redimensionar(ancho, alto) {
            ventana.resizeTo(ancho + 12, alto + 28)
            ventana.moveTo((screen.width - ancho) / 2, (screen.height - alto) / 2)
        }

        function SoloNumeros(eventObj) {

            var key;
            if (eventObj.keyCode)            // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser  

            if (key >= 48 && key <= 57) { }
            else {
                return false;//anula la entrada de texto. 
            }
        }

        //--------
        function ValidarActualizacion() {

            var pais_celular = document.getElementById("cboPaisTelefonoCel").value;
            var telf_movil1 = document.getElementById('<%=txt_det_clitelfmovil.ClientID%>').value;
            var pais_fijo = document.getElementById("cboPaisTelefonoFijo").value;
            var telf_fijo = document.getElementById('<%=txt_det_clitelffijo.ClientID%>').value;
            if (pais_celular == "" && telf_movil1 != "") { alert("Si ingresa un número celular debe indicar el pais."); return false; }
            else if (valida_celular(document.getElementById('<%=txt_det_clitelfmovil.ClientID%>'), (pais_celular == "162")) != true) { alert(SRC_Telefono_Movil_Invalido); return false; }
            if (pais_fijo == "" && telf_fijo != "") { alert("Si ingresa un número de teléfono debe indicar el pais."); return false; }
            else if (valida_telefono(document.getElementById('<%=txt_det_clitelffijo.ClientID%>')) != true) { alert(SRC_Telefono_Fijo_Invalido); return false; }
            if (telf_fijo == "" && telf_movil1 == "") { alert("Debe ingresar un teléfono fijo o un teléfono celular."); return false; }

        }

    </script>


    <table cellpadding="2" cellspacing="0" width="1000" border="0" style="height: 47px">
        <tr>
            <td>
                <!--INICIO ICONOS DE ACCCION -->
                <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
                    <tr>
                        <td style="width: 100%" align="right">
                            <%--<asp:ImageButton ID="btnQR" runat="server" ImageUrl="~/Images/iconos/b-imprimirI.gif"
                                ToolTip="Ver Reporte de QR" OnClick="btnQR_Click" onmouseover="javascript:this.src='../Images/iconos/b-imprimirI2.gif'"
                                onmouseout="javascript:this.src='../Images/iconos/b-imprimirI.gif'"
                                OnClientClick="javascript:return Fc_Imprimir();" />--%>
                        </td>
                        <td style="width: 100%" align="right">
                            <asp:ImageButton ID="btn_det_guardar" runat="server" ToolTip="Guardar" ImageUrl="~/Images/iconos/b-guardar.gif" OnClientClick="javascript:return ValidarActualizacion();"
                                onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClick="btn_det_guardar_Click" Visible="false" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/iconos/b-imprimir.gif"
                                ToolTip="Imprimir " OnClick="btnImprimir_Click" onmouseover="javascript:this.src='../Images/iconos/b-imprimir2.gif'"
                                onmouseout="javascript:this.src='../Images/iconos/b-imprimir.gif'" OnClientClick="javascript:return Fc_Imprimir();" Visible="false" />
                        </td>
                        <td style="width: 100%" align="right">
                            <asp:ImageButton ID="btn_det_retornar" runat="server" ToolTip="Retornar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btn_det_retornar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <cc1:TabContainer ID="tabActuEstCita" runat="server" CssClass="" ActiveTabIndex="0">
                    <cc1:TabPanel runat="server" ID="tabActuEst">
                        <HeaderTemplate>
                            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                    <td class="TabCabeceraOn">Detalle de la Cita</td>
                                    <td>
                                        <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="DivCuerpoTab">
                                <table width="980" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                                            <table cellpadding="1" cellspacing="1" style="background-color: #ffffff; margin-left: 5px; margin-right: 5px;" border="0">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:UpdatePanel ID="upd_admcitas" runat="server">
                                                            <ContentTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="cuerponuevo" style="width: 956px">
                                                                                <tr>
                                                                                    <td valign="top" style="width: 472px;">
                                                                                        <table style="width: 451px">
                                                                                            <tr>
                                                                                                <td class="lineatitulo">
                                                                                                    <asp:Label ID="Label1" runat="server" Text="CITA" Font-Bold="False" Font-Names="Rod" SkinID="lblCB"></asp:Label>
                                                                                                    &nbsp; &nbsp;&nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px">
                                                                                                    <asp:Panel ID="tbl_cita" runat="server">
                                                                                                        <table cellpadding="2" cellspacing="2">
                                                                                                            <tr>
                                                                                                                <td style="width: 120px">Fecha
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_det_citafecha" Width="120px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td style="width: 10px;"></td>
                                                                                                                <td>Hora
                                                                                                                </td>
                                                                                                                <td align="right">
                                                                                                                    <asp:TextBox ID="txt_det_citahora" Width="70px" runat="server"></asp:TextBox></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Asesor de Servicio
                                                                                                                </td>
                                                                                                                <td colspan="5">
                                                                                                                    <asp:TextBox ID="txt_det_citaasesorserv" Width="280px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Ubicación
                                                                                                                </td>
                                                                                                                <td colspan="5">
                                                                                                                    <asp:TextBox ID="txt_det_citaubicacion" Width="280px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Servicio Técnico
                                                                                                                </td>
                                                                                                                <td colspan="5">
                                                                                                                    <asp:TextBox ID="txt_det_citaServicio" runat="server" Width="280px"></asp:TextBox></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Código Reserva
                                                                                                                </td>
                                                                                                                <td colspan="5">
                                                                                                                    <asp:TextBox ID="txt_det_citaCodigo" runat="server" Width="280px"></asp:TextBox></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Estado
                                                                                                                </td>
                                                                                                                <td colspan="5">
                                                                                                                    <asp:DropDownList ID="cbo_det_citaestado" runat="server" Width="155px">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="lineatitulo">
                                                                                                    <asp:Label ID="Label14" runat="server" Text="VEHICULO" Font-Bold="False" SkinID="lblCB"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px">
                                                                                                    <asp:Panel ID="tbl_vehiculo" runat="server">
                                                                                                        <table cellpadding="2" cellspacing="2">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTextoPlaca" Width="120px" runat="server" Text="Placa/Patente"></asp:Label>
                                                                                                                </td>
                                                                                                                <td colspan="5">
                                                                                                                    <asp:TextBox ID="txt_det_vehplaca" Width="80px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Marca
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_det_vehmarca" Width="80px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td style="width: 10px;"></td>
                                                                                                                <td style="width: 60px;">Modelo
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_det_vehmodelo" Width="80px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                                <td style="width: 10px"></td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="lineatitulo" valign="top">
                                                                                                    <asp:Label ID="Label21" runat="server" Text="OBSERVACIONES" SkinID="lblCB" Visible="false"></asp:Label>
                                                                                                    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                                                    <asp:Button ID="btnEditarObserv" runat="server" OnClick="btnEditarObserv_Click" Text="Actualizar" Width="60px" BackColor="Gainsboro" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="7pt" ForeColor="#404040" Height="17px" Style="margin-left: 5px;" Visible="false" /></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px">
                                                                                                    <asp:Panel ID="tbl_observacion" runat="server">
                                                                                                        <table cellpadding="2" cellspacing="2">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_det_observaciones" TextMode="MultiLine" Width="380px" Height="40px" runat="server" Style="resize: none;" Visible="false" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <asp:HiddenField ID="hf_ID_CITA" runat="server" />
                                                                                        <asp:HiddenField ID="hf_ESTADO_CITA" runat="server" />
                                                                                        <asp:HiddenField ID="hf_ROW_INDEX" runat="server" />
                                                                                        <asp:HiddenField ID="hf_FECHA_HABIL" runat="server" />
                                                                                        <asp:HiddenField ID="hf_INTERVALO_TALLER" runat="server" />
                                                                                        <asp:HiddenField ID="hf_DATOS_TALLER" runat="server" />
                                                                                        <asp:HiddenField ID="hf_HORAS_VACIAS" runat="server" />
                                                                                        <asp:HiddenField ID="hf_QUICK_SERVICE" runat="server" />
                                                                                        <asp:HiddenField ID="hf_DIAS_SERVICIO" runat="server" />
                                                                                        <asp:HiddenField ID="hf_ID_TALLER" runat="server" />
                                                                                        <asp:HiddenField ID="hf_ID_SERVICIO" runat="server" />
                                                                                        <asp:HiddenField ID="hf_ID_MODELO" runat="server" />
                                                                                        <asp:HiddenField ID="hf_EmailCallCenter" runat="server" />
                                                                                        <asp:HiddenField ID="hf_STATUS_WS" Value="0" runat="server" />
                                                                                        <asp:TextBox ID="hf_DATOS_CITA" runat="server" Visible="False"></asp:TextBox></td>
                                                                                    <td valign="top">
                                                                                        <table style="width: 463px">
                                                                                            <tr>
                                                                                                <td class="lineatitulo">
                                                                                                    <asp:Label ID="Label2" runat="server" Text="CONTACTO" Font-Bold="False" Font-Names="Rod" SkinID="lblCB"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px">
                                                                                                    <asp:Panel ID="tbl_cliente" runat="server">
                                                                                                        <table cellpadding="2" cellspacing="2">
                                                                                                            <tr style="display: none;">
                                                                                                                <td>
                                                                                                                    <asp:ImageButton ID="btn_BuscarContacto" OnClick="btn_BuscarContacto_Click" runat="server"></asp:ImageButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td style="width: 100px;">Nombres(s)</td>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:TextBox ID="txt_det_clinombre" Width="340px" runat="server" MaxLength="50"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Apellido Paterno</td>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:TextBox ID="txt_det_cliapellidospat" Width="340px" runat="server" MaxLength="50"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Apellido Materno</td>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:TextBox ID="txt_det_cliapellidosmat" Width="340px" runat="server" MaxLength="50"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTextoTipoDoc" runat="server" Text="DNI/RUT"></asp:Label></td>
                                                                                                                <td style="width: 180px;">
                                                                                                                    <asp:TextBox ID="txt_det_clidnirut" Width="100px" runat="server" onblur="return Fc_BuscarContacto(this, event);" MaxLength="20"></asp:TextBox></td>
                                                                                                                <td style="width: 90px;"></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Teléfono Fijo</td>
                                                                                                                <td>
                                                                                                                    <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoFijo" />
                                                                                                                    <asp:HiddenField ID="hfPaisTelefonoFijo" runat="server" Value="162" />
                                                                                                                    <asp:TextBox ID="txt_det_clitelffijo" Width="80px" MaxLength="9" onkeypress="return SoloNumeros(event);" runat="server"></asp:TextBox>
                                                                                                                    <asp:TextBox runat="server" ID="txtTelefonoFijo_Anexo" class="cls_caja_anexo" MaxLength="10" placeholder="Anexo" />
                                                                                                                </td>
                                                                                                                <td>Teléfono  Oficina</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_det_clitelfoficina" Width="70px" MaxLength="20" runat="server"></asp:TextBox></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Teléfono Móvil 1</td>
                                                                                                                <td>
                                                                                                                    <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoCel" />
                                                                                                                    <asp:HiddenField ID="hfPaisTelefonoCelular" runat="server" Value="162" />
                                                                                                                    <asp:TextBox ID="txt_det_clitelfmovilCod" runat="server" Width="25px" Text="569" MaxLength="3" ReadOnly="true"></asp:TextBox>
                                                                                                                    <asp:TextBox ID="txt_det_clitelfmovil" runat="server" Width="80px" MaxLength="9" onkeypress="return SoloNumeros(event);"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>Teléfono Móvil 2</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_det_clitelfmovil2Cod" runat="server" Width="25px" Text="569" MaxLength="3" ReadOnly="true"></asp:TextBox>
                                                                                                                    <asp:TextBox ID="txt_det_clitelfmovil2" runat="server" Width="80px" MaxLength="20"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Email Personal</td>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:TextBox ID="txt_det_cliemail" Width="340px" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Email Trabajo</td>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:TextBox ID="txt_det_cliemail_trab" runat="server" Width="340px"></asp:TextBox></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Email Alternativo</td>
                                                                                                                <td colspan="4">
                                                                                                                    <asp:TextBox ID="txt_det_cliemail_alter" runat="server" Width="340px"></asp:TextBox></td>
                                                                                                            </tr>
                                                                                                            <tr id="trContacto_Mensaje" runat="server" visible="false">
                                                                                                                <td style="text-align: left; font-weight: bold; color: red" colspan="6">Nota: Algunos datos de la persona/empresa no pueden modificarse debido a que su identidad ha sido verificada con una entidad reguladora.</td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="lineatitulo">
                                                                                                    <asp:Label ID="Label15" runat="server" Text="RECORDATORIO" Font-Bold="False" Font-Names="Rod" SkinID="lblCB"></asp:Label></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px">
                                                                                                    <asp:Panel ID="tbl_recordatorio" runat="server">
                                                                                                        <table cellpadding="2" cellspacing="2">
                                                                                                            <tr>
                                                                                                                <td colspan="2">
                                                                                                                    <asp:Label ID="Label16" runat="server" Text="Las notificaciones con el cliente se enviarán de la sgte. forma :" Width="401px"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="right">
                                                                                                                    <asp:CheckBox ID="chk_det_recoremail" Text="Por E-mail" runat="server" />&nbsp;                                                                                               
                                                                                                &nbsp;</td>

                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan="2"></td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" colspan="2" valign="middle">
                                                                                        <asp:Button ID="btn_det_ActCliente" runat="server" Text="Actualizar datos cliente" OnClick="btn_det_ActCliente_Click" Font-Bold="True" Visible="false" />
                                                                                        <asp:Button ID="btn_det_FormRecor" runat="server" Text="Actualizar forma recordatorio" OnClick="btn_det_FormRecor_Click" Font-Bold="True" Visible="false" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="height: 40px; text-align: center">
                                                                                        <asp:Button ID="btn_Confirmar_Cita" runat="server"
                                                                                            Text="Confirmar Cita" Width="187px" OnClick="btn_Confirmar_Cita_Click" Font-Bold="True" />
                                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="btn_Anular_Cita" runat="server" Text="Anular Cita"
                                                                            Width="187px" OnClick="btn_Anular_Cita_Click" Font-Bold="True" />
                                                                                        &nbsp; &nbsp;<asp:Button ID="btn_Reprog_Cita" runat="server"
                                                                                            Text="Reprogramar Cita" Width="187px" OnClick="btn_Reprog_Cita_Click" Font-Bold="True" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="text-align: center">
                                                                                        <asp:Button ID="BtnAtenderCita" runat="server" Text="Atender Cita"
                                                                                            Width="187px" Font-Bold="True" OnClientClick="Fc_OpenAtenderCita();" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btn_det_guardar" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                    </tr>
                                </table>
                            </div>


                            <!-- modal popup MSGBOX  -->
                            <cc1:ModalPopupExtender ID="popup_msgbox_confirm" DropShadow="True" BackgroundCssClass="modalBackground" TargetControlID="hid_popupmsboxconfirm"
                                PopupControlID="upd_pn_msbox_confirm" runat="server" DynamicServicePath="" Enabled="True">
                            </cc1:ModalPopupExtender>
                            <input id="hid_popupmsboxconfirm" type="hidden" runat="server" />
                            <asp:UpdatePanel ID="upd_pn_msbox_confirm" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="div_upd_msgbox_confirm2" Width="297px" runat="server"
                                        Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
                                            <tr>
                                                <td style="width: 245px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;</td>
                                                <td style="width: 52px; background-repeat: repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
                                            </tr>
                                        </table>
                                        <table cellpadding="2" cellspacing="2" width="286px" style="vertical-align: middle; background-color: #FFFFFF;" align="center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table cellpadding="0" cellspacing="0" align="left" style="width: 270px">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lbl_mensajebox" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="#464646" Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 35px;" align="center">
                                                                    <asp:Button ID="btn_msgboxconfir_si" CssClass="btn" runat="server" Text="SI" Font-Bold="True" Font-Size="10pt" Height="30px" OnClick="btn_msgboxconfir_si_Click" Width="35px" />
                                                                    &nbsp;
                                    <asp:Button ID="btn_msgboxconfir_no" CssClass="btn" runat="server" Text="NO" Font-Bold="True" Font-Size="10pt" Height="30px" OnClick="btn_msgboxconfir_no_Click" Width="35px" /><asp:Button ID="btn_msgboxconfir_aceptar" CssClass="btn" runat="server" Text="ACEPTAR" Font-Bold="True" Font-Size="10pt" Height="30px" OnClick="btn_msgboxconfir_aceptar_Click" Width="90px" /></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />
                            <cc1:ModalPopupExtender ID="mpMensajes" runat="server"
                                BackgroundCssClass="modalBackground"
                                CancelControlID="btnRegresar2"
                                PopupControlID="PanelMensajes"
                                TargetControlID="hfMensjaes" RepositionMode="None">
                            </cc1:ModalPopupExtender>
                            <asp:UpdatePanel ID="PanelMensajes" runat="server">
                                <ContentTemplate>
                                    <table style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; border-bottom: black thin solid; background-color: white; width: 522px;">
                                        <tr>
                                            <td colspan="1" style="text-align: center; height: 43px;" align="center">
                                                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                    Font-Size="13pt" Height="100%" Text="La Cita fue Reprogramada" Width="100%" ForeColor="Maroon"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnRegresar2" runat="server" Font-Bold="True" Height="25px" Text="Regresar"
                                                    Width="130px" OnClick="btnRegresar2_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px"></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:HiddenField ID="hfMensjaes" runat="server" />
                            <br />
                            <cc1:ModalPopupExtender ID="MpeReprogramacion"
                                runat="server"
                                BackgroundCssClass="modalBackground"
                                PopupControlID="UdPnlReprogramacion"
                                RepositionMode="None"
                                TargetControlID="HdfReprogramacion">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="PnlReprogramacion" runat="server" BorderStyle="None" Width="919px">
                                <asp:UpdatePanel ID="UdPnlReprogramacion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel Style="background-image: url(../Images/fondo.gif); padding-bottom: 8px; padding-top: 0px; background-repeat: repeat" ID="Panel2" runat="server" CssClass="modalPopup" Width="840px">
                                            <table style="width: 848px; height: 44px" id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="background-image: url(../Images/SRC/LINE.PNG); background-repeat: no-repeat; height: 34px; text-align: center" colspan="3">&nbsp; &nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <table style="width: 897px" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-right: 10px; padding-left: 10px; width: 858px">
                                                            <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="" Width="820px" ActiveTabIndex="0">
                                                                <cc1:TabPanel runat="server" ID="TabPanel1">
                                                                    <HeaderTemplate>
                                                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 809px">
                                                                            <tr valign="bottom">
                                                                                <td style="width: 196px" valign="bottom">
                                                                                    <table id="Table3" runat="server"
                                                                                        cellpadding="0" cellspacing="0" border="0" style="height: 17px;">
                                                                                        <tr id="Tr2" runat="server">
                                                                                            <td id="Td4" runat="server" style="height: 20px">
                                                                                                <img alt="" src="../Images/Tabs/tab-izq.gif" height="30" /></td>
                                                                                            <td id="Td5" class="TabCabeceraOn" style="width: 220px; height: 20px;" runat="server">REPROGRAMACIÓN DE CITA</td>
                                                                                            <td id="Td6" style="width: 5px; height: 20px;" runat="server">
                                                                                                <img alt="" src="../Images/Tabs/tab-der.gif" height="30" /></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <table cellpadding="0" cellspacing="0" border="0" style="margin-right: 5px">
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:ImageButton ID="btnRegresarPanelreprog" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                                                                                    onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                                                                                    onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresarPanelreprog_Click" />

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>

                                                                    </HeaderTemplate>
                                                                    <ContentTemplate>
                                                                        <table id="Table8" cellspacing="0" cellpadding="0" border="0" backcolor="white">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <!-- Cabecera -->
                                                                                    <td style="width: 824px">
                                                                                        <img style="width: 100%" alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="background-color: white" align="center" valign="middle">
                                                                                        <table id="Table7" width="800" border="0" cellpadding="1" cellspacing="0" class="cuerponuevo" summary="cbusqueda">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="padding-left: 5px; width: 100px; height: 21px;" align="left">
                                                                                                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Tipo Servicio" Width="100px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="left" colspan="2" style="height: 21px">
                                                                                                        <asp:Label ID="lblGridTipoServicio" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px; height: 21px;" align="left">
                                                                                                        <asp:Label ID="Label27" runat="server" Font-Bold="True" Text="Servicio" Width="90px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="left" colspan="2" style="height: 21px">
                                                                                                        <asp:Label ID="lblGridServicio" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="padding-left: 5px; width: 100px" align="left">
                                                                                                        <asp:Label ID="lblTextoGridPlaca" runat="server" Font-Bold="True" Text="Patente" Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px" align="left">
                                                                                                        <asp:Label ID="lblGridPlaca" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px" align="left">
                                                                                                        <asp:Label ID="Label33" runat="server" Font-Bold="True" Text="Marca" Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px" align="left">
                                                                                                        <asp:Label ID="lblGridMarca" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px" align="left">
                                                                                                        <asp:Label ID="Label35" runat="server" Font-Bold="True" Text="Modelo" Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 100px" align="left">
                                                                                                        <asp:Label ID="lblGridModelo" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                        <!-- tabla 31 -->
                                                                        <center>
                                                                            <asp:Panel ID="PanelUno" runat="server" BackColor="White">
                                                                                <table style="background-color: white; text-align: center" id="Table9" cellspacing="0" cellpadding="0" border="0" backcolor="white">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; width: 100%; clip: rect(auto auto auto auto); padding-top: 0px">
                                                                                                <table class="textotab" cellspacing="1" cellpadding="2" width="800" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td style="border-right: dimgray 1px solid; padding-right: 1px; border-top: dimgray 1px solid; padding-left: 1px; padding-bottom: 1px; border-left: dimgray 1px solid; padding-top: 1px; border-bottom: dimgray 1px solid; height: 28px; background-color: #005cab" align="center" colspan="9">
                                                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                                                    <ContentTemplate>
                                                                                                                        <table width="100%">
                                                                                                                            <tr>
                                                                                                                                <td style="width: 97%; text-align: center;" align="center">
                                                                                                                                    <asp:Label ID="lblSeleccion1" runat="server" Font-Bold="True" ForeColor="WhiteSmoke"
                                                                                                                                        Text="Seleccion de Reserva" Width="100%" Font-Names="Verdana" Font-Size="10pt"></asp:Label></td>
                                                                                                                                <td style="width: 3%">
                                                                                                                                    <asp:ImageButton ID="ImgSalir" runat="server" Height="16px" ImageAlign="AbsMiddle"
                                                                                                                                        ImageUrl="~/Images/iconos/b-regresar.gif" OnClick="ImgSalir_Click" ToolTip="Regresar"
                                                                                                                                        Width="16px" Visible="False" /></td>
                                                                                                                            </tr>
                                                                                                                        </table>

                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="left" style="padding-left: 5px; width: 12%" valign="middle">
                                                                                                                <asp:Label ID="lblTextoLocalR" runat="server" Text="Local" Font-Bold="True"></asp:Label></td>
                                                                                                            <td align="left" style="width: 28%" valign="middle">
                                                                                                                <asp:DropDownList ID="ddlPuntoRed" runat="server" AutoPostBack="True" CssClass="seleccion_04" Width="200px" OnSelectedIndexChanged="ddlPuntoRed_SelectedIndexChanged" SkinID="cboob"></asp:DropDownList></td>
                                                                                                            <td align="left" style="width: 12%" valign="middle">
                                                                                                                <asp:Label ID="lblTextoTallerR" runat="server" Text="Taller" Font-Bold="True"></asp:Label></td>
                                                                                                            <td align="left" colspan="4" style="width: 45%" valign="middle">
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddlTaller" runat="server" Width="150px" OnSelectedIndexChanged="ddlTaller_SelectedIndexChanged" SkinID="cboob"></asp:DropDownList></td>
                                                                                                                        <td style="padding-left: 5px">
                                                                                                                            <asp:Label ID="lblTaller" runat="server" Visible="False" Font-Bold="False"></asp:Label></td>
                                                                                                                        <td valign="middle">
                                                                                                                            <div style="height: 30px">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:ImageButton ID="btnMapaTaller" runat="server" ImageUrl="~/Images/SRC/mapa.jpg" OnClick="btnMapaTaller_Click" />

                                                                                                                                    </ContentTemplate>
                                                                                                                                    <Triggers>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="ddlPuntoRed" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="ddlTaller" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                                                                                                    </Triggers>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="padding-left: 5px; font-weight: bold;" valign="middle" align="left">Fecha Inicio</td>

                                                                                                            <td valign="middle" align="left">
                                                                                                                <asp:TextBox ID="txtFecha" runat="server" CssClass="texto_00" Enabled="False"
                                                                                                                    Font-Size="8pt" MaxLength="1" Width="75px">

                                                                                                                </asp:TextBox>
                                                                                                                &nbsp;
                                                                                                               <asp:ImageButton ID="imbFecha"
                                                                                                                   runat="server" ImageUrl="~/Images/SRC/cal.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                                                &nbsp; &nbsp;
                                                                                                                <asp:ImageButton ID="imbFecAnt" OnClick="imbFecAnt_Click" runat="server" ImageUrl="~/Images/SRC/btn_atras.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>&nbsp;
                                                                                                                <asp:ImageButton ID="imbFecSgte" OnClick="imbFecSgte_Click" runat="server" ImageUrl="~/Images/SRC/btn_adelante.jpg" ImageAlign="AbsMiddle"></asp:ImageButton><cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" Font-Size="7pt"
                                                                                                                    Font-Italic="True" ValidationGroup="MKE" Display="Dynamic" ControlToValidate="txtFecha"
                                                                                                                    ControlExtender="mskF1" InvalidValueBlurredMessage="Fecha Invalida" InvalidValueMessage="Fecha Invalida"
                                                                                                                    EmptyValueMessage="Ingrese Fecha" ErrorMessage="MaskedEditValidator1">
                                                                                                                </cc1:MaskedEditValidator>
                                                                                                                <cc1:MaskedEditExtender ID="mskF1" runat="server" TargetControlID="txtFecha"
                                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                                    CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder=""
                                                                                                                    CultureDatePlaceholder="" ErrorTooltipEnabled="True">
                                                                                                                </cc1:MaskedEditExtender>
                                                                                                                <cc1:CalendarExtender ID="ceFecha" runat="server" TargetControlID="txtFecha" Enabled="True"
                                                                                                                    PopupButtonID="imbFecha" Format="dd/MM/yyyy" OnClientDateSelectionChanged="llama1">
                                                                                                                </cc1:CalendarExtender>
                                                                                                            </td>
                                                                                                            <td valign="middle" align="left" style="font-weight: bold">Hora Inicio</td>
                                                                                                            <td valign="middle" align="left">
                                                                                                                <asp:DropDownList ID="ddlHoraInicio1" runat="server" AutoPostBack="True" Width="80px" OnSelectedIndexChanged="ddlHoraInicio1_SelectedIndexChanged" Font-Bold="False" Font-Names="Verdana"></asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td valign="middle" align="left" style="font-weight: bold; width: 9%;">Hora Final</td>
                                                                                                            <td valign="middle" align="left" style="font-weight: normal; text-decoration: none">
                                                                                                                <asp:DropDownList ID="ddlHoraFin1" runat="server" AutoPostBack="True" CssClass="seleccion_00" Width="80px" OnSelectedIndexChanged="ddlHoraFin1_SelectedIndexChanged"></asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td valign="middle" align="center">
                                                                                                                <asp:LinkButton ID="lbTextoAqui" OnClick="lbTextoAqui_Click" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#0000C0" Font-Size="9pt">Turnos proximos >></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="9" style="border-top: #c7d7ee 2px solid; text-align: left;">
                                                                                                                <asp:HiddenField ID="hfFecha" runat="server" />
                                                                                                                <asp:HiddenField ID="hfHoraIni1" runat="server" />
                                                                                                                <asp:HiddenField ID="hfHoraFin1" runat="server" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%" align="left">
                                                                                                <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Height="2px" Width="2px" BackColor="White" ForeColor="White" BorderStyle="None" BorderColor="White"></asp:Button></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; height: 240px" align="center">
                                                                                                <asp:Panel ID="pnlHorarioReserva" runat="server" Height="215px" CssClass="tabla_horario" Width="800px" ScrollBars="Both" HorizontalAlign="Left">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                        <ContentTemplate>




                                                                                                            <asp:GridView ID="gvHorarioReserva" runat="server" AutoGenerateColumns="False" Font-Bold="False" Font-Size="8pt"
                                                                                                                HorizontalAlign="Left" OnRowCommand="gvHorarioReserva_RowCommand" SkinID="Grilla">
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="TALLER" HeaderText="Taller">
                                                                                                                        <ItemStyle CssClass="tabla_horario_filataller1" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                                                            Width="200px" />
                                                                                                                        <HeaderStyle CssClass="tabla_horario_taller" HorizontalAlign="Center"
                                                                                                                            VerticalAlign="Middle" Width="100px" />
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="ASESOR_SERV" HeaderText="Asesor de Servicio">
                                                                                                                        <ItemStyle CssClass="tabla_horario_filaasesor1" />
                                                                                                                        <HeaderStyle CssClass="tabla_horario_asesor" />
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="Rango_Horario" HeaderText="Rango_Horario">
                                                                                                                        <ItemStyle CssClass="tabla_horario_filaasesor1" />
                                                                                                                        <HeaderStyle CssClass="tabla_horario_asesor" />
                                                                                                                    </asp:BoundField>
                                                                                                                </Columns>
                                                                                                                <RowStyle Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                <EditRowStyle Font-Size="8pt" />
                                                                                                                <HeaderStyle Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                                                    Wrap="True" />
                                                                                                                <AlternatingRowStyle Font-Size="8pt" />
                                                                                                            </asp:GridView>


                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </asp:Panel>
                                                                                                <asp:Label ID="lblFlgHorario1" runat="server" Font-Bold="True" Text="No hay horario disponible para la consuta." Font-Names="Verdana" ForeColor="DimGray" Font-Size="12pt"></asp:Label>
                                                                                                <br />
                                                                                                <asp:Panel ID="pnlLeyenda" runat="server" BorderWidth="0px" Font-Names="Arial Narrow" BorderColor="White" HorizontalAlign="Left">
                                                                                                    <table border="0" cellpadding="0" cellspacing="0" class="textotab" width="100%">
                                                                                                        <tr>
                                                                                                            <td style="font-weight: bold; width: 10px">TCT:</td>
                                                                                                            <td>
                                                                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                                                                                                    <ContentTemplate>
                                                                                                                        &nbsp;<asp:Label ID="Lbltct" runat="server" BorderStyle="None" Font-Names="Arial Narrow"></asp:Label>

                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                            <td align="center">


                                                                                                                <img src="../Images/leyenda.jpg" /></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                                &nbsp; &nbsp; </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 100%; height: 45px; border-top: #c7d7ee 2px solid;" align="center">&nbsp;<asp:Button ID="btnReprogramarCita_1" OnClick="btnReprogramarCita_1_2_Click" runat="server" Font-Bold="True" Height="32px" CssClass="boton_02" Text="Reprogramar Cita" ForeColor="Black"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <!-- fin tabla 31 -->
                                                                            <asp:Panel ID="Paneldos" runat="server" BackColor="White" Visible="False">
                                                                                <table style="background-color: white" id="Table10" class="seccion" cellspacing="0" cellpadding="0" width="800" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="background-color: white" align="left">
                                                                                                <table class="textotab" cellspacing="0" cellpadding="0" width="800" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="center" colspan="11" style="border-right: dimgray 1px solid; padding-right: 1px; border-top: dimgray 1px solid; padding-left: 1px; padding-bottom: 1px; border-left: dimgray 1px solid; padding-top: 1px; border-bottom: dimgray 1px solid; height: 28px; background-color: #005cab">
                                                                                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:Label ID="lblSeleccion2" runat="server" Font-Bold="True" ForeColor="WhiteSmoke"
                                                                                                                            Text="Seleccion de Reserva" Width="100%" Font-Names="Verdana" Font-Size="10pt"></asp:Label>

                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="11" style="height: 10px; background-color: white"></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="font-weight: bold; width: 9%;" valign="middle" align="left" width="10%">Fecha Inicio</td>
                                                                                                            <td valign="middle" align="left" style="text-align: center">
                                                                                                                <asp:TextBox ID="txtFechaIni" runat="server" Width="70px" Font-Size="8pt" MaxLength="10" SkinID="txtob" Enabled="False"></asp:TextBox><!-- maskval, maskedi, calendar --><cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server" Font-Size="7pt" Font-Italic="True" ValidationGroup="MKE" Display="Dynamic" ControlToValidate="txtFechaIni" ControlExtender="mskF1" InvalidValueBlurredMessage="Fecha Invalida" InvalidValueMessage="Fecha Invalida" EmptyValueMessage="Ingrese Fecha" ErrorMessage="MaskedEditValidator2"></cc1:MaskedEditValidator>
                                                                                                                <cc1:MaskedEditExtender ID="mskFI" runat="server" TargetControlID="txtFechaIni" Enabled="True" Mask="99/99/9999" MaskType="Date" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
                                                                                                                <cc1:CalendarExtender ID="ceFechaIni" runat="server" TargetControlID="txtFechaIni" Enabled="True" PopupButtonID="imbFecha1" Format="dd/MM/yyyy" OnClientDateSelectionChanged="llama2"></cc1:CalendarExtender>
                                                                                                            </td>
                                                                                                            <td style="width: 5%;" valign="middle" align="left">
                                                                                                                <asp:ImageButton ID="imbFecha1" runat="server" ImageUrl="~/Images/SRC/cal.jpg"></asp:ImageButton>
                                                                                                            </td>
                                                                                                            <td style="font-weight: bold; width: 9%;" valign="middle" width="10%">Fecha Final</td>
                                                                                                            <td valign="middle" align="left" style="text-align: center">
                                                                                                                <asp:TextBox ID="txtFechaFin" runat="server" Width="70px" Font-Size="8pt" MaxLength="10" SkinID="txtob" Enabled="False"></asp:TextBox><!-- maskval, maskedit, calendar --><cc1:MaskedEditValidator ID="MaskedEditValidator3" runat="server" Font-Size="7pt" Font-Italic="True" ValidationGroup="MKE" Display="Dynamic" ControlToValidate="txtFechaFin" ControlExtender="mskFF" InvalidValueBlurredMessage="Fecha Invalida" InvalidValueMessage="Fecha Invalida" EmptyValueMessage="Ingrese Fecha" ErrorMessage="MaskedEditValidator3"></cc1:MaskedEditValidator>
                                                                                                                <cc1:MaskedEditExtender ID="mskFF" runat="server" TargetControlID="txtFechaFin" Enabled="True" Mask="99/99/9999" MaskType="Date" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ErrorTooltipEnabled="True"></cc1:MaskedEditExtender>
                                                                                                                <cc1:CalendarExtender ID="ceFechaFin" runat="server" TargetControlID="txtFechaFin" Enabled="True" PopupButtonID="imbFecha2" Format="dd/MM/yyyy" OnClientDateSelectionChanged="llama2"></cc1:CalendarExtender>
                                                                                                            </td>
                                                                                                            <td style="width: 7%;" valign="middle" align="left">
                                                                                                                <asp:ImageButton ID="imbFecha2" runat="server" ImageUrl="~/Images/SRC/cal.jpg"></asp:ImageButton>
                                                                                                            </td>
                                                                                                            <td style="font-weight: bold; width: 8%;" valign="middle" align="left" width="10%">Hora Inicio</td>
                                                                                                            <td valign="middle" align="left">
                                                                                                                <asp:DropDownList ID="ddlHoraInicio2" runat="server" CssClass="seleccion_00" Width="80px" OnSelectedIndexChanged="ddlHoraInicio2_SelectedIndexChanged"></asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td style="font-weight: bold; width: 8%;" valign="middle" align="left" width="10%">Hora Final</td>
                                                                                                            <td valign="middle" align="left">
                                                                                                                <asp:DropDownList ID="ddlHoraFin2" runat="server" Width="80px" OnSelectedIndexChanged="ddlHoraFin2_SelectedIndexChanged"></asp:DropDownList>
                                                                                                            </td>
                                                                                                            <td align="left" style="width: 10%;" valign="middle">
                                                                                                                <asp:LinkButton ID="lnkRegresar" runat="server" OnClick="lnkRegresar_Click" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#0000C0"><< Regresar</asp:LinkButton></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="11" style="border-bottom: #c7d7ee 2px solid; height: 10px"
                                                                                                                valign="middle">
                                                                                                                <asp:HiddenField ID="hfFechaIni" runat="server" />
                                                                                                                <asp:HiddenField ID="hfFechaFin" runat="server" />
                                                                                                                <asp:HiddenField ID="hfHoraIni2" runat="server" />
                                                                                                                <asp:HiddenField ID="hfHoraFin2" runat="server" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="bottom">
                                                                                                <asp:Button ID="Button2" OnClick="Button2_Click" runat="server" Height="1px" Width="2px" BackColor="White" ForeColor="White" BorderStyle="None" BorderColor="White"></asp:Button><asp:Panel ID="PnlCabecera" runat="server" Width="783px">
                                                                                                    <table id="tblCabezera" border="1" cellpadding="0" cellspacing="0" style="width: 802px; color: white; border-top-style: none; border-right-style: none; border-left-style: none; background-color: #005cab; border-bottom-style: none; height: 29px;">
                                                                                                        <tr>
                                                                                                            <th style="width: 138px">Punto de Red</th>
                                                                                                            <th style="width: 120px">Taller</th>
                                                                                                            <th style="width: 86px">Fecha</th>
                                                                                                            <th style="width: 72px">Hora</th>
                                                                                                            <th style="width: 180px">Asesor de Servicio</th>
                                                                                                            <th style="width: 100px">Quick Service</th>
                                                                                                            <th style="width: 85px">Seleccionar</th>
                                                                                                            <th style="width: 15px"></th>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 230px">
                                                                                                <asp:Panel ID="pnlHorarioDisponible" runat="server" Height="210px" CssClass="tabla_horario" Width="800px" ScrollBars="Vertical" HorizontalAlign="Left">
                                                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:GridView ID="gvHorarioDisponible" runat="server" AutoGenerateColumns="False"
                                                                                                                BackColor="White" BorderColor="DimGray" BorderWidth="1px" Font-Bold="False" Font-Size="8pt"
                                                                                                                ForeColor="Black" HorizontalAlign="Left" OnRowCommand="gvHorarioDisponible_RowCommand"
                                                                                                                ShowHeader="False" Width="782px" SkinID="Grilla">
                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="PUNTO_RED" DataFormatString="{0:d}" HeaderText="Punto de Red">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="135px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="138px"></ItemStyle>
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="TALLER" HeaderText="Taller">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"></ItemStyle>
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="Fecha">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></ItemStyle>
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="HORA" HeaderText="Hora">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></ItemStyle>
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="ASESOR_SERVICIO" HeaderText="Asesor de Servicio">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="180px"></ItemStyle>
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:BoundField DataField="QUICK_SERVICE" HeaderText="Quick Service">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                                                                                                    </asp:BoundField>
                                                                                                                    <asp:ButtonField ImageUrl="~/Images/SRC/si.PNG" ButtonType="Image" HeaderText="Seleccionar">
                                                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="85px"></HeaderStyle>

                                                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></ItemStyle>
                                                                                                                    </asp:ButtonField>
                                                                                                                </Columns>
                                                                                                                <RowStyle BackColor="#F7F6F3" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                <EditRowStyle Font-Size="8pt" />
                                                                                                                <HeaderStyle Font-Bold="False" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                                                    Wrap="True" />
                                                                                                                <AlternatingRowStyle BackColor="White" Font-Size="8pt" />
                                                                                                            </asp:GridView>

                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>


                                                                                                </asp:Panel>
                                                                                                <asp:Label ID="lblFlgHorario2" runat="server" Font-Bold="True" Text="No hay horario disponible para la consuta." Font-Names="Verdana" ForeColor="DimGray" Font-Size="12pt"></asp:Label><br />
                                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td align="center" valign="middle">
                                                                                                                <asp:Panel ID="pnlLeyenda2" runat="server" BorderWidth="0px" Height="15px" Width="100%" BorderColor="White" HorizontalAlign="Left">
                                                                                                                    <img src="../Images/leyenda.jpg" />
                                                                                                                </asp:Panel>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 45px; border-top: #c7d7ee 2px solid; width: 100%;" valign="middle" align="center">
                                                                                                <asp:Button ID="btnReprogramarCita_2" OnClick="btnReprogramarCita_1_2_Click" runat="server" Font-Bold="True" Height="30px" CssClass="boton_02" Text="Reprogramar Cita" ForeColor="Black"></asp:Button>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="seccion_fila" align="center"></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </center>
                                                                        <center>
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <img src="../Images/Tabs/borabajo.gif" width="820" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </center>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Reprog_Cita" EventName="Click" />
                                    </Triggers>



                                </asp:UpdatePanel>



                            </asp:Panel>
                            <asp:HiddenField ID="HdfReprogramacion" runat="server" />
                            <br />


                            <cc1:ModalPopupExtender ID="mpColaEspera" runat="server"
                                BackgroundCssClass="modalBackground"
                                PopupControlID="PanelColaEspera"
                                TargetControlID="hfColaEspera" RepositionMode="None">
                            </cc1:ModalPopupExtender>


                            <!-- add controls for cola de espera -->

                            <asp:UpdatePanel ID="PanelColaEspera" runat="server">
                                <ContentTemplate>

                                    <asp:Panel ID="Panel3" runat="server" Width="850px" CssClass="modalPopup"
                                        Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
                                            <tr>
                                                <td style="width: 168px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;</td>
                                                <td style="width: 380px; background-repeat: repeat; background-image: url(../Images/flotante/popcab2.gif);">&nbsp;</td>
                                                <td style="width: 35px; background-repeat: no-repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 850px">
                                            <tr>
                                                <td style="padding-left: 10px; padding-right: 10px">
                                                    <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass="" Width="850px" BackColor="White">
                                                        <cc1:TabPanel ID="TabPanel2" runat="server">
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 830px">
                                                                    <tr valign="bottom">
                                                                        <td style="width: 160px" valign="bottom">
                                                                            <table id="Table2" runat="server"
                                                                                cellpadding="0" cellspacing="0" border="0" style="height: 20px; width: 96%;">
                                                                                <tr id="Tr1" runat="server">
                                                                                    <td id="Td1" runat="server">
                                                                                        <img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                                                    <td id="Td2" class="TabCabeceraOn" style="width: 154px" runat="server">CLIENTES EN COLA DE ESPERA</td>
                                                                                    <td id="Td3" style="width: 5px" runat="server">
                                                                                        <img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="right">
                                                                            <table cellpadding="0" cellspacing="0" border="0" style="margin-right: 5px">
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <asp:ImageButton ID="btnRegresarColaEspera" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                                                                            onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                                                                            onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresarColaEspera_Click" />

                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 830px; background-color: white;">
                                                                    <tr>
                                                                        <!-- Cabecera -->
                                                                        <td>
                                                                            <img alt="" src="../Images/Mantenimiento/fbarr.gif" width="830" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <!-- Cuerpo -->
                                                                            <br />
                                                                            <asp:GridView ID="gv_Cola_Espera" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                DataKeyNames="NID_CITA" SkinID="Grilla" OnPageIndexChanging="gv_Cola_Espera_PageIndexChanging"
                                                                                OnSelectedIndexChanged="gv_Cola_Espera_SelectedIndexChanged" Width="810px">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="nu_placa" HeaderText="Placa">
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="fe_prog" HeaderText="Fec. Registro" DataFormatString="{0:d}">
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="fe_prog" HeaderText="Hora Llegada" DataFormatString="{0:t}">
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="no_ape_paterno" HeaderText="Apellido(s) Contacto">
                                                                                        <HeaderStyle Width="17%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre(s) Contacto">
                                                                                        <HeaderStyle Width="17%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="nu_telefono" HeaderText="Telefono">
                                                                                        <HeaderStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="no_correo" HeaderText="Email Personal">
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="no_correo_trabajo" HeaderText="Email Trabajo">
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="no_correo_alter" HeaderText="Email Alternativo">
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                    </asp:BoundField>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="Seleccionar" SelectImageUrl="~/Images/SRC/si.PNG"
                                                                                        ShowCancelButton="False" ShowSelectButton="True">
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    </asp:CommandField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 20px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <!-- Pie -->
                                                                        <td style="width: 618px">
                                                                            <img alt="" src="../Images/Mantenimiento/fba.gif" width="830" /></td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    </cc1:TabContainer>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                            <asp:HiddenField ID="hfColaEspera" runat="server" />

                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>




    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>


    <!-- PopPup - Atender Cita  -->
    <asp:Panel ID="pnlAtenderCita" runat="server" Width="420px" CssClass="modalPopup"
        Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
            <tr>
                <td style="width: 135px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;</td>
                <td style="width: 70px; background-repeat: repeat; background-image: url(../Images/flotante/popcab2.gif);">&nbsp;</td>
                <td style="width: 27px; background-repeat: no-repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
            <tr style="display: none">
                <td align="right">
                    <asp:ImageButton ID="btnCloseCA" runat="server" /></td>
            </tr>
            <tr>
                <td style="padding-left: 10px; padding-right: 10px">
                    <table style="width: 400px" border="0" cellpadding="0" cellspacing="0">
                        <tr valign="bottom">
                            <td style="width: 100px">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                        <td class="TabCabeceraOn">ATENDER CITA</td>
                                        <td>
                                            <img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 300px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td style="display: none">
                                            <asp:UpdatePanel ID="upGvRecordatorio" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_AtenderCita" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td align="right" style="width: 100%;">
                                            <asp:Button ID="btnOpenCA" Text="" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                BorderWidth="0px" Height="0px" Width="0px" />
                                            <asp:ImageButton ID="btn_AtenderCita" runat="server" ImageUrl="~/Images/iconos/b-aceptar.gif"
                                                OnClick="btn_AtenderCita_Click" onmouseout="javascript:this.src='../Images/iconos/b-aceptar.gif'"
                                                onmouseover="javascript:this.src='../Images/iconos/b-aceptar2.gif'" ToolTip="Atender Cita"
                                                OnClientClick="javascript:return Fc_GuardarCitaAtendida();" />
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="btnRegresarCA" runat="server" ToolTip="Cerrar" ImageUrl="~/Images/iconos/b-cerrar.gif"
                                                OnClientClick="javascript: return Fc_CloseAtenderCita();" onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table style="width: 400px; vertical-align: top" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <img alt="" src="../Images/Mantenimiento/fbarr.gif" width="400" /></td>
                        </tr>
                        <tr>
                            <td style="background-color: #ffffff; padding-left: 5px; width: 400px; padding-right: 5px; vertical-align: top; padding-bottom: 5px; padding-top: 5px;"
                                valign="middle">
                                <table class="cuerponuevo" style="width: 100%;" border="0" cellspacing="2" cellpadding="2">
                                    <tr>
                                        <td style="width: 17%">
                                            <asp:Label ID="lblTextoPlacaCA" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtnroplaca" runat="server" ReadOnly="true" Columns="12" MaxLength="20" SkinID="txtob"
                                                Style="text-transform: uppercase;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>km. Inicial</td>
                                        <td>
                                            <asp:TextBox ID="txtkminicial" runat="server" Columns="8" MaxLength="7" SkinID="txtob"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Glosario</td>
                                        <td>
                                            <asp:TextBox ID="txtglosa" runat="server" TextMode="MultiLine" Height="31px" Width="277px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Puntualidad</td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkpuntualidad" runat="server" Text=" " /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img alt="" src="../Images/Mantenimiento/fba.gif" width="400" /></td>
                        </tr>
                    </table>

                </td>
            </tr>
        </table>

    </asp:Panel>
    <cc1:ModalPopupExtender ID="mpAtenderCita" runat="server" PopupControlID="pnlAtenderCita"
        BackgroundCssClass="modalBackground" TargetControlID="btnOpenCA" CancelControlID="btnCloseCA" />


    <asp:Panel ID="panelUpdateProgress" runat="server">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div class="overlay" />
                <div class="overlayContent">
                    <img src="../bootstrap/img/ajax-loader.gif" alt="Loading" border="1" />
                </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>

    <cc1:ModalPopupExtender ID="ModalProgress" runat="server"
        TargetControlID="panelUpdateProgress" BackgroundCssClass="modalBackground"
        PopupControlID="panelUpdateProgress" />

    <script language="javascript" type="text/javascript">

        function NASort(a, b) { return (a.innerHTML > b.innerHTML) ? 1 : -1; };

        function fc_mostrarFiltro() {

            $('#tblCabezera').append("<tr style='background-color:#ffffff;'><td></td><td><select id='cboTaller' class='cdll' style='width:130px;' onchange='fc_filtrarGrilla(this)' ></select></td><td><select id='cboFecha' class='cdll'  style='width:80px;' onchange='fc_filtrarGrilla(this)' ></select></td><td><select id='cboHora' class='cdll'   onchange='fc_filtrarGrilla(this)' ></select></td><td><select id='cboAsesor' class='cdll' style='width:200px;'  onchange='fc_filtrarGrilla(this)' ></select></td><td></td><td></td><td></td></tr>");

            var sTaller = '<option value="' + "" + '"></option>';
            var sFecha = '<option value="' + "" + '"></option>';
            var sHora = '<option value="' + "" + '"></option>';
            var sAsesor = '<option value="' + "" + '"></option>';

            var grid = document.getElementById("<%=gvHorarioDisponible.ClientID %>");
            for (i = 0; i < grid.rows.length - 1; i++) {
                if (sTaller.indexOf('<option>' + grid.rows[i].cells[1].innerHTML + '</option>') == -1) sTaller += '<option>' + grid.rows[i].cells[1].innerHTML + '</option>';
                if (sFecha.indexOf('<option>' + grid.rows[i].cells[2].innerHTML + '</option>') == -1) sFecha += '<option>' + grid.rows[i].cells[2].innerHTML + '</option>';
                if (sHora.indexOf('<option>' + grid.rows[i].cells[3].innerHTML + '</option>') == -1) sHora += '<option>' + grid.rows[i].cells[3].innerHTML + '</option>';
                if (sAsesor.indexOf('<option>' + grid.rows[i].cells[4].innerHTML + '</option>') == -1) sAsesor += '<option>' + grid.rows[i].cells[4].innerHTML + '</option>';
            }

            $("#cboTaller").append(sTaller.replace('  ', ' '));
            $("#cboFecha").append(sFecha);
            $("#cboHora").append(sHora);
            $("#cboAsesor").append(sAsesor.replace('  ', ' '));

            $('#cboTaller option').sort(NASort).appendTo('#cboTaller');
            $('#cboFecha option').sort(NASort).appendTo('#cboFecha');
            $('#cboHora option').sort(NASort).appendTo('#cboHora');
            $("#cboAsesor option").sort(NASort).appendTo('#cboAsesor');

            $("#cboTaller")[0].selectedIndex = 0;
            $("#cboFecha")[0].selectedIndex = 0;
            $("#cboHora")[0].selectedIndex = 0;
            $("#cboAsesor")[0].selectedIndex = 0;
        }

        function FC_ReprogramacionAutomatico() {
            setTimeout(FC_ActivarReprogAutomatico, 2000);
        }
        function FC_ActivarReprogAutomatico() {
            $('#' + '<%= btn_Reprog_Cita.ClientID%>').trigger("click");
        }

        function fc_filtrarGrilla() {

            var sTaller = $('#cboTaller').val();
            var sFecha = $('#cboFecha').val();
            var sHora = $('#cboHora').val();
            var sAsesor = $('#cboAsesor').val();
            var index = 0;

            $('#<%=gvHorarioDisponible.ClientID %> tr').each(function () {

                index = index + 1;

                if (document.getElementById("<%=gvHorarioDisponible.ClientID %>").rows.length == (index + 1))
                    $(this).show();
                else {
                    if (
                        ($(this).find("td").eq(1).html().replace('  ', ' ') == sTaller || sTaller == '') &&
                        ($(this).find("td").eq(2).html().replace('  ', ' ') == sFecha || sFecha == '') &&
                        ($(this).find("td").eq(3).html().replace('  ', ' ') == sHora || sHora == '') &&
                        ($(this).find("td").eq(4).html().replace('  ', ' ') == sAsesor || sAsesor == '')
                    )
                        $(this).show();
                    else
                        $(this).hide();
                }
            });
        }

        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
