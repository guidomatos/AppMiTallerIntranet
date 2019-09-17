using System;
using System.Configuration;
using System.Web.UI;

public class Parametros
{
    //Nombres de los Mensajes [PERU - CHILE]
    public string SRC_CodEmpresa = Convert.ToString(ConfigurationManager.AppSettings["codEmpresa"]);
    public Int32 SRC_Pais = Convert.ToInt32(ConfigurationManager.AppSettings["CodPais"]);
    public string SRC_CodPais = Convert.ToString(ConfigurationManager.AppSettings["CodPais"]);
    public string SRC_HoraIniCC = Convert.ToString(ConfigurationManager.AppSettings["HoraIniCallCenter"]);
    public string SRC_HoraFinCC = Convert.ToString(ConfigurationManager.AppSettings["HoraFinCallCenter"]);
    public string SRC_TelefonoCC = Convert.ToString(ConfigurationManager.AppSettings["HoraFinCallCenter"]);
    public string SRC_DerechosCC = Convert.ToString(ConfigurationManager.AppSettings["HoraFinCallCenter"]);
    public string SRC_CambiarTaller = Convert.ToString(ConfigurationManager.AppSettings["CambiarTaller"]);
    public string SRC_DirArchivosAdjuntos = Convert.ToString(ConfigurationManager.AppSettings["ArchivosAdjuntos"]);
    public string SRC_DirListasGeneradas = Convert.ToString(ConfigurationManager.AppSettings["ListasGeneradas"]);
    public string SRC_OrdenGrilla = Convert.ToString(ConfigurationManager.AppSettings["OrdenGrilla"]);
    public string SRC_MostrarLogo = Convert.ToString(ConfigurationManager.AppSettings["MostrarLogo"]);
    public string SRC_MostrarTextoPie = Convert.ToString(ConfigurationManager.AppSettings["MostrarTextoPie"]);
    public string SRC_VINObligatorio = Convert.ToString(ConfigurationManager.AppSettings["VINObligatorio"]);
    public string SRC_MostrarColumnasUbigeo = Convert.ToString(ConfigurationManager.AppSettings["MostrarColumnasUbigeo"]);

	public string SRC_PlantillaCorreoQR = "'<tr><td colspan='2' align='center'><img src='{QR}' style='width: 200px;height: 200px;padding-top: 10px;'/></td></tr>";
    public string SRC_GuardaQR = ConfigurationManager.AppSettings["FileServerPath"].ToString() + @"Imagenes\QRCode\";
    public string SRC_AccedeQR = "http://localhost/FileServer/" + @"Imagenes/QRCode/";

    private static string nombreMensaje(string strMSG)
    {
        return ConfigurationManager.AppSettings[strMSG + "_" + Convert.ToString(ConfigurationManager.AppSettings["CodPais"])];
    }
    
    /* Parametros Principales del SRC */
    public enum PARM
    {
        _01 = 1,
        _02 = 2,
        _03 = 3,
        _04 = 4,
        _05 = 5,
        _06 = 6,
        _07 = 7,
        _08 = 8,
        _09 = 9,
        _10 = 10,
        _11 = 11,
        _12 = 12,
        _13 = 13,
        _14 = 14,
        _15 = 15,
        _16 = 16,

    };

    public string GetValor(PARM Prm)
    {
        return ConfigurationManager.AppSettings["PRM_" + Convert.ToInt32(Prm).ToString()].ToString();
    }

    public static void SRC_Mensaje(Page page, string strMensaje)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "", "<script>alert('" + strMensaje + "');</script>", false);
    }

    public static void SRC_Mensaje_Redireccionar(Page page, string strMensaje, string strRuta)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "", "<script>alert('" + strMensaje + "');location.href='" + strRuta + "';</script>", false);
    }


    //Nombres de los Controles [PERU - CHILE]
    public string N_Placa = Parametros.nombreMensaje("nPlaca");
    public string N_Departamento = Parametros.nombreMensaje("nDepartamento");
    public string N_Provincia = Parametros.nombreMensaje("nProvincia");
    public string N_Distrito = Parametros.nombreMensaje("nDistrito");
    public string N_Taller = Parametros.nombreMensaje("nTaller");
    public string N_Local = Parametros.nombreMensaje("nLocal");
   
    public string N_Disponibilidad = Parametros.nombreMensaje("nDisponibilidad");
    public string N_Asesor = Parametros.nombreMensaje("nAsesor");
    public string N_TextoPieCorreo = Parametros.nombreMensaje("nTextoPieCorreo");

    public string N_RangoAniosVehiculo = Convert.ToString(ConfigurationManager.AppSettings["RangoAniosVehiculo"]);
    public string N_TiposVehiculo = Convert.ToString(ConfigurationManager.AppSettings["TiposVehiculo"]);
    public string N_MostrarAnioTipoVehiculo = Convert.ToString(ConfigurationManager.AppSettings["MostrarAnioTipoVehiculo"]);
    
    /***************************************************************************************/

    public string msgNoAsesores = nombreMensaje("msgNoAsesores");
    public string msgCapacidadTaller = nombreMensaje("msgCapacidadTaller");
    public string msgYaVencCita = nombreMensaje("msgYaVencCita");

    public string msgServDia = Parametros.nombreMensaje("msgServDia");

    public string msgError = Parametros.nombreMensaje("msgError");

    public string msgCambioTaller = Parametros.nombreMensaje("msgCambioTaller");

    public string msgYaExistePlaca = Parametros.nombreMensaje("msgYaExistePlaca");

    public string msgAtenderUnid = Parametros.nombreMensaje("msgAtenderUnid");

    public string msgCitasPend = Parametros.nombreMensaje("msgCitasPend");

    public string msgCambDoc = Parametros.nombreMensaje("msgCambDoc");
    public string msgRegOKContacto = Parametros.nombreMensaje("msgRegOKContacto");
    public string msgActOKContacto = Parametros.nombreMensaje("msgActOKContacto");

    public string msgNoExisteContactoBD = Parametros.nombreMensaje("msgNoExisteContactoBD");
    public string msgNoExisteCita = Parametros.nombreMensaje("msgNoExisteCita");
    public string msgNoCita = Parametros.nombreMensaje("msgNoCita");
    public string msgRegCita = Parametros.nombreMensaje("msgRegCita");
    public string msgConfCita = Parametros.nombreMensaje("msgConfCita");

    public string msgAnulCita = Parametros.nombreMensaje("msgAnulCita");
    public string msgNoMapa = Parametros.nombreMensaje("msgNoMapa");
    public string msgNoHorario2 = Parametros.nombreMensaje("msgNoHorario2");

    public string msgNoHorario1 = Parametros.nombreMensaje("msgNoHorario1");
    public string msgYaReprogCita = Parametros.nombreMensaje("msgYaReprogCita");
    public string msgYaAnulCita = Parametros.nombreMensaje("msgYaAnulCita");
    public string msgYaAtendCita = Parametros.nombreMensaje("msgYaAtendCita");

    public string msgYaConfCita = Parametros.nombreMensaje("msgYaConfCita");
    public string msgNoExisteContacto = Parametros.nombreMensaje("msgNoExisteContacto");
    public string msgExistePlaca = Parametros.nombreMensaje("msgExistePlaca");
    public string msgFecMax = Parametros.nombreMensaje("msgFecMax");
    public string msgFecMin = Parametros.nombreMensaje("msgFecMin");

    public string msgFecIniFin = Parametros.nombreMensaje("msgFecIniFin");
    public string msgNoHorarioRango = Parametros.nombreMensaje("msgNoHorarioRango");

    public string msgSelFechaFin = Parametros.nombreMensaje("msgSelFechaFin");
    public string msgSelFec = Parametros.nombreMensaje("msgSelFec");

    public string msgSelHora = Parametros.nombreMensaje("msgSelHora");
    public string msgCitasPendPlaca = Parametros.nombreMensaje("msgCitasPendPlaca");
    public string msgNoExisteDoc = Parametros.nombreMensaje("msgNoExisteDoc");
    public string msgNoExisteRUC = Parametros.nombreMensaje("msgNoExisteRUC");
    public string msgRepRUC = Parametros.nombreMensaje("msgRepRUC");
    public string msgRepDoc = Parametros.nombreMensaje("msgRepDoc");
    public string msgRepCont = Parametros.nombreMensaje("msgRepCont");
    public string msgEstaCambio = Parametros.nombreMensaje("msgEstaCambio");

    //ComboBox
    public string _SELECCIONE = "--Seleccione--";
    public string _TODOS = "--Todos--";
    public string _VACIO = "      ---      ";
    
    public string GETFechaActual()
    {
        return DateTime.Now.ToString();
    }
      
    public enum PAGINA{ 
        CONSULTAR = 1,
        CONFIRMAR = 2,
        ANULAR = 3,
        REPROGRAMAR = 4,
        RESERVAR = 5
    }

    public enum EstadoCita
    {
        REGISTRADA = 1,
        REPROGRAMADA = 2,
        ANULADA = 3,
        CONFIRMADA = 4,
        REASIGNADA = 5,
        ATENDIDA = 6,
        COLA_ESPERA = 7,
        VENCIDA = 8
    }

    public enum Pais
    {
        PERU = 1
    };

    enum Empresa
    {
        GILDEMEISTER = 1,
        MANASA,
        MOTORMUNDO
    };


    public enum PERSONA
    {
        CLIENTE = 1,
        ASESOR = 2,
        CALL_CENTER = 3
    }

   
    /*
    public Parametros()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        
    }*/
}