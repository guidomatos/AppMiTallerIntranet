using System;
using System.Data;
using AppMiTaller.Intranet.BE;
using System.Data.SqlClient;
using System.Configuration;

namespace AppMiTaller.Intranet.DA
{
    public class ClienteDA
    {
        public static ClienteBE ListarDatosClienteDireccion(int nid_cliente)
        {
            ClienteBE Entidad = new ClienteBE();
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[SRC_SPS_DATOS_CLIENTE_DIRECCION_BO]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cliente", nid_cliente);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Entidad.nid_cliente = int.Parse(reader["nid_cliente"].ToString());
                    Entidad.nid_cliente_direccion = int.Parse(reader["nid_cliente_direccion"].ToString());
                    Entidad.nu_telefono = reader["nu_telefono"].ToString();
                    Entidad.nu_fax = reader["nu_fax"].ToString();
                    Entidad.no_direccion = reader["no_direccion"].ToString();
                    Entidad.nid_pais = int.Parse(reader["nid_pais"].ToString());
                    Entidad.coddpto = reader["coddpto"].ToString();
                    Entidad.codprov = reader["codprov"].ToString();
                    Entidad.coddist = reader["coddist"].ToString();
                    Entidad.no_dpto = reader["no_dpto"].ToString();
                    Entidad.no_prov = reader["no_prov"].ToString();
                    Entidad.no_dist = reader["no_dist"].ToString();
                    Entidad.no_ubigeo = reader["no_dpto"].ToString() + " / " + reader["no_prov"].ToString() + " / " + reader["no_dist"].ToString();
                    Entidad.no_correo = reader["no_correo"].ToString();

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
        public static string GuardarDireccionTaller(ClienteBE oMestroClienteBE)
        {
            string res = "";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SRC_SPI_CLIENTE_DIRECCION_BO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vi_nid_cliente_direccion", oMestroClienteBE.nid_cliente_direccion);
            cmd.Parameters.AddWithValue("@vi_nid_cliente", oMestroClienteBE.nid_cliente);
            cmd.Parameters.AddWithValue("@vi_nu_telefono", oMestroClienteBE.nu_telefono);
            cmd.Parameters.AddWithValue("@vi_nu_fax", oMestroClienteBE.nu_fax);
            cmd.Parameters.AddWithValue("@vi_nid_pais", oMestroClienteBE.nid_pais);
            cmd.Parameters.AddWithValue("@vi_no_direccion", oMestroClienteBE.no_direccion);
            cmd.Parameters.AddWithValue("@vi_coddpto", oMestroClienteBE.coddpto);
            cmd.Parameters.AddWithValue("@vi_codprov", oMestroClienteBE.codprov);
            cmd.Parameters.AddWithValue("@vi_coddist", oMestroClienteBE.coddist);
            cmd.Parameters.AddWithValue("@vi_no_correo", oMestroClienteBE.no_correo);
            cmd.Parameters.AddWithValue("@vi_co_usuario", oMestroClienteBE.co_usuario);
            cmd.Parameters.AddWithValue("@vi_no_usuario_red", oMestroClienteBE.no_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", oMestroClienteBE.no_estacion_red);

            try
            {
                conn.Open();
                res = cmd.ExecuteScalar().ToString();
            }
            catch //(Exception ex)
            {
                res = "";
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return res;
        }

        #region " Variable Global de la Clase "
        string var1 = "";
        #endregion 


        public ClienteBE ListarDatosCliente(int nid_cliente)
        {
            ClienteBE Entidad = new ClienteBE();
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("src_sps_datos_cliente_bo", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cliente", nid_cliente);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Entidad.nid_cliente = int.Parse(reader["nid_cliente"].ToString());
                    Entidad.co_tipo_cliente = reader["co_tipo_cliente"].ToString();
                    Entidad.co_tipo_documento = reader["co_tipo_documento"].ToString();
                    Entidad.no_cliente = reader["no_cliente"].ToString();
                    Entidad.no_ape_pat = reader["no_ape_pat"].ToString();
                    Entidad.no_ape_mat = reader["no_ape_mat"].ToString();
                    Entidad.nu_documento = reader["nu_documento"].ToString();
                    Entidad.nu_telefono = reader["nu_telefono"].ToString();
                    Entidad.nu_tel_oficina = reader["nu_tel_oficina"].ToString();
                    Entidad.nu_celular = reader["nu_celular"].ToString();
                    Entidad.nu_celular_alter = reader["nu_celular_alter"].ToString();
                    Entidad.no_correo = reader["no_correo"].ToString();
                    Entidad.no_correo_trabajo = reader["no_correo_trabajo"].ToString();
                    Entidad.no_correo_alter = reader["no_correo_alter"].ToString();
                    Entidad.fl_inactivo = reader["fl_inactivo"].ToString();
                    Entidad.fl_identidad_validada = int.Parse(reader["fl_identidad_validada"].ToString());
                    Entidad.nid_pais_celular = int.Parse(reader["nid_pais_celular"].ToString());
                    Entidad.nid_pais_telefono = int.Parse(reader["nid_pais_telefono"].ToString());
                    Entidad.nu_anexo_telefono = reader["nu_anexo_telefono"].ToString();
                }
            }
            catch //(Exception ex)
            {
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
        public ClienteBEList GETListarClientes(ClienteBE ent)
        {
            ClienteBEList lista = new ClienteBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_CLIENTES]";
                    db.AddParameter("@no_cliente", DbType.String, ParameterDirection.Input, ent.no_cliente);
                    db.AddParameter("@no_ape_pat", DbType.String, ParameterDirection.Input, ent.no_ape_pat);
                    db.AddParameter("@no_ape_mat", DbType.String, ParameterDirection.Input, ent.no_ape_mat);
                    db.AddParameter("@co_tipo_documento", DbType.String, ParameterDirection.Input, ent.co_tipo_documento);
                    db.AddParameter("@nu_documento", DbType.String, ParameterDirection.Input, ent.nu_documento);
                    db.AddParameter("@fl_inactivo", DbType.String, ParameterDirection.Input, ent.fl_inactivo);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ClienteBE oMestroClienteBE = CrearEntidad_Parametros(DReader);
                    lista.Add(oMestroClienteBE);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }

        public CombosBEList GETListarTipoDocumento(string oPersona)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DDL_TIPODOCUMENTO2]";
                    db.AddParameter("@vi_tipopersona", DbType.String, ParameterDirection.Input, oPersona);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ComboBE oCombosBE = CrearEntidad_Combo(DReader);
                    lista.Add(oCombosBE);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }

        public Int32 GETInserActuCliente(ClienteBE ent)
        {
            Int32 oResp = 0;
            string msj = "";
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("[SRC_SPS_INS_UPD_CLIENTE]", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_cliente", Int32.Parse(ent.nid_cliente.ToString()));
            cmd.Parameters.AddWithValue("@vi_co_tipo_documento", ent.co_tipo_documento);
            cmd.Parameters.AddWithValue("@vi_nu_documento", ent.nu_documento);
            cmd.Parameters.AddWithValue("@vi_no_cliente", ent.no_cliente);
            cmd.Parameters.AddWithValue("@vi_no_ape_pat", ent.no_ape_pat);
            cmd.Parameters.AddWithValue("@vi_no_ape_mat", ent.no_ape_mat);
            cmd.Parameters.AddWithValue("@vi_nid_pais_telefono", ent.nid_pais_telefono);
            cmd.Parameters.AddWithValue("@vi_nu_telefono1", ent.nu_telefono);
            cmd.Parameters.AddWithValue("@vi_nu_telefono2", ent.nu_tel_oficina);
            cmd.Parameters.AddWithValue("@vi_nu_telefono_anexo", ent.nu_anexo_telefono);
            cmd.Parameters.AddWithValue("@vi_nid_pais_celular", ent.nid_pais_celular);
            cmd.Parameters.AddWithValue("@vi_nu_celular1", ent.nu_celular);
            cmd.Parameters.AddWithValue("@vi_nu_celular2", ent.nu_celular_alter);
            cmd.Parameters.AddWithValue("@vi_no_correo", ent.no_correo);
            cmd.Parameters.AddWithValue("@vi_no_correo_trab", ent.no_correo_trabajo);
            cmd.Parameters.AddWithValue("@vi_no_correo_alter", ent.no_correo_alter);
            cmd.Parameters.AddWithValue("@vi_co_usuario_crea", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            cmd.Parameters.AddWithValue("@vi_fl_inactivo", ent.fl_inactivo);
            cmd.Parameters.AddWithValue("@vi_ind_accion", ent.ind_accion);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@val_retorno";
            parameter.DbType = DbType.Int32;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = oResp;
            parameter.SourceColumnNullMapping = true;
            cmd.Parameters.Add(parameter);

            parameter = new SqlParameter();
            parameter.ParameterName = "@val_msg_retorno";
            parameter.DbType = DbType.String;
            parameter.Direction = ParameterDirection.Output;
            parameter.Size = 100;
            parameter.Value = msj;
            parameter.SourceColumnNullMapping = true;
            cmd.Parameters.Add(parameter);
            
            try
            {
                conn.Open();
                cmd.ExecuteScalar();
                Int32.TryParse(cmd.Parameters["@val_retorno"].Value.ToString(), out oResp);
            }
            catch
            {
                oResp = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return oResp;

        }

        public int SRC_SPS_VAL_CLIENTE_X_DOC(ClienteBE ent)
        {
            IDataReader DReader = null;
            int r = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_VAL_CLIENTE_X_DOC]";
                    db.AddParameter("@co_tipo_documento", DbType.String, ParameterDirection.Input, (ent.co_tipo_documento == null ? "" : ent.co_tipo_documento.ToString()));
                    db.AddParameter("@nu_documento", DbType.String, ParameterDirection.Input, (ent.nu_documento == null ? "" : ent.nu_documento.ToString()));

                    DReader = db.GetDataReader();
                }

                int indice;
                while (DReader.Read())
                {
                    indice = DReader.GetOrdinal("Resultado");
                    r = DReader.GetInt32(indice);
                }
            }
            catch (Exception)
            {
                r = 1;
            }
            return r;
        }

        #region "Populate"

        private ComboBE CrearEntidad_Combo(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            int indice;

            indice = DReader.GetOrdinal("ID");
            Entidad.ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());

            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice).Trim());

            return Entidad;
        }
        private ClienteBE CrearEntidad_Parametros(IDataRecord DReader)
        {
            ClienteBE Entidad = new ClienteBE();
            int indice;

            indice = DReader.GetOrdinal("nid_cliente");
            Entidad.nid_cliente = DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = DReader.IsDBNull(indice) ? "" : DReader.GetString(indice);

            indice = DReader.GetOrdinal("no_ape_pat");
            var1 = DReader.GetString(indice);
            Entidad.no_ape_pat = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_ape_mat");
            var1 = DReader.GetString(indice);
            Entidad.no_ape_mat = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("co_tipo_documento");
            var1 = DReader.GetString(indice);
            Entidad.co_tipo_documento = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("Documento");
            var1 = DReader.GetString(indice);
            Entidad.Documento = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("nu_documento");
            var1 = DReader.GetString(indice);
            Entidad.nu_documento = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("fl_inactivo");
            var1 = DReader.GetString(indice);
            Entidad.fl_inactivo = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("Estado");
            var1 = DReader.GetString(indice);
            Entidad.Estado = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_correo");
            var1 = DReader.GetString(indice);
            Entidad.no_correo = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("nu_telefono");
            var1 = DReader.GetString(indice);
            Entidad.nu_telefono = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("nu_celular");
            var1 = DReader.GetString(indice);
            Entidad.nu_celular = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("co_tipo_cliente");
            var1 = DReader.GetString(indice);
            Entidad.co_tipo_cliente = (var1 == null ? "" : var1);

            return Entidad;
        }

        #endregion
    }
}