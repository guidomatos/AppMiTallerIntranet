/*Para Validar el INPUT FILE*/

/*Valida la ruta; que exista y que tenga la extension excel correcta*/
function fc_UploadFile_Validar(idUpload) {    
    var file = document.getElementById(idUpload); 
    var fileName=file.value; 

    fileName = fileName.slice(fileName.lastIndexOf("\\") + 1);   
    tipoError = -1;
    if ( fileName.length > 0 )
    {
        var ext = (fileName.toLowerCase().substring(fileName.lastIndexOf(".")))
        var listado = new Array(".xls",".csv");
        var ok = false;
        for (var i = 0; i < listado.length; i++){
            if (listado [i] == ext) { 
                ok = true;
                break; 
            }
        } 
        if(ok){                       
            tipoError = 0;
        }else{
            tipoError = 2;
        }
    }
    else
    {    
        tipoError = 1;   
    }
    return tipoError; 
}    
/*Limpia el control UploadFile*/
function fc_UploadFile_Limpiar(idUpload){
    f = document.getElementById(idUpload);
    nuevoFile = document.createElement('input');
    nuevoFile.id = f.id;
    nuevoFile.type = 'file';
    nuevoFile.name = f.name;
    nuevoFile.value = '';
    nuevoFile.className = f.className;
    nuevoFile.onchange = f.onchange;
    nuevoFile.style.width = f.style.width;
    nodoPadre = f.parentNode;
    nodoSiguiente = f.nextSibling;
    nodoPadre.removeChild(f);
    (nodoSiguiente == null) ? nodoPadre.appendChild(nuevoFile):
    nodoPadre.insertBefore(nuevoFile, nodoSiguiente);
}
/*Envia el mensaje de error del UploadFile*/
function fc_UploadFile_SendMsg(pResult){
    mstrError = "";
    switch(pResult){
        case 0: return ""; break;
        case 1: mstrError = "Seleccione un archivo para la carga."; break;
        case 2: mstrError = "Solo se admite la extensi\xf3n [.xls]."; break;
        default: mstrError = "Error al seleccionar el archivo."; break;
    }
    //alert(mstrError);
    return mstrError;
}
/*Valida la longitud en el texarea*/
function fc_MaxLength(pTextArea,num_caracteres_permitidos){ 
   contenido_textarea = pTextArea.value;
   num_caracteres = pTextArea.value.length 
   if (num_caracteres > num_caracteres_permitidos){ 
      pTextArea.value = pTextArea.value.substring(0,num_caracteres_permitidos)
   } 
} 
/* Redondea el valor de una expresion */
function fc_Round(num, dec) {
    num = (num+"")
    num = num.substring(0,num.indexOf(".")+dec+1);            
    var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
    return result;
}
/*WT: Valida si es numero entero*/
function fc_IsNumberInt(num){
    if(num==""){
        return false;
    }else{    
        m = "0123456789";
        for(i=0;i<num.length;i++){
            if(m.indexOf(num.split('')[i])==-1)
                return false;
        }
    }    
    return true;
}
/*Obtiene las coordenadas de un objeto*/
function fc_getCoordenadas(o) {
	cords = new Object();
	cords.x = o.offsetLeft;
	cords.y = o.offsetTop;
		
	j18 = o.offsetParent;
	while(j18 !=null) {
		cords.y += j18.offsetTop;
		cords.x += j18.offsetLeft;
		j18 = j18.offsetParent;
	}
	return cords;
}
/*Selecciona la fila de una tabla*/
function fc_SeleccionarFila(oTbl, indiceRow)
{
    oTabla = document.getElementById(oTbl);
    if(oTabla!=null){
        indiceRow = indiceRow*1;
        for(i=1;i<oTabla.rows.length;i++){
            if(i==(indiceRow+1)){
                oTabla.rows[i].style.backgroundColor = "#c4e4ff"
            }
            else
                oTabla.rows[i].style.backgroundColor = ""
        }
    }
}
/*Selecciona un Grupo de Checks segun el ID de un padre*/
function SelectGroupChecks(ParentID, TxtID)
{
    idCheck = ParentID.replace('All',''); 
    frm = document.forms[0];
    valores = "";
    for(i=0; i<frm.elements.length; i++)
    {
        obj = frm.elements[i];
        if(obj.type=='checkbox')
        {
            if(obj.id!=ParentID && obj.id.replace(idCheck,'').length!=obj.id.length)
            {
                obj.checked = document.getElementById(ParentID).checked;
                if(obj.checked)
                    valores = valores+"|"+obj.value+"|";
            }
        }
    }
    if(TxtID!=null){
        TxtID.value = valores;
    }
}
/*Ejecuta el click segun una determinada tecla*/
function fc_PressKey(oKey, CtrlClientID){
    if(event.keyCode==oKey)
        document.getElementById(CtrlClientID).click();
    return false;    
}
/*Retorna un objeto*/
function fc_Get(ClientID){
    obj = document.getElementById(ClientID);
    if(obj==null || obj==undefined) obj = null;
    return obj;   
} 
/*Da la visibilidad a un botton o Control HTML*/
function fc_ShowButton(pClientID,pBool){
    btn = fc_Get(pClientID);
    if(btn!=null) btn.style.display=(pBool?"inline":"none");
}
/*Validaciones*/
function fc_SoloEntero(){
    e = event.keyCode;
    if((e>=48 && e<=57) || (e==8)){}
    else{ event.keyCode=0; return false; }
}
