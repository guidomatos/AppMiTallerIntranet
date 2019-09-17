<%@ Page Language="C#" 
    MasterPageFile="~/Seguridad.master"
    AutoEventWireup="true" 
    CodeFile="SGS_Usuario_Bandeja.aspx.cs" 
    Inherits="SGS_Seguridad_SGS_Usuario_Bandeja" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboEstado.ascx" TagName="CboEstado" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
    
        function fc_Limpiar()
        {
            document.getElementById("<%=this.txtNombres.ClientID %>").value = "";
            document.getElementById("<%=this.txtApePat.ClientID %>").value = "";
            document.getElementById("<%=this.txtApeMat.ClientID %>").value = "";
            document.getElementById("<%=this.cboPerfil.ClientID %>").value = "";
            document.getElementById("<%=this.cboEstado.ClientID %>_cboEstado").value = "0";
            //Agregado por Ncamacho 08/06/2011
            document.getElementById("<%=this.txtNumDoc.ClientID %>").value = "";
            //Fin del agregado por Ncamacho 08/06/2011

            document.getElementById("<%=this.cboPuntoVenta.ClientID %>").value = "";
            document.getElementById("<%=this.txtLogin.ClientID %>").value = "";
            return false;
        }
        
        function fc_Eliminar()
        {
            if(fc_Trim(document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value)=="" || fc_Trim(document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value)=="|")
            {
                alert(mstrSeleccioneUno);
                return false;
            }
            else

            {
                return confirm(mstrSeguroEliminarUno);
            }  
        }
        function fc_Activar()
        {
            if(fc_Trim(document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value)=="" || fc_Trim(document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value)=="|")
            {
                alert(mstrSeleccioneUno);
                return false;
            }
            else
            {
                return confirm(mstrSeguroActivo);
            } 
        }
        function fc_Buscar()
        {
            var mstrError = "";
            
            if (fc_Trim(document.getElementById("<%=this.txtNombres.ClientID %>").value) != "" && 
                !fc_Trim(document.getElementById("<%=this.txtNombres.ClientID %>").value).match(RE_ALAFANUMERICO))
                mstrError += mstrElCampo + " nombres" + mstrReAlfanumerico;
                
            if (fc_Trim(document.getElementById("<%=this.txtApePat.ClientID %>").value) != "" && 
                !fc_Trim(document.getElementById("<%=this.txtApePat.ClientID %>").value).match(RE_ALAFANUMERICO))
                mstrError += mstrElCampo + " apellido paterno" + mstrReAlfanumerico;

            if (fc_Trim(document.getElementById("<%=this.txtApeMat.ClientID %>").value) != "" && 
                !fc_Trim(document.getElementById("<%=this.txtApeMat.ClientID %>").value).match(RE_ALAFANUMERICO))
                mstrError += mstrElCampo + " apellido materno" + mstrReAlfanumerico;
            
            if (fc_Trim(document.getElementById("<%=this.txtNumDoc.ClientID %>").value) != ""  && 
                !fc_Trim(document.getElementById("<%=this.txtNumDoc.ClientID %>").value).match(RE_SOLONRO))
                mstrError += mstrElCampo + " DNI del usuario" + mstrReSoloNro;
                
            if ( mstrError != "" ) 
            {
                alert(mstrError)
                return false;
            }
            return true;
        }
        function Fc_SeleccionaItemAsigNum(valor)
        {   if (document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value==''){document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value='|'}
            if (document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value.indexOf('|'+valor+'|')>-1)
            {
                var posicion1 = document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value.indexOf('|'+valor+'|');
                var posicion2 = String('|'+valor+'|').lastIndexOf('|');
                document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value = document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value.substring(0, posicion1) + document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value.substring(eval(posicion1)+eval(posicion2),document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value.length);
            }
            else
            {
                document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value = document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value + valor+'|';
            }    
        }

        function Fc_SelecDeselecTodos()
        {   
            if (document.getElementById("<%=this.txhNroFilasNum.ClientID %>").value>0)
            {
                if (document.getElementById("<%=this.txhFlagChekTodosNum.ClientID %>").value=='1')
                {   
                    document.getElementById("<%=this.txhFlagChekTodosNum.ClientID %>").value = ''
                    document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value = '';
                    for ( i=2 ; i < eval(document.getElementById("<%=this.txhNroFilasNum.ClientID %>").value)+2 ; i++ )
                    {
                        var iLen = String('0'+String(i)).length;
                        var cadena = String('0'+String(i)).substring(iLen, iLen - 2);                
                        document.getElementById("<%=this.gvUsuarios.ClientID %>"+"_ctl"+cadena+"_chkSelNum").checked=false;
                    }
                    document.getElementById("<%=this.txhTamanioNum.ClientID %>").value = "0";
                   
                }
                else
                {   
                    document.getElementById("<%=this.txhFlagChekTodosNum.ClientID %>").value = '1'
                    document.getElementById("<%=this.txhCadenaSelNum.ClientID %>").value = document.getElementById("<%=this.txhCadenaTotalNum.ClientID %>").value; 
                    for ( i=2 ; i < eval(eval(document.getElementById("<%=this.txhNroFilasNum.ClientID %>").value)+2) ; i++ )
                    {
                        var iLen = String('0'+String(i)).length;
                        var cadena = String('0'+String(i)).substring(iLen, iLen - 2);
                        var objCheck = document.getElementById("<%=this.gvUsuarios.ClientID %>"+"_ctl"+cadena+"_chkSelNum");
                        if (objCheck.disabled==false){objCheck.checked=true;}
                        
                    }
                    document.getElementById("<%=this.txhTamanioNum.ClientID %>").value = document.getElementById("<%=this.txhNroFilasNum.ClientID %>").value;
                   
                }
            }    
        }
    </script>
    <table id="tblIconos" cellpadding="0" cellspacing="0" border="0" class="TablaIconosMantenimientos" >
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnAgregar" runat="server" ToolTip="Agregar" ImageUrl="~/Images/iconos/b-nuevo.gif" OnClick="btnAgregar_Click" 
                onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"/>
                
                <asp:ImageButton ID="btnEliminar" runat="server" ToolTip="Eliminar" ImageUrl="~/Images/iconos/b-eliminar.gif" OnClientClick="javascript: return fc_Eliminar();" OnClick="btnEliminar_Click" 
                onmouseover="javascript:this.src='../Images/iconos/b-eliminar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-eliminar.gif'"/>
                <asp:ImageButton ID="btnActivar" runat="server" ToolTip="Activar" ImageUrl="~/Images/iconos/b-confirmar_aprob.png" OnClientClick="javascript: return fc_Activar();" OnClick="btnActivar_Click" 
                onmouseover="javascript:this.src='../Images/iconos/b-confirmar_aprob2.png'" onmouseout="javascript:this.src='../Images/iconos/b-confirmar_aprob.png'"/>
                <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif" OnClick="btnBuscar_Click" OnClientClick="javascript: return fc_Buscar();"
                onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"/>
                
                <asp:ImageButton ID="btnLimpiar" runat="server" ToolTip="Limpiar" ImageUrl="~/Images/iconos/b-limpiar.gif" OnClientClick="javascript: return fc_Limpiar();" 
                onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'"/>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <cc1:TabContainer ID="tabContPerfil" runat="server" ActiveTabIndex="0" CssClass="" >
        <cc1:TabPanel ID="tabPerfil" runat="server" CssClass="" >
            <HeaderTemplate>
                <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td><img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif"/></td>
                        <td class="TabCabeceraOff" >Usuarios</td>
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
                        <td style="background-color:#ffffff;vertical-align: top; height:450px">
                            <table cellpadding="1" cellspacing="1" style="margin-left:5px; margin-right:5px;">
                                <tr><td><asp:Label ID="lbl" runat="server" SkinID="lblcb" >CRITERIOS DE BÚSQUEDA</asp:Label></td></tr>
                                <tr>
                                    <td>
                                        <table class="cbusqueda" style="width: 785px; height:30px" cellspacing="1" cellpadding="2" border="0">
                                            <tr>
                                                <td style="width:10%">Nombres</td>
                                                <td style="width:30%"><asp:TextBox ID="txtNombres" runat="server" Width="140px" AutoPostBack="false" ></asp:TextBox></td>
                                                <td style="width:10%">Ape. Paterno</td>
                                                <td style="width:20%"><asp:TextBox ID="txtApePat" runat="server" Width="140px" AutoPostBack="false" ></asp:TextBox></td>
                                                <td style="width:10%">Ape. Materno</td>
                                                <td style="width:20%"><asp:TextBox ID="txtApeMat" runat="server" Width="140px" AutoPostBack="false" ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Perfil</td>
                                                <td><asp:DropDownList ID="cboPerfil" runat="server" width="220px" ></asp:DropDownList></td>
                                                <td>Estado</td>
                                                <td><uc1:CboEstado ID="cboEstado" runat="server" Width="143px"/></td>
                                                <%--Agregado por Ncamacho 08/06/2011--%>
                                                <td>Nro Documento</td>
                                                <td><asp:TextBox ID="txtNumDoc" runat="server" Width="140px"  MaxLength="8"  AutoPostBack="false" ></asp:TextBox></td>
                                                <%--Fin del agregado por Ncamacho 08/06/2011--%>
                                            </tr>
                                            <tr>
                                                <td>Punto Venta</td>
                                                <td><asp:DropDownList ID="cboPuntoVenta" runat="server" Width="220px"></asp:DropDownList></td>
                                                <td>Login</td>
                                                <td><asp:TextBox ID="txtLogin" runat="server" Width="150px" MaxLength="20"></asp:TextBox></td>
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
                                        <div style="overflow:auto; width:100%; height:350px;">
                                        <asp:UpdatePanel ID="upUsuario" runat="server" UpdateMode="Conditional">
                                            <contenttemplate>
                                                <asp:HiddenField ID="txhUsuarioID" runat="server" />
                                                <%-- Agregado por Ncamacho 09/06/2011--%>
                                               <asp:HiddenField ID="txhCadenaSelNum" runat="server" />
                                                <asp:HiddenField ID="txhCadenaTotalNum" runat="server" />
                                                <asp:HiddenField ID="txhFlagChekTodosNum" runat="server" />                            
                                                <asp:HiddenField ID="txhNroFilasNum" runat="server" />                            
                                                <asp:HiddenField ID="txhTamanioNum" runat="server" />
                                                
                                                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false"
                                                    AllowPaging="true" AllowSorting="true" SkinID="Grilla" Width="98%"
                                                    DataKeyNames="NID_USUARIO, fl_inactivo" 
                                                    OnRowDataBound="gvUsuarios_RowDataBound"
                                                    OnSorting="gvUsuarios_Sorting"
                                                    OnPageIndexChanging="gvUsuarios_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Fabricante" >
                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            <HeaderStyle Width="2%" />
                                                            <HeaderTemplate>
                                                                <asp:CheckBox  ID="chkSelCabeceraNum" runat="server"/>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox  ID="chkSelNum" runat="server"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="VNOMUSR" HeaderText="Nombres y Apellidos" SortExpression="VNOMUSR"
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="24%" >
                                                            <ItemStyle Width="24%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CUSR_ID" HeaderText="Login" SortExpression="CUSR_ID" 
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VUSR_TIPO" HeaderText="Tipo" SortExpression="VUSR_TIPO" 
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="VUSR_PERFIL" HeaderText="Perfil" SortExpression="VUSR_PERFIL" 
                                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="21%">
                                                            <ItemStyle Width="21%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NU_TIPO_DOCUMENTO" HeaderText="Número Documento" SortExpression="NU_TIPO_DOCUMENTO" 
                                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%">
                                                            <ItemStyle Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SFL_INACTIVO" HeaderText="Estado" SortExpression="SFL_INACTIVO" 
                                                            ItemStyle-HorizontalAlign="left" HeaderStyle-Width="6%">
                                                            <ItemStyle Width="6%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvUsuarios" EventName="Sorting" /> 
                                                <asp:AsyncPostBackTrigger ControlID="gvUsuarios" EventName="PageIndexChanging" /> 
                                            </triggers>
                                        </asp:UpdatePanel>
                                        </div>
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