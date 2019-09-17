using System;

namespace AppMiTaller.Intranet.BE
{
    public class Message
    {
        public static String keyActivo = "mstrActivo";
        public static String keyElimino = "mstrElimino";
        public static String keyNoElimino = "mstrNoElimino";
        public static String keyNoEliminoRelacionado = "mstrNoEliminoRelacionado";
        public static String keyNoRegistros = "mstrNoRegistros";
        public static String keySeleccioneUno = "mstrSeleccioneUno";
        public static String keySeguroGrabar = "mstrSeguroGrabar";

        public static String keySeguroEliminarUno = "mstrSeguroEliminarUno";
        public static String keyActualizar = "mstrActualizar";
        public static String keyErrorGrabar = "mstrErrorGrabar";
        public static String keyErrorGrabarCodDuplicado = "mstrErrorGrabarCodDuplicado";
        public static String keyErrorGrabarCodExiste = "mstrErrorGrabarCodExiste";
        public static String keyErrorGrabarNulo = "mstrErrorGrabarNulo";
        public static String keyErrorGrabarLlave = "mstrErrorGrabarLLave";
        public static String keyGrabar = "mstrGrabar";
        public static String KeyExiste = "mstrExiste";
        public static String KeyRUCExiste = "mstrRUCExiste";  //validacion solo para a nivel de pagina
        public static String KeyDNIExiste = "mstrDNIExiste";  //validacion solo para a nivel de pagina
        public static String KeyPartidaExiste = "mstrPartidaExiste";  //validacion solo para a nivel de pagina
        public static String KeyImprimirOK = "mstrImprimirOK";
        public static String KeySeguroImprimir = "mstrSeguroImprimir";
        public static String KeyDocExiste = "mstrDocYaExiste";
        public static String keyErrorCambiaNegocio = "mstrErrorCambiaNegocio";

        public static String keyFichaDuplicado = "mstrFichaDuplicado";
        public static String KeyAgenteAduana = "mstrAgenteExiste";
        public static String KeyNombreRepetido = "mstrNombreRepetido";
        public static String KeyZonaRepetida = "mstrZonaRepetida";
        public static String KeyMasdeunaCoincidencia = "mstrMasdeunaCoincidencia";
        public static String KeyFechaExpiro = "mstrFechaBrevExipo";
        public static String KeyAgenteAduanaYa = "mstrAgenteAduanaYaExiste";
        public static String KeyNombreComercExist = "mstrNombreComercExist";
        public static String keyErrorGrabarCodDupModelo = "mstrErrorGrabarCodDupModelo";
        public static String keyLineasImpAsignadas = "mstrLineasImpAsignadas";
        public static String keyErrorNegLinAsocModel = "mstrErrorNegLinAsocModel";
        public static String keyErrorInactivarIncoterm = "mstrErrorInactivarIncoterm";



        //Seguridad
        public static String keyUsuarioNoReg = "mstrUsuarioNoReg";
        public static String keyUsuarioRepetido = "mstrUsuarioRepetido";
        public static String keyPefilEnUso = "mstrPefilEnUso";
        public static String keyLoginRepetido = "mstrLoginRepetido";
        public static String keyDNIRepetido = "mstrDNIRepetido";
        public static String keyAccesoPaginaDenegado = "mstrAccesoPaginaDenegado";
        public static String keyErrorPerfilCotizacion = "mstrErrorPerfilCotizacion";
        public static String keyErrorPerfilDesaduanajeAGP = "mstrErrorPerfilDesaduanajeAGP";
        public static String KeyErrorVinesExhibicion = "mstrErrorVinesExhibicion";
        public static String KeyErrorEstadoExhibicion = "mstrErrorEstadoExhibicion";
        public static String mstrErrorVinesExhibicion = "'No se puede cerrar pues los siguientes vines tienen guias emitidas: {0} con guía {1}'";
        public static String mstrErrorEstadoExhibicion = "'La exhibición no se encuentra en el estado correcto. La exhibición debe estar Aprobada o Aprobada Parcial.'";
        public static String mstrErrorEstadoIncorrecto = "'No se encuentra en el estado correcto'";

        //Perfil
        public static String keyUsuariosNoVigentes = "mstrUsuariosNoVigentes";
        public static String keyUsuariosYaMultimarca = "mstrUsuariosYaMultimarca";

        //Maxima Longitud de Arcvhivos
        public static String keyEncima4Megas = "mstrEncima4Megas";
        public static String keyEncimaMaxMarcEmpImp = "mstrEncimaMaxMarcEmpImp";

        public static String keyArchivoNoPuedeSerAdj = "mstrArchivoNoPuedeSerAdj";
        public static String keyArchivoExcedioDim = "mstrArchivoExcedioDim";

        // Expresiones Regulares
        public static String keyArchivoPuedeTenerExpReg = "'Nomb. de arch. con caracteres no permitidos, modificar con letras y/o números.'"; // @043 I/F


        //Insertar
        public static String keyErrorRegistrarDatos = "mstrErrorRegistrarDatos";
        public static String keyErrorGrabarImporte = "mstrErrorGrabarImporte";
        public static String keyErrorGrabarEstado = "mstrErrorGrabarEstado";
        //Modificar
        public static String keyErrorModificarEstado = "mstrErrorModificarEstado";
        public static String keyErrorModificarImporte = "mstrErrorModificarImporte";
        public static String keyErrorModificarDetalle = "mstrErrorModificarDetalle";
        public static String keyNoRealizarAccion = "mstrNoSePuedeRealizarAccion";
        public static String keyNotifico = "mstrNotifico";

        /*MODELO*/
        public static String KeyModModelo5 = "mstrModModelo5";
        public static String KeyModModelo7 = "mstrModModelo7";
        public static String KeyModeloExistSGSNET = "mstrModeloExistSGSNET";

        public static String KeyErrorClienteDocumento = "mstrErrorClienteDocumento";
        public static String KeyErrorContactoDocumento = "mstrErrorContactoDocumento";
        public static String KeyErrorPropietarioDocumento = "mstrErrorPropietarioDocumento";
        public static String KeyErrorClienteExiste = "mstrErrorClienteExiste";
        public static String KeyErrorContactoExiste = "mstrErrorContactoExiste";
        public static String KeyErrorPropietarioExiste = "mstrErrorPropietarioExiste";
        public static String KeyErrorGrabarUbigeo = "mstrErrorGrabarUbigeo";
        public static String KeyErrorGrabarDireccionFacAsociadas = "mstrErrorGrabarDireccionFacAsociadas";
        public static String KeyErrorClienteMuchosDocumento = "mstrErrorClienteMuchosDocumentoDireccion";
        public static String keyErrorGrabarText = "No se pudo realizar la acción, consultar con el administrador.";
        public static String keyErrorGrabarNuloText = "No se pudo realizar la acción,\nse ingres un valor nulo en un campo no permitido.";
        public static String keyErrorGrabarCodDuplicadoText = "No se pudo realizar la acción,\nno existe la llave foránea.";
        public static String keyArchivoAdjuntoDosMb = "'El tamaño máximo permitido para los archivos es de 2mb.'";
    }
}
