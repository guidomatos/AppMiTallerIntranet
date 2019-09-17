<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_Mantenimiento_Parametros_Detalle.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Mantenimiento_Parametros_Detalle"
    EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
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

        function changeTab(index) {
            //Get a Handle to the Tab Behavior 
            var tabBehavior = $get('<%=this.tabContMantParametros.ClientID%>').control;
            var tabIndex = parseInt(index);
            //Set the Currently Visible Tab 
            document.getElementById("ctl00_ContentPlaceHolder1_hid_tab1").value = tabIndex;
            tabBehavior.set_activeTabIndex(tabIndex);
        }

    </script>

    <asp:UpdatePanel ID="upd_GENERAL" runat="server">
        <ContentTemplate>
            <!--HIDENNS-->
            <input id="hid_tab1" type="hidden" runat="server" />
            <table cellpadding="2" cellspacing="0" width="1000" border="0">
                <tr>
                    <td>
                        <!--INICIO ICONOS DE ACCCION   -->
                        <table id="tblIconos" border="0" class="TablaIconosMovimientos">
                            <tr>
                                <td style="width: 100%" align="right">
                                    <asp:ImageButton ID="btnGrabar" runat="server" ToolTip="Grabar" ImageUrl="~/Images/iconos/b-guardar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                        onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClick="btnGrabar_Click" />
                                    <asp:ImageButton ID="btnRegresar" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <!--FIN ICONOS DE ACCCION -->
                </tr>
                <tr>
                    <td valign="top">
                        <asp:TabContainer ID="tabContMantParametros" runat="server" ActiveTabIndex="0" AutoPostBack="True"
                            CssClass="">
                            <asp:TabPanel ID="tabHorTaller" runat="server">
                                <HeaderTemplate>
                                    <!-- TITULO DEL TAB-->
                                    <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" style="height: 21px;" /></td>
                                            <td class="TabCabeceraOffForm" onmouseover="javascript: return onTabCabeceraOverForm('0');" onmouseout="javascript: return onTabCabeceraOutForm('0');" style="height: 21px;">Detalle Parámetros</td>
                                            <!-- AGREGAR TITULO DEL TAB-->
                                            <td>
                                                <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der_off_plom.gif" style="height: 21px;" /></td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div class="DivCuerpoTab">
                                        <table cellspacing="0" cellpadding="0" width="960" border="0">
                                            <tbody>
                                                <tr>
                                                    <!-- CABECERA -->
                                                    <td>
                                                        <img src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr style="height: 435px">
                                                    <td style="background-color: #ffffff" valign="top">
                                                        <!-- CUERPO: AQUI COLOCAN SU CODIGO BIEN ESTRUCTURA -->
                                                        <asp:UpdatePanel ID="upd_hor_defecto" runat="server">
                                                            <ContentTemplate>
                                                                <table style="margin-left: 5px; margin-right: 5px" cellspacing="0" cellpadding="0" width="950" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl1" runat="server" SkinID="lblCB">HORARIO POR DEFECTO DEL TALLER</asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <!-- CUERPO: AQUI COLOCAN SU CODIGO BIEN ESTRUCTURA -->
                                                                                <table style="margin-left: 5px; margin-right: 5px" cellspacing="0" cellpadding="0" width="950" border="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table style="margin-left: 5px; margin-right: 5px" cellspacing="5" cellpadding="5" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td valign="top">
                                                                                                                <asp:Label ID="lbl3" runat="server" Text="Dias de la Semana : "></asp:Label></td>
                                                                                                            <td valign="top">
                                                                                                                <asp:ListBox ID="lst_dias_off" runat="server" Height="120px" Width="150px" SelectionMode="Multiple"></asp:ListBox></td>
                                                                                                            <td valign="middle">
                                                                                                                <asp:Button ID="btn_on" OnClick="btn_on_Click" runat="server" Text=">>"></asp:Button><br />
                                                                                                                <asp:Button ID="btn_off" OnClick="btn_off_Click" runat="server" Text="<<"></asp:Button>
                                                                                                            </td>
                                                                                                            <td valign="top">
                                                                                                                <asp:ListBox ID="lst_dias_on" runat="server" Height="120px" Width="150px" SelectionMode="Multiple"></asp:ListBox></td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table style="margin-left: 5px; margin-right: 5px" cellspacing="5" cellpadding="5" border="0">
                                                                                                    <tbody>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lbl4" runat="server" Width="110px" Text="Horas al Día : "></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lbl5" runat="server" Text="De "></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="ddl_hora_de" runat="server"></asp:DropDownList></td>
                                                                                                            <td style="width: 10px"></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lbl6" runat="server" Text=" a "></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="ddl_hora_a" runat="server"></asp:DropDownList></td>
                                                                                                        </tr>
                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="tabContMantParametros" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
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
    <asp:ModalPopupExtender ID="ModalProgress" runat="server"
        TargetControlID="panelUpdateProgress" BackgroundCssClass="modalBackground"
        PopupControlID="panelUpdateProgress" />

    <!-- modal popup MSGBOX  -->
    <asp:ModalPopupExtender ID="popup_msgbox_confirm" DropShadow="True" BackgroundCssClass="modalBackground" TargetControlID="hid_popupmsboxconfirm"
        PopupControlID="upd_pn_msbox_confirm" runat="server" DynamicServicePath="" Enabled="True">
    </asp:ModalPopupExtender>
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
                                <table cellpadding="5" cellspacing="5" align="left">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_mensajebox" runat="server" Text="xxxx"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td>
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

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
