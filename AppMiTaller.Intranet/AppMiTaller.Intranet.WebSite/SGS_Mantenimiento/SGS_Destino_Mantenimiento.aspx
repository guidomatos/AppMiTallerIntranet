<%@ Import Namespace="AppMiTaller.Intranet.BE" %>

<%@ Page Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true"
    CodeFile="SGS_Destino_Mantenimiento.aspx.cs" Inherits="SGS_Mantenimiento_SGS_Destino_Mantenimiento"
    Theme="Default" %>

<%@ MasterType VirtualPath="~/Mantenimientos.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboDistrito.ascx" TagName="ComboDistrito" TagPrefix="uc5" %>
<%@ Register Src="~/SGS_UserControl/ComboProvincia.ascx" TagName="ComboProvincia" TagPrefix="uc6" %>
<%@ Register Src="~/SGS_UserControl/ComboDepartamento.ascx" TagName="ComboDepartamento" TagPrefix="uc4" %>
<%@ Register Src="~/SGS_UserControl/ComboTipoDestino.ascx" TagName="ComboTipoDestino" TagPrefix="uc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="ComboEstado" TagPrefix="uc2" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
    var mstrError = "";
        function Fc_Valida() {

            if (fc_Trim(document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value) == "") {
                mstrError += mstrDebeSeleccionar + "tipo destino.\n";
            }

            if (fc_Trim(document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value) == "AL") {
                if (fc_Trim(document.getElementById("<%=this.txtCodAduana.ClientID %>").value) == "") {
                    mstrError += mstrDebeIngresar + "cod. aduana.\n";
                }
                else if (!fc_Trim(document.getElementById("<%=this.txtCodAduana.ClientID %>").value).match(RE_CODIGO)) {
                    mstrError += mstrElCampo + "cod. aduana" + mstrCodigo;
                }
            }


            if (fc_Trim(document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value) == "CO") {
                if (fc_Trim(document.getElementById("<%=this.txtvitrinaIdeal.ClientID %>").value) == "") {
                    mstrError += mstrDebeIngresar + "vitrina ideal.\n";
                }
                else if (!fc_Trim(document.getElementById("<%=this.txtvitrinaIdeal.ClientID %>").value).match(RE_SOLONRO)) {
                    mstrError += mstrElCampo + "vitrina ideal" + mstrReSoloNro;
                }
            }

            if (fc_Trim(document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value) == "SU") {
                if (fc_Trim(document.getElementById("<%=this.txtvitrinaIdeal.ClientID %>").value) == "") {
                    mstrError += mstrDebeIngresar + "vitrina ideal.\n";
                }
                else if (!fc_Trim(document.getElementById("<%=this.txtvitrinaIdeal.ClientID %>").value).match(RE_SOLONRO)) {
                    mstrError += mstrElCampo + "vitrina ideal" + mstrReSoloNro;
                }
            }

            if (fc_Trim(document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value) == "TE") {
                if (fc_Trim(document.getElementById("<%=this.txtNroTerminal.ClientID %>").value) == "") {
                    mstrError += mstrDebeIngresar + "el número de terminal.\n";
                }
                else if (!fc_Trim(document.getElementById("<%=this.txtNroTerminal.ClientID %>").value).match(RE_SOLONRO)) {
                    mstrError += mstrElCampo + "número de terminal" + mstrReSoloNro;
                }
            }

            if (fc_Trim(document.getElementById("<%=this.txtDescripcion.ClientID %>").value) == "") {
                mstrError += mstrDebeIngresar + "descripción.\n";
            }
            else if (!fc_Trim(document.getElementById("<%=this.txtDescripcion.ClientID %>").value).match(RE_ALAFANUMERICO)) {
                mstrError += mstrElCampo + "descripción" + mstrReAlfanumerico;
            }


            if (fc_Trim(document.getElementById("<%=this.txtNombreCorto.ClientID %>").value) != "") {
                if (!fc_Trim(document.getElementById("<%=this.txtNombreCorto.ClientID %>").value).match(RE_ALAFANUMERICO)) {
                    mstrError += mstrElCampo + "nombre corto" + mstrReAlfanumerico;
                }
            }

            if (fc_Trim(document.getElementById("<%=this.txtDireccion.ClientID %>").value) == "") {
                mstrError += mstrDebeIngresar + "dirección.\n";
            }

            if (fc_Trim(document.getElementById("<%=this.ComboDepartamento1.ClientID %>_cboDepartamento").value) == "") {
                mstrError += mstrDebeSeleccionar + "departamento.\n";
            }
            if (fc_Trim(document.getElementById("<%=this.ComboProvincia1.ClientID %>_cboProvincia").value) == "") {
                mstrError += mstrDebeSeleccionar + "provincia.\n";
            }
            if (fc_Trim(document.getElementById("<%=this.ComboDistrito1.ClientID %>_cboDistrito").value) == "") {
                mstrError += mstrDebeSeleccionar + "distrito.\n";
            }

            if (fc_Trim(document.getElementById("<%=this.ComboEstado1.ClientID %>_cboEstado").value) == "") {
                mstrError += mstrDebeSeleccionar + "estado.\n";
            }


            if (fc_Trim(document.getElementById("<%=this.txtContacto.ClientID %>").value) != "") {
                if (!fc_Trim(document.getElementById("<%=this.txtContacto.ClientID %>").value).match(RE_ALAFANUMERICO)) {
                    mstrError += mstrElCampo + "contacto" + mstrReAlfanumerico;
                }
            }


            if (fc_Trim(document.getElementById("<%=this.txtCorreo.ClientID %>").value) != "") {
                if (!fc_Trim(document.getElementById("<%=this.txtCorreo.ClientID %>").value).match(RE_EMAIL)) {
                    mstrError += mstrElCampo + "correo" + mstrCorreo;
                }
            }

            if (fc_Trim(document.getElementById("<%=this.txtDestinoGuia.ClientID %>").value) == "") {
                mstrError += mstrDebeIngresar + "destino guía.\n";
            }
            else if (!fc_Trim(document.getElementById("<%=this.txtDestinoGuia.ClientID %>").value).match(RE_ALAFANUMERICO)) {
                mstrError += mstrElCampo + "destino guía" + mstrReAlfanumerico;
            }

            if (fc_Trim(document.getElementById("<%=this.txtTelefono.ClientID %>").value) != "") {
                if (!fc_Trim(document.getElementById("<%=this.txtTelefono.ClientID %>").value).match(RE_NUMERO_TELEFONO)) {
                    mstrError += mstrElCampo + "teléfono" + mstrTelefono;
                }
            }

            if (fc_Trim(document.getElementById("<%=this.txtCelular.ClientID %>").value) != "") {
                if (!fc_Trim(document.getElementById("<%=this.txtCelular.ClientID %>").value).match(RE_NUMERO_TELEFONO)) {
                    mstrError += mstrElCampo + "celular" + mstrTelefono;
                }
            }


            if (mstrError != "") {
                alert(mstrError);
                mstrError = "";
                return false;
            }

            return confirm(mstrSeguroGrabar);
        }
    
        function Fc_Chekea() {

            document.getElementById("<%=this.tdVitrina1.ClientID %>").style.display = "none";
            document.getElementById("<%=this.tdVitrina2.ClientID %>").style.display = "none";
            document.getElementById("<%=this.tdCentral1.ClientID %>").style.display = "none";
            document.getElementById("<%=this.tdCentral2.ClientID %>").style.display = "none";

            document.getElementById("<%=this.tdTerminal1.ClientID %>").style.display = "none";
            document.getElementById("<%=this.tdTerminal2.ClientID %>").style.display = "none";

            document.getElementById("<%=this.tdCotizacion1.ClientID %>").style.display = "none";
            document.getElementById("<%=this.tdCotizacion2.ClientID %>").style.display = "none";

            if (document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value == "CL") {
                document.getElementById("<%=this.chkAlmacenCampo.ClientID %>").disabled = true;
            }
            else if (document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value == "TE") {
                document.getElementById("<%=this.chkAlmacenCampo.ClientID %>").checked = false;
                document.getElementById("<%=this.chkAlmacenCampo.ClientID %>").disabled = true;
                document.getElementById("<%=this.tdTerminal1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdTerminal2.ClientID %>").style.display = "";
            }
            else {
                document.getElementById("<%=this.chkAlmacenCampo.ClientID %>").disabled = false;
            }

            if (document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value == "AL") {
                document.getElementById("<%=this.tdAduana1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdAduana2.ClientID %>").style.display = "";
            }
            else {
                document.getElementById("<%=this.tdAduana1.ClientID %>").style.display = "none";
                document.getElementById("<%=this.tdAduana2.ClientID %>").style.display = "none";
            }

            if (document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value == "CO") {
                document.getElementById("<%=this.tdVitrina1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdVitrina2.ClientID %>").style.display = "";

                document.getElementById("<%=this.tdCotizacion1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdCotizacion2.ClientID %>").style.display = "";
            }

            if (document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value == "SU") {
                document.getElementById("<%=this.tdVitrina1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdVitrina2.ClientID %>").style.display = "";

                document.getElementById("<%=this.tdCotizacion1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdCotizacion2.ClientID %>").style.display = "";
            }

            if (document.getElementById("<%=this.ComboTipoDestino1.ClientID %>_cboTipoDestino").value == "DE") {
                document.getElementById("<%=this.tdCentral1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdCentral2.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdCtrlAlmacenaje1.ClientID %>").style.display = "";
                document.getElementById("<%=this.tdCtrlAlmacenaje2.ClientID %>").style.display = "";
            }
            else {
                document.getElementById("<%=this.tdCentral1.ClientID %>").style.display = "none";
                document.getElementById("<%=this.tdCentral2.ClientID %>").style.display = "none";
                document.getElementById("<%=this.tdCtrlAlmacenaje1.ClientID %>").style.display = "none";
                document.getElementById("<%=this.tdCtrlAlmacenaje2.ClientID %>").style.display = "none";
            }
        }
    </script>

    <asp:HiddenField runat="server" ID="txhIdDestino" />
    <cc1:TabContainer ID="tabContMarca" runat="server" ActiveTabIndex="0" CssClass=""
        OnActiveTabChanged="tabContMarca_ActiveTabChanged" AutoPostBack="true">
        <cc1:TabPanel ID="tabMarca" runat="server" CssClass="">
            <HeaderTemplate>
                <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                        <td class="TabCabeceraOff" onmouseover="javascript: onTabCabeceraOver('0');" onmouseout="javascript: onTabCabeceraOut('0');">
                            <%= this.tipoAccion %>
                            Destino
                        </td>
                        <td>
                            <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ContentTemplate>
                <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnGrabar" runat="server" ImageUrl="~/Images/iconos/b-guardar.gif"
                                OnClick="btnGrabar_Click" OnClientClick="javaScript: return Fc_Valida()" ToolTip="Grabar"
                                onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" />
                            <asp:ImageButton ID="btnRegresar" runat="server" ImageUrl="~/Images/iconos/b-regresar.gif"
                                OnClick="btnRegresar_Click" ToolTip="Regresar" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" />
                        </td>
                    </tr>
                </table>
                <table width="800px"  cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                    </tr>
                    <tr>
                        <td style="background-color: #ffffff; vertical-align: top; height: 450px;">
                            <table width="785px" cellpadding="2" cellspacing="1" border="0" class="cuerponuevo"
                                style="margin-left: 5px; margin-right: 5px; margin-top: 5px; height: 280px;">
                                <tr>
                                    <td style="width: 100px">
                                        Tipo Destino</td>
                                    <td style="width: 303px">
                                        <uc1:ComboTipoDestino ID="ComboTipoDestino1" OnSelectedIndexChanged="ComboTipo_SelectedIndexChanged"
                                            runat="server" OnChange="javascript: return Fc_Chekea();" Width="245" AutoPostBack="false" />
                                    </td>
                                    <td style="width: 110px">
                                        &nbsp;</td>
                                    <td style="width: 272px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Descripción</td>
                                    <td>
                                        <asp:TextBox ID="txtDescripcion" SkinID="txtob" runat="server" Width="240px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Nro. Documento</td>
                                    <td>
                                        <asp:UpdatePanel ID="upRucImportador" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtRuc" SkinID="txtob" runat="server" MaxLength="11" Width="85px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboPuntoVenta" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nombre Corto</td>
                                    <td><asp:TextBox ID="txtNombreCorto" MaxLength="100" runat="server" Width="240px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Dirección</td>
                                    <td>
                                        <asp:TextBox ID="txtDireccion" SkinID="txtob" runat="server" Width="240px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Departamento</td>
                                    <td>
                                        <uc4:ComboDepartamento ID="ComboDepartamento1" runat="server" OnSelectedIndexChanged="ComboDepartamento1_OnSelectedIndexChanged"
                                            AutoPostBack="true" Width="150" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Provincia</td>
                                    <td>
                                        <asp:UpdatePanel ID="upProvinciaImportador" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <uc6:ComboProvincia ID="ComboProvincia1" runat="server" OnSelectedIndexChanged="ComboProvincia1_OnSelectedIndexChanged"
                                                    AutoPostBack="true" Width="150"></uc6:ComboProvincia>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ComboDepartamento1" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        Distrito</td>
                                    <td>
                                        <asp:UpdatePanel ID="upDistritoImportador" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <uc5:ComboDistrito ID="ComboDistrito1" runat="server" Width="150"></uc5:ComboDistrito>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ComboProvincia1" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="ComboDepartamento1" EventName="SelectedIndexChanged">
                                                </asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Contacto
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContacto" runat="server" Width="240px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Correo
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCorreo" runat="server" Width="215px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Teléfono
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelefono" MaxLength="15" runat="server" Width="110px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Celular
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCelular" MaxLength="15" runat="server" Width="110px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Destino Guía</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDestinoGuia" MaxLength="100" runat="server" Width="635px" SkinID="txtob"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Almacén
                                    </td>
                                    <td>
                                        Campo&nbsp;<asp:CheckBox ID="chkAlmacenCampo" runat="server" />
                                        Aduanero&nbsp;<asp:CheckBox ID="chkAlmacenAduanero" runat="server" />
                                        
                                    </td>
                                    <td id="tdAduana1" runat="server" style="display: none">
                                        Cod. Aduana</td>
                                    <td id="tdAduana2" runat="server" style="display: none">
                                        <asp:TextBox ID="txtCodAduana" SkinID="txtob" runat="server" Width="68px" MaxLength="3"></asp:TextBox>
                                    </td>
                                    <td id="tdVitrina1" runat="server" style="display: none;">
                                        Vitrina Ideal</td>
                                    <td id="tdVitrina2" runat="server" style="display: none;">
                                        <asp:TextBox ID="txtvitrinaIdeal" SkinID="txtob" runat="server" Width="68px" MaxLength="3"></asp:TextBox>
                                    </td>
                                    <td id="tdCentral1" runat="server" style="display: none;">
                                        Almacen Central</td>
                                    <td id="tdCentral2" runat="server" style="display: none;">
                                        <asp:CheckBox ID="chkCentral" runat="server" />
                                    </td>
                                    
                                    
                                    
                                    <td id="tdTerminal1" runat="server" style="display: none;">
                                        Nro Terminal</td>
                                    <td id="tdTerminal2" runat="server" style="display: none;">
                                        <asp:TextBox ID="txtNroTerminal" SkinID="txtob" runat="server" Width="68px" MaxLength="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Almacen Satelite</td>
                                    <td>
                                        <asp:CheckBox ID="chkFlagAlsa" runat="server" /></td>
                                         <td>
                                        Carrocero</td>
                                    <td>
                                        <asp:CheckBox ID="chkCarrocero" runat="server" /></td>                                                                                                                                                   
                                </tr>      
                                <tr>
                                    <td>
                                        Estado
                                    </td>
                                    <td>
                                        <span id="spanCboEstado1" runat="server"></span>
                                    </td>
                                    <td id="tdCotizacion1" runat="server" style="display: none;">
                                        Validar Cotizacion</td>
                                    <td id="tdCotizacion2" runat="server" style="display: none;">
                                        <asp:CheckBox ID="chkCotizacion" runat="server" />
                                    </td>
                                <!-- @002 I -->
                                    <td id="tdCtrlAlmacenaje1" runat="server" style="display: none;">
                                        Control Almacenaje
                                    </td>
                                    <td id="tdCtrlAlmacenaje2" runat="server" style="display: none;">
                                        <asp:CheckBox ID="chkCtrlAlmacenaje" runat="server" />
                                    </td>
                                <!-- @002 -->
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>

    <script type="text/javascript">
                 if ( "<%=this.indiceTabOn %>" != "" ) setTabCabeceraOn("<%=this.indiceTabOn %>");         
    </script>

</asp:Content>
