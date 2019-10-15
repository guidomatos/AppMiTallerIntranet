<%@ Page Language="C#" MasterPageFile="~/Principal.master" AutoEventWireup="true"
    CodeFile="SRC_Maestro_Detalle_Usuarios.aspx.cs" Inherits="SRC_Mantenimiento_SRC_Maestro_Detalle_Usuarios"
    Title="Maestro Detalle Usuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

            if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabDatosGenerales').style.visibility != 'hidden') index = 0;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmAdmTienda').style.visibility != 'hidden') index = 1;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmAdmTaller').style.visibility != 'hidden') index = 2;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmAsesorServicio').style.visibility != 'hidden') index = 3;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmOprCallCenter').style.visibility != 'hidden') index = 8;

            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOffForm('2');
            setTabCabeceraOffForm('3');
            setTabCabeceraOffForm('8');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);


        }

        function Fc_CheckT(chkbox, tipo) {
            var flCheck = document.getElementById('<%=chkTotal.ClientID %>').checked;

            document.getElementById('<%=txt_capacidad_fo.ClientID %>').disabled = flCheck;
            document.getElementById('<%=txt_capacidad_bo.ClientID %>').disabled = flCheck;
            document.getElementById('<%=txt_capacidad.ClientID %>').disabled = !flCheck;

            if (!flCheck) {
                document.getElementById('<%=txt_capacidad.ClientID %>').value = '';
                document.getElementById('<%=txt_capacidad_fo.ClientID %>').focus();
            }
            else {
                document.getElementById('<%=txt_capacidad_fo.ClientID %>').value = '';
                document.getElementById('<%=txt_capacidad_bo.ClientID %>').value = '';

                document.getElementById('<%=txt_capacidad.ClientID %>').focus();
            }

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
                return false; //anula la entrada de texto. 
            }
        }

        function Valida_DNI(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser  

            if (key >= 48 && key <= 57) { }
            else if (key == 8 || key == 9) { } //BS y TAB
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false; //anula la entrada de texto. 
            }
        }

        function Valida_Usuario(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser              
            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú
            else if (key == 225 || key == 223 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            //        else if (key == 164 || key == 165){} // ñ Ñ
            else if (key == 209 || key == 241) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else
                return false; //anula la entrada de texto.         
        }

        function Valida_Nombre(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_nombres = document.getElementById('<%=txt_nombres.ClientID%>');
            if (txt_nombres.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false; //anula la entrada de texto. 
            }
        }

        function Valida_ApePaterno(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_ape_paterno = document.getElementById('<%=txt_ape_paterno.ClientID%>');
            if (txt_ape_paterno.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false; //anula la entrada de texto. 
            }
        }

        function Valida_ApeMaterno(eventObj) {
            var key;
            if (eventObj.keyCode)           // For IE
                key = eventObj.keyCode;
            else if (eventObj.Which)
                key = eventObj.Which;       // For FireFox
            else
                key = eventObj.charCode;    // Other Browser

            var txt_ape_materno = document.getElementById('<%=txt_ape_materno.ClientID%>');
            if (txt_ape_materno.value.length == 0) {
                if (key == 32) { return false; }
            }

            if (key >= 65 && key <= 90) { }
            else if (key >= 97 && key <= 122) { }
            else if (key == 32) { }   //ESPACIO
            else if (key == 8 || key == 9) { }    //BS y TAB
            else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218) { } // Á  É  Í  Ó  Ú          
            else if (key == 225 || key == 233 || key == 237 || key == 243 || key == 250) { } // á  é  í  ó  ú
            else if (key == 241 || key == 209) { } // ñ Ñ
            else if (key == 37 || key == 39) { } //IZQ y DER
            else if (key == 46) { } //SUPRIMIR
            else {
                return false; //anula la entrada de texto. 
            }
        }

        //validar fecha

        function Validar_Fecha(Cadena) {
            var Fecha = new String(Cadena);   // Crea un string  
            var RealFecha = new Date();   // Para sacar la fecha de hoy  
            // Cadena Año  
            var Ano = new String(Fecha.substring(Fecha.lastIndexOf("/") + 1, Fecha.length));
            // Cadena Mes  
            var Mes = new String(Fecha.substring(Fecha.indexOf("/") + 1, Fecha.lastIndexOf("/")));
            // Cadena Día  
            var Dia = new String(Fecha.substring(0, Fecha.indexOf("/")));

            // Valido el año  
            if (isNaN(Ano) || Ano.length < 4 || parseFloat(Ano) < 1900) {
                alert('Año inválido');
                return false;
            }
            // Valido el Mes  
            if (isNaN(Mes) || parseFloat(Mes) < 1 || parseFloat(Mes) > 12) {
                alert('Mes inválido');
                return false;
            }
            // Valido el Dia  
            if (isNaN(Dia) || parseInt(Dia, 10) < 1 || parseInt(Dia, 10) > 31) {
                alert('Día inválido');
                return false;
            }
            if (Mes == 4 || Mes == 6 || Mes == 9 || Mes == 11 || Mes == 2) {
                if (Mes == 2 && Dia > 28 || Dia > 30) {
                    alert('Día inválido');
                    return false;
                }
            }

        }

        function isMail(Cadena) {

            Punto = Cadena.substring(Cadena.lastIndexOf('.') + 1, Cadena.length)            // Cadena del .com
            Dominio = Cadena.substring(Cadena.lastIndexOf('@') + 1, Cadena.lastIndexOf('.'))    // Dominio @lala.com
            Usuario = Cadena.substring(0, Cadena.lastIndexOf('@'))                  // Cadena lalala@
            Reserv = "@⁄º\"\'+*{}\\<>?¿[]áéíóú#·¡!^*;,:"                      // Letras Reservadas

            // Añadida por El Codigo para poder emitir un alert en funcion de si email valido o no  
            valido = true

            // verifica qie el Usuario no tenga un caracter especial
            for (var Cont = 0; Cont < Usuario.length; Cont++) {
                X = Usuario.substring(Cont, Cont + 1)
                if (Reserv.indexOf(X) != -1)
                    valido = false
            }

            // verifica qie el Punto no tenga un caracter especial  
            for (var Cont = 0; Cont < Punto.length; Cont++) {
                X = Punto.substring(Cont, Cont + 1)
                if (Reserv.indexOf(X) != -1)
                    valido = false
            }

            // verifica qie el Dominio no tenga un caracter especial  
            for (var Cont = 0; Cont < Dominio.length; Cont++) {
                X = Dominio.substring(Cont, Cont + 1)
                if (Reserv.indexOf(X) != -1)
                    valido = false
            }

            // Verifica la sintaxis básica.....  
            if (Punto.length < 2 || Dominio < 1 || Cadena.lastIndexOf('.') < 0 || Cadena.lastIndexOf('@') < 0 || Usuario < 1) {
                valido = false
            }

            // Añadido por El Código para que emita un alert de aviso indicando si email válido o no  
            if (valido) {
                // alert('Email válido.')
                return true    //cambiar por return true para hacer el submit del formulario en caso de validacion correcta  
            } else {
                // alert('Correo no válido.')
                return false
            }
        }

        function Validar_Hora(hora) {
            a = hora.charAt(0) //<=2
            b = hora.charAt(1) //<4
            c = hora.charAt(2) //:
            d = hora.charAt(3) //<=5
            e = hora.charAt(5) //:
            f = hora.charAt(6) //<=5
            if ((a == 2 && b > 3) || (a > 2))
                return false;
            if (d > 5)
                return false;
        }

        function Guardar_Pass() {
            var txt_contraseña = document.getElementById('<%=txt_contraseña.ClientID%>');
            document.getElementById('<%=hdf_pass_nuevo.ClientID%>').value = txt_contraseña.value.trim();
        }

        function Validar_HoraInicio_Menor_HoraFin() {
            //Validar hora de inicio menor a hora de fin
            var txt_hora_inicio = document.getElementById('<%=txt_hora_inicio.ClientID%>');
            var txt_hora_fin = document.getElementById('<%=txt_hora_fin.ClientID%>');

            fragmento_texto = txt_hora_inicio.value.split(':');
            fragmento_texto2 = txt_hora_fin.value.split(':');

            if (fragmento_texto[0].substring(0, 1) == "0")
                hora_inicio2 = fragmento_texto[0].substring(1, 2) + fragmento_texto[1];
            else
                hora_inicio2 = fragmento_texto[0] + fragmento_texto[1];

            if (fragmento_texto2[0].substring(0, 1) == "0")
                hora_fin2 = fragmento_texto2[0].substring(1, 2) + fragmento_texto2[1];
            else
                hora_fin2 = fragmento_texto2[0] + fragmento_texto2[1];

            var valor1 = parseInt(hora_inicio2);
            var valor2 = parseInt(hora_fin2);

            if (valor1 > 2300 || valor2 > 2300) {
                alert('Las horas deben estar en el rango de 00:00 a 23.00.');
                return false;
            }
            else if (valor1 > valor2) {
                return false;
            }
        }

        function Valida99() {
            var txt_movil = document.getElementById('<%=txt_movil.ClientID%>');
            var txt_correo = document.getElementById('<%=txt_correo.ClientID%>');
            var txt_fec_inicio_acceso = document.getElementById('<%=txt_fec_inicio_acceso.ClientID%>');
            var txt_fin_acceso = document.getElementById('<%=txt_fin_acceso.ClientID%>');
            var txt_hora_inicio = document.getElementById('<%=txt_hora_inicio.ClientID%>');
            var txt_hora_fin = document.getElementById('<%=txt_hora_fin.ClientID%>');

            if (txt_movil.value.trim() == "") {
                alert('Ingrse el numero movil.');
                txt_movil.focus();
                return false;
            }
            else if (txt_movil.value.trim().length < 9) {
                alert('Telefono Movil no valido. Ingrese 9 Digitos como Minimo.');
                txt_movil.focus();
                return false;
            }
            else {
                alert('Telefono Movil ok.');
            }

        }

        function Validar_Datos() {

            var txt_ape_paterno = document.getElementById('<%=txt_ape_paterno.ClientID%>');
            var txt_nombres = document.getElementById('<%=txt_nombres.ClientID%>');
            var txt_login = document.getElementById('<%=txt_login.ClientID%>');
            var txt_contraseña = document.getElementById('<%=txt_contraseña.ClientID%>');
            var txt_nro_documento = document.getElementById('<%=txt_nro_documento.ClientID%>');
            var ddl_perfil = document.getElementById('<%=ddl_perfil.ClientID %>');
            var hdf_pass = document.getElementById('<%=hdf_pass.ClientID %>');

            var ddl_departamento_dg = document.getElementById('<%=ddl_departamento_dg.ClientID %>');
            var ddl_provincia_dg = document.getElementById('<%=ddl_provincia_dg.ClientID %>');
            var ddl_distrito_dg = document.getElementById('<%=ddl_distrito_dg.ClientID %>');
            var ddl_ubicacion_dg = document.getElementById('<%=ddl_ubicacion_dg.ClientID %>');
            var hdf_pass_nuevo = document.getElementById('<%=hdf_pass_nuevo.ClientID %>');
            var ddl_perfil = document.getElementById('<%=ddl_perfil.ClientID %>');
            var ddl_taller_ase_serv_t = document.getElementById('<%=ddl_taller_ase_serv_t.ClientID %>');

            var ddl_estado = document.getElementById('<%=ddl_estado.ClientID %>');
            var pais = '<%=ConfigurationManager.AppSettings["CodPais"].ToString() %>';

            if (txt_nro_documento.value.trim() == "") {
                alert('Ingrese Nro. de Documento.');
                txt_nro_documento.focus();
                return false;
            }

            else if ((txt_nro_documento.value.trim().length < 8) && (pais == '1')) {
                alert('Nro. de Documento no valido. Debe Tener 8 Digitos');
                txt_nro_documento.focus();
                return false;
            }

            else if ((txt_nro_documento.value.trim().length < 7) && (pais == '2')) {
                alert('Nro. de Documento no valido.');
                txt_nro_documento.focus();
                return false;
            }

            else if (txt_nombres.value.trim() == "") {
                alert('Ingrese Nombre.');
                txt_nombres.focus();
                return false;
            }

            else if (txt_ape_paterno.value.trim() == "") {
                alert('Ingrese Apellido Paterno.');
                txt_ape_paterno.focus();
                return false;
            }

            else if (txt_login.value.trim() == "") {
                alert('Ingrese Login.');
                txt_login.focus();
                return false;
            }

            else if (txt_login.value.trim().length < 4) {
                alert('Login Debe Tener Minimo 4 Caracteres.');
                txt_login.focus();
                return false;
            }

            else if (hdf_pass.value == "nuevo") { //debe ingresar password

                if (hdf_pass_nuevo.value.trim() == "") {
                    alert('Ingrese Contraseña.');
                    txt_contraseña.focus();
                    return false;
                } else if (hdf_pass_nuevo.value.trim().length < 4) {
                    alert('Contraseña debe tener 4 caracteres como minimo.');
                    txt_contraseña.focus();
                    return false;
                }
            }

            else if (ddl_perfil.selectedIndex == 0) {
                alert('Seleccione un Perfil.');
                return false;
            }

            else if (ddl_departamento_dg.selectedIndex == 0) {
                alert('Seleccione un Departamento.');
                return false;
            }

            else if (ddl_provincia_dg.selectedIndex == 0) {
                alert('Seleccione una Provincia.');
                return false;
            }

            else if (ddl_distrito_dg.selectedIndex == 0) {
                alert('Seleccione un Distrito.');
                return false;
            }

            else if (ddl_ubicacion_dg.selectedIndex == 0) {
                alert('Seleccione una Ubicacion.');
                return false;
            }

            var indice2 = ddl_perfil.selectedIndex;
            var valor2 = ddl_perfil.options[ddl_perfil.selectedIndex].value;

            if (valor2 == "ASRV") {
                var indice3 = ddl_taller_ase_serv_t.selectedIndex;
                if (indice3 == 0) {
                    alert('Debe seleccionar un taller para el Asesor de Servicio.'); return false;
                }
            }

            if (ddl_estado.selectedIndex == 0) {
                alert('Seleccione un Estado.');
                return false;
            }


            //DATOS NO OBLIGATORIOS
            //---------------------

            var txt_movil = document.getElementById('<%=txt_movil.ClientID%>');
            var txt_correo = document.getElementById('<%=txt_correo.ClientID%>');
            var txt_fec_inicio_acceso = document.getElementById('<%=txt_fec_inicio_acceso.ClientID%>');
            var txt_fin_acceso = document.getElementById('<%=txt_fin_acceso.ClientID%>');
            var txt_hora_inicio = document.getElementById('<%=txt_hora_inicio.ClientID%>');
            var txt_hora_fin = document.getElementById('<%=txt_hora_fin.ClientID%>');

            if (txt_movil.value.trim() != "") {
                if (txt_movil.value.trim().length < 9) {
                    alert('Ingrese los 9 dígitos como mínimo para el teléfono nóvil.');
                    txt_movil.focus();
                    return false;
                }
            }

            if (txt_correo.value.trim() != "") {
                if (!isMail(txt_correo.value.trim())) {
                    alert('Correo no valido.');
                    txt_correo.focus();
                    return false;
                }
            }

            //  FECHAS
            //-----------
            if (txt_fec_inicio_acceso.value.trim() != "") {
                if (Validar_Fecha(txt_fec_inicio_acceso.value.trim()) == false) {
                    txt_fec_inicio_acceso.focus();
                    return false;
                }

            }
            if (txt_fin_acceso.value.trim() != "") {
                if (Validar_Fecha(txt_fin_acceso.value.trim()) == false) {
                    txt_fin_acceso.focus();
                    return false;
                }
            }

            if (txt_fec_inicio_acceso.value.trim() != "" && txt_fin_acceso.value.trim() == "") {
                alert('Debe Ingresar la Fecha de Fin.');
                return false;
            }
            else if (txt_fec_inicio_acceso.value.trim() == "" && txt_fin_acceso.value.trim() != "") {
                alert('Debe Ingresar la Fecha de Inicio.');
                return false;
            }
            else if (txt_fec_inicio_acceso.value.trim() != "" && txt_fin_acceso.value.trim() != "") {
                datDate1 = Date.parse(txt_fec_inicio_acceso.value);
                datDate2 = Date.parse(txt_fin_acceso.value);
                dateDiff = ((datDate1 - datDate2) / (24 * 60 * 60 * 1000));


                //--------------------------------------

                var Ano1 = new String(txt_fec_inicio_acceso.value.substring(txt_fec_inicio_acceso.value.lastIndexOf("/") + 1, txt_fec_inicio_acceso.value.length));
                // Cadena Mes  
                var Mes1 = new String(txt_fec_inicio_acceso.value.substring(txt_fec_inicio_acceso.value.indexOf("/") + 1, txt_fec_inicio_acceso.value.lastIndexOf("/")));
                // Cadena Día  
                var Dia1 = new String(txt_fec_inicio_acceso.value.substring(0, txt_fec_inicio_acceso.value.indexOf("/")));

                var Ano2 = new String(txt_fin_acceso.value.substring(txt_fin_acceso.value.lastIndexOf("/") + 1, txt_fin_acceso.value.length));
                // Cadena Mes  
                var Mes2 = new String(txt_fin_acceso.value.substring(txt_fin_acceso.value.indexOf("/") + 1, txt_fin_acceso.value.lastIndexOf("/")));
                // Cadena Día  
                var Dia2 = new String(txt_fin_acceso.value.substring(0, txt_fin_acceso.value.indexOf("/")));

                var fec1s = Ano1 + Mes1 + Dia1;
                var fec2s = Ano2 + Mes2 + Dia2;



                if (fec1s.length > 0 && fec2s.length > 0) {
                    var fec1 = parseFloat(Ano1 + Mes1 + Dia1);
                    var fec2 = parseFloat(Ano2 + Mes2 + Dia2);

                    // --------------- validar fecha de hoy --------------
                    // --------------- validar fecha de hoy --------------
                    var d = new Date();
                    //var hoyDia =((( d.getDate()) < 10 ) ? '0' + (d.getDate()) : d.getDate()+1)  + '/' + ((( d.getMonth()+1 ) < 10 ) ? '0' + (d.getMonth()+1) : d.getMonth()+1) + '/' + d.getFullYear();
                    var Dia = new String(((d.getDate()) < 10) ? '0' + (d.getDate()) : d.getDate());
                    var Mes = new String(((d.getMonth() + 1) < 10) ? '0' + (d.getMonth() + 1) : d.getMonth() + 1);
                    var Ano = new String(d.getFullYear());
                    var fechaHoy = parseFloat(Ano + Mes + Dia);

                    if (fec2 < fechaHoy) {
                        alert('Fecha Final debe ser mayor que la fecha Actual.');
                        return false;
                    }
                    else if (fec1 >= fec2) {
                        alert('Fecha de Inicio debe ser menor que la Fecha de Fin');
                        return false;
                    }
                }
            }



            // Valida Hora   -------------------------------------------------------------------                  
            //----------------------------------------------------------------------------------


            var txt_hora_inicio = document.getElementById('<%=txt_hora_inicio.ClientID%>');
            var txt_hora_fin = document.getElementById('<%=txt_hora_fin.ClientID%>');

            if (txt_hora_inicio.value.trim() != "" && txt_hora_fin.value.trim() == "") {
                alert('Debe Ingresar la Hora Final.');
                return false;
            }
            else if (txt_hora_inicio.value.trim() == "" && txt_hora_fin.value.trim() != "") {
                alert('Debe Ingresar la Hora de Inicio.');
                return false;
            }
            else {

                strHI = txt_hora_inicio.value.split(':');
                strHF = txt_hora_fin.value.split(':');

                intHoraIni = parseInt(txt_hora_inicio.value.replace(':', ''));
                intHoraFin = parseInt(txt_hora_fin.value.replace(':', ''));

                intHI = parseInt(strHI[0]);
                intMI = parseInt(strHI[1]);

                intHF = parseInt(strHF[0]);
                intMF = parseInt(strHF[1]);

                if (intHI > 23 || intMI > 59) {
                    alert("Las horas deben estar en el Rango de 00:00 a 23:59.");
                    txt_hora_inicio.focus();
                    return false;
                }
                else if (intHF > 23 || intMF > 59) {
                    alert("Las horas deben estar en el Rango de 00:00 a 23:59.");
                    txt_hora_fin.focus();
                    return false;
                }
                else if (intHoraIni >= intHoraFin) {
                    alert("Hora de Inicio debe ser menor a Hora de Fin.");
                    txt_hora_inicio.focus();
                    return false;
                }
                else {

                }

            }

        }


        function Fc_Cambiartab(sender, e) {

            //Set Pestaña Seleccionada
            var index = 0;

            if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabDatosGenerales').style.visibility != 'hidden') index = 0;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmAdmTienda').style.visibility != 'hidden') index = 1;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmAdmTaller').style.visibility != 'hidden') index = 2;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmAsesorServicio').style.visibility != 'hidden') index = 3;
            else if (document.getElementById('ctl00_ContentPlaceHolder1_tabMantDetalleUsuarios_tabPrmOprCallCenter').style.visibility != 'hidden') index = 8;

            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOffForm('2');
            setTabCabeceraOffForm('3');
            setTabCabeceraOffForm('8');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);

        }

        function Fc_Cambiartab_AS(sender, e) {
            var CurrentTab1 = sender;
            var index = sender.get_activeTab()._tabIndex;

            onTabCabeceraOverForm('3');
            setTabCabeceraOffForm('0');
            setTabCabeceraOffForm('1');
            setTabCabeceraOffForm('2');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);
        }

        function Fc_Cambiartab_CC(sender, e) {
            var CurrentTab1 = sender;
            var index = sender.get_activeTab()._tabIndex + 9;

            setTabCabeceraOffForm('9');
            setTabCabeceraOffForm('10');
            setTabCabeceraOnForm(index);
            onTabCabeceraOverForm(index);
        }

    </script>

    <asp:UpdatePanel ID="Upd_GENERAL" runat="server">
        <ContentTemplate>
            <table style="height: 47px" cellspacing="0" cellpadding="2" width="1000" border="0">
                <tbody>
                    <tr>
                        <td>
                            <table id="tblIconos" class="TablaIconosMantenimientos" cellspacing="0" cellpadding="0"
                                border="0">
                                <tbody>
                                    <tr>
                                        <td style="width: 100%; height: 27px" align="right">
                                            <asp:ImageButton ID="btnEditar" onmouseover="javascript:this.src='../Images/iconos/b-registrofecha2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-registrofecha.gif'" OnClick="btnEditar_Click"
                                                runat="server" ImageUrl="~/Images/iconos/b-registrofecha.gif" ToolTip="Editar"></asp:ImageButton>
                                            <asp:ImageButton ID="btnGrabar" onmouseover="javascript:this.src='../Images/iconos/b-guardar2.gif'"
                                                onmouseout="javascript:this.src='../Images/iconos/b-guardar.gif'" OnClick="btnGrabar_Click"
                                                runat="server" OnClientClick="javascript:return Validar_Datos();" ImageUrl="~/Images/iconos/b-guardar.gif"
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
                            <asp:UpdatePanel ID="upDetalleUsuario" runat="server">
                                <ContentTemplate>
                                    <cc1:TabContainer ID="tabMantDetalleUsuarios" runat="server" OnClientActiveTabChanged="Fc_Cambiartab"
                                        CssClass="" ActiveTabIndex="0">
                                        <cc1:TabPanel runat="server" HeaderText="Datos Generales" ID="tabDatosGenerales">
                                            <HeaderTemplate>
                                                <table id="tblHeader0" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                        <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('0');"
                                                            onmouseout="javascript:onTabCabeceraOutForm('0');">Datos Generales</td>
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
                                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                                    cellpadding="1" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="UpdDG" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table style="width: 960px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 15%">Tipo de Documento
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_tipo_doc" runat="server" Width="189px" OnSelectedIndexChanged="ddl_tipo_doc_SelectedIndexChanged"
                                                                                                            AutoPostBack="True">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>Nro. Documento
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_nro_documento" runat="server" SkinID="txtob" MaxLength="8"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Nombres
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_nombres" runat="server" SkinID="txtob" Width="269px" MaxLength="100"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>Ape. Paterno
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_ape_paterno" runat="server" SkinID="txtob" Width="234px" MaxLength="50"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Ape. Materno
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_ape_materno" runat="server" Width="268px" MaxLength="50"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td colspan="2"></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <hr style="height: 1px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 15%;">Login
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_login" runat="server" SkinID="txtob" MaxLength="50"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>Contraseña
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:TextBox onblur="javascript:Guardar_Pass()" ID="txt_contraseña" runat="server"
                                                                                                                    TextMode="Password" MaxLength="20" SkinID="txtob"></asp:TextBox><asp:HiddenField
                                                                                                                        ID="hid_contrasena" runat="server" />
                                                                                                            </ContentTemplate>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Email
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_correo" runat="server" Width="215px" MaxLength="100"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>Telef. Fijo / Móvil
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_telefono" runat="server" Width="111px" MaxLength="20"></asp:TextBox>
                                                                                                        /
                                                                                                        <asp:TextBox ID="txt_movil" runat="server" Width="110px" MaxLength="20"></asp:TextBox><cc1:MaskedEditExtender
                                                                                                            ID="mee_movil" runat="server" TargetControlID="txt_movil" Mask="99999999999999999999">
                                                                                                        </cc1:MaskedEditExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <hr style="height: 1px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <!--
                                                                                                    <td>Tipo
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_tipo" runat="server" Width="188px" OnSelectedIndexChanged="ddl_tipo_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    -->
                                                                                                    <td>Perfil
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_perfil" runat="server" AutoPostBack="True" SkinID="cboob"
                                                                                                            Width="239px" OnSelectedIndexChanged="ddl_perfil_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoDepaDG" runat="server" Text="Departamento"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_departamento_dg" runat="server" AutoPostBack="True" SkinID="cboob"
                                                                                                            Width="246px" OnSelectedIndexChanged="ddl_departamento_dg_SelectedIndexChanged">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoProvDG" runat="server" Text="Provincia"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:DropDownList ID="ddl_provincia_dg" runat="server" OnSelectedIndexChanged="ddl_provincia_dg_SelectedIndexChanged"
                                                                                                                    Width="239px" SkinID="cboob" AutoPostBack="True">
                                                                                                                </asp:DropDownList>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddl_departamento_dg" EventName="SelectedIndexChanged" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoDistDG" runat="server" Text="Distrito"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:DropDownList ID="ddl_distrito_dg" runat="server" OnSelectedIndexChanged="ddl_distrito_dg_SelectedIndexChanged"
                                                                                                                    Width="246px" SkinID="cboob" AutoPostBack="True">
                                                                                                                </asp:DropDownList>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddl_provincia_dg" EventName="SelectedIndexChanged" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoLocalDG" runat="server" Text="Ubicación"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:DropDownList ID="ddl_ubicacion_dg" runat="server" Width="239px" SkinID="cboob"
                                                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_ubicacion_dg_SelectedIndexChanged">
                                                                                                                </asp:DropDownList>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddl_distrito_dg" EventName="SelectedIndexChanged" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Estado
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_estado" runat="server" SkinID="cboob" Width="116px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <!--
                                                                                                <tr>
                                                                                                    <td style="height: 19px" colspan="4">
                                                                                                        <hr />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Fec. Inicio Acceso
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_fec_inicio_acceso" runat="server" Width="89px" visible="false"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                            ID="btn_Calendario1" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                            ImageAlign="AbsMiddle" Visible="false"></asp:ImageButton>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fec_inicio_acceso"
                                                                                                            Format="dd/MM/yyyy" PopupButtonID="btn_Calendario1">
                                                                                                        </cc1:CalendarExtender>
                                                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fec_inicio_acceso"
                                                                                                            Mask="99/99/9999" MaskType="Date">
                                                                                                        </cc1:MaskedEditExtender>
                                                                                                    </td>
                                                                                                    
                                                                                                    <td>Fec. Fin Acceso
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_fin_acceso" runat="server" Width="86px" MaxLength="10" Visible="false"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                                            ID="btn_calendario2" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                            ImageAlign="AbsMiddle" Visible="false"></asp:ImageButton><cc1:CalendarExtender ID="CalendarExtender2"
                                                                                                                runat="server" TargetControlID="txt_fin_acceso" Format="dd/MM/yyyy" PopupButtonID="btn_calendario2">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_fin_acceso"
                                                                                                            Mask="99/99/9999" MaskType="Date">
                                                                                                        </cc1:MaskedEditExtender>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Hora Inicio
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_hora_inicio" runat="server" MaxLength="5" Columns="8" Visible="false"></asp:TextBox>
                                                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender31" runat="server" TargetControlID="txt_hora_inicio"
                                                                                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                                                            MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                                                                    </td>
                                                                                                    <td>Hora Fin
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txt_hora_fin" runat="server" MaxLength="5" Columns="8" Visible="false"></asp:TextBox>
                                                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender41" runat="server" TargetControlID="txt_hora_fin"
                                                                                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                                                            MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Mensaje
                                                                                                    </td>
                                                                                                    <td colspan="3">
                                                                                                        <asp:TextBox ID="txt_mensaje" runat="server" Width="575px" TextMode="MultiLine"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4" style="height: 19px">
                                                                                                        <hr />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="chk_bloqueado" runat="server" Width="108px" Text="Bloqueado" TextAlign="Left"></asp:CheckBox>
                                                                                                    </td>
                                                                                                    <td align="right">&nbsp;
                                                                                                        <asp:CheckBox ID="chk_no_disponible" runat="server" Text="No Disponible" TextAlign="Left"></asp:CheckBox>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td>
                                                                                                        <asp:CheckBox ID="chk_consulta_VIN" runat="server" Text="Consulta de VIN" TextAlign="Left"></asp:CheckBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                -->
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:HiddenField ID="hdf_pass_nuevo" runat="server"></asp:HiddenField>
                                                                                                        <asp:HiddenField ID="hf_hayerror" runat="server"></asp:HiddenField>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="tabMantDetalleUsuarios" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
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
                                        <cc1:TabPanel runat="server" HeaderText="Administrador de Tienda" ID="tabPrmAdmTienda">
                                            <HeaderTemplate>
                                                <table id="tblHeader1" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                        <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('1');"
                                                            onmouseout="javascript:onTabCabeceraOutForm('1');">Administrador de Tienda
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
                                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                                    cellpadding="1" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="height: 131px">
                                                                                <asp:UpdatePanel ID="UpdPrmAdmTienda_Usuario" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table style="width: 950px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 13%">
                                                                                                        <asp:Label ID="lblTextoDepaTND" runat="server" Text="Departamento"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 30%">
                                                                                                        <asp:DropDownList ID="ddl_departamento_tienda" runat="server" OnSelectedIndexChanged="ddl_departamento_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="width: 12%">
                                                                                                        <asp:Label ID="lblTextoProvTND" runat="server" Text="Provincia"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_provincia_tienda" runat="server" OnSelectedIndexChanged="ddl_provincia_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoDistTND" runat="server" Text="Distrito"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_distrito_tienda" runat="server" OnSelectedIndexChanged="ddl_distrito_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoTipoLocalTND" runat="server" Text="Punto de Red"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_tipoptored_tienda" runat="server" OnSelectedIndexChanged="ddl_tipoptored_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <asp:Label ID="lblTextoLocalTND" runat="server" Text="Puntos de Red"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ListBox ID="lst_ptored_tienda" runat="server" Width="230px" Height="133px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <table align="center">
                                                                                                            <tr>
                                                                                                                <td align="center">
                                                                                                                    <asp:ImageButton Style="position: static" ID="btn_del_ptored_tienda" OnClick="btn_del_ptored_tienda_Click"
                                                                                                                        runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="center">
                                                                                                                    <asp:ImageButton ID="btn_add_ptored_t" OnClick="btn_add_ptored_t_Click" runat="server"
                                                                                                                        ImageUrl="~/Images/iconos/derechas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ListBox ID="lst_ptored_sel_tienda" runat="server" Width="230px" Height="132px"
                                                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <hr style="height: 1px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Empresa
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_empresa_tienda" runat="server" OnSelectedIndexChanged="ddl_empresa_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Marca
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_marca_tienda" runat="server" OnSelectedIndexChanged="ddl_marca_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>Linea Comercial
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_linea_comercial_tienda" runat="server" OnSelectedIndexChanged="ddl_linea_comercial_tienda_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">Modelos
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ListBox ID="lst_modelos_tienda" runat="server" Width="230px" Height="119px"
                                                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                    <td style="text-align: left" align="left">
                                                                                                        <table align="center" style="text-align: left">
                                                                                                            <tr>
                                                                                                                <td align="center">
                                                                                                                    <asp:ImageButton Style="position: static" ID="btn_del_modelo_tienda" OnClick="btn_del_modelo_tienda_Click"
                                                                                                                        runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td></td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="center">
                                                                                                                    <asp:ImageButton ID="btn_add_modelo_tienda" OnClick="btn_add_modelo_tienda_Click"
                                                                                                                        runat="server" ImageUrl="~/Images/iconos/derechas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:ListBox ID="lst_modelo_sel_tienda" runat="server" Width="230px" Height="119px"
                                                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="tabMantDetalleUsuarios" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                                <asp:HiddenField ID="hdf_pass" runat="server" Value="nuevo" />
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
                                        <cc1:TabPanel runat="server" HeaderText="Admnistrador Taller" ID="tabPrmAdmTaller">
                                            <HeaderTemplate>
                                                <table id="tblHeader2" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                        <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('2');"
                                                            onmouseout="javascript:onTabCabeceraOutForm('2');">Administrador de Taller
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
                                                                <img alt="" src="../Images/Tabs/borarriba.gif" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: top; height: 450px; background-color: #ffffff">
                                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                                    cellpadding="1" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="UpdPrmAdmTaller" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table style="width: 950px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                            <tbody>
                                                                                                <tr>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 13%">
                                                                                                        <asp:Label ID="lblTextoDepaTAL" runat="server" Text="Departamento"></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="width: 30%">
                                                                                                        <asp:DropDownList ID="ddl_departamento_taller" runat="server" OnSelectedIndexChanged="ddl_departamento_taller_SelectedIndexChanged"
                                                                                                            Width="233px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="width: 12%">
                                                                                                        <asp:Label ID="lblTextoProvTAL" runat="server" Text="Provincia"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_provincia_taller" runat="server" OnSelectedIndexChanged="ddl_provincia_taller_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoDistTAL" runat="server" Text="Distrito"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_distrito_taller" runat="server" OnSelectedIndexChanged="ddl_distrito_taller_SelectedIndexChanged"
                                                                                                            Width="233px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTextoLocalTAL" runat="server" Text="Puntos de Red"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_ptored_taller" runat="server" OnSelectedIndexChanged="ddl_ptored_taller_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <asp:Label ID="lblTextoTallerTAL" runat="server" Text="Talleres"></asp:Label>
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <asp:ListBox ID="lst_talleres_t" runat="server" Width="232px" Height="110px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                    <td>&nbsp;<table align="center">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <asp:ImageButton Style="position: static" ID="btn_del_taller_t" OnClick="btn_del_taller_t_Click"
                                                                                                                    runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <asp:ImageButton Style="position: static" ID="btn_add_taller_t" OnClick="btn_add_taller_t_Click"
                                                                                                                    runat="server" ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <asp:ListBox ID="lst_talleres_sel_t" runat="server" Width="230px" Height="110px"
                                                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
                                                                                                        <hr style="height: 1px" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Empresa
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_empresa_taller" runat="server" OnSelectedIndexChanged="ddl_empresa_taller_SelectedIndexChanged"
                                                                                                            Width="232px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td></td>
                                                                                                    <td></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Marca
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_marca_taller" runat="server" OnSelectedIndexChanged="ddl_marca_taller_SelectedIndexChanged"
                                                                                                            Width="232px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>Línea Comercial
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="ddl_lineacomercial_taller" runat="server" OnSelectedIndexChanged="ddl_lineacomercial_taller_SelectedIndexChanged"
                                                                                                            Width="230px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td valign="top">Modelos
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <asp:ListBox ID="lst_modelos_taller" runat="server" Width="232px" Height="111px"
                                                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                    <td>&nbsp;<table align="center">
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <asp:ImageButton Style="position: static" ID="btn_del_modelo_t" OnClick="btn_del_modelo_t_Click"
                                                                                                                    runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td align="center">
                                                                                                                <asp:ImageButton Style="position: static" ID="btn_add_modelo_t" OnClick="btn_add_modelo_t_Click"
                                                                                                                    runat="server" ImageUrl="~/Images/iconos/derechas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    </td>
                                                                                                    <td valign="top">
                                                                                                        <asp:ListBox ID="lst_modelos_sel_taller" runat="server" Width="230px" Height="111px"
                                                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="tabMantDetalleUsuarios" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
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
                                        <cc1:TabPanel runat="server" HeaderText="Asesor de Servicio" ID="tabPrmAsesorServicio">
                                            <HeaderTemplate>
                                                <table id="tblHeader3" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                        <td id="td_AsesorServicio" runat="server" class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('3');"
                                                            onmouseout="javascript:onTabCabeceraOutForm('3');"><%-- @001 IF !--%>
                                                            Asesor de Servicio
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
                                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                                    cellpadding="1" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="upAsesorServicios" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <cc1:TabContainer ID="tabPrmAsesServ" runat="server" OnClientActiveTabChanged="Fc_Cambiartab_AS"
                                                                                            CssClass="" ActiveTabIndex="0">
                                                                                            <cc1:TabPanel runat="server" HeaderText="Taller" ID="tabAsesServTaller">
                                                                                                <HeaderTemplate>
                                                                                                    <table id="tblHeader4" cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                                                                            <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('4');"
                                                                                                                onmouseout="javascript:onTabCabeceraOutForm('4');">Taller
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:UpdatePanel ID="UpdPrmAsesServTaller" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table style="width: 950px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 13%">
                                                                                                                            <asp:Label ID="lblTextoDepaASE" runat="server" Text="Departamento"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td style="width: 30%">
                                                                                                                            <asp:DropDownList ID="ddl_departamento_ase_serv_t" runat="server" Enabled="False"
                                                                                                                                Width="230px" OnSelectedIndexChanged="ddl_departamento_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 12%">
                                                                                                                            <asp:Label ID="lblTextoProvASE" runat="server" Text="Provincia"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_provincia_ases_serv_t" runat="server" Enabled="False" Width="230px"
                                                                                                                                OnSelectedIndexChanged="ddl_provincia_ases_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTextoDistASE" runat="server" Text="Distrito"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_distrito_ase_serv_t" runat="server" Enabled="False" Width="230px"
                                                                                                                                OnSelectedIndexChanged="ddl_distrito_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTextoLocalASE" runat="server" Text="Puntos de Red"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_ptored_ase_serv_t" runat="server" Enabled="False" Width="230px"
                                                                                                                                OnSelectedIndexChanged="ddl_ptored_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTextoTallerASE" runat="server" Text="Talleres"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_taller_ase_serv_t" runat="server" AutoPostBack="True" Width="230px"
                                                                                                                                OnSelectedIndexChanged="ddl_taller_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblModulo" runat="server" Text="Módulo"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddlModulo" runat="server" Width="60px"></asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="4">
                                                                                                                            <hr style="height: 1px" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <!-- @001 IF -->
                                                                                                                    <tr id="trDatosEmpresa_Ase_Serv_1" runat="server">
                                                                                                                        <td>Empresa
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_empresa_ase_serv_t" runat="server" Width="230px" OnSelectedIndexChanged="ddl_empresa_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <!-- @001 IF -->
                                                                                                                    <tr id="trDatosEmpresa_Ase_Serv_2" runat="server">
                                                                                                                        <td>Marca
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_marca_ase_serv_t" runat="server" Width="230px" OnSelectedIndexChanged="ddl_marca_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>Línea Comercial
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_linea_comercial_ase_serv_t" runat="server" Width="230px"
                                                                                                                                OnSelectedIndexChanged="ddl_linea_comercial_ase_serv_t_SelectedIndexChanged">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>

                                                                                                                    <!-- @001 IF -->
                                                                                                                    <tr id="trDatosEmpresa_Ase_Serv_3" runat="server">
                                                                                                                        <td valign="top">Modelos
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_modelos_ase_serv_t" runat="server" Width="230px" SelectionMode="Multiple"
                                                                                                                                Height="111px"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td>&nbsp;<table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                            <tbody>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton Style="position: static" ID="btn_del_modelo_ase_serv_t" OnClick="btn_del_modelo_ase_serv_t_Click"
                                                                                                                                            runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_modelo_ase_serv_t" OnClick="btn_add_modelo_ase_serv_t_Click1"
                                                                                                                                            runat="server" ImageUrl="~/Images/iconos/derechas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </tbody>
                                                                                                                        </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_modelos_sel_ase_serv_t" runat="server" Width="230px" SelectionMode="Multiple"
                                                                                                                                Height="111px"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </ContentTemplate>
                                                                                            </cc1:TabPanel>
                                                                                            <cc1:TabPanel runat="server" HeaderText="Horario" ID="tabAsesServHorario">
                                                                                                <HeaderTemplate>
                                                                                                    <table id="tblHeader5" cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                                                                            <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('5');"
                                                                                                                onmouseout="javascript:onTabCabeceraOutForm('5');">Horario
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:UpdatePanel ID="UpdPrmAsesServHorarios" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table style="width: 950px;" class="textotab">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td align="left"></td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 15%;" valign="middle">Día de la Semana
                                                                                                                        </td>
                                                                                                                        <td style="width: 15%" valign="middle">
                                                                                                                            <asp:DropDownList ID="ddl_dias_semana" runat="server" Width="150px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%">
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton Style="position: static" ID="btn_del_dia_semana" OnClick="btn_del_dia_semana_Click"
                                                                                                                                            runat="server" ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 10px"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton Style="position: static" ID="btn_add_dia_semana" OnClick="btn_add_dia_semana_Click"
                                                                                                                                            runat="server" ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:ListBox ID="lst_dias_sel" runat="server" Width="367px" Height="101px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Día
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_dia" runat="server" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="ddl_dia_SelectedIndexChanged"
                                                                                                                                OnPreRender="ddl_dia_PreRender">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td colspan="2">
                                                                                                                            <asp:Panel ID="pnl_Capacidad" runat="server">
                                                                                                                                <table style="width: 525px; margin-left: 35px;" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 30%">Capacidad de Atención</td>
                                                                                                                                        <td style="width: 15%">FO&nbsp;<asp:TextBox ID="txt_capacidad_fo" runat="server" Width="30px" MaxLength="3"></asp:TextBox></td>
                                                                                                                                        <td style="width: 15%">BO &nbsp;<asp:TextBox ID="txt_capacidad_bo" runat="server" Width="30px" MaxLength="3"></asp:TextBox></td>
                                                                                                                                        <td>
                                                                                                                                            <asp:CheckBox ID="chkTotal" TextAlign="Left" runat="server" onclick="javascript:Fc_CheckT(this,'T');" Style="margin-top: 5px;" Text="Todos" />
                                                                                                                                            <asp:TextBox ID="txt_capacidad" runat="server" Width="30px" MaxLength="3"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </asp:Panel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="Label55" runat="server" Text="Horas al Día" Font-Bold="True" Font-Names="verdana"
                                                                                                                                Font-Size="10pt"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Inicio
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_hora_inicio" runat="server" Width="150px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td rowspan="2" valign="middle">
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_hora" OnClick="btn_del_hora_Click" runat="server" ImageUrl="~/Images/iconos/izquierdas.gif"
                                                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 10px"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_hora" OnClick="btn_add_hora_Click" runat="server" ImageUrl="~/Images/iconos/derechas.gif"
                                                                                                                                            Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td rowspan="2">
                                                                                                                            <asp:ListBox ID="lst_horas_sel" runat="server" Width="371px" Height="79px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                            <br />
                                                                                                                            * Incluye Gráfico
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Fin
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_hora_fin" runat="server" Width="150px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Días Exceptuados
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txt_dia_exceptuado" runat="server" Width="100px"></asp:TextBox>
                                                                                                                            <asp:ImageButton ID="btn_calendario3" runat="server" ImageUrl="~/Images/iconos/calendario.gif"
                                                                                                                                AlternateText="Fecha" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_dia_exceptuado"
                                                                                                                                PopupButtonID="btn_calendario3" Format="dd/MM/yyyy">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                                                                                                TargetControlID="txt_dia_exceptuado" MaskType="Date">
                                                                                                                            </cc1:MaskedEditExtender>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_dia_excep" OnClick="btn_del_dia_excep_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/izquierdas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 10px"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_dia_excep" OnClick="btn_add_dia_excep_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/derechas.gif" Width="20px" Height="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td rowspan="1">
                                                                                                                            <asp:ListBox ID="lst_dias_excep" runat="server" Width="371px" Height="99px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </ContentTemplate>
                                                                                            </cc1:TabPanel>
                                                                                            <cc1:TabPanel runat="server" HeaderText="Servicios" ID="tabAsesServ_S">
                                                                                                <HeaderTemplate>
                                                                                                    <table id="tblHeader7" cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                                                                            <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('7');"
                                                                                                                onmouseout="javascript:onTabCabeceraOutForm('7');">Servicios
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:UpdatePanel ID="UpdPrmAsesServ_Serv" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table style="width: 951px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td valign="top"></td>
                                                                                                                        <td valign="top"></td>
                                                                                                                        <td></td>
                                                                                                                        <td valign="top"></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 13%;" valign="top">Tipo de Servicio
                                                                                                                        </td>
                                                                                                                        <td style="width: 30%;" valign="top">
                                                                                                                            <asp:ListBox ID="lst_tipo_serv" runat="server" Height="111px" Width="229px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;">
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_tipo_serv" OnClick="btn_del_tipo_serv_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 15px;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_tipo_serv" OnClick="btn_add_tipo_serv_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_tipo_serv_sel" runat="server" Height="111px" Width="222px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Tipo
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_tipo_servicio_s" runat="server" Width="194px" OnSelectedIndexChanged="ddl_tipo_servicio_s_SelectedIndexChanged"
                                                                                                                                AutoPostBack="True">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">Servicio
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_servicio_espec_s" runat="server" Height="111px" Width="229px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_serv" OnClick="btn_del_serv_Click" runat="server" ImageUrl="~/Images/iconos/izquierdas.gif"
                                                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 15px;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_serv" OnClick="btn_add_serv_Click" runat="server" ImageUrl="~/Images/iconos/derechas.gif"
                                                                                                                                            Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_servicio_espec_sel_s" runat="server" Height="111px" Width="222px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </ContentTemplate>
                                                                                            </cc1:TabPanel>
                                                                                        </cc1:TabContainer>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                <asp:HiddenField ID="hfCapacidad" runat="server"></asp:HiddenField>
                                                                                <asp:HiddenField ID="hfDiaAtencion" runat="server"></asp:HiddenField>
                                                                                &nbsp;
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
                                        <cc1:TabPanel runat="server" HeaderText="Operador CallCenter" ID="tabPrmOprCallCenter">
                                            <HeaderTemplate>
                                                <table id="tblHeader8" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                        <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('8');"
                                                            onmouseout="javascript:onTabCabeceraOutForm('8');">Operador call center
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
                                                            <td style="vertical-align: top; background-color: #ffffff">
                                                                <table style="margin-left: 5px; margin-right: 5px; background-color: #ffffff" cellspacing="1"
                                                                    cellpadding="1" width="970" border="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="upOperadorCallCenter" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <cc1:TabContainer ID="tabPrmCallCenter" runat="server" OnClientActiveTabChanged="Fc_Cambiartab_CC"
                                                                                            CssClass="" ActiveTabIndex="0">
                                                                                            <cc1:TabPanel runat="server" HeaderText="Servicios" ID="tabCallCenter_Taller">
                                                                                                <HeaderTemplate>
                                                                                                    <table id="tblHeader9" cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                                                                            <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('9');"
                                                                                                                onmouseout="javascript:onTabCabeceraOutForm('9');">taller
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table style="width: 950px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 13%;">
                                                                                                                            <asp:Label ID="lblTextoDepaCALL" runat="server" Text="Departamento"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:DropDownList ID="ddl_departamento_call" runat="server" OnSelectedIndexChanged="ddl_departamento_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 12%;">
                                                                                                                            <asp:Label ID="lblTextoProvCALL" runat="server" Text="Provincia"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_provincia_call" runat="server" OnSelectedIndexChanged="ddl_provincia_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTextoDistCALL" runat="server" Text="Distrito"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_distrito_call" runat="server" OnSelectedIndexChanged="ddl_distrito_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTextoLocalCALL" runat="server" Text="Puntos de Red"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_ptored_call" runat="server" OnSelectedIndexChanged="ddl_ptored_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:Label ID="lblTextoTallerCALL" runat="server" Text="Talleres"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_talleres_call" runat="server" Width="230px" Height="111px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td>&nbsp;
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_taller_call" OnClick="btn_del_taller_call_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 10px;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_taller_call" OnClick="btn_add_taller_call_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_talleres_sel_call" runat="server" Width="230px" Height="111px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="4">
                                                                                                                            <hr style="height: 1px" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Empresa
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_empresa_call" runat="server" OnSelectedIndexChanged="ddl_empresa_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Marca
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_marca_call" runat="server" OnSelectedIndexChanged="ddl_marca_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td>Línea Comercial
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_linea_comercial_call" runat="server" OnSelectedIndexChanged="ddl_linea_comercial_call_SelectedIndexChanged"
                                                                                                                                Width="230px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">Modelo
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_modelos_call" runat="server" Width="230px" Height="109px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td>&nbsp;<table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                            <tr>
                                                                                                                                <td align="center">
                                                                                                                                    <asp:ImageButton ID="btn_del_modelo_call" OnClick="btn_del_modelo_call_Click" runat="server"
                                                                                                                                        ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td style="height: 10px;"></td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td align="center">
                                                                                                                                    <asp:ImageButton ID="btn_add_modelo_call" OnClick="btn_add_modelo_call_Click" runat="server"
                                                                                                                                        ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_modelos_sel_call" runat="server" Width="230px" Height="110px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </ContentTemplate>
                                                                                            </cc1:TabPanel>
                                                                                            <cc1:TabPanel runat="server" HeaderText="Servicios" ID="tabCallCenter_Servicios">
                                                                                                <HeaderTemplate>
                                                                                                    <table id="tblHeader10" cellpadding="0" cellspacing="0" border="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-izq_off_plom.gif" /></td>
                                                                                                            <td class="TabCabeceraOffForm" onmouseover="javascript:onTabCabeceraOverForm('10');"
                                                                                                                onmouseout="javascript:onTabCabeceraOutForm('10');">servicios
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img alt="" src="../Images/Tabs/tab-der_off_plom.gif" /></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <table style="width: 951px;" class="textotab" border="0" cellpadding="2" cellspacing="1">
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td valign="top"></td>
                                                                                                                        <td valign="top"></td>
                                                                                                                        <td></td>
                                                                                                                        <td valign="top"></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 13%;" valign="top">Tipo de Servicio
                                                                                                                        </td>
                                                                                                                        <td style="width: 30%;" valign="top">
                                                                                                                            <asp:ListBox ID="lst_tipo_serv_cc" runat="server" Height="111px" Width="229px" SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;">
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_tipo_serv_cc" OnClick="btn_del_tipo_serv_cc_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 15px;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_tipo_serv_cc" OnClick="btn_add_tipo_serv_cc_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_tipo_serv_sel_cc" runat="server" Height="111px" Width="222px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>Tipo
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:DropDownList ID="ddl_tipo_servicio_cc" runat="server" Width="194px" OnSelectedIndexChanged="ddl_tipo_servicio_cc_SelectedIndexChanged"
                                                                                                                                AutoPostBack="True">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">Servicio
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_servicio_espec_cc" runat="server" Height="111px" Width="229px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_del_serv_cc" OnClick="btn_del_serv_cc_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/izquierdas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td style="height: 15px;"></td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="center">
                                                                                                                                        <asp:ImageButton ID="btn_add_serv_cc" OnClick="btn_add_serv_cc_Click" runat="server"
                                                                                                                                            ImageUrl="~/Images/iconos/derechas.gif" Height="20px" Width="20px"></asp:ImageButton>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <asp:ListBox ID="lst_servicio_espec_sel_cc" runat="server" Height="111px" Width="222px"
                                                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </ContentTemplate>
                                                                                            </cc1:TabPanel>
                                                                                        </cc1:TabContainer>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                                                                <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                                                                                &nbsp;
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">&nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- UpdateProgress -->
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
    <!-- UpdateProgress -->
    <!-- modal popup MSGBOX  -->
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
    &nbsp;

    <script type="text/javascript">
        setTabCabeceraOnForm('0');
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormPost" runat="Server">
</asp:Content>
