using System;
using System.Data;
using AppMiTaller.Intranet.BE;
using System.Data.SqlClient;

namespace AppMiTaller.Intranet.DA
{
    public class TipoServicioDA
    {
        String var1 = "";

        public TipoServicioBEList BusqTServicioList(TipoServicioBE ent)
        {
            TipoServicioBEList lista = new TipoServicioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_TIPOS_SERVICIO_BY_PARAM_BO";
                    db.AddParameter("@vi_co_tipo_servicio", DbType.String, ParameterDirection.Input, ent.Co_tipo_servicio);
                    db.AddParameter("@vi_no_tipo_servicio", DbType.String, ParameterDirection.Input, ent.No_tipo_servicio);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TipoServicioBE oEnt = CrearEntidad(DReader);
                    lista.Add(oEnt);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }
        public Int32 InsertTServicio(TipoServicioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPI_TIPOS_SERVICIO_BO";
                    db.AddParameter("@vi_co_tipo_servicio", DbType.String, ParameterDirection.Input, ent.Co_tipo_servicio);
                    db.AddParameter("@vi_no_tipo_servicio", DbType.String, ParameterDirection.Input, ent.No_tipo_servicio);
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    db.AddParameter("@vi_fl_visible", DbType.String, ParameterDirection.Input, ent.fl_visible);
                    db.AddParameter("@vi_fl_valida_km", DbType.String, ParameterDirection.Input, ent.fl_validar_km);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch
            {
                return res = 1;
                throw;
            }
            return res;
        }
        public Int32 UpdateTServicio(TipoServicioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPU_TIPOS_SERVICIO_BO";
                    db.AddParameter("@vi_nid_tipo_servicio", DbType.String, ParameterDirection.Input, ent.Id_TipoServicio);
                    db.AddParameter("@vi_co_tipo_servicio", DbType.String, ParameterDirection.Input, ent.Co_tipo_servicio);
                    db.AddParameter("@vi_no_tipo_servicio", DbType.String, ParameterDirection.Input, ent.No_tipo_servicio);
                    db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    db.AddParameter("@vi_fl_visible", DbType.String, ParameterDirection.Input, ent.fl_visible);
                    db.AddParameter("@vi_fl_valida_km", DbType.String, ParameterDirection.Input, ent.fl_validar_km);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch
            {
                throw;
                //return res = 1;
            }
            return res;
        }

        private TipoServicioBE CrearEntidad(IDataRecord DReader)
        {
            TipoServicioBE Entidad = new TipoServicioBE();
            int indice;

            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.Id_TipoServicio = DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("co_tipo_servicio");
            var1 = DReader.GetString(indice);
            Entidad.Co_tipo_servicio = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_tipo_servicio");
            var1 = DReader.GetString(indice);
            Entidad.No_tipo_servicio = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("fl_activo");
            var1 = DReader.GetString(indice);
            Entidad.Fl_activo = (var1 == null ? "" : var1);
            //I @001
            indice = DReader.GetOrdinal("fl_visible");
            var1 = DReader.GetString(indice);
            Entidad.fl_visible = (var1 == null ? "" : var1); ;

            indice = DReader.GetOrdinal("fl_valida_km");
            var1 = DReader.GetString(indice);
            Entidad.fl_validar_km = (var1 == null ? "" : var1); ;
            //F @001

            return Entidad;
        }
        public TipoServicioBEList
            GETListarTiposServicio(TipoServicioBE ent)
        {
            TipoServicioBEList lista = new TipoServicioBEList();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPS_LISTAR_TIPO_SERVICIO_BO", conn);
            SqlDataReader reader = null;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_Nid_usuario", ent.Nid_usuario);
            cmd.Parameters.AddWithValue("@vi_nid_modelo", ent.nid_modelo == 0 ? (object)DBNull.Value : ent.nid_modelo); 
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(CrearEntidad1(reader));
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
        private TipoServicioBE CrearEntidad1(IDataRecord DReader)
        {
            TipoServicioBE Entidad = new TipoServicioBE();
            int indice;

            indice = DReader.GetOrdinal("NID_TIPO_SERVICIO");
            Entidad.Id_TipoServicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("NO_TIPO_SERVICIO");
            Entidad.No_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }


    }
}