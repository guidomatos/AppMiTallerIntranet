using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class MarcaDA
    {
        public MarcaBEList GetAll(String nomMarca, String codEstado)
        {
            MarcaBEList lista = new MarcaBEList();

            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_marca";
                    db.AddParameter("@vi_va_nom_marca", DbType.String, ParameterDirection.Input, nomMarca);
                    db.AddParameter("@vi_int_cod_estado", DbType.String, ParameterDirection.Input, codEstado);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    lista.Add(CrearEntidad(DReader));
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
        public MarcaBE GetById(Int32 id)
        {
            MarcaBE oMarcaBE = new MarcaBE();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_marca_x_id";
                    db.AddParameter("@vi_in_id_marca", DbType.Int32, ParameterDirection.Input, id);
                    DReader = db.GetDataReader();
                }
                if (DReader.Read())
                {
                    oMarcaBE = CrearEntidadById(DReader);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return oMarcaBE;
        }
        public Int32 Insertar(MarcaBE oMarcaBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_marca";

                    db.AddParameter("@vi_va_cod_marca", DbType.String, ParameterDirection.Input, oMarcaBE.co_marca);
                    db.AddParameter("@vi_va_nom_marca", DbType.String, ParameterDirection.Input, oMarcaBE.no_marca);
                    db.AddParameter("@vi_in_id_empresa", DbType.Int32, ParameterDirection.Input, oMarcaBE.nid_empresa);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oMarcaBE.fl_inactivo);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oMarcaBE.co_usuario_creacion);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oMarcaBE.no_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oMarcaBE.no_usuario_red);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }
        public Int32 Modificar(MarcaBE oMarcaBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_marca";

                    db.AddParameter("@vi_int_id_marca", DbType.Int32, ParameterDirection.Input, oMarcaBE.nid_marca);
                    db.AddParameter("@vi_va_cod_marca", DbType.String, ParameterDirection.Input, oMarcaBE.co_marca);
                    db.AddParameter("@vi_va_nom_marca", DbType.String, ParameterDirection.Input, oMarcaBE.no_marca);
                    db.AddParameter("@vi_in_id_empresa", DbType.Int32, ParameterDirection.Input, oMarcaBE.nid_empresa);
                    db.AddParameter("@vi_int_cod_estado", DbType.String, ParameterDirection.Input, oMarcaBE.fl_inactivo);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oMarcaBE.co_usuario_creacion);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oMarcaBE.no_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oMarcaBE.no_usuario_red);
                    
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }
        public Int32 Eliminar(MarcaBE oMarcaBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spd_marca";
                    db.AddParameter("@vi_int_id_marca", DbType.Int32, ParameterDirection.Input, oMarcaBE.nid_marca);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oMarcaBE.co_usuario_cambio);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oMarcaBE.no_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oMarcaBE.no_usuario_red);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oMarcaBE.fl_inactivo);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }
        public MarcaBEList GetListaMarca()
        {
            MarcaBEList lista = new MarcaBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_marca";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    MarcaBE oMarcaBE = CrearEntidadLista(DReader);
                    lista.Add(oMarcaBE);
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
        private MarcaBE CrearEntidadLista(IDataReader DReader)
        {
            MarcaBE oMarcaBE = new MarcaBE();
            int indice;
            indice = DReader.GetOrdinal("id_marca");
            oMarcaBE.nid_marca = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_marca");
            oMarcaBE.co_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_marca");
            oMarcaBE.no_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            return oMarcaBE;
        }
        private MarcaBE CrearEntidad(IDataReader DReader)
        {
            MarcaBE oMarcaBE = new MarcaBE();
            int indice;
            indice = DReader.GetOrdinal("id_marca");
            oMarcaBE.nid_marca = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_marca");
            oMarcaBE.co_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_marca");
            oMarcaBE.no_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("estado");
            oMarcaBE.estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_estado");
            oMarcaBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oMarcaBE;
        }
        private MarcaBE CrearEntidadById(IDataReader DReader)
        {
            MarcaBE oMarcaBE = new MarcaBE();
            int indice;
            Byte[] myData;

            indice = DReader.GetOrdinal("id_marca");
            oMarcaBE.nid_marca = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("cod_marca");
            oMarcaBE.co_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_marca");
            oMarcaBE.no_marca = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("id_empresa");
            if (!DReader.IsDBNull(indice))
                oMarcaBE.nid_empresa = DReader.GetInt32(indice);
            else
                oMarcaBE.nid_empresa = 0;
            indice = DReader.GetOrdinal("nu_ruc");
            oMarcaBE.nu_ruc = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_usu_crea");
            oMarcaBE.co_usuario_creacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_estado");
            oMarcaBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("fec_modi");
            if (!DReader.IsDBNull(indice))
            {
                oMarcaBE.fe_cambio = DReader.GetDateTime(indice);
                oMarcaBE.sfe_cambio = oMarcaBE.fe_cambio.ToShortDateString();
            }
            else oMarcaBE.sfe_cambio = String.Empty;

            indice = DReader.GetOrdinal("cod_usu_modi");
            oMarcaBE.co_usuario_cambio = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_estacion");
            oMarcaBE.no_estacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_usuario_red");
            oMarcaBE.no_usuario_red = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            
            return oMarcaBE;
        }
    }
}