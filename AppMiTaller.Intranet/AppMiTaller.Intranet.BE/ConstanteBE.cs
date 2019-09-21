using System;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class ConstanteBE : BaseBE
    {
        public const string OBJECTO_TODOS = "--Todos--";
        public const string OBJECTO_SELECCIONE = "--Seleccione--";
        public const string OBJETO_PROVINCIA = "--Provincia--";
        public const string OBJETO_DEPARTAMENTO = "--Departamento--";
        public const string OBJETO_DISTRITO = "--Distrito--";
        public const string OBJECTO_SEL = "--Sel--";
        public const string OBJECTO_TIPO_TODOS = "A";
        public const string OBJECTO_TIPO_SELECCIONE = "B";
        public const string OBJECTO_TIPO_DEPA = "C";
        public const string OBJECTO_TIPO_PROV = "D";
        public const string OBJECTO_TIPO_DIST = "E";
        public const string OBJETO_SUCURSAL = "SU";
        public const string formato_fecha_es = "dd/MM/yyyy";
        public const String Formato_Fecha_en = "yyyyMMdd";
        public const string PATRON_ENCRIPTA = "ABCDEFGHIJKLMNÑOPQRSTUWVXYZabcdefghijklmnñopqrstuwvxyz1234567890_-\\/";
        public const string PATRON_BUSQUEDA = "ABCDEFGHIJKLMNÑOPQRSTUWVXYZabcdefghijklmnñopqrstuwvxyz1234567890_-\\/";
        public const string FL_ESTADO_ACTIVO = "0";
        public const string FL_ESTADO_INACTIVO = "1";
        public const string COD_MARCA_HYUNDAI = "12";

        public const string FL_DISPONIBLE_ACTIVO = "0";
        public const string FL_DISPONIBLE_INACTIVO = "1";

        public const string CO_TABLA_FORMATO_FECHAS = "161";
        public const string CO_TABLA_FORMATO_MONTO = "162";

        //public const string CO_TABLA_FORMATO_FECHAS = "156";
        //public const string CO_TABLA_FORMATO_MONTO = "157";

        public const string NO_SEPARADOR_COMA = "COMA";
        public const string SIM_SEPARADOR_COMA = ",";
        public const string CO_SEPARADOR_COMA = "1";
        public const string NO_SEPARADOR_PUNTO = "PUNTO";
        public const string SIM_SEPARADOR_PUNTO = ".";
        public const string CO_SEPARADOR_PUNTO = "2";

        /*Negocios*/
        public const string CO_NEGOCIO_MOTOS = "MOT";
        public const string CO_NEGOCIO_VEHICULOS = "VEH";

        /*CONSTANTES DE TABLAS GENERALES*/
        public const Int32 NID_TABLA_TIPO_USUARIO = 10;
        public const Int32 NID_TABLA_ROLES_USUARIO = 11;

        public const String SETEOCOMBO = "S";

        public const String FL_CONFIRMADO_MEDIOPAGOCOMERCIAL = "1";
        public const String FL_NO_CONFIRMADO_MEDIOPAGOCOMERCIAL = "0";
        public const String NOM_CONFIRMADO_MEDIOPAGOCOMERCIAL = "Si";
        public const String NOM_NO_CONFIRMADO_MEDIOPAGOCOMERCIAL = "No";

        /*ROLES TIPOS DE USUARIO*/
        public const Int32 NID_TIPO_ADMINISTRADOR = 22;
        public const Int32 NID_TIPO_NORMAL = 23;
        public const Int32 NID_TIPO_EXTERNO = 24;
        public const Int32 NID_TIPO_USUARIO_INTERNO = 1;
        public const Int32 NID_TIPO_USUARIO_EXTERNO = 2;

        public const string TIPO_AGREGAR = "Agregar";
        public const string TIPO_MODIFICAR = "Modificar";

        /*RUTA IMAGEN TEMPORAL*/
        public const string RUTA_IMAGEN = "Images\\imagetemp\\";
        public const string RUTA_IMAGEN_URL = "../Images/imagetemp/";
        public const string RUTA_IMAGEN_TEM = "Images\\";
        public const string CONST_NOMBRE_LOGO = "logo_gildemeister.gif";
        public const string CONST_EXCEL_FORMATO_CARGA_MASIVA = "Formato de Carga de Reservas.xls";
        public const string RUTA_QRCODE = "Imagenes\\QRCode\\";
        public const string RUTA_MARCAS = "Imagenes\\Marca\\";
        public const string RUTA_FILE_SERVER = "Documentos\\FileServer\\";
        public const string RUTA_MANTENIMIENTO_CLIENTE = "Documentos\\Mantenimiento\\Cliente\\";
        
        public const string ID_TABLA = "1";
        public const string ID_TABLA_DETALLE = "1";
        public const Int32 NID_TABLA_MESES = 19;
        public const Int32 NID_TABLA_ANIOS = 1;
        public const Int32 NID_TABLA_ANIOS_DET = 2;
        public const string NID_TABLA_SINO = "22";
        public const String ELIMINADO_X = "X";
        public const String ELIMINADO_SI = "Si";

        public const string NID_PAIS_PERU = "162";

        /*TIPOS DE ARCHIVOS*/
        public const Int32 ARCHIVOS_TAMANHO_MAXIMO = 100000;
        public const Int32 ARCHIVOS_IMG_TAMANHO_MAXIMO = 100000;
        public const Int32 ARCHIVOS_1024 = 1024;
        public const String ARCHIVOS_INFORMATICOS = ".*";//".doc .docx .xls .xlsx .pdf .vsd .txt .ppt";
        public const String ARCHIVOS_TODOS = ".*";
        public const String ARCHIVOS_IMAGENES = ".jpg .gif .png";
        public const String ARCHIVOS_DOC = ".doc";
        public const String ARCHIVOS_DOC_PDF = ".*";//".doc .pdf";
        public const String ARCHIVOS_XLS = ".xls";
        public const String ARCHIVOS_DOC_PDF_AND_IMAGENES = ".*";//".doc .pdf .jpg .gif .png";  
        public const String ARCHIVOS_TXT = ".txt";
        public const String ARCHIVOS_DOCX = ".docx";

        /*KEYS PARA LAS TABLAS DE ATRIBUTOS DE ARCHIVOS E IMAGENES*/
        public const Int32 ID_TABLA_CONF_IMG_MARCA_EMP_IMP = 38;
        public const Int32 ID_TABLA_CONF_IMG_SPEC_CODE = 39;

        public const String COD_TABLA_DET_MEDIDAS_ANCHO = "1";
        public const String COD_TABLA_DET_MEDIDAS_ALTO = "2";
        public const String COD_TABLA_DET_MEDIDAS_PESO = "3";

        /*INDICADOR CONTROL COLOR*/
        public const String IND_CONTROL_SIN_COLOR = "0";
        public const String IND_CONTROL_COLOR_VERDE = "1";
        public const String IND_CONTROL_COLOR_AMBAR = "2";
        public const String IND_CONTROL_COLOR_ROJO = "3";
        
        /*KEY PARA OBTENER EL CODIGO DE PAIS PERU*/
        public const String COD_TABLA_DET_PAIS_PERU = "25";

        //TAMAO DE PAGINA DE PARTIDA ARANCELARIA Y CATEGORIA AMBAS CON TEXTAREA
        public const Int32 TAM_GRILLA_TEXTAREA = 7;

        public const string ID_TABLA_TIPO_DOCUMENTO = "69";
        public const string ID_TABLA_TIPO_PERSONA_NOTA_PEDIDO = "54";
        public const string ID_TABLA_TIPO_DOCUMENTO_NOTA_PEDIDO = "40";
        public const string ID_TABLA_TIPO_PREFIJO = "136";
        public const string ID_TABLA_TIPO_ESTADO_CIVIL = "137";
        public const string ID_TABLA_TIPO_CATEGORIA_CLIENTE = "138";
        public const string ID_TABLA_TIPO_CLASE_CLIENTE = "139";

        public const String COD_TIPO_PERSONA_NATURAL = "0001";
        public const String COD_TIPO_PERSONA_JURIDICA = "0002";
        public const String FLAT_EXCEL_CLIENTE_BANDEJA = "6";
        public const String FLAT_EXCEL_MODELO = "28";
        public const String COD_TIPO_TABLA_DETALLE_RUC = "03";
        public const String COD_CLIENTE_MASCULINO = "0001";
        public const String COD_CLIENTE_FEMENINO = "0002";
        public const String COD_TIPO_DOCUMENTO_DNI = "01";
        public const String COD_TIPO_DOCUMENTO_PASAPORTE = "02";
        public const String COD_TIPO_DOCUMENTO_RUC = "03";
        public const String COD_TIPO_DOCUMENTO_CARNET_EXTRANJERIA = "04";

        public struct TipoArchivo
        {
            public const string Excel = "application/vnd.ms-excel";
            public const string PDF = "application/pdf";
        }
        
        public const String CODIGO_TIPO_PERSONA_PROPIETARIO = "01";
        public const String CODIGO_TIPO_PERSONA_CLIENTE = "02";
        public const String CODIGO_TIPO_PERSONA_CONTACTO = "03";
        public const String NOMBRE_TIPO_PERSONA_CLIENTE = "Cliente";
        public const String NOMBRE_TIPO_PERSONA_PROPIETARIO = "Propietario";
        public const String NOMBRE_TIPO_PERSONA_CONTACTO = "Contacto";

        public static string SRC_MantParametros_AccionVerForm = "13010101";
        public static string SRC_MantParametros_AccionEditar = "13010102";

        public static string SRC_MantTipoServicio_AccionVerForm = "13020101";
        public static string SRC_MantTipoServicio_AccionVer = "13020102";
        public static string SRC_MantTipoServicio_AccionNuevo = "13020103";
        public static string SRC_MantTipoServicio_AccionEditar = "13020104";

        public static string SRC_MantServicio_AccionVerForm = "13020201";
        public static string SRC_MantServicio_AccionVer = "13020202";
        public static string SRC_MantServicio_AccionNuevo = "13020203";
        public static string SRC_MantServicio_AccionEditar = "13020204";

        public static string SRC_MantModelo_AccionVerForm = "13020301";
        public static string SRC_MantModelo_AccionVer = "13020302";
        public static string SRC_MantModelo_AccionEditar = "13020303";

        public static string SRC_MantVehiculo_AccionVerForm = "13020401";
        public static string SRC_MantVehiculo_AccionVer = "13020402";
        public static string SRC_MantVehiculo_AccionNuevo = "13020403";
        public static string SRC_MantVehiculo_AccionEditar = "13020404";

        public static string SRC_MantCliente_AccionVerForm = "13020501";
        public static string SRC_MantCliente_AccionNuevo = "13020502";
        public static string SRC_MantCliente_AccionEditar = "13020503";
        public static string SRC_MantCliente_AccionEditarDireccion = "13020504";

        public static string SRC_ReservaCita_AccionVerForm = "130302";
        public static string SRC_ReservaCita_AccionVerFormulario = "13030201";
        public static string SRC_ReservaCita_AccionVerBotonHistorialServicio = "13030202";
        public static string SRC_ReservaCita_AccionVerBotonCalculadora = "13030203";

        public static string SRC_AdminCitas_AccionVerForm = "13030101";
        public static string SRC_AdminCitas_VerDet = "13030102";
        public static string SRC_AdminCitas_ActEstVerif = "13030103";
        public static string SRC_AdminCitas_AnuCita = "13030104";
        public static string SRC_AdminCitas_ConfCita = "13030105";
        public static string SRC_AdminCitas_ReprogCita = "13030106";
        public static string SRC_AdminCitas_ReasigCita = "13030107";
        public static string SRC_AdminCitas_ActDatosVehPropie = "13030108";
        public static string SRC_AdminCitas_AtenderCita = "13030109";

        public static string SRC_MantTaller_AccionVerForm = "13020601";
        public static string SRC_MantTaller_AccionVerDetalle = "13020602";
        public static string SRC_MantTaller_AccionEditar = "13020603";
        public static string SRC_MantTaller_AccionNuevo = "13020604";

        public static string SRC_MantUsuario_AccionVerForm = "13010201";
        public static string SRC_MantUsuario_AccionVerDetalle = "13010202";
        public static string SRC_MantUsuario_AccionEditar = "13010203";
        public static string SRC_MantUsuario_AccionNuevo = "13010204";

        public static string SRC_Reporte_CitasxFecha = "130403";
        public static string SRC_Reporte_IngresoDiario = "130402";

        public static string SRC_Reporte_ListadoCitasDiarias = "13040101";
        public static string SRC_Reporte_ListadoCitasGeneral = "13040102";

        public const string RUTA_MANTENIMIENTO_SRC = "Documentos\\Mantenimiento\\SRC\\";
        public const string FLAT_EXCEL_SRC = "20";

        public static string SRC_MantTaller_AccionAprobarCont = "13020605";
        public static string SRC_MantTaller_AccionRechazarCont = "13020606";
        public static string SRC_MantTaller_AccionDescargarCont = "13020607";
        public static string RUTA_IMAGENES_CONTENIDO = "Documentos\\Mantenimiento\\SRC\\Contenidos\\";
        public static string RUTA_IMAGENES_CONTENIDO_WEB = "Documentos/Mantenimiento/SRC/Contenidos/";

        public static string SRC_MantTaller_OpcionHSGI = "13020608";

        public static string FLAT_RUTA_EPORTAR_EXCEL = "25";


    }

    public class CONSTANTE_SEGURIDAD
    {
        public const String AccesoConsulta = "0";
        public const String AccesoEdicion = "1";

        public const String Admin_Sistema = "05";
        public const String Mantenimiento_Tablas = "0501";
        public const String Configuraciones = "0502";
        public const String Seguridad = "0503";
        public const String Usuarios = "050301";
        public const String Perfiles = "050302";

        public const String Marca_Modelo = "050104";
        public const String Paises_Ciudades = "050113";
        public const String Operaciones = "07";
        public const String Consultas_Reportes = "09";
        public const String Destinos = "050115";

        public const String Adm_Mantenimiento_Clientes = "050137";
        public const String Adm_Mantenimiento_Clientes_Tipo_Cliente = "05015301";
        public const String Adm_Mantenimiento_Clientes_Tipo_Documento = "05015302";
        public const String Adm_Mantenimiento_Clientes_Nro_Documento = "05015303";
        public const String Adm_Mantenimiento_Clientes_Nombres = "05015304";
        public const String Adm_Mantenimiento_Clientes_Ape_Paterno = "05015305";
        public const String Adm_Mantenimiento_Clientes_Ape_Materno = "05015307";
        public const String Adm_Mantenimiento_Clientes_Prefijo_Cliente = "05015308";
        public const String Adm_Mantenimiento_Clientes_Sexo = "05015309";
        public const String Adm_Mantenimiento_Clientes_Estado_Civil = "05015310";
        public const String Adm_Mantenimiento_Clientes_Fecha_Nacimiento = "05015311";
        public const String Adm_Mantenimiento_Clientes_Razon_Social = "05015312";
        public const String Adm_Mantenimiento_Clientes_Prospecto = "05015313";
        public const String Adm_Mantenimiento_Clientes_Telefono = "05015314";
        public const String Adm_Mantenimiento_Clientes_Venta_Corporativa = "05015315";
        public const String Adm_Mantenimiento_Clientes_Correo_Cliente = "05015316";
        public const String Adm_Mantenimiento_Clientes_Pagina_Web = "05015317";
        public const String Adm_Mantenimiento_Clientes_Celular = "05015318";
        public const String Adm_Mantenimiento_Clientes_Cat_Tipo_Cliente = "05015319";
        public const String Adm_Mantenimiento_Clientes_Por_Dscto_Cliente = "05015320";
        public const String Adm_Mantenimiento_Clientes_Clase_Cliente = "05015321";
        public const String Adm_Mantenimiento_Clientes_Fec_1er_Contacto = "05015322";
        public const String Adm_Mantenimiento_Clientes_Fax_Cliente = "05015323";
        public const String Adm_Mantenimiento_Clientes_Observaciones_Cliente = "05015324";
        public const String Adm_Mantenimiento_Clientes_Nombre_Empresa = "05015325";
        public const String Adm_Mantenimiento_Clientes_Telefono_Empresa = "05015326";
        public const String Adm_Mantenimiento_Clientes_Giro_Empresa = "05015327";
        public const String Adm_Mantenimiento_Clientes_Fax_Empresa = "05015328";
        public const String Adm_Mantenimiento_Clientes_Tipo_Empleado = "05015329";
        public const String Adm_Mantenimiento_Clientes_Cargo_Empresa = "05015330";
        public const String Adm_Mantenimiento_Clientes_País_Empresa = "05015331";
        public const String Adm_Mantenimiento_Clientes_Departamento_Empresa = "05015332";
        public const String Adm_Mantenimiento_Clientes_Provincia_Empresa = "05015333";
        public const String Adm_Mantenimiento_Clientes_Distrito_Empresa = "05015334";
        public const String Adm_Mantenimiento_Clientes_Direccion_Empresa = "05015335";
        public const String Adm_Mantenimiento_Clientes_Correo_Empresa = "05015336";
        public const String Adm_Mantenimiento_Clientes_Nombre_Cont = "05015337";
        public const String Adm_Mantenimiento_Clientes_Correo_Cont = "05015338";
        public const String Adm_Mantenimiento_Clientes_Telefono_Cont = "05015339";
        public const String Adm_Mantenimiento_Clientes_Pais_Cli = "05015340";
        public const String Adm_Mantenimiento_Clientes_Departamento_Cli = "05015341";
        public const String Adm_Mantenimiento_Clientes_Provincia_Cli = "05015342";
        public const String Adm_Mantenimiento_Clientes_Distrito_Cli = "05015343";
        public const String Adm_Mantenimiento_Clientes_Tel_Dir_Cli = "05015344";
        public const String Adm_Mantenimiento_Clientes_Fax_Dir_Cli = "05015345";
        public const String Adm_Mantenimiento_Clientes_Cod_Postal_Cli = "05015346";
        public const String Adm_Mantenimiento_Clientes_Direccion_Cli = "05015347";
    }

    public class CONSTANTE_GENERACION_PDF
    {
        public const Int32 TAMANIO_FUENTE_CUERPO = 9;
        public const Int32 TAMANIO_FUENTE_TABLA = 7;
        public const Int32 TAMANIO_FUENTE_TABLA_PIE_AGP = 14;
        public const Int32 TAMANIO_FUENTE_CUERPO_PIE_AGP = 10;
        public const Int32 TAMANIO_FUENTE_CUERPO_CONSIGNACION = 4;
    }

}