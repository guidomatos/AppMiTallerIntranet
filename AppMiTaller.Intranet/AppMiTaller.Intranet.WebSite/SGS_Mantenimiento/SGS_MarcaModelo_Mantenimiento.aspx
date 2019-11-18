<%@ Import Namespace="AppMiTaller.Intranet.BE" %>
<%@ Page Language="C#" MasterPageFile="~/Mantenimientos.master" AutoEventWireup="true" Theme="Default" CodeFile="SGS_MarcaModelo_Mantenimiento.aspx.cs" Inherits="SGS_Mantenimiento_SGS_MarcaModelo_Mantenimiento" %>

<%@ MasterType VirtualPath="~/Mantenimientos.master" %>
<%@ Register Src="~/SGS_UserControl/ComboNegocio.ascx" TagName="ComboNegocio" TagPrefix="uc3" %>
<%@ Register Src="~/SGS_UserControl/ComboLinea.ascx" TagName="ComboLinea" TagPrefix="uc4" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="ComboEstado" TagPrefix="uc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEmpresa.ascx" TagName="ComboEmpresa" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">

        var blnFlagCambiado=false;

        function fc_GrabarMarca() {
            if (fc_ValidaMarca()) {
                if (confirm(mstrSeguroGrabar)) {
                    return true;
                }
            }
            return false;
        }
        function fc_ValidaMarca() {
            var mstrError = "";

            if (fc_Trim(document.getElementById("<%=this.txtCodMarca.ClientID %>").value) == "")
                mstrError = mstrDebeIngresar + "código.\n";
            else
                if (!fc_Trim(document.getElementById("<%=this.txtCodMarca.ClientID %>").value).match(RE_CODIGO))
                    mstrError += mstrElCampo + "código" + mstrCodigo;

            if (fc_Trim(document.getElementById("<%=this.txtNomMarca.ClientID %>").value) == "")
                mstrError += mstrDebeIngresar + "marca.\n";
            else
                if (!fc_Trim(document.getElementById("<%=this.txtNomMarca.ClientID %>").value).match(RE_ALAFANUMERICO))
                    mstrError += mstrElCampo + "marca" + mstrReAlfanumerico;

            if (fc_Trim(document.getElementById("<%=this.cboEstadoMarca.ClientID %>_cboEstado").value) == "")
                mstrError += mstrDebeSeleccionar + "estado.\n";

            if (mstrError == "")
                return true;
            else
                alert(mstrError);

            return false;
        }
        function fc_EliminarModelo() {
            idModelo = document.getElementById("<%=this.txhIdModelo.ClientID %>").value;
            if (idModelo == "") {
                alert(mstrSeleccioneUno);
                return false;
            }
            if (parseInt(fc_Trim(idModelo), 10) < 0) {
                alert(mstrRegistroInactivo);
                return false;
            }
            if (confirm(mstrSeguroEliminarUno)) {
                return true;
            }
            return false;
        }
        function fc_LimpiarModelo() {
            document.getElementById("<%=this.cboEstadoModelo.ClientID %>_cboEstado").value = "";
            document.getElementById("<%=this.txtCodModelo.ClientID %>").value = "";
            document.getElementById("<%=this.txtNomModelo.ClientID %>").value = "";
            return false;
        }
        function fc_BuscarModelo() {
            var mstrError = "";

            if (fc_Trim(document.getElementById("<%=this.txtCodModelo.ClientID %>").value) != "")
                if (!fc_Trim(document.getElementById("<%=this.txtCodModelo.ClientID %>").value).match(RE_CODIGO))
                    mstrError = mstrElCampo + "código modelo" + mstrCodigo;

            if (fc_Trim(document.getElementById("<%=this.txtNomModelo.ClientID %>").value) != "")
                if (!fc_Trim(document.getElementById("<%=this.txtNomModelo.ClientID %>").value).match(RE_ALAFANUMERICO))
                    mstrError += mstrElCampo + "modelo" + mstrReAlfanumerico;

            if (mstrError == "")
                return true;
            else
                alert(mstrError);

            return false;
        }
        function fc_CancelarPoput() {
            document.getElementById("<%=this.txtCodModeloPoput.ClientID %>").value = "";
            document.getElementById("<%=this.txtNomModeloPoput.ClientID %>").value = "";
            document.getElementById("<%=this.cboLineaModeloPoput.ClientID %>_cboLinea").value = "";
            document.getElementById("<%=this.cboLineaComercialModeloPoput.ClientID %>").value = "";
            document.getElementById("<%=this.cboEstadoModeloPoput.ClientID %>_cboEstado").value = "0";
        }
        function fc_Grabar() {
            if (fc_Valida()) {
                if (blnFlagCambiado) {
                    return true;
                }
                else {
                    if (confirm(mstrSeguroGrabar)) return true;
                }
            }
            return false;
        }
        function fc_Valida() {
            var mstrError = "";


            if (fc_Trim(document.getElementById("<%=this.txtCodModeloPoput.ClientID %>").value) == "")
                mstrError = mstrDebeIngresar + "código modelo.\n";
            else
                if (!fc_Trim(document.getElementById("<%=this.txtCodModeloPoput.ClientID %>").value).match(RE_CODIGO))
                    mstrError += mstrElCampo + "código modelo" + mstrCodigo;

            if (fc_Trim(document.getElementById("<%=this.txtNomModeloPoput.ClientID %>").value) == "")
                mstrError += mstrDebeIngresar + "modelo.\n";
            else
                if (!fc_Trim(document.getElementById("<%=this.txtNomModeloPoput.ClientID %>").value).match(RE_ALAFANUMERICO))
                    mstrError += mstrElCampo + "modelo" + mstrReAlfanumerico;

            if (fc_Trim(document.getElementById("<%=this.cboNegocioModeloPoput.ClientID %>_cboNegocio").value) == "")
                mstrError += mstrDebeSeleccionar + "familia.\n";

            if (fc_Trim(document.getElementById("<%=this.cboLineaModeloPoput.ClientID %>_cboLinea").value) == "")
                mstrError += mstrDebeSeleccionar + "línea.\n";

            if (fc_Trim(document.getElementById("<%=this.cboLineaComercialModeloPoput.ClientID %>").value) == "")
                mstrError += mstrDebeSeleccionar + "sub línea.\n";

            if (fc_Trim(document.getElementById("<%=this.cboEstadoModeloPoput.ClientID %>_cboEstado").value) == "")
                mstrError += mstrDebeSeleccionar + "estado.\n";

            if (mstrError == "")
                return true;
            else
                alert(mstrError);

            return false;
        }
        function fc_GrabarModelo() {
            document.getElementById("<%=this.btnGrabarModelo1.ClientID %>").click();
            return false;
        }
        function fc_CerrarModelo(){
            fc_CancelarPoput();
            RemoveModal('1');
            return false;
        }

        function DisplayModal(opt) {
            if (opt == "1") {
                document.getElementById("overlay").style.height = '100%';
                document.getElementById("overlay").className = "OverlayEffect";
                document.getElementById("modalMsg").className = "ShowModal";
            } else if (opt == "2") {
                document.getElementById("overlay1").style.height = '100%';
                document.getElementById("overlay1").className = "OverlayEffect";
                document.getElementById("modalMsg1").className = "ShowModal";
            }
            return true;
        }
        function RemoveModal(opt) {
            if (opt == "1") {
                document.getElementById("modalMsg").className = "HideModal";
                document.getElementById("overlay").className = "";
                fc_CancelarPoput();
            } else if (opt == "2") {
                document.getElementById("modalMsg1").className = "HideModal";
                document.getElementById("overlay1").className = "";
                Fc_CancelarPoputSalida()
            }
            return true;
        }

</script>

 <style type="text/css">
        .ShowModal
        {
            top: 150px;
            left: 300px;
            z-index: 1000;
            position: absolute;
            background-color: White;
            display: block;
        }
        .HideModal
        {
            display: none;
        }
        .OverlayEffect
        {
            background-color: #000000;
            filter: alpha(opacity=70);
            opacity: 0.7;
            width: 100%;
            height: 100%;
            z-index: 400;
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>
 

<table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientosMarca">
    <tr>
        <td align="right">
            <!--Marca-->
            <asp:ImageButton ID="btnGrabarMarca" runat="server" ImageUrl="~/Images/iconos/b-guardar.gif" ToolTip="Grabar" Visible="false" 
                OnClick="btnGrabarMarca_Click" OnClientClick="javascript:return fc_GrabarMarca();" onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"/>
            <asp:ImageButton ID="btnRegresarMarca" runat="server" ImageUrl="~/Images/iconos/b-regresar.gif" ToolTip="Regresar" Visible="false"
                OnClick="btnRegresarMarca_Click" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" />
            <!--/Marca-->
            <!--Modelo-->
            <asp:ImageButton ID="btnAgregarModeloPoput" runat="server" style="display: none;"/>

             <asp:ImageButton ID="btnModificarModelo" runat="server" style="display: none"
                OnClick="btnModificarModelo_Click" OnClientClick="return DisplayModal('1');"/>
            <asp:ImageButton ID="btnAgregarModelo" runat="server" ImageUrl="~/Images/iconos/b-agregaritem.gif" ToolTip="Agregar" Visible="false" OnClientClick="javascript:return DisplayModal('1');"
                OnClick="btnAgregarModelo_Click" onmouseover="javascript:this.src='../Images/iconos/b-agregaritem2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-agregaritem.gif'" />
            <asp:ImageButton ID="btnQuitarModelo" runat="server" ImageUrl="~/Images/iconos/b-eliminaritem.gif" ToolTip="Eliminar" Visible="false"
                OnClick="btnQuitarModelo_Click" OnClientClick="javascript:return fc_EliminarModelo();" onmouseover="javascript:this.src='../Images/iconos/b-eliminaritem2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-eliminaritem.gif'"/>
            <asp:ImageButton ID="btnBuscarModelo" runat="server" ImageUrl="~/Images/iconos/b-buscar.gif" ToolTip="Buscar" Visible="false"
                OnClick="btnBuscarModelo_Click" OnClientClick="javascript:return fc_BuscarModelo();" onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" />
            <asp:ImageButton ID="btnLimpiarModelo" runat="server" ImageUrl="~/Images/iconos/b-limpiar.gif" ToolTip="Limpiar" Visible="false"
                OnClientClick="javascript:return fc_LimpiarModelo();" onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'" />
            <asp:ImageButton ID="btnRegresarModelo" runat="server" ImageUrl="~/Images/iconos/b-regresar.gif" ToolTip="Regresar" Visible="false"
                OnClick="btnRegresarMarca_Click" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" />
            <asp:ImageButton ID="btnRegresar" runat="server" ImageUrl="~/Images/iconos/b-regresar.gif" ToolTip="Regresar" Visible="true"
                OnClick="btnRegresarMarca_Click" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" />
        </td>
    </tr>
</table>
<cc1:TabContainer ID="tabContMarca" runat="server" activetabindex="0" CssClass=""
    OnActiveTabChanged="tabContMarca_ActiveTabChanged" AutoPostBack="true" >
    <cc1:TabPanel ID="tabMarca" runat="server" CssClass="">
        <HeaderTemplate>
            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                    <td class="TabCabeceraOff" onmouseover="javascript: onTabCabeceraOver('0');" onmouseout="javascript: onTabCabeceraOut('0');">                        
                        <%= this.tipoAccion %> Marca
                    </td>
                    <td><img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                </tr>
            </table>
        </HeaderTemplate>    
        <ContentTemplate>
            <table ID="tblMarca" runat="server" width="800px" cellpadding="0" cellspacing="0" border="0">
                <tr>                    
                    <td><img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                </tr>
                <tr>
                    <td style="background-color:#ffffff;vertical-align: top; height:450px; width:785px;">
                        <table width="785px" cellpadding="2" cellspacing="1" border="0" class="cuerponuevo" style="margin-left:3px; margin-right:3px; margin-top:3px; height:70px;">         
                            <tr>
                                <td style="width: 15%">Código</td>
                                <td style="width: 35%"><asp:TextBox ID="txtCodMarca" runat="server" Columns="10" MaxLength="20" SkinID="txtob"></asp:TextBox></td>
                                <td style="width: 50%" rowspan="9" align="center">
                                    <table cellpadding="1" cellspacing="1" style="width:280px; height:210px;">
                                        <tr>
                                            <td>
                                                <img id="imgLogo" alt="" runat="server" visible="false" style="border:1px; border-color:#000000;" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Marca</td>
                                <td><asp:TextBox ID="txtNomMarca" runat="server" width="225px" MaxLength="50" SkinId="txtob"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Empresa</td>
                                <td><uc2:ComboEmpresa id="cboEmpMarca" runat="server" CssClass="cboob" Width="230"></uc2:ComboEmpresa></td>
                            </tr>        
                            <tr>
                                <td></td>                                
                                <td>
                                    <asp:Label ID="lblTamanoRecomendado" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Estado</td>
                                <td><span id="spanCboEstadoMarca" runat="server" ></span></td>
                            </tr>
                        </table>                            
                    </td>
                </tr>
                <tr>                    
                    <td><img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
                </tr>
            </table>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="tabModelo" runat="server" CssClass="">
        <HeaderTemplate>
            <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><img id="img3" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                    <td class="TabCabeceraOff" onmouseover="javascript: onTabCabeceraOver('1');" onmouseout="javascript: onTabCabeceraOut('1');">Modelo</td>
                    <td><img id="img4" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                </tr>
            </table>
        </HeaderTemplate>
        <ContentTemplate>
            <table ID="tblModelo" runat="server" width="800px" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                </tr>
                <tr>                    
                    <td style="background-color:#ffffff;vertical-align: top; height:450px; width:785px;">
                    <table cellpadding="1" cellspacing="1" width="785px" style="margin-left:5px; margin-right:5px;" border="0">
                        <tr>
                            <td><asp:Label ID="Label1" runat="server" SkinID="lblCB">CRITERIOS DE BÚSQUEDA</asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" style="width: 100%" class="cbusqueda">
                                    <tr>
                                        <td style="width: 15%">Marca</td>
                                        <td style="width: 30%"><asp:TextBox ID="txtNomMarModelo" runat="server" Columns="30" ReadOnly="True" SkinID="txtdes"></asp:TextBox></td>
                                        <td style="width: 15%">Estado</td>
                                        <td style="width: 40%"><uc1:ComboEstado ID="cboEstadoModelo" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td>Código Modelo</td>
                                        <td><asp:TextBox ID="txtCodModelo" runat="server" Columns="10" MaxLength="20" ></asp:TextBox></td>
                                        <td>Modelo</td>
                                        <td><asp:TextBox ID="txtNomModelo" runat="server" Columns="30" MaxLength="50" ></asp:TextBox></td>
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
                                <asp:UpdatePanel id="upGvCiudad" runat="server" UpdateMode="Conditional">
                                    <contenttemplate>
                                        <asp:hiddenfield ID="txhIdModelo" runat="server"></asp:hiddenfield>
                                        <asp:HiddenField ID="txhNombreArchivoBD" runat="server"/>
                                        <asp:GridView ID="gvModelo" runat="server" Width="785px" SkinId="Grilla"
                                            DataKeyNames="nid_modelo,fl_inactivo"
                                            AutoGenerateColumns="False" 
                                            OnRowDataBound="gvModelo_RowDataBound" 
                                            OnPageIndexChanging="gvModelo_PageIndexChanging"
                                            AllowPaging="True" 
                                            OnSorting="gvModelo_Sorting" 
                                            AllowSorting="True">
                                            <PagerStyle HorizontalAlign="Right"/>
                                            <Columns>
                                                <asp:BoundField DataField="co_modelo" HeaderText="Codigo" SortExpression="co_modelo">
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="no_modelo" HeaderText="Modelo" SortExpression="no_modelo">
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle Width="20%" />
                                                </asp:BoundField>
                                                   <asp:BoundField DataField="no_familia" HeaderText="Familia" SortExpression="no_familia">
                                                    <ItemStyle Width="20%" />
                                                    <HeaderStyle Width="20%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="no_linea_importacion" HeaderText="Linea" SortExpression="no_linea_importacion">
                                                    <ItemStyle Width="15%" />
                                                    <HeaderStyle Width="15%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="no_linea_comercial" HeaderText="Sub Linea" SortExpression="no_linea_comercial">
                                                    <ItemStyle Width="17%" />
                                                    <HeaderStyle Width="17%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado">
                                                    <ItemStyle Width="8%" />
                                                    <HeaderStyle Width="8%" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvModelo" EventName="Sorting"></asp:AsyncPostBackTrigger>
                                        <asp:AsyncPostBackTrigger ControlID="gvModelo" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarModelo" EventName="Click"></asp:AsyncPostBackTrigger>
                                        <asp:AsyncPostBackTrigger ControlID="btnModificarModelo" EventName="Click"></asp:AsyncPostBackTrigger>
                                    </triggers>
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
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
    
<div id="overlay"></div>
<div id="modalMsg" style="width: 470px; height: 200px;" class="HideModal">

    <asp:Panel ID="pnlRegistroModelo" runat="server" CssClass="PanelPopup_g" Style="display: block; width:470px;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height:44px;">
        <tr>
            <td class="TitleL">&nbsp;</td>
            <td class="TitleC" style="width: 175px;">&nbsp;</td>
            <td class="TitleR">&nbsp;</td>
        </tr>
    </table>
    <table width="450px" cellpadding="0" cellspacing="0" border="0" style=" margin-left:10px; margin-right:10px;">
        <tr style="display:none">
            <td align="right">
                <asp:Button ID="btnGrabarModelo1" runat="server" 
                    OnClick="btnGrabarModelo_Click" OnClientClick="javascript: return fc_Grabar();"></asp:Button>
                <asp:ImageButton ID="btnCerrarModelo1" runat="server" ></asp:ImageButton>
            </td>
        </tr>
        <tr><!--cuerpo-->
            <td style="width:100%;">
                <asp:UpdatePanel id="upDetalleCiudad" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <!--Tab-->
                        <table cellpadding="0" cellspacing="0" >
                            <tr>
                                <td style="width: 150px" valign="bottom">
                                    <table id="Table1" cellpadding="0" cellspacing="0" border="0" >
                                        <tr>
                                            <td><img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                            <td class="TabCabeceraOn" >
                                                <asp:label ID="lblModelo" runat="server" text="Nuevo Modelo"></asp:label>
                                            </td>
                                            <td><img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="width: 320px">
                                    <asp:ImageButton ID="btnGrabarModelo" runat="server" ImageUrl="~/Images/iconos/b-guardar.gif" ToolTip="Grabar" 
                                        OnClientClick="javascript: return fc_GrabarModelo();" 
                                        onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" 
                                        onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"></asp:ImageButton>
                                    <asp:ImageButton ID="btnCerrarModelo" runat="server" ImageUrl="~/Images/iconos/b-cerrar.gif" ToolTip="Cerrar"
                                        OnClientClick="javascript: return fc_CerrarModelo();"
                                        onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'" 
                                        onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                        <!--/Tab-->
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="bottom"><img alt="" src="../Images/Mantenimiento/fbarr.gif" width="450px" /></td>   
                            </tr>
                            <tr>
                                <td style="background-color: #ffffff; vertical-align: top; width:540px; height:120px; padding-left:5px; padding-right:5px;">
                                    <table style="width: 100%" border="0" cellpadding="1" cellspacing="1" class="cuerponuevo">
                                        <tr>
                                            <td style="width: 40%">Marca</td>
                                            <td style="width: 60%"><asp:TextBox ID="txtNomMarModeloPoput" runat="server" Columns="30" ReadOnly="True" SkinID="txtdes"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Código Modelo</td>
                                            <td><asp:TextBox ID="txtCodModeloPoput" runat="server" Columns="10" MaxLength="20" SkinID="txtob" ></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Modelo</td>
                                            <td><asp:TextBox ID="txtNomModeloPoput" runat="server" Columns="30" MaxLength="50" SkinID="txtob"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Familia</td>
                                            <td><uc3:ComboNegocio id="cboNegocioModeloPoput" runat="server" AutoPostBack="true" CssClass="cboob"
                                                    OnSelectedIndexChanged="cboNegocioModeloPoput_OnSelectedIndexChanged" Width="150"></uc3:ComboNegocio></td>
                                        </tr>
                                        <tr>
                                            <td>Línea</td>
                                            <td><uc4:ComboLinea id="cboLineaModeloPoput" runat="server" CssClass="cboob" OnSelectedIndexChanged="cboLineaModeloPoput_OnSelectedIndexChanged" AutoPostBack="true"></uc4:ComboLinea></td>
                                        </tr>
                                        <tr>
                                            <td>Sub Línea</td>
                                            <td><asp:DropDownList ID="cboLineaComercialModeloPoput" runat="server" Width="150px" CssClass="cboob"/></td>
                                        </tr>
                                        <tr>
                                            <td>Estado</td>
                                            <td><span id="spanCboEstadoModeloPoput" runat="server" ></span></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregarModelo" EventName="Click"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="btnModificarModelo" EventName="Click"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="cboNegocioModeloPoput" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="cboLineaModeloPoput" EventName="SelectedIndexChanged" />  
                        <asp:AsyncPostBackTrigger ControlID="cboNegocioModeloPoput" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                   </triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr><!--pie-->
            <td><img alt="" src="../Images/Mantenimiento/fba.gif" width="450" /></td>
        </tr>
    </table>
</asp:Panel>

</div>

<script type="text/javascript">
    if ("<%=this.indiceTabOn %>" != "") setTabCabeceraOn("<%=this.indiceTabOn %>");
</script>
</asp:Content>