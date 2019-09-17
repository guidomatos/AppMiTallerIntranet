using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.BL;
using AppMiTaller.Intranet.WebEvents;
using System.Security.Principal;
using System.Configuration;
using System.Net;

public partial class Logeo : System.Web.UI.Page
{
    private bool onError;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            this.pnlLogeo.Focus();
        }
    }

    private bool isMobileBrowser()
    {
        //GETS THE CURRENT USER CONTEXT
        HttpContext context = HttpContext.Current;
        //FIRST TRY BUILT IN ASP.NT CHECK
        if (context.Request.Browser.IsMobileDevice)
        {
            return true;
        }
        //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
        if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
        {
            return true;
        }
        //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
        if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
        context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
        {
            return true;
        }
        //AND FINALLY CHECK THE HTTP_USER_AGENT
        //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
        if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            //Create a list of all mobile types
            string[] mobiles =
            new[]
                {
                "midp", "j2me", "avant", "docomo",
                "novarra", "palmos", "palmsource",
                "240x320", "opwv", "chtml",
                "pda", "windows ce", "mmp/",
                "blackberry", "mib/", "symbian",
                "wireless", "nokia", "hand", "mobi",
                "phone", "cdm", "up.b", "audio",
                "SIE-", "SEC-", "samsung", "HTC",
                "mot-", "mitsu", "sagem", "sony"
                , "alcatel", "lg", "eric", "vx",
                "NEC", "philips", "mmm", "xx",
                "panasonic", "sharp", "wap", "sch",
                "rover", "pocket", "benq", "java",
                "pt", "pg", "vox", "amoi",
                "bird", "compal", "kg", "voda",
                "sany", "kdd", "dbt", "sendo",
                "sgh", "gradi", "jb", "dddi",
                "moto", "iphone"
                , "android"
                };
            //Loop through each item in the list created above
            //and check if the header contains that text
            foreach (string s in mobiles)
            {
                if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                ToLower().Contains(s.ToLower()))
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void Logeo_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String indLogeo, msgLogeo;
        SeguridadBL oSeguridadBL = new SeguridadBL();
        UsuarioBE oUsuario = new UsuarioBE();
        MembershipUser user;

        /*Opciones de seguridad*/
        PerfilBL oPerfilBL = new PerfilBL();
        OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();

        try
        {
            oSeguridadBL.ErrorEvent += new SeguridadBL.ErrorDelegate(Transaction_ErrorEvent);
            oPerfilBL.ErrorEvent += new PerfilBL.ErrorDelegate(Transaction_ErrorEvent);
            onError = false;
            oUsuario = oSeguridadBL.ValidaLogeoUsuario(this.pnlLogeo.UserName, this.pnlLogeo.Password, ConfigurationManager.AppSettings["AplicacionID"], out indLogeo, out msgLogeo);
            if (onError) return;

            if (indLogeo.Equals("0"))
            {
                if (ValidaHorarioAcceso(oUsuario, out msgLogeo))
                {
                    e.Authenticated = true;
                }
                else
                {
                    ((Panel)pnlLogeo.FindControl("pnlMensaje")).Visible = true;
                    this.pnlLogeo.FailureText = msgLogeo;
                    e.Authenticated = false;
                }
            }
            else e.Authenticated = false;

            if (e.Authenticated)
            {
                user = Membership.GetUser(this.pnlLogeo.UserName.ToUpper().Trim());
                if (user == null)
                {
                    user = Membership.CreateUser(this.pnlLogeo.UserName.ToUpper().Trim(), this.pnlLogeo.Password);
                }

                oUsuario.CUSR_ID = this.pnlLogeo.UserName.ToUpper().Trim();
                System.Net.IPHostEntry host;
                host = System.Net.Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]);

                //String clientComputerName = host.HostName;
                String clientComputerName = String.Concat((isMobileBrowser() ? "MT" : "MW"), "-", host.HostName); //MW => Mobile Web|MT => Mobile Tablet

                String usuarioRed = Request.ServerVariables["LOGON_USER"];
                String[] arrusuarioRed;
                if (usuarioRed != null)
                {
                    arrusuarioRed = usuarioRed.Split('\\');
                    if (arrusuarioRed.Length > 0) usuarioRed = arrusuarioRed[arrusuarioRed.Length - 1];
                }
                else usuarioRed = String.Empty;

                // Primero comprobamos si se accede desde un proxy
                string ipAddress1 = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                // Acceso desde una máquina particular
                string ipAddress2 = Request.ServerVariables["REMOTE_ADDR"];
                string ipAddress = string.IsNullOrEmpty(ipAddress1) ? ipAddress2 : ipAddress1;

                string EstacionRed = Dns.GetHostEntry(ipAddress).HostName;  //Estacion de Red.

                WindowsIdentity user_red = WindowsIdentity.GetCurrent();
                string usured = user_red.Name;

                /*Opciones relacionadas a su perfil*/
                ProfileCommon profile = Profile.GetProfile(oUsuario.CUSR_ID);
                profile.Estacion = clientComputerName;
                profile.UsuarioRed = usuarioRed;
                profile.Usuario = oUsuario;
                profile.PageSize = 10;
                profile.PageSizePopUp = 8;
                profile.PageSizeFiles = 3;
                profile.Aplicacion = ConfigurationManager.AppSettings["AplicacionID"];
                profile.Save();

                this.pnlLogeo.DestinationPageUrl = "Inicio/Default.aspx";
            }
            else
            {
                ((Panel)pnlLogeo.FindControl("pnlMensaje")).Visible = true;
                this.pnlLogeo.FailureText = msgLogeo;
                
                if (indLogeo.Equals("-5"))
                {
                    int nroIntentosFallidos = 0;
                    if (ViewState["nroIntentosFallidos"] != null) nroIntentosFallidos = (int)ViewState["nroIntentosFallidos"];
                    nroIntentosFallidos++;

                    int int_Bloqueo;
                    int.TryParse(txhPolitica.Value.ToString(), out int_Bloqueo);
                    
                    if (nroIntentosFallidos >= int_Bloqueo)
                    {
                        msgLogeo = "El usuario se encuentra bloqueado por exceso de errores en el ingreso de contraseña.";
                        oSeguridadBL.UpdateLogeoBloqueo(this.pnlLogeo.UserName, "1", msgLogeo);
                        nroIntentosFallidos = 0;
                    }

                    ViewState["nroIntentosFallidos"] = nroIntentosFallidos;
                }
            }
        }
        catch (Exception ex)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl, false);
            this.Web_ErrorEvent(this, ex);
        }
    }

    #region "Excepciones"
    public void Transaction_ErrorEvent(object sender, Exception ex)
    {
        TransactionFailureEvent input = new TransactionFailureEvent(sender, Profile.Usuario.CUSR_ID, ex.Message);
        input.Raise();
        onError = true;
    }
    public void Web_ErrorEvent(object sender, Exception ex)
    {
        WebFailureEvent input = new WebFailureEvent(sender, Profile.Usuario.CUSR_ID, ex.Message);
        input.Raise();
        onError = true;
    }
    #endregion

    #region "Validacion de Acceso al sistema"
    private Boolean ValidaHorarioAcceso(UsuarioBE oUsuario, out String msgError)
    {
        Boolean retorno = true;
        msgError = String.Empty;
        try
        {
            if (!RangoFechasValido(oUsuario.SFE_INICIO_ACCESO_PERFIL, oUsuario.SFE_FIN_ACCESO_PERFIL))
            {
                retorno = false;
                msgError = "Su contraseña a caducado.";
            }
            else if (!RangoHorasValido(oUsuario.HR_INICIO_ACCESO_PERFIL, oUsuario.HR_FIN_ACCESO_PERFIL))
            {
                retorno = false;
                msgError = "Su contraseña a caducado.";
            }
            else if (!RangoFechasValido(oUsuario.SFE_INICIO_ACCESO, oUsuario.SFE_FIN_ACCESO))
            {
                retorno = false;
                msgError = "Su contraseña a caducado.";
            }
            else if (!RangoHorasValido(oUsuario.HR_INICIO_ACCESO, oUsuario.HR_FIN_ACCESO))
            {
                retorno = false;
                msgError = "Su contraseña a caducado.";
            }
        }
        catch (Exception ex)
        {
            this.Web_ErrorEvent(this, ex);
        }
        return retorno;
    }

    private Boolean RangoFechasValido(String feInicio, String feFin)
    {
        Int32 intFeIni = 0, intFeFIn = 0, intFeActual = 0;

        try
        {
            String[] arrFeIni = feInicio.Split('/');
            String[] arrFeFin = feFin.Split('/');
            String[] arrFeAct = DateTime.Now.ToString(ConstanteBE.formato_fecha_es).Split('/');

            if (arrFeIni.Length == 3)
            {
                Int32.TryParse(arrFeIni[2] + arrFeIni[1] + arrFeIni[0], out intFeIni);
            }

            if (arrFeFin.Length == 3)
            {
                Int32.TryParse(arrFeFin[2] + arrFeFin[1] + arrFeFin[0], out intFeFIn);
            }

            if (arrFeAct.Length == 3)
            {
                Int32.TryParse(arrFeAct[2] + arrFeAct[1] + arrFeAct[0], out intFeActual);
            }

            if (intFeIni > 0 && intFeActual < intFeIni)
            {
                return false;
            }

            if (intFeFIn > 0 && intFeFIn < intFeActual)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            this.Web_ErrorEvent(this, ex);
        }

        return true;
    }

    private Boolean RangoHorasValido(String horaInicio, String horaFin)
    {
        Int32 intHoraInicio = 0, intHoraFin = 0, intHoraActual = 0;

        try
        {
            String[] arrHoraIni = horaInicio.Split(':');
            String[] arrHoraFin = horaFin.Split(':');
            intHoraActual = (DateTime.Now.Hour * 100) + DateTime.Now.Minute;

            if (arrHoraIni.Length == 2)
            {
                Int32.TryParse(arrHoraIni[0] + arrHoraIni[1], out intHoraInicio);
            }

            if (arrHoraFin.Length == 2)
            {
                Int32.TryParse(arrHoraFin[0] + arrHoraFin[1], out intHoraFin);
            }

            if (intHoraInicio > 0 && intHoraActual < intHoraInicio)
            {
                return false;
            }

            if (intHoraFin > 0 && intHoraFin < intHoraActual)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            this.Web_ErrorEvent(this, ex);
        }

        return true;
    }
    #endregion

    protected void lbRecuperarContrasena_Click(object sender, EventArgs e)
    {
        SeguridadBL oSeguridadBL = new SeguridadBL();
        UsuarioBL oUsuarioBL = new UsuarioBL();
        UsuarioBE oUsuario = new UsuarioBE();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate(Transaction_ErrorEvent);

        int retorno;
        try
        {
            System.Net.IPHostEntry host;
            host = System.Net.Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]);
            String clientComputerName = host.HostName;

            String usuarioRed = Request.ServerVariables["LOGON_USER"];
            String[] arrusuarioRed;
            if (usuarioRed != null)
            {
                arrusuarioRed = usuarioRed.Split('\\');
                if (arrusuarioRed.Length > 0) usuarioRed = arrusuarioRed[arrusuarioRed.Length - 1];
            }
            else usuarioRed = String.Empty;

            onError = false;
            //Se genera nueva clave aleatoria ************************
            string contrasenaTemporal = "";
            Random obj = new Random();
            string repositorio = "abcdefghijklmnopqrstuvwxyz";
            int longitud = repositorio.Length;
            char letra;
            int longitudContrasena = 8;
            for (int i = 0; i < longitudContrasena; i++)
            {
                letra = repositorio[obj.Next(longitud)];
                contrasenaTemporal += letra.ToString();
            }
            contrasenaTemporal = contrasenaTemporal.Substring(0, 4) + contrasenaTemporal.Substring(4, 4).ToUpper();
            //Agregando números a la contraseña
            contrasenaTemporal += obj.Next(10, 99).ToString();
            //Agregando caracteres especiales
            repositorio = "@$#%()=!¡¿?*+-_";
            longitud = repositorio.Length;
            for (int i = 0; i < 1; i++)
            {
                letra = repositorio[obj.Next(longitud)];
                contrasenaTemporal += letra.ToString();
            }
            retorno = oUsuarioBL.ModificarPassWord(pnlLogeo.UserName, contrasenaTemporal, pnlLogeo.UserName, usuarioRed, clientComputerName, "1", contrasenaTemporal);

            if (!onError && retorno > 0)
            {
                JavaScriptHelper.FuncionAjax(this, "fc_AlertSuccess", "'La operación fue realizada con éxito. Por favor, busque el correo electrónico con las instrucciones para ingresar nuevamente al sistema. Gracias.'", String.Empty);
            }
            else if (!onError && retorno == -5)
            {
                JavaScriptHelper.FuncionAjax(this, "fc_AlertError", Message.keyUsuarioNoReg, String.Empty);
            }
            else if (!onError && retorno == -6)
            {
                JavaScriptHelper.FuncionAjax(this, "fc_AlertError", Message.keyUsuarioRepetido, String.Empty);
            }
            else
            {
                JavaScriptHelper.FuncionAjax(this, "fc_AlertError", Message.keyErrorGrabar, String.Empty);
            }
        }
        catch (Exception ex)
        {
            Web_ErrorEvent(this, ex);
        }
    }

    protected void btnAceptarCambioContrasenha_Click(object sender, EventArgs e)
    {
        String indLogeo, msgLogeo;
        SeguridadBL oSeguridadBL = new SeguridadBL();
        UsuarioBL oUsuarioBL = new UsuarioBL();
        UsuarioBE oUsuario = new UsuarioBE();
        oUsuarioBL.ErrorEvent += new UsuarioBL.ErrorDelegate(Transaction_ErrorEvent);

        int retorno;
        try
        {
            /*Validamos que los datos ingresados sena correctos*/
            oSeguridadBL.ErrorEvent += new SeguridadBL.ErrorDelegate(Transaction_ErrorEvent);
            onError = false;
            oUsuario = oSeguridadBL.ValidaLogeoUsuario(this.txtUsuario_CambioPwd.Text, this.txtPwd_CambioPwd.Text,
                    ConfigurationManager.AppSettings["AplicacionID"], out indLogeo, out msgLogeo);

            if (onError)
            {
                String executeFuncion = "$('#modal_CambiarPwd').modal('show');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "__MODAL_OPEN__", executeFuncion, true);
                return;
            }

            if (indLogeo.Equals("0") || indLogeo.Equals("-6") || indLogeo.Equals("-7"))// I/F @003
            {
                
                System.Net.IPHostEntry host;
                host = System.Net.Dns.GetHostEntry(Request.ServerVariables["REMOTE_HOST"]);
                String clientComputerName = host.HostName;

                String usuarioRed = Request.ServerVariables["LOGON_USER"];
                String[] arrusuarioRed;
                if (usuarioRed != null)
                {
                    arrusuarioRed = usuarioRed.Split('\\');
                    if (arrusuarioRed.Length > 0) usuarioRed = arrusuarioRed[arrusuarioRed.Length - 1];
                }
                else usuarioRed = String.Empty;

                onError = false;

                retorno = oUsuarioBL.ModificarPassWord(this.txtUsuario_CambioPwd.Text, this.txtNewPwd_CambioPwd.Text, this.txtUsuario_CambioPwd.Text, usuarioRed, clientComputerName, "0", this.txtNewPwd_CambioPwd.Text);
                if (!onError && retorno > 0)
                {
                    JavaScriptHelper.FuncionAjax(this, "fc_AlertSuccess", Message.keyGrabar, String.Empty);
                }
                else if (!onError && retorno == -5)
                {
                    JavaScriptHelper.FuncionAjax(this, "fc_AlertError", Message.keyUsuarioNoReg, String.Empty);
                }
                else if (!onError && retorno == -6)
                {
                    JavaScriptHelper.FuncionAjax(this, "fc_AlertError", Message.keyUsuarioRepetido, String.Empty);
                }
                else
                {
                    JavaScriptHelper.FuncionAjax(this, "fc_AlertError", Message.keyErrorGrabar, String.Empty);
                }
            }
            else
            {
                JavaScriptHelper.FuncionAjax(this, "fc_Alert", String.Format("'{0}'", msgLogeo), String.Empty);
                String executeFuncion = "$('#modal_CambiarPwd').modal('show');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "__MODAL_OPEN__", executeFuncion, true);
            }

        }
        catch (Exception ex)
        {
            Web_ErrorEvent(this, ex);
        }
    }

}
