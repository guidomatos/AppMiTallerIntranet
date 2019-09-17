<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_Maestro_Detalle_Modelos.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_Modelos"
    EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        var ModalProgress ='<%= ModalProgress.ClientID %>';

        //Código JavaScript incluido en un archivo denominado jsUpdateProgress.js 
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args) {
            // muestra el popup 
            $find(ModalProgress).show();
        }
        function endReq(sender, args) {
            //  esconde el popup 
            $find(ModalProgress).hide();
        }
    </script>

    <asp:UpdatePanel ID="upd_GENERAL" runat="server">
        <ContentTemplate>
            <table cellpadding="2" cellspacing="0" width="1000" border="0">
                <tr>
                    <td>
                        <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
                            <tr>
                                <td style="width: 100%" align="right">
                                    <asp:ImageButton ID="btnEditar" onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'"
                                        onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'" runat="server"
                                        ImageUrl="~/Images/iconos/b-registrofecha.gif"
                                        ToolTip="Editar" OnClick="btnEditar_Click" />
                                    <asp:ImageButton ID="btnGrabar" runat="server" ToolTip="Grabar" ImageUrl="~/Images/iconos/b-guardar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClick="btnGrabar_Click" />
                                    <asp:ImageButton ID="btnRegresar" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">


                        <cc1:TabContainer ID="tabMantMaesModelo" runat="server" ActiveTabIndex="0" CssClass=""
                            OnActiveTabChanged="tabMantMaesModelo_ActiveTabChanged" AutoPostBack="true">
                            <cc1:TabPanel runat="server" ID="tabDatosGenerales">
                                <HeaderTemplate>
                                    <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                            <td class="TabCabeceraOffForm" onmouseover="javascript: return onTabCabeceraOverForm('0');" onmouseout="javascript: return onTabCabeceraOutForm('0');">Detalle Generales</td>
                                            <!-- AGREGAR TITULO DEL TAB-->
                                            <td>
                                                <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table cellspacing="0" cellpadding="0" width="980" border="0">
                                        <tr>
                                            <td>
                                                <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; height: 450px; background-color: #ffffff;">
                                                <table style="margin-left: 5px; margin-right: 5px;" cellspacing="1" cellpadding="1" width="970" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="lineadatos">
                                                                <asp:Label ID="Label2" runat="server" SkinID="lblcb">DATOS GENERALES</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdDG" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel ID="p_DG" runat="server" Width="125px">
                                                                            <table style="width: 966px;" cellspacing="1" cellpadding="1" border="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table cellspacing="1" cellpadding="2" border="0" class="textotab" width="100%">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td style="width: 140px">
                                                                                                        <asp:Label ID="Label4" runat="server" Width="90px" Text="Código"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_cod" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 140px">Marca</td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_marca" runat="server"></asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 140px">Nombre Modelo</td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_nommod" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 140px">Negocio</td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_negocio" runat="server"></asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 140px">Familia</td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_familia" runat="server"></asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <asp:HiddenField ID="hid_nid_parametro" runat="server"></asp:HiddenField>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="tabMantMaesModelo" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="lineadatos">&nbsp;</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel runat="server" ID="tabParametros">
                                <HeaderTemplate>
                                    <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <img id="img1" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                            <td class="TabCabeceraOffForm" onmouseover="javascript: return onTabCabeceraOverForm('0');" onmouseout="javascript: return onTabCabeceraOutForm('0');">Parametros</td>
                                            <!-- AGREGAR TITULO DEL TAB-->
                                            <td>
                                                <img id="img2" alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ContentTemplate>
                                    <table cellspacing="0" cellpadding="0" width="980" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                    <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1" cellpadding="1" width="970" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td class="lineadatos">
                                                                    <asp:Label ID="Label1" runat="server" SkinID="lblcb">PARAMETROS</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdParam" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="PP" runat="server" Width="125px">
                                                                                <table style="width: 966px;" cellspacing="0" cellpadding="0" border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellspacing="1" cellpadding="2" border="0" class="textotab" width="100%">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td style="width: 23%">Kilometraje promedio por defecto</td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txt_kildefecto" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Kilometraje de servicio </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txt_kilservicio" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Periodo de servicio</td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txt_perservicio" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Minimo de días para Reserva de citas</td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txt_minreservacita" runat="server" Width="100px" MaxLength="4"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="tabMantMaesModelo" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lineadatos">&nbsp;</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; text-align: center; vertical-align: middle;" id="DIV_PB">
                    <center>
                        <table id="TBL_WAIT" border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px">
                            <tr>
                                <td style="width: 50px">
                                    <asp:Image ID="Image56" runat="server" ImageUrl="~/Images/SRC/Espera.gif"></asp:Image></td>
                                <td style="font-size: 12px; color: dimgray; font-style: normal; font-family: verdana; text-align: center; font-variant: normal">Procesando...</td>
                            </tr>
                        </table>
                    </center>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server"
        TargetControlID="panelUpdateProgress" BackgroundCssClass="modalBackground"
        PopupControlID="panelUpdateProgress" />

    <!-- modal popup MSGBOX  -->
    <cc1:ModalPopupExtender ID="popup_msgbox_confirm" DropShadow="True" BackgroundCssClass="modalBackground" TargetControlID="hid_popupmsboxconfirm"
        PopupControlID="upd_pn_msbox_confirm" runat="server" Enabled="True">
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
                        <td>
                            <asp:Panel ID="Panel2" runat="server">
                                <table cellpadding="5" cellspacing="5" align="left" width="280px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_mensajebox" runat="server" Text="xxxx"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btn_msgboxconfir_aceptar" CssClass="btn" runat="server" Text="ACEPTAR" OnClick="btn_msgboxconfir_no_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!--</asp:Panel>-->

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>

