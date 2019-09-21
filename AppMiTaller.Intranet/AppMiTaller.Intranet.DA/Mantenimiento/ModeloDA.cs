using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class ModeloDA
    {
        public ModeloBE GetById(Int32 idModelo)
        {
            ModeloBE oBE = null;
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_modelo_x_id";
                    db.AddParameter("@vi_in_id_modelo", DbType.Int32, ParameterDirection.Input, idModelo);
                    DReader = db.GetDataReader();
                }
                if (DReader.Read())
                {
                    oBE = CrearEntidadById(DReader);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return oBE;
        }

        public Int32 Insertar(ModeloBE oBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_modelo";

                    db.AddParameter("@vi_va_cod_modelo", DbType.String, ParameterDirection.Input, oBE.co_modelo);
                    db.AddParameter("@vi_va_nom_modelo", DbType.String, ParameterDirection.Input, oBE.no_modelo);
                    db.AddParameter("@vi_in_id_marca", DbType.Int32, ParameterDirection.Input, oBE.nid_marca);
                    db.AddParameter("@vi_ch_cod_negocio", DbType.String, ParameterDirection.Input, oBE.co_negocio);
                    db.AddParameter("@vi_in_id_linea_imp", DbType.Int32, ParameterDirection.Input, oBE.nid_linea_importacion);
                    db.AddParameter("@vi_in_id_linea_com", DbType.Int32, ParameterDirection.Input, oBE.nid_linea_comercial);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oBE.fl_inactivo);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oBE.co_usuario_creacion);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oBE.no_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oBE.no_usuario_red);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }
        public Int32 Modificar(ModeloBE oBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_modelo";

                    db.AddParameter("@vi_in_id_modelo", DbType.Int32, ParameterDirection.Input, oBE.nid_modelo);
                    db.AddParameter("@vi_va_cod_modelo", DbType.String, ParameterDirection.Input, oBE.co_modelo);
                    db.AddParameter("@vi_va_nom_modelo", DbType.String, ParameterDirection.Input, oBE.no_modelo);
                    db.AddParameter("@vi_in_id_marca", DbType.Int32, ParameterDirection.Input, oBE.nid_marca);
                    db.AddParameter("@vi_ch_cod_negocio", DbType.String, ParameterDirection.Input, oBE.co_negocio);
                    db.AddParameter("@vi_in_id_linea_imp", DbType.Int32, ParameterDirection.Input, oBE.nid_linea_importacion);
                    db.AddParameter("@vi_in_id_linea_com", DbType.Int32, ParameterDirection.Input, oBE.nid_linea_comercial);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oBE.fl_inactivo);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oBE.co_usuario_creacion);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oBE.no_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oBE.no_usuario_red);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }
        public Int32 Eliminar(ModeloBE oBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spd_modelo";
                    db.AddParameter("@vi_in_id_modelo", DbType.Int32, ParameterDirection.Input, oBE.nid_modelo);
                    db.AddParameter("@vi_in_id_marca", DbType.Int32, ParameterDirection.Input, oBE.nid_marca);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oBE.co_usuario_cambio);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oBE.no_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oBE.no_usuario_red);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oBE.fl_inactivo);
                    db.AddParameter("@vi_ch_fecha", DbType.String, ParameterDirection.Input, oBE.sfe_cambio);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }

        public ModeloBEList GetAllBandejas(Int32 idMarca, String codModelo, String nomModelo, String codEstado, Int32 idUsuario)
        {
            ModeloBEList lista = new ModeloBEList();

            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_modelo";
                    db.AddParameter("@vi_in_id_marca", DbType.Int32, ParameterDirection.Input, idMarca);
                    db.AddParameter("@vi_ch_cod_modelo", DbType.String, ParameterDirection.Input, codModelo);
                    db.AddParameter("@vi_va_nom_modelo", DbType.String, ParameterDirection.Input, nomModelo);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, codEstado);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    lista.Add(CrearEntidadBandeja(DReader));
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

        private ModeloBE CrearEntidadById(IDataReader DReader)
        {
            ModeloBE oBE = new ModeloBE();
            int indice;

            indice = DReader.GetOrdinal("id_modelo");
            if (!DReader.IsDBNull(indice)) oBE.nid_modelo = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_modelo");
            oBE.co_modelo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_modelo");
            oBE.no_modelo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nid_marca");
            if (!DReader.IsDBNull(indice)) oBE.nid_marca = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("nom_marca");
            oBE.no_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_linea_importacion");
            oBE.nid_linea_importacion = DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_linea_comercial");
            oBE.nid_linea_comercial = DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_negocio");
            oBE.co_negocio = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("no_familia");
            oBE.no_familia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_estado");
            oBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            return oBE;
        }
        private ModeloBE CrearEntidad(IDataReader DReader)
        {
            ModeloBE oBE = new ModeloBE();
            int indice;

            indice = DReader.GetOrdinal("id_modelo");
            oBE.nid_modelo = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_modelo");
            oBE.co_modelo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_modelo");
            oBE.no_modelo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_linea_importacion");
            oBE.no_linea_importacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_linea_comercial");
            oBE.no_linea_comercial = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("estado");
            oBE.estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_estado");
            oBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            return oBE;
        }
        
        private ModeloBE CrearEntidadBandeja(IDataReader DReader)
        {
            ModeloBE oBE = new ModeloBE();
            int indice;

            indice = DReader.GetOrdinal("id_modelo");
            oBE.nid_modelo = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_modelo");
            oBE.co_modelo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_modelo");
            oBE.no_modelo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("no_familia");
            oBE.no_familia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_linea_importacion");
            oBE.no_linea_importacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_linea_comercial");
            oBE.no_linea_comercial = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("estado");
            oBE.estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_estado");
            oBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            return oBE;
        }



        public ModeloBEList GETListarModelos(ModeloBE ent)
        {
            ModeloBEList lista = new ModeloBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_SEL_MODELO";
                    db.AddParameter("@co_modelo", DbType.String, ParameterDirection.Input, ent.co_modelo);
                    db.AddParameter("@no_modelo", DbType.String, ParameterDirection.Input, ent.no_modelo);
                    db.AddParameter("@nid_marca", DbType.String, ParameterDirection.Input, int.Parse(ent.nid_marca.ToString().Trim()));
                    db.AddParameter("@co_negocio", DbType.String, ParameterDirection.Input, ent.co_negocio);
                    db.AddParameter("@co_familia", DbType.String, ParameterDirection.Input, ent.co_familia);
                    db.AddParameter("@Estado", DbType.String, ParameterDirection.Input, ent.estado);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    lista.Add(CrearEntidad_Parametros(DReader));
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

        public CombosBEList GETListarFamiliasByNegocio(ModeloBE ent)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_MODELO_BY_NEGOCIO]";
                    db.AddParameter("@co_negocio", DbType.String, ParameterDirection.Input, ent.co_negocio);
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

        public CombosBEList GETListarNegocios()
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_NEGOCIO]";
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

        public CombosBEList GETListarNegocios_X_Marca(string s_nid_marca)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_NEGOCIO_X_MARCA]";
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, Int32.Parse(s_nid_marca));
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

        public CombosBEList GETListarMarcas(int Nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_MARCA]";
                    db.AddParameter("@Nid_usuario", DbType.String, ParameterDirection.Input, Nid_usuario);
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
        public string[] GETListarParamByModelo(ModeloBE ent)
        {
            string[] lista = new string[5];
            IDataReader DReader = null;
            int indice;
            int var2;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_SEL_PRM_MODELO";
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, ent.nid_modelo);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    String var1 = "";

                    indice = DReader.GetOrdinal("kildefecto");
                    var2 = DReader.GetInt32(indice);
                    var1 = var2.ToString();
                    lista.SetValue(var1 == null ? "" : var1, 0);

                    indice = DReader.GetOrdinal("kilservicio");
                    var2 = DReader.GetInt32(indice);
                    var1 = var2.ToString();
                    lista.SetValue(var1 == null ? "" : var1, 1);

                    indice = DReader.GetOrdinal("perservicio");
                    var2 = DReader.GetInt32(indice);
                    var1 = var2.ToString();
                    lista.SetValue(var1 == null ? "" : var1, 2);

                    indice = DReader.GetOrdinal("minresercita");
                    var2 = DReader.GetInt32(indice);
                    var1 = var2.ToString();
                    lista.SetValue(var1 == null ? "" : var1, 3);

                    indice = DReader.GetOrdinal("nid_parametro");
                    var2 = DReader.GetInt32(indice);
                    var1 = var2.ToString();
                    lista.SetValue(var1 == null ? "" : var1, 4);
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
        public int GETInserActuParamByModelo(string[] lista)
        {
            int r = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_INS_UPD_PRM_MODELO]";
                    db.AddParameter("@nid_parametro", DbType.Int32, ParameterDirection.Input, (lista.GetValue(0) == null ? 0 : Int32.Parse(lista.GetValue(0).ToString())));
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, (lista.GetValue(1) == null ? 0 : Int32.Parse(lista.GetValue(1).ToString())));
                    db.AddParameter("@qt_km_promedio", DbType.Int32, ParameterDirection.Input, (lista.GetValue(2) == null ? 0 : Int32.Parse(lista.GetValue(2).ToString())));
                    db.AddParameter("@qt_km_servicio", DbType.Int32, ParameterDirection.Input, (lista.GetValue(3) == null ? 0 : Int32.Parse(lista.GetValue(3).ToString())));
                    db.AddParameter("@qt_per_servicio", DbType.Int32, ParameterDirection.Input, (lista.GetValue(4) == null ? 0 : Int32.Parse(lista.GetValue(4).ToString())));
                    db.AddParameter("@qt_min_diasreserva", DbType.Int32, ParameterDirection.Input, (lista.GetValue(5) == null ? 0 : Int32.Parse(lista.GetValue(5).ToString())));
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, (lista.GetValue(6) == null ? "" : lista.GetValue(6).ToString()));
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, (lista.GetValue(7) == null ? "" : lista.GetValue(7).ToString()));
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, (lista.GetValue(8) == null ? "" : lista.GetValue(8).ToString()));
                    db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, (lista.GetValue(9) == null ? "" : lista.GetValue(9).ToString()));
                    db.AddParameter("@ind_accion", DbType.String, ParameterDirection.Input, (lista.GetValue(10) == null ? "" : lista.GetValue(10).ToString()));

                    r = db.Execute();
                }
            }
            catch
            {
                r = 0;
            }
            return r;
        }
        private ComboBE CrearEntidad_Combo(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            int indice;
            String var1 = "";

            indice = DReader.GetOrdinal("ID");
            var1 = DReader.GetString(indice);
            Entidad.ID = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DES");
            var1 = DReader.GetString(indice);
            Entidad.DES = (var1 == null ? "" : var1);

            return Entidad;
        }
        private ModeloBE CrearEntidad_Parametros(IDataRecord DReader)
        {
            ModeloBE Entidad = new ModeloBE();
            int indice;
            String var1 = "";

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.nid_modelo = DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("co_modelo");
            var1 = DReader.GetString(indice);
            Entidad.co_modelo = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_modelo");
            var1 = DReader.GetString(indice);
            Entidad.no_modelo = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("co_marca");
            var1 = DReader.GetString(indice);
            Entidad.co_marca = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_marca");
            var1 = DReader.GetString(indice);
            Entidad.no_marca = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("co_negocio");
            var1 = DReader.GetString(indice);
            Entidad.co_negocio = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_negocio");
            var1 = DReader.GetString(indice);
            Entidad.no_negocio = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("co_familia");
            var1 = DReader.GetString(indice);
            Entidad.co_familia = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("no_familia");
            var1 = DReader.GetString(indice);
            Entidad.no_familia = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("Estado");
            var1 = DReader.GetString(indice);
            Entidad.estado = (var1 == null ? "" : var1);

            return Entidad;
        }

    }
}