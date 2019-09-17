<%@ Page Language="C#"
    AutoEventWireup="true"
    MasterPageFile="~/Principal.master"
    CodeFile="SRC_Maestro_Talleres.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_Talleres"
    Theme="Default"
    EnableEventValidation="true"
    Title="Maestro Talleres" %>

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
        function Valida_Codigo_Taller(eventObj) {
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
            else if (key == 8 || key == 9) { } //BS y TAB
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false;//anula la entrada de texto. 
            }
        }

        function Valida_Nombre_Taller(eventObj) {
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
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }        //BS y TAB
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false;//anula la entrada de texto. 
            }
        }

        function Valida_Boton_Editar() {
            var txh_nid_taller = document.getElementById('<%=txh_nid_taller.ClientID%>');
            if (txh_nid_taller.value == "") {
                alert("Debe Seleccionar un Registro de la Grilla.");
                return false;
            }
        }

        function Valida_Boton_Detalle() {
            var txh_nid_taller = document.getElementById('<%=txh_nid_taller.ClientID%>');
            if (txh_nid_taller.value == "") {
                alert("Debe Seleccionar un Registro de la Grilla.");
                return false;
            }
        }

        function Valida_Boton_Horario_Excep() {
            var txh_nid_taller = document.getElementById('<%=txh_nid_taller.ClientID%>');
            if (txh_nid_taller.value == "") {
                alert("Debe Seleccionar un Registro de la Grilla.");
                return false;
            }
        }

        function Valida_Boton_Exportar_Excel() {
            var hf_exportaexcel = document.getElementById('<%=hf_exportaexcel.ClientID%>');
            if (hf_exportaexcel.value == "") {
                alert("No Hay Data para Exportar.");
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
            <table cellpadding="2" cellspacing="0" width="1000" border="0" style="height: 47px">
                <tr>
                    <td>
                        <!--INICIO ICONOS DE ACCCION -->
                        <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
                            <tr>
                                <td style="width: 100%; height: 27px;" align="right">

                                    <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" OnClick="btnBuscar_Click" />

                                    <asp:ImageButton ID="btnVerDet" runat="server" ToolTip="Detalle" ImageUrl="~/Images/iconos/b-ordendespacho.png"
                                        onmouseover="javascript:this.src='../Images/iconos/b-ordendespacho2.png'" onmouseout="javascript:this.src='../Images/iconos/b-ordendespacho.png'"
                                        OnClick="btnVerDet_Click" OnClientClick="javascript:return Valida_Boton_Detalle()" />

                                    <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/iconos/b-nuevo.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"
                                        OnClick="btnNuevo_Click" />

                                    <asp:ImageButton ID="BtnEditar" runat="server" ToolTip="Editar" ImageUrl="~/Images/iconos/b-modificarped.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-modificarped2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-modificarped.gif'"
                                        OnClick="BtnEditar_Click" OnClientClick="javascript:return Valida_Boton_Editar()" />

                                    <asp:Image ID="btnGargaMasiva" runat="server" ToolTip="Carga Masiva" Visible="true" ImageUrl="~/Images/iconos/b-importar.gif"
                                        Style="cursor: pointer" onClick="javascript: return Fc_AbrirCargaMasiva();"
                                        onmouseover="javascript:this.src='../Images/iconos/b-importar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-importar.gif'" />

                                    <asp:ImageButton ID="btnExcel" runat="server" ToolTip="Excel" ImageUrl="~/Images/iconos/b-excel.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'"
                                        OnClick="btnExcel_Click" OnClientClick="javascript:return Valida_Boton_Exportar_Excel()" />

                                </td>
                            </tr>
                        </table>
                    </td>
                    <!--FIN ICONOS DE ACCCION -->
                </tr>
                <tr>
                    <td valign="top">
                        <asp:UpdatePanel ID="UpdBusqueda" runat="server">
                            <ContentTemplate>
                                <cc1:TabContainer ID="tabMantMaesTalleres" runat="server" CssClass="" Width="1000px">
                                    <cc1:TabPanel runat="server" ID="tabListMaesTalleres">
                                        <HeaderTemplate>
                                            <!-- TITULO DEL TAB-->
                                            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                    <td class="TabCabeceraOn">Listado de Talleres</td>
                                                    <!-- AGREGAR TITULO DEL TAB-->
                                                    <td>
                                                        <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="DivCuerpoTab">
                                                <table cellspacing="0" cellpadding="0" border="0" style="width: 960px">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <img alt="" style="width: 100%" src="../Images/Tabs/borarriba.gif" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top; height: 450px; background-color: #ffffff; width: 960px;">
                                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff; width: 960px;" cellspacing="1" cellpadding="1" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl" runat="server" SkinID="lblcb">CRITERIOS DE BÚSQUEDA</asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table class="cbusqueda" border="0" cellpadding="1" cellspacing="0" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 10%;">
                                                                                                <asp:Label ID="lbl_cod_taller" runat="server" Text="Código"></asp:Label></td>
                                                                                            <td style="width: 20%;">
                                                                                                <asp:TextBox ID="txt_codtaller" runat="server" MaxLength="20" Width="142px"></asp:TextBox></td>
                                                                                            <td style="width: 12%;">
                                                                                                <asp:Label ID="lbl_nom_taller" runat="server" Text="Nombre"></asp:Label></td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:TextBox ID="txt_nomtaller" runat="server" MaxLength="50" Width="200px"></asp:TextBox></td>
                                                                                            <td style="width: 10%;">Estado</td>
                                                                                            <td style="width: 23%;">
                                                                                                <asp:DropDownList ID="ddl_estado" runat="server" Width="150px">
                                                                                                </asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_dep_taller" runat="server" Text="Departamento"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_departamento" runat="server" AutoPostBack="True" Width="170px" OnSelectedIndexChanged="ddl_departamento_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_prov_taller" runat="server" Text="Provincia"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_provincia" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddl_provincia_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbl_dist_taller" runat="server" Text="Distrito"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_distrito" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddl_distrito_SelectedIndexChanged"></asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Punto de Red</td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_puntored" runat="server" Width="151px"></asp:DropDownList></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; width: 970px; border-right-width: 0px" id="Image43" src="../Images/iconos/fbusqueda.gif" /></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <br />
                                                                                <%--<asp:HiddenField ID="hndGridViewPrintContent" runat="server" /> --%>
                                                                                <div style="overflow: auto; width: 100%; height: 300px">
                                                                                    <asp:GridView ID="gdTalleres" runat="server" Width="960px" AllowSorting="True" OnSorting="gdTalleres_Sorting" OnPageIndexChanging="gdTalleres_PageIndexChanging" SkinID="Grilla" DataKeyNames="nid_taller" AutoGenerateColumns="False" CellPadding="1" AllowPaging="True" OnRowDataBound="gdTalleres_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="co_taller" HeaderText="Codigo Taller" SortExpression="co_taller">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="no_taller" HeaderText="Nombre Completo" SortExpression="no_taller">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="nomdpto" HeaderText="Departamento" SortExpression="nomdpto">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="nomprov" HeaderText="Provincia" SortExpression="nomprov">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="nomdist" HeaderText="Distrito" SortExpression="nomdist">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="no_ubica" HeaderText="Punto de Red" SortExpression="no_ubica">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                            <asp:BoundField DataField="fl_activo" HeaderText="Estado" SortExpression="fl_activo">
                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                                            </asp:BoundField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                                <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                                <asp:HiddenField ID="txh_nid_taller" runat="server"></asp:HiddenField>
                                                                                <asp:HiddenField ID="hf_exportaexcel" runat="server" />
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <img alt="" style="width: 100%" src="../Images/Tabs/borabajo.gif" /></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>

                                </cc1:TabContainer>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>



    <asp:HiddenField ID="txhIndErrror" runat="server" Value="" />

    <asp:Panel ID="pnlCargaMasiva" runat="server" CssClass="modalPopup" Width="500px"
        Style="display: none; background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 10px; padding-bottom: 8px">

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

    <!-- progress -->

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
