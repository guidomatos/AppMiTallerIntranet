<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SRC_Mantenimiento_Parametros.aspx.cs" Inherits="SRC_Mantenimiento_Parametros" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

        function Fc_Cambiartab(sender, e) {
            var CurrentTab1 = sender;
            //alert(sender.get_activeTab().get_id());
            var index = sender.get_activeTab()._tabIndex;

            document.getElementById('<%=this.btnNuevo.ClientID%>').style.display = (index == 2) ? 'inline' : 'none';
            document.getElementById('<%=this.btnBuscar.ClientID%>').style.display = (index == 2) ? 'inline' : 'none';
            setTabCabeceraOffForm('0');//setTabCabeceraOffForm('2');setTabCabeceraOnForm(index);
        }


        function changeTab(index) {

            //Get a Handle to the Tab Behavior 
            var tabBehavior = $get('<%=this.tabContMantParametros.ClientID%>').control;
            var tabIndex = parseInt(index);
            //Set the Currently Visible Tab 
            document.getElementById("ctl00_ContentPlaceHolder1_hid_tab1").value = tabIndex;
            tabBehavior.set_activeTabIndex(tabIndex);
        }

        function confirmar() {
            if (!confirm("Esta seguro de actualizar.")) {
                document.getElementById("ctl00_ContentPlaceHolder1_hid_ind_gr").value = "0";
                return false;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_hid_ind_gr").value = "1";
            }
        }

        function keyDownNumber() {
            var key;
            if (navigator.appName == 'Microsoft Internet Explorer')
                key = event.keyCode;
            else
                key = event.which
            if (!(key >= 48 && key <= 57) && key != 8 && key != 46 && key != 36 && key != 37) {
                event.returnValue = false;
            }
        }


        function Valida_Boton_Exportar_Excel() {
            var hf_exportaexcel = document.getElementById('<%=hf_exportaexcel.ClientID%>');
            if (hf_exportaexcel.value == "") {
                alert("No Hay Data para Exportar.");
                return false;
            }

        }

        function Fc_AgregarConfiguracion() {


        }


    </script>
    <!--HIDENNS-->
    <input id="hid_tab1" type="hidden" runat="server" />
    <table cellpadding="2" cellspacing="0" width="1000" border="0" style="height: 47px">
        <tr>
            <td>
                <!--INICIO ICONOS DE ACCCION-->
                <table id="tblIconos" border="0" class="TablaIconosMantenimientos">
                    <tr>
                        <td style="width: 100%" align="right">

                            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/iconos/b-nuevo.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'" OnClick="btnNuevo_Click" OnClientClick="javascript:return Fc_AgregarConfiguracion()" />
                            <asp:ImageButton ID="btnExcel" runat="server" ToolTip="Excel" ImageUrl="~/Images/iconos/b-excel.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-excel2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-excel.gif'" OnClick="btnExcel_Click" OnClientClick="javascript:return Valida_Boton_Exportar_Excel()" />

                        </td>
                    </tr>
                </table>
            </td>
            <!--FIN ICONOS DE ACCCION -->
        </tr>
        <tr>
            <td valign="top">
                <asp:UpdatePanel ID="upd_GENERAL" runat="server">
                    <ContentTemplate>
                        <asp:TabContainer ID="tabContMantParametros" runat="server" AutoPostBack="True" CssClass="" ActiveTabIndex="0" OnActiveTabChanged="tabContMantParametros_ActiveTabChanged" OnUnload="tabContMantParametros_Unload" OnClientActiveTabChanged="Fc_Cambiartab" BackColor="White">
                            <asp:TabPanel runat="server" ID="tabMantParametros">
                                <HeaderTemplate>
                                    <table id="tblHeader0" cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td style="height: 20px">
                                                    <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td onmouseover="javascript: return onTabCabeceraOverForm('0');" class="TabCabeceraOnForm" onmouseout="javascript: return onTabCabeceraOutForm('0');" style="height: 20px">Parametros</td>
                                                <!-- AGREGAR TITULO DEL TAB-->
                                                <td style="height: 20px">
                                                    <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div class="DivCuerpoTab">
                                        <asp:UpdatePanel ID="upd_list_parametro" runat="server">
                                            <ContentTemplate>
                                                <table cellspacing="0" cellpadding="0" width="980" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td style="height: 25px; background-color: #ffffff">
                                                                <table style="margin-left: 5px; margin-right: 5px" cellspacing="0" cellpadding="0" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td class="lineatitulo">
                                                                                <asp:Label ID="Label1" runat="server" SkinID="lblCB">PARÁMETROS DEL SISTEMA</asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td style="margin-left: 10px; margin-right: 10px; height: 430px; background-color: #ffffff" align="center">
                                                                <asp:GridView ID="gvListado" runat="server" PageSize="15" OnPageIndexChanging="gvListado_PageIndexChanging" AllowPaging="True" OnRowUpdating="gvListado_RowUpdating" DataKeyNames="no_tipo_valor,valor,nid_parametro" OnRowEditing="gvListado_RowEditing" AutoGenerateColumns="False" SkinID="Grilla" OnRowCancelingEdit="gvListado_RowCancelingEdit" Width="968px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ID Par&#225;metro">
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lbl_idverparam" runat="server" Text='<%# Bind("co_parametro") %>'></asp:Label>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_idupdparam" runat="server" Text='<%# Bind("co_parametro") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <HeaderStyle Width="150px"></HeaderStyle>

                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombre de Parametro">
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lbl_nomverparam" runat="server" Text='<%# Bind("no_parametro") %>'></asp:Label>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_nomupdparam" runat="server" Text='<%# Bind("no_parametro") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <HeaderStyle Width="350px"></HeaderStyle>

                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Valor">
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txt_valorparam" onkeypress="keyDownNumber()" onpaste="return false;" runat="server" Text='<%# Bind("valor_texto") %>'></asp:TextBox>
                                                                                <asp:CheckBox ID="chk_valorparam" Text="Verdadero" runat="server"></asp:CheckBox>
                                                                                <asp:DropDownList ID="ddl_conshoraspor" Width="140px" runat="server"></asp:DropDownList>
                                                                            </EditItemTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_valorparam" runat="server" Text='<%# Bind("valor_texto") %>'></asp:Label>
                                                                            </ItemTemplate>

                                                                            <HeaderStyle Width="340px"></HeaderStyle>

                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField ShowEditButton="True" HeaderText="Opci&#243;n">
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                        </asp:CommandField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:HiddenField ID="hf_exportaexcel" runat="server"></asp:HiddenField>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <!-- Pie -->
                                                            <td>
                                                                <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="tabContMantParametros" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel runat="server" ID="tabHorTaller">
                                <HeaderTemplate>
                                    <table id="tblHeader1" style="display: none;" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <img id="img1" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                            <td class="TabCabeceraOffForm" onmouseover="javascript: return onTabCabeceraOverForm('0');" onmouseout="javascript: return onTabCabeceraOutForm('0');">Párametros</td>
                                            <!-- AGREGAR TITULO DEL TAB-->
                                            <td>
                                                <img id="img2" alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                        </tr>
                                    </table>

                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div class="DivCuerpoTab">
                                        <table cellspacing="0" cellpadding="0" width="980" border="0">
                                            <tbody>
                                                <tr>
                                                    <!-- CABECERA -->
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr style="height: 435px">
                                                    <td style="background-color: #ffffff" valign="top">
                                                        <!-- CUERPO: AQUI COLOCAN SU CODIGO BIEN ESTRUCTURA -->
                                                        <asp:UpdatePanel ID="upd_hor_defecto" runat="server">
                                                            <ContentTemplate>
                                                                <table style="margin-left: 5px; margin-right: 5px" cellspacing="0" cellpadding="0" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl1" runat="server" SkinID="lblCB">HORARIO POR DEFECTO DEL TALLER</asp:Label></td>
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
                                                                                                                <asp:DropDownList ID="ddl_hora_de" runat="server">
                                                                                                                </asp:DropDownList>
                                                                                                            </td>
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
                            <asp:TabPanel runat="server" HeaderText="tabMantConfiguracion" ID="TabPanel1">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div class="DivCuerpoTab">
                                        <asp:UpdatePanel ID="upd_list_configuracion" runat="server">
                                            <ContentTemplate>
                                                <div style="height: 475px" class="DivCuerpoTab">
                                                    <table cellspacing="0" cellpadding="0" width="980" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <img alt="" src="../Images/Tabs/borarriba.gif" width="100%" /></td>
                                                            </tr>
                                                            <tr style="height: 450px">
                                                                <td style="vertical-align: top; height: 450px; background-color: #ffffff" align="left">
                                                                    <table style="margin-left: 5px; margin-right: 5px" cellspacing="0" cellpadding="0" width="950" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="height: 14px" id="td_Criterio_Busqueda"><span>
                                                                                    <asp:Label ID="lbl" runat="server" SkinID="lblCB">CRITERIOS DE BÚSQUEDA</asp:Label></span></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="bottom">
                                                                                    <asp:UpdatePanel ID="upAdminCitas" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <table style="width: 99.7%" class="cbusqueda" cellspacing="0" cellpadding="1" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td style="width: 10%">
                                                                                                            <asp:Label ID="Label5" runat="server" Text="Departamento"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:DropDownList ID="ddl_bus_departamento" runat="server" AutoPostBack="True" Width="150px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 12%">Ermpresa</td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:DropDownList ID="ddl_bus_estreserva" runat="server" Width="180px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 15%">Correo CallCenter</td>
                                                                                                        <td>&nbsp;<asp:TextBox ID="TextBox3" runat="server" Width="180px"></asp:TextBox></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label9" runat="server" Text="Provincia"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddl_bus_provincia" runat="server" AutoPostBack="True" Width="150px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>Banco</td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TextBox2" runat="server" Width="150px"></asp:TextBox></td>
                                                                                                        <td>Teléfono CallCenter</td>
                                                                                                        <td>&nbsp;<asp:TextBox ID="TextBox4" runat="server" Width="180px"></asp:TextBox></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="Label13" runat="server" Text="Distrito"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddl_bus_distrito" runat="server" AutoPostBack="True" Width="150px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>N° Cuenta</td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="TextBox1" runat="server" Width="180px"></asp:TextBox></td>
                                                                                                        <td></td>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblTextoLocal" runat="server" Text="Punto Red"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddl_bus_puntored" runat="server" AutoPostBack="True" Width="150px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td></td>
                                                                                                        <td></td>
                                                                                                        <td style="text-align: right" colspan="2">&nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblTextoTaller" runat="server" Text="Taller"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="ddl_bus_taller" runat="server" Width="150px">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td></td>
                                                                                                        <td></td>
                                                                                                        <td style="width: 230px; text-align: right" colspan="2"></td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <div id="Div1">
                                                                                        <table id="Table1" cellspacing="0" cellpadding="0" width="966">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <img alt="" style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; width: 970px; border-right-width: 0px" id="Img3" src="../Images/iconos/fbusqueda.gif" /></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div style="overflow: auto; width: 965px; height: 300px">
                                                                                        <asp:UpdatePanel ID="upConfiguracion" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:GridView ID="gvListaConfig" runat="server" SkinID="Grilla" OnPageIndexChanging="gvListaConfig_PageIndexChanging" AllowPaging="True" AutoGenerateColumns="False" Width="100%" AllowSorting="True" OnSorting="gvListaConfig_Sorting" OnRowDataBound="gvListaConfig_RowDataBound" OnRowCommand="gvListaConfig_RowCommand">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="no_taller" HeaderText="Taller" SortExpression="no_ape_contacto">
                                                                                                            <HeaderStyle Width="12%"></HeaderStyle>

                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_empresa" HeaderText="Empresa" SortExpression="no_contacto">
                                                                                                            <HeaderStyle Width="12%"></HeaderStyle>

                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_banco" HeaderText="Banco" SortExpression="no_direccion">
                                                                                                            <HeaderStyle Width="16%"></HeaderStyle>

                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="nu_cuenta" HeaderText="N&#176; Cuenta" SortExpression="no_distrito">
                                                                                                            <HeaderStyle Width="10%"></HeaderStyle>

                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_correo_callcenter" HeaderText="Correo CallCenter" SortExpression="fecha_prox_servicio">
                                                                                                            <HeaderStyle Width="6%"></HeaderStyle>

                                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="nu_callcenter" HeaderText="Tel&#233;fono CallCenter" SortExpression="no_empresa">
                                                                                                            <HeaderStyle Width="12%"></HeaderStyle>

                                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <asp:HiddenField ID="hf_ROW_INDEX" runat="server"></asp:HiddenField>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
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
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="tabContMantParametros" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
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
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hid_ind_gr" runat="server" />

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
