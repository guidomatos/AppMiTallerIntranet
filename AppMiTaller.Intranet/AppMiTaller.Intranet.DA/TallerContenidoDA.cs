using System;
using System.Data.SqlClient;
using System.Data;
using AppMiTaller.Intranet.BE;
using System.Configuration;
using System.Collections;

namespace AppMiTaller.Intranet.DA
{
    public class TallerContenidoDA
    {


        public static TallerContenidoBE ListarContenidoInformativoTaller(int nid_taller)
        {
            TallerContenidoBE Entidad = new TallerContenidoBE();
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_contenido_total_taller_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", nid_taller);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Entidad.no_taller = reader["no_taller"].ToString();
                    Entidad.tx_promociones = reader["tx_promociones"].ToString();
                    Entidad.tx_noticias = reader["tx_noticias"].ToString();
                    Entidad.tx_datos = reader["tx_datos"].ToString();
                    Entidad.tx_fotos = reader["tx_fotos"].ToString();

                }
            }
            catch //(Exception ex)
            {
                //LogHelper.logError(ex.Source, ex.TargetSite.Name, ex.Message, "");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return Entidad;
        }
        public static string ActualizarEstadoContenido(TallerContenidoBE oMaestroTallerContenidoBE)
        {
            String rpta = String.Empty;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_spu_estado_contenido_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_fl_tipo", oMaestroTallerContenidoBE.fl_tipo.Trim());
            cmd.Parameters.AddWithValue("@vi_co_masivo", oMaestroTallerContenidoBE.co_masivo.Trim());
            cmd.Parameters.AddWithValue("@vi_co_estado", oMaestroTallerContenidoBE.co_estado.Trim());
            cmd.Parameters.AddWithValue("@vi_tx_observacion", oMaestroTallerContenidoBE.tx_observacion.Trim());
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", oMaestroTallerContenidoBE.co_usuario.Trim());
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", oMaestroTallerContenidoBE.no_usuario_red.Trim());
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", oMaestroTallerContenidoBE.no_estacion_red.Trim());


            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();
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

        public static TallerContenidoBE ListarContenidoTaller(int nid_taller_contenido)
        {
            TallerContenidoBE Entidad = new TallerContenidoBE();
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_contenido_taller_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller_contenido", nid_taller_contenido);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Entidad.nid_taller_contenido = int.Parse(reader["nid_taller_contenido"].ToString());
                    Entidad.co_taller_contenido = reader["co_taller_contenido"].ToString();
                    Entidad.nid_taller = int.Parse(reader["nid_taller"].ToString());
                    Entidad.nid_negocio_taller = int.Parse(reader["nid_negocio_taller"].ToString());
                    Entidad.co_tipo = reader["co_tipo"].ToString();
                    Entidad.no_titulo = reader["no_titulo"].ToString();
                    Entidad.fe_inicio = reader["fe_inicio"].ToString();
                    Entidad.fe_fin = reader["fe_fin"].ToString();
                    Entidad.tx_contenido = reader["tx_contenido"].ToString();
                    Entidad.tx_observacion = reader["tx_observacion"].ToString();
                    Entidad.co_estado = reader["co_estado"].ToString();
                    Entidad.no_estado = reader["no_estado"].ToString();
                    Entidad.co_fotos = reader["no_fotos"].ToString();

                }
            }
            catch //(Exception ex)
            {
                //LogHelper.logError(ex.Source, ex.TargetSite.Name, ex.Message, "");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return Entidad;
        }

        public static string InsertarContenidoTaller(TallerContenidoBE oMaestroTallerContenidoBE)
        {
            String rpta = String.Empty;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_spi_contenido_taller_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nid_taller_contenido", oMaestroTallerContenidoBE.nid_taller_contenido);
            cmd.Parameters.AddWithValue("@vi_co_taller_contenido", oMaestroTallerContenidoBE.co_taller_contenido.Trim());
            cmd.Parameters.AddWithValue("@vi_nid_taller", oMaestroTallerContenidoBE.nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_negocio_taller", oMaestroTallerContenidoBE.nid_negocio_taller);
            cmd.Parameters.AddWithValue("@vi_co_tipo", oMaestroTallerContenidoBE.co_tipo.Trim());
            cmd.Parameters.AddWithValue("@vi_no_titulo", oMaestroTallerContenidoBE.no_titulo.Trim());
            cmd.Parameters.AddWithValue("@vi_fe_inicio", oMaestroTallerContenidoBE.fe_inicio.Trim());
            cmd.Parameters.AddWithValue("@vi_fe_fin", oMaestroTallerContenidoBE.fe_fin.Trim());
            cmd.Parameters.AddWithValue("@vi_tx_contenido", oMaestroTallerContenidoBE.tx_contenido.Trim());
            cmd.Parameters.AddWithValue("@vi_tx_observacion", oMaestroTallerContenidoBE.tx_observacion.Trim());
            cmd.Parameters.AddWithValue("@vi_co_estado", oMaestroTallerContenidoBE.co_estado.Trim());
            cmd.Parameters.AddWithValue("@vi_co_fotos", oMaestroTallerContenidoBE.co_fotos.Trim());
            cmd.Parameters.AddWithValue("@vi_co_descrip", oMaestroTallerContenidoBE.co_descrip.Trim());
            //---------------
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", oMaestroTallerContenidoBE.co_usuario.Trim());
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", oMaestroTallerContenidoBE.no_usuario_red.Trim());
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", oMaestroTallerContenidoBE.no_estacion_red.Trim());


            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();
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

        public static string ListarCodigoContenido()
        {
            String rpta = String.Empty;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_codigo_contenido_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                rpta = cmd.ExecuteScalar().ToString();
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

        public static TallerContenidoBEList ListarBandejaContenido(Hashtable listado)
        {
            TallerContenidoBEList lista = new TallerContenidoBEList();
            TallerContenidoBE Entidad;
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_bandeja_contenido_informativo_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            Entidad = new TallerContenidoBE();

            cmd.Parameters.AddWithValue("@vi_nid_taller", int.Parse(listado["nid_taller"].ToString()));
            cmd.Parameters.AddWithValue("@vi_no_titulo", listado["no_titulo"].ToString().Trim());
            cmd.Parameters.AddWithValue("@vi_fe_vigencia_ini", listado["fe_vigencia_ini"].ToString().Trim());
            cmd.Parameters.AddWithValue("@vi_fe_vigencia_fin", listado["fe_vigencia_fin"].ToString().Trim());
            cmd.Parameters.AddWithValue("@vi_fe_actualizacion", listado["fe_actualizacion"].ToString().Trim());
            cmd.Parameters.AddWithValue("@vi_co_tipo", listado["co_tipo"].ToString().Trim());
            cmd.Parameters.AddWithValue("@vi_co_estado", listado["co_estado"].ToString().Trim());
            cmd.Parameters.AddWithValue("@vi_nid_negocio_taller", int.Parse(listado["nid_negocio_taller"].ToString()));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Entidad = new TallerContenidoBE();
                    Entidad.nid_taller_contenido = int.Parse(reader["nid_taller_contenido"].ToString());
                    Entidad.no_tipo = reader["no_tipo"].ToString();
                    Entidad.no_negocio_taller = reader["no_negocio_taller"].ToString();
                    Entidad.no_titulo = reader["no_titulo"].ToString();
                    Entidad.tx_contenido = reader["tx_contenido"].ToString();
                    Entidad.fe_inicio = reader["fe_inicio"].ToString();
                    Entidad.fe_fin = reader["fe_fin"].ToString();
                    Entidad.fe_actualizacion = reader["fe_modi"].ToString();
                    Entidad.tx_observacion = reader["tx_observacion"].ToString();
                    Entidad.co_estado = reader["co_estado"].ToString();
                    Entidad.no_estado = reader["no_estado"].ToString();

                    lista.Add(Entidad);
                }
            }
            catch //(Exception ex)
            {
                //LogHelper.logError(ex.Source, ex.TargetSite.Name, ex.Message, "");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return lista;
        }

        public static TallerContenidoBEList ListarTipoContenido()
        {
            TallerContenidoBEList lista = new TallerContenidoBEList();
            TallerContenidoBE Entidad;
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_TIPO_CONTENIDO_INFORMATIVO_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Entidad = new TallerContenidoBE();
                    Entidad.co_tipo = reader["co_tipo"].ToString();
                    Entidad.no_tipo = reader["no_tipo"].ToString();
                    lista.Add(Entidad);
                }
            }
            catch //(Exception ex)
            {
                //LogHelper.logError(ex.Source, ex.TargetSite.Name, ex.Message, "");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return lista;
        }

        public static TallerContenidoBEList ListarEstadoContenido()
        {
            TallerContenidoBEList lista = new TallerContenidoBEList();
            TallerContenidoBE Entidad;
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_ESTADOS_CONTENIDO_INFORMATIVO_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Entidad = new TallerContenidoBE();
                    Entidad.co_estado = reader["co_estado"].ToString();
                    Entidad.no_estado = reader["no_estado"].ToString();
                    lista.Add(Entidad);
                }
            }
            catch //(Exception ex)
            {
                //LogHelper.logError(ex.Source, ex.TargetSite.Name, ex.Message, "");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return lista;
        }

        public static TallerContenidoBEList ListarNeogiciosTaller(int nid_taller, int nid_usuario)
        {
            TallerContenidoBEList lista = new TallerContenidoBEList();
            TallerContenidoBE Entidad;
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_negocio_taller_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_taller", nid_taller);
            cmd.Parameters.AddWithValue("@vi_nid_usuario", nid_usuario);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Entidad = new TallerContenidoBE();
                    Entidad.nid_negocio_taller = int.Parse(reader["nid_negocio_taller"].ToString());
                    Entidad.no_negocio_taller = reader["no_negocio_taller"].ToString();
                    lista.Add(Entidad);
                }
            }
            catch //(Exception ex)
            {
                //LogHelper.logError(ex.Source, ex.TargetSite.Name, ex.Message, "");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return lista;
        }

    }
}