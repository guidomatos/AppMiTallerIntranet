<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_Maestro_Vehiculo.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_Vehiculo"
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

        function Validar_Seleccion_Registro() {

            var txh_nid_vehiculo = document.getElementById('<%=txh_nid_vehiculo.ClientID%>');

            if (txh_nid_vehiculo.value.trim() == "") {
                alert('Debe Seleccionar un registro de la Grilla.');
                return false;
            }
            return true;
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
            <table style="height: 47px" cellspacing="0" cellpadding="2" width="1000" border="0">
                <tbody>
                    <tr>
                        <td>
                            <table id="tblIconos" class="TablaIconosMantenimientos" cellspacing="0" cellpadding="0"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnBuscarWarrant" onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" OnClick="btnBuscarWarrant_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-buscar.gif" ToolTip="Buscar"></asp:ImageButton>
                                            <asp:ImageButton ID="btnVerDet" onmouseover="javascript:this.src='../Images/iconos/b-ordendespacho2.png'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-ordendespacho.png'" OnClick="btnVerDet_Click"
                                                runat="server" OnClientClick="javascript:return Validar_Seleccion_Registro();"
                                                ImageUrl="~/Images/iconos/b-ordendespacho.png" ToolTip="Detalle"></asp:ImageButton>
                                            <asp:ImageButton ID="btnNuevo" onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'" OnClick="btnNuevo_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-nuevo.gif" ToolTip="Nuevo"></asp:ImageButton>
                                            <asp:ImageButton ID="BtnEditar" onmouseover="javascript:this.src='../Images/iconos/b-modificarped2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-modificarped.gif'" OnClick="BtnEditar_Click"
                                                runat="server" OnClientClick="javascript:return Validar_Seleccion_Registro();"
                                                ImageUrl="~/Images/iconos/b-modificarped.gif" ToolTip="Editar"></asp:ImageButton>
                                            <asp:ImageButton ID="btnExcel" onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'" OnClick="btnExcel_Click"
                                                runat="server"
                                                OnClientClick="javascript:return Valida_Boton_Exportar();"
                                                ImageUrl="~/Images/iconos/b-excel.gif" ToolTip="Excel"></asp:ImageButton>

                                            <asp:HiddenField ID="hf_exportaexcel" runat="server" />

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <cc1:TabContainer ID="tabMantMaesModelos" ActiveTabIndex="0" runat="server" CssClass="">
                                <cc1:TabPanel runat="server" ID="tabListMaesModelos">
                                    <HeaderTemplate>
                                        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td class="TabCabeceraOn">Listado de Vehiculos</td>
                                                <td>
                                                    <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div class="DivCuerpoTab">
                                            <table cellspacing="0" cellpadding="0" width="980px" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borarriba.gif" width="100%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                            <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                                cellpadding="1" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl" runat="server" SkinID="lblcb">CRITERIOS DE BÚSQUEDA</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdBusqueda" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table width="966" border="0" cellpadding="0" cellspacing="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table border="0" cellpadding="2" cellspacing="1" class="cbusqueda" style="width: 100%;">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td style="width: 8%;">
                                                                                                                    <asp:Label ID="lblTextoPlaca" runat="server" Text="Placa"></asp:Label>
                                                                                                                </td>
                                                                                                                <td style="width: 22%;">
                                                                                                                    <asp:TextBox ID="txt_busplacapatente" runat="server" MaxLength="50" Width="100px" Style="text-transform: uppercase;" onkeypress="return SoloPlaca(event);"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td style="width: 10%;">Marca</td>
                                                                                                                <td style="width: 25%;">
                                                                                                                    <asp:DropDownList ID="ddl_busmarca" runat="server" AutoPostBack="True" Width="200px"
                                                                                                                        OnSelectedIndexChanged="ddl_busmarca_SelectedIndexChanged">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td style="width: 10%;">Modelo</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddl_busmodelo" runat="server" Width="200px">
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>VIN</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_busnrovin" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>Kilometraje
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_buskilometraje" runat="server" MaxLength="10" Width="100px"></asp:TextBox></td>
                                                                                                                <td>Estado</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="cboEstado" runat="server" Width="150px">
                                                                                                                    </asp:DropDownList></td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                    <table cellspacing="0" cellpadding="0" width="966" id="UNDER">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; width: 970px; border-right-width: 0px"
                                                                                                                        id="Image43" src="../Images/iconos/fbusqueda.gif" /></td>
                                                                                                            </tr>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="padding-top: 15px" valign="top">
                                                                                                    <asp:GridView ID="gdVehiculos" runat="server" Width="100%" OnSorting="gdVehiculos_Sorting"
                                                                                                        AllowSorting="True" OnRowDataBound="gdVehiculos_RowDataBound" OnPageIndexChanging="gdModelos_PageIndexChanging"
                                                                                                        AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="nid_vehiculo,nu_placa,nu_vin,nid_marca,nid_modelo,qt_km_actual,fl_activo,pro_co_tipo_cliente,pro_co_tipo_documento,pro_nu_documento,clie_co_tipo_cliente,clie_co_tipo_documento,clie_nu_documento,cont_co_tipo_cliente,cont_co_tipo_documento,cont_nu_documento,co_tipo"
                                                                                                        SkinID="Grilla">

                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="nu_placa" HeaderText="Placa/Patente" SortExpression="nu_placa">
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="nu_vin" HeaderText="Nro. VIN" SortExpression="nu_vin">
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                <HeaderStyle Width="15%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="no_marca" HeaderText="Marca" SortExpression="no_marca">
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="no_modelo" HeaderText="Modelo" SortExpression="no_modelo">
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="qt_km_actual" HeaderText="Kilometraje" SortExpression="qt_km_actual">
                                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="nu_anio" HeaderText="Año" SortExpression="nu_anio">
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="no_tipo" HeaderText="Tipo" SortExpression="no_tipo">
                                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                                <HeaderStyle Width="10%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="fl_activo" HeaderText="Estado" SortExpression="fl_activo">
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                                                            </asp:BoundField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                    <asp:HiddenField ID="txh_nid_vehiculo" runat="server"></asp:HiddenField>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarWarrant" EventName="Click"></asp:AsyncPostBackTrigger>
                                                                                    <asp:AsyncPostBackTrigger ControlID="gdVehiculos" EventName="Sorting"></asp:AsyncPostBackTrigger>
                                                                                    <asp:AsyncPostBackTrigger ControlID="gdVehiculos" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borabajo.gif" width="100%" /></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel"></asp:PostBackTrigger>
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
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress" BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />

    <script type="text/javascript">
        setTabCabeceraOnForm('0');

        function SoloPlaca(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser   

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key >= 48 && key <= 57) { }
            //else if (key==45){} //sin guión
            else {
                return false; //anula la entrada de texto. 
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
