<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SRC_Maestro_Cliente.aspx.cs" Inherits="SRC_Mantenimiento_SRC_Maestro_Cliente" EnableEventValidation="true" %>

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

        function Valida_Boton_Editar() {
            var txh_nid_cliente = document.getElementById('<%=txh_nid_cliente.ClientID%>');
            if (txh_nid_cliente.value == "") {
                alert('Debe Seleccionar un Registro de la Grilla.');
                return false;
            }
        }
        function Valida_Nombre(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_nombres = document.getElementById('<%=txt_busnombres.ClientID%>');
            if (txt_nombres.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false;//anula la entrada de texto. 
            }
        }

        function Valida_Paterno(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_nombres = document.getElementById('<%=txt_busapepaterno.ClientID%>');
            if (txt_nombres.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false;//anula la entrada de texto. 
            }
        }

        function Valida_Materno(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_nombres = document.getElementById('<%=txt_busapematerno.ClientID%>');
            if (txt_nombres.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false;//anula la entrada de texto. 
            }
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
                            <!--INICIO ICONOS DE ACCCION -->
                            <table id="tblIconos" class="TablaIconosMantenimientos" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" align="right">

                                            <asp:ImageButton ID="btnBuscarWarrant" onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" OnClick="btnBuscarWarrant_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-buscar.gif" ToolTip="Buscar"></asp:ImageButton>

                                            <asp:ImageButton ID="btnNuevo" onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'" OnClick="btnNuevo_Click" runat="server"
                                                ImageUrl="~/Images/iconos/b-nuevo.gif" ToolTip="Nuevo"></asp:ImageButton>

                                            <asp:ImageButton ID="BtnEditar"
                                                onmouseover="javascript:this.src='../Images/iconos/b-modificarped2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-modificarped.gif'"
                                                OnClick="BtnEditar_Click" runat="server" OnClientClick="javascript:return Valida_Boton_Editar()"
                                                ImageUrl="~/Images/iconos/b-modificarped.gif" ToolTip="Editar"></asp:ImageButton>
                                           <!--
                                            <asp:ImageButton ID="btnExcel" onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'"
                                                OnClick="btnExcel_Click" runat="server" ImageUrl="~/Images/iconos/b-excel.gif"
                                                OnClientClick="javascript:return Valida_Boton_Exportar();"
                                                ToolTip="Excel"></asp:ImageButton>
                                            -->

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <!--FIN ICONOS DE ACCCION -->
                    </tr>
                    <tr>

                        <td valign="top">
                            <cc1:TabContainer ID="tabMantMaesClientes" runat="server" CssClass="" ActiveTabIndex="0">
                                <cc1:TabPanel runat="server" ID="tabListMaesModelos">
                                    <HeaderTemplate>
                                        <!-- TITULO DEL TAB-->
                                        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td class="TabCabeceraOn">Listado de Clientes</td>
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
                                                                    <asp:Label ID="Label1" runat="server" SkinID="lblCB">CRITERIOS DE BÚSQUEDA</asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>

                                                                    <asp:UpdatePanel ID="UpdBusqueda" runat="server">
                                                                        <ContentTemplate>
                                                                            <table border="0" cellspacing="1" cellpadding="2" class="cbusqueda" width="965">
                                                                                <tr>
                                                                                    <!-- COLOCAR CAMPOS DE BUSQUEDA / FILTROS-->
                                                                                    <td style="width: 15%;"></td>
                                                                                    <td style="width: 22%;"></td>
                                                                                    <td style="width: 10%;"></td>
                                                                                    <td style="width: 20%;"></td>
                                                                                    <td style="width: 13%;"></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Tipo de Documento</td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddl_bustipodocumento" runat="server" Width="100px"></asp:DropDownList></td>
                                                                                    <td>Nro. Documento</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_busnrodoc" runat="server" Width="100px" MaxLength="20"></asp:TextBox></td>
                                                                                    <td>Estado</td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="cboEstado" runat="server" Width="100px"></asp:DropDownList></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Nombre(s) o Razon Social</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_busnombres" runat="server" Width="180px" MaxLength="50"></asp:TextBox></td>
                                                                                    <td>Apellido Paterno</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_busapepaterno" runat="server" Width="170px" MaxLength="50"></asp:TextBox></td>
                                                                                    <td>Apellido Materno</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txt_busapematerno" runat="server" Width="170px" MaxLength="50"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                </tr>
                                                                                <!-- COLOCAR CAMPOS DE BUSQUEDA / FILTROS-->
                                                                            </table>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnBuscarWarrant" EventName="Click"></asp:AsyncPostBackTrigger>
                                                                            <asp:AsyncPostBackTrigger ControlID="gdClientes" EventName="Sorting"></asp:AsyncPostBackTrigger>
                                                                            <asp:AsyncPostBackTrigger ControlID="gdClientes" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>


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

                                                                    <div style="overflow: auto; width: 965px">
                                                                        <asp:GridView ID="gdClientes"
                                                                            runat="server"
                                                                            SkinID="Grilla"
                                                                            OnPageIndexChanging="gdClientes_PageIndexChanging"
                                                                            OnRowDataBound="gdClientes_RowDataBound"
                                                                            OnSorting="gdClientes_Sorting"
                                                                            Width="965px"
                                                                            PageSize="10"
                                                                            DataKeyNames="nid_cliente,no_cliente,no_ape_pat,no_ape_mat,co_tipo_documento,Documento,nu_documento,fl_inactivo,Estado,no_correo,nu_telefono,nu_celular"
                                                                            AutoGenerateColumns="False"
                                                                            AllowSorting="True"
                                                                            AllowPaging="True">

                                                                            <Columns>

                                                                                <asp:BoundField DataField="nid_cliente" HeaderText="C&#243;digo" SortExpression="nid_cliente" HeaderStyle-Width="10%"></asp:BoundField>
                                                                                <asp:BoundField DataField="no_cliente" HeaderText="Nombre(s)" SortExpression="no_cliente" HeaderStyle-Width="20%"></asp:BoundField>
                                                                                <asp:BoundField DataField="no_ape_pat" HeaderText="Apellido Paterno" SortExpression="no_ape_pat" HeaderStyle-Width="15%"></asp:BoundField>
                                                                                <asp:BoundField DataField="no_ape_mat" HeaderText="Apellido Materno" SortExpression="no_ape_mat" HeaderStyle-Width="15%"></asp:BoundField>
                                                                                <asp:BoundField DataField="Documento" HeaderText="Tipo Documento" SortExpression="Documento" HeaderStyle-Width="15%"></asp:BoundField>
                                                                                <asp:BoundField DataField="nu_documento" HeaderText="Numero Documento" SortExpression="nu_documento" HeaderStyle-Width="18%"></asp:BoundField>
                                                                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado"></asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>

                                                                    <asp:HiddenField ID="txh_Id_Servicio" runat="server" />
                                                                    <asp:HiddenField ID="hf_exportaexcel" runat="server" />

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
                                                        <img alt="" src="../Images/Tabs/borabajo.gif"></td>
                                                </tr>
                                            </table>

                                        </div>

                                        <asp:HiddenField ID="txh_nid_cliente" runat="server"></asp:HiddenField>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </tbody>
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

