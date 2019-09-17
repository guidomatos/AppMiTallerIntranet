<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" CodeFile="SRC_Maestro_Detalle_Cliente.aspx.cs" Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_Cliente" EnableEventValidation="true" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboPaisTelefono.ascx" TagName="ComboPaisTelefono" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/SRC_Validacion.js"></script>
    <style type="text/css">
        .ctxt {
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }

        .cdll {
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }

        .cboob {
            border: 1px solid rgb(149, 166, 198);
            width: 120px;
            color: rgb(85, 91, 108);
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
            background-color: rgb(218, 223, 233);
        }

        .lblCB {
            color: rgb(59, 131, 203);
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
        }

        .txtob {
            border: 1px solid rgb(149, 166, 198);
            width: 95px;
            color: rgb(85, 91, 108);
            text-transform: uppercase;
            font-family: Verdana;
            font-size: 10px;
            background-color: rgb(223, 228, 236);
        }

        .ctxta {
            border: 1px solid rgb(149, 166, 198);
            width: 670px;
            height: 30px;
            color: rgb(85, 91, 108);
            font-family: Verdana;
            font-size: 10px;
            resize: none;
        }

        .fondoPopup {
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            position: fixed;
            display: none;
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.50;
            z-index: 9999999;
            text-align: center;
            display: none;
        }

        .cls_combo_pais {
            font-family: Verdana;
            font-size: 10px;
            color: #555B6C;
            width: 65px;
            border-style: solid;
            border-width: 1px;
            border-color: #95A6C6;
        }

        .cls_caja_telefono {
            width: 60px;
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }

        .cls_caja_anexo {
            width: 40px;
            color: #555B6C;
            border-color: #95A6C6;
            border-width: 1px;
            border-style: Solid;
            font-family: Verdana;
            font-size: 10px;
        }
    </style>

    <script type="text/javascript">

        var ModalProgress = '<%= ModalProgress.ClientID %>';
        var combo_PaisTelefono = "";
        var combo_PaisCelular = "";
        //Código JavaScript incluido en un archivo denominado jsUpdateProgress.js 
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args) {
            saveCombos();
            // muestra el popup 
            $find(ModalProgress).show();
        }
        function endReq(sender, args) {
            //  esconde el popup
            $find(ModalProgress).hide();
            CargarInicial();
            recoverCombos();
        }

        function saveCombos() {
            combo_PaisTelefono = document.getElementById("cboPaisTelefonoFijo");
            combo_PaisCelular = document.getElementById("cboPaisTelefonoCel");
        }
        function recoverCombos() {
            if (document.getElementById("cboPaisTelefono") != null) {
                document.getElementById("cboPaisTelefono").id = "cboPaisTelefonoFijo";
                document.getElementById("cboPaisTelefono").id = "cboPaisTelefonoCel";
                for (var i = 0; i < combo_PaisTelefono.length; i++) {
                    $("#cboPaisTelefonoFijo").append("<option value='" + combo_PaisTelefono.options[i].value + "'>" + combo_PaisTelefono.options[i].text + "</option>");
                }
                for (var i = 0; i < combo_PaisCelular.length; i++) {
                    $("#cboPaisTelefonoCel").append("<option value='" + combo_PaisCelular.options[i].value + "'>" + combo_PaisCelular.options[i].text + "</option>");
                }
                setValuesCombo();
            }

        }
        function loadComboPaises() {
            fc_UserControlComboPaisTelefono("S", "0", false, 'cboPaisTelefonoFijo');
            fc_UserControlComboPaisTelefono("S", "0", false, 'cboPaisTelefonoCel');
            setValuesCombo();
        }
        function setValuesCombo() {
            $('#cboPaisTelefonoFijo').val(document.getElementById('<%=this.hfPaisTelefonoFijo.ClientID %>').value);
            $('#cboPaisTelefonoCel').val(document.getElementById('<%=this.hfPaisTelefonoCelular.ClientID %>').value);
            $("#cboPaisTelefonoFijo").removeClass("form-control").addClass("cls_combo_pais");
            $("#cboPaisTelefonoCel").removeClass("form-control").addClass("cls_combo_pais");
            $('#cboPaisTelefonoFijo').on('change', function () {
                document.getElementById("<%=this.hfPaisTelefonoFijo.ClientID %>").value = $("#cboPaisTelefonoFijo").val();
            });

            $('#cboPaisTelefonoCel').on('change', function () {
                document.getElementById("<%=this.hfPaisTelefonoCelular.ClientID %>").value = $("#cboPaisTelefonoCel").val();
            });
        }

        function fn_SoloLetras(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser                  


            if (key >= 65 && key <= 90) { }
            else if (key == 8 || key == 9 || key == 46 || key == 37 || key == 39 || key == 164 || key == 165 || key == 164 || key == 165) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 209 || key == 241) { } // ñ Ñ      
            else {
                return false;  // anula la entrada de texto. 
            }
        }

        function SoloNumeros(eventObj) {

            var key;
            if (eventObj.keyCode)            // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser              

            if (key >= 48 && key <= 57) { }
            else if (key == 8 || key == 9 || key == 46 || key == 37 || key == 39) { }
            else {
                return false;
            }
        }

        function SoloLetrasNumeros(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser  

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key >= 48 && key <= 57) { }
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú
            else if (key == 225 || key == 223 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 8 || key == 9 || key == 46 || key == 37 || key == 39) { }
            else if (key == 209 || key == 241) { } // ñ Ñ
            else {
                return false;
            }
        }

        function Valida_Grabar() {

            var mstrError = "";

            if (document.getElementById("<%=this.ddl_clie_tipopersona.ClientID %>").selectedIndex == 0)
                mstrError += mstrDebeSeleccionar + "el tipo persona" + ". \n";
            else if (document.getElementById("<%=this.ddl_grtipodocumento.ClientID %>").selectedIndex == 0)
                mstrError += mstrDebeSeleccionar + "el tipo de documento" + ". \n";
            else {

                var d = document.getElementById("<%=this.ddl_grtipodocumento.ClientID %>");
                var doc = d.options[d.selectedIndex].value;

                var e = document.getElementById("<%=this.ddl_clie_tipopersona.ClientID %>");
                var tipo = e.options[e.selectedIndex].value;

                if (fc_Trim(document.getElementById("<%=this.txt_grnrodoc.ClientID %>").value) == "")
                    mstrError += mstrDebeIngresar + "el número de documento. \n";
                else {

                    //01	DNI
                    //02	Pasaporte
                    //03	RUC
                    //04	CE
                    //05	CI

                    if ('<%=ConfigurationManager.AppSettings["CodPais"].ToString() %>' == '2') {
                        if (doc == '01') {
                            if (fc_Trim(document.getElementById("<%=this.txt_grnrodoc.ClientID %>").value).length < 7)
                                mstrError += mstrDebeIngresar + "un RUT válido. \n";
                        }
                    }
                    else {
                        if (doc == '01') {
                            if (fc_Trim(document.getElementById("<%=this.txt_grnrodoc.ClientID %>").value).length != 8)
                                mstrError += mstrDebeIngresar + "un DNI válido. \n";
                        }
                    }
                    if (doc == '03') {
                        if (fc_Trim(document.getElementById("<%=this.txt_grnrodoc.ClientID %>").value).length != 11)
                            mstrError += mstrDebeIngresar + "un RUC válido. \n";
                    }
                }
                if (tipo == '0002') //JURIDICA
                {
                    if (fc_Trim(document.getElementById("<%=this.txt_grnombres.ClientID %>").value) == "")
                        mstrError += mstrDebeIngresar + "la Razón Social del cliente. \n";
                }
                else {
                    if (fc_Trim(document.getElementById("<%=this.txt_grnombres.ClientID %>").value) == "")
                        mstrError += mstrDebeIngresar + "el Nombre del cliente. \n";
                    if (fc_Trim(document.getElementById("<%=this.txt_grpaterno.ClientID %>").value) == "")
                        mstrError += mstrDebeIngresar + "el apellido paterno del cliente. \n";
                }

            }

            if (fc_Trim(document.getElementById("<%=this.txt_gremail.ClientID %>").value) == "")
                mstrError += mstrDebeIngresar + "el correo del cliente.\n";
            else if (!fc_Trim(document.getElementById("<%=this.txt_gremail.ClientID %>").value).match(RE_EMAIL))
                mstrError += "- El correo del cliente no es correcto.\n";

            if (document.getElementById("<%=this.ddl_estado.ClientID %>").selectedIndex == 0)
                mstrError += mstrDebeSeleccionar + "el estado del cliente" + ". \n";

            var pais_celular = document.getElementById("cboPaisTelefonoCel").value;
            var telf_movil1 = document.getElementById('<%=txt_grtelmovil.ClientID%>').value;
            var pais_fijo = document.getElementById("cboPaisTelefonoFijo").value;
            var telf_fijo = document.getElementById('<%=txt_grtelfijo.ClientID%>').value;
            if (pais_celular == "" && telf_movil1 != "") { mstrError += "Si ingresa un número celular debe indicar el pais. \n"; }
            else if (valida_celular(document.getElementById('<%=txt_grtelmovil.ClientID%>'), (pais_celular == "162")) != true) { mstrError += SRC_Telefono_Movil_Invalido + " \n"; } 
            if (pais_fijo == "" && telf_fijo != "") { mstrError += "Si ingresa un número de teléfono debe indicar el pais. \n"; }
            else if (valida_telefono(document.getElementById('<%=txt_grtelfijo.ClientID%>')) != true) { mstrError += SRC_Telefono_Fijo_Invalido + " \n"; } 
            if (telf_fijo == "" && telf_movil1 == "") { mstrError += "Debe ingresar un teléfono fijo o un teléfono celular. \n"; }

            if (valida_email_blacklist(document.getElementById("<%=this.txt_gremail.ClientID %>")) != true)
                mstrError += SRC_Email_Invalido;

            if (mstrError.length != 0) {
                alert(mstrError);
                return false;
            }

        }
        
    </script>
    <asp:UpdatePanel ID="upd_GENERAL" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="2" width="1000" border="0">
                <tbody>
                    <tr>
                        <td>
                            <table id="tblIconos" class="TablaIconosMantenimientos" cellspacing="0" cellpadding="0"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnGrabar" onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClientClick="return Valida_Grabar();"
                                                runat="server" ImageUrl="~/Images/iconos/b-guardar.gif" meta:resourcekey="btnGrabarResource1"
                                                ToolTip="Grabar" OnClick="btnGrabar_Click"></asp:ImageButton>
                                            <asp:ImageButton ID="btnRegresar" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresar_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-regresar.gif" meta:resourcekey="btnRegresarResource1"
                                                ToolTip="Regresar"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <cc1:TabContainer ID="tabMantMaesVehiculo" runat="server" CssClass="" ActiveTabIndex="0">
                                <cc1:TabPanel runat="server" ID="tabVehiculo">
                                    <HeaderTemplate>
                                        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td class="TabCabeceraOnForm">DETALLE DE CLIENTE</td>
                                                <td>
                                                    <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table cellspacing="0" cellpadding="0" width="980" border="0">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                        <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                            cellpadding="1" width="970" border="0">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="height: 19px" class="lineadatos">
                                                                        <asp:Label ID="Label2" runat="server" SkinID="lblcb" meta:resourceKey="Label2Resource1"
                                                                            Text="DATOS"></asp:Label>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="UpdDV" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:Panel ID="p_DV" runat="server" Width="125px">
                                                                                    <table style="width: 966px;" cellspacing="1" cellpadding="1" border="0">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table cellspacing="1" cellpadding="2" border="0" class="textotab" width="100%">
                                                                                                        <tbody>
                                                                                                            <tr>
                                                                                                                <td style="width: 13%">Tipo de Persona
                                                                                                                </td>
                                                                                                                <td style="width: 22%">
                                                                                                                    <asp:DropDownList ID="ddl_clie_tipopersona" runat="server" AutoPostBack="True" Width="150px"
                                                                                                                        OnSelectedIndexChanged="ddl_clie_tipopersona_SelectedIndexChanged">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td style="width: 12%">Tipo de Documento
                                                                                                                </td>
                                                                                                                <td style="width: 22%">
                                                                                                                    <asp:DropDownList ID="ddl_grtipodocumento" runat="server" Width="100px" AutoPostBack="True"
                                                                                                                        OnSelectedIndexChanged="ddl_grtipodocumento_SelectedIndexChanged">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td style="width: 11%">Nro. Documento
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_grnrodoc" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label1" runat="server" Text="Nombre ó Razon Social"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_grnombres" runat="server" MaxLength="50" Width="190px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label3" runat="server" Text="Apellido Paterno"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_grpaterno" runat="server" MaxLength="50" Width="180px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label5" runat="server" Text="Apellido Materno"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_grmaterno" runat="server" MaxLength="50" Width="180px"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Teléfono Fijo
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <!--@002 I -->
                                                                                                                    <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoFijo" />
                                                                                                                    <asp:HiddenField ID="hfPaisTelefonoFijo" runat="server" Value="162" />
                                                                                                                    <asp:TextBox ID="txt_grtelfijo" runat="server" MaxLength="9" onkeypress="return SoloNumeros(event);" Width="90px"></asp:TextBox>
                                                                                                                    <input type="text" runat="server" id="txtTelefonoFijo_Anexo" class="cls_caja_anexo" maxlength="10" placeholder="Anexo" />
                                                                                                                    <!--@002 F -->
                                                                                                                </td>
                                                                                                                <td>Teléfono Oficina
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_grtelfijo2" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Teléfono Móvil 1
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <!--@002 I -->
                                                                                                                    <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoCel" />
                                                                                                                    <asp:HiddenField ID="hfPaisTelefonoCelular" runat="server" Value="162" />
                                                                                                                    <asp:TextBox ID="txt_grtelmovil" runat="server" MaxLength="9" onkeypress="return SoloNumeros(event);" Width="90px"></asp:TextBox>
                                                                                                                    <!--@002 F -->
                                                                                                                </td>
                                                                                                                <td>Teléfono Móvil 2
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_grtelmovil2" runat="server" MaxLength="20" Width="150px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Email Personal
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_gremail" runat="server" MaxLength="255" Width="180px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>Email Trabajo
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_gremail_trab" runat="server" MaxLength="255" Width="180px"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>Email Alternativo
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txt_gremail_alter" runat="server" MaxLength="255" Width="180px"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>Estado
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddl_estado" runat="server" Width="110px">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <%--@001 I--%>
                                                                                                            <tr id="trPersona_Mensaje" runat="server" visible="false">
                                                                                                                <td style="text-align: left; font-weight: bold; color: red" colspan="6">Nota: Algunos datos de la persona/empresa no pueden modificarse debido a que su identidad ha sido verificada con una entidad reguladora.</td>
                                                                                                            </tr>
                                                                                                            <%--@001 F--%>
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <asp:HiddenField ID="hid_indnuevo" runat="server"></asp:HiddenField>
                                                                                <asp:HiddenField ID="hid_indgrabar" runat="server" />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar"></asp:AsyncPostBackTrigger>
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 19px" class="lineadatos">
                                                                        <table border="0" cellpadding="2" style="width: 100%">
                                                                            <tr>
                                                                                <td><span class="lblCB">DATOS DE DIRECCIÓN TALLER</span></td>
                                                                                <td valign="middle" align="right">
                                                                                    <img alt="" id="btnModificarD" runat="server" src="../Images/iconos/agregar_1.gif" title="Modificar"
                                                                                        border="0" style="cursor: pointer;"
                                                                                        onclick="javascript:fc_OpenPopup();" />
                                                                                    <input id="hdIDCliente" type="hidden" runat="server" />


                                                                                    <input id="hdIDClienteDir" type="hidden" runat="server" />



                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 966px;" cellspacing="1" cellpadding="2" border="0" class="textotab">
                                                                            <tr>
                                                                                <td style="width: 13%">Dirección
                                                                                </td>
                                                                                <td style="width: 42%">
                                                                                    <input id="txtDireccion" style="width: 350px;" maxlength="50" class="ctxt" type="text"
                                                                                        readonly="readonly" />
                                                                                </td>
                                                                                <td style="width: 10%">Dep/Prov/Dist
                                                                                </td>
                                                                                <td style="width: 35%">
                                                                                    <input id="txtDireccionUbigeo" style="width: 280px;" maxlength="255" class="ctxt"
                                                                                        type="text" readonly="readonly" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td>Teléfono Fijo
                                                                                </td>
                                                                                <td>
                                                                                    <input id="txtDireccionTelefono" style="width: 120px;" maxlength="20" class="ctxt"
                                                                                        type="text" readonly="readonly" />
                                                                                </td>
                                                                                <td>Teléfono Fax
                                                                                </td>
                                                                                <td>
                                                                                    <input id="txtDireccionFax" style="width: 120px;" maxlength="20" class="ctxt" type="text"
                                                                                        readonly="readonly" />
                                                                                </td>
                                                                                <td>&nbsp;
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr style="display: none">
                                                                                <td>Email
                                                                                </td>
                                                                                <td colspan="4">
                                                                                    <input id="txtDireccionCorreo" style="width: 350px;" maxlength="255" class="ctxt"
                                                                                        type="text" readonly="readonly" />
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; text-align: center; vertical-align: middle;" id="DIV_PB">
                    <center>
                        <table id="TBL_WAIT" border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px">
                            <tr>
                                <td style="width: 50px">
                                    <asp:Image ID="Image56" runat="server" ImageUrl="~/Images/SRC/Espera.gif"></asp:Image>
                                </td>
                                <td style="font-size: 12px; color: dimgray; font-style: normal; font-family: verdana; text-align: center; font-variant: normal">Procesando...
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <!-- modal popup MSGBOX  -->
    <cc1:ModalPopupExtender ID="popup_msgbox_confirm" DropShadow="True" BackgroundCssClass="modalBackground"
        TargetControlID="hid_popupmsboxconfirm" PopupControlID="upd_pn_msbox_confirm"
        runat="server" Enabled="True">
    </cc1:ModalPopupExtender>
    <input id="hid_popupmsboxconfirm" type="hidden" runat="server" />
    <asp:UpdatePanel ID="upd_pn_msbox_confirm" runat="server">
        <ContentTemplate>
            <asp:Panel ID="div_upd_msgbox_confirm2" Width="297px" runat="server" Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
                    <tr>
                        <td style="width: 245px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;
                        </td>
                        <td style="width: 52px; background-repeat: repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="2" width="286px" style="vertical-align: middle; background-color: #FFFFFF;"
                    align="center">
                    <tr>
                        <td>
                            <asp:Panel ID="Panel2" runat="server">
                                <table cellpadding="5" cellspacing="5" align="left" width="280px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_mensajebox" runat="server" Text="xxxx"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btn_msgboxconfir_aceptar" CssClass="btn" runat="server" Text="ACEPTAR"
                                                OnClick="btn_msgboxconfir_no_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- modal popup MSGBOX ALERTA  -->
    <cc1:ModalPopupExtender ID="popup_alerta_msj" DropShadow="True" BackgroundCssClass="modalBackground"
        TargetControlID="hid_popup_alerta_msj" PopupControlID="upd_pn_popup_alerta_msj"
        runat="server" Enabled="True">
    </cc1:ModalPopupExtender>
    <input id="hid_popup_alerta_msj" type="hidden" runat="server" />
    <asp:UpdatePanel ID="upd_pn_popup_alerta_msj" runat="server">
        <ContentTemplate>
            <asp:Panel Style="background-image: url(../Images/fondo.gif); padding-bottom: 8px; padding-top: 0px; background-repeat: repeat"
                ID="div_upd_msgbox_confirm3" runat="server"
                Width="297px">
                <table style="width: 100%; height: 44px" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td style="background-image: url(../Images/flotante/popcab1.gif); width: 245px; background-repeat: repeat"></td>
                            <td style="background-image: url(../Images/flotante/popcab3.gif); width: 52px; background-repeat: repeat"></td>
                        </tr>
                    </tbody>
                </table>
                <table style="vertical-align: middle; background-color: #ffffff" cellspacing="2"
                    cellpadding="2" width="286" align="center">
                    <tbody>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel4" runat="server">
                                    <table cellspacing="5" cellpadding="5" width="280" align="left">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_alerta_msj" runat="server" Text="xxxx"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px"></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btn_alertaconfir_aceptar" OnClick="btn_alertaconfir_aceptar_Click"
                                                        runat="server" CssClass="btn" Text="ACEPTAR"></asp:Button>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>

    <div id="pnlDireccionF" class="fondoPopup" style="z-index: 10000;">
    </div>
    <div id="pnlDireccion" class="PanelPopup_g" style="z-index: 10001; position: fixed; width: 800px; display: none;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
            <tr>
                <td class="TitleL">&nbsp;
                </td>
                <td class="TitleC" style="width: 500px;">&nbsp;
                </td>
                <td class="TitleR">&nbsp;
                </td>
            </tr>
        </table>
        <table class="Cuerpo" cellpadding="0" cellspacing="0" style="width: 780px">
            <tr valign="bottom">
                <td style="width: 160px;">
                    <table id="Table6" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0"
                        style="height: 20px">
                        <tr id="Tr3" runat="server">
                            <td id="Td7" runat="server">
                                <img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                            <td id="Td8" class="TabCabeceraOn" style="width: 180px; text-align: center" runat="server">MODIFICAR DIRECCION</td>
                            <td id="Td9" runat="server">
                                <img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                        <tr>
                            <td align="right">
                                <img id="btnGuardarD" src="../Images/iconos/b-guardar.gif" title="Guardar" alt=""
                                    border="0" style="cursor: pointer;" onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"
                                    onclick="javascript:fc_GuardarDir();" />
                                <img id="btnLimpiarD" src="../Images/iconos/b-limpiar.gif" title="Limpiar" alt=""
                                    border="0" style="cursor: pointer; display: none;" onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'" />
                                <img id="btnCerrarD" src="../Images/iconos/b-cerrar.gif" title="Regresar" alt=""
                                    border="0" style="cursor: pointer;" onclick="javascript:fc_MostrarPoPup('0', 'pnlDireccion');"
                                    onmouseover="javascript:this.src='../Images/iconos/b-cerrar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-cerrar.gif'" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="Cuerpo" width="780px" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img alt="" src="../Images/Mantenimiento/fbarr.gif" width="780px" /></td>
            </tr>
            <tr>
                <td style="background-color: #ffffff; vertical-align: top;">
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="padding-left: 5px; padding-right: 5px;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="380px" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%;" class="cbusqueda">
                                    <tr>
                                        <td style="width: 10%;">Tipo Cliente
                                        </td>
                                        <td style="width: 23%;">
                                            <select id="ddlTipoClienteD" class="cdll" style="width: 150px;">
                                            </select>
                                        </td>
                                        <td style="width: 12%;">Tipo Documento
                                        </td>
                                        <td style="width: 22%;">
                                            <select id="ddlTipoDocD" class="cdll" style="width: 150px;">
                                            </select>
                                        </td>
                                        <td style="width: 12%;">Nro. Documento
                                        </td>
                                        <td style="width: 21%;">
                                            <input id="txtNumDocD" type="text" class="ctxt" style="width: 150px" maxlength="50"
                                                readonly="readonly" />
                                        </td>
                                    </tr>
                                    <tr id="TR_PJ">
                                        <td>Razón Social
                                        </td>
                                        <td colspan="3">
                                            <input id="txtRazonSocialD" type="text" class="ctxt" style="width: 250px" maxlength="50"
                                                readonly="readonly" />
                                        </td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr id="TR_PN">
                                        <td>Nombre
                                        </td>
                                        <td>
                                            <input id="txtNombreD" type="text" class="ctxt" style="width: 145px" maxlength="50"
                                                readonly="readonly" />
                                        </td>
                                        <td>Ape. Paterno
                                        </td>
                                        <td>
                                            <input id="txtApePatD" type="text" class="ctxt" style="width: 145px" maxlength="50"
                                                readonly="readonly" />
                                        </td>
                                        <td>Ape. Materno
                                        </td>
                                        <td>
                                            <input id="txtApeMatD" type="text" class="ctxt" style="width: 150px" maxlength="50"
                                                readonly="readonly" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>País
                                        </td>
                                        <td>
                                            <select id="ddlPaisD" class="cdll" style="width: 150px;">
                                            </select>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>Departamento
                                        </td>
                                        <td>
                                            <select id="ddlDepD" class="cdll" style="width: 150px;">
                                            </select>
                                        </td>
                                        <td>Provincia
                                        </td>
                                        <td>
                                            <select id="ddlProvD" class="cdll" style="width: 150px;">
                                            </select>
                                        </td>
                                        <td>Distrito
                                        </td>
                                        <td>
                                            <select id="ddlDistD" class="cdll" style="width: 150px;">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Dirección
                                        </td>
                                        <td colspan="3">
                                            <input id="txtDirD" type="text" class="ctxt" style="width: 412px" maxlength="50" />
                                        </td>
                                        <td style="display: none">
                                            <!--@002 I/F-->
                                            Teléfono Fijo
                                        </td>
                                        <td style="display: none">
                                            <!--@002 I/F-->
                                            <input id="txtTelefonoD" type="text" class="ctxt" style="width: 130px" maxlength="50" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <!--@002 I/F-->
                                        <td>Email
                                        </td>
                                        <td colspan="3">
                                            <input id="txtEmailD" type="text" class="ctxt" style="width: 250px" maxlength="50" />
                                        </td>
                                        <td>Fax
                                        </td>
                                        <td>
                                            <input id="txtFaxD" type="text" class="ctxt" style="width: 130px" maxlength="50" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <img alt="" src="../Images/Mantenimiento/fba.gif" width="780px" /></td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">

        var nid_usuario = "<%=this.Profile.Usuario.NID_USUARIO %>";
        var co_usuario = "<%=this.Profile.Usuario.CUSR_ID %>";
        var no_usuario_red = "<%=this.Profile.UsuarioRed %>";
        var no_estacion_red = "<%=this.Profile.Estacion %>";
        var nid_perfil = "<%=this.Profile.Usuario.NID_PERFIL %>";

        var _SELECIONAR = '<option value=0>--Seleccionar--</option>';
        var _TODOS = '<option value=0>--Todos--</option>';
        var _VACIO = '<option value=0>--</option>';
        //************************************************************************************
        function CallWebMethodAGP(methodName, paramterData, callbackFunctionWithArgs) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //async:true,
                url: "SRC_Maestro_Detalle_Cliente.aspx/" + methodName + "", //Metodo
                data: JSON.stringify({ filtro: paramterData }), //Envio Parametros al Metodo
                //beforeSend: function() { fc_MostrarCarga(1); }, //Mostrar Carga Antes   de ..
                //complete: function() { fc_MostrarCarga(0); },   //Mostrar Carga Despues de ..
                success: function (d) { callbackFunctionWithArgs(d.d); }, //Resultado
                error: function (jqXHR, textStatus, errorThrown) {
                    //fc_MostrarCarga(0);
                    var errEstado = jqXHR.status;
                    var errMensaje = jqXHR.responseText == "" ? "" : jQuery.parseJSON(jqXHR.responseText).Message; //Mensaje Error
                    var errTipo = jqXHR.responseText == "" ? "" : jQuery.parseJSON(jqXHR.responseText).ExceptionType; //Tipo Excepcion
                    //var errStack = jqXHR.responseText == "" ? "" : jQuery.parseJSON(jqXHR.responseText).StackTrace; //StackTrace
                    if (errEstado == '401')//Error de Autenticacion
                        location.reload(true);
                    else if (errEstado == '500')//Error Web
                    {
                        alert('Error: ' + errTipo + '\nMensaje: ' + errMensaje);
                    }
                } //Mostrar Error   

            });
        }


        function CargarInicial() {

            var filtro = new Array();
            filtro[1] = 0;

            ////ListarDepartamento
            CallWebMethodAGP('ListarDepartamento', filtro,
                function (resDD) {
                    $("#ddlDepD").html(_SELECIONAR);
                    $.each(JSON.parse(resDD), function () {
                        $("#ddlDepD").append($("<option></option>").val(this['id']).html(this['des']));
                    });
                });

            var idCliente = $("#<%=hdIDCliente.ClientID %>").val();
            if (idCliente != '0') {
                filtro[1] = idCliente;
                CallWebMethodAGP('ListarDatoDireccion', filtro,
                    function (res) {
                        if (res[0] != '0') {
                            //Datos Consulta
                            //--------------
                            $("#<%=hdIDClienteDir.ClientID %>").val(res[1]);
                            $("#txtDireccion").val(res[5]);
                            $("#txtDireccionUbigeo").val(res[9]);
                            $("#txtDireccionTelefono").val(res[2]);
                            $("#txtDireccionFax").val(res[3]);
                            $("#txtDireccionCorreo").val(res[10]);
                        }
                    });
            }
            //fc_CargarDatos();
        }

        function fc_OpenPopup() {
            //fc_CargarDatos();
            fc_MostrarPoPup('1', 'pnlDireccion');
        }

        $("#ddlDepD").change(function () {
            $("#ddlProvD").html(_SELECIONAR);
            $("#ddlDistD").html(_SELECIONAR);
            if ($("#ddlDepD").val() != '0') {
                var filtro = new Array();
                filtro[1] = $("#ddlDepD").val();
                CallWebMethodAGP('ListarProvincia', filtro,
                    function (res) {

                        $.each(JSON.parse(res), function () {
                            $("#ddlProvD").append($("<option></option>").val(this['id']).html(this['des']));
                        });
                    });
            }
        });

        $("#ddlProvD").change(function () {
            $("#ddlDistD").html(_SELECIONAR);
            if ($("#ddlProvD").val() != '0') {
                var filtro = new Array();
                filtro[1] = $("#ddlDepD").val();
                filtro[2] = $("#ddlProvD").val();
                CallWebMethodAGP('ListarDistrito', filtro,
                    function (res) {
                        $("#ddlDistD").html(_SELECIONAR);
                        $.each(JSON.parse(res), function () {
                            $("#ddlDistD").append($("<option></option>").val(this['id']).html(this['des']));
                        });
                    });
            }
        });

        function fc_GuardarDir() {

            var msgError = ''

            //if ($("#ddlPaisD").val() == '0')
            //    msgError += '-Debe seleccionar un País.\n';
            if ($("#ddlDepD").val() == '0')
                msgError += '-Debe seleccionar un Departamento.\n';
            if ($("#ddlProvD").val() == '0')
                msgError += '-Debe seleccionar una Provincia.\n';
            if ($("#ddlDistD").val() == '0')
                msgError += '-Debe seleccionar un Distrito.\n';
            if ($("#txtDirD").val() == '0')
                msgError += '-Debe ingresar una Dirección.\n';
            if ($("#txtEmailD").val() != '' && !fc_Trim($("#txtEmailD").val()).match(RE_EMAIL))
                msgError += '-Debe ingresar un Email válido.\n';

            if (msgError != '') {
                alert(msgError);
                return false;
            }
            else if (confirm('¿Esta seguro de Guardar la Dirección?.')) {

                var filtro = new Array();
                filtro[1] = $("#<%=hdIDClienteDir.ClientID %>").val();
                filtro[2] = $("#<%=hdIDCliente.ClientID %>").val();
                filtro[3] = $("#txtTelefonoD").val();
                filtro[4] = $("#txtFaxD").val();
                filtro[5] = 0; //$("#ddlPaisD").val();
                filtro[6] = $("#txtDirD").val();
                filtro[7] = $("#ddlDepD").val();
                filtro[8] = $("#ddlProvD").val();
                filtro[9] = $("#ddlDistD").val();
                filtro[10] = $("#txtEmailD").val();
                filtro[11] = co_usuario;
                filtro[12] = no_usuario_red;
                filtro[13] = no_estacion_red;

                CallWebMethodAGP('GuardarDireccionTaller', filtro,
                    function (res) {

                        if (res[0] == '1') {
                            alert(res[1]);
                            fc_MostrarPoPup('0', 'pnlDireccion');

                            location.reload(true);

                        }
                        else {//Error
                            fc_MostrarPoPup('0', 'pnlDireccion');
                            alert(res[1]); //error  //res[0] => (0 || -1 || -2 || -3)                    
                        }

                    });
            }

        }
        function fc_OpenPopup() {

            //default
            $("#ddlTipoClienteD").empty();
            $("#ddlTipoDocD").empty();
            $("#ddlTipoClienteD").append($("<option></option>").val($("#<%=ddl_clie_tipopersona.ClientID %> option:selected").val()).html($("#<%=ddl_clie_tipopersona.ClientID %> option:selected").text()));
            $("#ddlTipoDocD").append($("<option></option>").val($("#<%=ddl_grtipodocumento.ClientID %> option:selected").val()).html($("#<%=ddl_grtipodocumento.ClientID %> option:selected").text()));
            $("#txtNumDocD").val($("#<%=txt_grnrodoc.ClientID %>").val());

            if ($("#<%=ddl_clie_tipopersona.ClientID %> option:selected").val() == '0001') {

                $("#txtNombreD").val($("#<%=txt_grnombres.ClientID %>").val());
                $("#txtApePatD").val($("#<%=txt_grpaterno.ClientID %>").val());
                $("#txtApeMatD").val($("#<%=txt_grmaterno.ClientID %>").val());
                $("#TR_PN").css('display', '');
                $("#TR_PJ").css('display', 'none');
            }
            else {
                $("#txtRazonSocialD").val($("#<%=txt_grnombres.ClientID %>").val());
                $("#TR_PN").css('display', 'none');
                $("#TR_PJ").css('display', '');
            }

            //datos
            var idCliente = $("#<%=hdIDCliente.ClientID %>").val();
            var flNew = "1";
            if (idCliente == '0') {
                flNew = "1";
                $("#<%=hdIDClienteDir.ClientID %>").val("0");
            }
            else {
                var filtro = new Array();
                filtro[1] = idCliente;

                CallWebMethodAGP('ListarDatoDireccion', filtro,
                    function (res) {
                        if (res[0] != '0') {
                            flNew = "0";

                            $("#<%=hdIDClienteDir.ClientID %>").val(res[1]);
                            $("#ddlDepD").val(res[6]);

                            ////Provincia
                            filtro[1] = res[6];
                            CallWebMethodAGP('ListarProvincia', filtro,
                                function (res1) {
                                    $("#ddlProvD").html(_SELECIONAR);
                                    $.each(JSON.parse(res1), function () {
                                        $("#ddlProvD").append($("<option></option>").val(this['id']).html(this['des']));
                                    });
                                    $("#ddlProvD").val(res[7]);

                                    ////Distrito
                                    filtro[1] = res[6];
                                    filtro[2] = res[7];
                                    CallWebMethodAGP('ListarDistrito', filtro,
                                        function (res2) {
                                            $("#ddlDistD").html(_SELECIONAR);
                                            $.each(JSON.parse(res2), function () {
                                                $("#ddlDistD").append($("<option></option>").val(this['id']).html(this['des']));
                                            });
                                            $("#ddlDistD").val(res[8]);
                                        }
                                    );
                                    ////Distrito
                                }
                            );

                            $("#txtDirD").val(res[5]);
                            $("#txtTelefonoD").val(res[2]);
                            $("#txtEmailD").val(res[10]);
                            $("#txtFaxD").val(res[3]);

                        }
                        else {
                            flNew = "1";
                        }
                    }
                );
            }

            if (flNew = "1") {
                $("#<%=hdIDClienteDir.ClientID %>").val("0");
                $("#ddlDepD option:eq(0)").prop('selected', true);
                $("#ddlProvD").html(_SELECIONAR);
                $("#ddlDistD").html(_SELECIONAR);
                $("#txtDirD").val("");
                $("#txtTelefonoD").val("");
                $("#txtEmailD").val("");
                $("#txtFaxD").val("");
                //$("#ddlPaisD").val("162");
            }

            fc_MostrarPoPup('1', 'pnlDireccion');
        }

        function fc_MostrarPoPup(opc, div) {
            var estilo = (opc == '0') ? 'none' : 'block';
            var windowWidth = document.documentElement.clientWidth;

            var popupWidth = $("#" + div + "").width();
            $("#" + div + "").css(
                {
                    "position": "absolute",
                    "top": 150, //150px
                    "left": (windowWidth - popupWidth) / 2
                });
            document.getElementById(div).style.display = estilo;
            document.getElementById(div + 'F').style.display = estilo;
        }
        CargarInicial();
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
