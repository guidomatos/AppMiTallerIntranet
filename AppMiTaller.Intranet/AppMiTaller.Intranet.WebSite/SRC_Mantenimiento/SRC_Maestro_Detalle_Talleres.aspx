<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="SRC_Maestro_Detalle_Talleres.aspx.cs" Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_Talleres"
    EnableEventValidation="false" Title="Maestro Detalle Talleres" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../SGS_UserControl/TextBoxFecha.ascx" TagName="TextBoxFecha" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


        .wrapper {
            margin: auto;
            max-width: 330px;
        }

        #navegador ul {
            padding: 0;
        }

        #navegador li {
            color: #466DB7;
            display: inline;
            font-weight: bold;
            font-family: Arial,sans-serif,Verdana;
            font-size: 12px;
        }

            #navegador li :hover {
                background-color: #466DB7;
                color: #ffffff;
                cursor: pointer;
                font-weight: bold;
                font-family: Arial,sans-serif,Verdana;
                font-size: 12px;
            }



        .divTab {
            border: 2px solid #466DB7;
            float: left;
            height: 18px;
            padding-top: 2px;
            width: 32.5%;
        }

        .noticia {
            border-bottom: 2px dotted;
            padding: 10px;
        }

        .datos {
            color: #466DB7;
            font-family: Arial,sans-serif,Verdana;
            font-size: 12px;
            margin: 6px;
            width: 100%;
        }

        .header {
            border: 1px dotted #464646;
            border-bottom: none;
            color: #466DB7;
            font-family: Arial,sans-serif,Verdana;
            font-size: 12px;
            font-weight: bold;
            height: 20px;
            margin: 10px auto auto;
            text-align: center;
            vertical-align: middle;
            width: 450px;
            padding-top: 5px;
        }

        #close {
            float: right;
            background-image: url('http://www.hyundai.pe/js/jquery/images/ui-icons_e7e4e4_256x240.png');
            background-position: -96px -128px;
            height: 16px;
            width: 16px;
            display: block;
            overflow: hidden;
            text-indent: -99999px;
        }

            #close:hover {
                background-color: #999999;
            }

        .carrousel_left {
            background: url("../Images/SRC/nav_left.gif") no-repeat scroll left top transparent;
            border-radius: 10px 0px 0px 10px;
        }

        .carrousel_right {
            background: url("../Images/SRC/nav_right.gif") no-repeat scroll right top transparent;
            border-radius: 0px 10px 10px 0px;
        }

        .carrousel_left, .carrousel_right {
            border: 1px solid #646464;
            float: left;
            height: 190px;
            width: 30px;
            top: -1px;
        }

        carrousel_right:hover {
            background: url("../Images/SRC/nav_left.gif") no-repeat scroll -50px top transparent;
        }

        .carrousel_right:hover {
            background: url("../Images/SRC/nav_right.gif") no-repeat scroll -70px top transparent;
        }
    </style>
    <script type="text/javascript">
        
        function fc_tab(tab) {
            //alert(tab);
            document.getElementById('tab1').style.display = 'none';
            document.getElementById('tab2').style.display = 'none';
            document.getElementById('tab3').style.display = 'none';

            document.getElementById('Htab1').style.backgroundColor = '#FFFFFF';
            document.getElementById('Htab2').style.backgroundColor = '#FFFFFF';
            document.getElementById('Htab3').style.backgroundColor = '#FFFFFF';
            document.getElementById('Htab1').style.color = '#466DB7';
            document.getElementById('Htab2').style.color = '#466DB7';
            document.getElementById('Htab3').style.color = '#466DB7';


            document.getElementById('tab' + tab).style.display = 'block';
            document.getElementById('Htab' + tab).style.backgroundColor = '#466DB7';
            document.getElementById('Htab' + tab).style.color = '#FFFFFF';

        }

    </script>


    <script type="text/javascript">

        //para updatepogresss
        var ModalProgress = '<%= ModalProgress.ClientID %>';
        //Código JavaScript incluido en un archivo denominado jsUpdateProgress.js 
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args) {
            // muestra el popup 
            $find(ModalProgress).show();
        }
        function endReq(sender, args) {
            //  esconde el popup 
            $find(ModalProgress).hide();

            //Set Pestaña Seleccionada
            var index = 0;
            if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesTaller_tabDatosGenerales').style.visibility != 'hidden') index = 0;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesTaller_tabHorario').style.visibility != 'hidden') index = 1;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesTaller_tabServicios').style.visibility != 'hidden') index = 2;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesTaller_tabMarcasModelos').style.visibility != 'hidden') index = 3;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesTaller_tabHorExcepional').style.visibility != 'hidden') index = 4;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantMaesTaller_tabMapa').style.visibility != 'hidden') index = 5;

            Fc_SetCambiartab(index);

            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOffForm('2');
            setTabCabeceraOffForm('3');
            setTabCabeceraOffForm('4');
            setTabCabeceraOffForm('5');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);

        }

        function Fc_SubirImagen() {
            var mstrError = "";
            document.getElementById("<%=this.lblMensaje.ClientID %>").innerHTML = '';

            if (fc_Trim(document.getElementById("<%=this.FileUpload1.ClientID %>").value) == "") {
                mstrError += 'Debe buscar un mapa.';
            }
            else {
                var extension = (/(?:\.([^.]+))?$/).exec(fc_Trim(document.getElementById("<%=this.FileUpload1.ClientID %>").value))[1].toUpperCase();

                if (extension == 'JPG' || extension == 'JPEG' || extension == 'PNG' || extension == 'GIF') {

                    if (window.FileReader) {

                        file = document.getElementById("<%=this.FileUpload1.ClientID %>").files[0];
                        if (file.size > 4000000) {
                            mstrError += 'El tamaño máximo del mapa es de 4 MB.';
                        }
                        else {
                            document.getElementById('<%=btn_SubirMapa.ClientID%>').click();
                        }
                    }
                    else {
                        document.getElementById('<%=btn_SubirMapa.ClientID%>').click();
                    }
                }
                else {
                    mstrError += 'Solo se permiten imagenes con extension (JPG, JPEG, PNG, GIF).';
                }
            }

            if (mstrError != "") {
                document.getElementById("<%=this.FileUpload1.ClientID %>").value = '';
                document.getElementById("<%=this.imgMapa.ClientID %>").src = '';
                alert(mstrError)
                return false;
            }
            return true;
        }

        function Fc_CheckI(chkbox, tipo) {
            document.getElementById('<%=chkI.ClientID %>').checked = (tipo == 'I');
            document.getElementById('<%=chkT.ClientID %>').checked = (tipo == 'T');

            document.getElementById('<%=txt_capacidad_fo.ClientID %>').disabled = (tipo != 'I');
            document.getElementById('<%=txt_capacidad_bo.ClientID %>').disabled = (tipo != 'I');
            document.getElementById('<%=txt_capacidad.ClientID %>').disabled = (tipo == 'I');

            if (tipo == 'I') {
                document.getElementById('<%=txt_capacidad.ClientID %>').value = '';
            }
            else {
                document.getElementById('<%=txt_capacidad_fo.ClientID %>').value = '';
                document.getElementById('<%=txt_capacidad_bo.ClientID %>').value = '';
            }
        }



        function Valida_Codigo_Taller(eventObj) {
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
            else if (key == 8 || key == 9) { } //BS y TAB
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false; //anula la entrada de texto. 
            }
        }

        function Valida_Nombre_Taller(eventObj) {
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
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }        //BS y TAB
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false; //anula la entrada de texto. 
            }
        }

        function MostrarDiaSeleccionado() {
            var lista = document.getElementById('<%=lst_DiasHab.ClientID%>');
            document.getElementById('<%=txt_dia.ClientID%>').value = lista.options[lista.options.selectedIndex].text;
        }

        function Validar_Datos() {

            var txt_codtall = document.getElementById('<%=txt_codtall.ClientID%>');
            var txt_nomtall = document.getElementById('<%=txt_nomtall.ClientID%>');
            var ddl_intervAtenc = document.getElementById('<%=ddl_intervAtenc.ClientID%>');
            var ddl_dpto = document.getElementById('<%=ddl_dpto.ClientID%>');
            var ddl_prov = document.getElementById('<%=ddl_prov.ClientID%>');
            var ddl_dist = document.getElementById('<%=ddl_dist.ClientID%>');
            var ddl_ptored = document.getElementById('<%=ddl_ptored.ClientID%>');
            var ddl_estado = document.getElementById('<%=ddl_estado.ClientID%>');
            

            if (txt_codtall.value.trim() == "") {
                alert('Ingrese Codigo de Taller.');
                txt_codtall.focus();
                return false;
            }

            if (txt_nomtall.value.trim() == "") {
                alert('Ingrese Nombre de Taller.');
                txt_nomtall.focus();
                return false;
            }

            if ($.trim($(ddl_dpto).val()) == '') {
                alert('Debe Seleccionar un Departamento');
                return false;
            }

            if ($.trim($(ddl_prov).val()) == '') {
                alert('Debe Seleccionar una Provincia');
                return false;
            }

            if ($.trim($(ddl_dist).val()) == '') {
                alert('Debe Seleccionar un Distrito');
                return false;
            }

            if ($.trim($(ddl_ptored).val()) == '') {
                alert('Debe Seleccionar un Local');
                return false;
            }

            if ($.trim($(ddl_estado).val()) == '') {
                alert('Debe Seleccionar un Estado');
                return false;
            }

            if (ddl_intervAtenc.selectedIndex == 0 || ddl_intervAtenc.selectedIndex == -1) {
                alert('Debe Seleccionar un Intervalo de Atencion');
                return false;
            }

            return true;
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
            var index = sender.get_activeTab()._tabIndex;

            document.getElementById('<%=this.btnNuevo.ClientID%>').style.display = (index == 5) ? 'inline' : 'none';
            document.getElementById('<%=this.btnBuscar.ClientID%>').style.display = (index == 5) ? 'inline' : 'none';

            if (document.getElementById('<%=this.btnEditar.ClientID%>').style.display == 'none')
                document.getElementById('<%=this.btnGrabar.ClientID%>').style.display = (index == 5) ? 'none' : 'inline';

            
            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOffForm('2');
            setTabCabeceraOffForm('3');
            setTabCabeceraOffForm('4');
            setTabCabeceraOffForm('5');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);
        }

    </script>

    <asp:UpdatePanel ID="Upd_GENERAL" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="2" width="1000" border="0">
                <tr>
                    <td>
                        <!--INICIO ICONOS DE ACCCION -->
                        <table id="tblIconos" class="TablaIconosMantenimientos" cellspacing="0" cellpadding="0"
                            border="0" style="width: 230px; left: 770px;">
                            <tr>
                                <td style="width: 100%" align="right">
                                    <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/iconos/b-buscar.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'"
                                        OnClick="btnBuscar_Click" />
                                    <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/iconos/b-nuevo.gif"
                                        onmouseover="javascript:this.src='../Images/iconos/b-nuevo2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-nuevo.gif'"
                                        OnClick="btnNuevo_Click" />
                                    <asp:ImageButton ID="btnEditar" onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'"
                                        onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'" OnClick="btnEditar_Click"
                                        runat="server" ImageUrl="~/Images/iconos/b-registrofecha.gif" ToolTip="Editar" />
                                    <asp:ImageButton ID="btnGrabar" onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                        onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClick="btnGrabar_Click"
                                        runat="server" OnClientClick="javascript:return Validar_Datos();" ImageUrl="~/Images/iconos/b-guardar.gif"
                                        ToolTip="Grabar" />
                                    <asp:ImageButton ID="btnRegresar" onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'"
                                        onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'" OnClick="btnRegresar_Click"
                                        runat="server" ImageUrl="~/Images/iconos/b-regresar.gif" ToolTip="Regresar" />
                                </td>
                            </tr>
                        </table>
                        <!--FIN ICONOS DE ACCCION -->
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:UpdatePanel ID="upDetalleTaller" runat="server">
                            <ContentTemplate>
                                <cc1:TabContainer ID="tabMantMaesTaller" runat="server" CssClass="" OnClientActiveTabChanged="Fc_Cambiartab"
                                    ActiveTabIndex="0">
                                    <cc1:TabPanel runat="server" ID="tabDatosGenerales">
                                        <HeaderTemplate>
                                            <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                    <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('0');"
                                                        onmouseout="javascript:onTabCabeceraOutForm('0');">Datos Generales
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="980" cellpadding="0" cellspacing="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                            <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="0"
                                                                cellpadding="0" width="970" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="lineadatos" style="height: 20px" abbr="0" valign="bottom">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table class="textotab" border="0" cellpadding="2" cellspacing="1" width="965">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 9%;">Código
                                                                                        </td>
                                                                                        <td style="width: 24%;">
                                                                                            <asp:TextBox ID="txt_codtall" runat="server" SkinID="txtob" Width="200px" MaxLength="20"></asp:TextBox>

                                                                                        </td>
                                                                                        <td style="width: 8%;">Nombre
                                                                                        </td>
                                                                                        <td style="width: 27%">
                                                                                            <asp:TextBox ID="txt_nomtall" runat="server" SkinID="txtob" Width="250px" MaxLength="50"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_dep_taller" runat="server" Text="Departamento"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_dpto" runat="server" AutoPostBack="True" Width="210px"
                                                                                                OnSelectedIndexChanged="ddl_dpto_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_prov_taller" runat="server" Text="Provincia "></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_prov" runat="server" AutoPostBack="True" Width="210px"
                                                                                                OnSelectedIndexChanged="ddl_prov_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbl_dist_taller" runat="server" Text="Distrito"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="True" Width="210px"
                                                                                                OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblTextoLocal" runat="server" Text="Punto de Red"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_ptored" runat="server" AutoPostBack="True" Width="210px"
                                                                                                OnSelectedIndexChanged="ddl_ptored_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>Dirección
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txt_direccion" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Estado
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_estado" runat="server" Width="210px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                                    <td>Descripción del taller
                                                                                                    </td>
                                                                                                    <td colspan="3">
                                                                                                        <asp:TextBox ID="txt_descripcion" runat="server" Width="575px" TextMode="MultiLine"></asp:TextBox>
                                                                                                    </td>
                                                                                   </tr>
                                                                                    <tr>
                                                                                        <td></td>
                                                                                        <td></td>
                                                                                        <td colspan="4"></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
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
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tabHorario">
                                        <HeaderTemplate>
                                            <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                    <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('1');"
                                                        onmouseout="javascript:onTabCabeceraOutForm('1');">horario
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="980" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                                                        <table cellpadding="0" cellspacing="0" width="970" style="margin-left: 5px; margin-right: 5px;"
                                                            border="0">
                                                            <tr>
                                                                <td style="height: 10px" valign="bottom">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 970px;" class="textotab" border="0" cellpadding="0" cellspacing="2">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td class="lineadatos" colspan="3">
                                                                                    <asp:Label ID="Label3" runat="server" SkinID="Divisiones" Text="Días Disponibles"></asp:Label>
                                                                                </td>
                                                                                <td style="width: 3%"></td>
                                                                                <td class="lineadatos">
                                                                                    <asp:Label ID="Label8" runat="server" SkinID="Divisiones" Text="Horas de Atención"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2" rowspan="4" valign="top"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%; padding-top: 5px;" valign="top" align="center">
                                                                                    <asp:UpdatePanel ID="updDiasDis" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:ListBox ID="lst_DiasDisp" runat="server" Height="162px" Width="130px"></asp:ListBox>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td style="width: 5%; padding-top: 5px;" align="center">&nbsp;<asp:ImageButton ID="btn_adddhab" OnClick="btn_adddhab_Click" runat="server"
                                                                                    ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>&nbsp;<br />
                                                                                    <br />
                                                                                    <asp:ImageButton ID="btn_delhab" OnClick="btn_delhab_Click" runat="server" ImageUrl="~/Images/iconos/izquierdas.gif"
                                                                                        Height="20px" Width="20px"></asp:ImageButton>
                                                                                </td>
                                                                                <td style="width: 20%; padding-top: 5px;" valign="top" align="center">
                                                                                    <asp:UpdatePanel ID="updDiasHab" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:ListBox ID="lst_DiasHab" runat="server" Height="162px" onChange="javascript:MostrarDiaSeleccionado();"
                                                                                                Width="130px" AutoPostBack="True" OnSelectedIndexChanged="lst_DiasHab_SelectedIndexChanged"
                                                                                                CssClass="lineadatos"></asp:ListBox>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="btn_adddhab" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="btn_delhab" EventName="Click" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td></td>
                                                                                <td valign="top">
                                                                                    <table border="0" cellpadding="2" cellspacing="1" width="350">
                                                                                        <!--
                                                                                        <tr>
                                                                                            <td style="width: 45%">Día
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txt_dia" ReadOnly="True" runat="server" Columns="12" Width="120px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        -->
                                                                                        <tr>
                                                                                            <td>Desde
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="ddl_horainicio" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="ddl_horainicio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_horainicio_SelectedIndexChanged"
                                                                                                            Width="120px">
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Hasta
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:DropDownList ID="ddl_horafin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_horafin_SelectedIndexChanged"
                                                                                                            Width="120px">
                                                                                                        </asp:DropDownList>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:AsyncPostBackTrigger ControlID="ddl_horafin" EventName="SelectedIndexChanged" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Intervalo de Atencion
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddl_intervAtenc" runat="server" Width="120px" SkinID="cboob">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="lineadatos" colspan="3">
                                                                                    <asp:Label ID="Label9" runat="server" SkinID="Divisiones" Text="Días Exceptuados"></asp:Label>
                                                                                </td>
                                                                                <td></td>
                                                                                <!--
                                                                                <td class="lineadatos">
                                                                                    <asp:Label ID="Label10" runat="server" SkinID="Divisiones" Text="Capacidad de Atención (Cupos)"></asp:Label>
                                                                                </td>
                                                                                -->
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-top: 5px;" align="center">
                                                                                    <asp:Calendar ID="cal_DiasExcep" runat="server" Height="90px" NextPrevFormat="FullMonth"
                                                                                        ForeColor="Black" Font-Size="9pt" Font-Names="Verdana" BorderColor="#404040"
                                                                                        BackColor="White" Width="100px" BorderWidth="1px">
                                                                                        <SelectedDayStyle BackColor="Gray" ForeColor="White" />
                                                                                        <TodayDayStyle BackColor="#CCCCCC" />
                                                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                                                        <NextPrevStyle Font-Size="7pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
                                                                                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                                                        <TitleStyle BackColor="White" Font-Bold="True" Font-Size="9pt" ForeColor="Gray" BorderColor="Black"
                                                                                            BorderWidth="4px" />
                                                                                    </asp:Calendar>
                                                                                </td>
                                                                                <td>&nbsp;&nbsp;
                                                                                    <asp:ImageButton ID="btn_adddiasexc" OnClick="btn_adddiasexc_Click" runat="server"
                                                                                        ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>&nbsp;<br />
                                                                                    <br />
                                                                                    &nbsp; &nbsp;<asp:ImageButton ID="btn_deldiasexc" OnClick="btn_deldiasexc_Click"
                                                                                        runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                </td>
                                                                                <td align="center" style="padding-top: 5px;">
                                                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:ListBox ID="lst_DiasExcep" runat="server" Height="177px" Width="130px" SelectionMode="Multiple"></asp:ListBox>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="btn_adddiasexc" EventName="Click" />
                                                                                            <asp:AsyncPostBackTrigger ControlID="btn_deldiasexc" EventName="Click" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td></td>
                                                                                <!--
                                                                                <td valign="top">
                                                                                    <table border="0" cellpadding="2" cellspacing="1" width="300">
                                                                                        <tbody>
                                                                                            <tr style="text-align: left" valign="middle">
                                                                                                <td style="width: 52%"></td>
                                                                                                <td align="center" style="width: 20%"></td>
                                                                                                <td align="center" rowspan="1" style="width: 20%"></td>
                                                                                                <td align="center" rowspan="1"></td>
                                                                                            </tr>
                                                                                            <tr style="text-align: left" valign="middle">
                                                                                                <td>Front Office
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txt_capacidad_fo" runat="server" MaxLength="3" Width="45px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td rowspan="2" align="center" valign="middle">
                                                                                                    <asp:TextBox ID="txt_capacidad" runat="server" MaxLength="3" Width="45px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td rowspan="2" valign="middle"></td>
                                                                                            </tr>
                                                                                            <tr style="text-align: left" valign="middle">
                                                                                                <td>Back Office
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:TextBox ID="txt_capacidad_bo" runat="server" MaxLength="3" Width="45px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="text-align: left" valign="middle">
                                                                                                <td></td>
                                                                                                <td align="center">
                                                                                                    <asp:CheckBox ID="chkI" runat="server" Text=" " onclick="javascript:Fc_CheckI(this, 'I');" />
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <asp:CheckBox ID="chkT" runat="server" Text=" " onclick="javascript:Fc_CheckI(this, 'T');" />
                                                                                                </td>
                                                                                                <td align="center"></td>
                                                                                            </tr>
                                                                                            <tr style="text-align: left" valign="middle">
                                                                                                <td></td>
                                                                                                <td align="center">Individual
                                                                                                </td>
                                                                                                <td align="center">Total
                                                                                                </td>
                                                                                                <td align="center"></td>
                                                                                            </tr>
                                                                                            <tr style="text-align: left" valign="middle">
                                                                                                <td colspan="2">
                                                                                                    <asp:HiddenField ID="hf_Capacidad" runat="server" />
                                                                                                    <asp:HiddenField ID="hf_DiaValue" runat="server" />
                                                                                                </td>
                                                                                                <td colspan="1"></td>
                                                                                                <td colspan="1"></td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                                -->
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lineadatos">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tabServicios">
                                        <HeaderTemplate>
                                            <table id="tblHeader2" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="height: 20px">
                                                        <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                    <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('2');"
                                                        onmouseout="javascript:onTabCabeceraOutForm('2');">servicios
                                                    </td>
                                                    <td style="height: 20px">
                                                        <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="980" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                                                        <table cellpadding="0" cellspacing="0" width="970" style="background-color: #ffffff; margin-left: 5px; margin-right: 5px;"
                                                            border="0">
                                                            <tr>
                                                                <td class="lineadatos" style="height: 20px" valign="bottom">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="width: 965px;" cellspacing="0" cellpadding="0" border="0" class="textotab">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 25px" valign="bottom">Tipo de Servicio &nbsp;&nbsp;<asp:DropDownList ID="ddl_tiposerv" runat="server" AutoPostBack="true"
                                                                                            Width="229px" OnSelectedIndexChanged="ddl_tiposerv_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 15px"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 25%;" valign="bottom" class="lineadatos">
                                                                                            <asp:Label ID="Label1" runat="server" SkinID="Divisiones" Text="Servicios Disponibles"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" colspan="2" rowspan="3" style="width: 5%">
                                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:ImageButton ID="btn_addserv" OnClick="btn_addserv_Click" runat="server" ImageUrl="~/Images/iconos/derechas.gif"
                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 10px;"></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:ImageButton ID="btn_delserv" OnClick="btn_delserv_Click" runat="server" ImageUrl="~/Images/iconos/izquierdas.gif"
                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td valign="bottom" align="left" class="lineadatos">
                                                                                            <asp:Label ID="lblserv" runat="server" Text="Servicios Seleccionados" SkinID="Divisiones"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="lineadatos" style="width: 25%; height: 10px" valign="bottom"></td>
                                                                                        <td align="left" class="lineadatos" valign="bottom"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <asp:ListBox ID="lst_servdisp" runat="server" Height="250px" Width="220px" SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:GridView ID="gd_servsel" runat="server" SkinID="Grilla" Width="660px" CellPadding="3"
                                                                                                AllowPaging="True" PageSize="5" AutoGenerateColumns="False" OnPageIndexChanging="gd_servsel_PageIndexChanging"
                                                                                                DataKeyNames="no_tipo_servicio" OnRowDataBound="gd_servsel_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="no_tipo_servicio" HeaderText="Tipo de Servicio" HeaderStyle-Width="25%"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="no_servicio" HeaderText="Servicio" HeaderStyle-Width="30%"></asp:BoundField>
                                                                                                    <asp:TemplateField HeaderText="Dias de Atención">
                                                                                                        <ItemTemplate>
                                                                                                            <center>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="border-top: 0px solid; border-left: 0px solid; border-bottom: 0px solid; border-right-width: 0px">
                                                                                                                    <tr>
                                                                                                                        <td style="border: 0px;">
                                                                                                                            <asp:CheckBoxList ID="chkDias" RepeatDirection="Horizontal" runat="server" BorderWidth="0px" BorderStyle="None" Style="border: 0px;">
                                                                                                                                <asp:ListItem Text="LUN" Value="1" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="MAR" Value="1" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="MIE" Value="1" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="JUE" Value="1" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="VIE" Value="1" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="SAB" Value="1" Selected="True"></asp:ListItem>
                                                                                                                            </asp:CheckBoxList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </center>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <SelectedRowStyle Font-Bold="True" />
                                                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                                                <HeaderStyle Font-Bold="True" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="tabMantMaesTaller" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lineadatos">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tabMarcasModelos">
                                        <HeaderTemplate>
                                            <table id="tblHeader3" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="height: 20px">
                                                        <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                    <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('3');"
                                                        onmouseout="javascript:onTabCabeceraOutForm('3');" style="height: 20px">marcas y modelos
                                                    </td>
                                                    <td style="height: 20px">
                                                        <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="980" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="height: 4px">
                                                        <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                                                        <table cellpadding="0" cellspacing="0" width="970" style="background-color: #ffffff; margin-left: 5px; margin-right: 5px;"
                                                            border="0">
                                                            <tr>
                                                                <td class="lineadatos" style="height: 20px" valign="bottom">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="width: 965px;" cellspacing="0" cellpadding="0" border="0" class="textotab">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td colspan="3" valign="middle" style="height: 25px">Marca &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                            <asp:DropDownList ID="ddl_marca" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_marca_SelectedIndexChanged"
                                                                                                Width="218px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="3" style="height: 15px" valign="top"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="bottom" class="lineadatos" style="width: 25%">
                                                                                            <asp:Label ID="Label2" runat="server" SkinID="Divisiones" Text="Modelos Disponibles"></asp:Label>
                                                                                        </td>
                                                                                        <td align="center" colspan="1" rowspan="3" style="width: 5%">
                                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="btn_addmod" OnClick="btn_addmod_Click" runat="server" ImageUrl="~/Images/iconos/derechas.gif"
                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="height: 10px;"></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="btn_delmod" OnClick="btn_delmod_Click" runat="server" ImageUrl="~/Images/iconos/izquierdas.gif"
                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td valign="bottom" align="left" class="lineadatos">
                                                                                            <asp:Label ID="lblmodelo" runat="server" Text="Modelos Seleccionados" SkinID="Divisiones"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="lineadatos" style="width: 25%; height: 10px" valign="bottom"></td>
                                                                                        <td align="left" class="lineadatos" valign="bottom"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <asp:ListBox ID="lst_moddisp" runat="server" Height="250px" Width="220px" SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:GridView ID="gd_modsel" runat="server" SkinID="Grilla" Width="660px" OnPageIndexChanging="gd_modsel_PageIndexChanging"
                                                                                                OnRowDataBound="gd_modsel_RowDataBound" AutoGenerateColumns="False" PageSize="5"
                                                                                                AllowPaging="True" CellPadding="3" DataKeyNames="no_marca">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                        <HeaderStyle Width="5%" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="no_marca" HeaderText="Marca" HeaderStyle-Width="25%"></asp:BoundField>
                                                                                                    <asp:BoundField DataField="no_modelo" HeaderText="Modelos" HeaderStyle-Width="30%"></asp:BoundField>
                                                                                                    <asp:TemplateField HeaderText="Capacidad de Atención">
                                                                                                        <ItemTemplate>
                                                                                                            <center>
                                                                                                                <table border="0" cellpadding="0" cellspacing="2" style="border: 0px solid; text-align: center;" width="90%">
                                                                                                                    <tr>
                                                                                                                        <td id="TDT1" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia1" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                        <td id="TDT2" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia2" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                        <td id="TDT3" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia3" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                        <td id="TDT4" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia4" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                        <td id="TDT5" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia5" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                        <td id="TDT6" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia6" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                        <td id="TDT7" runat="server" style="border: 0px;">
                                                                                                                            <asp:TextBox ID="txtDia7" runat="server" Width="25px" MaxLength="2" Style="text-align: center;" onkeypress="javascript:return SoloNumeros(event);"></asp:TextBox></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td id="TDL1" runat="server" style="border: 0px;">LUN</td>
                                                                                                                        <td id="TDL2" runat="server" style="border: 0px;">MAR</td>
                                                                                                                        <td id="TDL3" runat="server" style="border: 0px;">MIE</td>
                                                                                                                        <td id="TDL4" runat="server" style="border: 0px;">JUE</td>
                                                                                                                        <td id="TDL5" runat="server" style="border: 0px;">VIE</td>
                                                                                                                        <td id="TDL6" runat="server" style="border: 0px;">SAB</td>
                                                                                                                        <td id="TDL7" runat="server" style="border: 0px;">DOM</td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </center>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <PagerStyle HorizontalAlign="Left" />
                                                                                                <HeaderStyle Font-Bold="True" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="tabMantMaesTaller" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lineadatos">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tabHorExcepional">
                                        <HeaderTemplate>
                                            <table id="tblHeader4" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                    <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('4');"
                                                        onmouseout="javascript:onTabCabeceraOutForm('4');">Horario Excepcional
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
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
                                                            <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="0"
                                                                cellpadding="0" width="970" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign="bottom">
                                                                            <asp:Label ID="Label4" runat="server" SkinID="lblTR">CRITERIO DE BUSQUEDA</asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table style="padding-bottom: 5px; padding-top: 5px" border="0" cellpadding="2" cellspacing="1"
                                                                                class="cbusqueda" width="965">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 7%">Descripción
                                                                                        </td>
                                                                                        <td colspan="9">
                                                                                            <asp:TextBox ID="txt_bus_excepdes" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>Fecha Inicio
                                                                                        </td>
                                                                                        <td style="width: 20%">
                                                                                            <asp:TextBox ID="txt_bus_excepfecini" runat="server" Columns="12" MaxLength="10"></asp:TextBox>
                                                                                            <asp:ImageButton ID="btn_bus_ExcepCalFecIni" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                ImageAlign="AbsMiddle" ToolTip="Calendario" />
                                                                                            <cc1:CalendarExtender ID="excepCalendarIni" runat="server" Format="dd/MM/yyyy" DefaultDate=""
                                                                                                TargetControlID="txt_bus_excepfecini" PopupButtonID="btn_bus_ExcepCalFecIni" />
                                                                                            <cc1:MaskedEditExtender ID="excepMasketIni" runat="server" MaskType="Date" Mask="99/99/9999"
                                                                                                UserDateFormat="DayMonthYear" ClearMaskOnLostFocus="true" ErrorTooltipEnabled="true"
                                                                                                MessageValidatorTip="true" ClearTextOnInvalid="true" TargetControlID="txt_bus_excepfecini">
                                                                                            </cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td style="width: 7%">Fecha Fin
                                                                                        </td>
                                                                                        <td style="width: 7%">
                                                                                            <asp:TextBox ID="txt_bus_excepfecfin" runat="server" Width="70px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="excepCalendarFin" runat="server" TargetControlID="txt_bus_excepfecfin"
                                                                                                Enabled="True" PopupButtonID="btn_bus_ExcepCalFecFin" Format="dd/MM/yyyy">
                                                                                            </cc1:CalendarExtender>
                                                                                            <cc1:MaskedEditExtender ID="excepMasketFin" runat="server" TargetControlID="txt_bus_excepfecfin"
                                                                                                Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder=""
                                                                                                CultureTimePlaceholder="" CultureDatePlaceholder="" MaskType="Date" Mask="99/99/9999">
                                                                                            </cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td style="width: 15%">
                                                                                            <asp:ImageButton ID="btn_bus_ExcepCalFecFin" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                        </td>
                                                                                        <td style="width: 5%">Estado&nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_excepestado" runat="server" Width="110px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <table border="0" cellpadding="1" cellspacing="0" width="960">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table cellpadding="0" cellspacing="0" width="970">
                                                                                                <tr>
                                                                                                    <td style="width: 100px">
                                                                                                        <img alt="" src="../Images/iconos/fbusqueda.gif" style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; width: 970px; border-right-width: 0px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <br />
                                                                                            <asp:GridView ID="gv_HorExcep" runat="server" SkinID="Grilla" OnRowCommand="gv_HorExcep_RowCommand"
                                                                                                AutoGenerateColumns="False" Width="960px">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="grid_cod_hor_excep" HeaderText="C&#243;digo" Visible="False">
                                                                                                        <HeaderStyle Width="1%"></HeaderStyle>
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="grid_des_hor_excep" HeaderText="Descripci&#243;n">
                                                                                                        <HeaderStyle Width="50%"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="grid_fecini_hor_excep" HeaderText="Fecha Iniicio">
                                                                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="grid_fecfin_hor_excep" HeaderText="Fecha Final">
                                                                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="grid_estado_hor_excep" HeaderText="Estado">
                                                                                                        <HeaderStyle Width="10%"></HeaderStyle>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:TemplateField HeaderText="Editar">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="img_horexcep_modi" runat="server" ImageUrl="~/Images/iconos/edit_hoja.ico"
                                                                                                                CommandName="HorExcepModi" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"grid_cod_hor_excep") %>' />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Elim." Visible="False">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="img_horexcep_eli" runat="server" ImageUrl="~/Images/iconos/quitar.gif"
                                                                                                                CommandName="HorExcepEli" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"grid_cod_hor_excep") %>' />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 4px">
                                                            <img alt="" src="../Images/Tabs/borabajo.gif" style="width: 100%" /></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <asp:HiddenField ID="hid_id_tllr" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hid_id_HorExcep" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hid_acc_hor_excep" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hid_acc_val_hor_excep" runat="server"></asp:HiddenField>
                                            <!-- INICIO HORARIO EXCEPCIONAL  -->
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tabMapa" Visible="true">
                                        <HeaderTemplate>
                                            <table id="tblHeader6" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                    <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('5');"
                                                        onmouseout="javascript:onTabCabeceraOutForm('5');">mapa
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="980" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borarriba.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="background-color: #ffffff; vertical-align: top; height: 450px">
                                                        <table cellpadding="0" cellspacing="0" width="970" style="background-color: #ffffff; margin-left: 5px; margin-right: 5px;"
                                                            border="0">
                                                            <tr>
                                                                <td class="lineadatos" style="height: 20px" valign="bottom">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdParam" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="width: 965px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 57px; height: 19px">Archivo
                                                                                        </td>
                                                                                        <td style="text-align: left; height: 19px" valign="top" colspan="3" align="center">
                                                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="txtob" onchange="javascript:Fc_SubirImagen();"></asp:FileUpload>&nbsp;&nbsp;Solo se permiten imagenes con extension: (jpg, jpeg,
                                                                                            png, gif)
                                                                                            <asp:ImageButton ID="btn_SubirMapa" OnClick="btn_SubirMapa_Click" runat="server"
                                                                                                ImageUrl="~/Images/iconos/subir.gif" Height="1px" Width="1px" ImageAlign="AbsMiddle"
                                                                                                ToolTip="Subir Mapa"></asp:ImageButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 57px" valign="top">Mapa
                                                                                        </td>
                                                                                        <td colspan="3" style="text-align: left" valign="top">
                                                                                            <div style="border-right: dimgray 1px solid; border-top: dimgray 1px solid; border-left: dimgray 1px solid; width: 430px; border-bottom: dimgray 1px solid; height: 210px">
                                                                                                <asp:Image ID="imgMapa" runat="server" Height="210px" Width="430px" BorderStyle="None" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 57px;" valign="top"></td>
                                                                                        <td valign="top" colspan="3">
                                                                                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="7pt"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btn_SubirMapa" />
                                                                            <asp:AsyncPostBackTrigger ControlID="tabMantMaesTaller" EventName="ActiveTabChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lineadatos">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../Images/Tabs/borabajo.gif" /></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td valign="top"></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- INICIO HORARIO EXCEPCIONAL  -->
    <asp:UpdatePanel ID="upd_pn_popup_horexcepcional" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel3" runat="server" Width="650px" CssClass="modalPopup" Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
                    <tr>
                        <td style="width: 153px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;
                        </td>
                        <td style="width: 220px; background-repeat: repeat; background-image: url(../Images/flotante/popcab2.gif);">&nbsp;<asp:Button ID="btnOpen1" runat="server" Text="Open" Visible="False" />
                            <td style="width: 35px; background-repeat: no-repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;
                            </td>
                    </tr>
                </table>
                <%--BODY--%>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr>
                        <td style="padding-left: 10px; padding-right: 10px">
                            <table style="width: 630px" border="0" cellpadding="0" cellspacing="0">
                                <tr valign="bottom">
                                    <td style="width: 250px">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td class="TabCabeceraOn">AGREGAR HORARIO EXCEPCIONAL
                                                </td>
                                                <td>
                                                    <img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 380px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td align="right">
                                                    <asp:ImageButton ID="btnGrabarHorExcep" runat="server" ToolTip="Grabar Horario" ImageUrl="~/Images/iconos/b-guardar.gif"
                                                        onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'"
                                                        OnClick="btnGrabarHorExcep_Click" />
                                                    <asp:ImageButton ID="btnRetornarHorExcep" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                                        onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'"
                                                        OnClick="btnRetornarHorExcep_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 630px" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table style="width: 630px; vertical-align: top" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Mantenimiento/fbarr.gif" width="630" /></td>
                                            </tr>
                                            <tr>
                                                <td style="background-color: #ffffff; padding-left: 5px; width: 630px; padding-right: 5px; vertical-align: top; padding-bottom: 5px; padding-top: 5px;"
                                                    valign="middle">
                                                    <table border="0" cellpadding="2" cellspacing="1" class="textotab" width="100%">
                                                        <tr>
                                                            <td class="lineadatos" colspan="8">
                                                                <asp:Label ID="Label7" runat="server" SkinID="lblTR" Text=" "></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 13%;">Descripción
                                                            </td>
                                                            <td colspan="7" valign="bottom">
                                                                <asp:TextBox ID="txt_horexcep_des" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Fecha Inicio&nbsp;
                                                            </td>
                                                            <td style="width: 20%">
                                                                <asp:TextBox ID="txt_horexcep_fini" runat="server" Columns="12" MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="btn_horexcep_fini" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                    ImageAlign="AbsMiddle" ToolTip="Calendario" />
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="" DefaultDate=""
                                                                    TargetControlID="txt_horexcep_fini" PopupButtonID="btn_horexcep_fini" />
                                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" Mask="99/99/9999"
                                                                    UserDateFormat="DayMonthYear" ClearMaskOnLostFocus="true" ErrorTooltipEnabled="true"
                                                                    MessageValidatorTip="true" ClearTextOnInvalid="true" TargetControlID="txt_horexcep_fini">
                                                                </cc1:MaskedEditExtender>
                                                                <td style="width: 12%">Fecha Fin&nbsp;
                                                                </td>
                                                                <td style="width: 8%">
                                                                    <asp:TextBox ID="txt_horexcep_ffin" runat="server" Width="70px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_horexcep_ffin"
                                                                        Enabled="True" PopupButtonID="btn_horexcep_ffin" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_horexcep_ffin"
                                                                        Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                        CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder=""
                                                                        CultureTimePlaceholder="" CultureDatePlaceholder="" MaskType="Date" Mask="99/99/9999">
                                                                    </cc1:MaskedEditExtender>
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <asp:ImageButton ID="btn_horexcep_ffin" runat="server" ImageUrl="~/Images/iconos/calendario.gif"></asp:ImageButton>
                                                                </td>
                                                                <td style="width: 7%">Estado&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddl_horexcep_estado" runat="server" Width="110px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="8" style="height: 10px;"></td>
                                                        </tr>
                                                    </table>
                                                    <asp:GridView ID="gv_horexcep_mant" runat="server" SkinID="Grilla" Height="200px"
                                                        Width="620px" AutoGenerateColumns="False" DataKeyNames="grid_cod_dia_hor_excep">
                                                        <Columns>
                                                            <asp:BoundField DataField="grid_cod_hor_excep" HeaderText="Cod. Dia" Visible="False">
                                                                <HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="grid_dia_hor_excep" HeaderText="D&#237;as">
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Horario 1">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_hor1ini" Width="75px" runat="server">
                                                                    </asp:DropDownList>
                                                                    &nbsp;
                                                                    <asp:DropDownList ID="ddl_hor1fin" Width="75px" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horario 2">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_hor2ini" Width="80px" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddl_hor2fin" Width="80px" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horario 3">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_hor3ini" Width="80px" runat="server">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddl_hor3fin" Width="80px" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img alt="" src="../Images/Mantenimiento/fba.gif" width="630" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>
    <cc1:ModalPopupExtender ID="popup_horexcepcional" runat="server" PopupControlID="upd_pn_popup_horexcepcional"
        BackgroundCssClass="modalBackground" TargetControlID="hid_popup_horexcepcional"
        RepositionMode="None" Enabled="True" />
    <asp:HiddenField ID="hid_popup_horexcepcional" runat="server" />
    <!-- FIN HORARIO EXCEPCIONAL  -->
    <!-- modal popup MSGBOX  -->
    <cc1:ModalPopupExtender ID="popup_msgbox_confirm" DropShadow="true" BackgroundCssClass="modalBackground"
        TargetControlID="hid_popupmsboxconfirm" PopupControlID="upd_pn_msbox_confirm"
        runat="server" Enabled="True">
    </cc1:ModalPopupExtender>
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
                                <table cellpadding="5" cellspacing="5" width="280px" align="left">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_mensajebox" runat="server" Text="xxxx"></asp:Label>
                                        </td>
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
    <input id="hid_popupmsboxconfirm" type="hidden" runat="server" />
    <!--  modal popup MSGBOX  -->
    <!-- modal popup MSGBOX ALERTA  -->
    <cc1:ModalPopupExtender ID="popup_alerta_msj" DropShadow="True" BackgroundCssClass="modalBackground"
        TargetControlID="hid_popup_alerta_msj" PopupControlID="upd_pn_popup_alerta_msj"
        runat="server" Enabled="True">
    </cc1:ModalPopupExtender>
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
                                                    <asp:Label ID="lbl_alerta_msj" runat="server" Text="xxxx"></asp:Label>
                                                </td>
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
    <input id="hid_popup_alerta_msj" type="hidden" runat="server" />
    <!-- modal popup MSGBOX ALERTA  -->
    <!-- UpdateProgress -->
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; text-align: center; vertical-align: middle;" id="DIV_PB">
                    <center>
                        <table id="TBL_WAIT" border="0" cellpadding="0" cellspacing="0" style="margin-top: 5px">
                            <tr>
                                <td style="width: 50px">
                                    <asp:Image ID="Image56" runat="server" ImageUrl="~/Images/SRC/Espera.gif"></asp:Image></td>
                                <td style="font-size: 12px; color: dimgray; font-style: normal; font-family: verdana; text-align: center; font-variant: normal">Procesando...</td>
                            </tr>
                        </table>
                    </center>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />

    <asp:HiddenField ID="hidFechaHoy" runat="server" />
    <asp:HiddenField ID="hidPerfil" runat="server" />
    <asp:HiddenField ID="hidAprobar" runat="server" />
    <asp:HiddenField ID="hidRechazar" runat="server" />
    <asp:HiddenField ID="hidImprimir" runat="server" />
    <asp:HiddenField ID="hidUsuarioRed" runat="server" />
    <asp:HiddenField ID="hidEstacionRed" runat="server" />

    <input id="hidCodFotos" type="hidden" />
    <!-- PopPup - Cargando.. -->
    <asp:Panel ID="__PopPanelC__" runat="server" CssClass="modalBackground" Style="left: 0px; top: 0px; display: none; position: fixed; z-index: 100002; display: none;" />
    <asp:Panel ID="__PopupCarga__" runat="server" Style="display: none; border-right: dimgray 2px solid; border-top: dimgray 2px solid; border-left: dimgray 2px solid; width: 150px; border-bottom: dimgray 2px solid; position: fixed; z-index: 100003; height: 40px; text-align: center; padding-right: 7px; padding-left: 7px; padding-bottom: 7px; vertical-align: middle; padding-top: 7px; background-color: White;">
        <table width="100%">
            <tr>
                <td style="width: 50px">
                    <asp:Image ID="imgcarga03" runat="server" ImageUrl="~/Images/SRC/Espera.gif"></asp:Image>
                </td>
                <td style="font-size: 12px; color: dimgray; font-style: normal; font-family: verdana; text-align: center; font-variant: normal">Procesando...
                </td>
            </tr>
        </table>
    </asp:Panel>

    <script type="text/javascript">

        function CallWebMethodAGP(methodName, paramterData, callbackFunctionWithArgs) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "SRC_Maestro_Detalle_Talleres.aspx/" + methodName + "", //Metodo
                data: JSON.stringify({ filtro: paramterData }), //Envio Parametros al Metodo
                success: function (d) { callbackFunctionWithArgs(d.d); }, //Resultado
                error: function (xmlHttpError, err, desc) { /*alert('ERROR :' + xmlHttpError.responseText); */ } //Mostrar Error

            });
        }

        function CargarInicial() {
            var Nid_usuario = "<%=this.Profile.Usuario.Nid_usuario %>";
            var nid_taller = $("#<%=hid_id_tllr.ClientID%>").val();
        }

        function Fc_SetCambiartab(index) {

            document.getElementById('<%=this.btnNuevo.ClientID%>').style.display = (index == 5) ? 'inline' : 'none';
            document.getElementById('<%=this.btnBuscar.ClientID%>').style.display = (index == 5) ? 'inline' : 'none';

            return false;
        }


        function fn_fecha_format(ddMMyyyy, caracter) {
            fecha = ddMMyyyy.split(caracter);
            fecha = fecha[2] + fecha[1] + fecha[0];
            return fecha;
        }

        function fn_fecha_compara(fecha1, fecha2) {
            fecha1 = fn_fecha_format(fecha1, "/");
            fecha2 = fn_fecha_format(fecha2, "/");
            int_01 = parseInt(fecha1);
            int_02 = parseInt(fecha2);
            return (int_01 <= int_02);
        }

        //------------------
        function fc_showError(error) {
            return false;
        }


        function MostrarCarga(opc) {

            var estilo = (opc == '1') ? 'block' : 'none';

            document.getElementById('<%=__PopupCarga__.ClientID%>').style.left = (((screen.width - 350) / 2) + 60) + 'px';
            document.getElementById('<%=__PopupCarga__.ClientID%>').style.top = (screen.height - 150) / 2 + 'px';
            document.getElementById('<%=__PopPanelC__.ClientID%>').style.width = screen.width + 'px';
            document.getElementById('<%=__PopPanelC__.ClientID%>').style.height = screen.height + 'px';
            document.getElementById('<%=__PopPanelC__.ClientID%>').style.display = estilo;
            document.getElementById('<%=__PopupCarga__.ClientID%>').style.display = estilo;
        }

        function fc_CambiaTabCargaMasiva(indice) {
            document.getElementById("tabPopup0").style.display = "none";
            document.getElementById("tabPopup1").style.display = "none";

            setTabCabeceraOffTabToTab("001");
            setTabCabeceraOffTabToTab("002");

            strClassNameOffTabToTab = "";
            strImgIzqOffTabToTab = "";
            strImgDerOffTabToTab = "";

            if (indice == 001) {
                document.getElementById("tabPopup0").style.display = "inline";
                setTabCabeceraOnTabToTab("001");
            }
            else if (indice == 002) {
                document.getElementById("tabPopup1").style.display = "inline";
                setTabCabeceraOnTabToTab("002");
            }
        }

        CargarInicial();


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
