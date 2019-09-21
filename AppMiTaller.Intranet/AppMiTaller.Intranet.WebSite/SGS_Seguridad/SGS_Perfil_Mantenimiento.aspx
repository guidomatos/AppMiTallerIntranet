<%@ Page Language="C#" MasterPageFile="~/Seguridad.master" AutoEventWireup="true"
    CodeFile="SGS_Perfil_Mantenimiento.aspx.cs" Inherits="SGS_Seguridad_SGS_Perfil_Mantenimiento" %>
<%@ MasterType VirtualPath="~/Seguridad.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/TextBoxFecha.ascx" TagName="Fecha" TagPrefix="uc1" %>
<%@ Register Src="~/SGS_UserControl/TextBoxHora.ascx" TagName="Hora" TagPrefix="uc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="CboEstado" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function fc_Grabar()
        {
            var mstrError = "";
            
            if (fc_Trim(document.getElementById("<%=this.txtDscPerfil.ClientID %>").value) == ""  )
                mstrError += mstrDebeIngresar  + "el nombre del perfil.\n";
            else if(!fc_Trim(document.getElementById("<%=this.txtDscPerfil.ClientID %>").value).match(RE_ALAFANUMERICO))
                mstrError += mstrElCampo + "perfil" + mstrReAlfanumerico;
            
            if (fc_Trim(document.getElementById("<%=this.cboEstado.ClientID %>_cboEstado").value) == ""  )
                mstrError += "- Debe seleccionar el estado del perfil.\n";
            
            //Se cambio porque se volvio opcional y no obligatorio
            if (fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value) != "" &&
                !isFecha(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value),"dd/MM/yyyy"))
                mstrError += "- La fecha de inicio " + mstrFecha;

            if (fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value) != "" &&
                !isFecha(fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value),"dd/MM/yyyy"))
                mstrError += "- La fecha de fin " + mstrFecha;
            
            if (fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value) != "" &&
                fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value) != "" &&
                isFecha(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value),"dd/MM/yyyy") &&
                isFecha(fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value),"dd/MM/yyyy"))
            {                
                if(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value)==
                    fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value)){
                        mstrError += "- La fecha de inicio " + mstrMayor + " la fecha fin.";
                    }else{
                        var comp = compareDates(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value),"dd/MM/yyyy",
                                                fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value),"dd/MM/yyyy");
                        if ( comp != "0")
                        mstrError += "- La fecha de inicio " + mstrMayor + " la fecha fin.";
                    }

            }
            
            if (fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value) != "" &&
                !isHora(fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value),"HH:mm"))
                mstrError += "- La hora de inicio " + mstrHora;

            if (fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value) != "" &&
                !isHora(fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value),"HH:mm"))
                mstrError += "- La hora de fin " + mstrHora;
            
            if (fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value) != "" &&
                fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value) != "" &&
                isHora(fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value),"HH:mm") &&
                isHora(fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value),"HH:mm"))
            {
                var comp = compareDates(fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value),"HH:mm",
                                        fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value),"HH:mm");
                if ( comp != "0")
                mstrError += "- La hora de inicio " + mstrMayor + " la hora de fin.";
            }
            
            var strIndicesSel = fc_Replace(document.getElementById('<%=this.txhIndOpciones.ClientID %>').value, "|", "");
            if (strIndicesSel == "")
            {
                mstrError += mstrDebeSeleccionar + "al menos una opción.";
            }
            
            if ( mstrError != "" ) 
            {
                alert(mstrError)
                return false;
            }
            
            return confirm(mstrSeguroGrabar);
        }
        
        function fc_CheckAll(objTrigger, sufijo)
        {
            objTabla = document.getElementById("<%=this.gvOpciones.ClientID %>");
            if (objTabla != null)
            {
                for(var i = 0; i < objTabla.rows.length; i++)
                {
                    var codRel = '0' + (i+2);
                    codRel = codRel.substring(codRel.length, codRel.length - 2)
                    var idChk = "<%=this.gvOpciones.ClientID %>_ctl" + codRel + "_" + sufijo;
                    //alert(idChk);
                    var objChk = document.getElementById(idChk);
                    if(objChk != null)
                    {
                        if(objChk.disabled == false && objTrigger.checked != objChk.checked)
                        {
                            objChk.click();
                        }
                    }
                }
            }
            
            if( sufijo == "chkMant" )
            {
                document.getElementById("<%=this.chkConsAll.ClientID %>").checked = objTrigger.checked;
                if ( objTrigger.checked ) document.getElementById("<%=this.chkConsAll.ClientID %>").disabled = true;
                else document.getElementById("<%=this.chkConsAll.ClientID %>").disabled = false;
            }            
        }
        
        
        function fc_SelOpcion(strChkMant, strChkCons, opcionId, ori, indIcono)
        {
            objChkMant = document.getElementById(strChkMant);
            objChkCons = document.getElementById(strChkCons);
            var indRelacion = "";
            
            if(objChkMant.checked && ori == "MANT")
            {
                if(indIcono == "0")
                {
                    objChkCons.checked = true;
                    objChkCons.disabled = true;
                }
                indRelacion = "A";
            }
            else            
            {
                if(indIcono == "0")
                {
                    objParent = objChkCons.parentElement;
                    objChkCons.disabled = false;
                    
                    if( objParent != null && objParent.tagName == "SPAN")
                    {
                        objParent.disabled = false;
                    }
                    
                    if (ori == "MANT") 
                    {
                        objChkCons.checked = false;
                        document.getElementById("<%=this.chkConsAll.ClientID %>").disabled = false;
                        document.getElementById("<%=this.chkConsAll.ClientID %>").checked = false;
                        document.getElementById("<%=this.chkMantAll.ClientID %>").checked = false;
                    }
                }
                if (objChkCons.checked) indRelacion = "C";
            }

            fc_SetIndRelacion(opcionId, indRelacion);
        }
        
        function fc_SetIndRelacion(opcionId, indRelacion)
        {
            var arrOpciones = document.getElementById('<%=this.txhCodOpciones.ClientID %>').value.split('|');
            var arrIndRelacion = document.getElementById('<%=this.txhIndOpciones.ClientID %>').value.split('|');
            var strRetorno = "";
            
            for (var i = 0; i < arrOpciones.length - 1; i++)
            {
                if (fc_Trim(arrOpciones[i]) == opcionId) 
                {
                    strRetorno += indRelacion + "|";
                }
                else 
                {   
                    strRetorno += arrIndRelacion[i] + "|";
                }    
            }
            
            document.getElementById('<%=this.txhIndOpciones.ClientID %>').value = strRetorno;
        }
        
        function fc_EliminarUsuario()
        {
            if( fc_Trim(document.getElementById('<%=this.txhPerfilUsuarioID.ClientID%>').value) == "" )
            {
                alert(mstrSeleccioneUno);
                return false;
            }
            
            return confirm(mstrSeguroEliminarUno);
        }
        
    </script>

    <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos">
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnGrabar" runat="server" ToolTip="Grabar" 
                    ImageUrl="~/Images/iconos/b-guardar.gif"
                    onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"
                    Visible="false" OnClientClick="javascript: return fc_Grabar();" 
                    OnClick="btnGrabar_OnClick" />
                    
                <asp:ImageButton ID="btnAgregar" runat="server" ToolTip="Agregar Usuario" 
                    ImageUrl="~/Images/iconos/b-agregaritem.gif"
                    onmouseover="javascript:this.src='../Images/iconos/b-agregaritem2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-agregaritem.gif'"
                    OnClientClick="javascript: return fc_NuevoPU();"
                    Visible="false" />
                    
                <asp:ImageButton ID="btnEliminar" runat="server" ToolTip="Eliminar Usuario" 
                    ImageUrl="~/Images/iconos/b-eliminaritem.gif" Visible="false" 
                    onmouseover="javascript:this.src='../Images/iconos/b-eliminaritem2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-eliminaritem.gif'"
                    OnClientClick="javascript: return fc_EliminarUsuario();" 
                    OnClick="btnEliminar_Click" />
                    
                <asp:ImageButton ID="btnRegresar" runat="server" ToolTip="Regresar" 
                    ImageUrl="~/Images/iconos/b-regresar.gif"
                    onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" 
                    onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'"
                    OnClick="btnRegresar_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRefreshOpciones" runat="server" OnClick="btnRefreshOpciones_OnClick" />
            </td>
        </tr>
    </table>
    <cc1:TabContainer ID="tabContPerfil" runat="server" ActiveTabIndex="0" CssClass=""
        OnActiveTabChanged="tabContPerfil_ActiveTabChanged" AutoPostBack="true">
        <cc1:TabPanel ID="tabNuevoPerfil" runat="server" CssClass="">
            <HeaderTemplate>
                <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td><img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                        <td class="TabCabeceraOff" onmouseover="javascript: onTabCabeceraOver('0');" onmouseout="javascript: onTabCabeceraOut('0');"><asp:Label ID="lblTipoPerfil" runat="server" Text=""></asp:Label> Perfil</td>
                        <td><img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ContentTemplate>
                <table width="800px" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <!-- Cabecera -->
                        <td>
                            <img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                    </tr>
                    <tr>
                        <!-- Cuerpo -->
                        <td style="background-color:#ffffff;vertical-align: top; height:450px; width:785px;">
                            <table id="tblNuevoPerfil" runat="server" width="785px" 
                                cellpadding="2" cellspacing="1" border="0" 
                                style="margin-left:5px; margin-right:5px; height:70px;">
                                <tr>
                                    <td colspan="2">
                                        <table width="785px" cellpadding="2" cellspacing="1" border="0" class="cbusqueda">
                                            <tr>
                                                <td>Perfil</td>
                                                <td ><asp:TextBox ID="txtDscPerfil" runat="server" Width="220px" SkinID="txtob"></asp:TextBox></td>
                                                <td style="width:10%">Estado</td>
                                                <td style="width:20%"><uc1:CboEstado ID="cboEstado" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%">Fec. Inicio Acceso</td>
                                                <td style="width: 30%"><uc1:Fecha ID="txtFecIni" runat="server" /></td>
                                                <td style="width: 20%">Fec. Fin Acceso</td>
                                                <td style="width: 30%"><uc1:Fecha ID="txtFecFin" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Hora Inicio</td>
                                                <td><uc1:Hora ID="txtHoraIni" runat="server" /></td>
                                                <td>Hora Fin</td>
                                                <td><uc1:Hora ID="txtHoraFin" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>Concesionario</td>
                                                <td><asp:CheckBox ID="chkConcesionario" runat="server" /></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0">
                                             <tr>
                                                <td><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="785px" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr valign="top" style="padding-top:10px">
                                    <td style="width: 40%; padding-left: 0px; padding-right: 5px">
                                        <asp:HiddenField ID="txhModuloSelected" runat="server" Value="" />
                                        <asp:GridView ID="gvModulos" runat="server" AutoGenerateColumns="false" DataKeyNames="NID_OPCION, CSTRUCT"
                                            SkinID="Grilla" Width="100%" OnRowDataBound="gvModulos_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="VDEMEN" HeaderText="Módulos">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                    <td style="width: 60%; padding-left: 5px; padding-right: 0px">
                                        <asp:UpdatePanel ID="upGvOpciones" runat="server" UpdateMode="Conditional">
                                            <contenttemplate>
                                                <asp:HiddenField ID="txhCodOpciones" runat="server" Value="" />
                                                <asp:HiddenField ID="txhIndOpciones" runat="server" Value="" />
                                                <asp:HiddenField ID="txhNidOpcionPerfil" runat="server" Value="" />
                                                <table width="450px" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td style="width:60%; border-right:0px; height:32px" class="CabeceraGrilla">Opción</td>
                                                        <td style="width:20%; border-right:0px; height:32px" class="CabeceraGrilla">Mant.<br />
                                                            <asp:CheckBox ID="chkMantAll" runat="server" onClick="javascript: return fc_CheckAll(this, 'chkMant');" AutoPostBack="false" />
                                                        </td>
                                                        <td style="width:20%; border-right:0px; height:32px" class="CabeceraGrilla">Cons.<br />
                                                            <asp:CheckBox ID="chkConsAll" runat="server" onClick="javascript: return fc_CheckAll(this, 'chkConst');" AutoPostBack="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="width:100%; height:280px; overflow:auto">
                                                    <asp:GridView ID="gvOpciones" runat="server" AutoGenerateColumns="false"
                                                        DataKeyNames="NID_OPCION, CSTRUCT, IND_REL,fl_icono" ShowHeader="false"
                                                        SkinID="Grilla" Width="450px" OnRowDataBound="gvOpciones_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="VDEMEN" HeaderText="Opción" ItemStyle-HorizontalAlign="Left" 
                                                                    HeaderStyle-Width="60%" ItemStyle-Width="60%"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Mant." ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkMant" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cons." ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkConst" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnRefreshOpciones" EventName="Click" /> 
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <!-- Pie -->
                        <td>
                            <img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabUsuariosAsignados" runat="server" CssClass="">
            <HeaderTemplate>
                <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td><img id="img1" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                        <td class="TabCabeceraOff" onmouseover="javascript: onTabCabeceraOver('1');" onmouseout="javascript: onTabCabeceraOut('1');">Usuarios Asignados</td>
                        <td><img id="img2" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                    </tr>
                </table>
            </HeaderTemplate>
            <ContentTemplate>
                <table width="800px" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <!-- Cabecera -->
                        <td><img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                    </tr>
                    <tr>
                        <!-- Cuerpo -->
                        <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                            <table id="tblUsuariosAsignados" runat="server" width="98%" 
                                cellpadding="0" cellspacing="0" border="0" 
                                style="margin-left:5px; margin-right:5px;">
                                <tr>
                                    <td align="center">
                                        <asp:UpdatePanel ID="upPerfilUsuario" runat="server" UpdateMode="Conditional">
                                            <contenttemplate>
                                                <asp:HiddenField ID="txhPerfilUsuarioID" runat="server" />
                                                <asp:GridView ID="gvPerfilUsuario" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                    AllowSorting="true" SkinID="Grilla" Width="785px" 
                                                    DataKeyNames="NID_PERFIL" 
                                                    OnRowDataBound="gvPerfilUsuario_RowDataBound" 
                                                    OnSorting="gvPerfilUsuario_Sorting" 
                                                    OnPageIndexChanging="gvPerfilUsuario_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="VNOMUSR" HeaderText="Nombres y Apellidos" SortExpression="VNOMUSR" 
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="35%"></asp:BoundField>
                                                        <asp:BoundField DataField="CUSR_ID" HeaderText="Login" SortExpression="CUSR_ID"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%"></asp:BoundField>
                                                        <asp:BoundField DataField="VUSR_TIPO" HeaderText="Tipo" SortExpression="VUSR_TIPO" 
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%"></asp:BoundField>
                                                        <asp:BoundField DataField="SFE_INICIO_ACCESO" HeaderText="Fecha Inicio Acceso" SortExpression="FE_INICIO_ACCESO" 
                                                            ItemStyle-HorizontalAlign="center" HeaderStyle-Width="10%"></asp:BoundField>
                                                        <asp:BoundField DataField="HR_INICIO_ACCESO" HeaderText="Hora Inicio Acceso" SortExpression="HR_INICIO_ACCESO" 
                                                            ItemStyle-HorizontalAlign="center" HeaderStyle-Width="10%"></asp:BoundField>
                                                        <asp:BoundField DataField="SFE_FIN_ACCESO" HeaderText="Fecha Fin Acceso" SortExpression="FE_FIN_ACCESO" 
                                                            ItemStyle-HorizontalAlign="center" HeaderStyle-Width="10%"></asp:BoundField>
                                                        <asp:BoundField DataField="HR_FIN_ACCESO" HeaderText="Hora Fin Acceso" SortExpression="HR_FIN_ACCESO" 
                                                            ItemStyle-HorizontalAlign="center" HeaderStyle-Width="10%"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvPerfilUsuario" EventName="Sorting" /> 
                                                <asp:AsyncPostBackTrigger ControlID="gvPerfilUsuario" EventName="PageIndexChanging" /> 
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <!-- Pie -->
                        <td><img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>

    <!-- Popup para asignacin de usuarios a perfil-->
    <script type="text/javascript">
        function fc_NuevoPU()
        {
            document.getElementById("<%=this.txtNomUsuario.ClientID %>").value = "";
            document.getElementById("<%=this.txtApePat.ClientID %>").value = "";
            document.getElementById("<%=this.txtApeMat.ClientID %>").value = "";
            document.getElementById("<%=this.cboTipo.ClientID %>").value = "";
            
            document.getElementById("<%=this.btnNuevoPerfilUsuario1.ClientID %>").click();
            
        }
        
        function fc_LimpiarAsigPU()
        {
            document.getElementById("<%=this.txtNomUsuario.ClientID %>").value = "";
            document.getElementById("<%=this.txtApePat.ClientID %>").value = "";
            document.getElementById("<%=this.txtApeMat.ClientID %>").value = "";
            document.getElementById("<%=this.cboTipo.ClientID %>").value = "";            
            return false;
        }

        function fc_AceptarAsigPU()
        {
            
            arrItemsSeleccionados = document.getElementById("<%=this.txhCadenaSelPU.ClientID %>").value.split("|");
            if (arrItemsSeleccionados.length < 3)
            {
                alert(mstrSeleccioneUno);
                return false;
            }
            
            return confirm(mstrSeguroGrabar);
        }

        function fc_CerrarAsigPU()
        {
            document.getElementById("<%=this.txhFlagChekTodosPU.ClientID %>").value = "";
            document.getElementById("<%=this.txhCadenaSelPU.ClientID %>").value = "";
            document.getElementById("<%=this.btnCerrarPerfilUsuario1.ClientID %>").click();
            return false;
        }

        function fc_BuscarAsigPU()
        {
            var mstrError = "";
            if (fc_Trim(document.getElementById("<%=this.txtNomUsuario.ClientID %>").value) != "" &&
                !fc_Trim(document.getElementById("<%=this.txtNomUsuario.ClientID %>").value).match(RE_ALAFANUMERICO))
            {
                mstrError += mstrElCampo + "nombre usuario" + mstrReAlfanumerico;
            }
            if (fc_Trim(document.getElementById("<%=this.txtApePat.ClientID %>").value) != "" &&
                !fc_Trim(document.getElementById("<%=this.txtApePat.ClientID %>").value).match(RE_ALAFANUMERICO))
            {
                mstrError += mstrElCampo + "apellido paterno" + mstrReAlfanumerico;
            }
            if (fc_Trim(document.getElementById("<%=this.txtApeMat.ClientID %>").value) != "" &&
                !fc_Trim(document.getElementById("<%=this.txtApeMat.ClientID %>").value).match(RE_ALAFANUMERICO))
            {
                mstrError += mstrElCampo + "apellido materno" + mstrReAlfanumerico;
            }
            
            if ( mstrError!="" )
            {
                alert(mstrError);
                return false;
            }
            
            return true;
        }
    </script>
    <asp:Panel ID="pnlAsigPerfilUsuario" runat="server" CssClass="modalPopup" Width="650px"
        Style="display: none; background-repeat: repeat; background-image: url(../Images/fondo.gif);
        padding-top: 0px; padding-bottom: 8px">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
            <tr>
                <td style="width: 245px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;</td>
                <td style="width: 353px; background-repeat: repeat; background-image: url(../Images/flotante/popcab2.gif);">&nbsp;</td>
                <td style="width: 52px; background-repeat: repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
            <tr style="display: none">
                <td align="right">
                    <asp:ImageButton ID="btnNuevoPerfilUsuario1" runat="server" OnClick="btnNuevoPerfilUsuario1_Click" />&nbsp;
                    <asp:ImageButton ID="btnCerrarPerfilUsuario1" runat="server" />&nbsp;
                    <asp:ImageButton ID="btnBuscarPerfilUsuario1" runat="server"  />&nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10px; padding-right: 10px">
                    <table cellpadding="0" cellspacing="0" border="0" width="630px">
                        <tr valign="bottom">
                            <td style="width: 170px">
                                <table id="tblCabeceraAsigPerfilUsuario" runat="server" width="100%" cellpadding="0"
                                    cellspacing="0" border="0" style="height: 20px">
                                    <tr>
                                        <td><img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                        <td class="TabCabeceraOn" style="width: 190px">Asignación de Usuarios</td>
                                        <td><img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 460px">
                                <table cellpadding="0" cellspacing="0" border="0" style="width:460px">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="btnBuscarPerfilUsuario" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif"
                                                OnClientClick="javascript: return fc_BuscarAsigPU();" OnClick="btnBuscarPerfilUsuario_Click" 
                                                onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" 
                                                onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" />
                                            
                                            <asp:Image ID="btnLimpiarPerfilUsuario" runat="server" ToolTip="Limpiar" ImageUrl="~/Images/iconos/b-limpiar.gif"
                                                onClick="javascript: return fc_LimpiarAsigPU();" 
                                                onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" 
                                                onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'" />
                                            
                                            <asp:ImageButton ID="btnAsignarPerfilUsuario" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/iconos/b-aceptar.gif"
                                                OnClientClick="javascript: return fc_AceptarAsigPU();" 
                                                OnClick="btnAsignarPerfilUsuario_Click" 
                                                onmouseover="javascript:this.src='../Images/iconos/b-aceptar2.gif'" 
                                                onmouseout="javascript:this.src='../Images/iconos/b-aceptar.gif'"/>
                                            
                                            <asp:ImageButton ID="btnCerrarPerfilUsuario" runat="server" ToolTip="Cerrar" ImageUrl="~/Images/iconos/b-cerrar.gif"
                                                OnClientClick="javascript: return fc_CerrarAsigPU();" 
                                                onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'" 
                                                onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="630px" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <!-- Cabecera -->
                            <td><img alt="" src="../Images/Mantenimiento/fbarr.gif" width="630" /></td>
                        </tr>
                        <tr>
                            <!-- Cuerpo -->
                            <td style="background-color: #ffffff; vertical-align: top; height: 300px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl" runat="server" SkinID="lblcb">CRITERIOS DE BÚSQUEDA</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="cbusqueda" style="width: 620px;" cellspacing="1" cellpadding="5">
                                                <tr>
                                                    <td style="width: 10%">Nombres</td>
                                                    <td style="width: 20%"><asp:TextBox ID="txtNomUsuario" runat="server" Width="95%" MaxLength="50"></asp:TextBox></td>
                                                    <td style="width: 15%">Ape. Paterno</td>
                                                    <td style="width: 20%"><asp:TextBox ID="txtApePat" runat="server" Width="95%" MaxLength="50"></asp:TextBox></td>
                                                    <td style="width: 15%">Ape. Materno</td>
                                                    <td style="width: 20%"><asp:TextBox ID="txtApeMat" runat="server" Width="95%" MaxLength="50"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Tipo</td>
                                                    <td colspan="3"><asp:DropDownList ID="cboTipo" runat="server" Width="100px"></asp:DropDownList></td>
                                                </tr>
                                            </table>
                                            <table cellpadding="0" cellspacing="0">
                                                 <tr>
                                                    <td><asp:Image id="Image9" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="620px" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="padding-top:15px;">
                                            <asp:UpdatePanel ID="upAsigPuntosVenta" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="txhNroFilasPU" runat="server" />
                                                    <asp:HiddenField ID="txhFlagChekTodosPU" runat="server" />
                                                    <asp:HiddenField ID="txhCadenaSelPU" runat="server" />
                                                    <asp:HiddenField ID="txhCadenaTotalPU" runat="server" />
                                                    <asp:GridView ID="gvAsigPerfilUsuario" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                        AllowSorting="true" SkinID="Grilla" Width="100%" 
                                                        DataKeyNames="Nid_usuario" 
                                                        OnRowDataBound="gvAsigPerfilUsuario_RowDataBound" 
                                                        OnSorting="gvAsigPerfilUsuario_Sorting"
                                                        OnPageIndexChanging="gvAsigPerfilUsuario_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelCabecera" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSel" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="VNOMUSR" HeaderText="Nombres y Apellidos" SortExpression="VNOMUSR"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="35%">
                                                                <ItemStyle Width="35%" /></asp:BoundField>
                                                            <asp:BoundField DataField="CUSR_ID" HeaderText="Login" SortExpression="CUSR_ID"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="9%">
                                                                <ItemStyle Width="9%" /></asp:BoundField>
                                                            <asp:BoundField DataField="VUSR_TIPO" HeaderText="Tipo" SortExpression="VUSR_TIPO"
                                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                                                <ItemStyle Width="8%" /></asp:BoundField>
                                                            <asp:BoundField DataField="SFE_INICIO_ACCESO" HeaderText="Fecha Inicio Acceso" SortExpression="FE_INICIO_ACCESO"
                                                                ItemStyle-HorizontalAlign="center" HeaderStyle-Width="12%" ItemStyle-Width="12%"></asp:BoundField>
                                                            <asp:BoundField DataField="HR_INICIO_ACCESO" HeaderText="Hora Inicio Acceso" SortExpression="HR_INICIO_ACCESO"
                                                                ItemStyle-HorizontalAlign="center" HeaderStyle-Width="12%" ItemStyle-Width="12%"></asp:BoundField>
                                                            <asp:BoundField DataField="SFE_FIN_ACCESO" HeaderText="Fecha Fin Acceso" SortExpression="FE_FIN_ACCESO"
                                                                ItemStyle-HorizontalAlign="center" HeaderStyle-Width="12%" ItemStyle-Width="12%"></asp:BoundField>
                                                            <asp:BoundField DataField="HR_FIN_ACCESO" HeaderText="Hora Fin Acceso" SortExpression="HR_FIN_ACCESO"
                                                                ItemStyle-HorizontalAlign="center" HeaderStyle-Width="12%" ItemStyle-Width="12%"></asp:BoundField>
                                                        </Columns>                                                                    
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="gvAsigPerfilUsuario" EventName="Sorting" />
                                                    <asp:AsyncPostBackTrigger ControlID="gvAsigPerfilUsuario" EventName="PageIndexChanging" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNuevoPerfilUsuario1" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%--asp:UpdateProgress ID="uprAsigPuntosVenta" runat="server" AssociatedUpdatePanelID="upAsigPuntosVenta">
                                                <ProgressTemplate>
                                                    <table style="width:100%; height: 300px" >
                                                        <tr>
                                                            <td align="center" valign="middle">
                                                                <img src="../Images/ajaxloader.gif" alt="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <!-- Pie -->
                            <td><img alt="" src="../Images/Mantenimiento/fba.gif" width="630" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="mpAsigPerfilUsuario" runat="server" 
        PopupControlID="pnlAsigPerfilUsuario" BackgroundCssClass="modalBackground" 
        TargetControlID="btnAgregar" CancelControlID="btnCerrarPerfilUsuario1"       
        X="200" Y="50" />
</asp:Content>
<asp:Content ContentPlaceHolderID="cphFormPost" ID="cphFormPost" runat="server">
    <script type="text/javascript">
         if ( "<%=this.strIndiceTabOn %>" != "" ) setTabCabeceraOn("<%=this.strIndiceTabOn %>");
         if ( "<%=this.idFilaGrilla %>" != "" && "<%=this.indRefreshGrilla %>" == "1" )
         {
            fc_SeleccionaFilaSimple(document.getElementById("<%=this.idFilaGrilla %>"));
         }
    </script>
</asp:Content>