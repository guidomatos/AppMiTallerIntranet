var mstrGrabar = "Se grabó exitosamente.";
var mstrElimino = "Se eliminó exitosamente.";
var mstrActualizar = "Se actualizó exitosamente.";
var mstrNoElimino = "No se pudo realizar la acción, consultar con el administrador.";
var mstrNoEliminoRelacionado = "No se puede realizar la acción,\nel registro seleccionado se encuentra relacionado con otros registros del sistema.";
var mstrNoRegistros = "No se encontraron coincidencias.";
var mstrSeleccioneUno = "Debe seleccionar un registro.";
var mstrSeleccioneVarios = "Debe seleccionar al menos un registro.";
var mstrAsigneVarios = "Debe asignar al menos un registro.";
var mstrAdjunteVarios = "Debe adjuntar al menos un archivo.";
var mstrSeguroGrabar = "¿Está seguro de grabar?";
var mstrImprimirOK = "Se imprimio exitosamente";
var mstrNoEliminarPerfil = "No se puede realizar la acción. El perfil es ingresado por el sistema";
var mstrDocYaExiste = "El Nro de documento a registrar ya existe.";
var mstrSeguroNotificarAbrirStock = "¿Está seguro de notificar abrir stock?";
var mstrExitoNotificarAbrirStock = "Se notifico el embarque exitosamente.";
var mstrErrorNotificarAbrirStock = "Problemas al notificar el embarque,\npor favor consulte con el administrador.";
var mstrNoEstadoCorrectoNotificarAbrirStock = "El estado del embarque debe ser en proceso para notificar.";
var mstrMsgCerrarExhiExito = "- Vin Rechazado exitosamente"
var mstrSeguroImprimir = "¿Está seguro de imprimir?";
var mstrSeguroEliminarUno = "¿Está seguro de eliminar?";
var mstrErrorGrabar = mstrNoElimino;
var mstrErrorGrabarCodDuplicado = "El SpecCode ingresado ya se encuentra registrado.";
var mstrErrorGrabarCodExiste = "El Código ingresado ya se encuentra registrado.";
var mstrErrorGrabarNulo = "No se pudo realizar la acción,\nse ingresó un valor nulo en un campo no permitido.";
var mstrErrorGrabarLLave = "No se pudo realizar la acción,\nno existe la llave foránea.";

var mstrRegistroInactivo = "No se puede realizar la acción,\nel registro seleccionado antes ha sido eliminado (inactivo).";
var mstrLongitudRUC = "- El campo RUC debe tener 11 caracteres.";
var mstrRUCInvalido = "- El campo RUC no es válido.";
var mstrRucDuplicado = "El campo RUC ya se encuentra registrado.";
var mstrFichaDuplicado = "El campo nombre ya se encuentra registrado.";
var mstrAgenteExiste = "El campo agencia ya se encuentra registrado.";
var mstrSeguroImportarExcel = "¿Está seguro de importar el archivo seleccionado?";
var mstrNombreRepetido = "El campo nombre ya se encuentra registrado.";
var mstrZonaRepetida = "La zona ya se encuentra definida para esta ubicación.";
var mstrNombreComercExist = "El nombre comercial ya se encuentra registrado.";
var mstrErrorGrabarCodDupModelo = "El código de modelo ya se encuentra registrado.";
var mstrLineasImpAsignadas = "Todas las líneas de importación ya fueron asignadas al negocio.";
var mstrErrorNegLinAsocModel = "El registro se encuentra asociado a un Modelo, no puede eliminarse.";
var mstrErrorInactivarIncoterm = "No podrá inactivar. El incoterm es obligatorio.";


//Seguridad
var mstrUsuarioNoReg = "El usuario no se encuentra registrado.";
var mstrUsuarioRepetido = "La nueva contraseña debe ser distinta a la actual.";
var mstrPefilEnUso = "No se puede realizar la acción,\nel registro seleccionado se encuentra relacionado con usuario del sistema.";
var mstrLoginRepetido = "El campo login ya se encuentra registrado para otro usuario.";
var mstrDNIRepetido = "El campo DNI, ya se encuentra registrado.";
var mstrAccesoPaginaDenegado = "Usted no cuenta con permisos para acceder a esta página.";
var mstrAgenteAduanaYaExiste = "No se pudo insertar. Ya existe el código de aduana ";
var mstrErrorPerfilCotizacion = "No puede ingresar. No tiene el perfil adecuado para el módulo de Cotización.";
var mstrErrorPerfilDesaduanajeAGP = "No puede ingresar. Su perfil no tiene asignado el acceso a esta opción.";


//Perfil
var mstrUsuariosNoVigentes = "Uno o mas de los usuario seleccionados tienen la fecha de acceso al sistema vencido. Favor de modificar la fecha de acceso al sistema por el mantenimiento de usuario.";
var mstrUsuariosYaMultimarca = "El usuario ya se encuentra relacionado a todas las marcas del sistema.";

//Maxima Longitud de Arcvhivos
var mstrEncima4Megas = "Los archivos deben pesar como máximo 4MB.";
var mstrEncimaMaxMarcEmpImp = "La imagen excede el máximo permitido para este mantenimiento.";

var mstrArchivoNoPuedeSerAdj = "El archivo no se pudo adjuntar.";
var mstrArchivoExcedioDim = "La imagen excedió las dimensiones permitidas. Se ajustarán sus medidas.";
var mstrExiste = "No puede registrar a la persona como representante principal porque ya existe.";
var mstrRUCExiste = "El RUC ingresado ya existe.";
var mstrDNIExiste = "El DNI ingresado ya existe.";
var mstrPermitidos = " contiene caracteres no válidos, los permitidos son:";
var mstrDebeIngresar = "- Debe ingresar ";
var mstrDebeSeleccionar = "- Debe seleccionar ";
var mstrElCampo = "- El campo ";
var mstrReAlfanumerico = mstrPermitidos + " A-Z a-z áéíóú üÜ 0-9 Ññ /- _ , . : & ( ).\n";
var mstrReSoloNro = mstrPermitidos + " 0-9 .\n";
var mstrCodigo = mstrPermitidos + " A-Z a-z 0-9 - _ .\n";
var mstrCorreo = mstrPermitidos + " A-Z a-z @ . _ .\n";
var mstrPlaca = mstrPermitidos + " A-Z a-z 0-9 Ññ - .\n";
var mstrTelefono = mstrPermitidos + " 0-9 - _ .\n";
var mstrDecimal = mstrPermitidos + " 0-9 , . .\n";
var mstrVersion = mstrPermitidos + "0-9 . .\n";
var mstrFormatoIncorrecto = " no tiene el formato correcto.\n";
var mstrFecha = " contiene caracteres no válidos, el formato es: DD/MM/AAAA.\n";
var mstrHora = " contiene caracteres no validos.\n";
var mstrMayor = " debe ser mayor que ";
var mstrMenor = " debe ser menor que ";
var mstrMayorIgual = " debe ser mayor o igual que ";
var mstrMenorIgual = " debe ser menor o igual que ";
var mstrMayorCero = " debe ser mayor a cero.\n";
var mstrRangoUnoCien = " debe contener un rango del [ 1-100 ].\n";
var mstrRangoCeroCien = " debe contener un rango del [ 0-100 ].\n";
var mstrTotalMenoroIgualCien = "- La suma total debe ser menor o igual a 100.\n";
var mstrPlacaExiste = "El Nro de placa ya existe.";

//Clientes
var mstrErrorClienteDireccion = "Otro Cliente con el mismo numero de doc tiene una direccion similar.";
var mstrErrorClienteDireccion11 = "Existen facturas asociadas a la dirección.";
var mstrErrorClienteDireccion12 = "Ya existe una direccion para el mismo distrito.";
var mstrSeguroAceptar = "¿Está seguro de aceptar?";