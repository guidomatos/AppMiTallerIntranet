<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" Inherits="SGS_UserControl_UploadFile" %>
<asp:HiddenField ID="txhArchivo" runat="server" />
<asp:HiddenField ID="txhTamanho" runat="server" />
<asp:HiddenField ID="txhNombre" runat="server" />
<asp:HiddenField ID="txhExtension" runat="server" />
<asp:HiddenField ID="txhAncho" runat="server" />
<asp:HiddenField ID="txhAlto" runat="server" />
<table id="tblUF" cellpadding="0" cellspacing="0" style="vertical-align: top" border="0" width="350px" runat="server">
    <tr>
        <td id="td1UF" style="width: 350px">
            <asp:FileUpload ID="fuArchivo" runat="server" CssClass="styleFileUpload" Width="350px" />
        </td>
        <td id="td2UF" style="width: 30px">
            <asp:Image ID="imgVer" ImageUrl="../Images/iconos/Buscar.jpg" runat="server" ToolTip="Ver" Visible="false" 
                onmouseover="javascript:this.src='../Images/iconos/buscar2.jpg'"
                onmouseout="javascript:this.src='../Images/iconos/buscar.jpg'" />&nbsp;
            <asp:Image ID="imgLimpiar" ImageUrl="../Images/iconos/Limpiar.jpg" runat="server" ToolTip="Limpiar" Visible="false" 
                onmouseover="javascript:this.src='../Images/iconos/limpiar2.jpg'"
                onmouseout="javascript:this.src='../Images/iconos/limpiar.jpg'" />&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblArchivo" runat="server" CssClass="Texto" Visible="false"></asp:Label>
            <asp:HiddenField ID="txhIndLimpio" runat="server" Value="0" />
        </td>
    </tr>
</table>