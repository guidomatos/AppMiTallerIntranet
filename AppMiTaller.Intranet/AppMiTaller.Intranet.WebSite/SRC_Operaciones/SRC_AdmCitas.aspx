<%@ Page Language="C#"
    MasterPageFile="~/Principal.master"
    AutoEventWireup="true"
    CodeFile="SRC_AdmCitas.aspx.cs"
    Inherits="SRC_Operaciones_SRC_AdmCitas"
    Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

        .overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #ffffff;
            filter: alpha(opacity=0.7);
            opacity: 0.5;
        }

        .overlayContent {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #ffffff;
            }

            .overlayContent img {
                width: 100px;
                height: 100px;
            }
    </style>

    <script type="text/javascript">

        var ModalProgress ='<%= ModalProgress.ClientID %>';

        //Código JavaScript incluido en un archivo denominado jsUpdateProgress.js 
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function beginReq(sender, args) {
            $find(ModalProgress).show();
        }
        function endReq(sender, args) {
            $find(ModalProgress).hide();
        }

        //-----------------------------------------------------------------------------

        var printWindow;
        function FC_PopupImprimir(strHTML) {
            printWindow = window.open("", "mywindow", "location=0,status=0,scrollbars=1,resizable=1,width=900px,height=700px");
            var strContent = "<html><body>";
            strContent = strContent + "<title>Listado de Citas</title>";
            strContent = strContent + "<script type='text/javascript' language='Javascript'>"
            strContent = strContent + "function imprimir() {window.print();}" //Print
            strContent = strContent + "</" + "script>"
            strContent = strContent + "</head><body>";
            strContent = strContent + strHTML;
            strContent = strContent + "</body>";
            strContent = strContent + "<script language='javascript' type='text/javascript'> imprimir(); </" + "script>";
            strContent = strContent + "</html>";

            printWindow.document.write(strContent);
            printWindow.document.close();
            printWindow.focus();
        }

        function fc_Limpiar() {

        }


        function llama1() {
            var ddlMarca = document.getElementById('<%=btn_Hidden_1.ClientID%>'); ddlMarca.click();
        }
        function llama2() {
            var btnHorario2 = document.getElementById('<%=btn_Hidden_2.ClientID%>'); btnHorario2.click();
        }

        function Fc_BuscarCita() {
            var hidindvalidador = document.getElementById('<%=hid_indvalidador.ClientID%>');
            var v_ddl_bus_tipodoc = document.getElementById('<%=ddl_bus_tipodoc.ClientID%>');
            var v_txt_bus_nrodoc = document.getElementById('<%=txt_bus_nrodoc.ClientID%>');
            var strDoc = v_ddl_bus_tipodoc.options[v_ddl_bus_tipodoc.selectedIndex].text;

            hidindvalidador.value = "0";
            if (strDoc != "--TODOS--") {
                if (strDoc == "DNI") {
                    if (parseInt(v_txt_bus_nrodoc.value.length) > 0) {
                        if (parseInt(v_txt_bus_nrodoc.value.length) < 8) {
                            hidindvalidador.value = "1";
                            alert("Debe Ingresar un DNI valido.(8 caracteres)");
                            return false;
                        }
                    }
                }
                else {
                    if (strDoc == "RUC") {
                        if (parseInt(v_txt_bus_nrodoc.value.length) > 0) {
                            if (parseInt(v_txt_bus_nrodoc.value.length) < 11) {
                                hidindvalidador.value = "1";
                                alert("Debe Ingresar un RUC valido.(11 caracteres)");
                                return false;
                            }
                        }
                    }
                }
            }


            //------------------------------------------------        
            // Valida Hora   
            //------------------------------------------------

        }

        var ventana
        var cont = 0
        var titulopordefecto = "Defecto"

        function foto(mapa, titulo) {
            if (cont == 1) { ventana.close(); ventana = null }
            if (titulo == null) { titulo = titulopordefecto }
            ventana = window.open('', 'ventana', 'toolbar=no,status=no,location=no,directories=0,menubar=no,scrollbars=no,resizable=0,width=50%,height=50%')
            ventana.document.write('<html><head><title>' + titulo + '</title></head><body style="overflow:hidden" marginwidth="0" marginheight="0" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" scroll="no" onUnload="opener.cont=0"><img src="' + mapa + '" onLoad="opener.redimensionar(this.width, this.height)">')
            ventana.document.close()
            cont++
        }
        function redimensionar(ancho, alto) {
            ventana.resizeTo(ancho + 12, alto + 28)
            ventana.moveTo((screen.width - ancho) / 2, (screen.height - alto) / 2)
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
                return false;//anula la entrada de texto. 
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
                return false;//anula la entrada de texto. 
            }
        }


        function validarSelGridView(tipo, oGridView) {
            var grid = document.getElementById(oGridView);
            var cell;
            var count = 0;
            var countNO = 0;

            var strAnulada = '';

            if (grid == null) {
                alert('No hay registro(s).');
                return false;
            }
            else {
                //alert(grid.rows.length);

                if (grid.rows.length - 2 > 0) {

                    if (tipo == '7') {
                        return true; //Impresion de todos
                    }

                    if (tipo == '1') {
                        if (document.getElementById("<%=hf_DETALLE.ClientID%>").value == '') {
                            alert(SRC_SeleccioneUno);
                            return false;
                        }
                        else {
                            return true;
                        }
                    }

                    for (i = 1; i < grid.rows.length - 1; i++) {
                        cell = grid.rows[i].cells[0];
                        for (j = 0; j < cell.childNodes.length; j++) {
                            if (cell.childNodes[j].type == "checkbox") {
                                if (cell.childNodes[j].checked == true) {
                                    count++;

                                    //'innerHTML' en vez de 'innerText'
                                    if (grid.rows[i].cells[13].innerHTML.toUpperCase() == "NO") {
                                        if (tipo == '6') {
                                            alert(SRC_SoloActualizarSI); return false;
                                        }
                                    }

                                    if (tipo == '4')  //validar los estados de la cita para su anulacion 
                                    {
                                        var estado = fc_Trim(grid.rows[i].cells[5].innerHTML.toUpperCase());
                                        //alert('-' + estado + '-');                                  
                                        if (estado == 'ANULADA' || estado == 'ATENDIDA' || estado == 'NO ASISTIÓ') {
                                            strAnulada = 'No se puede Anular la cita, debido a que se encuentra en Estado ' + estado
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //------------------
                    //Tipos
                    //------------------
                    //VerDetalle    1
                    //Confirmar     2
                    //Reprogramar   3
                    //Anular        4
                    //Reasignar     5
                    //Actualizar E. 6

                    //validaciones
                    //alert(count);
                    if (count == 0) { alert(((tipo == '5') || (tipo == '6')) ? SRC_SeleccioneAlmenosUno : SRC_SeleccioneUno); return false; }
                    else if (count > 1) {
                        if (tipo == '1') { alert(SRC_SeleccioneUno); return false; }
                        else if (tipo == '2') { alert(SRC_SoloConfirmarUno); return false; }
                        else if (tipo == '3') { alert(SRC_SoloReprogramarUno); return false; }
                        else if (tipo == '4') { alert(SRC_SoloAnularUno); return false; }
                        else if (tipo == '5') { return true; }
                        else if (tipo == '6') { return true; }
                    }
                    else if (count == 1) {
                        if (tipo == '4')  //validar los estados de la cita para su anulacion 
                        {
                            if (strAnulada != '') {
                                alert(strAnulada); return false;
                            }
                            else {
                                return true;//confirm('¿ Esta seguro de Anular la Cita ?.');
                            }
                        }
                        else
                            return true;
                    }
                }
                else {
                    alert('No hay registro(s).');
                    return false;
                }
            }

            return;
        }

        function Fc_verDetalleCita() {
            var btn = document.getElementById('<%=btnVerDetalle.ClientID%>');
            btn.click();
        }



    </script>

    <asp:UpdatePanel ID="upd_GENERAL" runat="server">
        <ContentTemplate>
            <table cellspacing="0" cellpadding="2" width="1000px" border="0">
                <tbody>
                    <tr>
                        <!--INICIO ICONOS DE ACCCION -->
                        <td valign="top">
                            <table style="height: 37px" class="TablaIconosMovimientos" border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%" valign="middle" align="right">
                                            <asp:ImageButton ID="btnBuscarCita" onmouseover="javascript:this.src='../Images/iconos/b-buscar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-buscar.gif'" OnClick="btnBuscarCita_Click"
                                                runat="server" OnClientClick="Fc_BuscarCita()" ImageUrl="~/Images/iconos/b-buscar.gif"
                                                ToolTip="Buscar"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnVerDetalle" onmouseover="javascript:this.src='../Images/iconos/b-verobs2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-verobs.gif'" OnClick="btnVerDetalle_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-verobs.gif" ToolTip="Ver Detalle"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnActualizarEstado" onmouseover="javascript:this.src='../Images/iconos/b-confirmar_aprob2.png'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-confirmar_aprob.png'" OnClick="btnActualizarEstado_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-confirmar_aprob2.png" ToolTip="Actualizar Estado de Verificación"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnAnularCita" onmouseover="javascript:this.src='../Images/iconos/b-anular_reserva2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-anular_reserva.gif'" OnClick="btnAnularCita_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-anular_reserva.gif" ToolTip="Anular Cita"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnConfirmarCita" onmouseover="javascript:this.src='../Images/iconos/b-registroterminado2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-registroterminado.gif'" OnClick="btnConfirmarCita_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-registroterminado.gif" ToolTip="Confirmar Cita"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnReprogramarCita" onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'" OnClick="btnReprogramarCita_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-registrofecha.gif" ToolTip="Reprogramar Cita"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btnReasignarCita" onmouseover="javascript:this.src='../Images/iconos/b-reasignarvin2.png'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-reasignarvin1.png'" OnClick="btnReasignarCita_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-reasignarvin1.png" ToolTip="Reasignar Cita"></asp:ImageButton>
                                        </td>
                                        <td style="width: 100%" align="right">
                                            <asp:ImageButton ID="btn_bus_ActDatosVehPropie" onmouseover="javascript:this.src='../Images/iconos/b-revertir2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-revertir.gif'" OnClick="btnActualizarVehiculo" runat="server" ImageUrl="~/Images/iconos/b-revertir.gif" ToolTip="Actualizar Datos Vehiculo/Propietario" ></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnLimpiar" onmouseover="javascript:this.src='../Images/iconos/b-limpiar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-limpiar.gif'" runat="server"
                                                ImageUrl="~/Images/iconos/b-limpiar.gif" ToolTip="Limpiar" OnClick="btnLimpiar_Click"
                                                OnClientClick="javascript: return fc_Limpiar();"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/iconos/b-imprimir.gif"
                                                ToolTip="Imprimir " OnClick="btnImprimir_Click" onmouseover="javascript:this.src='../Images/iconos/b-imprimir2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-imprimir.gif'" Visible="false" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <!--FIN ICONOS DE ACCCION -->
                    </tr>
                    <tr>
                        <td valign="top">
                            <cc1:TabContainer ID="tabAdminCitas" runat="server" CssClass="" ActiveTabIndex="0">
                                <cc1:TabPanel runat="server" ID="tabAdmCitas">
                                    <HeaderTemplate>
                                        <!-- TITULO DEL TAB-->
                                        <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <img id="imgTabIzq" alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                <td class="TabCabeceraOn">Listado de Citas</td>
                                                <!-- AGREGAR TITULO DEL TAB-->
                                                <td>
                                                    <img id="imgTabDer" alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div class="DivCuerpoTab" style="height: 475px">
                                            <table cellspacing="0" cellpadding="0" width="980" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borarriba.gif" width="100%" /></td>
                                                    </tr>
                                                    <tr style="height: 450px">
                                                        <td style="vertical-align: top; height: 450px; background-color: #ffffff" align="left">
                                                            <table style="margin-left: 5px; margin-right: 5px" cellspacing="0" cellpadding="0"
                                                                width="950" border="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 14px" id="td_Criterio_Busqueda">
                                                                            <span>
                                                                                <asp:Label ID="lbl" runat="server" SkinID="lblCB">CRITERIOS DE BÚSQUEDA</asp:Label></span></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="bottom">
                                                                            <table style="width: 100%" class="cbusqueda" cellspacing="0" cellpadding="1" border="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 9%">Código Reserva</td>
                                                                                        <td style="width: 18%">
                                                                                            <asp:TextBox ID="txt_bus_codreserva" runat="server" Width="165px"></asp:TextBox></td>
                                                                                        <td style="width: 11%">Asesor de Servicio</td>
                                                                                        <td style="width: 21%">
                                                                                            <asp:TextBox ID="txt_bus_asesorservicio" runat="server" Width="180px"></asp:TextBox></td>
                                                                                        <td style="width: 6%">
                                                                                            <asp:Label ID="lblTextoPlaca" runat="server" Text="Placa"></asp:Label></td>
                                                                                        <td style="width: 13%">
                                                                                            <asp:TextBox ID="txt_bus_placapatente" runat="server" Width="100px" Style="text-transform: uppercase;"></asp:TextBox></td>
                                                                                        <td style="width: 10%">Tipo Documento</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_tipodoc" runat="server" AutoPostBack="True" Width="110px"
                                                                                                OnSelectedIndexChanged="ddl_bus_tipodoc_SelectedIndexChanged">
                                                                                            </asp:DropDownList></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label5" runat="server" Text="Departamento"></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_departamento" runat="server" AutoPostBack="True" Width="165px"
                                                                                                OnSelectedIndexChanged="ddl_bus_departamento_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>Estado Reserva</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_estreserva" runat="server" Width="180px"></asp:DropDownList></td>
                                                                                        <td>Marca</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_marca" runat="server" AutoPostBack="True" Width="120px"
                                                                                                OnSelectedIndexChanged="ddl_bus_marca_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>Nro. Documento</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txt_bus_nrodoc" runat="server" Width="105px" Enabled="false"></asp:TextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label9" runat="server" Text="Provincia"></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_provincia" runat="server" AutoPostBack="True" Width="165px"
                                                                                                OnSelectedIndexChanged="ddl_bus_provincia_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>Ind. Pendiente</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="cboIndPendiente" runat="server"></asp:DropDownList></td>
                                                                                        <td>Modelo</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_modvehiculo" runat="server" Width="120px"></asp:DropDownList></td>
                                                                                        <td colspan="2">Nombre(s)<asp:TextBox ID="txt_bus_nombres" runat="server" Width="147px"></asp:TextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label13" runat="server" Text="Distrito"></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_distrito" runat="server" AutoPostBack="True" Width="165px"
                                                                                                OnSelectedIndexChanged="ddl_bus_distrito_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>Fecha Cita</td>
                                                                                        <td>
                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="middle" align="left">
                                                                                                            <asp:TextBox ID="txt_bus_feccita1" runat="server" Width="70px"></asp:TextBox>
                                                                                                            <asp:ImageButton ID="btn_Calendario3" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                                ImageAlign="AbsMiddle"></asp:ImageButton>&nbsp;
                                                                                                                    <asp:TextBox ID="txt_bus_feccita2" runat="server" Width="70px"></asp:TextBox>
                                                                                                            <asp:ImageButton ID="btn_Calendario4" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                                ImageAlign="AbsMiddle"></asp:ImageButton></td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txt_bus_feccita2" Mask="99/99/9999" MaskType="Date" PromptCharacter="_"></cc1:MaskedEditExtender>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_bus_feccita1" PopupButtonID="btn_Calendario3" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_bus_feccita2" PopupButtonID="btn_Calendario4" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_bus_feccita1" Mask="99/99/9999" MaskType="Date" PromptCharacter="_"></cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td>Hora Cita</td>
                                                                                        <td>
                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="middle" align="center">
                                                                                                            <asp:TextBox ID="txt_bus_hr1" runat="server" Width="40px"></asp:TextBox>
                                                                                                            -
                                                                                                                    <asp:TextBox ID="txt_bus_hr2" runat="server" Width="40px"></asp:TextBox>
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txt_bus_hr1"
                                                                                                Mask="99:99" MaskType="Time" PromptCharacter="_">
                                                                                            </cc1:MaskedEditExtender>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txt_bus_hr2"
                                                                                                Mask="99:99" MaskType="Time" PromptCharacter="_">
                                                                                            </cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td colspan="2">Apellido(s)<asp:TextBox ID="txt_bus_apellidos" runat="server" Width="147px"></asp:TextBox></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblTextoLocal" runat="server" Text="Punto Red"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_puntored" runat="server" AutoPostBack="True" Width="165px"
                                                                                                OnSelectedIndexChanged="ddl_bus_puntored_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>Fecha Registro</td>
                                                                                        <td>
                                                                                            <table cellspacing="0" cellpadding="0" border="0">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td valign="middle">
                                                                                                            <asp:TextBox ID="txt_bus_fecreg1" runat="server" Width="70px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                                ID="btn_Calendario1" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                                ImageAlign="AbsMiddle"></asp:ImageButton>&nbsp;
                                                                                                                    <asp:TextBox ID="txt_bus_fecreg2" runat="server" Width="70px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                                        ID="btn_Calendario2" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                                        ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                                            <!-- add Regular Expresion Validator -->
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
                                                                                            </table>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_bus_fecreg1"
                                                                                                PopupButtonID="btn_Calendario1" Format="dd/MM/yyyy">
                                                                                            </cc1:CalendarExtender>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_bus_fecreg1"
                                                                                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_">
                                                                                            </cc1:MaskedEditExtender>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_bus_fecreg2"
                                                                                                PopupButtonID="btn_Calendario2" Format="dd/MM/yyyy">
                                                                                            </cc1:CalendarExtender>
                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_bus_fecreg2"
                                                                                                Mask="99/99/9999" MaskType="Date" PromptCharacter="_">
                                                                                            </cc1:MaskedEditExtender>
                                                                                        </td>
                                                                                        <td style="text-align: right" colspan="2">
                                                                                            <asp:HiddenField ID="hid_indvalidador" runat="server"></asp:HiddenField>
                                                                                            <td>
                                                                                                <asp:Label ID="Label22" runat="server"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label23" runat="server"></asp:Label></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblTextoTaller" runat="server" Text="Taller"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddl_bus_taller" runat="server" Width="165px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td></td>
                                                                                        <td style="width: 230px; text-align: right" colspan="2"></td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label27" runat="server"></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label28" runat="server"></asp:Label></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <div id="upNotaPedido">
                                                                                <table cellspacing="0" cellpadding="0" width="966" id="UNDER">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <img style="border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; width: 970px; border-right-width: 0px"
                                                                                                    id="Image43" src="../Images/iconos/fbusqueda.gif" /></td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                            <div style="overflow: auto; width: 965px; height: 300px">
                                                                                <asp:UpdatePanel ID="upAdminCitasDet" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:GridView ID="gv_admcitas" runat="server" SkinID="Grilla" Width="230%" OnSorting="gv_admcitas_Sorting"
                                                                                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="grid_nid_cita,grid_nid_estado,grid_IndPendiente,grid_no_dias_validos,grid_nid_servicioCita,grid_nid_modelo"
                                                                                            AllowPaging="True" OnPageIndexChanging="gv_admcitas_PageIndexChanging" OnRowDataBound="gv_admcitas_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chk_opcion" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="grid_Taller" HeaderText="Taller" SortExpression="grid_Taller">
                                                                                                    <HeaderStyle Width="7%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_FE_HORA_REG" HeaderText="Fec. - Hora Registrada" SortExpression="grid_FE_HORA_REG">
                                                                                                    <HeaderStyle Width="5%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_FECHA_CITA" HeaderText="Fecha Cita" SortExpression="grid_FECHA_CITA">
                                                                                                    <HeaderStyle Width="3%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_HORA_CITA" HeaderText="Hora Cita" SortExpression="grid_HORA_CITA">
                                                                                                    <HeaderStyle Width="3%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_ESTADO_CITA" HeaderText="Estado Reserva" SortExpression="grid_ESTADO_CITA">
                                                                                                    <HeaderStyle Width="5%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_PlacaPatente" HeaderText="Placa" SortExpression="grid_PlacaPatente">
                                                                                                    <HeaderStyle Width="3%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_AsesorServicio" HeaderText="Asesor de Servicio" SortExpression="grid_AsesorServicio">
                                                                                                    <HeaderStyle Width="8%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_NomCliente" HeaderText="Nombre(s)" SortExpression="grid_NomCliente">
                                                                                                    <HeaderStyle Width="8%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_ApeCliente" HeaderText="Apellido(s)" SortExpression="grid_ApeCliente">
                                                                                                    <HeaderStyle Width="8%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_NumDocumento" HeaderText="Num. Documento" SortExpression="grid_NumDocumento">
                                                                                                    <HeaderStyle Width="5%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_co_usuario_crea" HeaderText="Usuario Creaci&#243;n"
                                                                                                    SortExpression="grid_co_usuario_crea">
                                                                                                    <HeaderStyle Width="3.5%" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_co_usuario_modi" HeaderText="Usuario Modificado"
                                                                                                    SortExpression="grid_co_usuario_modi">
                                                                                                    <HeaderStyle Width="3.5%" />
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_IndPendiente" HeaderText="Pendiente Datos" SortExpression="grid_IndPendiente">
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                    <HeaderStyle Width="3%" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_Departamento" HeaderText="Departamento" SortExpression="grid_Departamento">
                                                                                                    <HeaderStyle Width="6%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_Provincia" HeaderText="Provincia" SortExpression="grid_Provincia">
                                                                                                    <HeaderStyle Width="6%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_Distrito" HeaderText="Distrito" SortExpression="grid_Distrito">
                                                                                                    <HeaderStyle Width="7%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_Punto_RED" HeaderText="Local" SortExpression="grid_Punto_RED">
                                                                                                    <HeaderStyle Width="8%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_cod_reserva_cita" HeaderText="C&#243;digo Reserva" SortExpression="grid_cod_reserva_cita">
                                                                                                    <HeaderStyle Width="7%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="grid_TelefonoCliente" HeaderText="Télefono Contacto" SortExpression="grid_TelefonoCliente">
                                                                                                    <HeaderStyle Width="4%"></HeaderStyle>
                                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>
                                                                            <asp:HiddenField ID="hf_ROW_INDEX" runat="server" />
                                                                            <asp:HiddenField ID="hf_FECHA_HABIL" runat="server" />
                                                                            <asp:HiddenField ID="hf_INTERVALO_TALLER" runat="server" />
                                                                            <asp:HiddenField ID="hf_DATOS_TALLER" runat="server" />
                                                                            <asp:HiddenField ID="hf_HORAS_VACIAS" runat="server" />
                                                                            <asp:HiddenField ID="hf_QUICK_SERVICE" runat="server" />
                                                                            <asp:HiddenField ID="hf_DIAS_SERVICIO" runat="server" />
                                                                            <asp:HiddenField ID="hf_ID_TALLER" runat="server" />
                                                                            <asp:HiddenField ID="hf_ESTADO_CITA" runat="server" />
                                                                            <asp:HiddenField ID="hf_ID_CITA" runat="server" />
                                                                            <asp:HiddenField ID="hf_EmailCallCenter" runat="server" />
                                                                            <asp:HiddenField ID="hf_STATUS_WS" Value="0" runat="server" />
                                                                            <asp:HiddenField ID="hf_DETALLE" runat="server" />
                                                                            <asp:TextBox ID="hf_DATOS_CITA" runat="server" Visible="False"></asp:TextBox></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/borabajo.gif" width="100%" /></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>

                                        <br />
                                        <asp:HiddenField ID="hfEmailsCliente" runat="server" />
                                        <br />
                                        <cc1:ModalPopupExtender ID="popup_msgbox_confirm" DropShadow="True" BackgroundCssClass="modalBackground"
                                            TargetControlID="hid_popupmsboxconfirm" PopupControlID="upd_pn_msbox_confirm"
                                            runat="server" DynamicServicePath="" Enabled="True">
                                        </cc1:ModalPopupExtender>
                                        <input id="hid_popupmsboxconfirm" type="hidden" runat="server" />
                                        <asp:UpdatePanel ID="upd_pn_msbox_confirm" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="div_upd_msgbox_confirm2" Width="297px" runat="server" Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
                                                        <tr>
                                                            <td style="width: 245px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;</td>
                                                            <td style="width: 52px; background-repeat: repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <table cellpadding="2" cellspacing="2" width="286px" style="vertical-align: middle; background-color: #FFFFFF;"
                                                        align="center">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Panel ID="Panel3" runat="server">
                                                                    <table cellpadding="0" cellspacing="0" align="left" style="width: 270px">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Label ID="lbl_mensajebox" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="#464646"
                                                                                    Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 20px;"></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 35px;" align="center">
                                                                                <asp:Button ID="btn_msgboxconfir_si" CssClass="btn" runat="server" Text="SI" Font-Bold="True"
                                                                                    Font-Size="10pt" Height="30px" OnClick="btn_msgboxconfir_si_Click" Width="35px" />
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_msgboxconfir_no" CssClass="btn" runat="server" Text="NO" Font-Bold="True"
                                                                                    Font-Size="10pt" Height="30px" OnClick="btn_msgboxconfir_no_Click" Width="35px" /><asp:Button
                                                                                        ID="btn_msgboxconfir_aceptar" CssClass="btn" runat="server" Text="ACEPTAR" Font-Bold="True"
                                                                                        Font-Size="10pt" Height="30px" OnClick="btn_msgboxconfir_aceptar_Click" Width="90px" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <!-- add controls for cola de espera -->
                                        <asp:UpdatePanel ID="PanelColaEspera" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel1" runat="server" Width="850px" CssClass="modalPopup" Style="background-repeat: repeat; background-image: url(../Images/fondo.gif); padding-top: 0px; padding-bottom: 8px">
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 44px;">
                                                        <tr>
                                                            <td style="width: 168px; background-repeat: repeat; background-image: url(../Images/flotante/popcab1.gif);">&nbsp;</td>
                                                            <td style="width: 380px; background-repeat: repeat; background-image: url(../Images/flotante/popcab2.gif);">&nbsp;</td>
                                                            <td style="width: 35px; background-repeat: no-repeat; background-image: url(../Images/flotante/popcab3.gif);">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 850px">
                                                        <tr>
                                                            <td style="padding-left: 10px; padding-right: 10px">
                                                                <cc1:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" CssClass=""
                                                                    Width="850px" BackColor="White">
                                                                    <cc1:TabPanel ID="TabPanel2" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 830px">
                                                                                <tr valign="bottom">
                                                                                    <td style="width: 160px" valign="bottom">
                                                                                        <table id="Table7" runat="server" cellpadding="0" cellspacing="0" border="0" style="height: 20px; width: 96%;">
                                                                                            <tr id="Tr1" runat="server">
                                                                                                <td id="Td1" runat="server">
                                                                                                    <img alt="" src="../Images/Tabs/tab-izq.gif" /></td>
                                                                                                <td id="Td2" class="TabCabeceraOn" style="width: 154px" runat="server">CLIENTES EN COLA DE ESPERA</td>
                                                                                                <td id="Td3" style="width: 5px" runat="server">
                                                                                                    <img alt="" src="../Images/Tabs/tab-der.gif" /></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <table cellpadding="0" cellspacing="0" border="0" style="margin-right: 5px">
                                                                                            <tr>
                                                                                                <td align="right">
                                                                                                    <asp:ImageButton ID="btnRegresarColaEspera" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                                                                                        onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'"
                                                                                                        OnClick="btnRegresarColaEspera_Click" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ContentTemplate>
                                                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 830px; background-color: white;">
                                                                                <tr>
                                                                                    <!-- Cabecera -->
                                                                                    <td>
                                                                                        <img alt="" src="../Images/Mantenimiento/fbarr.gif" width="830" /></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <!-- Cuerpo -->

                                                                                        <br />

                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>

                                                                                                <asp:GridView ID="gv_Cola_Espera" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                                                    DataKeyNames="NID_CITA" SkinID="Grilla" OnPageIndexChanging="gv_Cola_Espera_PageIndexChanging"
                                                                                                    OnSelectedIndexChanged="gv_Cola_Espera_SelectedIndexChanged" Width="810px">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="nu_placa" HeaderText="Placa">
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="fe_prog" HeaderText="Fec. Registro" DataFormatString="{0:d}">
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="fe_prog" HeaderText="Hora Llegada" DataFormatString="{0:t}">
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="7%" />
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_ape_paterno" HeaderText="Apellido(s) Contacto">
                                                                                                            <HeaderStyle Width="17%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre(s) Contacto">
                                                                                                            <HeaderStyle Width="17%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="nu_telefono" HeaderText="Telefono">
                                                                                                            <HeaderStyle Width="8%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_correo" HeaderText="Email Personal">
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_correo_trabajo" HeaderText="Email Trabajo">
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="no_correo_alter" HeaderText="Email Alternativo">
                                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:CommandField ButtonType="Image" HeaderText="Seleccionar" SelectImageUrl="~/Images/SRC/si.PNG"
                                                                                                            ShowCancelButton="False" ShowSelectButton="True">
                                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                                        </asp:CommandField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>

                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:PostBackTrigger ControlID="gv_Cola_Espera" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>

                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" style="height: 20px"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <!-- Pie -->
                                                                                    <td style="width: 618px">
                                                                                        <img alt="" src="../Images/Mantenimiento/fba.gif" width="830" /></td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </cc1:TabPanel>
                                                                </cc1:TabContainer>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        <!-- Modal Popup -->
                                        <cc1:ModalPopupExtender ID="mpColaEspera" runat="server" PopupControlID="PanelColaEspera"
                                            BackgroundCssClass="modalBackground" TargetControlID="hfColaEspera" RepositionMode="None"
                                            Enabled="True" DynamicServicePath="">
                                        </cc1:ModalPopupExtender>
                                        <!-- Hidden -->
                                        <asp:HiddenField ID="hfColaEspera" runat="server"></asp:HiddenField>
                                        <!-- modal Popup-->


                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>




    <asp:UpdatePanel ID="UdPnlReprogramacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel Style="background-image: url(../Images/fondo.gif); padding-bottom: 8px; padding-top: 0px; background-repeat: repeat"
                ID="Panel2" runat="server" CssClass="modalPopup"
                Width="840px">
                <table style="width: 848px; height: 44px" id="ID23" cellspacing="0" cellpadding="0"
                    border="0">
                    <tbody>
                        <tr>
                            <td style="background-image: url(../Images/SRC/LINE.PNG); background-repeat: no-repeat; height: 34px; text-align: center"
                                colspan="3">&nbsp; &nbsp;</td>
                        </tr>
                    </tbody>
                </table>
                <table style="width: 897px" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; width: 858px">
                                <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="" Width="820px" ActiveTabIndex="0">
                                    <cc1:TabPanel runat="server" ID="TabPanel1">
                                        <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 809px">
                                                <tr valign="bottom">
                                                    <td style="width: 196px" valign="bottom">
                                                        <table id="Table3" runat="server" cellpadding="0" cellspacing="0" border="0" style="height: 17px;">
                                                            <tr id="Tr2" runat="server">
                                                                <td id="Td4" runat="server" style="height: 20px">
                                                                    <img alt="" src="../Images/Tabs/tab-izq.gif" height="30" /></td>
                                                                <td id="Td5" class="TabCabeceraOn" style="width: 220px; height: 20px;" runat="server">REPROGRAMACIÓN DE CITA</td>
                                                                <td id="Td6" style="width: 5px; height: 20px;" runat="server">
                                                                    <img alt="" src="../Images/Tabs/tab-der.gif" height="30" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right">
                                                        <table cellpadding="0" cellspacing="0" border="0" style="margin-right: 5px">
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:ImageButton ID="btnRegresarPanelreprog" runat="server" ToolTip="Regresar" ImageUrl="~/Images/iconos/b-regresar.gif"
                                                                        onmouseover="javascript:this.src='../Images/iconos/b-regresar2.gif'" onmouseout="javascript:this.src='../Images/iconos/b-regresar.gif'"
                                                                        OnClick="btnRegresarPanelreprog_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table id="Table4" cellspacing="0" cellpadding="0" border="0" backcolor="white">
                                                <tbody>
                                                    <tr>
                                                        <!-- Cabecera -->
                                                        <td style="width: 824px">
                                                            <img style="width: 100%" alt="" src="../Images/Mantenimiento/fbarr.gif" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: white" align="center" valign="middle">
                                                            <table id="Table2" width="800" border="0" cellpadding="1" cellspacing="0" class="cuerponuevo"
                                                                summary="cbusqueda">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="padding-left: 5px; width: 100px; height: 21px;" align="left">
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Tipo Servicio" Width="100px"
                                                                                Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="2" style="height: 21px">
                                                                            <asp:Label ID="lblGridTipoServicio" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px; height: 21px;" align="left">
                                                                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Servicio" Width="90px"
                                                                                Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="2" style="height: 21px">
                                                                            <asp:Label ID="lblGridServicio" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left: 5px; width: 100px" align="left">
                                                                            <asp:Label ID="lblTextoGridPlaca" runat="server" Font-Bold="True" Text="Patente"
                                                                                Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:Label ID="lblGridPlaca" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:Label ID="Label33" runat="server" Font-Bold="True" Text="Marca" Width="70px"
                                                                                Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:Label ID="lblGridMarca" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:Label ID="Label35" runat="server" Font-Bold="True" Text="Modelo" Width="70px"
                                                                                Font-Names="Arial" Font-Size="10pt"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 100px" align="left">
                                                                            <asp:Label ID="lblGridModelo" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!-- tabla 31 -->
                                            <center>
                                                <asp:Panel ID="PanelUno" runat="server" BackColor="White">
                                                    <table style="background-color: white; text-align: center" id="Table5" cellspacing="0"
                                                        cellpadding="0" border="0" backcolor="white">
                                                        <tbody>
                                                            <tr>
                                                                <td style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; width: 100%; clip: rect(auto auto auto auto); padding-top: 0px">
                                                                    <table class="textotab" cellspacing="1" cellpadding="2" width="800" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style="border-right: dimgray 1px solid; padding-right: 1px; border-top: dimgray 1px solid; padding-left: 1px; padding-bottom: 1px; border-left: dimgray 1px solid; padding-top: 1px; border-bottom: dimgray 1px solid; height: 28px; background-color: #005cab"
                                                                                    align="center"
                                                                                    colspan="9">
                                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td style="width: 97%; text-align: center;" align="center">
                                                                                                        <asp:Label ID="lblSeleccion1" runat="server" Font-Bold="True" ForeColor="WhiteSmoke"
                                                                                                            Text="Seleccion de Reserva" Width="100%" Font-Names="Verdana" Font-Size="10pt"></asp:Label></td>
                                                                                                    <td style="width: 3%">
                                                                                                        <asp:ImageButton ID="btnCerrarReprogramar" runat="server" Height="16px" ImageAlign="AbsMiddle"
                                                                                                            ImageUrl="~/Images/iconos/b-regresar.gif" OnClick="btnCerrarReprogramar_Click" ToolTip="Regresar"
                                                                                                            Width="16px" Visible="False" /></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" style="padding-left: 5px; width: 12%" valign="middle">
                                                                                    <asp:Label ID="lblTextoLocalR" runat="server" Text="Local" Font-Bold="True"></asp:Label></td>
                                                                                <td align="left" style="width: 6%" valign="middle">
                                                                                    <asp:DropDownList ID="ddlPuntoRed" runat="server" AutoPostBack="True" CssClass="seleccion_04"
                                                                                        Width="200px" OnSelectedIndexChanged="ddlPuntoRed_SelectedIndexChanged" SkinID="cboob">
                                                                                    </asp:DropDownList></td>
                                                                                <td align="left" style="width: 12%" valign="middle">
                                                                                    <asp:Label ID="lblTextoTallerR" runat="server" Text="Taller" Font-Bold="True"></asp:Label></td>
                                                                                <td align="left" colspan="4" style="width: 45%" valign="middle">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlTaller" runat="server" Width="150px" OnSelectedIndexChanged="ddlTaller_SelectedIndexChanged"
                                                                                                    SkinID="cboob">
                                                                                                </asp:DropDownList></td>
                                                                                            <td style="padding-left: 5px">
                                                                                                <asp:Label ID="lblTaller" runat="server" Visible="False" Font-Bold="False"></asp:Label></td>
                                                                                            <td valign="middle">
                                                                                                <div style="height: 30px">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:ImageButton ID="btnMapaTaller" runat="server" ImageUrl="~/Images/SRC/mapa.jpg" />
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="ddlPuntoRed" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="ddlTaller" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="padding-left: 5px; font-weight: bold;" valign="middle" align="left">Fecha Inicio</td>
                                                                                <td valign="middle" align="left">
                                                                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="texto_00" Enabled="False" Font-Size="8pt"
                                                                                        MaxLength="1" Width="75px">

                                                                                    </asp:TextBox>
                                                                                    &nbsp;
                                                                                    <asp:ImageButton ID="imbFecha" runat="server" ImageUrl="~/Images/SRC/cal.jpg" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                    &nbsp; &nbsp;
                                                                                    <asp:ImageButton ID="imbFecAnt" OnClick="imbFecAnt_Click" runat="server" ImageUrl="~/Images/SRC/btn_atras.jpg"
                                                                                        ImageAlign="AbsMiddle"></asp:ImageButton>&nbsp;
                                                                                    <asp:ImageButton ID="imbFecSgte" OnClick="imbFecSgte_Click" runat="server" ImageUrl="~/Images/SRC/btn_adelante.jpg"
                                                                                        ImageAlign="AbsMiddle"></asp:ImageButton><cc1:MaskedEditValidator ID="MaskedEditValidator1"
                                                                                            runat="server" Font-Size="7pt" Font-Italic="True" ValidationGroup="MKE" Display="Dynamic"
                                                                                            ControlToValidate="txtFecha" ControlExtender="mskF1" InvalidValueBlurredMessage="Fecha Invalida"
                                                                                            InvalidValueMessage="Fecha Invalida" EmptyValueMessage="Ingrese Fecha" ErrorMessage="MaskedEditValidator1">
  
  
                                                                                        </cc1:MaskedEditValidator>
                                                                                    <cc1:MaskedEditExtender ID="mskF1" runat="server" TargetControlID="txtFecha" Enabled="True"
                                                                                        Mask="99/99/9999" MaskType="Date" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                        CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder=""
                                                                                        CultureTimePlaceholder="" CultureDatePlaceholder="" ErrorTooltipEnabled="True">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="ceFecha" runat="server" TargetControlID="txtFecha" Enabled="True"
                                                                                        PopupButtonID="imbFecha" Format="dd/MM/yyyy" OnClientDateSelectionChanged="llama1">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td valign="middle" align="left" style="font-weight: bold">Hora Inicio</td>
                                                                                <td valign="middle" align="left">
                                                                                    <asp:DropDownList ID="ddlHoraInicio1" runat="server" AutoPostBack="True" Width="80px"
                                                                                        OnSelectedIndexChanged="ddlHoraInicio1_SelectedIndexChanged" Font-Bold="False"
                                                                                        Font-Names="Verdana">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td valign="middle" align="left" style="font-weight: bold; width: 9%;">Hora Final</td>
                                                                                <td valign="middle" align="left" style="font-weight: normal; text-decoration: none">
                                                                                    <asp:DropDownList ID="ddlHoraFin1" runat="server" AutoPostBack="True" CssClass="seleccion_00"
                                                                                        Width="80px" OnSelectedIndexChanged="ddlHoraFin1_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td valign="middle" align="left">
                                                                                    <asp:LinkButton ID="lnkVerHorariosProximos" OnClick="lnkVerHorariosProximos_Click" runat="server" Font-Bold="True"
                                                                                        Font-Names="Arial" ForeColor="#0000C0" Font-Size="9pt">Turnos proximos >></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="9" style="border-top: #c7d7ee 2px solid; text-align: left;">
                                                                                    <asp:HiddenField ID="hfFecha" runat="server" />
                                                                                    <asp:HiddenField ID="hfHoraIni1" runat="server" />
                                                                                    <asp:HiddenField ID="hfHoraFin1" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%" align="left">
                                                                    <asp:Button ID="btn_Hidden_1" OnClick="btn_Hidden_1_Click" runat="server" Height="2px" Width="2px"
                                                                        BackColor="White" ForeColor="White" BorderStyle="None" BorderColor="White"></asp:Button></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%; height: 240px" align="center">
                                                                    <asp:Panel ID="pnlHorarioReserva" runat="server" Height="215px" CssClass="tabla_horario"
                                                                        Width="800px" ScrollBars="Both" HorizontalAlign="Left">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="gvHorarioReserva" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                                                                    Font-Size="8pt" HorizontalAlign="Left" OnRowCommand="gvHorarioReserva_RowCommand"
                                                                                    SkinID="Grilla">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="TALLER" HeaderText="Taller">
                                                                                            <ItemStyle CssClass="tabla_horario_filataller1" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                                Width="200px" />
                                                                                            <HeaderStyle CssClass="tabla_horario_taller" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                                Width="100px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ASESOR_SERV" HeaderText="Asesor de Servicio">
                                                                                            <ItemStyle CssClass="tabla_horario_filaasesor1" />
                                                                                            <HeaderStyle CssClass="tabla_horario_asesor" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Rango_Horario" HeaderText="Rango_Horario">
                                                                                            <ItemStyle CssClass="tabla_horario_filaasesor1" />
                                                                                            <HeaderStyle CssClass="tabla_horario_asesor" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    <RowStyle Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <EditRowStyle Font-Size="8pt" />
                                                                                    <HeaderStyle Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                                                    <AlternatingRowStyle Font-Size="8pt" />
                                                                                </asp:GridView>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </asp:Panel>
                                                                    <asp:Label ID="lblFlgHorario1" runat="server" Font-Bold="True" Text="No hay horario disponible para la consuta."
                                                                        Font-Names="Verdana" ForeColor="DimGray" Font-Size="12pt"></asp:Label>
                                                                    <br />
                                                                    <asp:Panel ID="pnlLeyenda" runat="server" BorderWidth="0px" Font-Names="Arial Narrow"
                                                                        BorderColor="White" HorizontalAlign="Left">
                                                                        <table border="0" cellpadding="0" cellspacing="0" class="textotab" width="100%">
                                                                            <tr>
                                                                                <td style="font-weight: bold; width: 10px">TCT:</td>
                                                                                <td>
                                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                                                                        <ContentTemplate>
                                                                                            &nbsp;<asp:Label ID="Lbltct" runat="server" BorderStyle="None" Font-Names="Arial Narrow"></asp:Label>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td align="center">
                                                                                    <img src="../Images/leyenda.jpg" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    &nbsp; &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%; height: 45px; border-top: #c7d7ee 2px solid;" align="center">&nbsp;<asp:Button ID="btnReprogramarCita_1" OnClick="btnReprogramarCita_1_2_Click"
                                                                    runat="server" Font-Bold="True" Height="32px" CssClass="boton_02" Text="Reprogramar Cita"
                                                                    ForeColor="Black"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
                                                <!-- fin tabla 31 -->
                                                <asp:Panel ID="Paneldos" runat="server" BackColor="White" Visible="False">
                                                    <table style="background-color: white" id="Table6" class="seccion" cellspacing="0"
                                                        cellpadding="0" width="800" border="0">
                                                        <tbody>
                                                            <tr>
                                                                <td style="background-color: white" align="left">
                                                                    <table class="textotab" cellspacing="0" cellpadding="0" width="800" border="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="center" colspan="11" style="border-right: dimgray 1px solid; padding-right: 1px; border-top: dimgray 1px solid; padding-left: 1px; padding-bottom: 1px; border-left: dimgray 1px solid; padding-top: 1px; border-bottom: dimgray 1px solid; height: 28px; background-color: #005cab">
                                                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:Label ID="lblSeleccion2" runat="server" Font-Bold="True" ForeColor="WhiteSmoke"
                                                                                                Text="Seleccion de Reserva" Width="100%" Font-Names="Verdana" Font-Size="10pt"></asp:Label>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="11" style="height: 10px; background-color: white"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="font-weight: bold; width: 9%;" valign="middle" align="left" width="10%">Fecha Inicio</td>
                                                                                <td valign="middle" align="left" style="text-align: center">
                                                                                    <asp:TextBox ID="txtFechaIni" runat="server" Width="70px" Font-Size="8pt" MaxLength="10"
                                                                                        SkinID="txtob" Enabled="False"></asp:TextBox><!-- maskval, maskedi, calendar --><cc1:MaskedEditValidator
                                                                                            ID="MaskedEditValidator2" runat="server" Font-Size="7pt" Font-Italic="True" ValidationGroup="MKE"
                                                                                            Display="Dynamic" ControlToValidate="txtFechaIni" ControlExtender="mskF1" InvalidValueBlurredMessage="Fecha Invalida"
                                                                                            InvalidValueMessage="Fecha Invalida" EmptyValueMessage="Ingrese Fecha" ErrorMessage="MaskedEditValidator2"></cc1:MaskedEditValidator>
                                                                                    <cc1:MaskedEditExtender ID="mskFI" runat="server" TargetControlID="txtFechaIni" Enabled="True"
                                                                                        Mask="99/99/9999" MaskType="Date" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                        CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder=""
                                                                                        CultureTimePlaceholder="" CultureDatePlaceholder="" ErrorTooltipEnabled="True">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="ceFechaIni" runat="server" TargetControlID="txtFechaIni"
                                                                                        Enabled="True" PopupButtonID="imbFecha1" Format="dd/MM/yyyy" OnClientDateSelectionChanged="llama2">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td style="width: 5%;" valign="middle" align="left">
                                                                                    <asp:ImageButton ID="imbFecha1" runat="server" ImageUrl="~/Images/SRC/cal.jpg"></asp:ImageButton>
                                                                                </td>
                                                                                <td style="font-weight: bold; width: 9%;" valign="middle" width="10%">Fecha Final</td>
                                                                                <td valign="middle" align="left" style="text-align: center">
                                                                                    <asp:TextBox ID="txtFechaFin" runat="server" Width="70px" Font-Size="8pt" MaxLength="10"
                                                                                        SkinID="txtob" Enabled="False"></asp:TextBox><!-- maskval, maskedit, calendar --><cc1:MaskedEditValidator
                                                                                            ID="MaskedEditValidator3" runat="server" Font-Size="7pt" Font-Italic="True" ValidationGroup="MKE"
                                                                                            Display="Dynamic" ControlToValidate="txtFechaFin" ControlExtender="mskFF" InvalidValueBlurredMessage="Fecha Invalida"
                                                                                            InvalidValueMessage="Fecha Invalida" EmptyValueMessage="Ingrese Fecha" ErrorMessage="MaskedEditValidator3"></cc1:MaskedEditValidator>
                                                                                    <cc1:MaskedEditExtender ID="mskFF" runat="server" TargetControlID="txtFechaFin" Enabled="True"
                                                                                        Mask="99/99/9999" MaskType="Date" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                        CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder=""
                                                                                        CultureTimePlaceholder="" CultureDatePlaceholder="" ErrorTooltipEnabled="True">
                                                                                    </cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="ceFechaFin" runat="server" TargetControlID="txtFechaFin"
                                                                                        Enabled="True" PopupButtonID="imbFecha2" Format="dd/MM/yyyy" OnClientDateSelectionChanged="llama2">
                                                                                    </cc1:CalendarExtender>
                                                                                </td>
                                                                                <td style="width: 7%;" valign="middle" align="left">
                                                                                    <asp:ImageButton ID="imbFecha2" runat="server" ImageUrl="~/Images/SRC/cal.jpg"></asp:ImageButton>
                                                                                </td>
                                                                                <td style="font-weight: bold; width: 8%;" valign="middle" align="left" width="10%">Hora Inicio</td>
                                                                                <td valign="middle" align="left">
                                                                                    <asp:DropDownList ID="ddlHoraInicio2" runat="server" CssClass="seleccion_00" Width="80px"
                                                                                        OnSelectedIndexChanged="ddlHoraInicio2_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="font-weight: bold; width: 8%;" valign="middle" align="left" width="10%">Hora Final</td>
                                                                                <td valign="middle" align="left">
                                                                                    <asp:DropDownList ID="ddlHoraFin2" runat="server" Width="80px" OnSelectedIndexChanged="ddlHoraFin2_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="left" style="width: 10%;" valign="middle">
                                                                                    <asp:LinkButton ID="lnkVerHorariosActuales" runat="server" OnClick="lnkVerHorariosActuales_Click" Font-Bold="True"
                                                                                        Font-Names="Arial" Font-Size="10pt" ForeColor="#0000C0"><< Regresar</asp:LinkButton></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="11" style="border-bottom: #c7d7ee 2px solid; height: 10px" valign="middle">
                                                                                    <asp:HiddenField ID="hfFechaIni" runat="server" />
                                                                                    <asp:HiddenField ID="hfFechaFin" runat="server" />
                                                                                    <asp:HiddenField ID="hfHoraIni2" runat="server" />
                                                                                    <asp:HiddenField ID="hfHoraFin2" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom">
                                                                    <asp:Button ID="btn_Hidden_2" OnClick="btn_Hidden_2_Click" runat="server" Height="1px" Width="2px"
                                                                        BackColor="White" ForeColor="White" BorderStyle="None" BorderColor="White"></asp:Button><asp:Panel
                                                                            ID="PnlCabecera" runat="server" Width="783px">
                                                                            <table id='tblCabezera' border="1" cellpadding="0" cellspacing="0" style="width: 802px; color: white; border-top-style: none; border-right-style: none; border-left-style: none; background-color: #005cab; border-bottom-style: none; height: 29px;">
                                                                                <tr>
                                                                                    <th style="width: 138px">Punto de Red</th>
                                                                                    <th style="width: 120px">Taller</th>
                                                                                    <th style="width: 86px">Fecha</th>
                                                                                    <th style="width: 72px">Hora</th>
                                                                                    <th style="width: 180px">Asesor de Servicio</th>
                                                                                    <th style="width: 100px">Quick Service</th>
                                                                                    <th style="width: 85px">Seleccionar</th>
                                                                                    <th style="width: 15px"></th>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 230px">
                                                                    <asp:Panel ID="pnlHorarioDisponible" runat="server" Height="210px" CssClass="tabla_horario"
                                                                        Width="800px" ScrollBars="Vertical" HorizontalAlign="Left">
                                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="gvHorarioDisponible" runat="server" AutoGenerateColumns="False"
                                                                                    BackColor="White" BorderColor="DimGray" BorderWidth="1px" Font-Bold="False" Font-Size="8pt"
                                                                                    ForeColor="Black" HorizontalAlign="Left" OnRowCommand="gvHorarioDisponible_RowCommand"
                                                                                    ShowHeader="False" Width="782px" SkinID="Grilla">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="PUNTO_RED" DataFormatString="{0:d}" HeaderText="Punto de Red">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="138px"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="TALLER" HeaderText="Taller">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="FECHA" DataFormatString="{0:d}" HeaderText="Fecha">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="HORA" HeaderText="Hora">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ASESOR_SERVICIO" HeaderText="Asesor de Servicio">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="180px"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="QUICK_SERVICE" HeaderText="Quick Service">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:ButtonField ImageUrl="~/Images/SRC/si.PNG" ButtonType="Image" HeaderText="Seleccionar">
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="85px"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></ItemStyle>
                                                                                        </asp:ButtonField>
                                                                                    </Columns>
                                                                                    <RowStyle BackColor="#F7F6F3" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <EditRowStyle Font-Size="8pt" />
                                                                                    <HeaderStyle Font-Bold="False" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                                        Wrap="True" />
                                                                                    <AlternatingRowStyle BackColor="White" Font-Size="8pt" />
                                                                                </asp:GridView>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </asp:Panel>
                                                                    <asp:Label ID="lblFlgHorario2" runat="server" Font-Bold="True" Text="No hay horario disponible para la consuta."
                                                                        Font-Names="Verdana" ForeColor="DimGray" Font-Size="12pt"></asp:Label><br />
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="center" valign="middle">
                                                                                    <asp:Panel ID="pnlLeyenda2" runat="server" BorderWidth="0px" Height="15px" Width="100%"
                                                                                        BorderColor="White" HorizontalAlign="Left">
                                                                                        <img src="../Images/leyenda.jpg" />
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 45px; border-top: #c7d7ee 2px solid; width: 100%;" valign="middle"
                                                                    align="center">
                                                                    <asp:Button ID="btnReprogramarCita_2" OnClick="btnReprogramarCita_1_2_Click" runat="server"
                                                                        Font-Bold="True" Height="30px" CssClass="boton_02" Text="Reprogramar Cita" ForeColor="Black"></asp:Button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="seccion_fila" align="center"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
                                            </center>
                                            <center>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center">
                                                            <img src="../Images/Tabs/borabajo.gif" width="820" /></td>
                                                    </tr>
                                                </table>
                                            </center>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReprogramarCita" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <cc1:ModalPopupExtender ID="MpeReprogramacion" runat="server" BackgroundCssClass="modalBackground"
        DynamicServicePath="" Enabled="True" PopupControlID="UdPnlReprogramacion" RepositionMode="None"
        TargetControlID="HdfReprogramacion">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="HdfReprogramacion" runat="server" />

    <asp:Panel ID="panelUpdateProgress" runat="server">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div class="overlay" />
                <div class="overlayContent">
                    <img src="../bootstrap/img/ajax-loader.gif" alt="Loading" border="1" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>

    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />


    <script language="javascript" type="text/javascript">

        function NASort(a, b) { return (a.innerHTML > b.innerHTML) ? 1 : -1; };

        function fc_mostrarFiltro() {

            $('#tblCabezera').append("<tr style='background-color:#ffffff;'><td></td><td><select id='cboTaller' class='cdll' style='width:130px;' onchange='fc_filtrarGrilla(this)' ></select></td><td><select id='cboFecha' class='cdll'  style='width:80px;' onchange='fc_filtrarGrilla(this)' ></select></td><td><select id='cboHora' class='cdll'   onchange='fc_filtrarGrilla(this)' ></select></td><td><select id='cboAsesor' class='cdll' style='width:200px;'  onchange='fc_filtrarGrilla(this)' ></select></td><td></td><td></td><td></td></tr>");

            var sTaller = '<option value="' + "" + '"></option>';
            var sFecha = '<option value="' + "" + '"></option>';
            var sHora = '<option value="' + "" + '"></option>';
            var sAsesor = '<option value="' + "" + '"></option>';

            var grid = document.getElementById("<%=gvHorarioDisponible.ClientID %>");
            for (i = 0; i < grid.rows.length - 1; i++) {
                if (sTaller.indexOf('<option>' + grid.rows[i].cells[1].innerHTML + '</option>') == -1) sTaller += '<option>' + grid.rows[i].cells[1].innerHTML + '</option>';
                if (sFecha.indexOf('<option>' + grid.rows[i].cells[2].innerHTML + '</option>') == -1) sFecha += '<option>' + grid.rows[i].cells[2].innerHTML + '</option>';
                if (sHora.indexOf('<option>' + grid.rows[i].cells[3].innerHTML + '</option>') == -1) sHora += '<option>' + grid.rows[i].cells[3].innerHTML + '</option>';
                if (sAsesor.indexOf('<option>' + grid.rows[i].cells[4].innerHTML + '</option>') == -1) sAsesor += '<option>' + grid.rows[i].cells[4].innerHTML + '</option>';
            }

            $("#cboTaller").append(sTaller.replace('  ', ' '));
            $("#cboFecha").append(sFecha);
            $("#cboHora").append(sHora);
            $("#cboAsesor").append(sAsesor.replace('  ', ' '));

            $('#cboTaller option').sort(NASort).appendTo('#cboTaller');
            $('#cboFecha option').sort(NASort).appendTo('#cboFecha');
            $('#cboHora option').sort(NASort).appendTo('#cboHora');
            $("#cboAsesor option").sort(NASort).appendTo('#cboAsesor');

            $("#cboTaller")[0].selectedIndex = 0;
            $("#cboFecha")[0].selectedIndex = 0;
            $("#cboHora")[0].selectedIndex = 0;
            $("#cboAsesor")[0].selectedIndex = 0;
        }

        function fc_filtrarGrilla() {

            var sTaller = $('#cboTaller').val();
            var sFecha = $('#cboFecha').val();
            var sHora = $('#cboHora').val();
            var sAsesor = $('#cboAsesor').val();

            var index = 0;

            $('#<%=gvHorarioDisponible.ClientID %> tr').each(function () {

                index = index + 1;

                if (document.getElementById("<%=gvHorarioDisponible.ClientID %>").rows.length == (index + 1))
                    $(this).show();
                else {
                    if (
                        ($(this).find("td").eq(1).html().replace('  ', ' ') == sTaller || sTaller == '') &&
                        ($(this).find("td").eq(2).html().replace('  ', ' ') == sFecha || sFecha == '') &&
                        ($(this).find("td").eq(3).html().replace('  ', ' ') == sHora || sHora == '') &&
                        ($(this).find("td").eq(4).html().replace('  ', ' ') == sAsesor || sAsesor == '')
                    )
                        $(this).show();
                    else
                        $(this).hide();
                }
            });
        }

        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
