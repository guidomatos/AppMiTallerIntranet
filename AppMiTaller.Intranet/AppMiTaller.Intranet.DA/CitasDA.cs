using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppMiTaller.Intranet.BE;
using System.Data.SqlClient;
using System.Xml;

/*
 * @001: ECH,  CH,  15-08-2013,  Gestion de Colas
 * @002: ECH,  PE,  02-12-2013,  validar si el vin tiene un recall
 * @003: ECH,  PE,  02-12-2013,  agregar campo fl_taxi
 * @004: ARC, 07/08/2014, MOTIVO DE CAMBIO: Ajuste RFC
 * @005: MBR, 16/02/2016, MOTIVO DE CAMBIO: Validación Km
 * @006: YKVR, 21/04/2016, MOTIVO DE CAMBIO: Se agrego flag de validacion si el cliente si es RUC: Validado por Sunat y si es DNI: Validado por RENIEC
 * @007: YKVR, 02/06/2016, MOTIVO DE CAMBIO: Se agrego el parametro de entrada cod_tipo_documento a la función GETListarDatosContactoPorDoc.
 * @008: FPS, 01/03/2017, MOTIVO DE CAMBIO: Ajustes SRC Responsivo.
 * @009: MMP, 28/12/2017, MOTIVO DE CAMBIO: Se graba el nombre del archivo QR
 * @010: MMP, 22/02/2018, MOTIVO DE CAMBIO: Se agrego el campo de co_tipo_cita 
 * @011: MMP, 09/03/2018, MOTIVO DE CAMBIO: Se agrega el campo de fl_recojounidad
 * @012: RJRA 26/03/2018 Permite dar como atendida a una cita con estado no asistio (Solo para el asesor asignado y del dia actual)
 * @013: MANTRA 02/04/2018 Proyecto Calidad de Datos de Clientes
 */

namespace AppMiTaller.Intranet.DA
{
    public class CitasDA
    {
        Int32 nid_usuario; //@004 I/F
        
        public static XmlDocument WSListarCitasXML(string consultaXML)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[SRC_CONSULTAS_CITAS_XML_BO]", conn);
            XmlDocument xd = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@consultaXML", consultaXML);

            try
            {
                conn.Open();
                using (System.Xml.XmlReader CitasXML = cmd.ExecuteXmlReader())
                {
                    if (CitasXML.Read())
                    {
                        xd = new XmlDocument();
                        xd.Load(CitasXML);
                        conn.Close();
                        return xd;
                    }
                    CitasXML.Close();
                }
            }
            catch (Exception)
            {

                //if (CitasXML != null && !CitasXML.IsClose) CitasXML.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return xd;
        }
        public CitasBEList ListarBandejaCitasColaPorAsesor(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            CitasBE Entidad;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[SRC_SPS_BANDEJA_CITAS_COLA]", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_patente", ent.nu_patente);
            cmd.Parameters.AddWithValue("@vi_nid_marca", ent.nid_marca);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@vi_nid_asesor", ent.nid_asesor);
            cmd.Parameters.AddWithValue("@vi_fl_tipo", ent.fl_tipo_cita);
            cmd.Parameters.AddWithValue("@vi_ho_inicio", ent.ho_inicio_c);
            cmd.Parameters.AddWithValue("@vi_ho_fin", ent.ho_fin_c);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entidad = new CitasBE();
                    Entidad.nid_cita = int.Parse(reader["nid_cita"].ToString());
                    Entidad.ho_inicio = reader["ho_inicio"].ToString();
                    Entidad.no_cliente = reader["no_cliente"].ToString();
                    Entidad.nu_patente = reader["nu_patente"].ToString();
                    Entidad.no_marca = reader["no_marca"].ToString();
                    Entidad.no_modelo = reader["no_modelo"].ToString();
                    Entidad.co_estado_cita = reader["co_estado"].ToString();
                    Entidad.no_estado_cita = reader["no_estado"].ToString();
                    Entidad.nu_ticket = reader["nu_ticket"].ToString();
                    lista.Add(Entidad);
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }
        public Int32 ActualizarEstadoCitaCola(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPU_ESTADO_CITAS_COLA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_co_estado", ent.co_estado_cita);
            cmd.Parameters.AddWithValue("@vi_fl_tipo_cita", ent.fl_tipo_cita);
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    rpta = 1;
            }
            catch //(Exception ex)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }
        public static String GenerarTickets(string nu_rut, string nu_patente, string fl_tipo, string ho_inicio, string ho_fin, string co_usuario,
             string co_usuario_red, string no_estacion_red)
        {
            String rpta = String.Empty;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_GENERAR_TICKET", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nu_rut", nu_rut);
            cmd.Parameters.AddWithValue("@vi_nu_patente", nu_patente);
            cmd.Parameters.AddWithValue("@vi_fl_tipo", fl_tipo);
            cmd.Parameters.AddWithValue("@vi_ho_inicio", ho_inicio);
            cmd.Parameters.AddWithValue("@vi_ho_fin", ho_fin);
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", co_usuario);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", no_estacion_red);
            cmd.Parameters.Add("@vo_mensaje", SqlDbType.VarChar, 300).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                int iResp = cmd.ExecuteNonQuery();
                rpta = cmd.Parameters["@vo_mensaje"].Value.ToString();

            }
            catch //(Exception ex)
            {
                rpta = String.Empty;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }
        public static String VerificarAsesorCitaDiaria(int nid_asesor)
        {
            string rpta = string.Empty;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_ASESOR_PERMISO_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nid_asesor", nid_asesor);

            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();

            }
            catch (Exception)
            {
                rpta = string.Empty;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }
        public CitasBEList ListarBandejaReporte(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            CitasBE Entidad;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_REPORTE_CITAS_COLA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_fe_ini", ent.fecha_ini);
            cmd.Parameters.AddWithValue("@vi_fe_fin", ent.fecha_fin);
            cmd.Parameters.AddWithValue("@vi_fl_tipo", ent.fl_tipo_cita);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entidad = new CitasBE();
                    //Entidad.nid_cita = int.Parse(reader["nid_cita"].ToString());
                    Entidad.no_dpto = reader["no_dpto"].ToString();
                    Entidad.no_prov = reader["no_prov"].ToString();
                    Entidad.no_dist = reader["no_dist"].ToString();
                    Entidad.no_ubica = reader["no_ubica"].ToString();
                    Entidad.no_taller = reader["no_taller"].ToString();
                    Entidad.no_asesor = reader["no_asesor"].ToString();
                    Entidad.cod_reserva_cita = reader["co_reserva"].ToString();
                    Entidad.fe_hora_registro = reader["fe_hora_registro"].ToString();
                    Entidad.fe_hora_cita = reader["fe_hora_cita"].ToString();
                    Entidad.fl_reprogramado = reader["fl_reprogramado"].ToString();
                    Entidad.fe_hora_cita_orig = reader["fe_hora_cita_orig"].ToString();
                    Entidad.no_cliente = reader["no_cliente"].ToString();
                    Entidad.nu_telefono = reader["nu_telefono"].ToString();
                    Entidad.nu_placa = reader["nu_placa"].ToString();
                    Entidad.no_marca = reader["no_marca"].ToString();
                    Entidad.no_modelo = reader["no_modelo"].ToString();
                    Entidad.no_tipo_servicio = reader["no_tipo_servicio"].ToString();
                    Entidad.no_servicio = reader["no_servicio"].ToString();
                    Entidad.tx_observacion = reader["tx_observacion"].ToString();
                    Entidad.no_origen = reader["no_origen"].ToString();
                    Entidad.co_usuario_crea = reader["co_usuario_crea"].ToString();
                    Entidad.no_estado = reader["no_estado"].ToString();
                    Entidad.nu_ticket_ate = reader["nu_ticket_ate"].ToString();
                    Entidad.fe_hora_ticket_a = reader["fe_hora_ticket_a"].ToString();
                    Entidad.fe_hora_espera_a = reader["fe_hora_espera_a"].ToString();
                    Entidad.fe_hora_llamada_a = reader["fe_hora_llamada_a"].ToString();
                    Entidad.fe_hora_atencion_a = reader["fe_hora_atencion_a"].ToString();
                    Entidad.fe_hora_recepcion_a = reader["fe_hora_recepcion_a"].ToString();
                    Entidad.nu_ticket_ent = reader["nu_ticket_ent"].ToString();
                    Entidad.fe_hora_ticket_e = reader["fe_hora_ticket_e"].ToString();
                    Entidad.fe_hora_espera_e = reader["fe_hora_espera_e"].ToString();
                    Entidad.fe_hora_llamada_e = reader["fe_hora_llamada_e"].ToString();
                    Entidad.fe_hora_atencion_e = reader["fe_hora_atencion_e"].ToString();
                    Entidad.fe_hora_entrega_e = reader["fe_hora_entrega_e"].ToString();


                    lista.Add(Entidad);
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }
        //@001-F

        //I @005
        public CitasBE Obtiene_Validacion_Km(string patente, int nid_servicio, int nid_marca)
        {
            CitasBE oHistorial = new CitasBE();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_OBTENER_KM_ULTIMAOT", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_patente", patente);
            cmd.Parameters.AddWithValue("@vi_nid_servicio", nid_servicio);
            cmd.Parameters.AddWithValue("@vi_nid_marca", nid_marca);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    oHistorial = Entidad_Validacion_Km(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return oHistorial;
        }
        private CitasBE Entidad_Validacion_Km(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("tx_alternativo_01");
            Entidad.tx_alternativo_01 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("tx_alternativo_02");
            Entidad.tx_alternativo_02 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));


            return Entidad;
        }
        public CitasBEList ListarDatosSecVehiculo(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[SRC_SPS_DATOS_SEC_VEHICULO_BO]", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_vin", ent.Nu_vin);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarDatosSecVehiculo(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList ListarTalleresDisponibles_PorFecha(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_TALLERES_DISPONIBLES_FECHA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@VI_CODDPTO", ent.coddpto);
            cmd.Parameters.AddWithValue("@VI_CODPROV", ent.codprov);
            cmd.Parameters.AddWithValue("@VI_CODDIST", ent.coddis);
            cmd.Parameters.AddWithValue("@VI_NID_UBICA", ent.nid_ubica);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_FECHA", ent.fe_atencion);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarTalleresDisponibles_PorFecha(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList ListarHorarioExcepcional_Talleres(CitasBE ent)
        {
            //CitasBEList lista = new CitasBEList();
            //IDataReader DReader = null;
            //try
            //{
            //    using (Database db = new Database())
            //    {
            //        db.ProcedureName = "[SRC_SPS_HORARIO_EXCEPCIONAL_TALLERES_BO]";

            //        db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
            //        db.AddParameter("@vi_fecha", DbType.DateTime, ParameterDirection.Input, ent.fe_atencion);
            //        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.dd_atencion);

            //        DReader = db.GetDataReader();

            //    }
            //    while (DReader.Read())
            //    {
            //        CitasBE oCitasBE = Entidad_ListarHorarioExcepcional_Talleres(DReader);
            //        lista.Add(oCitasBE);
            //    }
            //    DReader.Close();
            //}
            //catch (Exception)
            //{
            //    if (DReader != null && !DReader.IsClosed) DReader.Close();
            //    throw;
            //}
            //return lista;

            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_HORARIO_EXCEPCIONAL_TALLERES_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_fecha", ent.fe_atencion);
            cmd.Parameters.AddWithValue("@vi_dd_atencion", ent.dd_atencion);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarHorarioExcepcional_Talleres(reader));
                }

                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList ListarAsesoresDisponibles_PorFecha(CitasBE ent)
        {

            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_ASESORES_DISPONIBLES_FECHA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_fecha", ent.fe_atencion);
            cmd.Parameters.AddWithValue("@vi_dd_atencion", ent.dd_atencion);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarAsesoresDisponibles_PorFecha(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList ListarCitasAsesores(CitasBE ent)
        {

            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_CITAS_ASESORES_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_fecha", ent.fe_atencion);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarCitasAsesores(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }


        public CitasBEList ListarTalleresDisponibles_Capacidad(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_TALLERES_CAPACIDAD_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@VI_CODDPTO", ent.coddpto);
            cmd.Parameters.AddWithValue("@VI_CODPROV", ent.codprov);
            cmd.Parameters.AddWithValue("@VI_CODDIST", ent.coddis);
            cmd.Parameters.AddWithValue("@VI_NID_UBICA", ent.nid_ubica);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_FECHA", ent.fe_atencion);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarTalleresDisponibles_Capacidad(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList ListarCitasAsesores_PorTaller(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_CITAS_ASESORES_POR_TALLER_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_FECHACITA", ent.fe_prog);
            cmd.Parameters.AddWithValue("@VI_NID_SERVICIO", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@VI_NID_TALLER", ent.nid_taller);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarCitasAsesores_PorTaller(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public Int32 VerificarFechaExceptuadaTaller(CitasBE ent)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_FECHAEXCEPTUADA_TALLER_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_fecha", ent.fe_prog);

            Int32 count = 0;
            try
            {
                conn.Open();
                count = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch //(Exception ex)
            {
                count = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return count;
        }

        public Int32 VerificarFechaExceptuadaAsesor(CitasBE ent)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_FECHAEXCEPTUADA_ASESOR_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@vi_fecha", ent.fe_prog);

            Int32 count = 0;
            try
            {
                conn.Open();
                count = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch //(Exception ex)
            {
                count = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return count;
        }

        public Int32 ActualizarDatosTallerEmpresa(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPU_DATOS_TALLER_EMPRESA_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nid_taller_empresa", ent.nid_taller_empresa);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_empresa", ent.Nid_empresa);
            cmd.Parameters.AddWithValue("@vi_no_banco", ent.no_banco);
            cmd.Parameters.AddWithValue("@vi_nu_cuenta", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_no_correo_callcenter", ent.no_correo_callcenter);
            cmd.Parameters.AddWithValue("@vi_nu_callcenter", ent.nu_callcenter);
            cmd.Parameters.AddWithValue("@vi_co_usuario_modi", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    rpta = 1;
            }
            catch //(Exception ex)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public Int32 InsertarDatosTallerEmpresa(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_DATOS_TALLER_EMPRESA_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_empresa", ent.Nid_empresa);
            cmd.Parameters.AddWithValue("@vi_no_banco", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_nu_cuenta", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_no_correo_callcenter", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_nu_callcenter", ent.nu_callcenter);
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    rpta = 1;
            }
            catch //(Exception ex)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public CitasBEList GETListarDatosTallerEmpresaPorId(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_DATOS_TALLER_EMPRESA_ID_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_NID_TALLER", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_NID_EMPRESA", ent.Nid_empresa);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita29(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarDatosTallerEmpresa(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_DATOS_TALLER_EMPRESA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_empresa", ent.Nid_empresa);
            cmd.Parameters.AddWithValue("@vi_no_banco", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_nu_cuenta", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_no_correo_callcenter", ent.nu_cuenta);
            cmd.Parameters.AddWithValue("@vi_nu_callcenter", ent.nu_callcenter);
            cmd.Parameters.AddWithValue("@vi_fl_activo", ent.fl_activo);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita30(reader));
                }

                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarDatosCita(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_CITA_POR_DATOS_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_cod_resverva_cita", ent.cod_reserva_cita);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita28(reader));
                }
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarHorariosDisponiblesTalleres(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_HORARIO_DISPONIBLE_TALLERES_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@VI_CODDPTO", ent.coddpto);
            cmd.Parameters.AddWithValue("@VI_CODPROV", ent.codprov);
            cmd.Parameters.AddWithValue("@VI_CODDIST", ent.coddis);
            cmd.Parameters.AddWithValue("@VI_NID_UBICA", ent.nid_ubica);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_FECHA", ent.fe_atencion);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita27(reader));
                }
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarHorarioAsesores(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_HORARIO_ASESORES_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_FechaCita", ent.fe_prog);
            cmd.Parameters.AddWithValue("@vi_dia", ent.dia_prog);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita24(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarCitasPorAsesor(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_CITAS_RESERVADAS_ASESOR_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_FECHACITA", ent.fe_prog);
            cmd.Parameters.AddWithValue("@VI_NID_TALLER", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita25(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarDatosTaller(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_DATOS_TALLER_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_NID_TALLER", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita26(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public Int32 AtenderCita(CitasBE ent)
        {
            int rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_ATENDER_CITA_BO", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            if (String.IsNullOrEmpty(ent.nu_placa))
                cmd.Parameters.AddWithValue("@vi_nu_placa", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@vi_nu_placa", ent.nu_placa);
            cmd.Parameters.AddWithValue("@vi_fl_puntualidad", ent.Fl_puntualidad);
            if (ent.qt_km_inicial == 0)
                cmd.Parameters.AddWithValue("@vi_qt_km_inicial", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@vi_qt_km_inicial", ent.qt_km_inicial);
            cmd.Parameters.AddWithValue("@vi_tx_glosa_atencion", ent.tx_glos);
            cmd.Parameters.AddWithValue("@vi_co_usuario_modi", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    rpta = 1;
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public string VerificarCitasCambiaEstado(CitasBE ent)
        {
            string rpta = string.Empty;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_CITAS_CAMBIA_ESTADO_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_estado", ent.nid_Estado);
            cmd.Parameters.AddWithValue("@vi_tex_verifica", ent.tex_verifica);
            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                rpta = string.Empty;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public CitasBEList VerificarCitasPedientesPlaca(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[SRC_SPS_VERIFICAR_CITAS_PENDIENTES_PLACA_BO]", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_placa", ent.nu_placa);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_Listar_Citas_Pendiente(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GetPlacaPatente(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_PLACA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_placa", ent.nu_placa);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita16(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }
        public CitasBEList GETListarDatosVehiculoClientePorPlaca(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_DATOS_VEHICULO_CLIENTE_POR_PLACA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_placa", ent.nu_placa);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarDatosVehiculoClientePorPlaca(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarMarcas(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_MARCAS_POR_USUARIO_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita2(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarModelosPorMarca(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_MODELOS_POR_MARCA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@vi_nid_marca", ent.nid_marca);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita3(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarTipoDocumentos()
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_TIPOS_DOCUMENTOS_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita9(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarDatosContactoPorDoc(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_DATOSCONTACTO_POR_DOC_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_documento", ent.nu_documento);
            cmd.Parameters.AddWithValue("@vi_co_tipo_documento", ent.cod_tipo_documento);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListarDatosContactoPorDoc(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList GETListarClientesEnColaEspera(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_CLIENTES_COLAESPERA_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita11(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public Int32 Reprogramar(CitasBE ent)
        {
            int rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_REPROGRAMAR_CITA_BO", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_fe_reprogramacion", ent.fe_prog);
            cmd.Parameters.AddWithValue("@vi_ho_inicio", ent.ho_inicio);
            cmd.Parameters.AddWithValue("@vi_ho_fin", ent.ho_fin);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_estado_ant", ent.nid_Estado);
            cmd.Parameters.AddWithValue("@vi_tx_observacion", ent.tx_observacion);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            //-------------------------
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            cmd.Parameters.AddWithValue("@vi_no_nombreqr", ent.no_nombreqr); //@009
            try
            {
                conn.Open();
                rpta = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public Int32 GetCantidadColaEspera(CitasBE ent)
        {
            int rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_CANTIDAD_COLAESPERA_BO", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@vi_ho_inicio", ent.ho_inicio);
            cmd.Parameters.AddWithValue("@vi_ho_fin", ent.ho_fin);
            cmd.Parameters.AddWithValue("@vi_fe_programada", ent.fe_prog);

            try
            {
                conn.Open();
                rpta = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public Int32 ConfirmarCita(CitasBE ent)
        {
            int rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_CONFIRMAR_CITA_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_nid_estado_ant", ent.nid_Estado);
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                rpta = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }
        public Int32 AnularCita(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_ANULAR_CITA_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_nid_estado_ant", ent.nid_Estado);
            //-------------------------
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                rpta = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public string AsignarClienteColaEspera(CitasBE ent)
        {
            string rpta = string.Empty;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPU_ASIGNAR_CLIENTE_CITA_COLAESPERA_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_no_pais", ent.No_pais);
            cmd.Parameters.AddWithValue("@vi_nid_asesor", ent.nid_asesor);
            //-------------------------
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                rpta = string.Empty;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public CitasBEList GetBuscarPlacaVehiculo(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_BUSCAR_VEHICULO_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nu_placa", ent.nu_placa);
            cmd.Parameters.AddWithValue("@vi_nid_marca", ent.nid_marca);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@vi_nu_vin", ent.Nu_vin);
            cmd.Parameters.AddWithValue("@vi_tipo", ent.Tipo);
            cmd.Parameters.AddWithValue("@vi_ape_paterno", ent.no_ape_paterno);
            cmd.Parameters.AddWithValue("@vi_ape_materno", ent.no_ape_materno);

            if (String.IsNullOrEmpty(ent.no_nombre))
            {
                if (!String.IsNullOrEmpty(ent.no_razon_social))
                    cmd.Parameters.AddWithValue("@vi_nombres", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@vi_nombres", ent.no_nombre);
            }
            else
            {
                cmd.Parameters.AddWithValue("@vi_nombres", ent.no_nombre);
            }
            if (String.IsNullOrEmpty(ent.no_razon_social))
            {
                if (!String.IsNullOrEmpty(ent.no_razon_social))
                    cmd.Parameters.AddWithValue("@vi_razon_social", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@vi_razon_social", ent.no_razon_social);
            }
            else
            {
                cmd.Parameters.AddWithValue("@vi_razon_social", ent.no_razon_social);
            }
            cmd.Parameters.AddWithValue("@vi_co_tipo_documento", ent.cod_tipo_documento);
            cmd.Parameters.AddWithValue("@vi_nu_documento", ent.nu_documento);
            cmd.Parameters.AddWithValue("@vi_nid_empresa", ent.Nid_empresa);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita17(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }
        public string VerificarEstadoCita(CitasBE ent)
        {
            string rpta = string.Empty;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_ESTADO_CITA_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_nid_asesor", ent.nid_asesor); //@012 I/F

            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();

            }
            catch (Exception)
            {
                rpta = string.Empty;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public string ReservarCitaBO(CitasBE ent)
        {
            string rpta = string.Empty;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_RESERVAR_CITA_WEB_BO", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_contacto", ent.nid_contacto_sr);
            cmd.Parameters.AddWithValue("@vi_nid_vehiculo", ent.nid_vehiculo);
            cmd.Parameters.AddWithValue("@vi_co_tipo_documento", ent.cod_tipo_documento);
            cmd.Parameters.AddWithValue("@vi_nu_documento", ent.nu_documento);
            cmd.Parameters.AddWithValue("@vi_no_cliente", ent.no_nombre);
            cmd.Parameters.AddWithValue("@vi_no_ape_pat", ent.no_ape_paterno);
            cmd.Parameters.AddWithValue("@vi_no_ape_mat", ent.no_ape_materno);
            cmd.Parameters.AddWithValue("@vi_nu_telefono", ent.nu_telefono);
            cmd.Parameters.AddWithValue("@vi_nu_tel_oficina", ent.nu_tel_oficina);
            cmd.Parameters.AddWithValue("@vi_nu_celular", ent.nu_celular);
            cmd.Parameters.AddWithValue("@vi_nu_celular_alter", ent.nu_celular_alter);
            cmd.Parameters.AddWithValue("@vi_no_correo", ent.no_correo);
            cmd.Parameters.AddWithValue("@vi_no_correo_trab", ent.no_correo_trabajo);
            cmd.Parameters.AddWithValue("@vi_no_correo_alter", ent.no_correo_alter);
            cmd.Parameters.AddWithValue("@vi_no_pais", ent.No_pais);
            cmd.Parameters.AddWithValue("@vi_nid_taller", ent.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@vi_nid_servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nu_placa", ent.nu_placa);
            cmd.Parameters.AddWithValue("@vi_nid_marca", ent.nid_marca);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@vi_fe_programada", ent.fe_prog);
            cmd.Parameters.AddWithValue("@vi_fl_origen", ent.fl_origen);
            cmd.Parameters.AddWithValue("@vi_ho_inicio", ent.ho_inicio);
            cmd.Parameters.AddWithValue("@vi_ho_fin", ent.ho_fin);
            cmd.Parameters.AddWithValue("@vi_fl_datos_pend", ent.fl_datos_pend);
            cmd.Parameters.AddWithValue("@vi_tx_observacion", ent.tx_observacion);
            cmd.Parameters.AddWithValue("@vi_tipo_reg", ent.Tipo_reg);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            //-------
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_no_usuario_red", ent.no_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);

            cmd.Parameters.AddWithValue("@vi_fl_taxi", ent.fl_taxi);
            cmd.Parameters.AddWithValue("@vi_no_nombreqr", ent.no_nombreqr);
            cmd.Parameters.AddWithValue("@co_tipo_cita", ent.co_tipo_cita);
            cmd.Parameters.AddWithValue("@fl_recojounidad", ent.fl_recojounidad);

            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                rpta = string.Empty;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public CitasBEList GetListar_Ubigeos_Disponibles(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_UBIGEOS_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita18(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList Listar_PuntosRed(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_PUNTOS_RED_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_Servicio", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@VI_CODDPTO", ent.coddpto);
            cmd.Parameters.AddWithValue("@VI_CODPROV", ent.codprov);
            cmd.Parameters.AddWithValue("@VI_CODDIST", ent.coddis);
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita20(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList Listar_Talleres(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_TALLERES_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_NID_SERVICIO", ent.nid_Servicio);
            cmd.Parameters.AddWithValue("@VI_NID_MODELO", ent.nid_modelo);
            cmd.Parameters.AddWithValue("@VI_NID_UBICA", ent.nid_ubica);
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);
            
            cmd.Parameters.AddWithValue("@vi_coddpto", ent.coddpto);
            cmd.Parameters.AddWithValue("@vi_codprov", ent.codprov);
            cmd.Parameters.AddWithValue("@vi_coddist", ent.coddis);
            
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita21(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public CitasBEList Listar_HorarioRecordatorio()
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_HORARIO_RECORDATORIO_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita22(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public Int32 Verificar_Capacidad_Taller(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_CAPACIDAD_TALLER_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_NID_TALLER", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_FECHA", ent.fe_prog);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            try
            {
                conn.Open();
                rpta = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public Int32 Verificar_Capacidad_Asesor(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_VERIFICAR_CAPACIDAD_ASESOR_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_NID_USUARIO", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@VI_FECHA", ent.fe_prog);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            try
            {
                conn.Open();
                rpta = Int32.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }

        public CitasBEList Listar_Horario_Excepcional_Taller(CitasBE ent)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_HORARIO_EXCEPCIONAL_TALLER_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VI_NID_TALLER", ent.nid_taller);
            cmd.Parameters.AddWithValue("@VI_FECHA", ent.fe_prog);
            cmd.Parameters.AddWithValue("@VI_DD_ATENCION", ent.dd_atencion);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidadCita23(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }

        public Int32 ConfirmarCitaPorCorreo(CitasBE ent)
        {
            Int32 nId = 0;

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_CONFIRMAR_CITAS_POR_CORREO_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nid_cita", ent.nid_cita);
            cmd.Parameters.AddWithValue("@vi_co_estado_cita", ent.co_estado_cita);
            if (String.IsNullOrEmpty(ent.co_usuario_crea))
                cmd.Parameters.AddWithValue("@vi_co_usuario_crea", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);

            if (String.IsNullOrEmpty(ent.co_usuario_red))
                cmd.Parameters.AddWithValue("@vi_co_usuario_red", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);

            if (String.IsNullOrEmpty(ent.no_estacion_red))
                cmd.Parameters.AddWithValue("@vi_no_estacion_red", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);

            if (String.IsNullOrEmpty(ent.fl_activo))
                cmd.Parameters.AddWithValue("@vi_fl_activo", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@vi_fl_activo", ent.fl_activo);

            try
            {
                conn.Open();
                nId = cmd.ExecuteNonQuery();
            }
            catch //(Exception ex)
            {
                nId = -1;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return nId;
        }
        public CitasBE Listar_HistorialServiciosPorVehiculo(CitasBE ent)
        {
            CitasBE oHistorial = new CitasBE();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_GET_DATOS_VEH_PROP", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nu_placa", ent.nu_placa);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    oHistorial = Entidad_HistorialServiciosPorVehiculo(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return oHistorial;
        }
        private CitasBE Entidad_HistorialServiciosPorVehiculo(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_cliente");
            Entidad.co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("doc_cliente");
            Entidad.doc_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        public CitasBE Listar_HistorialCitasPorVehiculo(CitasBE ent)
        {
            nid_usuario = Convert.ToInt32(ent.nid_usuario);
            CitasBE oHistorial = new CitasBE();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_GET_DATOS_VEH_PROP", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nu_placa", ent.nu_placa);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    oHistorial = Entidad_HistorialCitasPorVehiculo(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return oHistorial;
        }
        private CitasBE Entidad_HistorialCitasPorVehiculo(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_cliente");
            Entidad.co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("doc_cliente");
            Entidad.doc_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));


            Entidad.lstcitas = GetListaDetalleHistorialCitas(Entidad.nu_placa);

            return Entidad;
        }
        private CitasBEList GetListaDetalleHistorialCitas(String pPlaca)
        {
            CitasBEList lista = new CitasBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_GET_HIST_CITAS_X_VEH", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nu_placa", pPlaca);
            cmd.Parameters.AddWithValue("@nid_usuario", nid_usuario); //@004 I/F
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(Entidad_ListaDetalleHistorialCitas(reader));
                }
                reader.Close();
            }
            catch (Exception)
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return lista;
        }
        private CitasBE Entidad_ListaDetalleHistorialCitas(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;

            indice = DReader.GetOrdinal("item");
            Entidad.Itm = (DReader.IsDBNull(indice) ? "" : Convert.ToString(DReader.GetInt32(indice)));
            indice = DReader.GetOrdinal("nid_cita");
            Entidad.nid_cita = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_ubica_corto");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.no_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_km_inicial");
            Entidad.km_ult_serv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_asesor");
            Entidad.AsesorServ = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fe_programada");
            Entidad.fecha_atencion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.ho_prog = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_estado");
            Entidad.nom_estado = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }


        private CitasBE Entidad_ListarDetalleOT(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;

            indice = DReader.GetOrdinal("seccion");
            Entidad.seccion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("tipo_servicio");
            Entidad.tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_repuesto");
            Entidad.co_repuesto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("des_repuesto_servicio");
            Entidad.des_repuesto_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("estado_ot");
            Entidad.estado_ot = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE Entidad_ListarDatosSecVehiculo(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;

            indice = DReader.GetOrdinal("an_fabricacion");
            Entidad.an_fabricacion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_motor");
            Entidad.nu_motor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_color_exterior");
            Entidad.no_color_exterior = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        #region "---------------- POPULATES ------------------"

        private CitasBE Entidad_Listar_Citas_Pendiente(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_cita");
            Entidad.nid_cita = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("no_correo_trabajo");
            Entidad.no_correo_trabajo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("no_correo_alter");
            Entidad.no_correo_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_tel_fijo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_tel_movil = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("cod_reserva_cita");
            Entidad.cod_reserva_cita = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fe_programada");
            Entidad.fe_prog = (DReader.IsDBNull(indice) ? DateTime.MinValue : DReader.GetDateTime(indice));
            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.no_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_asesor");
            Entidad.no_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_direccion");
            Entidad.no_direccion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_callcenter");
            Entidad.nu_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }



        private CitasBE CrearEntidadCita0(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_parametro");
            Entidad.nid_parametro = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("valor");
            Entidad.no_valor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE Entidad_ListarDatosVehiculoClientePorPlaca(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_vehiculo");
            Entidad.nid_vehiculo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_propietario");
            Entidad.nid_propietario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_cliente");
            Entidad.nid_cliente = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_contacto");
            Entidad.nid_contacto_sr = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_tipo_persona_prop");
            Entidad.co_tipo_persona_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_documento_prop");
            Entidad.co_tipo_documento_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento_prop");
            Entidad.nu_documento_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_razon_social_prop");
            Entidad.no_razon_social_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente_prop");
            Entidad.no_cliente_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_pat_prop");
            Entidad.no_ape_pat_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_mat_prop");
            Entidad.no_ape_mat_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono_prop");
            Entidad.nu_telefono_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono2_prop");
            Entidad.nu_telefono2_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_prop");
            Entidad.nu_celular_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular2_prop");
            Entidad.nu_celular2_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_prop");
            Entidad.no_correo_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trab_prop");
            Entidad.no_correo_trab_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter_prop");
            Entidad.no_correo_alter_prop = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("propietario");
            Entidad.propietario = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_persona_cli");
            Entidad.co_tipo_persona_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_documento_cli");
            Entidad.co_tipo_documento_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento_cli");
            Entidad.nu_documento_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_razon_social_cli");
            Entidad.no_razon_social_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente_cli");
            Entidad.no_cliente_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_pat_cli");
            Entidad.no_ape_pat_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_mat_cli");
            Entidad.no_ape_mat_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono_cli");
            Entidad.nu_telefono_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono2_cli");
            Entidad.nu_telefono2_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_cli");
            Entidad.nu_celular_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular2_cli");
            Entidad.nu_celular2_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_cli");
            Entidad.no_correo_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trab_cli");
            Entidad.no_correo_alter_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter_cli");
            Entidad.no_correo_trab_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_pais_celular_cli");
            Entidad.nid_pais_celular_cli = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_pais_telefono_cli");
            Entidad.nid_pais_telefono_cli = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nu_anexo_telefono_cli");
            Entidad.nu_anexo_telefono_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("cliente");
            Entidad.cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_vin");
            Entidad.Nu_vin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_marca");
            Entidad.nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_km_actual");
            Entidad.qt_km_inicial = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_tipo_documento");
            Entidad.cod_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nom_documento_cont");
            Entidad.nom_documento_cont = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_doc_contacto");
            Entidad.nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_nombre = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_pat");
            Entidad.no_ape_paterno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_mat");
            Entidad.no_ape_materno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_tel_oficina");
            Entidad.nu_tel_oficina = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_alter");
            Entidad.nu_celular_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trab");
            Entidad.no_correo_trabajo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter");
            Entidad.no_correo_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private CitasBE CrearEntidadCita2(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;
            string var1 = "";
            int var2 = 0;

            indice = DReader.GetOrdinal("nid_marca");
            var2 = DReader.GetInt32(indice);
            Entidad.nid_marca = (var2 == 0 ? 0 : var2);

            indice = DReader.GetOrdinal("co_marca");
            var1 = DReader.GetString(indice);
            Entidad.co_marca = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_marca");
            var1 = DReader.GetString(indice);
            Entidad.no_marca = (var1 == null ? "" : var1);


            return Entidad;
        }

        private CitasBE CrearEntidadCita3(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_marca");
            Entidad.nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_modelo");
            Entidad.co_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE CrearEntidadCita9(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("cod_tipo_documento");
            Entidad.cod_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("des_tipo_documento");
            Entidad.des_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private CitasBE Entidad_ListarDatosContactoPorDoc(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_CLIENTE");
            Entidad.nid_cliente = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_tipo_documento");
            Entidad.cod_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_nombre = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_pat");
            Entidad.no_ape_paterno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_mat");
            Entidad.no_ape_materno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_tel_oficina");
            Entidad.nu_tel_oficina = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_alter");
            Entidad.nu_celular_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trab");
            Entidad.no_correo_trabajo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter");
            Entidad.no_correo_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private CitasBE CrearEntidadCita11(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_CITA");
            Entidad.nid_cita = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("FE_CREA");
            Entidad.fe_prog = (DReader.IsDBNull(indice) ? DateTime.MinValue : DReader.GetDateTime(indice));
            indice = DReader.GetOrdinal("NOMBRES");
            Entidad.nombre = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("APELLIDOS");
            Entidad.no_ape_paterno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("TELEFONO");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("EMAIL_PERS");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("EMAIL_TRAB");
            Entidad.no_correo_trabajo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("EMAIL_ALTER");
            Entidad.no_correo_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("FL_ORIGEN");
            Entidad.fl_origen = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE CrearEntidadCita16(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE CrearEntidadCita17(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("row");
            Entidad.Itm = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_vehiculo");
            Entidad.nid_vehiculo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_vin");
            Entidad.Nu_vin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_marca");
            Entidad.nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_tipo_documento");
            Entidad.cod_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("PROPIETARIO");
            Entidad.propietario = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        ///********************   V 2.0   *************************************************/

        private CitasBE CrearEntidadCita18(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("coddist");
            Entidad.coddis = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nomdpto");
            Entidad.nomdpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nomprov");
            Entidad.nomprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nomdist");
            Entidad.nomdist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }


        //Disponibilidad
        private CitasBE CrearEntidadCita19(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_UBICA");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("CODDPTO");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODPROV");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODDIST");
            Entidad.coddis = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_UBICA");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NID_TALLER");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_TALLER");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_DIRECCION");
            Entidad.di_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("DISTRITO");
            Entidad.no_distrito = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NU_TELEFONO");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("MAPA_TALLER");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("INTERVALO_ATENC");
            Entidad.qt_intervalo_atenc = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        //Punto_Red
        private CitasBE CrearEntidadCita20(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_UBICA");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_UBICA");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        //Taller
        private CitasBE CrearEntidadCita21(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_TALLER");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_TALLER");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("TX_MAPA_TALLER");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //@003-I
            indice = DReader.GetOrdinal("fl_taxi");
            Entidad.fl_taxi = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //@003-F

            
            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.coddis = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            

            return Entidad;
        }

        //Horario Record
        private CitasBE CrearEntidadCita22(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NO_VALOR2");
            Entidad.no_valor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        //Horario Excepcional
        private CitasBE CrearEntidadCita23(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("HO_RANGO1");
            Entidad.ho_rango1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("HO_RANGO2");
            Entidad.ho_rango2 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("HO_RANGO3");
            Entidad.ho_rango3 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }



        ////OPTMIZACION: 07.06.2011

        private CitasBE CrearEntidadCita24(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("COD_USUARIO");
            Entidad.nid_usuario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("ASESOR_SERV");
            Entidad.no_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HORA_INICIO");
            Entidad.ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HORA_FIN");
            Entidad.ho_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("TELEFONO");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("EMAIL");
            Entidad.no_correo_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //indice = DReader.GetOrdinal("CAPACIDAD");
            //Entidad.qt_capacidad = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }
        private CitasBE CrearEntidadCita25(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            //indice = DReader.GetOrdinal("CO_ESTADO_CITA");
            //Entidad.co_estado_cita = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("HORA_INICIO");
            Entidad.ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("HORA_FIN");
            Entidad.ho_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private CitasBE Entidad_ListarCitasAsesores_PorTaller(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_asesor");
            Entidad.nid_asesor = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.ho_inicio_a = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("ho_fin");
            Entidad.ho_fin_a = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("qt_colaespetra");
            Entidad.qt_cola_espera = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }


        private CitasBE CrearEntidadCita26(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_TALLER");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_TALLER");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_UBICA_CORTO");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("TX_MAPA_TALLER");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NU_TELEFONO");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_DIRECCION");
            Entidad.di_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("DISTRITO");
            Entidad.no_distrito = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_INICIO");
            Entidad.ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_FIN");
            Entidad.ho_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("INTERVALO");
            Entidad.qt_intervalo_atenc = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //@003-I
            indice = DReader.GetOrdinal("fl_taxi");
            Entidad.fl_taxi = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //@003-F
            return Entidad;
        }


        private CitasBE CrearEntidadCita27(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("NID_UBICA");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("CODDPTO");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODPROV");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODDIST");
            Entidad.coddis = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_UBICA");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NID_TALLER");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_TALLER");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_DIRECCION");
            Entidad.di_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("DISTRITO");
            Entidad.no_distrito = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NU_TELEFONO");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("MAPA_TALLER");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("INTERVALO_ATENC");
            Entidad.qt_intervalo_atenc = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_INICIO");
            Entidad.ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_FIN");
            Entidad.ho_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE CrearEntidadCita28(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            int indice;

            indice = DReader.GetOrdinal("NID_CITA");
            Entidad.nid_cita = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("COD_RESERVA_CITA");
            Entidad.cod_reserva_cita = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("FE_PROGRAMADA");
            Entidad.fe_prog = (DReader.IsDBNull(indice) ? DateTime.MinValue : DReader.GetDateTime(indice));
            indice = DReader.GetOrdinal("ho_inicio_c");
            Entidad.ho_inicio_c = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ho_fin_c");
            Entidad.ho_fin_c = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice)); indice = DReader.GetOrdinal("FL_ORIGEN");
            Entidad.fl_origen = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NID_ESTADO");
            Entidad.nid_Estado = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("COD_ESTADO");
            Entidad.co_estado_cita = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_ESTADO");
            Entidad.no_estado_cita = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NU_ESTADO");
            Entidad.nu_estado = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("FL_DATOS_PENDIENTES");
            Entidad.fl_datos_pend = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("TX_OBSERVACION");
            Entidad.tx_observacion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("QT_KM_INICIAL");
            Entidad.qt_km_inicial = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("FE_ATENCION");
            Entidad.fe_atencion = (DReader.IsDBNull(indice) ? DateTime.MinValue : DReader.GetDateTime(indice));
            indice = DReader.GetOrdinal("TX_GLOSA_ATENCION");
            Entidad.tx_glos = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("NID_CONTACTO_SRC");
            Entidad.nid_contacto_sr = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_APE_PATERNO");
            Entidad.no_ape_paterno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_APE_MATERNO");
            Entidad.no_ape_materno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CO_TIPO_DOC");
            Entidad.co_tipo_doc = (DReader.IsDBNull(indice) ? 0 : Int32.Parse(DReader.GetString(indice)));
            indice = DReader.GetOrdinal("NU_DOCUMENTO");
            Entidad.nu_documento_cli = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trabajo");
            Entidad.no_correo_trabajo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter");
            Entidad.no_correo_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono_c");
            Entidad._nu_celular_c = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_c");
            Entidad.nu_celular_c = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_alter_c");
            Entidad.nu_celular_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_servicio");
            Entidad.nid_Servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.no_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fl_quick_service");
            Entidad.fl_quick_service = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.nid_tipo_servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.no_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CO_INTERVALO_ATENC");
            Entidad.co_intervalo_atenc = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("INTERVALO");
            Entidad.qt_intervalo_atenc = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NO_DIRECCION");
            Entidad.di_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono_t");
            Entidad.nu_telefono_t = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("TX_MAPA_TALLER");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("DD_ATENCION");
            Entidad.dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("HO_INICIO_TALLER");
            Entidad.ho_taller_ini = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_FIN_TALLER");
            Entidad.ho_taller_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("NID_UBICA");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_UBICA_CORTO");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODDPTO");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODPROV");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("CODDIST");
            Entidad.coddis = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_distrito");
            Entidad.no_distrito = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("NID_VEHICULO");
            Entidad.nid_vehiculo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NID_MODELO");
            Entidad.nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_MODELO");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NID_MARCA");
            Entidad.nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("NO_MARCA");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("NID_USUARIO");
            Entidad.nid_usuario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_asesor");
            Entidad.no_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono_a");
            Entidad.nu_telefono_a = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_asesor");
            Entidad.no_correo_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("nid_taller_empresa");
            Entidad.nid_taller_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_banco");
            Entidad.no_banco = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("nu_cuenta");
            Entidad.nu_cuenta = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("no_correo_callcenter");
            Entidad.no_correo_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("nu_callcenter");
            Entidad.nu_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("fl_nota");
            Entidad.fl_nota = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("nid_empresa");
            Entidad.Nid_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("fl_entrega");
            Entidad.fl_entrega = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("tx_url_taller");
            Entidad.tx_url_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            //@009 I
            indice = DReader.GetOrdinal("no_nombreqr");
            Entidad.no_nombreqr = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            indice = DReader.GetOrdinal("no_color_exterior");
            Entidad.no_color_exterior = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());
            //@009 F
            return Entidad;

        }

        private CitasBE CrearEntidadCita29(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();

            int indice;

            indice = DReader.GetOrdinal("nid_taller_empresa");
            Entidad.nid_taller_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_empresa");
            Entidad.Nid_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_banco");
            Entidad.no_banco = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_cuenta");
            Entidad.nu_cuenta = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_correo_callcenter");
            Entidad.no_correo_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_callcenter");
            Entidad.nu_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;

        }

        private CitasBE CrearEntidadCita30(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();

            int indice;

            indice = DReader.GetOrdinal("nid_taller_empresa");
            Entidad.nid_taller_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_empresa");
            Entidad.Nid_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_empresa");
            Entidad.no_empresa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_banco");
            Entidad.no_banco = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_cuenta");
            Entidad.nu_cuenta = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_correo_callcenter");
            Entidad.no_correo_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_callcenter");
            Entidad.nu_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;

        }


        private CitasBE Entidad_ListarTalleresDisponibles_Capacidad(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_capacidad_t");
            Entidad.qt_capacidad_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_citas_t");
            Entidad.qt_citas_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_cantidad_t");
            Entidad.qt_cantidad_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }

        private CitasBE Entidad_ListarTalleresDisponibles_PorFecha(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_intervalo");
            Entidad.qt_intervalo_atenc = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_capacidad_t");
            Entidad.qt_capacidad_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("dd_atencion");
            Entidad.dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("ho_inicio_t");
            Entidad.ho_inicio_t = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ho_fin_t");
            Entidad.ho_fin_t = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_direccion");
            Entidad.di_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_distrito");
            Entidad.no_distrito = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("tx_mapa_taller");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("tx_url_taller");
            Entidad.tx_url_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_ubica");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("coddist");
            Entidad.coddis = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fl_control");
            Entidad.fl_control = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_citas_t");
            Entidad.qt_citas_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_cantidad_t");
            Entidad.qt_cantidad_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_capacidad_m");
            Entidad.qt_capacidad_m = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_citas_m");
            Entidad.qt_citas_m = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_cantidad_m");
            Entidad.qt_cantidad_m = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_dia_exceptuado_t");
            Entidad.nid_dia_exceptuado_t = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }

        private CitasBE Entidad_ListarAsesoresDisponibles_PorFecha(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_asesor");
            Entidad.nid_asesor = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_asesor");
            Entidad.no_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ho_asesor");
            Entidad.horario_asesor = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono_a");
            Entidad.nu_telefono_a = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo_a = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_capacidad_a");
            Entidad.qt_capacidad_a = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_citas_a");
            Entidad.qt_citas_a = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("qt_cantidad_a");
            Entidad.qt_cantidad_a = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_dia_exceptuado_a");
            Entidad.nid_dia_exceptuado_a = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }

        private CitasBE Entidad_ListarHorarioExcepcional_Talleres(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("HO_RANGO1");
            Entidad.ho_rango1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_RANGO2");
            Entidad.ho_rango2 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_RANGO3");
            Entidad.ho_rango3 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private CitasBE Entidad_ListarCitasAsesores(IDataRecord DReader)
        {
            CitasBE Entidad = new CitasBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_asesor");
            Entidad.nid_asesor = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("ho_inicio_c");
            Entidad.ho_inicio_c = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ho_fin_c");
            Entidad.ho_fin_c = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_cola_espera");
            Entidad.qt_cola_espera = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            return Entidad;
        }



        #endregion

    }
}
