function fc_CambiarTitleCombo(objeto)
{
    objeto.title = objeto.options[objeto.selectedIndex].text;
    return true;
}
/*
Crear efecto de abrir y cerrar subopciones del menu vertical.
*/
function fc_SelOpcionMenuVertical(raiz, nomObjCadenaCodigos)
{
    strCadenaCodigos = document.getElementById(nomObjCadenaCodigos).value;
    arrCadenaCodigos = strCadenaCodigos.split("|");
    
    for(var i = 0; i < arrCadenaCodigos.length ; i++)
    {
        if(arrCadenaCodigos[i] != "")
        {
            if(raiz != arrCadenaCodigos[i])
            {
                document.getElementById(arrCadenaCodigos[i] + "_SUBOP").style.display = "none";
            }
            else
            {
                objSubOpeRaiz = document.getElementById(arrCadenaCodigos[i] + "_SUBOP");
                if ( objSubOpeRaiz.style.display != "none" )
                    objSubOpeRaiz.style.display = "none";
                else objSubOpeRaiz.style.display = "inline";
            }
        }
    }
}

function fc_FireOnChange(Object)
{
    
    if ( Object.fireEvent ) // IE 5.5(WIN)
    {
        Object.fireEvent("onChange");
    }
    else // Mozilla, Safari...
    {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("change",true,true);
        Object.dispatchEvent( evt );
    }
}

function fc_FireOnClick(Object)
{    
    if ( Object.fireEvent ) // IE 5.5(WIN)
    {
        Object.fireEvent("onClick");
    }
    else // Mozilla, Safari...
    {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("click",true,true);
        Object.dispatchEvent( evt );
    }
}

function Fc_Popup(url,ancho,alto,nombre) 
{ msg=window.open(url,nombre,"toolbar=no,left=150,top=150,width=" + ancho + ",height="+alto+",directories=no,status=no,scrollbars=no,resize=no,menubar=no");	
}
function Fc_PopupG(url,ancho,alto,nombre) 
{ msg=window.open(url,nombre,"toolbar=no,left=150,top=150,width=" + ancho + ",height="+alto+",directories=no,status=no,scrollbars=no,resize=no,menubar=no");	
}

function Fc_Popup_Scroll(url,ancho,alto,nombre,top,left) 
{ msg=window.open(url,nombre,"toolbar=no,left=" + left + ",top="+ top + ",width=" + ancho + ",height="+alto+",directories=no,status=no,scrollbars=yes,resize=no,menubar=no");	
}
function Fc_Popup_Pos(url,ancho,alto,nombre,top,left) 
{ msg=window.open(url,nombre,"toolbar=no,left=" + left + ",top="+ top + ",width=" + ancho + ",height="+alto+",directories=no,status=no,scrollbars=no,resize=no,menubar=no");	
}

function fc_Decimal_OnBlur(pObjControl, pIntNroDeci) {
    var Valor = pObjControl.value;
    if (Valor != "") {
        //VERIFICAMOS SI EL VALOR INGRESADO ES CORRECTO
        var RespVal = true;
        var cont = 0;
        var strCadena = new String(Valor);
        var valido = "1234567890.";
        strCadena = strCadena;

        for (i = 0; i <= strCadena.length - 1; i++) {
            var caracter = strCadena.substring(i, i + 1);
            if (caracter == ".") { cont++; }
            if (cont > 1) { RespVal = false; break; }

            if (valido.indexOf(caracter, 0) == -1) {
                RespVal = false;
                break;
            }
        }

        //SI EL NUMERO INGRESADO NO ES VALIDO MOSTRAMOS UN MENSAJE DE ERROR
        if (!RespVal) {
            alert("Ingrese un valor correcto");
            pObjControl.value = '';
            //pObjControl.focus();
            return;
        }

        //DAMOS FORMATO AL NUMERO DECIMAL
        var num = Number(Valor);
        pObjControl.value = num.toFixed(pIntNroDeci);
    }
}

function fc_FormatoNroSepMiles(val) {

    var valRes = new String();
    var valTemp = new String();
    var valTempo = new String();
    valTemp = val.substring(0, val.lastIndexOf('.'));

    if (valTemp == '') valTemp = val;
    valTempo = '';

    while (true) {
        valTempo = ',' + valTemp.substring(valTemp.length - 3, valTemp.length) + valTempo;
        valTemp = valTemp.substring(0, valTemp.length - 3);

        if (valTemp == '') break;
    }

    valTempo = valTempo.substring(1, valTempo.length);

    if (val.lastIndexOf('.') >= 0)
        valRes = valTempo + val.substring(val.lastIndexOf('.'), val.length);
    else
        valRes = valTempo;

    if (valRes.indexOf('.') == -1) { return valRes + ".00"; }

    return valRes;

}

function fc_Replace(texto,s1,s2){
	return texto.split(s1).join(s2);
}

function fc_MontarObjeto(idObjOrigen, idObjMontar)
{
    document.getElementById(idObjMontar).style.border = "0";
    document.getElementById(idObjMontar).style.position = "absolute";
    
    var newLeft = parseFloat(document.getElementById(idObjOrigen).offsetLeft) + 1;
    document.getElementById(idObjMontar).style.left = newLeft + "px";
    
    var newTop = parseFloat(document.getElementById(idObjOrigen).offsetTop) + 2;    
    document.getElementById(idObjMontar).style.top = newTop + "px";
}

function fc_FocusFiltro(idObjOrigen, idObjMontar)
{
    if ( document.getElementById(idObjOrigen).value == "")
    {
        document.getElementById(idObjMontar).value = "";
    }
    
    return true;
}

function fc_LoseFocusFiltro(idObjOrigen, idObjMontar)
{
    if ( document.getElementById(idObjOrigen).value == "" && document.getElementById(idObjMontar).value == "")
    {
        document.getElementById(idObjMontar).value = document.getElementById(idObjOrigen).options[0].innerText;;
    }
    
    return true;
}

function fc_AsignaValorObjeto(idObjOrigen, idObjMontar) {
    document.getElementById(idObjMontar).value = document.getElementById(idObjOrigen).options[document.getElementById(idObjOrigen).selectedIndex].innerText;
}

function fc_ControlaEventoKeyPress(e, idTrigger)
{
    if( e == null ){e = window.event;}
    
    if (e != null)
    {        
        if (e.keyCode == 13) return false;
    }
    return true;
}

function fc_DisparaEventoClick(e, idTrigger)
{
    if( e == null ){e = window.event;}
    
    if (e != null)
    {
        if (e.keyCode == 13)
        {
            document.getElementById(idTrigger).click();
        }
    }
    return true;
}

var objFilaAnt = null;
var backgroundColorFilaAnt = "";
function fc_SeleccionaFilaSimple(objFila)
{
    try
    {
        if ( objFilaAnt != null)
        {
            objFilaAnt.style.backgroundColor = backgroundColorFilaAnt;
        }
        
        objFilaAnt = objFila;
        backgroundColorFilaAnt = objFila.style.backgroundColor;
        objFila.style.backgroundColor = "#c4e4ff";
    }
    catch(e)
    {
        error = e.message;
    }
}

function Fc_SeleccionaItem(strNomCadenaSel, valor)
{
    objCadenaSeleccion = document.getElementById(strNomCadenaSel);
    
    if (objCadenaSeleccion.value=='') objCadenaSeleccion.value='|';
    
    if (objCadenaSeleccion.value.indexOf('|'+valor+'|')>-1)
    {
        var posicion1 = objCadenaSeleccion.value.indexOf('|'+valor+'|');
        var posicion2 = String('|'+valor+'|').lastIndexOf('|');
        objCadenaSeleccion.value = objCadenaSeleccion.value.substring(0, posicion1) + 
                                   objCadenaSeleccion.value.substring(eval(posicion1) + eval(posicion2),objCadenaSeleccion.value.length);
    }
    else
    {
        objCadenaSeleccion.value += valor+'|';
    }
}

function Fc_SelecDeselecTodos(strNomNroFilas, strNomFlagChekTodos, strNomCadenaSel, strNomTabla, strNomCadenaCompleta) {
    objNroFilas = document.getElementById(strNomNroFilas);
    objFlagChekTodos = document.getElementById(strNomFlagChekTodos);
    objCadenaSeleccion = document.getElementById(strNomCadenaSel);
    objTabla = document.getElementById(strNomTabla);
    objCadenaCopleta = document.getElementById(strNomCadenaCompleta);

    var codRel = "";

    if (objNroFilas.value > 0) {
        if (objFlagChekTodos.value == '1') {
            objFlagChekTodos.value = ''
            objCadenaSeleccion.value = '';
            for (var i = 1; i < objTabla.rows.length; i++) {
                codRel = '0' + (i + 1);
                codRel = codRel.substring(codRel.length, codRel.length - 2)
                if (document.getElementById(strNomTabla + '_ctl' + codRel + '_chkSel') != null)
                    document.getElementById(strNomTabla + '_ctl' + codRel + '_chkSel').checked = false;
            }
        }
        else {
            objFlagChekTodos.value = '1'
            objCadenaSeleccion.value = objCadenaCopleta.value;
            for (var i = 1; i < objTabla.rows.length; i++) {
                codRel = '0' + (i + 1);
                codRel = codRel.substring(codRel.length, codRel.length - 2)
                if (document.getElementById(strNomTabla + '_ctl' + codRel + '_chkSel') != null)
                    document.getElementById(strNomTabla + '_ctl' + codRel + '_chkSel').checked = true;
            }
        }
    }
}