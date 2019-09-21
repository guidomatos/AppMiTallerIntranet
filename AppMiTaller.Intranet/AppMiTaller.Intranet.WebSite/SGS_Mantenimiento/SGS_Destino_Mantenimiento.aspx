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
                mstrError += mstrDebeSeleccionar + "tipo ubicacion.\n";
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

            if (mstrError != "") {
                alert(mstrError);
                mstrError = "";
                return false;
            }

            return confirm(mstrSeguroGrabar);
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
                                        Tipo Ubicacion</td>
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
                                        <asp:TextBox ID="txtRuc" SkinID="txtob" runat="server" MaxLength="11" Width="85px"></asp:TextBox>
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
                                        Estado
                                    </td>
                                    <td>
                                        <span id="spanCboEstado1" runat="server"></span>
                                    </td>
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