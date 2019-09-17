<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_Maestro_Modelos.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_Modelos"
    EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
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

        function Valida_Boton_Exportar() {
            var hf_exportaexcel = document.getElementById('<%=hf_exportaexcel.ClientID%>');
            if (hf_exportaexcel.value == "") {
                alert("No Hay Data para Exportar.");
                return false;
            }
        }

    </script>
    <asp:UpdatePanel ID="upd_GENERAL" runat="server">
        <ContentTemplate>
            <table cellpadding="2" cellspacing="0" width="1000" border="0" style="height: 47px">
                <tr>
                    <td>
                        <!--INICIO ICONOS DE ACCCION -->
                        <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
                            <tr>
                                <td style="width: 100%" align="right">
                                    <asp:ImageButton ID="btnBuscarWarrant" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"
                                        OnClick="btnBuscarWarrant_Click" />
                                    <asp:ImageButton ID="btnVerDet" runat="server" ToolTip="Detalle" ImageUrl="~/Images/iconos/b-ordendespacho.png"
                                        onmouseover="javascript:this.src='../Images/iconos/b-ordendespacho2.png'" onmouseout="javascript:this.src='../Images/iconos/b-ordendespacho.png'" OnClick="btnVerDet_Click" /><asp:ImageButton ID="BtnEditar" runat="server" ToolTip="Editar" ImageUrl="~/Images/iconos/b-modificarped.gif"
                                            onmouseover="javascript:this.src='../Images/iconos/b-modificarped2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-modificarped.gif'" OnClick="BtnEditar_Click" />
                                    <asp:ImageButton ID="btnExcel" runat="server" ToolTip="Excel" ImageUrl="~/Images/iconos/b-excel.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'" OnClick="btnExcel_Click" OnClientClick="javascript:return Valida_Boton_Exportar()" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <!--FIN ICONOS DE ACCCION -->
                </tr>
                <tr>
                    <td valign="top">
                        <cc1:TabContainer ID="tabMantMaesModelos" runat="server" CssClass="" ActiveTabIndex="0">
                            <cc1:TabPanel runat="server" ID="tabListMaesModelos">
                                <HeaderTemplate>
                                    <!-- TITULO DEL TAB-->
                                    <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                            <td class="TabCabeceraOn">Listado de Modelos</td>
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
                                                    <img src="../Images/Tabs/borarriba.gif"></td>
                                            </tr>
                                            <tr style="height: 420px">
                                                <td style="background-color: #ffffff" valign="top">
                                                    <!-- CUERPO: AQUI COLOCAN SU CODIGO BIEN ESTRUCTURA -->
                                                    <table border="0" cellpadding="0" cellspacing="0" width="970px"
                                                        style="margin-left: 5px; margin-right: 5px">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" SkinID="lblCB">CRITERIOS DE BÚSQUEDA</asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" style="width: 965px" cellspacing="1" cellpadding="2" class="cbusqueda">
                                                                    <tr>
                                                                        <!-- COLOCAR CAMPOS DE BUSQUEDA / FILTROS-->
                                                                        <td style="width: 9%">Código Modelo </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_codmodelo" runat="server" MaxLength="20"></asp:TextBox></td>
                                                                        <td style="width: 5%">Marca</td>
                                                                        <td style="width: 203px">
                                                                            <asp:DropDownList ID="ddl_marca" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_marca_SelectedIndexChanged" Width="180px">
                                                                            </asp:DropDownList></td>
                                                                        <td>Negocio</td>
                                                                        <td style="width: 203px">
                                                                            <asp:DropDownList ID="ddl_negocio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_negocio_SelectedIndexChanged" Width="180px">
                                                                            </asp:DropDownList></td>
                                                                        <td>Familia</td>
                                                                        <td style="width: 203px">
                                                                            <asp:DropDownList ID="ddl_familia" runat="server" Width="180px"></asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Modelo</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txt_nommodelo" runat="server" MaxLength="50"></asp:TextBox></td>
                                                                        <td>Estado</td>
                                                                        <td>
                                                                            <asp:DropDownList ID="cboEstado" runat="server" Width="155px"></asp:DropDownList></td>

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

                                                                <asp:UpdatePanel ID="UpdBusqueda" runat="server">
                                                                    <ContentTemplate>
                                                                        <div style="overflow: auto; height: 350px; width: 965px">
                                                                            <asp:GridView ID="gdModelos" runat="server" Width="965px" SkinID="Grilla"
                                                                                DataKeyNames="nid_modelo,co_modelo,co_marca,co_negocio,co_familia,no_modelo"
                                                                                AutoGenerateColumns="False"
                                                                                AllowPaging="True"
                                                                                OnPageIndexChanging="gdModelos_PageIndexChanging"
                                                                                OnRowDataBound="gdModelos_RowDataBound"
                                                                                AllowSorting="True"
                                                                                OnSorting="gdModelos_Sorting">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="no_marca" HeaderText="Marca" SortExpression="no_marca"></asp:BoundField>
                                                                                    <asp:BoundField DataField="co_modelo" HeaderText="Codigo" SortExpression="co_modelo">
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="no_modelo" HeaderText="Nombre" SortExpression="no_modelo"></asp:BoundField>
                                                                                    <asp:BoundField DataField="no_negocio" HeaderText="Negocio" SortExpression="no_negocio"></asp:BoundField>
                                                                                    <asp:BoundField DataField="no_familia" HeaderText="Familia" SortExpression="no_familia"></asp:BoundField>
                                                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado"></asp:BoundField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>

                                                                        <asp:HiddenField ID="txh_nid_modelo" runat="server" />
                                                                        <asp:HiddenField ID="hf_exportaexcel" runat="server" />

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnBuscarWarrant" EventName="Click"></asp:AsyncPostBackTrigger>
                                                                        <asp:AsyncPostBackTrigger ControlID="gdModelos" EventName="Sorting" />
                                                                        <asp:AsyncPostBackTrigger ControlID="gdModelos" EventName="PageIndexChanging" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #ffffff;">
                                                <td></td>
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>

