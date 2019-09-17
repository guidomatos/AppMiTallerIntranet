using System.Net.Mail;
using System.Text;
using AppMiTaller.Intranet.BE;
using System.Configuration;
using System.IO;
using System;
using System.ComponentModel;

public class CorreoElectronico
{
    private string path_server;

    public CorreoElectronico(string path_server)
    {
        this.path_server = path_server;
    }

    public enum Receptor
    {
        CLIENTE = 0,
        ASESOR_SERVICIO = 1,
        CALL_CENTER = 2,
        IMPRESION = 3
    }

    private Parametros oParametros = new Parametros();


    private string GetFechaLarga(DateTime dt)
    {
        return dt.ToString("D", System.Globalization.CultureInfo.CurrentCulture);
    }
    private string FormatoHora(string strHora)
    {
        string strHF;
        try
        {
            string strHoraF = Convert.ToDateTime(strHora).ToString("hh:mm");
            Int32 strHoraF1 = Convert.ToInt32(strHora.Replace(":", ""));
            strHF = strHoraF + (((strHoraF1 < 1200)) ? " a.m." : " p.m.");
        }
        catch (Exception)
        {
            strHF = strHora;
        }
        return strHF;

    }
    public StringBuilder CargarPlantilla_Cliente(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        StringBuilder strBodyHTML = new StringBuilder();

        string strRutaPlantilla = Path.Combine(String.Concat(this.path_server, "SRC_Operaciones\\"), ConfigurationManager.AppSettings["PlantillaCorreo"].Replace("/", "\\"));

        string strVerNota = oDatos.fl_nota; /*Nota: 26.11.2012 */
        try
        {
            if (!System.IO.File.Exists(strRutaPlantilla))
            {
                strBodyHTML.Append("-1");
            }
            else
            {
                FileStream stream = new FileStream(strRutaPlantilla, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);


                string strTipoCita = string.Empty;

                switch (oTipoCita)
                {
                    case Parametros.EstadoCita.REGISTRADA: strTipoCita = "Reserva"; break;
                    case Parametros.EstadoCita.REPROGRAMADA: strTipoCita = "Reprogramaci&oacute;n"; break;
                    case Parametros.EstadoCita.REASIGNADA: strTipoCita = "Asignaci&oacute;n"; break;
                    case Parametros.EstadoCita.ANULADA: strTipoCita = "Anulaci&oacute;n"; break;
                }

                string strTextoPie = oParametros.N_TextoPieCorreo;
                string strNumCallCenter = string.Empty;

                if (!oDatos.nid_taller_empresa.Equals(0))
                {
                    strTextoPie = strTextoPie.Replace("{banco}", oDatos.no_banco);
                    strTextoPie = strTextoPie.Replace("{num_cuenta}", oDatos.nu_cuenta);
                    strNumCallCenter = oDatos.nu_callcenter.Trim();
                }

                string url = "";
                switch (oDatos.Nid_empresa)
                {
                    case 1: url = ConfigurationManager.AppSettings["UrlCitasGildemeister"].ToString(); break;
                    case 2: url = ConfigurationManager.AppSettings["UrlCitasManasa"].ToString(); break;
                    case 3: url = ConfigurationManager.AppSettings["UrlCitasMotormundo"].ToString(); break;
                }

                string linea = null;

                while (reader.Peek() > -1)
                {
                    linea = reader.ReadLine().ToString();
                    linea = linea.Replace("{ImagenLogo}", url + "img/marcas/logo_" + oDatos.nid_marca + ".jpg");
                    linea = linea.Replace("{ImagenLogoApp}", url + "img/titulo.jpg");
                    linea = linea.Replace("{Titulo}", url + "img/titulo.jpg");
                    linea = linea.Replace("{Fondo}", url + "img/fondoC1.jpg");
                    linea = linea.Replace("{Cliente}", oDatos.no_cliente.Trim().ToUpper() + " " + oDatos.no_ape_paterno.Trim().ToUpper() + " " + oDatos.no_ape_materno.Trim().ToUpper());
                    linea = linea.Replace("{TipoCita}", strTipoCita);
                    linea = linea.Replace("{TipoPlaca}", oParametros.N_Placa + ": ");
                    linea = linea.Replace("{NumPlaca}", oDatos.nu_placa.ToUpper());
                    linea = linea.Replace("{Marca}", oDatos.no_marca.ToUpper());
                    linea = linea.Replace("{Modelo}", oDatos.no_modelo.ToUpper());
                    linea = linea.Replace("{DiaHoraCita}", GetFechaLarga(oDatos.fe_prog) + " - " + FormatoHora(oDatos.ho_inicio_c));
                    linea = linea.Replace("{TextoAsesor}", oDatos.fl_entrega.Equals("1") ? "Asesor de Entrega:" : oParametros.N_Asesor + ": "); //@001
                    linea = linea.Replace("{Asesor}", oDatos.no_asesor);
                    linea = linea.Replace("{MovilAsesor}", oDatos.nu_telefono_a);
                    linea = linea.Replace("{Servicio}", oDatos.no_servicio.Trim().ToUpper());
                    linea = linea.Replace("{Taller}", oDatos.no_taller);
                    linea = linea.Replace("{TextoLocal}", oParametros.N_Local + ": ");
                    linea = linea.Replace("{PuntoRed}", oDatos.no_ubica);
                    linea = linea.Replace("{Direccion}", oDatos.di_ubica + " - " + oDatos.no_distrito.Trim());
                    linea = linea.Replace("{Telefono}", oDatos.nu_telefono_t);
                    linea = linea.Replace("{CodReserva}", oDatos.cod_reserva_cita);
                    linea = linea.Replace("{FormaRecordatorio}", "- Por Email");
                    linea = linea.Replace("{TextoPieCorreo}", strTextoPie);
                    linea = linea.Replace("{CallCenter}", strNumCallCenter);
                    linea = linea.Replace("{UrlPagina}", url);
                    linea = linea.Replace("{ImagenPie}", url + "img/Pie1.jpg");
                    linea = linea.Replace("{blLogo}", ((oParametros.SRC_MostrarLogo.Equals("1")) ? "block" : "none"));
                    linea = linea.Replace("{nu_taller}", oDatos.nu_telefono_t);
                    linea = linea.Replace("{QR}", oDatos.no_nombreqr == null ? "" : oDatos.no_nombreqr == "" ? "" : oParametros.SRC_PlantillaCorreoQR.Replace("{QR}", oParametros.SRC_AccedeQR + oDatos.no_nombreqr)); //@010 I/F

                    strBodyHTML.Append(linea);
                }

                reader.Close();
            }

            // Ocultar Etiquetas
            //--------------------------------
            if (strVerNota.Equals("0")) strBodyHTML = Ocultar_Etiqueta(strBodyHTML, "SPAN_3");
            if (oDatos.fl_entrega.Equals("1")) strBodyHTML = Ocultar_Etiqueta(strBodyHTML, "SPAN_2");
            if (oDatos.fl_entrega.Equals("1")) strBodyHTML = Ocultar_Etiqueta(strBodyHTML, "SPAN_4");
            if (oDatos.fl_entrega.Equals("0")) strBodyHTML = Ocultar_Etiqueta(strBodyHTML, "SPAN_1");
            //--------------------------------                     
            if (oDatos.fl_entrega.Equals("1")) strBodyHTML = Ocultar_Etiqueta_2(strBodyHTML, "Envio_1");
            if (oDatos.fl_entrega.Equals("1")) strBodyHTML = Ocultar_Etiqueta_2(strBodyHTML, "Envio_2");
            if (oDatos.fl_entrega.Equals("1")) strBodyHTML = Ocultar_Etiqueta_2(strBodyHTML, "Envio_3");
            if (oDatos.fl_entrega.Equals("0")) strBodyHTML = Ocultar_Etiqueta(strBodyHTML, "SPAN_5");
            if (!oDatos.nid_marca.ToString().Equals("12") && oParametros.SRC_CodPais.Equals("1")) strBodyHTML = Ocultar_Etiqueta_2(strBodyHTML, "trLogoApp");
        }
        catch//(Exception ex)
        {
            strBodyHTML = new StringBuilder();
            strBodyHTML.Append("-2");
        }

        return strBodyHTML;
    }
    private StringBuilder CargarPlantilla_Asesor(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        StringBuilder strBodyHTML = new StringBuilder();

        string strRutaPlantilla = String.Concat(this.path_server, "SRC_Operaciones\\", ConfigurationManager.AppSettings["PlantillaCorreoAsesor"]);
        try
        {
            if (!(System.IO.File.Exists(strRutaPlantilla)))
            {
                strBodyHTML.Append("-1");
            }
            else
            {
                FileStream stream = new FileStream(strRutaPlantilla, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                string strTipoCita = string.Empty;
                switch (oTipoCita)
                {
                    case Parametros.EstadoCita.REGISTRADA: strTipoCita = "Reserva"; break;
                    case Parametros.EstadoCita.REPROGRAMADA: strTipoCita = "Reprogramacion"; break;
                    case Parametros.EstadoCita.REASIGNADA: strTipoCita = "Asignaci&oacute;n"; break;
                    case Parametros.EstadoCita.ANULADA: strTipoCita = "Anulacion"; break;
                }

                string linea = null;
                string url = "";
                switch (oDatos.Nid_empresa)
                {
                    case 1: url = ConfigurationManager.AppSettings["UrlCitasGildemeister"].ToString(); break;
                    case 2: url = ConfigurationManager.AppSettings["UrlCitasManasa"].ToString(); break;
                    case 3: url = ConfigurationManager.AppSettings["UrlCitasMotormundo"].ToString(); break;
                }

                while (reader.Peek() > -1)
                {
                    linea = reader.ReadLine().ToString();
                    linea = linea.Replace("{ImagenLogo}", (url + "Images/SRC/marcas/logo_" + oDatos.nid_marca.ToString() + ".jpg")); //@011 I/F
                    linea = linea.Replace("{ImagenLogo}", (url + "Images/SRC/marcas/logo_" + oDatos.nid_marca.ToString() + ".jpg")); /*@001: */
                    linea = linea.Replace("{Fondo}", (url + ConfigurationManager.AppSettings["Fondo"]));
                    linea = linea.Replace("{Asesor}", oDatos.no_asesor.Trim().ToUpper());
                    linea = linea.Replace("{TipoCita}", strTipoCita);
                    linea = linea.Replace("{FechaCita}", GetFechaLarga(oDatos.fe_prog).ToUpper());
                    linea = linea.Replace("{HoraCita}", FormatoHora(oDatos.ho_inicio_c));
                    linea = linea.Replace("{Cliente}", oDatos.no_cliente.Trim().ToUpper() + " " + oDatos.no_ape_paterno.Trim().ToUpper() + " " + oDatos.no_ape_materno.Trim().ToUpper());
                    linea = linea.Replace("{Servicio}", oDatos.no_servicio.ToUpper());
                    linea = linea.Replace("{TipoPlaca}", oParametros.N_Placa + ":");
                    linea = linea.Replace("{Placa}", oDatos.nu_placa.ToUpper());
                    linea = linea.Replace("{Marca}", oDatos.no_marca);
                    linea = linea.Replace("{Modelo}", oDatos.no_modelo);
                    linea = linea.Replace("{UrlPagina}", url);
                    linea = linea.Replace("{ImagenPie}", (url + ConfigurationManager.AppSettings["Pie" + oParametros.SRC_CodEmpresa + ""]));
                    strBodyHTML.Append(linea);
                }

                reader.Close();
            }
        }
        catch//(Exception ex)
        {
            strBodyHTML = new StringBuilder();
            strBodyHTML.Append("-1");
        }

        return strBodyHTML;
    }
    private StringBuilder CargarPlantilla_CallCenter(CitasBE oDatos, Parametros.EstadoCita oTipoCita)
    {
        StringBuilder strBodyHTML = new StringBuilder();

        string strRutaPlantilla = String.Concat(this.path_server, "SRC_Operaciones\\", ConfigurationManager.AppSettings["PlantillaCorreoCallCenter"]);

        try
        {
            if (!(System.IO.File.Exists(strRutaPlantilla)))
            {
                strBodyHTML.Append("-1");
            }
            else
            {
                FileStream stream = new FileStream(strRutaPlantilla, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                string strTipoCita = string.Empty;
                switch (oTipoCita)
                {
                    case Parametros.EstadoCita.REGISTRADA: strTipoCita = "Reserva"; break;
                    case Parametros.EstadoCita.REPROGRAMADA: strTipoCita = "Reprogramacion"; break;
                    case Parametros.EstadoCita.REASIGNADA: strTipoCita = "Asignaci&oacute;n"; break;
                    case Parametros.EstadoCita.ANULADA: strTipoCita = "Anulacion"; break;
                }

                string linea = null;
                string url = "";
                switch (oDatos.Nid_empresa)
                {
                    case 1: url = ConfigurationManager.AppSettings["UrlCitasGildemeister"].ToString(); break;
                    case 2: url = ConfigurationManager.AppSettings["UrlCitasManasa"].ToString(); break;
                    case 3: url = ConfigurationManager.AppSettings["UrlCitasMotormundo"].ToString(); break;
                }

                while (reader.Peek() > -1)
                {
                    linea = reader.ReadLine().ToString();
                    linea = linea.Replace("{ImagenLogo}", (url + "Images/SRC/marcas/logo_" + oDatos.nid_marca.ToString() + ".jpg")); //@011 I/F
                    linea = linea.Replace("{Titulo}", (url + ConfigurationManager.AppSettings["Titulo"]));
                    linea = linea.Replace("{Fondo}", (url + ConfigurationManager.AppSettings["Fondo"]));
                    linea = linea.Replace("{Asesor}", oDatos.no_asesor.Trim().ToUpper());
                    linea = linea.Replace("{TipoCita}", strTipoCita);
                    linea = linea.Replace("{FechaCita}", GetFechaLarga(oDatos.fe_prog).ToUpper());
                    linea = linea.Replace("{HoraCita}", FormatoHora(oDatos.ho_inicio_c));
                    linea = linea.Replace("{Cliente}", oDatos.no_cliente.Trim().ToUpper() + " " + oDatos.no_ape_paterno.Trim().ToUpper() + " " + oDatos.no_ape_materno.Trim().ToUpper());
                    linea = linea.Replace("{Servicio}", oDatos.no_servicio.ToUpper());
                    linea = linea.Replace("{TipoPlaca}", oParametros.N_Placa + ":");
                    linea = linea.Replace("{Placa}", oDatos.nu_placa.ToUpper());
                    linea = linea.Replace("{Marca}", oDatos.no_marca);
                    linea = linea.Replace("{Modelo}", oDatos.no_modelo);
                    linea = linea.Replace("{UrlPagina}", url);
                    linea = linea.Replace("{ImagenPie}", (url + ConfigurationManager.AppSettings["Pie" + oParametros.SRC_CodEmpresa + ""]));
                    strBodyHTML.Append(linea);
                }

                reader.Close();
            }
        }
        catch//(Exception ex)
        {
            strBodyHTML = new StringBuilder();
            strBodyHTML.Append("-1");
        }

        return strBodyHTML;
    }

    private string _no_error;
    public string no_error
    {
        get { return _no_error; }
        set { _no_error = value; }
    }
    static bool mailSent = false;
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        //MailMessage mail = (MailMessage)e.UserState;
        //string no_asunto = mail.Subject;
        if (e.Error != null)
        {
            //Console.WriteLine("Error {1} ocurrido mientras se enviaba el correo [{0}] ", no_asunto, e.Error.ToString());
            mailSent = false;
        }
        else if (e.Cancelled)
        {
            mailSent = false;
            //Console.WriteLine("Envio cancelado de correo con asunto [{0}].", no_asunto);
        }
        else
        {
            //Console.WriteLine("Mensaje [{1}] enviado.", no_asunto);
            mailSent = true;
        }
    }
    public bool EnviarMensajeCorreo(CitasBE oDatos, Parametros.EstadoCita oTipoCita, Parametros.PERSONA oPersona)
    {
        bool flEnvio = true;
        Int32 intResp = 0;
        mailSent = false;
        StringBuilder strHTML = new StringBuilder();

        switch (oPersona)
        {
            case Parametros.PERSONA.CLIENTE: strHTML = CargarPlantilla_Cliente(oDatos, oTipoCita); break;
            case Parametros.PERSONA.ASESOR: strHTML = CargarPlantilla_Asesor(oDatos, oTipoCita); break;
            case Parametros.PERSONA.CALL_CENTER: strHTML = CargarPlantilla_CallCenter(oDatos, oTipoCita); break;
        }

        //Validando retorno del HTML
        if (strHTML.ToString().Equals("-1") || strHTML.ToString().Equals("-2"))
        {
            //-1 Error al cargar la Plantilla
            //-2 Error al rrellenar Plantilla

            intResp = Int32.Parse(strHTML.ToString());
            flEnvio = false;
        }
        else
        {
            System.Net.Mail.MailMessage oEmail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            try
            {
                string strAsunto = string.Empty;
                switch (oTipoCita)
                {
                    case Parametros.EstadoCita.REGISTRADA: strAsunto = "SubjectCitaReserv"; break;
                    case Parametros.EstadoCita.REPROGRAMADA: strAsunto = "SubjectCitaReprog"; break;
                    case Parametros.EstadoCita.REASIGNADA: strAsunto = "SubjectCitaAsigna"; break;
                    case Parametros.EstadoCita.ANULADA: strAsunto = "SubjectCitaAnula"; break;
                }
                if (oDatos.fl_entrega.Equals("1")) strAsunto = "SubjectCitaEntrega";
                oEmail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["MailAddress"], ConfigurationManager.AppSettings["DisplayName"]);

                switch (oPersona)
                {
                    case Parametros.PERSONA.CLIENTE:
                        if (!(oDatos.no_correo.Trim().ToString().Equals(""))) { oEmail.To.Add(oDatos.no_correo.Trim()); }
                        if (!(oDatos.no_correo_trabajo.Trim().ToString().Equals(""))) { oEmail.To.Add(oDatos.no_correo_trabajo.Trim()); }
                        if (!(oDatos.no_correo_alter.Trim().ToString().Equals(""))) { oEmail.To.Add(oDatos.no_correo_alter.Trim()); }
                        break;

                    case Parametros.PERSONA.ASESOR:
                        oEmail.To.Add(oDatos.no_correo_asesor.Trim());
                        break;
                    case Parametros.PERSONA.CALL_CENTER:
                        oEmail.To.Add(oDatos.no_correo_callcenter.Trim());
                        break;
                }

                oEmail.Subject = ConfigurationManager.AppSettings[strAsunto];
                oEmail.Body = strHTML.ToString();
                oEmail.BodyEncoding = Encoding.GetEncoding("UTF-8");
                oEmail.IsBodyHtml = true;
                oEmail.Priority = System.Net.Mail.MailPriority.High;
                if (oDatos.fl_entrega.Equals("1") && oPersona == Parametros.PERSONA.CLIENTE) { oEmail.Bcc.Add("yaguirre@agildemeister.com.pe"); }/* @005 I/F  */
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UsuarioMail"].ToString(), ConfigurationManager.AppSettings["ClaveMail"].ToString());

                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback); //@001
                object userState = oEmail;
                try
                {
                    //smtp.Send(oEmail);
                    smtp.SendAsync(oEmail, userState);
                }
                catch (Exception ex)
                {
                    no_error = string.Format("{0}|{1}", ex.Message, (ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                }
                finally
                {
                    /*if (!mailSent)
                        smtp.SendAsyncCancel();*/
                    //oEmail.Dispose();
                }
                //@007-F
            }
            catch//(Exception ex)
            {
                flEnvio = false;
            }
            finally
            {
                oEmail = null;
                smtp = null;

            }
        }

        return flEnvio;
    }

    private StringBuilder Ocultar_Etiqueta(StringBuilder bodyHTML, string SPAN)
    {
        string _strTR1 = bodyHTML.ToString().Substring(bodyHTML.ToString().IndexOf("<span id='" + SPAN + "'"));
        string _strTR2 = _strTR1.Substring(0, _strTR1.IndexOf("span>") + 5);
        return bodyHTML.Replace(_strTR2, "");
    }

    private StringBuilder Ocultar_Etiqueta_2(StringBuilder bodyHTML, string TR)
    {
        string _strTR1 = bodyHTML.ToString().Substring(bodyHTML.ToString().IndexOf("<tr id='" + TR + "'"));
        string _strTR2 = _strTR1.Substring(0, _strTR1.IndexOf("tr>") + 3);
        return bodyHTML.Replace(_strTR2, "");
    }

}