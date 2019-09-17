<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaginaError.aspx.cs" Inherits="PaginaError" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sistema de Reserva de Citas</title>

    <script type="text/javascript" language="javascript" src="js/fc_FuncComunes.js"></script>
    <script type="text/javascript" language="javascript" src="js/Mensajes.js"></script>
    <script type="text/javascript" language="javascript" src="js/fc_FuncDisenho.js"></script>
    <script type="text/javascript" language="javascript" src="js/fc_date.js"></script>
    <script type="text/javascript" language="javascript" src="js/fc_Validacion.js"></script>
    <script type="text/javascript">
        function fc_CerrarSesion()
        {
            return confirm("Esta seguro de cerrar sesión?");
        }        
    </script>
</head>
<body style="margin-top: 0px; margin-left: 0px; margin-right: 0px; margin-bottom: 0px;
    background-repeat: repeat-x; background-image: url(Images/fondo.gif);" scroll="no">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" 
            style="width:1004px; height: 155;margin-top: 10px; margin-left: 10px; margin-right:10px">
            <tr>
                <td style="width:100%">
                    <table cellpadding="0" cellspacing="0" border="0" style="width:100%;margin-left:0px;">
                        <tr>
                            <td align="left" style="width: 100%"><img src="Images/cabecera/cabecera.gif" width="999px" alt="" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:999px; margin-left:0px; background-color:Transparent" cellpadding="1" cellspacing="0" border="0">
                        <tr>
                            <td style="width: 9%"><asp:Label ID="lblBienvenida" runat="server" SkinID="lblLogeo" Visible="false"></asp:Label></td>
                            <td style="width: 20%" class="textousuario"><asp:Label ID="lblNombreUsuario" runat="server" SkinID="lblLogeo" Font-Bold="false" Visible="false"></asp:Label></td>
                            <td align="right" style="padding-right: 13px"><asp:ImageButton ID="btnCerrarSesion" ImageUrl="~/Images/iconos/cerrar.gif" ToolTip="Cerrar sesión"
                                    runat="server" OnClientClick="javascript: return fc_CerrarSesion();" OnClick="btnCerrarSesion_OnClick" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 994px; height:25px;" cellpadding="0" cellspacing="0" border="0">
                        <tr valign="top">
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="overflow:auto; width: 1016px; height: 500px; margin-left:10px; color:White; font-family:Arial; font-size:medium;">
            &nbsp; Se ha producido un error interno en el sistema:
            <br />
            <asp:Label ForeColor="white" ID="MensajeError" runat="server" Font-Bold="true"></asp:Label>
            <br />&nbsp;Favor de capturar la pantalla en un documento Word y enviar la pantalla al administrador del sistema (sgaAdmin@agildemeister.com.pe). Para continuar hacer clic en el botón CONTINUAR.
            <asp:ImageButton ID="btnContinuar" runat="server" ToolTip="Continuar"
                    OnClick="btnContinuar_Click" ImageUrl="~/Images/botones/Continuar.gif"/>
        </div>
        <table style="width: 100%; margin-bottom: 0px; margin-left: 0px;margin-right: 0px; background-color:Black" 
                border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="txtopiepag" style="height: 25px;">©2019 Todos los derechos reservados | Sistema de Reserva de Citas v1.0</td>
            </tr>
        </table>
    </form>
</body>
</html>
