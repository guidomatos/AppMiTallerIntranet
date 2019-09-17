using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;
using System.Collections.Generic;
using System.Drawing;
using ZXing.QrCode;

namespace AppMiTaller.Intranet.BL
{
    public class CitasBL
    {

        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public CitasBE Obtiene_Validacion_Km(string patente, int nid_servicio, int nid_marca)
        {
            try
            {
                return new CitasDA().Obtiene_Validacion_Km(patente, nid_servicio, nid_marca);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }
        public static System.Xml.XmlDocument WSListarCitasXML(string consultaXML)
        {
            return CitasDA.WSListarCitasXML(consultaXML);
        }
        public CitasBEList ListarBandejaCitasColaPorAsesor(CitasBE ent)
        {
            return new CitasDA().ListarBandejaCitasColaPorAsesor(ent);
        }
        public Int32 ActualizarEstadoCitaCola(CitasBE ent)
        {
            return new CitasDA().ActualizarEstadoCitaCola(ent);
        }
        public static String GenerarTickets(string nu_rut, string nu_patente, string fl_tipo, string ho_inicio, string ho_fin, string co_usuario,
             string co_usuario_red, string no_estacion_red)
        {
            return AppMiTaller.Intranet.DA.CitasDA.GenerarTickets(nu_rut, nu_patente, fl_tipo, ho_inicio, ho_fin, co_usuario, co_usuario_red, no_estacion_red);
        }
        public static String VerificarAsesorCitaDiaria(int nid_asesor)
        {
            return AppMiTaller.Intranet.DA.CitasDA.VerificarAsesorCitaDiaria(nid_asesor);
        }
        public CitasBEList ListarBandejaReporte(CitasBE ent)
        {
            return new CitasDA().ListarBandejaReporte(ent);
        }
        public CitasBEList ListarDatosSecVehiculo(CitasBE ent)
        {
            return new CitasDA().ListarDatosSecVehiculo(ent);
        }
        public CitasBEList ListarTalleresDisponibles_PorFecha(CitasBE ent)
        {
            return new CitasDA().ListarTalleresDisponibles_PorFecha(ent);
        }

        public CitasBEList ListarHorarioExcepcional_Talleres(CitasBE ent)
        {
            return new CitasDA().ListarHorarioExcepcional_Talleres(ent);
        }
        
        public CitasBEList ListarAsesoresDisponibles_PorFecha(CitasBE ent)
        {
            return new CitasDA().ListarAsesoresDisponibles_PorFecha(ent);
        }


        public CitasBEList ListarCitasAsesores(CitasBE ent)
        {
            return new CitasDA().ListarCitasAsesores(ent);
        }

        //--------------------------------------------------------------------------
        public CitasBEList ListarTalleresDisponibles_Capacidad(CitasBE ent)
        {
            return new CitasDA().ListarTalleresDisponibles_Capacidad(ent);
        }


        public Int32 VerificarFechaExceptuadaTaller(CitasBE ent)
        {
            return new CitasDA().VerificarFechaExceptuadaTaller(ent);
        }

        public Int32 VerificarFechaExceptuadaAsesor(CitasBE ent)
        {
            return new CitasDA().VerificarFechaExceptuadaAsesor(ent);
        }
        public Int32 ActualizarDatosTallerEmpresa(CitasBE ent)
        {
            return new CitasDA().ActualizarDatosTallerEmpresa(ent);
        }

        public Int32 InsertarDatosTallerEmpresa(CitasBE ent)
        {
            return new CitasDA().InsertarDatosTallerEmpresa(ent);
        }

        public CitasBEList GETListarDatosTallerEmpresaPorId(CitasBE ent)
        {
            return new CitasDA().GETListarDatosTallerEmpresaPorId(ent);
        }

        public CitasBEList GETListarDatosTallerEmpresa(CitasBE ent)
        {
            return new CitasDA().GETListarDatosTallerEmpresa(ent);
        }

        public CitasBEList GETListarDatosCita(CitasBE ent)
        {
            return new CitasDA().GETListarDatosCita(ent);
        }


        public CitasBEList GETListarHorariosDisponiblesTalleres(CitasBE ent)
        {
            return new CitasDA().GETListarHorariosDisponiblesTalleres(ent);
        }


        
        public CitasBEList GETListarHorarioAsesores(CitasBE ent)
        {
            return new CitasDA().GETListarHorarioAsesores(ent);
        }

        public CitasBEList GETListarCitasPorAsesor(CitasBE ent)
        {
            return new CitasDA().GETListarCitasPorAsesor(ent);
        }
        
        public CitasBEList ListarCitasAsesores_PorTaller(CitasBE ent)
        {
            return new CitasDA().ListarCitasAsesores_PorTaller(ent);
        }

        public CitasBEList GETListarDatosTaller(CitasBE ent)
        {
            return new CitasDA().GETListarDatosTaller(ent);
        }

        public Int32 AtenderCita(CitasBE ent)
        {
            return new CitasDA().AtenderCita(ent);
        }

        public string VerificarCitasCambiaEstado(CitasBE ent)
        {
            return new CitasDA().VerificarCitasCambiaEstado(ent);
        }
        //
        public CitasBEList VerificarCitasPedientesPlaca(CitasBE ent)
        {
            return new CitasDA().VerificarCitasPedientesPlaca(ent);
        }

        public CitasBEList GetPlacaPatente(CitasBE ent)
        {
            return new CitasDA().GetPlacaPatente(ent);
        }

        public string GETFechaActual()
        {
            return DateTime.Now.ToString();
        }
        public CitasBEList GETListarDatosVehiculoClientePorPlaca(CitasBE ent)
        {
            return new CitasDA().GETListarDatosVehiculoClientePorPlaca(ent);
        }
        public CitasBEList GETListarMarcas(CitasBE ent)
        {
            return new CitasDA().GETListarMarcas(ent);
        }
        public CitasBEList GETListarModelosPorMarca(CitasBE ent)
        {
            return new CitasDA().GETListarModelosPorMarca(ent);
        }

        public CitasBEList GETListarTipoDocumentos()
        {
            return new CitasDA().GETListarTipoDocumentos();
        }

        public CitasBEList GETListarDatosContactoPorDoc(CitasBE ent)
        {
            return new CitasDA().GETListarDatosContactoPorDoc(ent);
        }

        public Int32 ConfirmarCita(CitasBE ent)
        {
            return new CitasDA().ConfirmarCita(ent);
        }

        public Int32 Reprogramar(CitasBE ent)
        {
            return new CitasDA().Reprogramar(ent);
        }

        public Int32 GetCantidadColaEspera(CitasBE ent)
        {
            return new CitasDA().GetCantidadColaEspera(ent);
        }

        public Int32 AnularCita(CitasBE ent)
        {
            return new CitasDA().AnularCita(ent);
        }

        public CitasBEList GETListarClientesEnColaEspera(CitasBE ent)
        {
            return new CitasDA().GETListarClientesEnColaEspera(ent);
        }

        public string AsignarClienteColaEspera(CitasBE ent)
        {
            return new CitasDA().AsignarClienteColaEspera(ent);
        }

        public CitasBEList GetBuscarPlacaVehiculo(CitasBE ent)
        {
            return new CitasDA().GetBuscarPlacaVehiculo(ent);
        }

        public string VerificarEstadoCita(CitasBE ent)
        {
            return new CitasDA().VerificarEstadoCita(ent);
        }

        public string ReservarCitaBO(CitasBE ent)
        {
            return new CitasDA().ReservarCitaBO(ent);
        }

        public CitasBEList GetListar_Ubigeos_Disponibles(CitasBE ent)
        {
            return new CitasDA().GetListar_Ubigeos_Disponibles(ent);
        }
        
        public CitasBEList Listar_PuntosRed(CitasBE ent)
        {
            return new CitasDA().Listar_PuntosRed(ent);
        }

        public CitasBEList Listar_Talleres(CitasBE ent)
        {
            return new CitasDA().Listar_Talleres(ent);
        }

        public CitasBEList Listar_HorarioRecordatorio()
        {
            return new CitasDA().Listar_HorarioRecordatorio();
        }

        public Int32 Verificar_Capacidad_Taller(CitasBE ent)
        {
            return new CitasDA().Verificar_Capacidad_Taller(ent);
        }

        public Int32 Verificar_Capacidad_Asesor(CitasBE ent)
        {
            return new CitasDA().Verificar_Capacidad_Asesor(ent);
        }

        public CitasBEList Listar_Horario_Excepcional_Taller(CitasBE ent)
        {
            return new CitasDA().Listar_Horario_Excepcional_Taller(ent);
        }

        public Int32 ConfirmarCitaPorCorreo(CitasBE ent)
        {
            return new CitasDA().ConfirmarCitaPorCorreo(ent);
        }     

        public CitasBE Listar_HistorialCitasPorVehiculo(CitasBE ent)
        {
            return new CitasDA().Listar_HistorialCitasPorVehiculo(ent);
        }

        public CitasBE Listar_HistorialServiciosPorVehiculo(CitasBE ent)
        {
            return new CitasDA().Listar_HistorialServiciosPorVehiculo(ent);
        }
        public string GetNombreImagenQR(string placa)
        {
            String imgQRCode = String.Concat("img_QR_", placa, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".png");

            return imgQRCode;
        }

        public string GetNombreImagenQRWithText(string imagen)
        {
            return imagen.Replace("_QR_", "_QRTEXT_");
        }

        public string SaveImageQRText(string rutaguarda, string nombreimagen, string placa, DateTime fecha, string hora, string marca, string modelo, string color, bool saveQRPNG)
        {
            int proporcionqr = 100;
            int anchotexto = 200;
            int anchototal = anchotexto + proporcionqr;
            int alturatexto = 20;
            string nombreimagentext = GetNombreImagenQRWithText(nombreimagen);
            Bitmap QR = GetBitMapQR(placa, proporcionqr);
            string rutaQRCode = "";
            if (saveQRPNG)
            {
                rutaQRCode = rutaguarda + nombreimagen;
                QR.Save(@rutaQRCode, System.Drawing.Imaging.ImageFormat.Png);
            }
            Bitmap Text = GetBitMapText(anchotexto, proporcionqr, fecha, hora, placa, marca, modelo, color);

            rutaQRCode = rutaguarda + nombreimagentext;
            Bitmap QRText = GetBitMapQRText(anchotexto,proporcionqr,alturatexto,QR,Text);
            QRText.Save(@rutaQRCode, System.Drawing.Imaging.ImageFormat.Png);

            return nombreimagen;
        }

        public Bitmap GetBitMapQR(string placa,  int proporcionqr)
        {
            QRCodeWriter qr = new QRCodeWriter();
            IDictionary<ZXing.EncodeHintType, object> hints = new Dictionary<ZXing.EncodeHintType, object>();
            hints.Add(ZXing.EncodeHintType.CHARACTER_SET, "UTF-8");
            hints.Add(ZXing.EncodeHintType.MARGIN, 0);
            string strMensaje = placa;
            var matrix = qr.encode(strMensaje, ZXing.BarcodeFormat.QR_CODE, proporcionqr, proporcionqr, hints);
            ZXing.BarcodeWriter w = new ZXing.BarcodeWriter();
            w.Format = ZXing.BarcodeFormat.QR_CODE;
            System.Drawing.Bitmap imgQR = w.Write(matrix);

            return imgQR;
        }

        public Bitmap GetBitMapText(int anchotexto,int proporcionqr,DateTime fecha, string hora, string placa, string marca, string modelo, string color)
        {
            int fuente = 9;
            Font drawFont = new Font("Arial", fuente, FontStyle.Regular);
            System.Drawing.Bitmap imgTxt = new Bitmap(anchotexto, proporcionqr);
            string fechaformateada = fecha.ToString("dd/MM/yyyy");
            string texto = ""
                            + MySubstring(
                //"Placa: " + 
                                        placa) + "\n"
                            + MySubstring(
                //"Marca: " + 
                                        marca) + "\n"
                            + MySubstring(
                //"Modelo: "+ 
                                        modelo) + " \n"
                            + MySubstring(
                //"Color: " + 
                                        color) + "\n"
                            + MySubstring(fechaformateada + " " + FormatoHora(hora));
            RectangleF rectf = new RectangleF(0, 0, anchotexto, proporcionqr);
            Graphics g = Graphics.FromImage(imgTxt);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.DrawString(texto, drawFont, Brushes.Black, rectf);
            g.Flush();

            return imgTxt;
        }

        public Bitmap GetBitMapQRText(int anchotexto,int proporcionqr, int alturatexto, Bitmap QR, Bitmap Text)
        {
            System.Drawing.Bitmap imgBase = new System.Drawing.Bitmap(anchotexto, proporcionqr);
            //get a graphics object from the image so we can draw on it
            using (System.Drawing.Graphics g2 = System.Drawing.Graphics.FromImage(imgBase))
            {
                //set background color
                g2.Clear(System.Drawing.Color.White);

                g2.DrawImage(QR, new System.Drawing.Rectangle(0, 0, proporcionqr, proporcionqr));
                g2.DrawImage(Text, new System.Drawing.Rectangle(proporcionqr, alturatexto, anchotexto, proporcionqr));

                return imgBase;
            }
        }
        public static string MySubstring(string texto)
        {

            int corte = 17;
            if (texto.Length > corte)
            {
                return texto.Substring(0, corte);
            }
            else
            {
                return texto;
            }
        }

        public string FormatoHora(string strHora)
        {
            string strHF = strHora;
            try
            {
                string strHoraF = Convert.ToDateTime(strHora).ToString("hh:mm");
                Int32 strHoraF1 = Convert.ToInt32(strHora.Replace(":", ""));
                strHF = strHoraF + ((strHoraF1 < 1200) ? " a.m." : " p.m.");
            }
            catch (Exception)
            {
                strHF = strHora;
            }
            return strHF;
        }
    }
}
