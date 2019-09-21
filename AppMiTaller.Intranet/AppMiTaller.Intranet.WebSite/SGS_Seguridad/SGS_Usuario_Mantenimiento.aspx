<%@ Import Namespace="AppMiTaller.Intranet.BE" %>

<%@ Page Language="C#" MasterPageFile="~/Seguridad.master" AutoEventWireup="true" CodeFile="SGS_Usuario_Mantenimiento.aspx.cs" Inherits="SGS_Seguridad_SGS_Usuario_Mantenimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/SGS_UserControl/TextBoxFecha.ascx" TagName="Fecha" TagPrefix="uc1" %>
<%@ Register Src="~/SGS_UserControl/TextBoxHora.ascx" TagName="Hora" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.multiselect.js"></script>
    <script type="text/javascript">


        var indExisteClave = "";
        function fc_Grabar() {
            try {
                var mstrError = "";

                if (fc_Trim(document.getElementById("<%=this.txtNombres.ClientID %>").value) == "")
                    mstrError += mstrDebeIngresar + "el nombre del usuario.\n";

                if (fc_Trim(document.getElementById("<%=this.txtApePat.ClientID %>").value) == "")
                    mstrError += mstrDebeIngresar + "el apellido paterno del usuario.\n";

                if (fc_Trim(document.getElementById("<%=this.txtDNI.ClientID %>").value) == "")
                    mstrError += mstrDebeIngresar + "el DNI del usuario.\n";
                else {
                    if (!fc_Trim(document.getElementById("<%=this.txtDNI.ClientID %>").value).match(RE_SOLONRO))
                        mstrError += mstrElCampo + "DNI del usuario" + mstrReSoloNro;
                    if (document.getElementById("<%=this.txtDNI.ClientID %>").value.length != 8)
                        mstrError += "- El DNI del usuario debe de ser de 8 dígitos.\n";
                }

                if (fc_Trim(document.getElementById("<%=this.txtLogin.ClientID %>").value) == "")
                    mstrError += mstrDebeIngresar + "el login del usuario.\n";
                else if (!fc_Trim(document.getElementById("<%=this.txtLogin.ClientID %>").value).match(RE_ALAFANUMERICONOESP))
                    mstrError += "- El login del usuario" + mstrReAlfanumerico;

                if (fc_Trim(document.getElementById("<%=this.txtClave.ClientID %>").value) == "" && indExisteClave != "1")
                    mstrError += mstrDebeIngresar + "la contraseña del usuario.\n";
                else if (fc_Trim(document.getElementById("<%=this.txtClave.ClientID %>").value) != "") {
                    if (!document.getElementById("<%=this.txtClave.ClientID %>").value.match(RE_ALAFANUMERICO))
                        mstrError += mstrElCampo + "contraseña del usuario" + mstrReAlfanumerico;
                    if (document.getElementById("<%=this.txtClave.ClientID %>").value.length < 6)
                        mstrError += "- La contraseña del usuario debe de ser de mínimo 6 caracteres.\n";
                }

                if (fc_Trim(document.getElementById("<%=this.txtCorreo.ClientID %>").value) == "")
                    mstrError += mstrDebeIngresar + "el correo del usuario.\n";
                else if (!fc_Trim(document.getElementById("<%=this.txtCorreo.ClientID %>").value).match(RE_EMAIL))
                    mstrError += "- El correo del usuario no es correcto.\n";


                if (fc_Trim(document.getElementById("<%=this.cboTipoUsuario.ClientID %>").value) == "")
                    mstrError += mstrDebeSeleccionar + "tipo de usuario.\n";

                if (fc_Trim(document.getElementById("<%=this.cboPerfil.ClientID %>").value) == "")
                    mstrError += mstrDebeSeleccionar + "el perfil del usuario.\n";

                if (fc_Trim(document.getElementById("<%=this.cboUbicacion.ClientID %>").value) == "")
                    mstrError += mstrDebeSeleccionar + "la ubicación del usuario.\n";

                //Fechas de ingreso
                if (fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value) != "") {
                    if (!isFecha(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value), "dd/MM/yyyy"))
                        mstrError += "- La fecha de inicio " + mstrFecha;
                }

                if (fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value) != "") {
                    if (!isFecha(fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value), "dd/MM/yyyy"))
                        mstrError += "- La fecha de fin " + mstrFecha;
                }

                if (fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value) != "" &&
                    fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value) != "" &&
                    isFecha(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value), "dd/MM/yyyy") &&
                    isFecha(fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value), "dd/MM/yyyy")) {

                    if (fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value) ==
                        fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value)) {
                        mstrError += "- La fecha de inicio " + mstrMayor + " la fecha fin.";
                    } else {
                        var comp = compareDates(fc_Trim(document.getElementById("<%=this.txtFecIni.ClientID %>_txtFecha").value), "dd/MM/yyyy",
                            fc_Trim(document.getElementById("<%=this.txtFecFin.ClientID %>_txtFecha").value), "dd/MM/yyyy");
                        if (comp != "0") mstrError += "- La fecha de inicio " + mstrMayor + " la fecha fin.";
                    }


                }

                //Horas de ingreso
                if (fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value) != "") {
                    if (!isHora(fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value), "HH:mm"))
                        mstrError += "- La hora de inicio " + mstrHora;
                }

                if (fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value) != "") {
                    if (!isHora(fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value), "HH:mm"))
                        mstrError += "- La hora de fin " + mstrHora;
                }

                if (fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value) != "" &&
                    fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value) != "" &&
                    isHora(fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value), "HH:mm") &&
                    isHora(fc_Trim(document.getElementById("<%=this.txtHoraFin.ClientID %>_txtHora").value), "HH:mm")) {
                    var comp = compareDates(fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value), "HH:mm",
                        fc_Trim(document.getElementById("<%=this.txtHoraIni.ClientID %>_txtHora").value), "HH:mm");
                    if (comp != "0") mstrError += "- La hora de inicio " + mstrMayor + " la hora de fin.";
                }

                if (mstrError != "") {
                    alert(mstrError)
                    return false;
                }

                return confirm(mstrSeguroGrabar);
            }
            catch (e) {
                alert(e.message);
            }
            return false;
        }

        function cleanArray(actual) {
            var newArray = new Array();
            for (var i = 0, j = actual.length; i < j; i++) {
                if (actual[i]) {
                    newArray.push(actual[i]);
                }
            }
            return newArray;
        }

        function fc_Cambia() {
            document.getElementById("<%=this.cboPerfil.ClientID %>").click();
            return false;
        }

        /*Para efecto de progress update*/
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_initializeRequest(InitializeRequest);
        //prm.add_endRequest(EndRequest);
        var postBackElement;

        function fc_ResetPassword() {
            var mstrError = "";
            if (document.getElementById("<%=this.txtClave.ClientID %>").value.length < 6)
                mstrError += "- La contraseña del usuario debe de ser de mínimo 6 caracteres.\n";
            if (mstrError != "") {
                alert(mstrError)
                return false;
            }


            return confirm("Seguro de reiniciar contraseña?");
        }

        function fc_SelOpcion(strChkMant, strChkCons, opcionId, ori, indIcono) {
            objChkMant = document.getElementById(strChkMant);
            objChkCons = document.getElementById(strChkCons);
            var indRelacion = "";

            if (objChkMant.checked && ori == "MANT") {
                if (indIcono == "0") {
                    objChkCons.checked = true;
                    objChkCons.disabled = true;
                }
                indRelacion = "A";
            }
            else {
                if (indIcono == "0") {
                    objParent = objChkCons.parentElement;
                    objChkCons.disabled = false;

                    if (objParent != null && objParent.tagName == "SPAN") {
                        objParent.disabled = false;
                    }

                    if (ori == "MANT") {
                        objChkCons.checked = false;
                        document.getElementById("<%=this.chkConsAll.ClientID %>").disabled = false;
                        document.getElementById("<%=this.chkConsAll.ClientID %>").checked = false;
                        document.getElementById("<%=this.chkMantAll.ClientID %>").checked = false;
                    }
                }
                if (objChkCons.checked) indRelacion = "C";
            }

            fc_SetIndRelacion(opcionId, indRelacion);
        }
        function fc_SetIndRelacion(opcionId, indRelacion) {
            var arrOpciones = document.getElementById('<%=this.txhCodOpciones.ClientID %>').value.split('|');
            var arrIndRelacion = document.getElementById('<%=this.txhIndOpciones.ClientID %>').value.split('|');
            var strRetorno = "";

            for (var i = 0; i < arrOpciones.length - 1; i++) {
                if (fc_Trim(arrOpciones[i]) == opcionId)
                    strRetorno += indRelacion + "|";
                else strRetorno += arrIndRelacion[i] + "|";
            }

            document.getElementById('<%=this.txhIndOpciones.ClientID %>').value = strRetorno;
        }

        function fc_CheckAll(objTrigger, sufijo) {
            objTabla = document.getElementById("<%=this.gvOpciones.ClientID %>");
            if (objTabla != null) {
                for (var i = 0; i < objTabla.rows.length; i++) {
                    var codRel = '0' + (i + 2);
                    codRel = codRel.substring(codRel.length, codRel.length - 2)
                    var idChk = "<%=this.gvOpciones.ClientID %>_ctl" + codRel + "_" + sufijo;
                    //alert(idChk);
                    var objChk = document.getElementById(idChk);
                    if (objChk != null) {
                        if (objChk.disabled == false && objTrigger.checked != objChk.checked) {
                            objChk.click();
                        }
                    }
                }
            }

            if (sufijo == "chkMant") {
                document.getElementById("<%=this.chkConsAll.ClientID %>").checked = objTrigger.checked;
                if (objTrigger.checked) document.getElementById("<%=this.chkConsAll.ClientID %>").disabled = true;
                else document.getElementById("<%=this.chkConsAll.ClientID %>").disabled = false;
            }
        }

        function fc_GrabarPerfil() {
            try {
                var mstrError = "";

                var strIndicesSel = fc_Replace(document.getElementById('<%=this.txhIndOpciones.ClientID %>').value, "|", "");
                if (strIndicesSel == "") {
                    mstrError += mstrDebeSeleccionar + "al menos una opción.";
                }

                if (mstrError != "") {
                    alert(mstrError)
                    return false;
                }

                return confirm(mstrSeguroGrabar);
            }
            catch (ex) {
                alert(ex.message);
                return false;
            }
        }

        function fc_AgregarProceso() {
            return true;
        }
    </script>

    <script type="text/javascript">
        function fc_TabChange(indiceTab) {

            document.getElementById("<%=this.txhIndiceTab.ClientID %>").value = indiceTab;
            document.getElementById("<%=this.divTabUsuario.ClientID %>").style.display = "none";
            document.getElementById("<%=this.divTabPerfil.ClientID %>").style.display = "none";

            setTabCabeceraOffForm(0);
            setTabCabeceraOffForm(1);
            setTabCabeceraOffForm(2);
            setTabCabeceraOffForm(3);
            setTabCabeceraOffForm(4);
            setTabCabeceraOffForm(5);
            setTabCabeceraOffForm(6);

            strClassNameOffForm = "";
            strImgIzqOffForm = "";
            strImgDerOffForm = "";

            if (document.getElementById("<%=this.txhNidUsuario.ClientID %>").value != "" && document.getElementById("<%=this.txhNidUsuario.ClientID %>").value != "0") {
                document.getElementById("tblHeader0").style.display = "";
                document.getElementById("tblHeader0").style.display = "";
            }
            else {
                document.getElementById("tblHeader1").style.display = "none";
                document.getElementById("tblHeader1").style.display = "none";
            }

            if (indiceTab == "0") {
                document.getElementById("<%=this.divTabUsuario.ClientID %>").style.display = "";
                setTabCabeceraOnForm(0);
            }
            if (indiceTab == "1") {
                document.getElementById("<%=this.divTabPerfil.ClientID %>").style.display = "";
                setTabCabeceraOnForm(1);
            }
        }

        function fc_TabChangeEvento(indiceTab) {

            document.getElementById("<%=this.txhIndiceTab.ClientID %>").value = indiceTab;
            document.getElementById("<%=this.triggerTabIndexChange.ClientID %>").click();
            return true;
        }
    </script>
    <table id="Table5" cellpadding="0" cellspacing="0" border="0" width="800px">
        <tr>
            <td valign="bottom" style="width: 123px;">
                <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <img id="img13" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                        <td class="TabCabeceraOffForm" onclick="javascript: return fc_TabChangeEvento(0);" style="cursor: pointer"
                            onmouseover="javascript: return onTabCabeceraOverForm('0');"
                            onmouseout="javascript: return onTabCabeceraOutForm('0');"><%= this.tipoAccion %> Usuario</td>
                        <td>
                            <img id="img14" alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                    </tr>
                </table>
            </td>
            <td style="width: 5px;">&nbsp;</td>
            <td valign="bottom" style="width: 108px;">
                <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <img id="img15" alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                        <td class="TabCabeceraOffForm" onclick="javascript: return fc_TabChangeEvento(1);" style="cursor: pointer"
                            onmouseover="javascript: return onTabCabeceraOverForm('1');"
                            onmouseout="javascript: return onTabCabeceraOutForm('1');">Perfil Asociado </td>
                        <td>
                            <img id="img16" alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                    </tr>
                </table>
            </td>
            <td valign="bottom" align="right">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            <asp:HiddenField ID="txhco_empresa" runat="server" Value="" />
                            <asp:HiddenField ID="txhNidUsuario" runat="server" Value="0" />
                            <asp:HiddenField ID="txhIndiceTab" runat="server" Value="0" />
                            <asp:CheckBox ID="triggerTabIndexChange" runat="server" Style="display: none;" AutoPostBack="true" OnCheckedChanged="tabTabIndexChange_ActiveTabChanged" />

                            <td align="right">
                                <asp:ImageButton ID="btnLebantaGuia" runat="server" ImageUrl="~/Images/iconos/b-nuevo.gif"
                                    Style="display: none;" />
                                <asp:ImageButton ID="btnGrabar" runat="server" ToolTip="Grabar" ImageUrl="~/Images/iconos/b-guardar.gif"
                                    Visible="false" OnClientClick="javascript: return fc_Grabar();" OnClick="btnGrabar_OnClick"
                                    onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" ImageAlign="AbsBottom" />

                                <asp:ImageButton ID="btnGrabarPerfil" runat="server" ToolTip="Grabar"
                                    ImageUrl="~/Images/iconos/b-guardar.gif"
                                    onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"
                                    Visible="false" OnClientClick="javascript: return fc_GrabarPerfil();"
                                    OnClick="btnGrabarPerfil_OnClick" ImageAlign="AbsBottom" />

                                <asp:ImageButton ID="btnRegresar" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                    OnClick="btnRegresar_Click"
                                    onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                    onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" ImageAlign="AbsBottom" />&nbsp;&nbsp;
                            </td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="DivCuerpoTab" id="divTabUsuario" runat="server">
        <table width="800px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <!-- Cabecera -->
                <td>
                    <img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
            </tr>
            <tr>
                <!-- Cuerpo -->
                <td style="background-color: #ffffff; vertical-align: top; height: 450px; width: 785px;">
                    <div id="idUsrMnt" class="DivCuerpoTab" runat="server" style="overflow: auto; height: 450px; width: 795px;">
                        <table id="tblNuevoUsuario" runat="server" width="768px" class="cuerponuevo"
                            cellpadding="2" cellspacing="1" border="0"
                            style="margin-left: 5px; margin-right: 5px; margin-top: 5px; height: 70px;">
                            <tr>
                                <td style="width: 15%">Nombres</td>
                                <td style="width: 35%">
                                    <asp:TextBox ID="txtNombres" runat="server" Width="250px" SkinID="txtob" MaxLength="30"></asp:TextBox></td>
                                <td style="width: 15%">Ape. Paterno</td>
                                <td style="width: 35%">
                                    <asp:TextBox ID="txtApePat" runat="server" Width="250px" SkinID="txtob" MaxLength="30"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Ape. Materno</td>
                                <td>
                                    <asp:TextBox ID="txtApeMat" runat="server" Width="250px" MaxLength="30"></asp:TextBox></td>
                                <td>DNI</td>
                                <td>
                                    <asp:UpdatePanel ID="upTexto" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDNI" runat="server" Width="65px" SkinID="txtob" CssClass="" MaxLength="8"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtDNI" EventName="Load" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>Login</td>
                                <td>
                                    <asp:TextBox ID="txtLogin" runat="server" Width="150px" SkinID="txtob" MaxLength="20"></asp:TextBox></td>
                                <td>Contraseña</td>
                                <td>
                                    <asp:TextBox ID="txtClave" TextMode="Password" runat="server" Width="150px" SkinID="txtob"
                                        MaxLength="20"></asp:TextBox>&nbsp;
                                        <asp:ImageButton ID="btnPassReset" ToolTip="Reiniciar Contraseña" runat="server"
                                            OnClientClick="javascript: return fc_ResetPassword();" OnClick="btnPassReset_Click"
                                            ImageUrl="~/Images/iconos/usuario.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td>Correo</td>
                                <td>
                                    <asp:TextBox ID="txtCorreo" runat="server" Width="250px" SkinID="txtob" MaxLength="255"></asp:TextBox></td>
                                <td>Teléfono</td>
                                <td>
                                    <asp:TextBox ID="txtTelefono" runat="server" Width="150px" MaxLength="50"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Tipo</td>
                                <td>
                                    <asp:UpdatePanel ID="upComboTipo" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboTipoUsuario" runat="server" Width="155px" SkinID="cboob"></asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>Perfil</td>
                                <td>
                                    <asp:DropDownList ID="cboPerfil" runat="server" AutoPostBack="true" OnSelectedIndexChanged="combos_SelectedIndexChanged" Width="240px" SkinID="cboob"></asp:DropDownList></td>
                                <td>
                                    <asp:CheckBox ID="chkUpdateCombos" runat="server" AutoPostBack="true" OnCheckedChanged="combos_SelectedIndexChanged" /></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr style="height: 25px">
                                                    <td style="width: 15%">Ubicación</td>
                                                    <td style="width: 35%">
                                                        <asp:DropDownList ID="cboUbicacion" runat="server" Width="200px" SkinID="cboob"></asp:DropDownList></td>
                                                    <td style="width: 15%"></td>
                                                    <td style="width: 35%"></td>
                                                </tr>
                                            </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>Fec.    </td>
                                <td>
                                    <uc1:Fecha ID="txtFecIni" runat="server" />
                                </td>
                                <td>Fec. Fin Acceso</td>
                                <td>
                                    <uc1:Fecha ID="txtFecFin" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Hora Inicio</td>
                                <td>
                                    <uc1:Hora ID="txtHoraIni" runat="server" />
                                </td>
                                <td>Hora Fin</td>
                                <td>
                                    <uc1:Hora ID="txtHoraFin" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Mensaje</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" Rows="3" MaxLength="255" Width="640px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td style="width: 13%">Bloqueado</td>
                                            <td style="width: 15%">
                                                <asp:CheckBox ID="chkBloqueado" runat="server" Text="" /></td>
                                            <td style="width: 15%"></td>
                                            <td style="width: 15%"></td>
                                            <td style="width: 15%"></td>
                                            <td style="width: 15%"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <!-- Pie -->
                <td>
                    <img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
            </tr>
        </table>
    </div>
    <div class="DivCuerpoTab" id="divTabPerfil" runat="server">
        <table width="800px" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <!-- Cabecera -->
                <td>
                    <img alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
            </tr>
            <tr>
                <!-- Cuerpo -->
                <td style="background-color: #ffffff; vertical-align: top; height: 450px;">
                    <table id="tblPerfilAsociado" runat="server" cellpadding="1" cellspacing="1" style="margin-left: 5px; margin-right: 5px;">
                        <tr>
                            <td>
                                <table class="cbusqueda" style="width: 785px; height: 30px" cellspacing="1" cellpadding="2" border="0">
                                    <tr align="left">
                                        <td style="width: 10%">Usuario</td>
                                        <td style="width: 45%">
                                            <asp:TextBox ID="txtUsuarioPerfil" runat="server" Width="250px" SkinID="txtdes" ReadOnly="true"></asp:TextBox></td>
                                        <td style="width: 10%">Perfil</td>
                                        <td style="width: 35%">
                                            <asp:TextBox ID="txtUsuarioPerfilDsc" runat="server" Width="150px" SkinID="txtdes"
                                                ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <table cellpadding="0" cellspacing="0" width="785px">
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/iconos/fbusqueda.gif" Width="785px" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td style="padding-top: 15px; width: 40%;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr valign="top">
                                        <td style="width: 40%; padding-left: 0px; padding-right: 0px">
                                            <asp:Button ID="btnRefreshOpciones" runat="server" OnClick="btnRefreshOpciones_OnClick" />
                                            <asp:HiddenField ID="txhModuloSelected" runat="server" Value="" />
                                            <asp:GridView ID="gvModulos" runat="server" AutoGenerateColumns="false" DataKeyNames="NID_OPCION, CSTRUCT"
                                                SkinID="Grilla" Width="100%" SelectedRowStyle-BackColor="Beige" OnRowDataBound="gvModulos_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="VDEMEN" HeaderText="Módulos">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <td style="width: 60%; padding-left: 5px; padding-right: 5px">
                                            <asp:UpdatePanel ID="upGvOpciones" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="txhCodOpciones" runat="server" Value="" />
                                                    <asp:HiddenField ID="txhIndOpciones" runat="server" Value="" />
                                                    <table width="96%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td style="width: 60%; border-right: 0px; height: 32px" class="CabeceraGrilla">Opción</td>
                                                            <td style="width: 18%; border-right: 0px; height: 32px" class="CabeceraGrilla">Mant.<br />
                                                                <asp:CheckBox ID="chkMantAll" runat="server" onClick="javascript: return fc_CheckAll(this, 'chkMant');" AutoPostBack="false" />
                                                            </td>
                                                            <td style="width: 18%; border-right: 0px; height: 32px" class="CabeceraGrilla">Cons.<br />
                                                                <asp:CheckBox ID="chkConsAll" runat="server" onClick="javascript: return fc_CheckAll(this, 'chkConst');" AutoPostBack="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="width: 100%; height: 350px; overflow: auto">
                                                        <asp:GridView ID="gvOpciones" runat="server" AutoGenerateColumns="false"
                                                            DataKeyNames="NID_OPCION, CSTRUCT, IND_REL,fl_icono"
                                                            SkinID="Grilla" Width="96%" OnRowDataBound="gvOpciones_RowDataBound"
                                                            ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="VDEMEN" HeaderText="Opción" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="60%" ItemStyle-Width="60%"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="Mant." ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="18%" ItemStyle-Width="18%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkMant" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cons." ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="18%" ItemStyle-Width="18%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkConst" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnRefreshOpciones" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!-- Pie -->
                <td>
                    <img alt="" src="../Images/Mantenimiento/fba.gif" /></td>
            </tr>
        </table>
    </div>


</asp:Content>
<asp:Content ContentPlaceHolderID="cphFormPost" ID="cphFormPost" runat="server">
    <script type="text/javascript">

        if (fc_Trim(document.getElementById("<%=this.txhIndiceTab.ClientID %>").value)) fc_TabChange(fc_Trim(document.getElementById("<%=this.txhIndiceTab.ClientID %>").value));
        indExisteClave = "<%=this.indExisteClave %>";
        if ("<%=this.idFilaGrilla %>" != "" && "<%=this.indRefreshGrilla %>" == "1") {
            fc_SeleccionaFilaSimple(document.getElementById("<%=this.idFilaGrilla %>"));
        }
    </script>
</asp:Content>
