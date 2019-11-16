using System;
using System.Data;
using AppMiTaller.Intranet.BE;
using System.Data.SqlClient;

namespace AppMiTaller.Intranet.DA
{
    public class ReporteDA
    {
        
        public CitasBEList ListarCitasAtendidasPorMarca(Int32 nid_usuario)
        {
            CitasBEList lista = new CitasBEList();
            CitasBE Entidad;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_reporte_citas_atendidas_por_marca", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entidad = new CitasBE();
                    Entidad.no_marca = reader["no_marca"].ToString();
                    Entidad.qt_citas_a = int.Parse(reader["qt_cita"].ToString());
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

        public CitasBEList ListarCitasAtendidasPorAsesor(Int32 nid_usuario)
        {
            CitasBEList lista = new CitasBEList();
            CitasBE Entidad;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_reporte_citas_atendidas_por_asesor", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", nid_usuario);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entidad = new CitasBE();
                    Entidad.no_asesor = reader["no_asesor"].ToString();
                    Entidad.qt_citas_a = int.Parse(reader["qt_cita"].ToString());
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
    }
}
