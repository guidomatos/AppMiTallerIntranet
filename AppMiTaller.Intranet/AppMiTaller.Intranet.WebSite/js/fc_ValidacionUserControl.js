function fc_KeyPressTxtImporte(e, objTextBox, mascara)
{
    mascara = fc_Replace(mascara, ",", "");
    //Partimos la mascara para determinar la parte entera y la parte decimal.
    var arrMascara = mascara.split(".");
    
    //Validando que la tecla seleccionada sea válida.
    if( e == null ){e = window.event;}
    
    if (e != null)
    {
        var intKeyPress = 0;
        if(e.which) // Netscape/Firefox/Opera
        {
            intKeyPress = e.which;
        }
        else
        {
            intKeyPress = e.keyCode;
        }
        
        if( intKeyPress == 35 || intKeyPress == 36 || intKeyPress == 37 || intKeyPress == 38 || intKeyPress == 39 ||
            intKeyPress == 40 || intKeyPress == 46 || intKeyPress == 45 || intKeyPress == 9 || intKeyPress == 8)
        {
            return true;
        }
        //Verificamos el MaxLen
        if(objTextBox.value.length >= mascara.length)
        {
            return false;
        }

        if ( intKeyPress != 46 && (intKeyPress < 48 || intKeyPress > 57) ) return false;
        //Validamos que solo se halla introducido un punto decimal.
        if (intKeyPress == 46 && ( ( arrMascara.length > 1 && objTextBox.value.indexOf(".") > -1) || arrMascara.length <= 1) )
        {
            return false;
        }
    }
    
    return true;
}

function fc_OnFocusTxtImporte(e, objTextBox, mascara)
{
    objTextBox.value = fc_Replace(objTextBox.value, ",", "");
    //objTextBox.focus();
    return true;
}

function fc_OnBlurTxtImporte(e, objTextBox, mascara)
{
    var exRegDecimales = /^[\- \d ,.]+$/i;
    var exRegEnteros = /^\d+$/i;
    
    objTextBox.value = fc_Replace(objTextBox.value, ",", "");
    
    if(fc_Trim(objTextBox.value) != "")
    {
        //mascara = fc_Replace(mascara, ",", "");
        var arrMascara = mascara.split(".");
        if ( arrMascara.length > 1)
        {
            if(!fc_Trim(objTextBox.value).match(exRegDecimales))
            {
                alert("El valor ingresado" + mstrFormatoIncorrecto + "El formato correcto es: " + mascara);
                objTextBox.value = "";
                objTextBox.focus();
                return false;
            }
        }
        else
        {
            if(!fc_Trim(objTextBox.value).match(exRegEnteros))
            {
                alert("El valor ingresado" + mstrFormatoIncorrecto + "El formato correcto es: " + mascara);
                objTextBox.value = "";
                objTextBox.focus();
                return false;
            }        
        }

        
        
        var mstrResultado = "";

        if(arrMascara.length > 1)
        {
            objTextBox.value = parseFloat(objTextBox.value);
            objTextBox.value = roundNumber(objTextBox.value, arrMascara[1].length);
        }
        else
        {
            objTextBox.value = parseFloat(objTextBox.value);
        }
        //objTextBox.value = mstrResultado;
        objTextBox.value = objTextBox.value;
        
        arrTextBox = objTextBox.value.split(".");
        var arrParteEntera = arrTextBox[0].split("");
        for(var j = arrParteEntera.length - 1; j >= 0 ;j--)
        {
            mstrResultado = mstrResultado + arrParteEntera[j];
            if ( (arrParteEntera.length - j) != 0 && j != 0) 
            {
                if ( ((arrParteEntera.length - j) % 3) == 0 )
                {
                    mstrResultado = mstrResultado + ",";
                }
            }
        }

        arrParteEntera = mstrResultado.split("");
        mstrResultado = "";
        for(var j = arrParteEntera.length - 1; j >= 0 ;j--)
        {
            mstrResultado = mstrResultado + arrParteEntera[j];
        }

        if(arrTextBox.length > 1)
        {
            mstrResultado = mstrResultado + "." + arrTextBox[1];
        }
        
        objTextBox.value = mstrResultado;
    }
}

function roundNumber(number,decimals) {
	var newString;// The new rounded number
	decimals = Number(decimals);
	if (decimals < 1) {
		newString = (Math.round(number)).toString();
	} else {
		var numString = number.toString();
		if (numString.lastIndexOf(".") == -1) {// If there is no decimal point
			numString += ".";// give it one at the end
		}
		var cutoff = numString.lastIndexOf(".") + decimals;// The point at which to truncate the number
		var d1 = Number(numString.substring(cutoff,cutoff+1));// The value of the last decimal place that we'll end up with
		var d2 = Number(numString.substring(cutoff+1,cutoff+2));// The next decimal, after the last one we want
		if (d2 >= 5) {// Do we need to round up at all? If not, the string will just be truncated
			if (d1 == 9 && cutoff > 0) {// If the last digit is 9, find a new cutoff point
				while (cutoff > 0 && (d1 == 9 || isNaN(d1))) {
					if (d1 != ".") {
						cutoff -= 1;
						d1 = Number(numString.substring(cutoff,cutoff+1));
					} else {
						cutoff -= 1;
					}
				}
			}
			d1 += 1;
		} 
		newString = numString.substring(0,cutoff) + d1.toString();
	}
	if (newString.lastIndexOf(".") == -1) {// Do this again, to the new string
		newString += ".";
	}
	var decs = (newString.substring(newString.lastIndexOf(".")+1)).length;
	for(var i=0;i<decimals-decs;i++) newString += "0";
	//var newNumber = Number(newString);// make it a number if you like
	return newString; // Output the result to the form field (change for your purposes)
}


/*
function fc_KeyUpTxtImporte(e, objTextBox, mascara)
{
    //Mascara
    mascara = fc_Replace(mascara, ",", "");
    var arrMascara = mascara.split(".");
    //Texbox despues key up
    var valorTextBox = objTextBox.value;
    var arrTextBox = valorTextBox.split(".");    
    //Valores antes del key up
    var arrKeyPressValor = valorAntesKeyUp.split(".");
    var valorKeyPressParteEntera = arrKeyPressValor[0];
    var valorKeyPressParteDecimal = "";
    if(arrKeyPressValor.length > 1) valorKeyPressParteDecimal = arrKeyPressValor[1];
    
    var valorParteEntera = arrTextBox[0];
    var valorParteDecimal = "";
    if(arrTextBox.length > 1) valorParteDecimal = arrTextBox[1];
    
    if(arrTextBox[0].length > arrMascara[0].length)
    {
        //alert("Error parte Entera");
        var valorKeyUpParteEntera = arrTextBox[0];
        valorParteEntera = "";
        var flagInfDiferencia = false;
        var arrValorKeyUpParteEntera = valorKeyUpParteEntera.split("");
        for(var i = 0; i < valorKeyUpParteEntera.length ;i++)
        {
            if(i < valorKeyPressParteEntera.length)
            {
                if(valorKeyPressParteEntera[i] != valorKeyUpParteEntera[i])
                    flagInfDiferencia = true;
                else valorParteEntera = valorParteEntera + arrValorKeyUpParteEntera[i];                
            }
            else if(flagInfDiferencia)
            {
                valorParteEntera = valorParteEntera + arrValorKeyUpParteEntera[i];
            }
        }
    }
    
    if(arrMascara.length > 1 && arrTextBox.length > 1)
    {
        if(arrTextBox[1].length > arrMascara[1].length)
        {
            //alert("Error parte decimal");
            var valorKeyUpParteDecimal = arrTextBox[0];
            valorParteDecimal = "";
            var flagInfDiferencia = false;
            var arrValorKeyUpParteDecimal = valorKeyUpParteDecimal.split("");
            for(var i = 0; i < valorKeyUpParteDecimal.length ;i++)
            {
                if(i < valorKeyPressParteDecimal.length)
                {
                    if(valorKeyPressParteDecimal[i] != valorKeyUpParteDecimal[i])
                        flagInfDiferencia = true;
                    else valorParteDecimal = valorParteDecimal + arrValorKeyUpParteDecimal[i];                
                }
                else if(flagInfDiferencia)
                {
                    valorParteDecimal = valorParteDecimal + arrValorKeyUpParteDecimal[i];
                }
            }
        }
    }
    
    if(arrTextBox.length > 1)
    {
        objTextBox.value = valorParteEntera + "." + valorParteDecimal;
    }
    else
    {
        objTextBox.value = valorParteEntera;
    }
}
*/