<%@ Import Namespace="AppMiTaller.Intranet.BE" %>

<%@ Page Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" CodeFile="SGS_Destino_Bandeja.aspx.cs" Inherits="SGS_Mantenimiento_SGS_Destino_Bandeja" Theme="Default" %>
<%@ MasterType VirtualPath="~/Mantenimientos.master" %>
<%@ Register Src="~/SGS_UserControl/ComboTipoDestino.ascx" TagName="ComboTipoDestino" TagPrefix="uc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="ComboEstado" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


<script language="javascript" type="text/javascript">
var mstrError = "";
    function Fc_SeleccionaItem(valor) {
        document.getElementById("<%=this.txhIdDestinos.ClientID %>").value = valor;
    }
    function Fc_Elimina() {
        if (fc_Trim(document.getElementById('<%=this.txhIdDestinos.ClientID%>').value) == "") {
            alert(mstrSeleccioneUno);
            return false;
        }

        if (parseFloat(fc_Trim(document.getElementById('<%=this.txhIdDestinos.ClientID%>').value)) < 0) {
            alert(mstrRegistroInactivo);
            return false;
        }
        return confirm(mstrSeguroEliminarUno);
    }
    function Fc_Limpiar() {
        document.getElementById("<%=this.cboTipoDestino.ClientID %>_cboTipoDestino").value = "";
        document.getElementById("<%=this.txtRuc.ClientID %>").value = "";
        document.getElementById("<%=this.txtDescripcion.ClientID %>").value = "";
        document.getElementById("<%=this.ComboEstado1.ClientID %>_cboEstado").value = "0";
        return false;
    }
    function Fc_Valida() {
        if (fc_Trim(document.getElementById("<%=this.txtRuc.ClientID %>").value) != "") {

            if (fc_Trim(document.getElementById("<%=this.txtRuc.ClientID %>").value).match(RE_SOLONRO)) {
                var cadenaRuc = fc_ValidarRuc('<%=this.txtRuc.ClientID %>');
                if (fc_Trim(cadenaRuc) != "")
                    mstrError += cadenaRuc + ".\n";
            }
            else {
                mstrError += mstrElCampo + "RUC" + mstrReSoloNro;
            }
        }


        if (fc_Trim(document.getElementById("<%=this.txtDescripcion.ClientID %>").value) != "") {

            if (!fc_Trim(document.getElementById("<%=this.txtDescripcion.ClientID %>").value).match(RE_ALAFANUMERICO)) {
                mstrError += mstrElCampo + "descripción" + mstrReAlfanumerico;
            }
        }

        if (mstrError != "") {
            alert(mstrError);
            mstrError = "";
            return false;
        }
        return true;
    }
    function fc_exportar() {
        var Server = '<%=Convert.ToString(ConfigurationManager.AppSettings["ServerReporting"])%>/Pages/ReportViewer.aspx?/<%=Convert.ToString(ConfigurationManager.AppSettings["FolderReporting"])%>';
        var RDL = 'sga_rpt_ubicacion';

        try {
            var va_co_destino = fc_Trim(document.getElementById("<%=this.cboTipoDestino.ClientID %>_cboTipoDestino").value);
            var va_nro_ruc = fc_Trim(document.getElementById("<%=this.txtRuc.ClientID %>").value);
            var va_descripcion = fc_Trim(document.getElementById("<%=this.txtDescripcion.ClientID %>").value);
            var va_co_estado = fc_Trim(document.getElementById("<%=this.ComboEstado1.ClientID %>_cboEstado").value);

            var mstrMsgReporteCampoVacio = 'TODOS';
            var no_destino = (document.getElementById("<%=this.cboTipoDestino.ClientID %>_cboTipoDestino").selectedIndex != 0 ? document.getElementById("<%=this.cboTipoDestino.ClientID %>_cboTipoDestino")[document.getElementById("<%=this.cboTipoDestino.ClientID %>_cboTipoDestino").selectedIndex].text : mstrMsgReporteCampoVacio);
            var no_estado = (document.getElementById("<%=this.ComboEstado1.ClientID%>_cboEstado").selectedIndex != 0 ? document.getElementById("<%=this.ComboEstado1.ClientID %>_cboEstado")[document.getElementById("<%=this.ComboEstado1.ClientID %>_cboEstado").selectedIndex].text : mstrMsgReporteCampoVacio);

            Parametros = "&vi_va_cod_tipo_ubicacion=" + va_co_destino;
            Parametros += "&vi_va_ruc=" + va_nro_ruc;
            Parametros += "&vi_va_ubicacion=" + va_descripcion;
            Parametros += "&vi_ch_cod_estado=" + va_co_estado;

            Parametros += "&vl_no_destino=" + no_destino;
            Parametros += "&vl_no_estado=" + no_estado;

            url = Server + RDL + '&rs%3aCommand=Render&rc:Parameters=False' + Parametros
            window.open(url, '', 'toolbar=no,left=0,top=0,width=' + screen.width + ',height=' + screen.height + ', directories=no, status=no, scrollbars=yes, resizable=yes, menubar=no');
        }
        catch (ex) {
            alert(ex.message);
        }
        return false;
    }
 
</script>
   
    <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
        <tr>
            <td align="right">
                <asp:ImageButton id="btnExportar" runat="server" imageurl="../images/iconos/b-Rundown.gif"
                    ToolTip="Exportar" OnClientClick = "javascript: return fc_exportar();"
                    onmouseover="javascript:this.src='../Images/iconos/b-Rundown2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-Rundown.gif'" />
                <asp:ImageButton id="btnNuevo" runat="server" imageurl="../images/iconos/b-nuevo.gif"
                    onclick="btnNuevo_Click" ToolTip="Agregar" 
                    onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'" />
                
                <asp:ImageButton id="btnEliminar" runat="server" imageurl="~/Images/iconos/b-eliminar.gif"
                    onclick="btnEliminar_Click" onclientclick="javaScript: return Fc_Elimina();" ToolTip="Eliminar"
                    onmouseover="javascript:this.src='../Images/iconos/b-eliminar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-eliminar.gif'" />
               
               <asp:ImageButton id="btnBuscar" runat="server" imageurl="~/Images/iconos/b-buscar.gif"
                    onclick="btnBuscar_Click" ToolTip="Buscar" onclientclick="javaScript: return Fc_Valida();" 
                    onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" />
                
                <asp:ImageButton id="btnLimpiar" runat="server" imageurl="~/Images/iconos/b-limpiar.gif" 
                    onclientclick="javaScript: return Fc_Limpiar();" ToolTip="Limpiar" 
                    onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'" />
                
            </td>
        </tr>
    </table>
    <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td><img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
            <td class="TabCabeceraOff">Destinos</td>
            <td><img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
        </tr>
    </table>
     <table width="800px" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <!-- Cabecera -->
            <td>
                <img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
        </tr>
        <tr>
            <!-- Cuerpo -->
            <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                <table cellpadding="1" cellspacing="1" style="margin-left:5px; margin-right:5px;">  
                    <tr>
                        <td><asp:Label ID="lbl" runat="server" SkinID="lblcb" >CRITERIOS DE BÚSQUEDA</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <table class="cbusqueda" style="width: 785px; height:50px" cellspacing="1" cellpadding="2" border="0">
                                <tr>
                                    <td style="width: 12%">Tipo Destino</td>
                                    <td style="width: 40%"><uc1:ComboTipoDestino ID="cboTipoDestino" runat="server" Width="250" OnSelectedIndexChanged="ComboTipo_SelectedIndexChanged" AutoPostBack="false"/></td>
                                    <td style="width: 10%">RUC</td>
                                    <td><asp:TextBox id="txtRuc" maxlength="11" runat="server" width="85px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Descripción</td>
                                    <td><asp:TextBox id="txtDescripcion" runat="server" width="213px"></asp:TextBox></td>
                                    <td>Estado</td>
                                    <td><uc2:ComboEstado ID="ComboEstado1" runat="server" /></td>
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
                            <asp:UpdatePanel id="upMantenimiento" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField runat="server" id="txhIdDestinos" />
                                    <asp:GridView ID="grwDestino" SkinID="Grilla" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                        DataKeyNames="Id_ubicacion,Cod_estado"  OnPageIndexChanging="grwDestino_PageIndexChanging"
                                        OnRowDataBound="grwDestino_RowDataBound" 
                                        OnSorting="grwDestino_Sorting" AllowSorting="true" Width="785px">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo_ubicacion" HeaderText="Tipo" SortExpression="Tipo_ubicacion" HeaderStyle-Width="13%" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Nom_corto_ubicacion" HeaderText="Descripción" SortExpression="Nom_corto_ubicacion" HeaderStyle-Width="17%" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" HeaderStyle-Width="20%" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Nom_dpto" HeaderText="Dpto." SortExpression="Nom_dpto" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Nom_provincia" HeaderText="Prov." SortExpression="Nom_provincia" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Nom_distrito" HeaderText="Dist." SortExpression="Nom_distrito" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Dsc_estado" HeaderText="Estado" SortExpression="Dsc_estado" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>    
            <td><img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
        </tr>
    </table>
    <script type="text/javascript">
        setTabCabeceraOn("0");
    </script>
</asp:Content>
