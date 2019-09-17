<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_Maestro_Detalle_Servicio.aspx.cs"
    Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_Servicio"
    Title="Detalle Servicio"
    EnableEventValidation="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">   
        function IsNum(numstr) {
            // Return immediately if an invalid value was passed in
            if (numstr + "" == "undefined" || numstr + "" == "null" || numstr + "" == "")
                return false;
            var isValid = true;
            var decCount = 3;		// number of decimal points in the string
            // convert to a string for performing string comparisons.
            numstr += "";
            // Loop through string and test each character. If any
            // character is not a number, return a false result.
            // Include special cases for negative numbers (first char == '-')
            // and a single decimal point (any one char in string == '.').   
            for (i = 0; i < numstr.length; i++) {
                // track number of decimal points
                if (numstr.charAt(i) == ".")
                    decCount++;
                if (!((numstr.charAt(i) >= "0") && (numstr.charAt(i) <= "9") ||
                    (numstr.charAt(i) == "-") || (numstr.charAt(i) == "."))) {
                    isValid = false;
                    break;
                } else if ((numstr.charAt(i) == "-" && i != 0) ||
                    (numstr.charAt(i) == "." && numstr.length == 1) ||
                    (numstr.charAt(i) == "." && decCount > 1)) {
                    isValid = false;
                    break;
                }
                //if (!((numstr.charAt(i) >= "0") && (numstr.charAt(i) <= "9")) || 
            } // END for   

            return isValid;
        }  // end IsNum
        function IsNumeric(sText) {
            var ValidChars = "0123456789";
            var IsNumber = true;
            var Char;


            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    IsNumber = false;
                }
            }
            return IsNumber;

        }


        function Validar_Datos() {

            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var txtNom = document.getElementById('<%=txtNom.ClientID%>');
            var txtTprom = document.getElementById('<%=txtTprom.ClientID%>');


            if (txtCodigo.value.trim() == "") {
                alert('Ingrese Codigo de Servicio.');
                txtCodigo.focus();
                return false;
            }
            if (txtNom.value.trim() == "") {
                alert('Ingrese Nombre de Servicio.');
                txtNom.focus();
                return false;
            }

            if (txtTprom.value.trim() == "") {
                alert('Ingrese Tiempo Promedio.');
                txtTprom.focus();
                return false;
            }

            if (parseInt(txtTprom.value.trim()) <= 0) {
                alert('Tiempo Promedio Debe Ser Mayor a Cero');
                txtTprom.focus();
                return false;
            }
            else {

                if (IsNumeric(txtTprom.value.trim()) == false) //IsNum(txtTprom.value.trim())==false)
                {
                    alert('Tiempo Promedio es numerico.');//(maximo dos decimales)');
                    txtTprom.focus();
                    return false;
                }
            }
            return true;
        }

        function Valida_Nombre(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_nombres = document.getElementById('<%=txtNom.ClientID%>');
            if (txt_nombres.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key >= 48 && key <= 57) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false;//anula la entrada de texto. 
            }
        }
    </script>

    <table cellpadding="2" cellspacing="0" width="1000" border="0">
        <tr>
            <td>
                <!--INICIO ICONOS DE ACCCION -->
                <table border="0" class="TablaIconosMantenimientos">
                    <tr>
                        <td style="width: 380px" align="right">
                            <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/iconos/b-registrofecha.gif"
                                OnClick="btnEditar_Click" onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'"
                                onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'" ToolTip="Editar"
                                Visible="False" />
                            <asp:ImageButton ID="btnGrabar" runat="server" ToolTip="Grabar" ImageUrl="~/Images/iconos/b-guardar.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"
                                OnClick="btnGrabar_Click" OnClientClick="javascript:return Validar_Datos();" />
                            <asp:ImageButton ID="btnRegresar" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'"
                                OnClick="btnRegresar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <!--FIN ICONOS DE ACCCION -->
        </tr>
        <tr>
            <td valign="top">
                <cc1:TabContainer ID="TabContDetServicio" runat="server" CssClass="" ActiveTabIndex="0">
                    <cc1:TabPanel runat="server" ID="TabDetServicio">
                        <HeaderTemplate>
                            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                    <td class="TabCabeceraOffForm" onmouseover="javascript: return onTabCabeceraOverForm('0');" onmouseout="javascript: return onTabCabeceraOutForm('0');">Detalle de Servicio</td>
                                    <!-- AGREGAR TITULO DEL TAB-->
                                    <td>
                                        <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                </tr>
                            </table>

                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="DivCuerpoTab">
                                <table cellspacing="0" cellpadding="0" width="980" border="0">
                                    <tbody>
                                        <tr>
                                            <!-- Cabecera -->
                                            <td>
                                                <img alt="" src="../Images/Tabs/borarriba.gif" width="990" /></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; height: 450px; background-color: #ffffff" valign="top">
                                                <table style="margin-left: 5px; margin-right: 5px;" cellspacing="1" cellpadding="1" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td class="lineadatos">
                                                                <asp:Label ID="lbl" runat="server" SkinID="lblcb">SERVICIO</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>

                                                                <asp:UpdatePanel ID="UpdDetServ" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel ID="p_items" runat="server">
                                                                            <table style="width: 965px; margin-left: 5px; margin-right: 5px;" cellspacing="1" cellpadding="2" border="0" class="textotab">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 12%">Codigo</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" SkinID="txtob"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Nombre de Servicio</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtNom" runat="server" MaxLength="40" Width="432px" SkinID="txtob"></asp:TextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Tipo de Servicio</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="cboTServicio" runat="server" Width="241px" SkinID="cboob">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Tiempo Promedio </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtTprom" runat="server" MaxLength="5" SkinID="txtob"></asp:TextBox>&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Quick Service</td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkquickservice" runat="server" Checked="True" Text=" " /></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Estado</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="cboEstado" runat="server" Width="120px" SkinID="cboob">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">Dias Válidos</td>
                                                                                        <td>
                                                                                            <asp:CheckBoxList ID="chkDias" runat="server" BorderColor="Gray" BorderStyle="Solid"
                                                                                                BorderWidth="1px" RepeatDirection="Horizontal" Width="470px">
                                                                                                <asp:ListItem Selected="True" Value="1">LUN</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="2">MAR</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="3">MIER</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="4">JUEV</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="5">VIER</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="6">SAB</asp:ListItem>
                                                                                                <asp:ListItem Selected="True" Value="7">DOM</asp:ListItem>
                                                                                            </asp:CheckBoxList></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="lineadatos">&nbsp;</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <!-- Cabecera -->
                                            <td>
                                                <img alt="" src="../Images/Tabs/borabajo.gif" width="990" /></td>
                                        </tr>
                                    </tbody>

                                </table>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>


    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>