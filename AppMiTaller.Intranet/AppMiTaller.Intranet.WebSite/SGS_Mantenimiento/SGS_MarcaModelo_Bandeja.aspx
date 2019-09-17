<%@ Page Language="C#" 
    MasterPageFile="~/Mantenimientos.master" 
    AutoEventWireup="true" 
    Theme="Default" 
    CodeFile="SGS_MarcaModelo_Bandeja.aspx.cs" 
    Inherits="SGS_Mantenimiento_SGS_MarcaModelo_Bandeja" %>

<%@ MasterType VirtualPath="~/Mantenimientos.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="ComboEstado" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function fc_Eliminar(){
    idMarca = fc_Trim(document.getElementById("<%=this.txhIdMarca.ClientID %>").value);
    if(idMarca == ""){
        alert(mstrSeleccioneUno);
        return false;
    }
    if(parseInt(fc_Trim(idMarca),10) < 0){
        alert(mstrRegistroInactivo);
        return false;
    }
    return confirm(mstrSeguroEliminarUno)
}
function fc_LimpiarMarca(){    
    document.getElementById("<%=this.txtNomMarca.ClientID %>").value = "";
    document.getElementById("<%=this.cboEstadoMarca.ClientID %>_cboEstado").value = "0";
    return false;
}
function fc_BuscarMarca(){
    var mstrError = "";
    
    if(fc_Trim(document.getElementById("<%=this.txtNomMarca.ClientID %>").value)!="")
        if(!fc_Trim(document.getElementById("<%=this.txtNomMarca.ClientID %>").value).match(RE_ALAFANUMERICO))
            mstrError = mstrElCampo + "marca" +mstrReAlfanumerico;
     
    if(mstrError == "")
        return true
    else
        alert(mstrError);
        
    return false;
}
</script>
<cc1:tabcontainer ID="tabContMarca" runat="server" activetabindex="0" CssClass="">
<cc1:TabPanel ID="tabMarca" runat="server" CssClass="">
    <HeaderTemplate>
        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width:30%" align="left" valign="bottom">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif"/></td>
                            <td class="TabCabeceraOn" >Marcas</td>
                            <td><img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif"/></td>
                        </tr>
                    </table>
               </td>
               <td valign="bottom"> 
                    <table border="0" cellpadding="0" cellspacing="0" class="TablaIconosMantenimientos">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnAgregarMarca" runat="server" ToolTip="Agregar" ImageUrl="~/Images/iconos/b-nuevo.gif" 
                                onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"
                                OnClick="btnAgregarMarca_Click" />           
                            <asp:ImageButton ID="btnEliminarMarca" runat="server" ToolTip="Eliminar" ImageUrl="~/Images/iconos/b-eliminar.gif" 
                                onmouseover="javascript:this.src='../Images/iconos/b-eliminar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-eliminar.gif'"
                                OnClick="btnEliminarMarca_Click" OnClientClick="javascript:return fc_Eliminar();"/>
                            <asp:ImageButton ID="btnBuscarMarca" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif" 
                                onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"
                                OnClick="btnBuscarMarca_Click" OnClientClick="javascript:return fc_BuscarMarca();"/>                                
                            <asp:ImageButton ID="btnLimpiarMarca" runat="server" ToolTip="Limpiar" ImageUrl="~/Images/iconos/b-limpiar.gif" 
                                onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'"
                                OnClientClick="javascript:return fc_LimpiarMarca();"/>
                            </td>
                        </tr>
                    </table>
               </td>
            </tr>
        </table>        
    </HeaderTemplate>    
    <ContentTemplate>
        <table width="800px" cellpadding="0" cellspacing="0" border="0">
            <tr><!-- Cabecera -->
                <td><img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
            </tr>
            <tr><!-- Cuerpo -->
                <td style="background-color:#ffffff;vertical-align: top; height:450px; width:785px;">
                    <table cellpadding="1" cellspacing="1" width="785px" style="margin-left:5px; margin-right:5px;" border="0">
                        <tr><td><asp:Label ID="lbl" runat="server" SkinID="lblcb" >CRITERIOS DE BÚSQUEDA</asp:Label></td></tr>
                        <tr>
                            <td>    
                                <table class="cbusqueda" style="width: 785px; height:30px" cellspacing="1" cellpadding="2" border="0">
                                    <tr>
                                        <td style="width: 8%">Marca</td>
                                        <td style="width: 32%"><asp:TextBox ID="txtNomMarca" runat="server" MaxLength="100" Columns="40"></asp:TextBox></td>
                                        <td style="width: 10%">Estado</td>
                                        <td style="width: 5%"><uc1:ComboEstado ID="cboEstadoMarca" runat="server" /></td>
                                    </tr>
                                </table>
                                <table cellpadding="0" cellspacing="0" width="785px">
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="785px" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="padding-top:15px">                                
                                <asp:UpdatePanel id="upMarca" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <asp:HiddenField ID="txhIdMarca" runat="server"></asp:HiddenField>
                                    <asp:GridView ID="gvMarca" runat="server" Width="785px" SkinID="Grilla"
                                        DataKeyNames="nid_marca,fl_inactivo"
                                        AutoGenerateColumns="False"                     
                                        OnRowDataBound="gvMarca_OnRowDataBound"
                                        OnPageIndexChanging="gvMarca_OnPageIndexChanging"
                                        AllowPaging="True"                                         
                                        OnSorting="gvMarca_Sorting"
                                        AllowSorting="true">                                        
                                        <Columns>
                                            <asp:BoundField DataField="co_marca" HeaderText="Código" SortExpression="co_marca" HeaderStyle-Width="40%">
                                                <ItemStyle Width="20%" />
                                                <HeaderStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="no_marca" HeaderText="Marca" SortExpression="no_marca" HeaderStyle-Width="50%">
                                                <ItemStyle Width="70%" />
                                                <HeaderStyle Width="70%" />
                                            </asp:BoundField>                
                                            <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" HeaderStyle-Width="10%">
                                                <ItemStyle Width="10%" />
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    </ContentTemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvMarca" EventName="Sorting"></asp:AsyncPostBackTrigger>  
                                        <asp:AsyncPostBackTrigger ControlID="gvMarca" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>            
                                    </triggers>
                                </asp:UpdatePanel>
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
</cc1:tabcontainer>
<script type="text/javascript">
    setTabCabeceraOn("0");
</script>
</asp:Content>

