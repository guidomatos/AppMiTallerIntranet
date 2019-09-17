//  Mensajes SRC
//**************************************************************
    
    var SRC_SeleccioneAlmenosUno    = "Debe seleccionar por lo menos un registro.";
    
    var SRC_SoloAnularUno           = "Solo se permite Anular un registro.";
    var SRC_SoloReprogramarUno      = "Solo se permite Reprogramar un registro.";
    var SRC_SoloConfirmarUno        = "Solo se permite Confirmar un registro.";
	var SRC_SeleccioneUno 			= "Debe seleccionar un registro.";                     
	var SRC_SoloActualizarSI		= "Solo se permite citas donde el flag Ind. pendiente datos sean SI.";                     

    var SRC_NoCitasAReasignar       = "No hay citas para reasignar.";       
	var SRC_SeguroReasignar		    = "¿Está seguro de reasignar las citas?.";
	var SRC_YaReasignoCitas		    = "Las Citas ya fueron reasignadas.";
	var SRC_NoRegistros 			= "No se encontraron coincidencias.";	

	var SRC_SeguroPonerColaEspera   = "¿Está seguro de colocar la cita en cola de espera?.";
	var SRC_SeguroReprogramar	    = "¿Está seguro de reprogramar la cita?.";
	var SRC_SeguroAnular		    = "¿Está seguro de anular la cita?.";
	
 	var SRC_ColaEspera              = "Se colocó en cola de espera.";
	var SRC_Reprogramar	            = "Se reprogramó la cita.";
	var SRC_Anular		            = "Se anuló la cita.";     
	
	var SRC_SeguroEnviarEmail       = "¿Está seguro de enviar los recordatorios por Email?.";
	var SRC_SeguroEnviarEmailUno    = "¿Está seguro de enviar el recordatorios por Email?.";
    var SRC_NoListaRecordatorio     = "No hay listado de Recordatorio.";
    var SRC_SeguroEliminarArchivo   = "¿Está seguro de eliminar el archivo seleccionado ?.";
    var SRC_SeguroEliminarRegistro  = "¿Está seguro de eliminar el registro seleccionado ?.";
    var SRC_SeguroImprimirRecord    = "¿Está seguro de imprimir los recordatorios?.";
    var SRC_SeguroImprimirRecordUno = "¿Está seguro de imprimir el recordatorio?.";
    var SRC_SeguroImportarExcel     = "¿Está seguro importar el Excel seleccionado?.";
    var SRC_NoExisteExcelImportar = "No existe Excel para importar.";
    var SRC_Email_Invalido = "El Email ingresado no es válido.";
    var SRC_Email_Facturacion_Invalido = "El Email de facturación ingresado no es válido.";
    var SRC_Telefono_Fijo_Invalido = "El número de Teléfono Fijo ingresado no es válido.";
    var SRC_Telefono_Movil_Invalido = "El número de Teléfono Movil ingresado no es válido.";


// Archivo JScript
function fn_SoloLetras(eventObj)
{        
    var key;
    if(eventObj.keyCode)           // For IE
        key = eventObj.keyCode;
    else if(eventObj.Which)
        key = eventObj.Which;       // For FireFox
    else
        key = eventObj.charCode;    // Other Browser                  
           
            
         if (key >= 65 &&  key <= 90){}
    else if (key >= 97 &&  key <= 122){}  
    else if (key==32) {}      
    else{            
        return false;  // anula la entrada de texto. 
    }        
}

/* ------------------------------------------- */

function SoloNumeros(eventObj){    
    var key;
    if(eventObj.keyCode)            // For IE
        key = eventObj.keyCode;
    else if(eventObj.Which)
        key = eventObj.Which;       // For FireFox
    else
        key = eventObj.charCode;    // Other Browser  
        
    if (key >=48  && key <=57 ){} 
    else
    {
        return false;//anula la entrada de texto. 
    }
}

function SoloLetrasNumeros(eventObj){
    var key;
    if(eventObj.keyCode)           // For IE
        key = eventObj.keyCode;
    else if(eventObj.Which)
        key = eventObj.Which;       // For FireFox
    else
        key = eventObj.charCode;    // Other Browser  
    
         if (key >= 65 &&  key <= 90){}
    else if (key >= 97 &&  key <= 122){}  
    else if (key >=48  &&  key <=57 ){}  
    else if (key==32) {}
    else{
        return false;//anula la entrada de texto. 
    }
}

function SoloLetrasyEspacio(e)
{        
    var key= (e.keyCode)? e.keyCode: ( (e.Which)?  e.Which: e.charCode );
             
          if (key >= 65 &&  key <= 90){}
     else if (key == 32  || key == 8  ||  key == 9  ||  key == 46  ||  key == 37  ||  key == 39  ){} 
     else if (key == 193 || key == 201 || key == 205 || key == 211 || key == 218 ){} // Á  É  Í  Ó  Ú
     else if (key == 225 || key == 223 || key == 237 || key == 243 || key == 250 ){} // á  é  í  ó  ú
     else if (key >= 97  &&  key <= 122){}        
     else if (key == 209 || key == 241){} // ñ Ñ
     else{            
         return false;
     }        
}
    

//------------------------------------

//var RE_EMAIL = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/i;

function validaEmail(nombreElemento)
{
    var s = nombreElemento.value;
    //var filter=/^[A-Za-z][A-Za-z0-9_.-]*@[A-Za-z0-9_-]+\.[A-Za-z0-9_.-]+[A-za-z]$/;
    var filter=/^[A-Za-z0-9][A-Za-z0-9_.-]*@[A-Za-z0-9_-]+\.[A-Za-z0-9_.-]+[A-za-z]$/;
    if (s.length == 0 ) 
    {
        alert("Ingrese una dirección de correo válida");return false;
    }
    else
    {
        if (filter.test(s))
        {
            return true;
        }
        else
        {   alert("Ingrese una dirección de correo válida");
            nombreElemento.focus();
            return false;           
        }
    }
}

function fc_Trim(pstrInput) {
	var i;
	var vstrTemp = '';
	var j = 0;
	var cadena = pstrInput;
	
	for(i=0; i<cadena.length; )
	    {
		    if(cadena.charAt(i)==" ")
			    cadena=cadena.substring(i+1, cadena.length);
		    else
			    break;
	    }

	    for(i=cadena.length-1; i>=0; i=cadena.length-1)
	    {
		    if(cadena.charAt(i)==" ")
			    cadena=cadena.substring(0,i);
		    else
			    break;
	    }

	return cadena;

}

function valida_telefono(control) {
    var expresion = /^([0-9]+)$/;
    if (control.value == "") return true;
    else if (isNaN(control.value) || !expresion.test(control.value)) {
        return false;
    }
    else if (control.value.length < 5) { return false; }
    else {
        return validateInfoBlackList("002", control.value); 
    }
}
function valida_celular(control, nacional) {
    var expresion = "";
    if (nacional) expresion = /^9[0-9]{8}$/;
    else expresion = /^[0-9]{9}$/;

    if (control.value == "") return true;
    else if (isNaN(control.value) || !expresion.test(control.value)) {
        return false;
    }
    if (control.value.length < 5) return false;

    if (!validateInfoBlackList("002", control.value)) return false; 
    return true;
}

var items_blacklist = [];

function valida_email_blacklist(nombreElemento) {
    var s = nombreElemento.value;
    //var filter=/^[A-Za-z][A-Za-z0-9_.-]*@[A-Za-z0-9_-]+\.[A-Za-z0-9_.-]+[A-za-z]$/;
    var filter = /^[A-Za-z0-9][A-Za-z0-9_.-]*@[A-Za-z0-9_-]+\.[A-Za-z0-9_.-]+[A-za-z]$/;
    if (s.length == 0) {
        return true;
    }
    else {
        if (filter.test(s)) {
            return validateInfoBlackList("002", nombreElemento.value);
        }
        else {
            return false;
        }
    }
}

function validateInfoBlackList(tipo, valor) {
    var tmp = items_blacklist;
    for (var x = 0; x < tmp.length; x++) {
        if (tmp[x].no_descripcion == valor && tmp[x].co_tipo_dato == tipo) {
            return false;
        }
    }
    return true;
}