<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="SRC_Maestro_Detalle_Vehiculo.aspx.cs" Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_Vehiculo" %>
<%@ MasterType VirtualPath="~/Principal.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/ComboPaisTelefono.ascx" TagName="ComboPaisTelefono" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/SRC_Validacion.js"></script>
    <style>
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

    <script language="javascript" type="text/javascript">

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
            // esconde el popup 
            $find(ModalProgress).hide();

            //Set Pestaña Seleccionada    
            var index = (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesVehiculo_tabVehiculo').style.visibility == 'visible') ? 0 : 1;
            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);
            recoverCombos();
        }
        function saveCombos() {
            combo_PaisTelefono = document.getElementById("cboPaisTelefonoFijo");
            combo_PaisCelular = document.getElementById("cboPaisTelefonoCel");
        }
        function recoverCombos() {
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


            var deshabilitado = document.getElementById('<%=this.txt_clie_telefono.ClientID %>').disabled;
            $("#cboPaisTelefonoFijo").prop("disabled", deshabilitado);
            $("#cboPaisTelefonoCel").prop("disabled", deshabilitado);
        }

        function SoloPlaca(eventObj) {
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
            //else if (key==45){} //sin guión
            else {
                return false; //anula la entrada de texto. 
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
                return false; //anula la entrada de texto. 
            }
        }


        function Fc_Cambiartab(sender, e) {
            var CurrentTab = sender;
            var index = sender.get_activeTab()._tabIndex;

            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);
        }

        function Fc_BuscarPropietario() {
            if ((document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesVehiculo_tabPropietario_ddl_pro_tipodoc').selectedIndex > 0) &&
                (fc_Trim(document.getElementById('<%=txt_pro_nro_doc.ClientID%>').value) != ""))
                document.getElementById('<%=btn_valpro.ClientID%>').click();
            else
                return false;
        }
        function Fc_BuscarCliente() {
            if ((document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesVehiculo_tabPropietario_ddl_clie_tipodoc').selectedIndex > 0) &&
                (fc_Trim(document.getElementById('<%=txt_clie_nro_doc.ClientID%>').value) != ""))
                document.getElementById('<%=btn_valcliente.ClientID%>').click();
            else
                return false;
        }
        function Fc_BuscarContacto() {
            if ((document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesVehiculo_tabPropietario_ddl_cont_tipodoc').selectedIndex > 0) &&
                (fc_Trim(document.getElementById('<%=txt_cont_nro_doc.ClientID%>').value) != ""))
                document.getElementById('<%=btn_val_contacto.ClientID%>').click();
            else
                return false;
        }

        function Fc_RegistrarVehiculo() {


            var txtplaca = document.getElementById('<%=txt_placapatente.ClientID%>');
            var txtvin = document.getElementById('<%=txt_nrovin.ClientID%>');
            var ddlmarca = document.getElementById('<%=ddl_marca.ClientID%>');
            var ddlmodelo = document.getElementById('<%=ddl_modelo.ClientID%>');
            var txtkilometraje = document.getElementById('<%=txt_kilometraje.ClientID%>');
            var hidnid_propietario = document.getElementById('<%=hid_nid_propietario.ClientID%>');
            var hidnid_cliente = document.getElementById('<%=hid_nid_cliente.ClientID%>');
            var hidnid_contacto = document.getElementById('<%=hid_nid_contacto.ClientID%>');
            var hidtipopersona = document.getElementById('<%=hid_tipopersona.ClientID%>');
            var ddlpro_tipopersona = document.getElementById('<%=ddl_pro_tipopersona.ClientID%>');
            var ddlcli_tipopersona = document.getElementById('<%=ddl_clie_tipopersona.ClientID%>');

            var correo = /^[A-Za-z][A-Za-z0-9_.]*@[A-Za-z0-9_]+\.[A-Za-z0-9_.]+[A-za-z]$/;

            //---------------------    
            // Datos Vehiculo
            //---------------------

            if (fc_Trim(txtplaca.value) == "") {
                alert('Debe ingresar una ' + document.getElementById("<%=this.lblTextoPlaca.ClientID %>").innerText + '.');
                return false;
            }

            if ('<%=ConfigurationManager.AppSettings["VINObligatorio"].ToString() %>' == '1') {
                if (fc_Trim(txtvin.value) == "") {
                    alert('Debe asociar un número de VIN.'); return false;
                }
            }
            if (ddlmarca.selectedIndex == 0) {
                alert('Debe seleccionar una marca.'); return false;
            }
            if (ddlmodelo.selectedIndex == 0) {
                alert('Debe seleccionar un modelo.'); return false;
            }
            if (txtkilometraje.value != "") {
                if (parseInt(txtkilometraje.value) < 0) {
                    alert('Debe ingresar Km. Actual'); return false;
                }
            }
            else {
                alert('Debe ingresar Km. Actual.(No Blanco, mayor o igual a 0)'); return false;
            }

            //-------------------------    
            // Anio y tipo vehiculo
            //-------------------------

            if ('<%=ConfigurationManager.AppSettings["CodPais"].ToString() %>' == '2') {
                if (document.getElementById('<%=ddl_Anio.ClientID%>').selectedIndex == 0) {
                    alert('Debe seleccionar el Año del Vehículo'); return false;
                }
                if (document.getElementById('<%=ddl_Tipo.ClientID%>').selectedIndex == 0) {
                    alert('Debe seleccionar un Tipo de Vehículo'); return false;
                }
            }


            //------------------------------------------
            // Datos Propietario - Cliente - Contacto
            //------------------------------------------

            var msError = '';

            //----------------   
            // PROPIETARIO ==>
            //----------------

            if (document.getElementById('<%=ddl_pro_tipopersona.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddl_pro_tipodoc.ClientID%>').selectedIndex == 0) {
                    alert('- Seleccione el tipo de Documento para el Propietario.\n'); return false;
                }

                if (fc_Trim(document.getElementById('<%=txt_pro_nro_doc.ClientID%>').value) == '')
                    msError += '- Ingresar el número de documento para el Propietario.\n';
                if (document.getElementById('<%=ddl_pro_tipopersona.ClientID%>').selectedIndex == 1) {
                    if (fc_Trim(document.getElementById('<%=txt_pro_nom_rz.ClientID%>').value) == '')
                        msError += '- Ingresar el Nombre del Propietario.\n';
                    if (fc_Trim(document.getElementById('<%=txt_prop_apepaterno.ClientID%>').value) == '')
                        msError += '- Ingresar el Apellido Paterno del Propietario.\n';
                    if (fc_Trim(document.getElementById('<%=txt_prop_apematerno.ClientID%>').value) == '')
                        msError += '- Ingresar el Apellido Materno  del Propietario.\n';
                }
                else {
                    if (fc_Trim(document.getElementById('<%=txt_pro_nom_rz.ClientID%>').value) == '')
                        msError += '- Ingresar la razón social del Propietario.\n';
                }

                if (fc_Trim(document.getElementById('<%=txt_prop_email.ClientID%>').value) == '')
                    msError += mstrDebeIngresar + "el correo del Propietario.\n";
                else if (!fc_Trim(document.getElementById("<%=this.txt_prop_email.ClientID %>").value).match(RE_EMAIL))
                    msError += mstrDebeIngresar + "un correo válido para el Propietario.\n";

                if ((fc_Trim(document.getElementById('<%=txt_prop_email_trab.ClientID%>').value) != '') &&
                    (!fc_Trim(document.getElementById("<%=this.txt_prop_email_trab.ClientID %>").value).match(RE_EMAIL)))
                    msError += mstrDebeIngresar + "un correo de trabajo válido para el Propietario.\n";

                if ((fc_Trim(document.getElementById('<%=txt_prop_email_alter.ClientID%>').value) != '') &&
                    (!fc_Trim(document.getElementById("<%=this.txt_prop_email_alter.ClientID %>").value).match(RE_EMAIL)))
                    msError += mstrDebeIngresar + "un correo alternativo válido para el Propietario.\n";

                if (msError.length != 0) {
                    alert(msError); return false;
                }
            }

            //----------------   
            // CLIENTE ==>
            //----------------

            msError = '';

            if (document.getElementById('<%=ddl_clie_tipopersona.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddl_clie_tipodoc.ClientID%>').selectedIndex == 0) {
                    alert('- Seleccione el tipo de Documento para el Cliente.\n'); return false;
                }

                if (fc_Trim(document.getElementById('<%=txt_clie_nro_doc.ClientID%>').value) == '')
                    msError += '- Ingresar el número de documento para el Cliente.\n';
                if (document.getElementById('<%=ddl_clie_tipopersona.ClientID%>').selectedIndex == 1) {
                    if (fc_Trim(document.getElementById('<%=txt_clie_nom_rz.ClientID%>').value) == '')
                        msError += '- Ingresar el Nombre del Cliente.\n';
                    if (fc_Trim(document.getElementById('<%=txt_clie_apepaterno.ClientID%>').value) == '')
                        msError += '- Ingresar el Apellido Paterno del Cliente.\n';
                    if (fc_Trim(document.getElementById('<%=txt_clie_apematerno.ClientID%>').value) == '')
                        msError += '- Ingresar el Apellido Materno  del Cliente.\n';
                }
                else {
                    if (fc_Trim(document.getElementById('<%=txt_clie_nom_rz.ClientID%>').value) == '')
                        msError += '- Ingresar la razón social del Cliente.\n';
                }


                if (fc_Trim(document.getElementById('<%=txt_clie_email.ClientID%>').value) == '')
                    msError += mstrDebeIngresar + "el correo del Cliente.\n";
                else if (!fc_Trim(document.getElementById("<%=this.txt_clie_email.ClientID %>").value).match(RE_EMAIL))
                    msError += mstrDebeIngresar + "un correo personal válido para el Cliente.\n";

                if ((fc_Trim(document.getElementById('<%=txt_clie_email_trab.ClientID%>').value) != '') &&
                    (!fc_Trim(document.getElementById("<%=this.txt_clie_email_trab.ClientID %>").value).match(RE_EMAIL)))
                    msError += mstrDebeIngresar + "un correo de trabajo válido para el Cliente.\n";

                if ((fc_Trim(document.getElementById('<%=txt_clie_email_alter.ClientID%>').value) != '') &&
                    (!fc_Trim(document.getElementById("<%=this.txt_clie_email_alter.ClientID %>").value).match(RE_EMAIL)))
                    msError += mstrDebeIngresar + "un correo alternativo válido para el Cliente.\n";

                var pais_celular = document.getElementById("cboPaisTelefonoCel").value;
                var telf_movil1 = document.getElementById('<%=txt_clie_celular.ClientID%>').value;
                var pais_fijo = document.getElementById("cboPaisTelefonoFijo").value;
                var telf_fijo = document.getElementById('<%=txt_clie_telefono.ClientID%>').value;
                if (pais_celular == "" && telf_movil1 != "") { msError += "Si ingresa un número celular debe indicar el pais. \n"; }
                else if (valida_celular(document.getElementById('<%=txt_clie_celular.ClientID%>'), (pais_celular == "162")) != true) { msError += SRC_Telefono_Movil_Invalido + " \n"; };
                if (pais_fijo == "" && telf_fijo != "") { msError += "Si ingresa un número de teléfono debe indicar el pais. \n"; }
                else if (valida_telefono(document.getElementById('<%=txt_clie_telefono.ClientID%>')) != true) { msError += SRC_Telefono_Fijo_Invalido + " \n"; }
                if (telf_fijo == "" && telf_movil1 == "") { msError += "Debe ingresar un teléfono fijo o un teléfono celular. \n"; }


                if (valida_email_blacklist(document.getElementById("<%=this.txt_cont_email.ClientID %>")) != true)
                    mstrError += SRC_Email_Invalido;


                if (msError.length != 0) {
                    alert(msError); return false;
                }
            }

            //----------------   
            // CONTACTO ==>
            //----------------

            msError = '';

            if (document.getElementById('<%=ddl_cont_tipopersona.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddl_cont_tipodoc.ClientID%>').selectedIndex == 0) {
                    alert('- Seleccione el tipo de Documento para el Contacto.\n'); return false;
                }

                msError = '';

                if (fc_Trim(document.getElementById('<%=txt_cont_nro_doc.ClientID%>').value) == '')
                    msError += '- Ingresar el número de documento para el Contacto.\n';
                if (document.getElementById('<%=ddl_cont_tipopersona.ClientID%>').selectedIndex == 1) {
                    if (fc_Trim(document.getElementById('<%=txt_cont_nom_rz.ClientID%>').value) == '')
                        msError += '- Ingresar el Nombre del Contacto.\n';
                    if (fc_Trim(document.getElementById('<%=txt_cont_apepaterno.ClientID%>').value) == '')
                        msError += '- Ingresar el Apellido Paterno del Contacto.\n';
                    if (fc_Trim(document.getElementById('<%=txt_cont_apematerno.ClientID%>').value) == '')
                        msError += '- Ingresar el Apellido Materno  del Contacto.\n';
                }
                else {
                    if (fc_Trim(document.getElementById('<%=txt_cont_nom_rz.ClientID%>').value) == '')
                        msError += '- Ingresar la razón social del Contacto.\n';
                }

                if (fc_Trim(document.getElementById('<%=txt_cont_email.ClientID%>').value) == '')
                    msError += mstrDebeIngresar + "el correo del Contacto.\n";
                else if (!fc_Trim(document.getElementById("<%=this.txt_cont_email.ClientID %>").value).match(RE_EMAIL))
                    msError += mstrDebeIngresar + "un correo personal válido para el Contacto.\n";

                if ((fc_Trim(document.getElementById('<%=txt_cont_email_trab.ClientID%>').value) != '') &&
                    (!fc_Trim(document.getElementById("<%=this.txt_cont_email_trab.ClientID %>").value).match(RE_EMAIL)))
                    msError += mstrDebeIngresar + "un correo de trabajo válido para el Contacto.\n";

                if ((fc_Trim(document.getElementById('<%=txt_cont_email_alter.ClientID%>').value) != '') &&
                    (!fc_Trim(document.getElementById("<%=this.txt_cont_email_alter.ClientID %>").value).match(RE_EMAIL)))
                    msError += mstrDebeIngresar + "un correo alternativo válido para el Contacto.\n";


                if (msError.length != 0) {
                    alert(msError); return false;
                }
            }


            //Validar duplicidad de Propietario-Cliente-Contacto
            //------------------------------------------------------
            if (document.getElementById('<%=ddl_pro_tipopersona.ClientID%>').selectedIndex > 0) {
                if ((fc_Trim(document.getElementById('<%=hid_nid_propietario.ClientID%>').value) == '0') &&
                    (fc_Trim(document.getElementById('<%=hid_nid_cliente.ClientID%>').value) == '0') &&
                    (document.getElementById('<%=ddl_pro_tipopersona.ClientID%>').selectedIndex ==
                        document.getElementById('<%=ddl_clie_tipopersona.ClientID%>').selectedIndex) &&
                    (document.getElementById('<%=ddl_pro_tipodoc.ClientID%>').selectedIndex ==
                        document.getElementById('<%=ddl_clie_tipodoc.ClientID%>').selectedIndex) &&
                    (fc_Trim(document.getElementById('<%=txt_pro_nro_doc.ClientID%>').value) ==
                        fc_Trim(document.getElementById('<%=txt_clie_nro_doc.ClientID%>').value))
                ) {
                    document.getElementById('<%=hid_nid_cliente.ClientID%>').value = '11';
                }
            }

            if (document.getElementById('<%=ddl_clie_tipopersona.ClientID%>').selectedIndex > 0) {
                if ((fc_Trim(document.getElementById('<%=hid_nid_propietario.ClientID%>').value) == '0') &&
                    (fc_Trim(document.getElementById('<%=hid_nid_contacto.ClientID%>').value) == '0') &&
                    (document.getElementById('<%=ddl_pro_tipopersona.ClientID%>').selectedIndex ==
                        document.getElementById('<%=ddl_cont_tipopersona.ClientID%>').selectedIndex) &&
                    (document.getElementById('<%=ddl_pro_tipodoc.ClientID%>').selectedIndex ==
                        document.getElementById('<%=ddl_cont_tipodoc.ClientID%>').selectedIndex) &&
                    (fc_Trim(document.getElementById('<%=txt_pro_nro_doc.ClientID%>').value) ==
                        fc_Trim(document.getElementById('<%=txt_cont_nro_doc.ClientID%>').value))
                ) {
                    document.getElementById('<%=hid_nid_contacto.ClientID%>').value = '11';
                }
            }

            if (document.getElementById('<%=ddl_cont_tipopersona.ClientID%>').selectedIndex > 0) {
                if ((fc_Trim(document.getElementById('<%=hid_nid_cliente.ClientID%>').value) == '0') &&
                    (fc_Trim(document.getElementById('<%=hid_nid_contacto.ClientID%>').value) == '0') &&
                    (document.getElementById('<%=ddl_clie_tipopersona.ClientID%>').selectedIndex ==
                        document.getElementById('<%=ddl_cont_tipopersona.ClientID%>').selectedIndex) &&
                    (document.getElementById('<%=ddl_clie_tipodoc.ClientID%>').selectedIndex ==
                        document.getElementById('<%=ddl_cont_tipodoc.ClientID%>').selectedIndex) &&
                    (fc_Trim(document.getElementById('<%=txt_clie_nro_doc.ClientID%>').value) ==
                        fc_Trim(document.getElementById('<%=txt_cont_nro_doc.ClientID%>').value))
                ) {
                    document.getElementById('<%=hid_nid_contacto.ClientID%>').value = '12';
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="upd_GENERAL" runat="server" UpdateMode="Conditional">
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
                                            <asp:ImageButton ID="btnEditar" onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'" OnClick="btnEditar_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-registrofecha.gif" ToolTip="Editar"></asp:ImageButton>
                                            <asp:ImageButton ID="btnGrabar" onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClick="btnGrabar_Click"
                                                runat="server" OnClientClick="return Fc_RegistrarVehiculo();" ImageUrl="~/Images/iconos/b-guardar.gif"
                                                ToolTip="Grabar"></asp:ImageButton>
                                            <asp:ImageButton ID="btnRegresar" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresar_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-regresar.gif" ToolTip="Regresar"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">

                            <cc1:TabContainer ID="tabMantMaesVehiculo" runat="server" CssClass="" ActiveTabIndex="0"
                                OnClientActiveTabChanged="Fc_Cambiartab">
                                <cc1:TabPanel runat="server" ID="tabVehiculo">
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
                                                                    <td class="lineadatos">
                                                                        <asp:Label ID="Label2" runat="server" SkinID="lblCB" Text="DATOS"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="p_DV" runat="server" Width="125px">
                                                                            <table style="width: 966px;">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellspacing="1" cellpadding="2" border="0" class="textotab" width="950">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td style="width: 12%">
                                                                                                            <asp:Label runat="server" ID="lblTextoPlaca"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="width: 35%">
                                                                                                            <asp:TextBox runat="server" MaxLength="50" ID="txt_placapatente" Style="text-transform: uppercase;" onkeypress="return SoloPlaca(event);"></asp:TextBox>
                                                                                                            <asp:Button runat="server" ID="btn_obtvin" BorderColor="Gray" BorderWidth="1px" BackColor="#E0E0E0"
                                                                                                                ToolTip="Vin" Width="30px" Font-Size="8pt" Text="VIN" Height="17px" BorderStyle="Solid"
                                                                                                                OnClick="btn_obtvin_Click"></asp:Button>
                                                                                                        </td>
                                                                                                        <td style="width: 5%">
                                                                                                            <asp:Label runat="server" Text="A&#241;o" ID="lblEtiquetaAnio"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList runat="server" ID="ddl_Anio">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:TextBox runat="server" Enabled="False" Columns="10" ID="txtAnio"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>VIN
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox runat="server" Width="250px" Enabled="False" MaxLength="50" ID="txt_nrovin"
                                                                                                                Style="text-transform: uppercase;"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="display: none;">
                                                                                                            <asp:Label runat="server" Text="Tipo" ID="lblEtiquetaTipo"></asp:Label>
                                                                                                            <asp:Label runat="server" Text="Color" ID="lblEtiquetaColor"></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="display: none;">
                                                                                                            <asp:DropDownList runat="server" ID="ddl_Tipo">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:TextBox runat="server" Enabled="False" Columns="30" ID="txtColor"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Marca
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList runat="server" Width="180px" AutoPostBack="True" ID="ddl_marca"
                                                                                                                OnSelectedIndexChanged="ddl_marca_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label runat="server" Text="Motor" ID="lblEtiquetaMotor"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox runat="server" Enabled="False" Columns="30" ID="txtMotor"></asp:TextBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Modelo
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList runat="server" Width="180px" ID="ddl_modelo">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td></td>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Km. Actual
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox runat="server" MaxLength="10" ID="txt_kilometraje" Columns="8"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td></td>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>Estado
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList runat="server" Width="153px" ID="ddl_estado">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td></td>
                                                                                                        <td></td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <asp:HiddenField ID="hid_indnuevo" runat="server"></asp:HiddenField>
                                                                        <asp:HiddenField ID="hid_indvalidador" runat="server"></asp:HiddenField>
                                                                        <asp:HiddenField ID="hid_ind_pl_gb" runat="server"></asp:HiddenField>
                                                                        <asp:HiddenField ID="hid_NumPlaca" runat="server" __designer:wfdid="w1"></asp:HiddenField>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lineadatos">&nbsp;
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
                                    <HeaderTemplate>
                                        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('0');"
                                                    onmouseout="javascript:onTabCabeceraOutForm('0');">detalle de vehiculo
                                                </td>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" ID="tabPropietario">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hid_nid_propietario" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_bus_propietario" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_nid_cliente" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_bus_cliente" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_nid_contacto" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_bus_contacto" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_tipopersona" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_indnuevopro" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_indnuevoclie" runat="server"></asp:HiddenField>
                                                <asp:HiddenField ID="hid_indnuevocont" runat="server"></asp:HiddenField>

                                                <div style="width: 995px; height: 480px; overflow-y: auto;">
                                                    <table cellspacing="0" cellpadding="0" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; height: 447px; background-color: #ffffff; padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px;">
                                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="p_propietario" runat="server">
                                                                                        <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="0"
                                                                                            cellpadding="0" width="960" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="lineadatos">
                                                                                                        <asp:Label ID="Label7" runat="server" SkinID="Divisiones" Text="PROPIETARIO"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table width="960" border="0" cellpadding="2" cellspacing="1" class="textotab">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 13%">Tipo de Persona</td>
                                                                                                                    <td style="width: 22%">
                                                                                                                        <asp:DropDownList ID="ddl_pro_tipopersona" runat="server" AutoPostBack="True" Width="150px"
                                                                                                                            OnSelectedIndexChanged="ddl_pro_tipopersona_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 12%">Tipo de Documento</td>
                                                                                                                    <td style="width: 22%">
                                                                                                                        <asp:DropDownList ID="ddl_pro_tipodoc" runat="server" Width="100px" AutoPostBack="True"
                                                                                                                            OnSelectedIndexChanged="ddl_pro_tipodoc_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 11%">Nro. Documento</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_pro_nro_doc" runat="server" Width="100px" MaxLength="20"></asp:TextBox><asp:Button
                                                                                                                            ID="btn_valpro" OnClick="btn_valpro_Click" runat="server" BorderWidth="0px" Height="0px"
                                                                                                                            Width="0px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"></asp:Button>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label11" runat="server" Text="Nombre ó Razon Social"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_pro_nom_rz" runat="server" MaxLength="50" Width="190px"></asp:TextBox></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblPropAP" runat="server" Text="Apellido Paterno"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_prop_apepaterno" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblPropAM" runat="server" Text="Apellido Materno"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_prop_apematerno" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Teléfono Fijo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_pro_telefono" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td>Teléfono Oficina</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_pro_telefono2" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td></td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Teléfono Móvil 1</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_pro_celular" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td>Teléfono Móvil 2</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_pro_celular2" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td></td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Email Personal</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_prop_email" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>Email Trabajo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_prop_email_trab" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>Email Alternativo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_prop_email_alter" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr id="trPropietario_Mensaje" runat="server" visible="false">
                                                                                                                    <td style="text-align: left; font-weight: bold; color: red" colspan="6">Nota: Algunos datos de la persona/empresa no pueden modificarse debido a que su identidad ha sido verificada con una entidad reguladora.</td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 5px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="p_cliente" runat="server" Visible="true">
                                                                                        <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="0"
                                                                                            cellpadding="0" width="960" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="lineadatos">
                                                                                                        <asp:Label ID="Label17" runat="server" SkinID="Divisiones" Text="CLIENTE"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="textotab">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 13%">Tipo de Persona</td>
                                                                                                                    <td style="width: 22%">
                                                                                                                        <asp:DropDownList ID="ddl_clie_tipopersona" runat="server" AutoPostBack="True" Width="150px"
                                                                                                                            OnSelectedIndexChanged="ddl_clie_tipopersona_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 12%">Tipo de Documento</td>
                                                                                                                    <td style="width: 22%">
                                                                                                                        <asp:DropDownList ID="ddl_clie_tipodoc" runat="server" Width="100px" AutoPostBack="True"
                                                                                                                            OnSelectedIndexChanged="ddl_clie_tipodoc_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 11%">Nro. Documento</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_nro_doc" runat="server" Width="100px" MaxLength="20"></asp:TextBox><asp:Button
                                                                                                                            ID="btn_valcliente" OnClick="btn_valcliente_Click" runat="server" BorderWidth="0px" Height="0px"
                                                                                                                            Width="0px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"></asp:Button>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label1" runat="server" Text="Nombre ó Razon Social"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_nom_rz" runat="server" MaxLength="50" Width="190px"></asp:TextBox></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblCliAP" runat="server" Text="Apellido Paterno"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_apepaterno" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblCliAM" runat="server" Text="Apellido Materno"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_apematerno" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Teléfono Fijo</td>
                                                                                                                    <td>
                                                                                                                        <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoFijo" />
                                                                                                                        <asp:HiddenField ID="hfPaisTelefonoFijo" runat="server" Value="162" />
                                                                                                                        <asp:TextBox ID="txt_clie_telefono" runat="server" MaxLength="9" Width="90px" onkeypress="return SoloNumeros(event);"></asp:TextBox>
                                                                                                                        <asp:TextBox runat="server" ID="txtTelefonoFijo_Anexo" class="cls_caja_anexo" MaxLength="10" placeholder="Anexo" />
                                                                                                                    </td>
                                                                                                                    <td>Teléfono Oficina</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_telefono2" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td></td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Teléfono Móvil 1</td>
                                                                                                                    <td>
                                                                                                                        <uc1:ComboPaisTelefono runat="server" ID="cboPaisTelefonoCel" />
                                                                                                                        <asp:HiddenField ID="hfPaisTelefonoCelular" runat="server" Value="162" />
                                                                                                                        <asp:TextBox ID="txt_clie_celular" runat="server" MaxLength="9" Width="90px" onkeypress="return SoloNumeros(event);"></asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td>Teléfono Móvil 2</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_celular2" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td></td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Email Personal</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_email" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>Email Trabajo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_email_trab" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>Email Alternativo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_clie_email_alter" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr id="trCliente_Mensaje" runat="server" visible="false">
                                                                                                                    <td style="text-align: left; font-weight: bold; color: red" colspan="6">Nota: Algunos datos de la persona/empresa no pueden modificarse debido a que su identidad ha sido verificada con una entidad reguladora.</td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 5px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="p_contacto" runat="server" Visible="true">
                                                                                        <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="0"
                                                                                            cellpadding="0" width="960" border="0">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td class="lineadatos">
                                                                                                        <asp:Label ID="Label27" runat="server" SkinID="Divisiones" Text="CONTACTO"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="textotab">
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td style="width: 13%">Tipo de Persona</td>
                                                                                                                    <td style="width: 22%">
                                                                                                                        <asp:DropDownList ID="ddl_cont_tipopersona" runat="server" AutoPostBack="True" Width="150px"
                                                                                                                            OnSelectedIndexChanged="ddl_cont_tipopersona_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 12%">Tipo de Documento</td>
                                                                                                                    <td style="width: 22%">
                                                                                                                        <asp:DropDownList ID="ddl_cont_tipodoc" runat="server" Width="100px" AutoPostBack="True"
                                                                                                                            OnSelectedIndexChanged="ddl_cont_tipodoc_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td style="width: 11%">Nro. Documento</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_nro_doc" runat="server" Width="100px" MaxLength="20"></asp:TextBox><asp:Button
                                                                                                                            ID="btn_val_contacto" OnClick="btn_val_contacto_Click" runat="server" BorderWidth="0px" Height="0px"
                                                                                                                            Width="0px" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"></asp:Button>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label3" runat="server" Text="Nombre ó Razon Social"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_nom_rz" runat="server" MaxLength="50" Width="190px"></asp:TextBox></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblContAP" runat="server" Text="Apellido Paterno"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_apepaterno" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblContAM" runat="server" Text="Apellido Materno"></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_apematerno" runat="server" MaxLength="50" Width="180px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Teléfono Fijo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_telefono" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td>Teléfono Oficina</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_telefono2" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td></td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Teléfono Móvil 1</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_celular" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td>Teléfono Móvil 2</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_celular2" runat="server" MaxLength="20" Width="150px"></asp:TextBox></td>
                                                                                                                    <td></td>
                                                                                                                    <td></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Email Personal</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_email" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>Email Trabajo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_email_trab" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                    <td>Email Alternativo</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_email_alter" runat="server" MaxLength="255" Width="180px"></asp:TextBox></td>
                                                                                                                </tr>

                                                                                                                <tr>
                                                                                                                    <td>Departamento</td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddl_cont_dep" runat="server" Width="150px" AutoPostBack="True"
                                                                                                                            OnSelectedIndexChanged="ddl_cont_dep_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList></td>
                                                                                                                    <td>Provincia</td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddl_cont_prov" runat="server" Width="150px" AutoPostBack="True"
                                                                                                                            OnSelectedIndexChanged="ddl_cont_prov_SelectedIndexChanged">
                                                                                                                        </asp:DropDownList></td>
                                                                                                                    <td>Distrito</td>
                                                                                                                    <td>
                                                                                                                        <asp:DropDownList ID="ddl_cont_dist" runat="server" Width="150px"></asp:DropDownList></td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>Dirección</td>
                                                                                                                    <td colspan="3">
                                                                                                                        <asp:TextBox ID="txt_cont_dir" runat="server" MaxLength="255" Width="515px"></asp:TextBox></td>
                                                                                                                    <td>Fax</td>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txt_cont_fax" runat="server" MaxLength="20" Width="100px"></asp:TextBox></td>
                                                                                                                </tr>
                                                                                                                <tr style="display: none;">
                                                                                                                    <td colspan="5">
                                                                                                                        <asp:HiddenField runat="server" ID="txhIDDireccion" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr id="trContacto_Mensaje" runat="server" visible="false">
                                                                                                                    <td style="text-align: left; font-weight: bold; color: red" colspan="6">Nota: Algunos datos de la persona/empresa no pueden modificarse debido a que su identidad ha sido verificada con una entidad reguladora.</td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </asp:Panel>
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
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                    <HeaderTemplate>
                                        <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('1');"
                                                    onmouseout="javascript:onTabCabeceraOutForm('1');">detalle de propietario
                                                </td>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
