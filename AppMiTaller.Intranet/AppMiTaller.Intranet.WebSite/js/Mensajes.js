//General
//var msjEnConstruccion 			= "Funcionalidad en construcción.";  //---------> eliminar
var mstrGrabar = "Se grabó exitosamente.";
var mstrElimino = "Se eliminó exitosamente.";
var mstrActualizar = "Se actualizó exitosamente.";
var mstrNoElimino = "No se pudo realizar la acción, consultar con el administrador.";
var mstrNoEliminoRelacionado = "No se puede realizar la acción,\nel registro seleccionado se encuentra relacionado con otros registros del sistema.";
var mstrNoRegistros = "No se encontraron coincidencias.";
var mstrSeleccioneUno = "Debe seleccionar un registro.";
var mstrSeleccioneAlmenosUno = "Debe seleccionar por lo menos un registro.";
var mstrSeleccioneVarios = "Debe seleccionar al menos un registro.";
var mstrAsigneVarios = "Debe asignar al menos un registro.";
var mstrAdjunteVarios = "Debe adjuntar al menos un archivo.";
var mstrSeguroGrabar = "¿Está seguro de grabar?";
var mstrSeguroEnviarAgencia = "¿Está seguro de enviar a Agencia?"; //"¿Está seguro de realizar el envío?";
var mstrImprimirOK = "Se imprimio exitosamente";
var mstrNoEliminarPerfil = "No se puede realizar la acción. El perfil es ingresado por el sistema";
var mstrDocYaExiste = "El Nro de documento a registrar ya existe.";
var mstrSeguroNotificarAbrirStock = "¿Está seguro de notificar abrir stock?";
var mstrSeguroPasarStock = "¿Desea bloquear el stock?";
var mstrExitoNotificarAbrirStock = "Se notifico el embarque exitosamente.";
var mstrErrorNotificarAbrirStock = "Problemas al notificar el embarque,\npor favor consulte con el administrador.";
var mstrNoEstadoCorrectoNotificarAbrirStock = "El estado del embarque debe ser en proceso para notificar.";

var mstrSeguroImprimir = "¿Está seguro de imprimir?";
var mstrSeguroEliminarUno = "¿Está seguro de eliminar?";
var mstrErrorGrabar = mstrNoElimino;
var mstrErrorGrabarCodDuplicado = "El SpecCode ingresado ya se encuentra registrado.";
var mstrErrorGrabarCodExiste = "El Código ingresado ya se encuentra registrado.";
var mstrErrorGrabarNulo = "No se pudo realizar la acción,\nse ingresó un valor nulo en un campo no permitido.";
var mstrErrorGrabarLLave = "No se pudo realizar la acción,\nno existe la llave foránea.";
var mstrErrorCambiaNegocio = "No puede realizar la acción. El campo negocio no puede cambiar porque existen atributos asociados.";

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
//	var mstrErrorVinesExhibicion ="No se puede cerrar pues los siguientes vines tienen guias emitidas: {0} con guía {1}";
//	var mstrErrorEstadoExhibicion = "La exhibición no se encuentra en el estado correcto. La exhibición debe estar Aprobada o Aprobada Parcial.";
//	   

//Perfil
var mstrUsuariosNoVigentes = "Uno o mas de los usuario seleccionados tienen la fecha de acceso al sistema vencido. Favor de modificar la fecha de acceso al sistema por el mantenimiento de usuario.";
var mstrUsuariosYaMultimarca = "El usuario ya se encuentra relacionado a todas las marcas del sistema.";

//Maxima Longitud de Arcvhivos
var mstrEncima4Megas = "Los archivos deben pesar como máximo 4MB.";
var mstrEncimaMaxMarcEmpImp = "La imagen excede el máximo permitido para este mantenimiento.";

var mstrArchivoNoPuedeSerAdj = "El archivo no se pudo adjuntar.";
var mstrArchivoExcedioDim = "La imagen excedió las dimensiones permitidas. Se ajustarán sus medidas.";

//Ficha Técnica: Estados
var mstrEstadoVigente = "VIGENTE";

//Ficha Tecnica
var mstrErrorSpecCodeVig = "No se puede realizar esta acción, el registro se encuentra " + mstrEstadoVigente + ".";
var mstrErrorSpecCodeVigAttrib = "No se puede cambiar al estado VIGENTE. Debe asociar atributos a la ficha y/o registrar valores a los mismos.";
var mstrErrorPedRelPedido = "No se puede realizar la acción, el registro seleccionado se encuentra relacionado con otros registros del sistema.";
var mstrErrorPedAsocCompleto = mstrErrorPedRelPedido;
var mstrErrorSpecCodeNoModVig = "No se puede realizar la acción si el estado del registro es " + mstrEstadoVigente + " o esta asociado a un pedido.";

var mstrErrorSpecCodeYaElim = "No se puede eliminar porque ya fue eliminado.";

/*MEDIO DE PAGO*/
var mstrSeguroCerraMedioPago = "¿Está seguro de cerrar el medio de pago?";
var mstrModMedioPago11 = "No se puede realizar la acción.\nEl número de medio de pago ingresado ya existe.";
var mstrModMedioPago10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrModMedioPagoEnviar10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrErrorModificarEstadoPedido = "Error el estado actual del pedido debe ser Pendiente.";
var mstrErrorTipoCambio = "No se encuentra registrado el tipo de cambio para la fecha de inicio del medio de pago.\nConsultar con el administrador.";
var mstrCanceloMedioPago = "Se canceló el medio pago exitosamente.";
var mstrNoCanceloMedioPago = mstrNoElimino;
//Enviar
var mstrErrorMedioPagoNoExiste = "No se puede realizar la acción.\nDebe registrar el medio de pago."; //-18
var mstrErrorMedioPagoDatObli = "No se puede realizar la acción.\nDebe registrar los campos obligatorios."; //-17
var mstrErrorEnviarFinDet = "No se puede realizar la acción.\nDebe registrar el detalle del medio de pago."; //-19
var mstrErrorMedioPagoEnviado = "No se puede realizar la acción.\nEl registro antes ha sido enviado."; //-16
var mstrErrorEnviarFinCan = "No se puede realizar la acción.\nDebe completar las cantidades asociadas al detalle del medio de pago."; //-20 no se usa
//Eliminar
var mstrErrorElimEstGraMedioPago = "No puede eliminar error al grabar el estado del medio de pago."; //-11
var mstrErrorElimEstMedioPago = "No puede eliminar el medio de pago porque el estado actual no es el correcto."; //-10
var mstrErrorElimMedioPagoElim = "No puede eliminar el medio de pago porque ya fue eliminado."; //-25
//Insertar
var mstrErrorRegistrarDatos = "No puede registrar el medio de pago con los datos ingresados porque no corresponde en el estado actual."; //-21
var mstrErrorGrabarImporte = "El importe del saldo del pedido es cero ó mayor al importe del medio de pago."; //-10
var mstrErrorGrabarEstado = "Error al grabar el estado del medio de pago."; //-12
//Modificar    
var mstrErrorModificarEstado = "No puede modificar el medio de pago porque el estado no es el correcto."; //-27,-22,-23
var mstrErrorModificarImporte = "El importe del medio de pago, es menor al importe asociado del pedido."; //-14
var mstrErrorModificarDetalle = "No se puede realizar la acción.\nDebe eliminar el detalle asociado o crear un nuevo medio de pago."; //-13
//Cerrar
var mstrErrorCerrarEmbAso = "No puede cerrar el medio de pago porque no tiene embarques asociados."; //-29
var mstrErrorCerrarEmbAsoInc = "No puede cerrar el medio de pago porque existe embarques asociados incompletos."; //-21
var mstrErrorCerrarEstCor = "No se puede realizar la accion.\nEl medio de pago debe tener saldo mayor a cero y su estado debe ser registrado."; //-20
var mstrErrorCerrarCerrado = "No puede cerrar el medio de pago porque ya fue cerrado."; //-19
var mstrErrorCerrarNoReg = "No se puede realizar la acción.\nAntes debe registrar el medio de pago."; //-18
var mstrErrorCerrarGrabar = "Error al grabar el estado del medio de pago."; //-12    
var mstrNoCerroMedioPago = "Problemas al cerrar el medio de pago. Consulte con el administrador.";
var mstrCerroMedioPago = "Se cerro el medio pago exitosamente.";
//Adjunto
var mstrErrorAdjMedioPago = "No se puede realizar la acción.\nAntes debe registrar el medio de pago.";

/*MEDIO DE PAGO DETALLE*/
var mstrSeguroEnviarMedioPago = "¿Está seguro de notificar el medio de pago?";
var mstrSeguroCerrarMedioPago = "¿Está seguro de cerrar el medio de pago?";
var mstrSegEnvMedPagNot = "¿Está seguro de enviar notificación?";
var mstrEnvioMedioPago = "Se notificó exitosamente.";
//Grabar Detalle
var mstrErrorModificarMedioPago = "No se puede actualizar, los registros han sido enviados."; //-10
var mstrErrorImportesMedioPago = "Los importes de detalle del medio de pago exceden su importe (inc. tolerancia)."; //-11
var mstrErrorEstadoMedioPago = "Error al grabar el estado del medio de pago."; //-12-Enviar no se usa en detalle
var mstrErrorModEstMedioPago = "No se puede actualizar los registros porque el estado no es el correcto."; //-26
//Eliminar Envio
var mstrErrorEliminarEnviar = "No se puede eliminar el estado del medio de pago no es correcto."; //-10
//Eliminar Detalle
var mstrErrorEliminarEnviado = "No se puede eliminar el registro antes ha sido enviado."; //-17
//Enviar
var mstrErrorEnviarEstadoInc = "No puede enviar el medio de pago porque el estado no es el correcto."; //-30
var mstrErrorEnviarEstado = "No se puede enviar el estado del medio de pago."; //-12
var mstrErrorEnviarRegistradas = "No se puede enviar algunas cantidades asociadas al detalle del envio no han sido registradas."; //-11
var mstrErrorEnviarAntes = "No se puede enviar el registro ya fue enviado."; //-10
var mstrErrorEnviarMedioPago = "Problemas al Enviar. Consulte con el administrador.";
//Cerrar
var mstrErrorCerrarMedioPago10 = "No se puede realizar la acción.\nEl estado del medio de pago debe ser REGISTRADO.";
var mstrErrorCerrarMedioPago11 = "No se puede realizar la acción.\nEl saldo del medio de pago debe ser mayor a cero.";
//Insertar Modificar        
var mstrInsMPEstadoInc = "No se puede agregar registros el estado del medio de pago no es el correcto."; //-22//-10
var mstrInsMPSinSaldo = "No se puede agregar registros el medio de pago no cuenta con saldo."; //-11	
//Observacion
var mstrErrorObsMedioPago = "No se puede realizar la acción. El estado debe ser EN FINANZAS ó REGISTRADO.";

/*SOLICITUD MEDIO PAGO*/
var mstrSolicitudMedPagMismoPedido10 = "No se puede realizar la acción.\nLos registros deben corresponder a un mismo pedido.";
var mstrSolicitudMedPagMismoPedido11 = "No se puede realizar la acción.\nAlgunos registros cuentan con una línea de crédito asignada.";
var mstrSolicitudMedPagMismoPedido12 = "No se puede realizar la acción.\nLos registros seleccionados deben tener el estado POR ASIGNAR.";
var mstrEliSolicitudMedPag10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE o POR ASIGNAR sin banco asignado.";
var mstrEliSolicitudMedPag11 = "No se puede realizar la acción.\nEl registro tiene un banco asignado";
var mstrEliSolicitudMedPagDet10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrInsSolicitudMedPagLinCre10 = "No se puede realizar la acción.\nEl importe disponible de la linea debe ser mayor o igual que el importe ingresado.";
var mstrInsSolicitudMedPagLinCre11 = "No se puede realizar la acción.\nEl importe ingresado no es valido de acuerdo al % de tolerancia para la marca relacionada al pedido.";
var mstrEliSolicitudMedPagLinCre10 = "Algunos registros no han sido actualizados porque no cuentan con una línea de crédito asignada.";
var mstrEliSolicitudMedPagLinCre11 = "No se puede realizar la acción.\nNingún registro cuenta con una línea de crédito asignada.";
var mstrEliSolicitudMedPagLinCre12 = "No se puede realizar la acción.\nEl estado de los registros deben ser POR ASIGNAR.";
var mstrModSolicitudMedPagDet10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrModSolicitudMedPagDetEnviar10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrInsSolicitudMedPagDetAsignarLineas10 = "No se puede realizar la acción.\nSólo debe seleccionar registros con estado POR ASIGNAR y con línea de crédito asignada.";

/*MEDIO PAGO ENMIENDA*/
var mstrSeguroEnviarEnmienda = "¿Está seguro de confirmar la enmienda?";
var mstrEnvioEnmienda = "Se confirmó la enmienda exitosamente.";
var mstrErrorEnviarEnmienda = "Problemas al confirmar enmienda. Consulte con el administrador.";
//Insertar
var mstrInsMedPagEnmienda10 = "No se puede realizar la acción.\nExiste un registro de ampliación pendiente de envío.";
//Modificar
var mstrModMedPagEnmienda11 = "No se puede realizar la acción.\nEl registro antes ha sido enviado.";
//Enviar
//RNP - 11/02/2011 - Inicio
var mstrModMedPagEnmiendaEnviar11 = "No se puede realizar la acción.\n- Debe ingresar el % de comisión y/o.\n- Debe ingresar otros gastos relacionados.";
//RNP - 11/02/2011 - Fin
var mstrModMedPagEnmiendaEnviar10 = "No se puede realizar la acción.\nEl registro antes ha sido enviado.";
//Eliminar
var mstrEliMedPagEnmienda10 = "No se puede realizar la acción.\nSolo se pueden eliminar registros sin enviar.";

/*FINANCIAMIENTO*/
var mstrSeguroEnviarFinanciamiento = "¿Está seguro de enviar a proceso warrant?";
var mstrSeguroConfirmarFinanciamiento = "¿Está seguro de confirmar el financiamiento?";
var mstrEnviarFinanciamiento = "Se envió a proceso warrant exitosamente.";
var mstrConfirmarFinanciamiento = "Se confirmo el financiamiento exitosamente.";
var mstrCartasFinanciamiento = "Se generaron las cartas exitosamente.";
var mstrInsFinanciamientoBL11 = "No se puede realizar la acción.\nEl BL tiene facturas sin importe asociado.";
var mstrInsFinanciamientoBL10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrEliFinanciamientoBL10 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE.";
var mstrModFinanciamiento15 = "No se puede realizar la acción.\nEl estado del registro debe ser PENDIENTE ó REGISTRADO.";
var mstrModFinanciamiento10 = "No se puede realizar la acción.\nNo existe línea de crédito registrado para la empresa y banco asociado al financiamiento.";
var mstrModFinEnvWarrant10 = "No se puede realizar la acción.\nSólo se puede enviar registros con estado PENDIENTE.";
var mstrModFinEnvWarrant14 = "No se puede realizar la acción.\nFalta relacionar BL's al financiamiento.";
var mstrModFinConfirmar16 = "No se puede realizar la acción.\nEl estado del registro debe ser REGISTRADO.";
var mstrModFinConfirmar14 = "No se puede realizar la acción.\nFalta relacionar BL's al financiamiento.";
var mstrEliFinanciamiento13 = "No se puede realizar la acción.\nSólo se puede eliminar registros con estado PENDIENTE.";
//Financiamiento File
var mstrSeguroGenerarCartas = "¿Está seguro de generar cartas?";
var mstrInsFinanciamientoFile10 = "No se puede realizar la acción.\nLa carta de solicitud de medio de pago antes ha sido generada.";
var mstrEliFinanciamientoFile10 = "No se puede realizar la acción.\nEl adjunto corresponde a la carta generada por el sistema.";
//Financiamiento Ampliacion	
var mstrModFinAmpliacion10 = "No se puede realizar la acción.\nLa ampliación ha sido enviada.";
var mstrInsFinAmpliacion17 = "No se puede realizar la acción.\nExiste un registro de ampliación pendiente de envío.";
var mstrEliFinAmpliacion18 = "No se puede realizar la acción.\nSolo se pueden eliminar registros sin enviar.";
var mstrModFinAmpEnviar19 = "No se puede realizar la acción.\nEl registro antes ha sido enviado.";
var mstrSeguroEnviarAmpliacion = "¿Está seguro de enviar la ampliación?";
var mstrEnvioAmpliacion = "Se realizó el envío exitosamente.";
var mstrEnvioOrdenes = "No se puede realizar la acción. Solo se debe seleccionar registros relacionados a una carta";
var mstrErrorEnviarAmpliacion = mstrNoElimino;

/*TESORERIA*/
var mstrErrorModTesoreria10 = "No se puede realizar la acción.\nLa fecha de confirmación es menor a la fecha de generación de la carta.";
var mstrErrorCalTesoreria10 = "No se puede realizar la acción.\nLa fecha de confirmación es menor a la fecha de generación de la carta y/o fecha de inicio del financiamiento.";

/*EMBARQUE BL*/
//insertar
var mstrErrorInsPLPreliminar = "No se puede realizar la acción.\nDebe importar el packing list preliminar."; //-83
var mstrErrorInsExcBackOrderPed = "No se puede insertar. La cantidad de unidades del bl ingresado excede el back order respecto al pedido."; //-78
var mstrErrorInsExcSaldo = "No se puede insertar. El importe del bl ingresado excede el saldo respecto al pedido."; //-77
var mstrErrorInsExcBackOrderMP = "No se puede insertar. La cantidad de unidades del bl ingresado excede el back order respecto al medio de pago."; //-70
var mstrErrorInsMedioPagoUsado = "No se puede insertar. Este medio de pago ya fue usado en otro embarque."; //-68
var mstrErrorInsMedioPagoSaldo = "No se puede insertar. El importe del bl ingresado excede el saldo respecto al medio de pago."; //-67	
var mstrErrorInsMedioPagoPedDif = "No se puede insertar. El EmbarqueBL que se estás ingresando tiene un pedido diferente al común."; //-51
var mstrErrorInsMedioPagoAsocPed = "No se puede insertar. Ya existen para este embarque EmbarqueBLs asociados a Pedidos diferentes."; //-50
var mstrErrorInsMedioPagoEst = "No se puede insertar. El medio de pago asociado no esta en estado “En proceso” o “Registrado”."; //-46
var mstrErrorInsMedioPagoNulo = "No se puede insertar. El parámetro de medio de pago es nulo o vacio."; //-45
var mstrErrorInsEmb84 = "No se puede insertar. El nro. de documento de embarque (bl) ya fue ingresado para este embarque.";
var mstrErrorInsEmb85 = "No podra agregar el bl. La cantidad de unidades usadas para este medio de pago en embarques excedería su máximo valor.";
var mstrErrorEmbarquePackingListVinsAsociados = "No podrá cargar el packing list preliminar. El embarque tiene vins asociados."; //-6
var mstrErrorEmbarquePackingListFacturasAsociados = "No podrá cargar el packing list preliminar. El embarque tiene facturas asociadas."; //-7


//modificar
var mstrErrorModPLPreliminar = "No se puede realizar la acción.\nDebe importar el packing list preliminar."; //-83
var mstrErrorModExcBackOrderPed = "No se puede modificar. La cantidad de unidades del bl ingresado excede el back order respecto al pedido."; //-78
var mstrErrorModExcSaldo = "No se puede modificar. El importe del bl ingresado excede el saldo respecto al pedido."; //-77
var mstrErrorModExcBackOrderMP = "No se puede modificar. La cantidad de unidades del bl ingresado excede el back order respecto al medio de pago."; //-70	
var mstrErrorModMedioPagoSaldo = "No se puede modificar. El importe del bl ingresado excede el saldo respecto al medio de pago."; //-67
var mstrErrorModPLAsociado = "No se puede modificar. El EmbarqueBL tiene packing list asociados."; //48
var mstrErrorModFactAsociado = "No se puede modificar. El EmbarqueBL tiene facturas asociadas."; //-47
var mstrErrorModFactCantAsociado = "No se puede modificar. El EmbarqueBL tiene menor cantidad de unidades que sus facturas."; //-49
var mstrErrorModMedioPagoEst = "No se puede modificar. El medio de pago asociado no esta en estado “En proceso” o “Registrado”."; //46
var mstrErrorModMedioPagoNulo = "No se puede modificar. El parámetro de medio de pago es nulo o vacio."; //45
var mstrErrorModBlMedPagExcMediPag = "No podra modificar el bl. La cantidad de unidades usadas para este medio de pago en embarques excedería su máximo valor."; //45

//eliminar
var mstrErrorEliminarEmbBLEst = "No se puede eliminar. El estado del EmbarqueBL es diferente a “Pendiente” o “Registrado”.";
var mstrErrorEliminarEmbBLFac = "No se puede eliminar. El EmbarqueBL tiene facturas asociadas.";
var mstrErrorEliminarEmbBLPL = "No se puede eliminar. El EmbarqueBL tiene packing list asociados.";
//grabar Terminal
var mstrErrorVariosTerminal = "Algunos de los registros no han podido ser procesados.";

//Pedidos - Estados
var mstrEstdoPendiente = "pendiente";
var mstrEstdoRegistrado = "confirmado";
var mstrEstdoEnProceso = "en proceso";
var mstrEstdoObservado = "observado";
var mstrEstdoCompleto = "completo";
var mstrEstdoCancelado = "cancelado";
var mstrEstdoEliminado = "eliminado";

//Orden de liberacion warrant
var mstrErrorFechaRecepcion = "No se puede registrar. Falta ingresar la fecha de recepción del banco";
var mstrErrorNroOrdenLiberacion = "No se puede registrar. Falta ingresar el nro orden de liberación ";


var mstrNoEliminarPendientes = "Solo se puede eliminar pedidos en estado " + mstrEstdoPendiente;
var mstrNoEliminarPendientesConfirmado = "Solo se puede eliminar pedidos en estado " + mstrEstdoPendiente + " y " + mstrEstdoRegistrado;
var mstrNoEliminarDetPendientes = "Solo se puede eliminar detalles relacionados a pedidos en estado " + mstrEstdoPendiente + ", " + mstrEstdoRegistrado + ", " + mstrEstdoEnProceso + " u " + mstrEstdoObservado + ".";
var mstrNoEliminarRelacionado = "El pedido seleccionado se encuentra relacionado a un medio de pago y no puede ser eliminado.";

var mstrSeguroNotificarPedido = "¿Está seguro de notificar el pedido?";
var mstrPedidoSinDetalle = "El pedido no cuenta con registros en el detalle.";
var mstrPedidoNotificarExito = "El pedido se envio para su aprobación.";
var mstrPedidoNotificarError = "Problemas al notificar. Consulte con el administrador.";
var mstrNoTCProformaGrabar = "No se cuenta con un tipo de cambio para la fecha de proforma ingresada, favor de comunicarse con el Departamento de Contabilidad para que sea ingresado antes de realizar la notificación del pedido.";
var mstrNoTCProforma = "No se cuenta con un tipo de cambio para la fecha de proforma ingresada. Favor de comunicarse con el Departamento de Contabilidad.";
var mstrNoTCProformaNoti = "No se cuenta con un tipo de cambio para la fecha de proforma relacionada al pedido. Favor de comunicarse con el Departamento de Contabilidad.";
var mstrNoEliminarEmbarquePendientes = "El embarque seleccionado se encuentra relacionado y no puede ser eliminado.";
var mstrNoEliminarEstadoDifPendiente = "No se puede eliminar el embarque porque el estado no es el correcto.";
var mstrNoEliminarEmbarqueYaEliminado = "No se puede eliminar el embarque porque ya fue eliminado.";
var mstrPedidoExiste = "El nro pedido ingresado ya existe,\npor favor ingrese otro nro pedido.";

//Pedidos - Importar Pedido.
var mstrPedidoImportarPrefijo = "- El Spec Code ";
var mstrPedidoImportarRelEmbarque = " no se puede importar pues ya se encuentra relacionado a un embarque.";
var mstrPedidoImportarErrorCantidadBO = " no se puede importar pues la cantidad de unidades ingresadas es menor que la cantidad de unidades de B/O del medio de pago al cual se encuentra asociado.";
var mstrPedidoImportarErrorImporteMP = " no se puede importar pues el importe del pedido es mayor que el importe del medio de pago asociado.";
var mstrPedidoImportarErrorEstado = " no se puede realizar la importación sobre pedidos en estado " + mstrEstdoEliminado + ", " + mstrEstdoCancelado + " o " + mstrEstdoCompleto;
var mstrPedidoImportarError = " no se pudo importar consulte con el administrador.";
var mstrPedidoRegistroDuplicado = "No puede realizar la accion.\nEl detalle del pedido ingresado antes ya fue registrado.";

var mstrEnvioAgencia = "El embarque se envió exitosamente";
var mstrEnvioAgenciaCorreos = "El embarque se envió exitosamente, pero los correos no se pudieron enviar.";
var mstrNoEnvioAgenciaFactura = "El monto total de los montos de bls no cuadra con las facturas.";
var mstrNoEnvioAgenciaPacking = "El monto total de las facturas no cuadran con los de paking list.";

var mstrNoEnvioAgenciaTipoRegimen = "Algunas de las unidades no tiene asignado un tipo de régimen.";
var mstrNoEnvioAgenciaPedidosMedios = "La cantidad de unidades en pedidos es menor a la cantidad comprometida en Medios de pago. Se debe corregir la incongruencia.";
var mstrNoEnvioAgenciaMedioPagoEmbarque = "La cantidad de unidades en medios de pago es menor a la cantidad comprometida en el Embarque. Se debe corregir la incongruencia.";
var mstrNoEnvioAgenciaNumeracion = "Este embarque ya fue enviado para numeración.";
var mstrNoEnvioAgenciaNoHayBls = "No se puede enviar a Agencia. No se tiene ningún BL registrado.";
//NUEVOS MENSAJES ENVIAR AGENCIA
var mstrNoEnvioAgenciaMsg87 = "No se puede enviar. No todos los vins han sido publicados.";
var mstrNoEnvioAgenciaMsg88 = "No se puede Enviar. El monto de Packing list no cuadra con el Monto Comprometido de Medio de Pago en Embarque.";
var mstrNoEnvioAgenciaMsg89 = "No se puede Enviar. El monto de Packing list no cuadra con el Monto Comprometido de Pedido en Embarque.";
var mstrNoEnvioAgenciaMsg90 = "No se puede Enviar. La cantidad de unidades en Packing list no cuadra con la Cantidad Comprometida de Medio de Pago en Embarque.";
var mstrNoEnvioAgenciaMsg91 = "No se puede Enviar. La cantidad de unidades en Packing list no cuadra con la Cantidad Comprometida de Pedido en Embarque.";
var mstrNoEnvioAgenciaMsg92 = "No se puede Enviar. La cantidad de unidades en Packing list no cuadra con la Cantidad Comprometida de Embarque en Medio de Pago.";
var mstrNoEnvioAgenciaMsg93 = "No se puede Enviar. El monto de Packing list no cuadra con el Monto comprometido de Embarque en Medio de Pago.";
var mstrNoEnvioAgenciaMsg94 = "No se puede Enviar. La cantidad de unidades en Packing list no cuadra con la Cantidad Comprometida de Embarque en el detalle del Pedido.";
var mstrNoEnvioAgenciaMsg95 = "No se puede Enviar. El monto de Packing list no cuadra con el Monto comprometido de Embarque en el detalle del Pedido.";
var mstrNoEnvioAgenciaMsg96 = "No se puede Enviar. La cantidad comprometida de unidades de Medio de Pago en el Detalle del Pedido no cuadra con la cantidad de Unidades Comprometidas del Embarque en Medio de Pago.";
var mstrNoEnvioAgenciaMsg97 = "No se puede Enviar. El monto comprometido de Medio de Pago en el Detalle del Pedido no cuadra con el monto comprometido de Embarque en Medio de Pago.";
var mstrNoEnvioAgenciaMsg98 = "No se puede Enviar. La cantidad comprometida de unidades de Medio de Pago en el Detalle del Pedido no cuadra con la cantidad de Unidades finales de Medio de Pago en el Detalle del Pedido.";
var mstrNoEnvioAgenciaMsg99 = "No se puede Enviar. El monto comprometido de Medio de Pago en el Detalle del Pedido no cuadra con el monto final de Medio de Pago en el Detalle del Pedido.";
var mstrNoEnvioAgenciaMsg100 = "No se puede Enviar. El documento de gases en Documentos Desembarque no se ha generado correctamente.";
var mstrNoEnvioAgenciaMsg101 = "No se puede Enviar. No se ha indicado la recepción completa de documentos de Desembarque.";
var mstrNoEnvioAgenciaMsg102 = "No se puede Enviar. La cantidad de unidades en Packing List no es igual que en Embarque.";
var mstrNoEnvioAgenciaMsg103 = "No se puede Enviar. La cantidad de unidades en BL's no es igual que en Packing List.";
var mstrNoEnvioAgenciaMsg104 = "No se puede Enviar. La cantidad de unidades en Facturas no es igual que en Packing List.";
var mstrNoEnvioAgenciaMsg121 = "No se puede Enviar. Algunos Bl's no tienen asignado un Terminal.";

var mstrEnvAgenUniPackPrelPack = "La cantidad de unidades cargados en el Packing List Preliminar no es igual a los asignados en Packing List.";
var mstrEnvAgenMontTotVinSpec = "El monto total de los vins en algún spec code excede su máximo asignado en medio de pago.";
var mstrEnvAgenPackPackPrelim = "La cantidad de vins asignados a alguna factura en Packing List excede su Packing List Preliminar.";
var mstrEnvAgenFactPackPrelim = "La cantidad de vins asignados a alguna factura excede su Packing List Preliminar.";


var mstrNoEnvioAgencia = "Problemas al enviar. Consulte con el administrador.";
var mstrNoSePuedeRealizarAccion = "No se puede realizar esta acción.";


var mstrErrorNotificarNoDescripcion = "No puede enviar y/o notificar porque no existen observaciones que enviar";
var mstrNotifico = "Se envio la notificacion,exitosamente.";

var mstrImporteExcedeEmbarqueBL = "El importe de las facturas asociadas excede el EmbarqueBL.";
var mstrFacturaIngresadaEmbarqueBL = "La factura ya fue ingresada para el EmbarqueBL.";
var mstrFacturaIngCantUnidadesEmbarqueBL = "La cantidad de unidades de las facturas asociadas excede el EmbarqueBL.";
var mstrFacturaIngCantMennorVins = "No podrá modificar. La cantidad de unidades en la factura es menor a los vins asociados.";
var mstrFacturaPackingListAsociadoEmbarqueBL = "No se puede eliminar. La factura tiene packing list asociados.";
var mstrTipoRegimenEmbarqueCerrado = "No puede cambiarse el tipo de Regimen. El Embarque ya se encuentra CERRADO";
var mstrTipoConsumoATerminalEmbarqueCompleto = "No se puede cambiar el tipo de Consumo a Terminal. El embarque ya se encuentra COMPLETO";
var mstrNoEliminarEmbarquePublicado = "No puede eliminar el VIN porque esta publicado.";
var mstrNoEliminarEmbarqueNumerado = "No puede eliminar el VIN porque esta numerado.";
var mstrNoCambiarStockPublicado = "No se puede cambiar un stock publicado.";
var mstrNoCambiarStockDesPublicado = "No se puede cambiar un stock que ha sido despublicado.";

/*PACKING LIST*/
//RNAPA - 23/02/2009 - Mensaje.
var mstrPackingErrorEstado = "No se puede realizar la acción.\nEl vin tiene estado comercial DISPONIBLE.";
//RNAPA - 23/02/2009 - Fin.
var mstrPackingErrorVinAbierto = "Solo se puede publicar un Vin con Stock Abierto.";
var mstrPackingErrorAsigCantidadVin = "No puede asignar VIN's porque no se puede exceder la cantidad maxima permitido para la factura.";
var mstrPackingErrorAsigVin = "No puede asignar VIN's porque no se puede exceder el monto maximo permitido para la factura.";
var mstrPackingErrorCantidadMayor = "No puede asignar VIN's,La cantidad asignada es mayor al B/O del medio de pago asociado.";
var mstrPackingErrorImporteMayor = "No puede asignar VIN's,El importe asignado es mayor al saldo del medio de pago asociado.";
var mstrNoVinsByFactura = "No existen VIN's cargados para la factura seleccionada";
var mstrPackingErrorFleteBLPedido = "No podrá agregar el vin. El flete del pedido es diferente al flete prorrateado del bl.";
var mstrPackingErrorSumaVinsExcedeFact = "No podrá agregar el vin. La suma de montos en vins excederían a su factura asociada.";
var mstrPackingErrorSumaFleteExceBL = "No podrá agregar el vin. La suma de flete total de vins excede el flete del bl.";
var mstrPackingNroVinFactPack = "No podrá agregar el vin. El número de vins excederían al total por factura en el Packing List Preliminar.";
var mstrMontVinFactMedioPag = "No podrá agregar el vin. El monto total de vins excederían al total del medio de pago.";
var mstrPackingErrorNroVinExcMediPago = "No podrá agregar el vin. El número de vins excederían al total del medio de pago.";
var mstrPackingErrorMontSpecMedioPag = "No podrá agregar el vin. El monto total de vins por spec_code excederían al total del medio de pago por spec code.";
var mstrPackingErrorNroSpecMedioPag = "No podrá agregar el vin. El número de vins por spec_code excederían al total del medio de pago por spec code.";


var mstrPackingErrorMarcaAutoUltra = "No podrá agregar el vin. La marca asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorModeloAutoUltra = "No podrá agregar el vin. El modelo asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorSpecCodeAutoUltra = "No podrá agregar el vin. El SpecCode asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorCarroceriaAutoUltra = "No podrá agregar el vin. La Carroceria asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorCategoriaAutoUltra = "No podrá agregar el vin. La Categoria asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorModeloAutoUltraColorExt = "No podrá agregar el vin. El color exterior asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorModeloAutoUltraColorInt = "No podrá agregar el vin. El color interior asociada al Vin no tiene código AutoPro.";
var mstrPackingErrorMarcaUltraColor = "No podrá agregar el vin. La marca asociada al Vin no tiene código UltraGestión en Bd Externa.";
var mstrPackingErrorModeloUltraColor = "No podrá agregar el vin. El modelo asociado al Vin no tiene código UltraGestión en Bd Externa.";
var mstrPackingErrorModeloAutoProVin = "No podrá agregar el vin. El modelo asociado al Vin no tiene código UltraGestión en Bd Externa.";
var mstrPackingErrorModeloUltraVin = "No podrá agregar el vin. El modelo asociado al Vin no tiene código UltraGestión en Bd Externa.";

var mstrPackingErrorMarcaUltra = "No podrá agregar el vin. La marca asociada al Vin no tiene código Ultragestión.";
var mstrPackingErrorModeloUltra = "No podrá agregar el vin. El modelo asociada al Vin no tiene código Ultragestión.";
var mstrPackingErrorSpecCodeUltra = "No podrá agregar el vin. El SpecCode asociada al Vin no tiene código Ultragestión.";
var mstrPackingErrorModeloAutoProVin = "No podrá agregar el vin. El Numero de vin ya se encuentra registrado en la bd AutoPro.";
var mstrPackingErrorCategoriaUltra = "No podrá agregar el vin. El Numero de vin ya se encuentra registrado en la bd UltraGestión.";


//para abrir y cerrar stock
var mstrStockyaAbierto = "El stock ya se encuentra abierto.";
var mstrStockyaCerrado = "El stock ya se encuentra cerrado.";
var mstrStock116 = "Solo se puede abrir o cerrar stock de VIN en estado comercial No Disponible, Disponible o Activo Fijo.";
var mstrStocksAbiertos = "Uno o más de los registros seleccionados ya se encuentra en stock abierto y no fueron modificados.";
var mstrStocksCerrados = "Uno o más de los registros seleccionados ya se encuentra en stock cerrado y no fueron modificados.";
var mstrStocksCerradosAbiertos = "Uno o más de los registros seleccionados ya se encuentra en stock cerrado o abierto y no fueron modificados.";

var mstrExiste = "No puede registrar a la persona como representante principal porque ya existe.";
var mstrRUCExiste = "El RUC ingresado ya existe.";
var mstrDNIExiste = "El DNI ingresado ya existe.";
var mstrPartidaExiste = "El nro partida ingresado ya existe.";

var mstrExtranetErrorOrdenNumerada = "Esta orden ya fue numerada.";
var mstrExtranetErrorOrdenDesaduanada = "Esta orden ya fue desaduanada.";

var mstrErrorGrabarEmbarqueEstadoIncorrecto = "No se puede grabar un embarque en estado Enviado.";

var mstrErrorEmbarqueImporteFacturaMayorSaldoPedido = "El importe de la factura ingresada excede el saldo respecto al pedido.";
var mstrErrorEmbarqueCantidadFacturaMayorPedidoBackOrder = "La cantidad de unidades de la factura ingresada excede el back order respecto al pedido.";
var mstrErrorEmbarqueImporteFacturaMayorMedioPago = "El importe de la factura ingresada excede el saldo respecto al medio de pago.";
var mstrErrorEmbarqueCantidadFacturaMayorMedioPagoBackOrder = "La cantidad de unidades de la factura ingresada excede el back order respecto al medio de pago.";
var mstrNoRegistrado = "No puede visualizar el documento porque no ha sido registrado.";

//mensajes de validaciones.
var mstrPermitidos = " contiene caracteres no válidos, los permitidos son:";

var mstrDebeIngresar = "- Debe ingresar ";
var mstrDebeSeleccionar = "- Debe seleccionar ";
var mstrElCampo = "- El campo ";
var mstrReAlfanumerico = mstrPermitidos + " A-Z a-z áéíóú üÜ 0-9 Ññ /- _ , . : & ( ).\n";
var mstrReSoloAlfaNumerico = mstrPermitidos + " A-Z a-z 0-9.\n";
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
//var mstrRangoMayorCeroCien = " debe contener un rango del ] 0-100 ].\n";
var mstrTotalMenoroIgualCien = "- La suma total debe ser menor o igual a 100.\n";
var mstrErrorEmbarqueVinRepetidos = "- No se puede realizar esta accion, el numero de VIN antes ha sido registrado.\n";
var mstrErrorEmbarqueErrorEmbarqueFacturaMenoraVins = "- No podrá modificar la factura. El monto de la factura es menor a los vins asociados.\n";
var mstrErrorEmbarqueErrorFactMontExcedMed = "- No se podrá modificar. El monto total de facturas excede el total del medio de pago.\n";
var mstrErrorEmbarqueErrorFactCantExcedMed = "- No se podrá modificar. La cantidad de unidades total de facturas excede el total del medio de pago.\n";

var mstrErrorDesaduanajeMuchosRegistros = "- Se encontró más de una coincidencia. Por favor ingrese mayor información.\n";
var mstrErrorDesaduanajeNingunRegistros = "- No se encontraron coincidencias.\n";
var mstrMasdeunaCoincidencia = "- Se encontró más de una coincidencia.\n";
var mstrFechaBrevExipo = "- El fecha de vencimiento del brevete ha expirado.\n";

var mstrErrorDesaduanajeProceso = "- Se realizo el proceso de desaduanaje: por favor revise el log de errores.\n";
var mstrErrorDesaduanajeProcesoImcompleto = "- Se realizo el proceso de desaduanaje, algunos VIN'S no fueron desaduanados.\n";
var mstrErrorExcelTodosRegistros = "- Todos los registros del excel tuvieron errores.\n";
var mstrErrorExcelAlgunosRegistros = "- Algunos registros del excel tuvieron errores.\n";
var mstrExcelExitoAlgunosRegistros = "- Algunos registros del excel se guardaron exitosamente,\n  y algunos registros tuvieron errores.\n";
var mstrExcelExitoTodosRegistros = "- Se cargo la información del excel exitosamente.\n";
var mstrExtranetNingunRegistro = "- No se encontro información para el nro orden o nro embarque.\n";
var mstrExtranetDoSearch = "- Debe realizar una busqueda primero.\n";
var mstrErrorNumeracionProceso = "- Problemas al realizar el proceso de numeración.\n";
var mstrErrorExcelSinRegistro = "- El excel adjuntado no contiene ningun registro.\n";
var mstrExtranetOrdenCerrada = "- El estado de la orden es cerrado, no se puede realizar esta acción.\n";
var mstrExitoExcelTodosRegistros = "- Todos los registros del excel se cargaron exitosamente.\n";

var mstrErrorTxtTodosRegistros = "- Todos los registros del txt tuvieron errores.\n";
var mstrErrorTxtAlgunosRegistros = "- Algunos registros del txt tuvieron errores.\n";
var mstrTxtExitoAlgunosRegistros = "- Algunos registros del txt se guardaron exitosamente,\n  y algunos registros tuvieron errores.\n";
var mstrTxtExitoTodosRegistros = "- Se cargo la información del txt exitosamente.\n";
var mstrErrorTxtSinRegistro = "- El txt adjuntado no contiene ningun registro.\n";
var mstrExitoTxtTodosRegistros = "- Todos los registros del txt se cargaron exitosamente.\n";

/*OPERACIONES - PDI*/
var mstrErrorEstEnProceso = "El Vin seleccionado no esta en el estado correcto.\nEl estado debe ser pendiente y su ubicación debe ser Recepcionado-PDI."; //-4:en proceso
var mstrErrorEstConcluido = "El Vin seleccionado no esta en el estado correcto.\nLa etapa debe ser en proceso"; //-4:concluido
var mstrErrorVinNoExiste = "El Vin no existe.\n"; //-5
var mstrErrorVinIncPend = "no puede ser puesto en PDI listo si tiene incidencias pendientes.\n"//-6
var mstrErrorVinNoMovilizado = "no puede ser ingresado a PDI pues no se encuentra movilizado.\n"; //-11
var mstrErrorVinModEstadoPDI12 = "ya se encuentra dentro del proceso de PDI.\n"; //-12
var mstrErrorVinModEstadoPDI13 = "no puede ser ingresado a PDI pues debe tener warrant de campo o estar libre de warrant.\n"; //-13
var mstrErrorVinModEstadoPDI21 = "debe estar en la etapa PDI para poder ser movido entre estaciones.\n"; //-21
var mstrErrorVinModEstadoPDI41 = "no se encuentra en el estado correcto o no se encuentra en Recepcionado PDI.\n"; //-41
var mstrErrorVinModEstadoPDI42 = "no se encuentra en el estado correcto. La etapa debe ser En Proceso.\n"; //-42
var mstrErrorEstadoVarios = "Algunos de los registros seleccionados no han podido ser procesados.\nVerifique el estado."; //varios registros
var mstrNoPuestoPDI = "El VIN no puede ser puesto en PDI Listo si tiene incidencias pendientes.";
var mstrNoUbicadoEspacio = "El VIN no puede ser ubicado pues el espacio del almacén no lo permite.";
var mstrNoEspecificadoAltoAncho = "El VIN no puede ser ubicado pues no se ha configurado los atributos de ancho y largo.";
var mstrErrorEstPDIListo = "El Vin seleccionado esta en el estado PDI Listo.\nNo se pueden generar incidencias.";
var mstrErrorEstIncorrecto = "No se puede realizar la accíón.\nEl estado es incorrecto.";

var mstrVinNoNumeracionDua = "El VIN no puede ser ingresado a almacén pues no cuenta con numeración de DUA.";

var mstrVinIncidenciasPendientes = "El VIN cuenta con incidencias pendientes, primero debe cerrar estas antes de proceder.";
var mstrVinDocPendientes = "El VIN cuenta con documentos obligatorios pendientes, primero debe adjuntar estos antes de proceder.";
var mstrvinSinGuia = "El VIN no cuenta con una guía de salida emitida, favor de generarla antes de proceder.";

var mstrVinAlmacenAduanero = "El VIN no puede ser ingresado a almacén. El almacén debe ser de tipo Almacén Aduanero.";
var mstrVinAlmacenCampo = "El VIN no puede ser ingresado a almacén. El almacén debe ser de tipo Almacén de Campo.";
var mstrVinNoUbicadoNoEspacioAlmacen = "El VIN no puede ser ubicado pues el espacio del almacén no lo permite.";
var mstrVinPDIEst = "El VIN debe estar en la etapa PDI para poder ser movido entre estaciones.";
var mstrVinEstProc = "El VIN no se encuentra en el estado correcto. La etapa debe ser En Proceso.";
var mastrVinPrePDI = "El VIN no se encuentra en el estado correcto o no se encuentra en Recepcionado-PDI";

var mstrVinEstadoMalPrePDI = "El VIN no se encuentra en el estado correcto o no se encuentra en PRE-PDI.";
var mstrVinErrorEstado = "El VIN no se encuentra en el estado correcto";
var mstrVinNoLibreWarrant = "El VIN no puede ser entregado pues no se encuentra libre de warrant";
var mstrVinSinGuiaRemision = "El VIN no puede ser entregado pues no cuenta con una guía de remisión emitida";
var mstrVinDestinoNoAlmaCampo = "El destino del VIN no es almacén de campo, el VIN debe estar libre de warrant";
var mstrVinLibreWarrantoWarrantCampo = "El VIN debe estar libre de warrant o con warrant de campo";


/*OPERACIONES - DESPACHO*/
//var mstrErrorEstPorDespachar = "";//-4:PorDespachar
//var mstrErrorEstDespachado = "";//-4:Despachado
var mstrErrorEstRecepcionado = "no esta en el estado correcto. El estado debe ser despachado.\n"; //-4:Recepcionado
var mstrErrorEstQAListo = "no esta en el estado correcto. El estado debe ser recepcionado.\n"; //-4:QAListo
var mstrErrorEstEntregado = "no esta en el estado correcto. El estado debe ser QA listo.\n"; //-4:Entregado
var mstrErrorIncPend = "cuenta con incidencias pendientes, primero debe cerrar estas antes de proceder.\n"; //-6
var mstrErrorDocPend = "cuenta con documentos obligatorios pendientes, primero debe adjuntar estos antes de proceder.\n"; //-7
var mstrErrorGuiaEmit = "no cuenta con una guía de salida remisión, favor de generarla antes de proceder.\n"; //-8
var mstrErrorModEstOrdenDes9 = "su destino no es almacén de campo, el vin debe estar libre de warrant.\n"; //-9
var mstrErrorModEstOrdenDes10 = "debe estar libre de warrant o con warrant de campo.\n"; //-10
var mstrErrorModEstOrdenDes11 = "no puede ser ingresado a Pre-Despacho pues no se encuentra movilizado.\n"; //-11
var mstrErrorModEstOrdenDes12 = "no puede ser entregado pues no se encuentra libre de warrant.\n"; //-12
var mstrErrorModEstOrdenDes13 = "no puede ser entregado pues no cuenta con una guía de remisión emitida.\n"; //-13
var mstrErrorModEstOrdenDes14 = "no puede ser ingresado a Pre-Despacho pues no a completado el tiempo necesario en PDI.\n"; //-14
var mstrErrorModEstOrdenDes15 = "no puede salir de almacén pues no a completado el tiempo necesario en despacho.\n"; //-15
var mstrErrorModEstOrdenDes16 = "no puede salir de almacén pues no ha sido desaduanado.\n"; //-16
var mstrErrorModEstOrdenDes17 = "no puede ser entregado pues no ha sido cancelado al 100%.\n"; //-17
var mstrErrorModEstOrdenDes41 = "no se encuentra en el estado correcto. El VIN debe estar en Pre-Despacho.\n"; //-41
var mstrErrorModEstOrdenDes42 = "no se encuentra en el estado correcto. El VIN debe estar en Despacho.\n"; //-42
var mstrErrorModEstOrdenDes43 = "no se encuentra en el estado correcto. El VIN debe estar en Traslado.\n"; //-43
var mstrErrorModEstOrdenDes44 = "no se encuentra en el estado correcto. El VIN debe estar Recepcionado.\n"; //-44
var mstrErrorModEstOrdenDes45 = "no se encuentra en el estado correcto. EL VIN debe estar QA Listo.\n"; //-45
var mstrErrorSinGuiaPen = "El VIN no cuenta con guía de salida pendientes.\n"; //-4:Anular Guia
var mstrErrorEtapaDespacho = "La guia no puede ser anulada pues ya se encuentra en transito. Solo se pueden anular guias pendientes o emitidas.\n"; //-5:Anular Guia
var mstrGuiaNoEliminada = "La guia no puede ser anulada pues ya se encuentra recepcionada. Solo se pueden anular guias pendientes o emitidas.\n";

/*OPERACIONES - SEGUIMIENTO*/
//@0032 - DAC - Inicio
var mstrErrorVinNoAlmacenCentral = "El Vin seleccionado no se encuentra dentro del almacén central ó satelite.\nNo se puede realizar esta acción.\n"; //-5
//@0032 - DAC - Fin
var mstrErrorVinEnviadoPDI = "El Vin seleccionado ya fue enviado a PDI.\n"; //-4
var mstrInsMovilizacionPDI2 = "La solicitud de movilización a PDI se procesará el día de mañana.\nSe grabó exitosamente.\n"; //2
var mstrErrorVinOrdenDespacho = "El vin seleccionado ya tiene una orden de despacho.\n"; //-4
var mstrErrorVinNoPDIListo = "El Vin seleccionado no esta en el estado correcto.\nEl Vin debe de estar en PDI listo."; //-5    
var mstrInsOrdenDespacho2 = "La solicitud de orden de despacho se procesará el día de mañana.\nSe grabó exitosamente."; //2

/*OPERACIONES - WARRANT*/
var mstrEstadoWarrant = "No se puede realizar la acción.\nEl estado de la orden debe ser pendiente.";
var mstrMismoWarrant = "Los vines seleccionados deben corresponder a un mismo warrant.";
var mstrFecPermWarrant = "No se puede realizar la acción.\nNo se cuenta con una fecha permitida.";
//CAPITAL
var mstrModWarCapEnv10 = "No se puede realizar la acción.\nNo existen vehículos asociados a la orden."; //-10
var mstrModWarCapEnv11 = "No se puede realizar la acción.\nEl importe de la orden debe ser mayor o igual al importe del financiamiento menos el porcentaje considerado para warrant."; //-11
var mstrModWarCapEnv12 = "No se puede realizar la acción.\nLa orden antes ha sido enviada."; //-12
var mstrInsWarCapDet10 = "No se puede realizar la acción.\nEl estado aduanero de los vehículos deben ser mínimo NUMERADO."; //-10
//CONSTITUCION / EXONERACION
var mstrNoAccionLibres = "No se puede realizar la acción.\nLos importes seleccionados superan el importe base de distribución de warrant de la columna LIBRES."; //DAC - 23/02/2011 - I-F    
var mstrInsWarProDetCap10 = "No se puede realizar la acción.\nEl número de warrant ya existe."; //-10
var mstrInsWarProDetEmb10 = "No se puede realizar la acción.\nAntes debe verificar las cartas de exoneración generadas por el sistema."; //-10
var mstrInsWarProDetEmb11 = "No se puede realizar la acción.\nDebe seleccionar unidades como Libres."; //-11
var mstrInsWarProDetEmb12 = "No se puede realizar la acción.\nEl total de los unidades libres debe ser menor en un X% o igual\nal importe de libres de la distribución de warrant."; //-12
var mstrInsWarProDetEmb13 = "No se puede realizar la acción.\nEl total de los unidades a constituir debe ser mayor ó igual al importe por constituir."; //-13
var mstrInsWarProDetEmb14 = "No se puede realizar la acción.\nAlgun(os) vehículo(s) con DUA DE DEPOSITO no se encuentran en un deposito aduanero."; //-14
var mstrInsWarProDetEmb15 = "No se puede realizar la acción.\nEl(Los) vehículo(s) seleccionados deben estar NUMERADOS."; //-15
var mstrInsWarProDetEmb16 = "No se puede realizar la acción.\nAlgun(os) vehículo(s) con DUA DE IMPORTACION se encuentran en TERMINAL."; //-16
var mstrInsWarProDetEmb17 = "No se puede realizar la acción.\nEl estado comercial de los vehículos debe corresponder a la politica de exoneración."; //-17
var mstrInsWarProDetEmb18 = "No se puede realizar la acción.\nSe deben ingresar las fechas del financiamiento y confirmar sus datos."; //DAC - 23/02/2011 - I-F   //-18
var mstrInsWarProDetEmb19 = "No se puede realizar la acción.\nEl importe excede al % warrant que respalda el saldo del financiamiento."; //DAC - 23/02/2011 - I-F   //-19
var mstrInsWarProDetEmb20 = "No se puede realizar la acción.\nEl importe excede al % warrant que respalda el saldo de la deuda."; //DAC - 23/02/2011 - I-F   //-20
//@0032 - DAC - Inicio
var mstrSeguroAgregarConsWarr = "¿Esta seguro de agregar los registros a la lista de constitución de warrant?";
//@0032 - DAC - Fin
//LIBERACION / RENOVACION
var mstrInsWarLibRenLd10 = "No se puede realizar la acción.\nEl estado comercial de los vehículos debe corresponder a la politica de liberación definitiva."; //-10
var mstrInsWarLibRenLd11 = "No se puede realizar la acción.\nSe deben ingresar las fechas del financiamiento y confirmar sus datos."; //DAC - 23/02/2011 - I-F   //-11
var mstrInsWarLibRenLd12 = "No se puede realizar la acción.\nEl importe de algun(os) vehículo(s) seleccionado(s) excede al % warrant comprometido en el financiamiento."; //DAC - 23/02/2011 - I-F   //-12
//EXONERACIONES
var mstrInsWarProEmbMas10 = "No se puede realizar la acción.\nEl estado aduanero de los vehículos deben ser NUMERADO, EN DESADUANAJE o CON DESADUANAJE."; //-10
var mstrInsWarProEmbMas11 = "No se puede realizar la acción.\nSe deben ingresar las fechas del financiamiento y confirmar sus datos."; //DAC - 23/02/2011 - I-F   //-11
var mstrInsWarProEmbMas19 = "No se puede realizar la acción.\nEl importe de algun(os) vehículo(s) excede al % warrant que respalda el saldo del financiamiento."; //DAC - 23/02/2011 - I-F   //-19
var mstrInsWarProEmbMas20 = "No se puede realizar la acción.\nEl importe de algun(os) vehículo(s) excede al % warrant que respalda el saldo de la deuda."; //DAC - 23/02/2011 - I-F  //-20

/*Operaciones para almacen*/
var mstrVinyaIngresadoAlmacen = "El Vin ya fue ingresado a almacén.";

var mstrPlacaExiste = "El Nro de placa ya existe.";

/*INCIDENCIAS*/
var mstrMsgEnvioExito = "- Se envio el registro exitosamente.";
var mstrMsgEstCerradoExito = "- Se actualizo el estado del registro a cerrado exitosamente.";
var mstrMsgErrorEstadoIncidencia = "- No se puede realizar la acción.\nLa incidencia se encuentra ";
var mstrMsgErrorRegistrosCierreIncidencias = "- No se puede realizar la acción.\n  Debe registrar los datos de cierre de la incidencia.";
var mstrMsgErrorRegistrosCierreIncidencias15 = "- No se puede realizar la acción.\nLa incidencia sólo puede ser cerrada por el responsable.";
var mstrErrorInsertVinIncidencia10 = "- No se puede realizar la acción.\nSólo puede existir una inicidencia de cada tipo pendiente de atención.";
var mstrErrorUpdateVinIncidencia11 = "- No se puede realizar la acción.\nLa incidencia se encuentra cerrada.";
var mstrErrorUpdateVinIncidencia12 = "- No se puede realizar la acción.\nLa incidencia sólo puede ser actualizada por el responsable.";
var mstrMsgErrorEstadoIncidenciaIncorrecto = "- No se puede realizar la acción.\n  La incidencia debe encontrarse ";
var mstrMsgErrorEliAdjuntoIncidenciasRegistro = "- No se puede realizar la acción.\n  El adjunto corresponde a la etapa de registro.";
var mstrMsgErrorEliAdjuntoIncidenciasAtencion = "- No se puede realizar la acción.\n  El adjunto corresponde a la etapa de atención de la incidencia.";

var mstrConfEnviarIncidencia = "¿Está seguro de enviar el registro?";
var mstrConfCerrarIncidencia = "¿Está seguro de cerrar el registro?";
var mstrMsgEstadoIncidenciaPendiente = " Pendiente";
var mstrMsgEstadoIncidenciaCerrado = " Cerrado";
var mstrMsgEstadoIncidenciaEnProceso = " Vigente";
var mstrMsgRegistrarIncidencia = "- Debe registrar una incidencia primero.";
var mstrMsgSelRegAdjuntoIncidencia = "- Debe seleccionar un registro de archivos.";
var mstrMsgErrorVinNoExiste = "- El VIN ingresado no existe, por favor ingrese un VIN existente.";
/*SOLICITUD EXHIBICIONES*/
var mstrMsgErrorEstadoExhibicion = "- No se puede realizar la acción.\n La exhibicion se encuentra ";
var mstrMsgEstEnviarExito = "- Se envio la solicitud de exhibición exitosamente.";
var mstrMsgEstConfirmarExito = "- Se confirmo la solicitud de exhibición exitosamente.";
var mstrMsgEstCancelarExito = "- Se cancelo la solicitud de exhibición exitosamente.";
var mstrMsgCerrarExhiExito = "- Vin Rechazado exitosamente"; //DAC - 23/02/2011 - I-F


var mstrConfEnviarSolExhi = "¿Está seguro de enviar la solicitud de exhibición?";
var mstrConfConfirmarSolExhi = "¿Está seguro de confirmar la solicitud de exhibición?";
var mstrConfConfirmarSolParcial = "¿Está seguro de confirmar la solicitud Parcialmente?";
var mstrConfCancelarSolExhi = "¿Está seguro de cancelar la solicitud de exhibición?";

var mstrMsgEstEnviarIncorrecto = "- La solicitud debe estar en estado pendiente.";
var mstrMsgEstConfirmarIncorrecto = "- La solicitud debe estar en estado enviado.";
var mstrMsgEstCancelarIncorrecto = "- La solicitud debe estar en estado confirmado.";

var mstrEliminarErrorEstaExhi = "- Solo se puede eliminar exhibiciones en estado PENDIENTE.";
var mstrEliminarErrorEstaExhiDet = "- Solo se puede eliminar detalles de exhibiciones en estado PENDIENTE.";

var mstrErrorEnviarExhiPendente = "- No puede enviar la solicitud. El estado es diferente a Pendiente.";
var mstrErrorEnviarExhiEnvAprobado = "- No puede aprobar la solicitud. El estado es diferente a En Proceso.";
var mstrErrorEnviarExhiEnvCancelado = "- No puede cancelar la solicitud. El estado es diferente a Enviado.";
var mstrErrorEnviarExhiSinVin = "- Al menos uno de los detalle de la exhibicion debe tener VIN asignado.\n";
var mstrErrorEnviarExhiSinVin111 = "- Uno o mas de los detalles tiene asociado un VIN que no se\n  encuentra en estado disponible. Favor de cambiar el VIN al detalle.\n";
var mstrErrorEnviarExhiSinVin112 = "- La cantidad de detalles ingresados sobrepasa el tamaño\n  de la vitrina ideal para la ubicación seleccionada. No se puede realizar la acción.\n";
var mstrErrorCopiarExhi5 = "- La cantidad de copias que desea ingresar sobrepasaría el tamaño\n  de la vitrina ideal para la ubicación seleccionada.\n";

var mstrMsgEstadoExhibicionPendiente = " Pendiente";
var mstrMsgEstadoExhibicionEnProceso = " En Proceso"; //mstrMsgEstadoExhibicionEnviado = "Enviada"
var mstrMsgEstadoExhibicionAprobada = " Aprobado"; //mstrMsgEstadoExhibicionConfirmado = "Confirmada"
var mstrMsgEstadoExhibicionRechazada = " Rechazada"; //mstrMsgEstadoExhibicionCancelado = "Cancelado"

var mstrMsgErrorEstadoExhibicion = "- No se puede realizar la acción.\n  La exhibición debe encontrarse en estado";
var mstrMsgErrorEstadoActExhibicion = "- No se puede realizar la acción.\n  La exhibición se encuentra en estado";

var mstrErrorVinsTerminal = "- Algunos Vins seleccionados no están en estado En Terminal.\n";
var mstrErrorVinsVinsYatieneDA = "- No podrá realizar la acción. Algunos de los vins ya cuenta con una solicitud de orden para Depósito Aduanero.\n";
var mstrErrorVinsVinsYatieneID = "- No podrá realizar la acción. Algunos de los vins ya cuenta con una solicitud de orden para Importación Definitiva.\n";
var mstrErrorAlmaceneraOrdenCerrada = "- No se puede realizar la acción.\n  La orden se encuentra cerrada.\n";
var mstrNoEliminarEstadoPendiente = "No se puede eliminar. El estado es diferente de PENDIENTE.";


var mstrVinNoUbicado = "El VIN solo puede ser ubicado si se encuentra ingresado a almacén.";
var mstrVinYaEnPDI = "El VIN ya se encuentra dentro del proceso de PDI."

var mstrErrorWarrantSeguiFecBanco = "No se puede realizar la acción.\nLa fecha de confirmación es menor a la fecha de envío al banco.";


/*GUIA DE SALIDA Y REMISION*/
var mstrVinConGiaPendiente = "El VIN ya tiene una guia PENDIENTE para uno de los destinos.";
var mstrgiaYaRegistrada = "El nro de guía ya fue registrado en el Sistema.";
var mstrNoActualizableGuia = "No se podrá actualizar. El estado de la guía es diferente de PENDIENTE.";
var mstrNoActualizaEmitido = "No se actualizó al estado EMITIDO. El estado actual es diferente a PENDIENTE.";
var mstrNoActualizaNroRepetido = "El numero de guia debe ser mayor al correlativo actual";

var mstrNoActualizoEnTransito = "No se actualizó al estado EN TRANSITO. El estado actual es diferente a EMITIDO.";
var mstrNoActualizaTransitoUbicacion = "No se actualizó al estado EN TRANSITO. El origen de la guia es diferente a la ubicación del usuario.";
var mstrNoActializaRecepcionado = "No se actualizó al estado RECEPCIONADO. El estado actual es diferente a EN TRANSITO.";
var mstrNoActualizaRecepcionadoUbicacion = "No se actualizó al estado RECEPCIONADO. El destino de la guia es diferente a la ubicación del usuario.";
var mstrNoActualizaAnulado = "No se actualizó al estado ANULADO. El estado actual es diferente a EMITIDO.";
var mstrNoEliminarPendiente = "No se puede eliminar. El estado es diferente de PENDIENTE.";
/*RNapa - 02/03/2009 - Inicio*/
var mstrErrorOrdenGenerada = "La orden de despacho ya fue generada para el vin seleccionado.";
var mstrErrorNoOrdenGenerada = "El vin no se encuentra en PDI listo, la orden de despacho no puede ser generada.";
/*RNapa - 02/03/2009 - Fin*/

var mstrNotaPedidoEnviada = "Se envio la nota de pedido exitosamente.";
var mstrAprobarPedidoEnviada = "Se aprobo la nota de pedido exitosamente.";
var mstrAprobarNotaPedidoMasivo = "El(Los) registros(s) seleccionados se aprobaron exitosamente.";
var mstrRechazarPedidoEnviada = "Se rechazo la nota de pedido exitosamente.";
var mstrRechazarNotaPedidoMasivo = "El(Los) registros(s) seleccionados se rechazaron exitosamente.";
var mstrAnularNotaPedido = "Se anulo la nota de pedido exitosamente.";
var mstrCopiasExitoNotaPedido = "Se registro el numero de copias exitosamente.";
var mstrUsuSinPuntoVenta = "Usted no tiene asignado un punto de venta, por favor comunicarse con el administrador.";

var mstrErrorNotaPedidoNoOrdenesComerciales = "Solo se puede realizar envio sobre notas de pedido en estado registrado.";
var mstrErrorEnviarNotaPedido = "La nota de pedido no cumple con las políticas comerciales.";
var mstrErrorEnviarNotaPedidoContacto = "No se puede enviar la nota de pedido. Para los clientes de tipo jurídico se debe registrar los datos del contacto.";
var mstrErrorEstadoNotaPedido = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser en proceso o en lista de espera.";
var mstrErrorNotaPedidoSinVin = "La nota de pedido no cuenta con un VIN asociado.";
var mstrErrorVinNotaPedidoIndispuesto = "El VIN asociado a la nota de pedido no se encuentra disponible,\nfavor de seleccionar otro.";
var mstrListaEsperaNotaPedido = "En estos momentos no existen VIN'S disponibles.\nSu pedido ha sido puesto en lista de espera.";
var mstrErrorNotaPedidoAproLis = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser En Aprobación o En Lista de Espera.\n";
var mstrErrorNotaPedidoListaEspera = "Por el momento no existen VIN disponibles.\n La nota de pedido seguirá  En Lista de Espera.\n";
var mstrErrorNotaPedidoAprobacion = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser En Aprobación.\n";
var mstrErrorCotizacionRelaOtraNotaPedido = "La cotización seleccionada actualmente se encuentra relacionada a una nota de pedido.\n No se puede realizar la acción.\n";
var mstrErrorAprobarExNotaPedidoEstado = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser en proceso - excepción.";
var mstrErrorAprobarNotaPedidoAUTOPRO = "No se puede aprobar pues no se han asociado los anticipos con los comprobantes en AUTOPRO";
var mstrErrorAsignarNotaPedidoEstado = "La reserva no se encuentra en el estado correcto.\nEl estado debe ser reservado."
var mstrErrorReasignarNotaPedidoEstado = "La reserva no se encuentra en el estado correcto.\nEl estado debe ser reservado con VIN."
var mstrErrorCancelarNotaPedidoEstado = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser facturado.";
var mstrErrorCancelarNotaPedidoImporte = "El importe abonado no coincide con el total de la nota de pedido.No se puede realizar la acción.";

var mstrErrorAnularNotaPedidoEstado = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser reservado, reservado con VIN, o registrado.";
var mstrErrorAnularNotaPedidoAUTOPRO = "La nota de pedido no puede ser anulada en AUTOPRO. Consulte con el Administrador.";

var mstrErrorAsignarComprobante = "El comprobante seleccionado tiene un importe superior al permitido. El comprobante no puede ser asigndo.";
var mstrErrorGrabarDatosPropietario = "Error al grabar los datos del propietario."; //-4
var mstrErrorGrabarDatosCliente = "Error al grabar los datos del cliente."; //-5

/*OPERACIONES - NOTA PEDIDO*/
var mstrErrorAsigVin4 = "El vin no se encuentra disponible.\n";
var mstrErrorAsigVin5 = "El vin ya se encuentra asociado a otra nota de pedido.\n";
var mstrErrorAsigVin6 = "El cliente no existe en el sistema de AUTOPRO.\n";
var mstrErrorAsigVin7 = "El Numero de VIN  no existe en el sistema de AUTOPRO.\n";
var mstrErrorAsigVin8 = "No se puede asignar el VIN. La nota de pedido debe estar en estado Reservado.\n";
var mstrErrorAsigVin9 = "No se puede asignar un VIN a la nota de pedido pues no se cumple con la política actual de facturación.\n";
var mstrErrorAnularNotPed4 = "La nota de pedido no se encuentra en el estado correcto.\nSolo se puede realizar la acción si la nota de pedido se encuentra en lista de espera o asignado.\n";
var mstrErrorAnularNotPed5 = "El vin asociado a la nota de pedido no se encuentra en el estado correcto.\nEl vin debe estar en estado comercial reservado o facturado.\n";

var mstrErrorReasigVin6 = "El VIN ya se encuentra facturado.\n No se puede asignar otro VIN a la reserva.\n";
var mstrErrorReasigVin7 = "El VIN ya se encuentra cancelado.\n No se puede asignar otro VIN a la reserva.\n";
var mstrErrorReasigVin8 = "El VIN ya se encuentra entregado.\n No se puede asignar otro VIN a la reserva.\n";
var mstrErrorReasigVin9 = "El VIN ya se encuentra solicitado para despacho.\n No se puede asignar otro VIN a la reserva.\n";
var mstrErrorReasigVin10 = "El VIN se encuentra en proceso de despacho.\n No se puede asignar otro VIN a la reserva.\n";
var mstrErrorReasigVin11 = "El VIN ya se encuentra asignado en AUTOPRO.\n";

var mstrAbonoMayorTotal = "EL importe ingresado excede al total de la nota de pedido. El importe abonado debe ser menor.";
var mstrAbonoSustento10 = "No puede realizar la acción. El numero de operacion para el banco debe ser obligatorio.";
var mstrAbonoSustento11 = "El comprobante ya existe, debe realizar la asociación por la bandeja de comprobantes.";
var mstrAbonoSustento12 = "El comprobante ya existe, no tiene saldo disponible, ingresar a la bandeja de comprobantes.";
var mstrErrorEliminarNotaPedido = "La nota de pedido no puede ser eliminada.\nSe debe encontrar en estado registrado.";

var mstrErrorConfTransNoTrans = "El VIN no tiene transformación pendiente.";
var mstrErrorConfTransNoPend = "El VIN no cuenta con transformaciones pendiente. La transformación ya fue confirmada.";

var mstrErrorNoExistePoliticaFac = "No se puede asignar un VIN a la nota de pedido\npues no se cuenta con política actual de facturación para los datos seleccionados.\nFavor de consultar con el administrador.";
var mstrErrorNoCumplePoliticaFac = "La nota de pedido no cumple con la política actual de facturación y será enviada al Jefe de ADV para su aprobación.\n ¿Esta seguro de enviar la nota de pedido a aprobación?";
var mstrErrorNoCumpleAsigComprobantes = "No se puede asignar un VIN a la nota de pedido pues no todos los abonos realizados \nse encuentran asociados en AUTOPRO.";

//@0039 - RAL - Inicio
//var mstrErrorAsignacionVinFlag4 = "La nota de pedido de forma de pago diferente a contado debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
var mstrErrorAsignacionVinFlag4 = "La nota de pedido debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
//@0039 - RAL - Fin

var mstrErrorVinSinCuentaConNumeroDua = "El VIN no cuenta con numero de DUA.\nNo puede realizar esta acción.";
var mstrErrorNotaPedidoCopias = "Solo se puede realizar copias sobre notas de pedido en estado pendiente.";
var mstrErrorNoCumplePoliticaAsig = "La nota de pedido no cumple con la política actual de asignación y será enviada al Jefe de ADV para su aprobación.\n ¿Esta seguro de enviar la nota de pedido a aprobación?";
var mstrErrorNoExistePoliticaAsig = "No se puede asignar un VIN a la nota de pedido\npues no se cuenta con política actual de asignación para los datos seleccionados.\nFavor de consultar con el administrador.";

//ACTIVAR NOTA VENTA
var mstrSeguroActivarNotaVenta = "¿Está seguro de activar la nota de venta?";
var mstrErrorNoExistePolFacNotaVenta = "No se puede activar la nota de venta pues no se cuenta con política actual de facturación para los datos seleccionados.\nFavor de consultar con el administrador.";
var mstrErrorNoCumplePolFacNotaVenta = "La nota de venta no cumple con la política actual de facturación y será enviada al Jefe de ADV para su aprobación.\n ¿Esta seguro de enviar la nota de pedido a aprobación?";

//@0039 - RAL - Inicio
//var mstrErrorFacturacionVinFlag4 = "La nota de pedido de forma de pago diferente a contado debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
var mstrErrorFacturacionVinFlag4 = "La nota de pedido debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
//@0039 - RAL - Fin

var mstrErrorNoCumpleAsigCompNotaVenta = "No se puede activar la nota de venta pues no todos los abonos realizados se encuentran asociados en AUTOPRO.";

var mstrErrorActivarNotaVenta6 = "La nota de pedido no se encuenta en el estado correcto.\nEl estado debe ser reservado con vin y no estar activa.";
var mstrErrorActivarNotaVenta7 = "La nota de venta no cumple con la política actual de facturación.";

/*OPERACIONES - EXHIBICION*/
var mstrErrorAsigVinExh4 = "El vin no se encuentra disponible.\n";
var mstrErrorAsigVinExh5 = "El vin ya se encuentra asociado a otro proceso.\n";

/*OPERACIONES - ENVIO RECEPCION DUAS*/
var mstrErrorDuaNoRecep = "La DUA no ha sido recepcionada. Por favor realice primero la recepción.\n";
var mstrErrorDuaNoEntregada = "No se puede entregar la DUA pues el VIN no se encuentra cancelado.\n";

/*OPERACIONES - TRANSFORMACIONES*/
var mstrErrorTransEliminar5 = "No se puede eliminar la transformación. El estado es diferente a PENDIENTE.\n";
var mstrErrorTransDetEliminar5 = "No se puede eliminar el registro. El estado de la transformación es diferente a PENDIENTE.\n";
var mstrErrorTransformacion130 = "No se actualizó al estado ENVIADO. La transformación no cuenta con items en el detalle.\n";
var mstrErrorTransformacion131 = "No se actualizó al estado RECHAZADO. El estado actual es diferente a ENVIADO.\n";
var mstrErrorTransformacion132 = "No se actualizó al estado APROBADO. El estado actual es diferente a ENVIADO.\n";
var mstrErrorTransformacion134 = "No se actualizó el estado de la Transformación. No existe el registro en el Sistema.\n";
var mstrErrorTransformacion135 = "No se actualizó al estado ENVIADO. El estado actual es diferente a PENDIENTE.\n";
var mstrErrorNoAgregDifPend = "No podrá agregar registros. El estado de la transformación es diferente a PENDIENTE.\n";

/*OPERACIONES - CONCILIACION*/
var mstrModConciliadoLibRes4 = "La reserva no se encuentra en el estado correcto.\nLa reserva debe estar en estado facturado.\n";
var mstrModConciliadoLibRes5 = "La reserva ya se encuentra cerrada y/o enviada a la casa matriz.\nNo se puede realizar la acción.\n";
var mstrModConciliarADV4 = "No se puede realizar la acción.\nEl día ya fue cerrado.\n";
var mstrModConciliarCerEmp4 = "No existen registros pendientes por procesar.\n";
var mstrModConciliarCerEmp5 = "No se puede realizar la acción. Todos los ADV's deben de haber cerrado el día.\n";
var mstrModConciliarEnvCasMat4 = "No existen registros pendientes por procesar.\n";

//Conforme
var mstrErrorTransConforme133 = "No se actualizó al estado CONFORME. El estado actual es diferente a APROBADO.\n";
var mstrErrorTransConforme134 = "No se actualizó al estado CONFORME. No existe un registro de Transformación.\n";

/*COMERCIO EXTERIOR - LINEA CREDITO*/
var mstrErrorLinCreEliminar12 = "No se puede realizar la acción.\nLa empresa actualmente tiene compromiso de pago con el banco.\n";
var mstrErrorLinCreModificar11 = "No se puede realizar la acción.\nEl monto de la línea de crédito no puede ser menor al monto comprometido.\n";
var mstrErrorLinCreInsertar10 = "No se puede realizar la acción.\nYa existe una línea de crédito para el banco seleccionado.\n";

//Plan de Desembarque
var mstrErrorPlanDesembarque3 = "- No se encontraron coincidencias.\n Verificar Nave y Marca del Embarque.\n";
var mstrErrorPlanDesembarque4 = "- No se encontraron coincidencias.\n Verificar Vins, SpecCode, Marca y Modelo.\n";

//Adm Vins Seguimiento
var mstrErrorUsuSinCriteriosBusqueda = "- Usted no tiene asignado ningun de los dos ambitos de\n  criterios de busqueda, consulte con el administrador.\n";

/** AGREGADO RANCCANA 02/03/2009 **/
var mstrNoErrorValNumeracionByUsuario = "Usted no podrá generar la guía debido a que\nno tiene asignado un nro de serie para esta ubicación.";
var mstrNoErrorValNumeracionByUsuarioComercial = "Usted no podrá generar la guía debido a que\nel pedido del vin no tiene asignado una empresa comercial.";
var mstrNoErrorValNumeracion6 = "Usted no podra generar la guía debido a que\nel pedido del vin no tiene asignado una empresa comercial.";
var mstrNoErrorValNumeracion7 = "No podrá generar la guía. El vin debe tener una ubicación origen.";
var mstrNoErrorValNumeracion8 = "No podrá generar la guía debido a que los vin's tienen asignado más de una empresa comercial.";
var mstrNoErrorValNumeracion9 = "No podrá generar la guía. Los vins deben tener la misma ubicación origen.";

var mstrErrorSegCartasConf = "- No se puede realizar la acción.\nLas cartas antes han sido confirmadas.\n";
var mstrErrorSegAlgCartasConf = "- Algunas cartas no fueron confirmadas.\n";
var mstrErrorSegCartasImp = "- No se puede realizar la acción.\nLas cartas antes han sido impresas.\n";
var mstrErrorSegActCartasImp = "- Algunas impresiones no fueron actualizadas.\n";
var mstrErrorSegCartasConfUbi = "- No se puede realizar la acción.\nLas cartas antes han sido confirmadas.\n";
var mstrErrorSegAlgCartasSinConfUbi = "- Algunas cartas no fueron confirmadas.\n";

var mstrExitoSegCartasConf = "- Se confirmaron las cartas enviadas al banco exitosamente.\n"; //Se confirmaron las cartas de banco exitosamente
var mstrExitoSegCartasImp = "- Se confirmaron las cartas de impresion exitosamente.\n";
var mstrExitoSegCartasConfUbi = "- Se confirmaron las cartas de confirmacion ubicación exitosamente.\n";

var mstrExitoReasignarVinNotaPedido = "- Se reasigno el vin de la nota de pedido exitosamente.\n";
var mstrErrorReasignarVinNotaPedidoNoDisp = "- El VIN no se encuentra disponible.\n"; //-4
var mstrErrorReasignarVinNotaPedidoYaAsignado = "- El VIN ya se encuentra asociado a otra nota de pedido.\n"; //-5
var mstrErrorExtAlmaceneraOrdenNoCorresponde = "- No se puede realizar la acción.\n  La orden no corresponde a la almacenera.\n";
var mstrErrorSinRegStockDisponibleNotaPedido = "- La nota pedido no cuenta con stock disponible en este momento.\n"; //0
var mstrExitoPermisoSalidaDespacho = "- Se actualizo el permiso de salida del registro exitosamente.\n";

/*Agregado Edominguez 03/03/20009*/
var mstrNoEliminarPedidoPendiente = "No se puede eliminar porque existen pedidos pendientes de atención.";
var mstrNoeliminarTieneRelaModelo = "No se puede eliminar porque tiene relacion con Modelo.";

var mstrNoModificarTieneRelaSpecCode = "No se puede modificar porque tiene relacion con Spec Code.";
var mstrNoeliminarTieneRelaSpecCode = "No se puede eliminar porque tiene relacion con Spec Code.";
var mstrNoeliminarGrupoAsociadoSprcCode = "No se puede eliminar porque tiene asociado un Spec Code.";
/* FIN */

/*   AGREGADO RANCCANA 11/03/2009    */
var mstrMsgReporteCampoVacio = "";
var mstrMsgErrorEnviaNotaPedidoFlat1 = "No se puede enviar la nota de pedido pues no se cuenta con política actual para los datos seleccionados.\n Favor de consultar con el administrador";
var mstrMsgErrorEnviaNotaPedidoFlat2 = "La nota de pedido no cumple con el porcentaje mínimo de reserva y será enviada al Jefe de ADV para su aprobación.\n ¿Esta seguro de enviar la nota de pedido a aprobación?";

//@0039 - RAL - Inicio
//var mstrMsgErrorEnviaNotaPedidoFlat3 = "La nota de pedido de forma de pago diferente a contado debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
var mstrMsgErrorEnviaNotaPedidoFlat3 = "La nota de pedido debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
//@0039 - RAL - Fin
//@0043 NCP Inicio
var mstrMsgErrorEnviaNotaPedidoMasivaFlat1 = "No se puede enviar las notas de pedido {0} pues no se cuenta con política actual para los datos seleccionados.  \n Favor de consultar con el administrador";
var mstrMsgErrorEnviaNotaPedidoMasivaFlat2 = "Las notas de pedido {0} no cumple con el porcentaje mínimo de reserva y será enviada al Jefe de ADV para su aprobación.\n ¿Esta seguro de enviar la nota de pedido a aprobación?";
var mstrMsgErrorEnviaNotaPedidoMasivaFlat3 = "Las notas de pedido {0} debe de tener un sustento.\n¿Esta seguro de enviar la nota de pedido a un proceso de excepción?";
//@0043 NCP Fin
var mstrMsgConfirEnviaNotaPedidoAprobacion = "¿Está seguro de enviar la nota pedido a aprobación?";
var mstrErrorDetalleNotaPedido = "Los datos del detalle de la cotización no han sido obtenidos satisfactoriamente,\n por favor consulte con el administrador.";

var mstrErrorAdjMedioPagoReg = "No se puede realizar la acción.\nEl medio de pago no ha sido registrado.";
var mstrErrorCartaMedioPagoReg = "No se puede realizar la acción.\nLa carta de solicitud de medio de pago antes ha sido generada.";
var mstrErrorEliminarCartaMedioPago = "No se puede realizar la acción.\nEl adjunto corresponde a la carta de solicitud generada por el sistema.";
var mstrExitoAdjCartaMedioPago = "Se genero el documento de solicitud de medio de pago exitosamente.";
var mstrErrorPlantillaNoExiste = "El documento de plantilla de solicitud de medio de pago \nno esta registrado en el servidor de archivos,\n favor de registrarlo primero.";

//DAC - 24/02/2011 - Inicio
var mstrErrorGenerarDocumento = "Problemas al generar el documento, consulte con el administrador.";
var mstrErrorPlantillaRegistrada = "La plantilla para el documento no se encuentra registrada.\nComuníquese con el administrador.";
var mstrErrorPlantillaFormato = "El documento de plantilla no cuenta con el formato requerido.";
var mstrErrorPlantillaFileServer = "El documento de plantilla fue registrado pero no se encuentra en el servidor de archivos.";
//DAC - 24/02/2011 - Fin

var mstrErrorGenVinByNotaPedido = " Usted no podrá generar la guia debido\n a que no tiene una nota de pedido asociada al vin.";
var mstrErrorGenVinByNuSerieUbicacion = "Usted no podrá generar la guia debido\n a que no tiene asignado un nro de serie para esta ubicación.";
var mstrErrorNoModificaNroActual = "No puede ingresar un número actual menor al máximo generado.";

var mstrErrorModificaNroSerieDoc = "No puede ingresar un número de serie menor al máximo generado para la empresa.";
var mstrErrorModificaNroSerie1 = "No puede ingresar un número actual menor al máximo generado.";
var mstrErrorModificaNroSerie2 = "No puede ingresar un número actual menor al máximo generado.";

var mstrErrorVinCartaSolBanco = "No se puede realizar la acción.\nLos VIN's seleccionados deben corresponder a una misma carta de solicitud al banco.";
var mstrErrorNoModificaNroActual = "No puede ingresar un número actual menor al máximo generado.";

/* Mensajes Nuevos Ranccana 24/04/2009 */
var mstrErrorModificarAutoProMoneda = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Moneda en SGA.";
var mstrErrorAgregarAutoProMoneda = "No podrá agregar la moneda.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorEliminarAutoProMoneda = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Moneda en SGA";
var mstrErrorModificarAutoProMarca = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Marca en SGA.";
var mstrErrorEliminarAutoProMarca = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Marca en SGA.";
var mstrErrorModificarAutoProPais = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Pais en SGA.";
var mstrErrorEliminarAutoProPais = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Pais en SGA.";
var mstrErrorAgregarAutoProPais = "No podrá agregar el país.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";

var mstrErrorModificarAutoProCarroceria = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Carroceria en SGA.";
var mstrErrorEliminarAutoProCarroceria = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Carroceria en SGA.";
var mstrErrorModificarAutoProCategoria = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Categoria en SGA.";
var mstrErrorEliminarAutoProCategoria = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Categoria en SGA.";
var mstrErrorModificarAutoProColorInterior = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Color Interior en SGA.";
var mstrErrorEliminarAutoProColorInterior = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Color Interior en SGA.";
var mstrErrorInsertarAutoProColorInterior = "No podrá insertar.\nNo se tiene registrado la Marca y Modelo en Spec Code de Auto Pro en SGA.";
var mstrErrorModificarAutoProColorExterior = "No podrá modificar.\nNo se tiene el código AutoPro de la tabla Color Exterior en SGA.";
var mstrErrorEliminarAutoProColorExterior = "No podrá eliminar.\nNo se tiene el código AutoPro de la tabla Color Exterior en SGA.";
var mstrErrorInsertarAutoProColorExterior = "No podrá insertar.\nNo se tiene registrado la Marca y Modelo en Spec Code de Auto Pro en SGA.";
var mstrErrorInsertarTipoCombustibleAutoPro = "No podrá agregar el tipo combustible.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";


var mstrErrorModificarCodAutoPro = "No podrá modificar. Ya existe el código AutoPro en el Sistema.";
var mstrErrorCodAutoProNoExiste = "El codigo AutoPro ingresado no existe en la Base de Datos externa AutoPro.";
var mstrErrorCodAutoProNoExisteEnUnaBD = "No podra modificar el registro.\nEl codigo AutoPro ingresado no existe en al menos una Base de Datos externa AutoPro.";

//COLOR ADUANA
var mstrErrorEliminarColorAduana9 = "No podrá eliminar. No se tiene el código AutoPro de la tabla Color Aduana en SGA.";
var mstrErrorEliminarColorAduana10 = "No podrá eliminar. No se tiene el código UltraGestion de la tabla Color Aduana en SGA.";
var mstrErrorInsertarColorExteriorUltragestion11 = "No se puede insertar. El código Ultragestión autogenerado ya existe registrado. Intente nuevamente.";

//COLOR INTERIOR
var mstrModificarColorInterior10 = "El color Aduana seleccionado debe estar completamente llenado (AutoPro).";
var mstrInsertarColorInterior5 = "El color Aduana seleccionado debe estar completamente llenado (AutoPro).";
var mstrEliminarColorInterior11 = "El color Aduana seleccionado debe estar completamente llenado (AutoPro).";
var mstrModificarColorExterior10 = "El color Aduana seleccionado debe estar completamente llenado (AutoPro).";
var mstrInsertarColorExterior5 = "El color Aduana seleccionado debe estar completamente llenado (AutoPro).";
var mstrEliminarColorExterior11 = "El color Aduana seleccionado debe estar completamente llenado (AutoPro).";

var mstrErrorModificarUltragestionColorExterior = "No podrá modificar.\nNo se tiene el código Ultragestión de la tabla Color Exterior en SGA.";
/* Fin */

/*   FIN  */
/*MODELO*/
var mstrModModelo5 = "El código de modelo ya se encuentra registrado.";
var mstrModmodelo7 = "No podrá modificar.\nYa existe el código AutoPro en el Sistema.";
var mstrModeloExistUltraGestion = "No podrá modificar.\nYa existe el código UltraGestión para otro modelo en la Bd SGSNET.";
var mstrModeloExistAutoPro = "No podrá modificar.\nYa existe el código AutoPro para otro modelo en la Bd SGSNET.";
var mstrModeloExistSGSNET = "No podrá modificar.\nYa existe el código Modelo en la Bd SGSNET.";


var mstrEliColorExterior10 = "No podrá eliminar.\nNo se tiene el código UltraGestion de la tabla Color Exterior en SGA.";
var mstrModColorExterior8 = "No podrá modificar.\nYa existe el código UltraGestion en el Sistema.";
var mstrErrorModiModeloAutopro = "No podrá modificar el modelo.\nEl código AutoPro del modelo ingresado debe existir al menos en una Bd AutoPro.";
var mstrErrorModiModeloUltragestion = "No podrá modificar el modelo.\nEl código Ultragestión del modelo ingresado debe existir al menos en una Bd Ultragestión.";
var mstrErrorAgregarModeloAutopro = "No podrá agregar el modelo.\nLa marca debe tener un código de AutoPro al menos en una BD.";
var mstrErrorAgregarModeloUltragestion = "No podrá agregar el modelo.\nLa marca debe tener un código de Ultragestión al menos en una BD.";
var mstrErrorAgregarModeloAutopro13 = "No podrá agregar el modelo. El código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarModeloUltragestion14 = "No podrá agregar el modelo. El código Ultragestión autogenerado ya existe en la Bd Ultragestión.\nIntente nuevamente.";
var mstrErrorModiMarcaAutopro = "No podrá modificar la marca.\nEl código AutoPro de la marca ingresado debe existir al menos una Bd AutoPro.";
var mstrErrorModiMarcaUltragestion = "No podrá modificar la marca.\nEl código Ultragestión de la marca ingresado debe existir al menos una Bd Ultragestión.";
var mstrErrorAgregarMarcaAutopro = "No podrá agregar la marca.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarMarcaUltragestion = "No podrá agregar la marca.\nEl código Ultragestión autogenerado ya existe en la Bd Ultragestión.\nIntente nuevamente.";
var mstrErrorAgregarMarcaModUltra = "No podrá crear el SpecCode.\nEl código AutoPro de la Marca y SpecCode ingresados no existen en la Bd AutoPro.\nIntente nuevamente.";

var mstrErrorAgregarCarroceriaAutoPro = "No podrá agregar la carrocería.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarCategoriaAutoPro = "No podrá agregar la categoría.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarTraccionAutoPro = "No podrá agregar la tracción.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarTransmisionAutoPro = "No podrá agregar el Tipo de Transmisión.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarColorExtAutoPro = "No podrá agregar el color exterior.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";
var mstrErrorAgregarColorIntAutoPro = "No podrá agregar el color interior.\nEl código AutoPro autogenerado ya existe en la Bd AutoPro.\nIntente nuevamente.";

var mstrErrorMonedaInsertSimbolo = "No podrá insertar la moneda.\nYa existe el simbolo ingresado.";
var mstrErrorMonedaUpdateSimbolo = "No podrá modificar la moneda.\nYa existe el simbolo ingresado.";

var mstrErrorSpecCodeUpdateAutoPro = "No podrá modificar el spec code.\nEl código AutoPro de la marca ingresado debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProMarca = "No podrá modificar el spec code.\nEl código AutoPro de la marca ingresado debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProMarcaModelo = "Los códigos AutoPro Marca-Modelo ingresados debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProModelo = "No podrá modificar el spec code.\nEl código AutoPro del modelo ingresado debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProCarroceria = "No podrá modificar el spec code.\nEl código AutoPro de la carroceria ingresado debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProCategoria = "No podrá modificar el spec code.\nEl código AutoPro de la categoria ingresado debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProMarcSpec = "No podrá modificar el spec code.\nEl código AutoPro de la Marca-SpecCode debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProPais = "No podrá modificar el spec code.\nEl código AutoPro del País debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProTransmi = "No podrá modificar el spec code.\nEl código AutoPro Tipo de Transmisión debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProTracc = "No podrá modificar el spec code.\nEl código AutoPro de la Tracción debe existir al menos en una Bd AutoPro.";
var mstrErrorSpecCodeUpdateAutoProCombus = "No podrá modificar el spec code.\nEl código AutoPro del Tipo de Combustible debe existir al menos en una Bd AutoPro.";



var mstrErrorSpecCodeObligAutoProMarca = "El código marca autopro tiene data no válida.";
var mstrErrorSpecCodeObligAutoProSpec = "El código spec code autopro tiene data no válida.";
var mstrErrorSpecCodeObligAutoProCateg = "El código del atributo categoría autopro no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProComb = "El combustible no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProAnoFab = "El año fabricación tiene data no válida.";
var mstrErrorSpecCodeObligAutoProCilindra = "El atributo Cilindradada (cc) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProCilindro = "El atributo Numero de cilindros no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProNroRued = "El atributo Número de ruedas no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProNroEje = "El atributo Cantidad de ejes no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProDistanEje = "El atributo Distancia entre ejes (mm) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProPuerta = "El atributo Puertas no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProAltura = "El atributo Altura (mm) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProAncho = "El atributo Ancho (mm) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProLargo = "El atributo Largo (mm) no fue ingresado o tiene data no válida.";
//RNapa - Nuevo Parametro Autopro - 23/09/2009
var mstrErrorSpecCodeObligAutoProPotencia = "El atributo Potencia del motor (Hp/rpm) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProNeumatico = "El atributo Neumáticos no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProCategoria = "La categoria no fue ingresada o tiene data no válida.";
//RNapa - Fin - 23/09/2009
//RNapa - Nuevo Parametro Autopro - 24/09/2009
var mstrErrorSpecCodeObligAutoProCarroceria = "La carrocería no fue ingresada o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProFamilia = "La familia no fue ingresada o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProAro = "El atributo Diámetro de Aro Delantero (Pulgadas) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProPotenciaPeso = "El atributo Potencia del motor (Kw/rpm) no fue ingresado o tiene data no válida.";
var mstrErrorSpecCodeObligAutoProTamanio = "Existen atributos que exceden el maximo tamaño permitido por el campo de Bd.";
//RNapa - Fin - 23/09/2009

/*MANTENIMIENTO GRUPO*/
var mstrErrorInsModGrupo5 = "No se pudo realizar la acción.\nEl registro se encuentra asociado a otro registro.";
var mstrErrorInsModGrupo6 = "No se pudo realizar la acción.\nEl nombre del grupo ya se encuentra registrado.";
var mstrErrorInsModGrupo7 = "No se pudo realizar la acción.\nEl nro. de orden registrado ya existe.";
var mstrErrorInsModGrupo8 = "No se pudo realizar la acción.\nEl nro. de orden debe ser menor que el total de grupos registrados.";

/*CONFIGURACIONES CONTENIDO NOTA PEDIDO*/
var mstrErrorInsConNotaPedido10 = "No se puede realizar la acción.\nYa existe un registro para misma la marca, linea y canal de venta";

/**DESPACHO**/
var mstrExitoGrabarVinDespacho2 = "El VIN fue ingresado a despacho,\npero sin haber completado el tiempo estimado en PDI.";
var mstrExitoGrabarVinDespacho3 = "El VIN fue despachado, pero sin haber completado el tiempo estimado en Despacho.";

var mstrEnvioExitoCotizacion = "Se envio exitosamente.";

/**GUIAS DE REMISION**/
var mstrErrorGuiaRemisionUbicaOrigenCentral = "No se puede realizar la acción.\nLa ubicación origen del VIN debe ser almacen central.";

var mstrErrorAntesAdjuntarDocNotaPedido = "No se puede realizar la acción.\nAntes debe adjuntar los documentos obligatorios para esta etapa.";
var mstrErrorGuiaSinPermisoSalida = "No se podrá imprimir. La guia de despacho no tiene permiso de salida.";
var mstrErrorNotasPedidoPendCancel = "No se puede realizar la acción. Existen notas de pedido pendientes de cancelación.";

var mstrErrorEstadoEmitidoByUbicacion = "No se actualizó al estado EMITIDO.\nLa ubicación actual del VIN no coincide con la ubicación de origen de la guía."; //-123
var mstrErrorEstadoEmitidoByGuiaProceso = "No se actualizó al estado EMITIDO.\nEl VIN cuenta con otra Guia en proceso."; //-130

var mstrErrorNoExisteGuiaAnular = "No existe guia para anular.";
var mstrErrorGuiaAnularByVinTransito = "No se puede anular la guia poque el VIN se encuentra en Transito.";
var mstrErrorGuiaAnularByGuiaRecepcionada = "No se puede anular la guia poque la guia se encuentra recepcionada.";
//var mstrErrorGenerarGuiaSalida              = "No se puede volver a generar una guia de salida pues el VIN ya cuenta con una guia de salida.";
var mstrErrorGenerarGuiaSalida = "El VIN ya cuenta con una guia de salida.";
var mstrErrorNoExisteGuiaSalida = "No existe guia de salida para el VIN seleccionado.";
var mstrSeguroAnular = "¿Está seguro de anular?";

/*GUIAS REMISION*/
var mstrModEstadoGuia131 = "Usted no podrá generar la guia debido a que no tiene asignado un nro de serie para esta ubicación.";
var mstrModEstadoGuia132 = "Usted no podrá generar la guia debido a que no tiene asignado un nro de serie para esta ubicación.";
var mstrModEstadoGuia133 = "El nro de guía ya fue registrado en el Sistema.";

var mstrErrorClienteDocumento = "No existe un cliente con el nro documento ingresado.";
var mstrErrorClienteMuchosDocumento = "Existe mas de un cliente con el nro documento ingresado.";

//SEGUIMIENTO VALIDACION MASIVO
var mstrValidacionSegMasivo4 = "ya tiene una orden de despacho.\n";
var mstrValidacionSegMasivo5 = "no tiene estado correcto. El vin debe de estar en PDI Listo.\n";
var mstrValidacionSegMasivo6 = "no se encuentra dentro del almacén central. No puede realizar esta acción.\n";
var mstrValidacionSegMasivo7 = "no pertenece a un negocio que permita la movilizacion masiva. No puede realizar esta acción.\n";
var mstrValidacionSegMasivo8 = "no cuenta con numero de DUA. No puede realizar esta acción.\n";

var mstrErrorEliminarMedioPagoCerrado = "No se puede realizar la acción.\nEl estado del registro debe ser distinto a CERRADO.\n";
var mstrErrorEliminarMedioPagoFinAsociados = "No se puede realizar la acción.\nEl medio de pago cuenta con financiamientos asociados.\n";

//DESADUANAJE
var mstrExitoDesaduanajeOrdenes = "Las ordenes se generaron exitósamente.";
var mstrExitoDesaduanajeOrdenesCorreos = "Las ordenes se generaron exitósamente; pero los correos no se pudieron enviar.";

var mstrExitoEnviarCorreoAgenteAduanas = "Se envio el archivo exitosamente.";
var mstrErrorEnviarCorreoAgenteAduanas = "Problemas al enviar el archivo, consulte con el administrador.";

var mstrErrorEnviarDepositoAduaneroImportDefinitiva = "No se podrá enviar a Importación Definitiva.\n Uno o más vines seleccionados son de Venta liberada.";

//ACTIVAR GARANTIA SEGUIMIENTO
var mstrSeguroActivarGarantia = "¿Está seguro de activar la garantia de los vin(es) seleccionado(s)?";
var mstrExitoActivarGarantia = "Se activaron las garantias de los vines exitosamente.";
var mstrErrorVinPDI = "Algun vin tiene PDI.";
var mstrErrorVinTienePlaca = "Algún vin ya tienen placa.";
var mstrErrorVinNoExiste = "Algun vin no existe.";
var mstrErrorVinNoEntregado = "Algun vin no está entregado.";

var mstrErrorVinProcesoPDI = "No se puede actualizar la ubicación al VIN pues se encuentra en proceso de PDI."; //-4
var mstrErrorVinProcesoDespacho = "No se puede actualizar la ubicación del VIN pues se encuentra en proceso de Despacho."; //-5

//TARIFARIO
var mstrErrorEliminarTarifario = "No se puede eliminar ya que el Tarifario está Vigente.";
var mstrErrorFechaVigenciaTarifario = "Existe mas de un registro con la misma fecha de vigencia.";
var mstrErrorConcesionarioTarifario = "Un concesionario solo debe poder estar asociado a un solo tarifario pendiente.";
var mstrErrorNombreTarifarioExiste = "El nombre del tarifario ingresado ya se encuentra registrado.";
var mstrCopiasExitoTarifario = "Se realizo la copia del tarifario exitosamente.";

//Nota Pedido ADV por Punto de Venta
var mstrErrorADVConfiguradoNoExiste = "No existe ningun ADV configurado, no podra enviar la nota de pedido.";
var mstrErrorIngreseValor = "- Debe ingresar una valor de ";
var mstrErrorIngreseMenorACien = " menor o igual a 100.\n";

//ADV FINMEISTER
var mstrExitoLimiparUsuarioADV = "Se limpio exitosamente el adm. venta.";
var mstrExitoLimiparUsuarioFinmeister = "Se limpio exitosamente el usuario finmeister.";
var mstrSeguroLimpiarUsuarioADV = "¿Está seguro de limpiar el adm. venta?";
var mstrSeguroLimpiarUsuarioFinmeister = "¿Está seguro de limpiar el usuario finmeister?";

//Spec Code
var mstrErrorEliminarMasivoSpecCodeAtributos = "Uno o mas de los atributos seleccionados no puede ser eliminado pues son parte de la ficha aduanera.";
var mstrErrorSpecCodeAtributo = "El código ingresado se encuentra repetido para otro registro";
var mstrExitoSpecCodeNoVigente = "Se cambiaron a no vigente los registros exitosamente.";

//Clientes
var mstrErrorClienteDireccion = "Otro Cliente con el mismo numero de doc tiene una direccion similar.";
var mstrErrorClienteDireccion11 = "Existen facturas asociadas a la dirección.";
var mstrErrorClienteDireccion12 = "Ya existe una direccion para el mismo distrito.";

// flindrs - Carrocero
var mstrVinEnCarrocero = "Existen Vines que se encuentran en carrocero y requieren ser despachados.";
var mstrEstadoNull = "Error al Insertar Valor Null.";
var mstrEstadoUnique = "Error valor unique.";
var mstrEstadoUpdateOk = "Se recepciono el vin en carrocero correctamente.";
var mstrStockConIncidencia = "No se puede abrir el stock pues el VIN:{0} cuenta con incidencias pendientes.";
//Exhibicion anular vin
var msgErrorAnularVin = "La exhibición no se encuentra en el estado correcto. La exhibición debe estar Aprobada o Aprobada Parcial.";
var msgErrorAnularVinVarios = "Alguno de los vines seleccionados no pueden ser cerrados por los sigiuentes motivos:";
var msgEstAnuladoExito = "Se Anulo el vin exitosamente.";


//Niveles de Aprobacion
var mstrErrorNivelAprobacionModOriDestino = "No se puede incluir el Modelo Origen como Destino."; // -10
var mstrErrorNivelAprobacionModOriSinNivel = "El Modelo Origen no tiene Niveles de Aprobacion."; // -11

// Carrocero 
var mstrEstadoGuiaIncorrecto = "El estado de la guia no es el correcto, debe estar en transito."; // -250
var mstrErrorGrabarEstadoCarrocero = "Debe Recepcionar el vin antes de cerrar."; // -250
var mstrErrorGrabarEstadoGuia = "El estado de la guia no es el correcto, debe estar emitida."; // 251

//Aprobacion Precios Concesionario
var mstrErrorAprobacionPrecioConcesionario4 = "El precio es menor al minimo configurado."; // -4
var mstrErrorAprobacionPrecioConcesionario5 = "No existe usuario para aprobar."; // -5
var mstrErrorAprobacionPrecioConcesionario6 = "La nota de pedido no está en el estado correcto."; // -6

var mstrErrorNotaPedidoPendienteDescuento = "La Nota Pedido está pendiente de aprobación de descuento."; // -17

//Imprimir Duas
var mstrImprimirDua = "¿Desea imprimir el cargo?"; // -17   
//PDI en sucursales
var mstrNoExisteVin = "El vin no existe ó se encuentra desactivo.";
var mstrNoSucursal = "El vin no se encuentra en sucursal ó en un concesionario.";
var mstrKeyEnProceso = "El vin ya se encuentra en estado En Proceso.";
var mstrSolicitaPDI = "El vin requiere pasar por PDI.";
var mstrFinalizado = "El vin ya se encuentra Finalizado ó aun no tiene un proceso iniciado.";
var mstrErrorAsignarVinPDI = "No puede iniciar el proceso.\nEl estado debe ser reservado con VIN.";  //DAC - 23/02/2011 - I-F

var mstrErrorGrabarNivelAprobacionDuplicado = "El nivel de aprobacion ingresado ya existe en el sistema, favor de ingresar otro.";

//NIveles de Aprobacion Pedido
var mstrAprobarComexPedidoEnviada = "Se aprobo el pedido exitosamente.";
var mstrErrorNivelAprobacionPedido100 = "No existe usuario para aprobar.";
var mstrErrorNivelAprobacionPedido101 = "El monto es menor al minimo configurado.";
var mstrErrorNivelAprobacionPedido102 = "El pedido no tiene configurado el nivel de aprobacion. No se puede realizar la acción.";
var mstrErrorNivelAprobacionPedido103 = "En proceso de aprobacion.";
var mstrExitoPedidoNotificar2daVez = "El pedido se actualizo exitosamente.";

//GNV
var mstrErrorGrabarGNV200 = "No se pudo insertar. Ya existe un planning con la misma descripción.";
var mstrErrorGrabarGNV201 = "No se pudo insertar. El vin ya ha sido registrado.";
var mstrErrorGrabarPlanningGNV = "Los vines no tienen el mismo modelo, spec code, color exterior y color interior.";
var mstrErrorValidarPlanningGNV = "Los vines seleccionados no tienen el mismo modelo, spec code, color exterior y color interior.";
var mstrErrorValidarPlanningGNV202 = "El embarque seleccionado no tiene vines, no podra realizar la acción.";
var mstrErrorValidarPlanningGNV203 = "Los vines seleccionados no pertenecen al embarque seleccionado, no podra realizar la acción.";
var mstrErrorValidarPlanningGNV204 = " Los vines consultados no pertenecen al embarque, marca y modelo seleccionado o se encuentran en GNV.\n Primero realice la búsqueda por embarque, marca y modelo.";
var mstrErrorPDINoSolicitado = "El VIN no puede ser ingresado a PDI pues no se encuentra solicitado.";
var mstrErrorODEstadoNoCorrecto = "El VIN no puede ser ingresado a PDI pues la OD no esta en el estado correcto.";

//Asignacion Canal Vin
var mstrExitoAsignacionCanalVin = "Se asignaron los canales a los vines seleccionados exitosamente.";
var mstrErrorCanalVin = "No se puede realizar la acción. El vin {0} no cuenta con canal de venta asignado.";

var mstrClienteDiferentePropietario = "No se puede realizar la acción. Los datos del cliente deben ser diferentes a los datos del propietario.";
var mstrAnularNotaPedidoNoJADV = "No se puede realizar la acción. El Vin cuenta con guía de remisión Emitida o se encuentra en el concesionario destino. Consultar con el Jefe de ADV.";
var mstrAnularNotaPedidoNoJADV_LE = "No se puede realizar la acción. Consulte con el Jefe de ADV.";
var mstrReservarNPNoStock = "No se puede reservar la nota de pedido, no se cuenta con stock disponible.";

//DAC - 02/12/10 - Inicio
var mstrErrorPlantillaBD = "El documento de plantilla fue creado pero no se encuentra registrada en la base de datos para ser linkeada.";
//DAC - 02/12/10 - Inicio

//DAC - 03/12/10 - Inicio
var mstrErrorNivelAprobacionPedido150 = "No se puede realizar. Debe adjuntar el archivo Rundown para notificar el pedido.";
//DAC - 03/12/10 - Fin 

//DAC - 10/12/10 - Inicio
var mstrDetOrdEnvWarrantCapNotificarVacio = "No existen datos a mostrar para el detalle de la Orden de Envío seleccionada.";
//DAC - 10/12/10 - Fin 

//DAC - 13/12/10 - Inicio
var mstrSeguroEnviarWC = "¿Está seguro de enviar?";
//DAC - 13/12/10 - Fin 

//DAC - 14/12/2010 - Inicio
var mstrModFinanciamiento30 = "No se puede realizar la acción.\nYa existe el número de financiamiento para el banco.";
//DAC - 14/12/2010 - Fin

//DAC - 07/01/2011 - Inicio
var mstrNoAbrirBolsaUbicaciones = "El punto de venta seleccionado no pertenece al canal de venta concesionario.";
var mstrNoSelMuchosBolsaUbicaciones = "Debe seleccionar solo un punto de venta de tipo canal\nde venta concesionario para ingresar a ubicaciones.";
var mstrNoDirectoBolsaUbicaciones = "Debe seleccionar un punto de venta de tipo canal de venta\nconcesionario para ingresar a ubicaciones.";
//DAC - 07/01/2011 - Fin

//DAC - 03/12/10 - Inicio
var mstrErrorPlantillaBD = "El documento de plantilla fue creado pero no se encuentra registrada en la Base de Datos para ser linkeada.";
var mstrErrorNivelAprobacionPedido150 = "No se puede realizar. Debe adjuntar el archivo Rundown para notificar el pedido.";
//DAC - 03/12/10 - Fin 

//DAC - 07/02/2011 - Inicio
var mstrErrorActivarNotaVenta9 = "Por políticas de la MARCA, esta unidad no permite \nfacturar con fecha del real arribo posterior a la fecha actual.";
//DAC - 07/02/2011 - Fin

//DAC - 14/02/2011 - Inicio
var mstrErrorAdmComprobante5 = "Solo puede modificar comprobantes en estado pendiente o estado exceptuado.";
var mstrErrorAdmComprobante6 = "Debe ingresar el número de operación del banco.";
var mstrErrorAdmComprobante7 = "Número de operación duplicado a nivel de banco, moneda y fecha.";
var mstrErrorAdmComprobante8 = "No existe el tipo de cambio con fecha del comprobante.";
var mstrErrorAdmComprobante9 = "Solo puede modificar comprobantes en estado exceptuado con saldo mayor que cero.";
var mstrErrorAdmComprobante10 = "El comprobante está asociado a una nota de pedido cancelada o entregada.";
//DAC - 14/02/2011 - Fin

//DAC - 16/02/2011 - Inicio
var mstrErrorAdmComprobanteEliminar5 = "Solo puede eliminar comprobantes en estado pendiente.";
var mstrErrorAdmComprobanteEliminar6 = "No se pudo realizar la acción. El comprobante está asignado a un pedido.";

var mstrErrorAdmComprobanteCDC1 = "Debe seleccionar un comprobante para asignar CDC.";
var mstrErrorAdmComprobanteCDC5 = "El comprobante tiene notas de pedido canceladas.";
var mstrErrorAdmComprobanteCDC6 = "No se pudo realizar la acción. El comprobante se encuentra cerrado.";
//DAC - 16/02/2011 - Fin

//DAC - 17/02/2011 - Inicio
var mstrMensajeEnviarComprobante = "Se confirmo el envío de comprobante exitosamente.";
var mstrNoEnvioComprobante = "No se pudo realizar la acción, consultar con el administrador.";
var mstrErrorAdmComprobanteEnviarC5 = "Solo puede enviar comprobantes en estado pendiente.";
var mstrErrorAdmComprobanteEnviarC6 = "El comprobante debe tener medio de pago asignado.";
var mstrErrorAdmComprobanteEnviarC7 = "El medio de pago debe tener el indicador de confirmación de abono encendido.";
var mstrErrorAdmComprobanteEnviarC8 = "Debe ingresar el número de operación del banco.";
var mstrErrorAdmComprobanteEnviarC9 = "Solo puede confirmar comprobantes en estado enviado o en estado excepción.";
var mstrErrorAdmComprobanteEnviarC10 = "Debe ingresar el número de operación del banco.";
var mstrErrorAdmComprobanteEnviarC11 = "Debe ingresar la fecha del comprobante.";
var mstrErrorAdmComprobanteEnviarC12 = "Debe ingresar el número de la cuenta corriente.";
var mstrErrorAdmComprobanteEnviarC13 = "Solo puede confirmar comprobantes en estado enviado o estado excepción.";
var mstrErrorAdmComprobanteEnviarC14 = "Solo puede enviar a excepción comprobantes en estado enviado.";
var mstrErrorAdmComprobanteEnviarC15 = "No se pudo realizar la acción. El comprobante se encuentra cerrado.";

var mstrMensajeCerrarComprobante = "Se confirmo el cierre de comprobante exitosamente.";
var mstrNoCerrarComprobante = "No se pudo realizar la acción, consultar con el administrador.";

var mstrSeguroEnviarComprobanteUno = "¿Está seguro de enviar?";
var mstrSeguroCerrarComprobanteUno = "¿Está seguro de cerrar?";
var mstrSeguroAsignarComprobanteUno = "¿Está seguro de asignar CDC?";
//DAC - 17/02/2011 - Inicio

//DAC - 21/02/2011 - Inicio
var mstrErrorNPComprobanteEliminar15 = "No se pudo realizar la acción. El comprobante se encuentra cerrado.";
var mstrErrorNPComprobanteEliminar12 = "El comprobante no tiene saldo disponible.";
var mstrErrorNPComprobanteEliminar11 = "El abono se encuentra asociado a un comprobante AUTOPRO.";
var mstrErrorNPComprobanteEliminar10 = "La nota de pedido no tiene saldo pendiente.";
var mstrErrorNPComprobanteEliminar9 = "No se puede realizar la acción. La nota de pedido pertenece a otro ADV.";
var mstrErrorNPComprobanteEliminar8 = "No se puede realizar la acción. El perfil no es el correcto. El asesor comercial\n solo puede abonar notas de pedido en estado registrado, en proceso ó reservado.";
var mstrErrorNPComprobanteEliminar7 = "No se puede realizar la acción. La nota de pedido pertenece a otro asesor comercial.";
//@0036 NCP Inicio, se ccambio el mensaje
//var mstrErrorNPComprobanteEliminar6 = "No se puede realizar la acción. El perfil no es el correcto.\nEl asesor comercial solo puede abonar notas de pedido en estado registrado, en proceso ó reservado.";
var mstrErrorNPComprobanteEliminar6 = "No se puede realizar la acción. El perfil no es el correcto.\nEl asesor comercial solo puede abonar notas de pedido en estado Registrado, En Proceso, Lista de Espera, Reservado con Vin, Reservado ó Facturado.";
//@0036 NCP Inicio
var mstrErrorNPComprobanteEliminar16 = "No se podrá modificar ó eliminar comprobantes relacionados a\nnotas de abono en estado cancelado o entregado.";
var mstrErrorNPComprobanteEliminar5 = "No se puede realizar la acción. El abono esta relacionado a una nota de pedido masiva.";

var mstrErrorGrabarNPComprobante = "No se pudo realizar la acción, consultar con el administrador.";
var mstrkeyGrabarNPComprobante = "Se asignó la nota de pedido al comprobante exitosamente.";
//DAC - 21/02/2011 - Fin

//DAC - 22/02/2011 - Inicio
var mstrErrorCerrarComprobante5 = "No se puede realizar la acción. El comprobante se encuentra cerrado.";
//DAC - 22/02/2011 - Fin

//DAC - 23/02/2011 - Inicio
var mstrErrorPlantillaWarrantNoExiste = "La plantilla para el documento no se encuentra registrada.\nComuníquese con el administrador.";
//DAC - 23/02/2011 - Fin

//DAC - 25/02/2011 - Inicio
var mstrErrorGrabarDescMPDuplicado = "La descripción del medio de pago ya se encuentra registrado.";
//DAC - 25/02/2011 - Fin

//DAC - 31/01/2011 - Inicio
var mstrErrorGrabarCodMPDuplicado = "El código del medio de pago ya se encuentra registrado.";
//DAC - 31/01/2011 - Fin

//DAC - 01/03/2011 - Inicio
var mstrErrorNPAbonoAsesor12 = "No existe el tipo de cambio para esta fecha.";
//@0036 NCP Inicio
//var mstrErrorNPAbonoAsesor11 = "No puede utilizar este comprobante, pertenece a\notro ADV."; 
var mstrErrorNPAbonoAsesor11 = "No puede realizar la acción. La Suma de abonos\nes mayor al importe del comprobante.";
//@0036 NCP Fin
var mstrErrorNPAbonoAsesor9 = "El comprobante ya existe. No tiene saldo disponible,\ningresar por la bandeja de comprobantes.";
var mstrErrorNPAbonoAsesor8 = "El comprobante ya existe. Debe realizar la asociación\npor la bandeja de comprobantes.";
var mstrErrorNPAbonoAsesor7 = "No puede realizar la acción. El número de comprobante\npara el banco debe ser obligatorio.";
var mstrErrorNPAbonoAsesor6 = "No puede realizar la acción. El perfil no es el correcto.\nSolo el asesor comercial concesionario en el estado lista de\nespera ó reservado puede realizar abonos.";
var mstrErrorNPAbonoAsesor5 = "No puede realizar la acción. El perfil no es el correcto.\nSolo el asesor comercial sucursal o corporativo en el estado\nreservado puede realizar abonos.";
var mstrErrorNPAbonoAsesor4 = "El importe ingresado excede al total de la nota de pedido.\nEl importe abonado debe ser menor.";
//DAC - 01/03/2011 - Fin

//DAC - 03/03/2011 - Inicio
var mstrSeguroAceptar = "¿Está seguro de aceptar?";
//DAC - 03/03/2011 - Fin

//DAC - 07/03/2011 - Inicio
var mstrSeguroRechazarComprobanteUno = "¿Está seguro de rechazar?";
var mstrMensajeRechazarComprobante = "Se confirmo el rechazo de comprobante exitosamente.";
var mstrNoRechazoComprobante = "No se pudo realizar la acción, consultar con el administrador.";

//@0030 - DAC - Inicio
//var mstrSeguroExcepcionComprobanteUno = "¿Está seguro de exceptuar?";
var mstrSeguroExcepcionComprobanteUno = "¿Está seguro de hacer en consulta?";

//@0031 - DAC - Inicio
//var mstrMensajeExceptuoComprobante = "Se confirmo la excepción de comprobante exitosamente.";
var mstrMensajeExceptuoComprobante = "El comprobante ha sido colocado En Consulta exitosamente.";
//var mstrMensajeExceptuoComprobante = "Se realizó la consulta del comprobante exitosamente.";
//@0031 - DAC - Fin
//@0030 - DAC - Fin

var mstrNoExceptuoComprobante = "No se pudo realizar la acción, consultar con el administrador.";
//DAC - 07/03/2011 - Fin

//DAC - 08/03/2011 - Inicio
var mstrNoExcelConfirmacionComprobante = "Problemas al generar el excel, consulte con el administrador.";
var mstrSeguroExportarComprobante = "¿Está seguro de exportar?";
var mstrNoConfirmarComprobante = "No se pudo realizar la acción, consultar con el administrador.";
var mstrGrabarConfirmarComprobante = "Se confirmó el comprobante exitosamente.";
//DAC - 08/03/2011 - Fin

//RNP - 10/03/2011 - Inicio
var mstrSeguroDesasignarUsrResponsable = "¿Está seguro de desasignar responsable de abono de los registros seleccionados?";
var mstrSeguroDesasignarUsrCajero = "¿Está seguro de desasignar cajero de los registros seleccionados?";
var mstrExitoDesasignarUsrResponsable = "Se desasigno exitosamente el/los responsable de abono.";
var mstrExitoDesasignarUsrCajero = "Se desasigno exitosamente el/los cajero.";
//RNP - 10/03/2011 - Fin

//DAC - 14/03/2011 - Inicio
var mstrErrorNotaPedidoAlerta13 = "No se puede aprobar pues la nota de pedido, no cuenta con un abono ingresado.";
var mstrErrorNotaPedidoAlerta23 = "No se puedo realizar la acción. No se pueden cancelar pedidos que tengan comprobantes con estado EXCEPCION.";
var mstrErrorNotaPedidoAlerta24 = "Debe tener el medio de pago asignado.";
var mstrErrorNotaPedidoAlerta25 = "Debe de ingresar el número de operación del banco.";
var mstrErrorNotaPedidoAlerta26 = "El número de operación es duplicado a nivel de banco, moneda y fecha.";
var mstrErrorNotaPedidoAlerta27 = "No existe tipo de cambio con fecha del comprobante.";
//DAC - 14/03/2011 - Fin

//DAC - 21/03/2011 - Inicio
var mstrErrorNotaPedidoMantenimiento24 = "Debe tener medio de pago asignado.";
var mstrErrorNotaPedidoMantenimiento25 = "Debe ingresar el número de operación del banco.";
var mstrErrorNotaPedidoMantenimiento26 = "El número de operación es duplicado a nivel de banco, moneda y fecha.";
var mstrErrorNotaPedidoMantenimiento27 = "No existe tipo de cambio con fecha del comprobante.";
//DAC - 21/03/2011 - Fin

//DAC - 23/03/2011 - Inicio
var mstrErrorTipoCambioNoExiste = "No existe el tipo de cambio, para\nla fecha y moneda asignado.";
//DAC - 23/03/2011 - Fin

//DAC - 28/03/2011 - Inicio
var mstrNoExisteInteresCG = "No existe el interés para el financiamiento con la fecha aplicación ingresada.";
var mstrNoExisteInteresCG10 = "La fecha aplicación tiene que ser mayor o igual a la fecha de financiamiento.";
//DAC - 28/03/2011 - Fin

//@001 - DAC - Inicio
var mstrErrorGrabarTesoreria18 = "No se puede realizar la acción.\nSe deben ingresar las fechas del financiamiento y\nconfirmar sus datos.";
//@001 - DAC - Fin

//@002 - DAC - Inicio
var mstrMensajeEnviarAprobacion = "Se aprobó existosamente.";
var mstrNoEnvioAprobacion = "No se pudo realizar la acción, consultar con el administrador.";
var mstrMensajeEnviarRechazar = "Se rechazó existosamente.";
var mstrNoEnvioRechazar = "No se pudo realizar la acción, consultar con el administrador.";

var mstrErrorGuia122 = "No se podrá actualizar.\nEl estado de la guía es diferente de PENDIENTE.";
var mstrErrorGuia129 = "No se puede generar la guía,\nel VIN cuenta con una guía POR APROBAR.\nConsulte al supervisor de warrant.";
var mstrErrorGuia130 = "El destino de la unidad no es almacén de campo.\nLa generación del despacho requerirá aprobación del\nsupervisor de warrants.\n¿Desea continuar?";
var mstrErrorGuia131 = "Unidad bloqueada por warrant.\nSe debe esperar la constitución o exoneración de la unidad.";
var mstrErrorGuia132 = "El destino de la unidad no es almacén de campo.\nLa generación de la guía requerirá aprobación del\nsupervisor de warrants.\n¿Desea continuar?";
//@002 - DAC - Fin

//@0030 - DAC - Inicio
var mstrErrorGuia133 = "El VIN no está cancelado, no puede emitir\nla guía de entrega final.";

var mstrErrorActivarNotaVenta8 = "No se pudo realizar la acción.\nError en AUTOPRO.";
var mstrErrorActivarNotaVenta20 = "No se pudo realizar la acción.\nTodos los abonos de la nota pedido deben tener comprobantes\nconfirmados.";

//@0044 - DAC - Inicio 
//   //@0031 - DAC - Inicio
//   //var mstrErrorActivarNotaVenta21 = "No se pudo realizar la acción.\nPrimero debe ingresar y grabar el gestor de placas y la\nfecha de asignación del gestor.";
//   var mstrErrorActivarNotaVenta21 = "No se pudo realizar la acción.\nPrimero debe grabar el gestor de placas y la\nfecha de asignación.";
//   //@0031 - DAC - Fin
var mstrErrorActivarNotaVenta21 = "No se pudo realizar la acción.\nDebe registrar un Tipo de Gestor de Placas válido.";

var mstrErrorNotaPedidoAlerta32 = "No se pudo realizar la acción.\nDebe registrar un Tipo de Gestor de Placas válido.";
//@0044 - DAC - Fin

//@0045 - DAC - Inicio 
var mstrErrorNotaPedidoAlerta33 = "No se pudo realizar la acción.\nDebe registrar una Fecha de Gestor de Placas válida.";
//@0045 - DAC - Fin

//@0030 - DAC - Fin

//@003 - DAC - Inicio
var mstrErrorRecepcionDUACourier1 = "Debe seleccionar un VIN para agregar el Nro. de Guía Courier.";
var mstrErrorAsignarGestorPlaca6 = "No se puede realizar la acción.\nYa se existe una configuración igual ingresada.";
var mstrErrorAsignarGestorPlaca7 = "No se puede realizar la acción.\nYa se existe una configuración igual ingresada.";
var mstrErrorValidaNumByUsuario10 = "El VIN seleccionado no tiene registrado el número de placa.\nNo puede realizar esta acción.";
//@003 - DAC - Fin

//@004 - DAC - Inicio
var mstrMensajeMovEntFin1 = "Se anularon los movimientos exitosamente.";
var mstrErrorMovEntFin5 = "No se puede realizar la acción.\nSolo se pueden anular movimientos en Estado Registrado o Confirmado.";
var mstrErrorMovEntFin6 = "No puede anular el movimiento, se\nencuentra asignado a una Nota de Pedido.";
//@004 - DAC - Fin

//@005 - DAC - Inicio
var mstrSeguroAnularSolicitudObsequioUno = "¿Está seguro de anular?";
var mstrSeguroEnviarSolicitudObsequioUno = "¿Está seguro de enviar?";
var mstrEliminoSolicitudObsequio = "Se eliminó la solicitud de obsequios exitosamente.";
var mstrAnularSolicitudObsequio = "Se anuló la solicitud de obsequios exitosamente.";
var mstrMensajeNoEliminarServicioSOAprobar = "No se puede eliminar el servicio.\nLa solicitud de obsequio no es del estado Registrada u Observada.";
var mstrMensajeNoAgregarServicioSOAprobar = "No se puede agregar servicios.\nLa solicitud de obsequio no es del estado Registrada u Observada.";
var mstrEliminoServicio = "Se eliminó el servicio exitosamente.";
var mstrMensajeNoEnviarSOAprobar = "No se puede enviar la solicitud obsequio.\nSolo se puede enviar la solicitud obsequio en estado\nRegistrada u Observada.";
var mstrErrorNoEliminarSolicitudObsequio = "No se puede eliminar la solicitud de obsequio,\nsu estado debe ser: Registrado."; //"No se puede eliminar la solicitud de obsequio.";

//@007 - DAC - Inicio
//var mstrErrorGrabarSolicitudObsequio11 = "No se puede realizar la acción.\nEl estado comercial del vin, no\npermite registrar la solicitud obsequio.";
var mstrErrorGrabarSolicitudObsequio11 = "No se puede realizar la acción.\nEl estado comercial del vin, no permite registrar la\nsolicitud obsequio.";
//@007 - DAC - Fin

var mstrErrorGrabarSolicitudObsequio12 = "No se puede realizar la acción.\nLa marca no tiene asignado niveles de aprobación.";
var mstrErrorGrabarSolicitudObsequio13 = "No se puede realizar la acción.\nEl usuario no puede registrar esta marca de vehículo.";
var mstrErrorGrabarSolicitudObsequio14 = "No se puede realizar la acción.\nEl Vin está asignado a otro canal de venta.";
var mstrErrorGrabarSolicitudObsequio10 = "No se puede realizar la acción.\nSolo se puede modificar la solicitud obsequio en estado\nRegistrada u Observada.";
var mstrEnviarSolicitudObsequio = "Se envió la solicitud de obsequios exitosamente.";
var mstrNoCoincidenciaNroPlaca = "No se encontraron coincidencias con el nro. de placa.";
var mstrGrabarAsignarServicio = "Se asignó el servicio a la solicitud obsequio correctamente.";
var mstrErrorGrabarAsignarServicio = "No se pudo realizar la acción.\nConsultar con el administrador.";
var mstrErrorEliminarServicio16 = "No se puede eliminar el servicio,\nla solicitud obsequio está en estado Enviado.";
var mstrErrorVariosServicios = "- Debe seleccionar un servicio.";

//@0041 - DAC - Inicio
//var mstrErrorEnviarSolicitudObsequio17 = "No se puede enviar la solicitud.\nLa solicitud obsequio debe estar en estado nRegistrada u Observada.";
var mstrErrorEnviarSolicitudObsequio17 = "No se puede enviar la solicitud.\nLa solicitud obsequio debe estar en estado Registrada u Observada.";
//@0041 - DAC - Fin

var mstrErrorEnviarSolicitudObsequio18 = "No se puede enviar la solicitud.\nLa solicitud obsequio no tiene servicios ingresados.";
var mstrErrorEnviarSolicitudObsequio19 = "No se puede enviar la solicitud.\nEl Vin ingresado no es válido.";
var mstrErrorEnviarSolicitudObsequio20 = "No se puede enviar la solicitud.\nEl cliente ingresado no es válido.";
var mstrErrorEnviarSolicitudObsequio21 = "No se puede enviar la solicitud.\nNo existe configurado el nivel de aprobación para el monto asociado a la solicitud.";
var mstrEliminoNivelAprobacionObsequio = "Se eliminó el nivel de aprobación obsequio exitosamente.";
var mstrErrorNoEliminarNivelAprobacionObsequio21 = "No se puede eliminar el registro.\nExisten solicitudes obsequio en estado Enviado.";
var mstrErrorGrabarNivelAprobacionObsequio10 = "No se puede realizar la acción.\nEl nivel de aprobación ya existe.";
var mstrErrorGrabarNivelAprobacionObsequio11 = "No se puede realizar la acción.\nDebe haber por lo menos un nivel de aprobación.";
var mstrErrorGrabarNivelAprobacionObsequio12 = "No se puede realizar la acción.\nEl usuario aprobador solo puede estar en un nivel.";
var mstrErrorGrabarNivelAprobacionObsequio13 = "No se puede realizar la acción.\nEl importe a aprobar no puede ser cero.";
var mstrErrorGrabarNivelAprobacionObsequio14 = "No se puede realizar la acción.\nEl monto a aprobar no puede ser menor al monto del nivel anterior.";
var mstrAprobarSolicitudObsequio = "Se aprobó la solicitud obsequio exitosamente.";
var mstrErrorAprobarSolicitudObsequio30 = "No se puede aprobar la solicitud.\nLa solicitud obsequio debe estar en estado Enviado.";
var mstrErrorAprobarSolicitudObsequio31 = "No se puede aprobar la solicitud.\nEl usuario no está autorizado para aprobar esta solicitud.";
var mstrRechazarSolicitudObsequio = "Se rechazó la solicitud obsequio exitosamente.";
var mstrErrorAprobarSolicitudObsequio35 = "No se puede rechazar la solicitud.\nLa solicitud obsequio debe estar en estado Enviado.";
var mstrErrorAprobarSolicitudObsequio36 = "No se puede rechazar la solicitud.\nEl usuario no está autorizado para rechazar esta solicitud.";
var mstrObservarSolicitudObsequio = "Se observó la solicitud obsequio exitosamente.";
var mstrErrorAprobarSolicitudObsequio40 = "No se puede observar la solicitud.\nLa solicitud obsequio debe estar en estado Enviado.";
var mstrErrorAprobarSolicitudObsequio41 = "No se puede observar la solicitud.\nEl usuario no está autorizado para observar esta solicitud.";
var mstrSeguroAprobarSolicitudObsequio = "¿Está seguro de aprobar?";
var mstrSeguroRechazarSolicitudObsequio = "¿Está seguro de rechazar?";
var mstrSeguroObservarSolicitudObsequio = "¿Está seguro de observar?";
//@005 - DAC - Fin

//@006 - DAC - Inicio
var mstrErrorNPAbonoAsesor13 = "No se puede registrar la operación.\nAbono duplicado.";
var mstrErrorCopiarNivelAprobacion10 = "No se puede registrar la operación.\nRegistro duplicado.";
var mstrVerServicioNroVin = "Debe ingresar el número de vin.";
var mstrSaltoLinea = "\n";
//@006 - DAC - Fin

//@007 - DAC - Inicio
var mstrErrorAnularSolicitudObsequio25 = "No se pudo realizar la acción.\nSolo se pueden anular solicitudes obsequio en estado Vigente.";
//@007 - DAC - Fin

//@009 - DAC - Inicio
var mstrLineaComercialAsignada = "La línea comercial ya fue asignada.";
//@009 - DAC - Fin

//Agregado por NCamacho 13/06/2011
var mstrActivo = "Se activo exitosamente.";
var mstrSeguroActivo = "¿Está seguro de activar?";
var mstrSeguroGrabarKilometraje = "El nuevo kilometraje es menor al actual.\n¿Está seguro de grabar?";
//Fin del agregado por Ncamacho 13/06/2011

//@0031 - DAC - Inicio
var mstrErrorVinEnviadoPDI6 = "El modelo relacionado al VIN no debe ser ingresado a PDI.\nNo se puede realizar esta acción.";
var mstrErrorVinEnviadoPDI9 = "El Vin seleccionado no se encuentra dentro del almacén central ó satelite.\nNo se puede realizar esta acción.";

var mstrErrorVinEnviadoPDI8 = "No se le puede solicitar PDI Sucursal al Vin.\nNo se puede realizar esta acción.";
var mstrErrorVinEnviadoPDI5 = "El VIN seleccionado debe encontrarse en Sucursal o Concesionario.";
var mstrErrorVinEnviadoPDISucursal6 = "El modelo relacionado al VIN no debe ser ingresado a PDI en Sucursal o Concesionario sino en Almacen Central.\nNo se puede realizar esta acción.";
//@0031 - DAC - Fin

//@0035 - DAC - Inicio
////@0033 - DAC - Inicio
var mstrErrorNPAbonoAsesor15 = "No se puede realizar la acción.\nEl comprobante está asignado a otros abonos.\nRealizar la acción por la bandeja de comprobante.";
var mstrErrorNPAbonoAsesor16 = "No se puede realizar la acción.\nEl comprobante es duplicado.";
//var mstrErrorNPAbonoAsesor15 = "No se puede registrar el abono.\nEl comprobante está asignado a otros abonos.";
//var mstrErrorNPAbonoAsesor16 = "No se puede registrar el abono.\nEl comprobante es duplicado.";
////@0033 - DAC - Fin
//@0035 - DAC - Inicio

//@0034 - DAC - Inicio
var mstrConsultaGrabarEnvioAbono = "¿Desea enviar el comprobante para confirmación?";
//@0034 - DAC - Fin

//@0036 NCP Inicio
var mstrErrorAdmComprobante11 = "No puede realizar la acción.\nLa Suma de abonos es mayor al importe del comprobante.";
var mstrErrorNPAbonoAsesor17 = "No se puede realizar la acción.\nEl importe base no puede ser menor al importe utilizado del comprobante.";
//@0036 NCP Fin

//@0037 - DAC - Inicio
var mstrErrorNPAsignaDoc8 = "El importe del abono es diferente al importe del anticipo AUTOPRO.";
var mstrErrorNPAsignaDoc7 = "La moneda del abono es diferente a la moneda del anticipo AUTOPRO.";
var mstrErrorNPAsignaDoc6 = "El comprobante seleccionado es diferente al grupo de comprobantes existentes para la nota de pedido.";
var mstrErrorNPAsignaDoc4 = "El identificador de la empresa no es correcto.\nConsulte con el administrador.";
//@0037 - DAC - Fin

//@0038 - DAC - Inicio
var mstrErrorAdmComprobanteEnviarC16 = "No se puede realizar la acción. Número de movimiento duplicado.";
//@0038 - DAC - Fin

//@0040 - DAC - Inicio
var mstrErrorAtributo5 = "No se pudo realizar la acción.\nEl código de aduanas ya existe.";
var mstrErrorAtributo6 = "No se pudo realizar la acción.\nEl atributo se encuentra asignado a una Ficha Técnica.";
var mstrErrorEliminarMasivoSpecCodeAtributos5 = "Uno ó más de los atributos seleccionados, no puede ser eliminado por ser del tipo aduanero.";
var mstrErrorGrabarAtributoCalculado6 = "No se pudo realizar la acción.\nNo se puede actualizar atributos de tipo aduanero.";
var mstrErrorGrabarAtributoCalculado5 = "No se pudo realizar la acción.\nNo se puede actualizar atributos calculados.";
var mstrErrorImportarAtributo6 = "No se pudo realizar la acción.\nNo se puede actualizar atributos de tipo aduanero.";
var mstrErrorImportarAtributo5 = "No se pudo realizar la acción.\nNo se puede actualizar atributos calculados.";
//@0040 - DAC - Fin

// I-@0001
var mstrErrorFacturaFOB = "El importe total FOB no coincide con el monto comprometido del pedido.";
// F-@0001

//@0041 - DAC - Inicio
var mstrErrorEnviarSolicitudObsequio22 = "No se puede enviar la solicitud.\nDebe asignar un presupuesto vigente.";
var mstrErrorEnviarSolicitudObsequio23 = "No se puede enviar la solicitud.\Presupuesto asignado a otra solicitud.";

var mstrErrorGrabarSolicitudObsequio15 = "No se puede realizar la acción.\nLa marca del VIN no tiene asignado una ubicación de atención.";
var mstrErrorGrabarSolicitudObsequio16 = "No se puede realizar la acción.\nEl área a la cual se cargó el presupuesto es diferente a la del obsequio.";

var mstrErrorImprimirCuponServicio10 = "No se puede imprimir el cupón.\nSolo puede imprimir cupones vigentes y aprobados.";
//@0041 - DAC - Fin

//@0042 - DAC - Inicio
var mstrErrorGrabarAtributoOrden10 = "No se pudo realizar la acción.\nEl nro. de orden ingresado ya existe en el mismo grupo impresión.";
//@0042 - DAC - Fin

//@0052 - DAC - Inicio
var mstrErrorGrabarAtributoOrdenComercial10 = "No se pudo realizar la acción.\nEl nro. de orden ingresado se encuentra repetido dentro de un mismo grupo de atributo.";
//@0052 - DAC - Fin

//@0051 - DAC - Inicio
//   //@0046 - DAC - Inicio
//   var mstrEnviarAprobacionFactura = "Se envió a aprobación exitosamente.\n\nSe enviará un email con el monto de la diferencia FOB.";
var mstrEnviarAprobacionFactura = "Se envío correo para aprobación de las diferencias encontradas.";
//   //@0046 - DAC - Fin
//@0051 - DAC - Fin

//@0047 NCP Inicio
var mstrArchivoAdjuntoNoPermitido = "El archivo {0} excede el tamaño permitido.";
var mstrArchivoAdjuntoMinimoPermitido = "El tamaño del archivo {0} debe ser mayor a cero.";
var mstrSobregiroDebeRegistrado = "El estado del sobregiro debe ser REGISTRADO";
//@0047 NCP Fin

//@0048 RAL Inicio
var mstrErrorLineaFinSinCreditoDisponible = "La línea de financiamiento no tiene línea de crédito disponible.";
var mstrErrorLineaFinConFacturasVdas = "La línea de financiamiento tiene facturas vencidas después de la fecha de vencimiento de desbloqueo.";
//@0048 RAL Fin

//@0049 - DAC - Inicio
//@0066 NCP Inicio
var mstrSeguroAprobarLinea = "¿Está seguro de aprobar la línea de financiamiento?.";
var mstrSeguroAprobarExcepcionLinea = "¿Está seguro de aprobar con excepción la Línea de financiamiento?.";
//@0066 NCP Fin
//@0067 NCP Inicio
//var mstrSeguroRechazarLinea = "¿Desea rechazar la Línea?";
//var mstrSeguroSuspenderLinea = "¿Desea suspender la Línea?";
var mstrSeguroRechazarLinea = "¿Está seguro de rechazar la línea de financiamiento?.";
var mstrSeguroSuspenderLinea = "¿Está seguro de suspender la línea de financiamiento?.";
//@0067 NCP Fin
var mstrSeguroModificarLinea = "¿Desea modificar la Línea?";

var mstrGraboModificarLineaFin = "La línea de financiamiento ha sido Modificada.";
//@0058 - DAC - Inicio
//   //@0053 - DAC - Inicio
//   //var mstrErrorGraboModificarLineaFin12 = "La fecha fin de la línea de financiamiento debe ser,\n menor a la fecha fin que se encontraba registrada.";
//   var mstrErrorGraboModificarLineaFin12 = "La fecha de fin de línea no puede ser menor a la fecha actual.";
//   //@0053 - DAC - Fin
var mstrErrorGraboModificarLineaFin12 = "La fecha de fin de línea no puede ser menor a la fecha inicio ni igual a la fecha actual.";
//@0058 - DAC - Fin
var mstrGraboAprobarExcepcionLineaFin = "La línea de financiamiento ha sido Aprobada con Excepción.";
var mstrGraboRechazarLineaFin = "La línea de financiamiento ha sido Rechazada.";
var mstrGraboSuspenderLineaFin = "La línea de financiamiento ha sido Suspendida.";
var mstrGrabarAprobarCreditoLineaFin = "La línea de financiamiento de tipo Crédito para Concesionario, ha sido Aprobada.";
var mstrGrabarAprobarConsignacionLineaFin = "La línea de financiamiento de tipo Consignación para Concesionario, ha sido Aprobada.";
var mstrErrorAprobarCreditoLineaFin5 = "El estado de la línea de financiamiento debe estar en registrado.";
var mstrErrorAprobarCreditoLineaFin6 = "Debe existir al menos una garantía ingresada.";
var mstrErrorAprobarCreditoLineaFin7 = "La fecha de fin de la línea debe ser la menor fecha fin de las garantías registradas.";
var mstrErrorAprobarCreditoLineaFin8 = "El monto de la línea de financiamiento debe ser igual a la sumatoria del valor de la garantías.";
var mstrErrorAprobarCreditoLineaFin9 = "Todos los requisitos deben de estar aprobados.";
var mstrErrorAprobarCreditoLineaFin11 = "Error en el registro de la bitácora de la línea.";
var mstrGraboEliminarLineaFin = "La línea de financiamiento ha sido Eliminada.";
//@0054 - DAC - Inicio
//var mstrErrorGraboLineaFin5 = "No se puede grabar el registro. Esta línea ya se encuentra registrada y vigente.";
var mstrErrorGraboLineaFin5 = "No se puede grabar el registro. Esta línea existe y se encuentra vigente.";
//@0054 - DAC - Fin
//@0049 - DAC - Fin

//@0050 RAL Inicio
var mstrErrorTraspasoLineaNoActiva = "No se puede ejecutar el traspaso. La linea no se encuentra activa.";
var mstrErrorTraspasoLineaNoAprobada = "No se puede ejecutar el traspaso. La linea debe estar aprobada.";
var mstrErrorTraspasoLineaNoSobregirada = "No se puede ejecutar el traspaso. La linea no puede ser sobregirada.";

var mstrErrorDevolucionNPFacturada = "No se puede ejecutar la devolución. La nota de pedido se encuentra ya facturada.";
var mstrErrorDevolucionNoActiva = "No se puede ejecutar la devolución. La Linea de financiamiento no se encuentra activa.";
var mstrErrorDevolucionNoAprobada = "No se puede ejecutar la devolución. La Linea de financiamiento no se encuentra aprobada.";
var mstrErrorDevolucionNPAnulada = "No se puede ejecutar la devolución. La nota de pedido se encuentra ya anulada.";
//@0050 RAL Fin

//@0051 - DAC - Inicio
var mstrErrorEnviarAprobacionFactura10 = "No existe un Gerente de Marca asignado para aprobación.";
var mstrSeguroDescargarExcel = "¿Está seguro de exportar?";
//@0051 - DAC - Fin

//@0055 - DAC - Inicio
var mstrErrorGraboModificarLineaFin13 = "La fecha fin de línea debe ser menor a la fecha fin de\nlínea que se registró anteriormente.";
//@0055 - DAC - Fin

//@0056 NCP Inicio
var mstrErrorGraboParametrosAmicar = "Se grabaron los datos satisfactoriamente.";
var mstrErrorNoGraboParametrosAmicar = "No se puede grabar la información registrada.";
var mstrErrorAsociacionMarcaProducto5 = "- Ya existe un registro para la asignación marca producto.";
//@0056 NCP Fin

//@0057 - DAC - Inicio
var mstrErrorGraboLineaFin6 = "Error en el registro de la bitácora de la línea.";
//@0057 - DAC - Fin

//@0059 - DAC - Inicio
var mstrErrorGraboFichaTecnicaNV = "El Spec Code se encuentra vigente, no se puede modificar los atributos.\nPara modificar cualquier atributo aduanero deberá modificar el estado del Spec Code a No Vigente.";
//@0059 - DAC - Fin

//@0060 NCP Inicio
var mstrErrorNivelAprobacionExiste = "El nivel de aprobación ya existe.";
var mstrErrorDebeExistirMinUnNivel = "Debe haber por lo menos un nivel de aprobación.";
var mstrErrorAprobadorSoloNivel = "El usuario aprobador solo puede estar en un nivel.";
var mstrErrorImporteMayorCero = "El importe a aprobar no puede ser cero.";
var mstrErrorMontoMayorAnterior = "El monto a aprobar no puede ser menor al monto del nivel anterior.";

var mstrErrorCampaniaNoVigente = "La campaña es no vigente.";
var mstrErrorCampaniaAnulada = "La campaña esta anulada.";
//@0068 - DAC - Inicio
//var mstrErrorSuperaFechaVigencia = "la fecha de hoy supera la fecha de fin de vigencia.";
var mstrErrorSuperaFechaVigencia = "La fecha de hoy supera la fecha de fin de vigencia.";
//@0068 - DAC - Fin
//@0060 NCP Fin

//@0061 NCP Inicio
var mstrErrorTraspasoNoTieneDisponible = "La línea de crédito no tiene el disponible suficiente.";
//@0061 NCP Fin

//@0062 NCP Inicio
var mstrErrorNoHayStockDisponible = "No hay stock disponible, debe RECHAZAR el sobregiro.";
//@0062 NCP Fin

//@0063 NCP Inicio
var mstrErrorTraspasoLineaSobregirada = "La línea de consignación se encuentra sobregirada.";
//@0063 NCP Fin

//@0064 NCP Inicio
var mstrErrorAbonoLineaSobregirada = "No puede realizar la acción.\nLa línea de Financimiento se encuentra SOBREGIRADA.";
var mstrErrorLineaDisponibleMenorAbono = "No puede realizar acción.\nLa línea Disponible es menor que el Abono.";
//@0064 NCP Fin

//@0065 - DAC - Inicio
var mstrSeguroObsLevantada = "¿Está seguro de levantar la observación?";
var mstrMensajeLevantoObs = "La observación fue levantada correctamente.";
var mstrErrorObsSubsanada5 = "No se puede realizar la acción.\nLa observación ya fue levantada anteriormente.";
var mstrErrorObsSubsanada6 = "No se puede realizar la acción.\nLa observación no tiene registrada la respuesta del concesionario.";
var mstrErrorConRespuestaCliente5 = "No se puede realizar la acción.\nLa observación se encuentra con respuesta del cliente.";
//@0065 - DAC - Fin

//@0067 NCP Inicio
var mstrErrorLineaCreditoNoDisponible = "No se encuentra la línea de crédito disponible.";
//@0067 NCP Fin

//@0069 I
var mstrErrorGrabarUbigeo = "No se puede realizar la acción,\nla ubicación de Departamento - Provincia - Distrito no existe.";
var mstrErrorGrabarDireccionFacAsociadas = "No se puede realizar la acción,\nExisten facturas asociadas a la dirección.";
var mstrErrorClienteDocumento = "No existe un cliente con el nro documento ingresado.";
var mstrErrorContactoDocumento = "No existe un contacto con el nro documento ingresado.";
var mstrErrorPropietarioDocumento = "No existe un propietario con el nro documento ingresado.";
var mstrErrorClienteMuchosDocumento = "Existe mas de un cliente con el nro documento ingresado.";
var mstrErrorClienteExiste = "No se puede realizar la acción,\nCliente ya se encuentra registrado.";
var mstrErrorContactoExiste = "No se puede realizar la acción,\nContacto ya se encuentra registrado.";
var mstrErrorPropietarioExiste = "No se puede realizar la acción,\nPropietario ya se encuentra registrado.";
var mstrErrorClienteMuchosDocumentoDireccion = "Existe más de una dirección con el número de documento ingresado.";
//@0069 F

//@0070 I
var mstrTLCHaciaMismoPais = "No se puede realizar la acción,\nno puede haber un TLC con el mismo país.";
var mstrTLCPaisRepetido = "No se puede realizar la acción,\nya existe un TLC con el país seleccionado seleccionado.";
var mstrTLCPrefSinCambio = "No se ha modificado ninguna preferencia."
var mstrPDIMarcaPremiumFechaTermino = "Fecha de término PDI premium debe ser menor o igual a la fecha actual";
var mstrPDIMarcaPremiumSucursal = "La ubicacion actual del VIN debe ser del Tipo SUCURSAL"
//@0070 F