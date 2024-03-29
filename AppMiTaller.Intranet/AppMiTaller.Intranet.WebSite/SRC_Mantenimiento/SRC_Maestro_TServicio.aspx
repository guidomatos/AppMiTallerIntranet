﻿<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_Maestro_TServicio.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_TServicio"
    Theme="Default"
    Title="Tipo Servicio"
    EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //para updatepogresss
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
        function Valida_Boton_Exportar() {
            var hf_exportaexcel = document.getElementById('<%=hf_exportaexcel.ClientID%>');
            if (hf_exportaexcel.value == "") {
                alert("No Hay Data para Exportar.");
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="Upd_GENERAL" runat="server">
        <ContentTemplate>
            <table cellpadding="2" cellspacing="0" width="1000" border="0" style="height: 47px">
                <tr>
                    <td>
                        <!--INICIO ICONOS DE ACCCION -->
                        <table id="tblIconos" border="0" cellpadding="0" cellspacing="0" class="TablaIconosMantenimientos">
                            <tr>
                                <td style="width: 100%" align="right">
                                    <asp:ImageButton ID="btnBuscarWarrant" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"
                                        OnClick="btnBuscarWarrant_Click" />
                                    <asp:ImageButton ID="btnVerDet" runat="server" ToolTip="Detalle" ImageUrl="~/Images/iconos/b-ordendespacho.png"
                                        onmouseover="javascript:this.src='../Images/iconos/b-ordendespacho2.png'" onmouseout="javascript:this.src='../Images/iconos/b-ordendespacho.png'" OnClick="btnVerDet_Click" />
                                    <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/iconos/b-nuevo.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'" OnClick="btnNuevo_Click" />
                                    <asp:ImageButton ID="BtnEditar" runat="server" ToolTip="Editar" ImageUrl="~/Images/iconos/b-modificarped.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-modificarped2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-modificarped.gif'" OnClick="BtnEditar_Click" />
                                    <!--
                                    <asp:ImageButton ID="btnExcel" runat="server" ToolTip="Excel" ImageUrl="~/Images/iconos/b-excel.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'"
                                        OnClientClick="javascript:return Valida_Boton_Exportar()" OnClick="btnExcel_Click" />
                                    -->
                                </td>
                            </tr>
                        </table>
                    </td>
                    <!--FIN ICONOS DE ACCCION -->
                </tr>
                <tr>
                    <td valign="top">
                        <cc1:TabContainer ID="TabContTServicio" runat="server" CssClass="" ActiveTabIndex="0">
                            <cc1:TabPanel runat="server" ID="TabPanel1">
                                <HeaderTemplate>
                                    <!-- TITULO DEL TAB-->
                                    <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                            <td class="TabCabeceraOn">Tipos de Servicios</td>
                                            <!-- AGREGAR TITULO DEL TAB-->
                                            <td>
                                                <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ContentTemplate>

                                    <div class="DivCuerpoTab">
                                        <!-- CUERPO -->
                                        <table cellspacing="0" cellpadding="0" width="980" border="0">

                                            <tr>
                                                <!-- CABECERA -->
                                                <td>
                                                    <img alt="" src="../Images/Tabs/borarriba.gif"></td>
                                            </tr>
                                            <tr style="height: 435px">
                                                <td style="background-color: #ffffff" valign="top">
                                                    <!-- CUERPO: AQUI COLOCAN SU CODIGO BIEN ESTRUCTURA -->
                                                    <table border="0" cellpadding="0" cellspacing="0" width="970px"
                                                        style="margin-left: 5px; margin-right: 5px">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl" runat="server" SkinID="lblCB">CRITERIOS DE BÚSQUEDA</asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" style="width: 100%" cellspacing="1" cellpadding="2" class="cbusqueda">
                                                                    <tr>
                                                                        <!-- COLOCAR CAMPOS DE BUSQUEDA / FILTROS-->
                                                                        <td style="width: 7%">Codigo</td>
                                                                        <td style="width: 20%">
                                                                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" Width="100px"></asp:TextBox></td>
                                                                        <td style="width: 15%">Nombre Tipo de Servicio</td>
                                                                        <td style="width: 25%">
                                                                            <asp:TextBox ID="txtNom" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 10%">Estado</td>
                                                                        <td>
                                                                            <asp:DropDownList ID="cboEstado" runat="server" Width="100px"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <!-- COLOCAR CAMPOS DE BUSQUEDA / FILTROS-->
                                                                </table>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="100%" /></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="padding-top: 15px;">
                                                                <asp:UpdatePanel ID="UpdBusqTS" runat="server">

                                                                    <ContentTemplate>

                                                                        <div style="overflow: auto; height: 370px; width: 965px">
                                                                            <asp:GridView ID="gdTServicio" runat="server" Width="965px" SkinID="Grilla" DataKeyNames="id_TipoServicio" OnRowDataBound="gdTServicio_RowDataBound" AutoGenerateColumns="False" OnPageIndexChanging="gdTServicio_PageIndexChanging" AllowPaging="True" OnRowCommand="gdTServicio_RowCommand" AllowSorting="True" OnSorting="gdTServicio_Sorting">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderStyle-Width="30%" DataField="co_tipo_servicio" HeaderText="Codigo" SortExpression="Co_tipo_servicio" />
                                                                                    <asp:BoundField HeaderStyle-Width="50%" DataField="no_tipo_servicio" HeaderText="Tipo de Servicio" SortExpression="No_tipo_servicio" ItemStyle-HorizontalAlign="Left" />
                                                                                    <asp:BoundField DataField="fl_activo" HeaderText="Estado" SortExpression="Fl_activo" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>

                                                                        <asp:HiddenField ID="txh_id_TipoServicio" runat="server" />
                                                                        <asp:HiddenField ID="hf_exportaexcel" runat="server" />

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnBuscarWarrant" EventName="Click"></asp:AsyncPostBackTrigger>
                                                                        <asp:AsyncPostBackTrigger ControlID="gdTServicio" EventName="Sorting" />
                                                                        <asp:AsyncPostBackTrigger ControlID="gdTServicio" EventName="PageIndexChanging" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #ffffff;">
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <!-- Pie -->
                                                <td>
                                                    <img src="../Images/Tabs/borabajo.gif"></td>
                                            </tr>
                                        </table>

                                    </div>

                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>



    <!-- progress -->
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

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>
</asp:Content>
