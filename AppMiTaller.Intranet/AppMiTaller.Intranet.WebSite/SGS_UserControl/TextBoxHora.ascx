<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextBoxHora.ascx.cs" Inherits="SGS_UserControl_TextBoxHora" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:TextBox ID="txtHora" runat="server" MaxLength="5" Width="50px"></asp:TextBox>
<cc1:MaskedEditExtender ID="meFecha" runat="server"
        MaskType="Time" Mask="99:99" UserTimeFormat="TwentyFourHour"
        ClearMaskOnLostFocus="true" ErrorTooltipEnabled="true" MessageValidatorTip="true"
        ClearTextOnInvalid="true" TargetControlID="txtHora" >
</cc1:MaskedEditExtender>