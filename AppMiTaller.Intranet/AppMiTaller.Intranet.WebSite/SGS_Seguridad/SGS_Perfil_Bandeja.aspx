<%@ Page Language="C#" 
    MasterPageFile="~/Seguridad.master"
    AutoEventWireup="true" 
    CodeFile="SGS_Perfil_Bandeja.aspx.cs" 
    Inherits="SGS_Seguridad_SGS_Perfil_Bandeja"
    Theme="Default"%>
<%@ MasterType VirtualPath="~/Seguridad.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="CboEstado" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fc_Limpiar()
        {
            document.getElementById("<%=this.txtPerfil.ClientID %>").value = "";
            document.getElementById("<%=this.cboEstado.ClientID %>_cboEstado").value = "0";
            return false;
        }
        
        function fc_Eliminar()
        {
        if( fc_Trim(document.getElementById('<%=this.txhFlObligatorio.ClientID%>').value) == "0" )
            {
              if( fc_Trim(document.getElementById('<%=this.txhPerfilID.ClientID%>').value) == "" )
            {
                alert(mstrSeleccioneUno);
                return false;
            }
            
            return confirm(mstrSeguroEliminarUno);
            }
        if( fc_Trim(document.getElementById('<%=this.txhFlObligatorio.ClientID%>').value) == "1" )
            {
            alert(mstrNoEliminarPerfil);
            return false;
            }
        }
        
        function fc_Buscar()
        {
            if (fc_Trim(document.getElementById("<%=this.txtPerfil.ClientID %>").value) != ""  &&
                !fc_Trim(document.getElementById("<%=this.txtPerfil.ClientID %>").value).match(RE_ALAFANUMERICO))
            {
                alert(mstrElCampo + "perfil" + mstrReAlfanumerico);
                return false;
            }

            return true;
        }
    </script>
    <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos" >
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnAgregar" runat="server" ToolTip="Agregar" ImageUrl="~/Images/iconos/b-nuevo.gif" OnClick="btnAgregar_Click" 
                    onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"/>
                
                <asp:ImageButton ID="btnEliminar" runat="server" ToolTip="Eliminar" ImageUrl="~/Images/iconos/b-eliminar.gif" OnClientClick="javascript: return fc_Eliminar();" OnClick="btnEliminar_Click" 
                    onmouseover="javascript:this.src='../Images/iconos/b-eliminar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-eliminar.gif'"/>
                
                <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif" OnClick="btnBuscar_Click" OnClientClick="javascript: return fc_Buscar();"
                    onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"/>
                
                <asp:ImageButton ID="btnLimpiar" runat="server" ToolTip="Limpiar" ImageUrl="~/Images/iconos/b-limpiar.gif" OnClientClick="javascript: return fc_Limpiar();" 
                    onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'"/>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <cc1:TabContainer ID="tabContPerfil" runat="server" ActiveTabIndex="0" CssClass="" >
        <cc1:TabPanel ID="tabPerfil" runat="server" CssClass="" >
            <HeaderTemplate>
                <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td><img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif"/></td>
                        <!--onmouseover="javascript: onTabCabeceraOver('0');" onmouseout="javascript: onTabCabeceraOut('0');"-->
                        <td class="TabCabeceraOff" >Perfiles</td>
                        <td><img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif"/></td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ContentTemplate>
                <table width="800px" cellpadding="0" cellspacing="0" border="0">
                    <tr><!-- Cabecera -->
                        <td><img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                    </tr>
                    <tr><!-- Cuerpo -->
                        <td style="background-color:#ffffff;vertical-align: top; height:450px;">
                            <table cellpadding="1" cellspacing="1" style="margin-left:5px; margin-right:5px;">
                                <tr>
                                    <td><asp:Label ID="lbl" runat="server" SkinID="lblcb" >CRITERIOS DE BÚSQUEDA</asp:Label></td></tr>
                                <tr>
                                    <td>
                                        <table class="cbusqueda" style="width: 785px; height:30px" cellspacing="1" cellpadding="2" border="0">
                                            <tr>
                                                <td style="width:10%">Perfil</td>
                                                <td style="width:45%"><asp:TextBox ID="txtPerfil" runat="server" Width="250px" AutoPostBack="false" ></asp:TextBox></td>
                                                <td style="width:10%">Estado</td>
                                                <td style="width:35%"><uc1:CboEstado ID="cboEstado" runat="server" /></td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" width="785px">
                                            <tr>
                                                <td><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="785px" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="padding-top:15px">
                                        <asp:UpdatePanel ID="upPerfiles" runat="server" UpdateMode="Conditional">
                                            <contenttemplate>
                                                <asp:GridView ID="gvPerfiles" runat="server" AutoGenerateColumns="false"
                                                    AllowPaging="true" AllowSorting="true" SkinID="Grilla" Width="100%"
                                                    DataKeyNames="NID_PERFIL,fl_obligatorio" 
                                                    OnRowDataBound="gvPerfiles_RowDataBound"
                                                    OnSorting="gvPerfiles_Sorting"
                                                    OnPageIndexChanging="gvPerfiles_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="VDEPRF" HeaderText="Perfil" SortExpression="VDEPRF"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="70%" >
                                                            <ItemStyle Width="70%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NRO_USUARIOS" HeaderText="Nro. Usuarios<br>Asignados" SortExpression="NRO_USUARIOS" 
                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="22%" HtmlEncode ="false">
                                                            <ItemStyle Width="22%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VFL_INACTIVO" HeaderText="Estado" SortExpression="VFL_INACTIVO" 
                                                            ItemStyle-HorizontalAlign="left" HeaderStyle-Width="8%">
                                                            <ItemStyle Width="8%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvPerfiles" EventName="Sorting" /> 
                                                <asp:AsyncPostBackTrigger ControlID="gvPerfiles" EventName="PageIndexChanging" /> 
                                            </triggers>
                                        </asp:UpdatePanel>
                                        <asp:HiddenField ID="txhPerfilID" runat="server" />
                                        <asp:HiddenField ID="txhFlObligatorio" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><!-- Pie -->
                        <td><img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <script type="text/javascript">
        setTabCabeceraOn("0");
    </script>
</asp:Content>

