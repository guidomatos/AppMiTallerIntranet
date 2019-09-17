<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextBoxFecha.ascx.cs" Inherits="SGS_UserControl_TextBoxFecha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:TextBox ID="txtFecha" runat="server" Columns="11" MaxLength="10"></asp:TextBox>
<asp:Image ID="btnFecha" runat="server" ImageUrl="~/Images/iconos/calendario.gif" ImageAlign="AbsMiddle" ToolTip="Calendario"  />
<cc1:CalendarExtender ID="ceFecha" runat="server" Format="dd/MM/yyyy" DefaultDate=""
    TargetControlID="txtFecha" PopupButtonID="btnFecha" />
<cc1:MaskedEditExtender ID="meFecha" runat="server"
        MaskType="Date" Mask="99/99/9999" UserDateFormat="DayMonthYear"
        ClearMaskOnLostFocus="true" ErrorTooltipEnabled="true" MessageValidatorTip="true"
        ClearTextOnInvalid="true" TargetControlID="txtFecha" >
</cc1:MaskedEditExtender>