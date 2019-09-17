<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ComboLinea.ascx.cs" Inherits="SGS_UserControl_ComboLinea" %>
<asp:DropDownList ID="cboLinea" runat="server" Width="150px" OnSelectedIndexChanged="cboLinea_SelectedIndexChanged">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvCboLinea" runat="server" ControlToValidate="cboLinea" Enabled="false" SetFocusOnError="true"
    InitialValue="" Text="(*)" ErrorMessage="Debe seleccionar una Linea Importación."></asp:RequiredFieldValidator>
