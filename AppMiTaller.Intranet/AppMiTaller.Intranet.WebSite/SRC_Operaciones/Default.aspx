<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SRC_Operaciones_Default" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:TextBox ID="txt" runat="server" onkeypress="return fn_SoloLetras(event)"></asp:TextBox>
<asp:Button ID="btnOpen" runat="server" Text="Abrir Popup con Estilo" />    
<asp:Panel ID="Panel" runat="server" CssClass="PanelPopup_g" Style="position:absolute; display: none; width:620px;">    
    <div style="width:620px; height:11px; cursor: move;" name="imgDragDrop">&nbsp;</div>
    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">        
        <tr>
            <td class="TitleL">&nbsp;</td>
            <td class="TitleC" style="width: 325px;">&nbsp;</td>
            <td class="TitleR" >&nbsp;</td>
        </tr>
    </table>
    <table class="Cuerpo" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr valign="bottom">
            <td style="width: 400px"><!--SOLO CUANDO SEA NECESARIO-->
                <table id="Table2" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0" style="height: 20px">
                    <tr id="Tr3" runat="server">
                        <td id="Td7" runat="server"><img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                        <td id="Td8" class="TabCabeceraOn" style="width: 420px" runat="server">Confirmar Pedido</td>
                        <td id="Td9" runat="server"><img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                    </tr>
                </table>
            </td>
            <td align="right">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar"/>
                            <asp:ImageButton ID="btnClose" runat="server" ToolTip="Cerrar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table class="Cuerpo" width="600px" cellpadding="0" cellspacing="0">
        <tr><td><img alt="" src="../Images/Mantenimiento/fbarr.gif" width="600px" /></td></tr>
        <tr>
            <!-- Cuerpo -->
            <td style="background-color: #ffffff; vertical-align: top; width: 600px;">
                <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="padding-left: 5px; padding-right: 5px;">                            
                            <table border="0" class="cbusqueda" cellpadding="2" style="width:100%" cellspacing="1">
                                <tr>
                                    <td style="width:80px;">Sucursal</td>
                                    <td>
                                        <select style="width:75%">
                                            <option>[Seleccione]</option>
                                            <option>SURCO</option>
                                            <option>LA MARINA</option>
                                            <option>SURQUILLO</option>
                                        </select>                            
                                    </td>                                    
                                </tr>    
                                <tr>
                                    <td>Caja</td>
                                    <td>
                                        <select style="width:75%">
                                            <option>[Seleccione]</option>
                                            <option>002 - EMISOR LA MARINA</option>
                                            <option>01 - CAJA LA MARINA</option>
                                            <option>003 - EMISOR LOS FRUTALES</option>
                                            <option>004 - EMISOR SURCO</option>
                                        </select>
                                    </td>                                
                                </tr>                            
                                <tr> 
                                    <td>Cajero:</td>
                                    <td>
                                        <select style="width:75%">
                                            <option>[Seleccione]</option>
                                            <option>COMERCIAL VEHICULOS</option>
                                            <option>COMERCIAL TALLER</option>
                                            <option>CAJA SAN MIGUEL VEHICULOS</option>
                                            <option>CAJA SAN MIGUEL TALLER</option>
                                        </select>
                                    </td>                                    
                                </tr>
                            </table>                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td><img alt="" src="../Images/Mantenimiento/fba.gif" width="600px" /></td></tr>
    </table>      
</asp:Panel>
<cc1:ModalPopupExtender ID="mpeAdd" runat="server" PopupControlID="Panel"
    BackgroundCssClass="modalBackground" TargetControlID="btnOpen"
    CancelControlID="btnClose" >     
</cc1:ModalPopupExtender>

<script language="javascript">
function fn_SoloLetras(eventObj)
    {        
        var key;
        if(eventObj.keyCode)           // For IE
            key = eventObj.keyCode;
        else if(eventObj.Which)
            key = eventObj.Which;       // For FireFox
        else
            key = eventObj.charCode;    // Other Browser                  
        
        if (key >= 65 &&  key <= 90){}
        else if (key >= 97 &&  key <= 122){}        
        else if (key ==241){}        
        else if (key ==209 ){}        
        else{            
            return false;  // anula la entrada de texto. 
        }        
    }
</script>
</asp:Content>