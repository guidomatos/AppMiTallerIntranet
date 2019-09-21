<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Principal.master"
    CodeFile="SRC_Maestro_Usuarios.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_Usuarios"
    Theme="Default"
    EnableEventValidation="true"
    Title="Maestro Usuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Principal.master" %>

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
        //

        function Valida_DNI(eventObj) {

            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            if (key >= 48 && key <= 57) { }
            else if (key == 8 || key == 9) { } //BS y TAB
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else
                return false;//anula la entrada de texto.         
        }

        function Valida_Usuario(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser  

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú
            else if (key == 225 || key == 223 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 164 || key == 165) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else
                return false;//anula la entrada de texto.         
        }

        function Valida_Nombre(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_Nombres = document.getElementById('<%=txt_Nombres.ClientID%>');

            if (txt_Nombres.value.length == 0) {
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
            else
                return false;//anula la entrada de texto.        
        }

        function Valida_ApePaterno(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_ApPaterno = document.getElementById('<%=txt_ApPaterno.ClientID%>');

            if (txt_ApPaterno.value.length == 0) {
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
            else
                return false;//anula la entrada de texto.        
        }
        function Valida_ApeMaterno(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_ApMaterno = document.getElementById('<%=txt_ApMaterno.ClientID%>');

            if (txt_ApMaterno.value.length == 0) {
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
            else
                return false;//anula la entrada de texto.        
        }
        function Valida_Boton_Detalle() {
            var txh_Nid_usuario = document.getElementById('<%=txh_Nid_usuario.ClientID%>');
            if (txh_Nid_usuario.value == "") {
                alert('Debe Seleccionar un Registro de la Grilla.');
                return false;
            }
        }
        function Valida_Boton_Editar() {
            var txh_Nid_usuario = document.getElementById('<%=txh_Nid_usuario.ClientID%>');
            if (txh_Nid_usuario.value == "") {
                alert('Debe Seleccionar un Registro de la Grilla.');
                return false;
            }
        }
        function Valida_Boton_Exportar() {
            var hf_exportaexcel = document.getElementById('<%=hf_exportaexcel.ClientID%>');
            if (hf_exportaexcel.value == "") {
                alert('No Hay Data para Exportar.');
                return false;
            }
        }
        function fc_CerrarCargaMasiva() {
            document.getElementById("<%=this.btnCerrarCargaMasiva1.ClientID %>").click();
            return false;
        }

        function Fc_AbrirCargaMasiva() {
            document.getElementById("<%=this.OpenCarga.ClientID %>").click();
            return false;
        }
        function Fc_CargarExcelMasivo() {

            if (fc_Trim(document.getElementById("<%=this.txtAdjuntoCargaMasiva.ClientID %>").value) == "") {
                alert(SRC_NoExisteExcelImportar); return false;
            }

            else if (((/(?:\.([^.]+))?$/).exec(fc_Trim(document.getElementById("<%=this.txtAdjuntoCargaMasiva.ClientID %>").value))[1].toUpperCase() != 'XLS') &&
                ((/(?:\.([^.]+))?$/).exec(fc_Trim(document.getElementById("<%=this.txtAdjuntoCargaMasiva.ClientID %>").value))[1].toUpperCase() != 'XLSX')) {
                alert("El Formato debe ser un archivo Excel."); return false;
            }

            return confirm(SRC_SeguroImportarExcel);
        }
    </script>

    <asp:UpdatePanel ID="Upd_GENERAL" runat="server">
        <ContentTemplate>
            <table style="height: 47px" cellspacing="0" cellpadding="2" width="1000" border="0">
                <tbody>
                    <tr>
                        <td>
                            <!--INICIO ICONOS DE ACCCION -->
                            <table id="tblIconos" class="TablaIconosMantenimientos" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%; height: 27px" align="right">

                                            <asp:ImageButton ID="btnBuscar" onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"
                                                OnClick="btnBuscar_Click" runat="server"
                                                ImageUrl="~/Images/iconos/b-buscar.gif" ToolTip="Buscar"></asp:ImageButton>

                                            <asp:ImageButton ID="btnVerDet" onmouseover="javascript:this.src='../Images/iconos/b-ordendespacho2.png'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-ordendespacho.png'" OnClick="btnVerDet_Click"
                                                runat="server" OnClientClick="javascript:return Valida_Boton_Detalle()"
                                                ImageUrl="~/Images/iconos/b-ordendespacho.png" ToolTip="Detalle"></asp:ImageButton>

                                            <asp:ImageButton ID="btnNuevo" onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"
                                                OnClick="btnNuevo_Click" runat="server" ImageUrl="~/Images/iconos/b-nuevo.gif"
                                                ToolTip="Nuevo"></asp:ImageButton>

                                            <asp:ImageButton ID="BtnEditar"
                                                onmouseover="javascript:this.src='../Images/iconos/b-modificarped2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-modificarped.gif'"
                                                OnClick="BtnEditar_Click" runat="server" OnClientClick="javascript:return Valida_Boton_Editar()"
                                                ImageUrl="~/Images/iconos/b-modificarped.gif" ToolTip="Editar"></asp:ImageButton>

                                            <asp:ImageButton ID="btnExcel" onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'" OnClick="btnExcel_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-excel.gif" OnClientClick="javascript:return Valida_Boton_Exportar()"
                                                ToolTip="Excel"></asp:ImageButton>

                                            <asp:Image ID="btnGargaMasiva" Visible="true" runat="server" ImageUrl="~/Images/iconos/b-importar.gif"
                                                onclick="javascript: return Fc_AbrirCargaMasiva();"
                                                onmouseout="javascript:this.src='../Images/iconos/b-importar.gif'"
                                                onmouseover="javascript:this.src='../Images/iconos/b-importar2.gif'" Style="cursor: pointer"
                                                ToolTip="Importar Datos" />

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <!--FIN ICONOS DE ACCCION -->
                    </tr>
                    <tr>
                        <td valign="top">
                            <cc1:TabContainer ID="tabMantMaesUsuarios" runat="server" CssClass="">
                                <cc1:TabPanel runat="server" ID="tabListMaesUsuarios">
                                    <HeaderTemplate>
                                        <!-- TITULO DEL TAB-->
                                        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td class="TabCabeceraOn">Listado de Usuarios</td>
                                                <!-- AGREGAR TITULO DEL TAB-->
                                                <td>
                                                    <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                            </tr>
                                        </table>

                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div class="divCuerpoTab">
                                            <table cellspacing="0" cellpadding="0" width="980" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borarriba.gif" style="width: 100%" width="0" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                            <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1" cellpadding="1" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 975px">
                                                                            <asp:Label ID="lbl" runat="server" SkinID="lblcb">CRITERIOS DE BÚSQUEDA</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 975px">
                                                                            <asp:UpdatePanel ID="UpdBusqueda" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table style="width: 100%" class="cbusqueda" border="0" cellpadding="1" cellspacing="0">
                                                                                        <tr>
                                                                                            <td style="width: 10%">Nro. Documento</td>
                                                                                            <td style="width: 15%">
                                                                                                <asp:TextBox ID="txt_NroDNI" runat="server" MaxLength="8"></asp:TextBox></td>
                                                                                            <td style="width: 10%">Nombres&nbsp;</td>
                                                                                            <td style="width: 20%">
                                                                                                <asp:TextBox ID="txt_Nombres" runat="server" Width="210px" MaxLength="100"></asp:TextBox></td>
                                                                                            <td style="width: 10%">
                                                                                                <asp:Label ID="lbl_dep_taller" runat="server" Text="Departamento"></asp:Label></td>
                                                                                            <td style="width: 20%">
                                                                                                <asp:DropDownList ID="ddl_departamento" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddl_departamento_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Usuario</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txt_Usuario" runat="server" MaxLength="20"></asp:TextBox></td>
                                                                                            <td>Apellido Paterno&nbsp;</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txt_ApPaterno" runat="server" Width="210px" MaxLength="50"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_prov_taller" runat="server" Text="Provincia"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_provincia" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Estado</td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_Estado" runat="server" Width="100px">
                                                                                                </asp:DropDownList></td>
                                                                                            <td>Apellido Materno</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txt_ApMaterno" runat="server" Width="210px" MaxLength="50"></asp:TextBox></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_dist_taller" runat="server" Text="Distrito"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_distrito" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddl_distrito_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td>Perfil</td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_Perfil" runat="server" Width="210px"></asp:DropDownList></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_nom_taller" runat="server" Text="Taller"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_taller" runat="server" Width="200px" OnSelectedIndexChanged="ddl_taller_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table cellspacing="0" cellpadding="0" width="970">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; width: 970px; border-right-width: 0px" id="Image43" src="../Images/iconos/fbusqueda.gif" /></td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>&nbsp;<div style="overflow: auto; width: 965px; height: 300px">
                                                                                                    <asp:GridView ID="gdUsuarios" runat="server" Width="1200px" OnSorting="gdUsuarios_Sorting" OnRowDataBound="gdUsuarios_RowDataBound" DataKeyNames="Nid_usuario" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" SkinID="Grilla" OnPageIndexChanging="gdUsuarios_PageIndexChanging">
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="nu_tipo_documento" HeaderText="Nro. Doc" SortExpression="Nu_tipo_documento">
                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                                                                <HeaderStyle Width="5%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="VNOMUSR" HeaderText="Nombres" SortExpression="VNOMUSR">
                                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="No_ape_paterno" HeaderText="Ape. Paterno" SortExpression="No_ape_paterno">
                                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="no_ape_materno" HeaderText="Ape. Materno" SortExpression="No_ape_materno">
                                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="CUSR_ID" HeaderText="Usuario" SortExpression="CUSR_ID">
                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>

                                                                                                            <asp:BoundField DataField="va_correo" HeaderText="Correo" SortExpression="va_correo">
                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>

                                                                                                            <asp:BoundField DataField="nro_telf" HeaderText="Celular" SortExpression="nro_telf">
                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>

                                                                                                            <asp:BoundField DataField="perfil" HeaderText="Perfil" SortExpression="Perfil">
                                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                                <HeaderStyle Width="10%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="dpto" HeaderText="Dep" SortExpression="Dpto">
                                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                                <HeaderStyle Width="8%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="prov" HeaderText="Prov" SortExpression="Prov">
                                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                                <HeaderStyle Width="6%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="dist" HeaderText="Dist" SortExpression="Dist">
                                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                                <HeaderStyle Width="9%" />
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="no_taller" HeaderText="Taller" SortExpression="No_taller">
                                                                                                                <HeaderStyle Wrap="True" Width="7%"></HeaderStyle>

                                                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                            </asp:BoundField>
                                                                                                            <asp:BoundField DataField="Fl_activo" HeaderText="Estado" SortExpression="Fl_activo">
                                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                                                            </asp:BoundField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <asp:HiddenField ID="txh_Nid_usuario" runat="server"></asp:HiddenField>
                                                                                    <asp:HiddenField ID="hf_exportaexcel" runat="server"></asp:HiddenField>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"></asp:AsyncPostBackTrigger>
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borabajo.gif" style="width: 100%" width="0" /></td>
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
            <asp:PostBackTrigger ControlID="btnExcel" />

        </Triggers>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlCargaMasiva" runat="server" CssClass="modalPopup" Width="500px"
        Style="display: none; background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
            <tr>
                <td style="width: 275px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif)">&nbsp;</td>
                <td style="width: 230px; background-repeat: repeat; background-image: url(../Images/flotante/popcab2.gif);">&nbsp;</td>
                <td style="width: 55px; background-repeat: repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" style="width: 500px">
            <tr style="display: none">
                <td align="right">
                    <asp:Button ID="OpenCarga" runat="server" />&nbsp;
                    <asp:ImageButton ID="btnCerrarCargaMasiva1" runat="server" />&nbsp;
                    <asp:ImageButton ID="btnGargaMasiva1" runat="server" OnClick="btnGargaMasiva_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px; padding-right: 10px">
                    <table cellpadding="0" cellspacing="0" border="0" width="480">
                        <tr valign="bottom">
                            <td style="width: 210px">
                                <table id="tblHeaderImportar" runat="server" width="100%" cellpadding="0" cellspacing="0"
                                    border="0" style="height: 20px">
                                    <tr>
                                        <td style="height: 20px">
                                            <img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                        <td class="TabCabeceraOn" style="width: 210px; height: 20px;">Importar</td>
                                        <td style="height: 20px">
                                            <img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 470px" align="right">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 300px">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="btnAceptarCargaMasiva" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/iconos/b-aceptar.gif"
                                                onmouseover="javascript:this.src='../Images/iconos/b-aceptar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-aceptar.gif'"
                                                OnClientClick="javascript: return Fc_CargarExcelMasivo(30);" OnClick="btnAceptarCargaMasiva_Click" />
                                            <asp:ImageButton ID="btnCerrarCargaMasiva" runat="server" ToolTip="Cerrar" ImageUrl="~/Images/iconos/b-cerrar.gif"
                                                onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'"
                                                OnClientClick="javascript: return fc_CerrarCargaMasiva(31);" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="480" cellpadding="0" cellspacing="0" border="0" bordercolor="blue">
                        <tr>
                            <!-- Cabecera -->
                            <td>
                                <img alt="" src="../Images/Mantenimiento/fbarr.gif" width="480" /></td>
                        </tr>
                        <tr>
                            <!-- Cuerpo -->
                            <td style="background-color: #ffffff; vertical-align: top; width: 470px; padding-left: 5px; padding-right: 5px; height: 60px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="470">
                                    <tr>
                                        <td>
                                            <table border="0" width="470" cellspacing="0" cellpadding="0" class="cbusqueda" bordercolor="red" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px; height: 50px">
                                                <tr>
                                                    <td style="width: 150px">Nombre Archivo</td>
                                                    <td style="width: 330px">
                                                        <asp:FileUpload ID="txtAdjuntoCargaMasiva" runat="server" Width="330px" CssClass="txtob" />
                                                    </td>
                                                    <td style="width: 90px" align="center"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle">Tipo Usuario</td>
                                                    <td style="width: 330px">
                                                        <asp:RadioButtonList ID="rbTipo" runat="server" RepeatDirection="Horizontal" Width="98%">
                                                            <asp:ListItem Selected="True" Value="0">Asesor de Servicio</asp:ListItem>
                                                            <asp:ListItem Value="1">Administrador de Taller</asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                    <td align="center"></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="height: 15px"></td>
                                                    <td align="center"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="left">*Debe cargar un archivo .xls con formato correcto.<asp:HiddenField ID="hf_ARCHIVO"
                                                        runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <!-- Pie -->
                            <td>
                                <img alt="" src="../Images/Mantenimiento/fba.gif" width="480" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <cc1:ModalPopupExtender ID="mpeCargaMasiva" runat="server" PopupControlID="pnlCargaMasiva"
        BackgroundCssClass="modalBackground" TargetControlID="OpenCarga" CancelControlID="btnCerrarCargaMasiva1" />

    <asp:HiddenField ID="txhIndErrror" runat="server" Value="" />


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
