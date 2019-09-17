<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ComboEmpresa.ascx.cs" Inherits="SGS_UserControl_ComboEmpresa" %>
<asp:DropDownList ID="cboEmpresa" runat="server" Width="150px" >
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvCboEmpresa" runat="server" ControlToValidate="cboEmpresa" Enabled="false"
    InitialValue="" Text="(*)" ErrorMessage="Debe seleccionar una Empresa."></asp:RequiredFieldValidator>