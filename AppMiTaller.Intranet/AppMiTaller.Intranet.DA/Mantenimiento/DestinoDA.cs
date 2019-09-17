using System;
using AppMiTaller.Intranet.BE;
using System.Data;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class DestinoDA
    {
        public Int32 Insertar(DestinoBE oDestinoBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[sgsnet_spi_destino]";

                    db.AddParameter("@vi_va_nom_ubicacion", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_ubicacion);
                    db.AddParameter("@vi_va_nom_ubicacion_corta", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_corto_ubicacion);
                    db.AddParameter("@vi_ch_cod_tipo_ubicacion", DbType.String, ParameterDirection.Input, oDestinoBE.Tipo_ubicacion);
                    db.AddParameter("@vi_va_ruc", DbType.String, ParameterDirection.Input, oDestinoBE.Nro_ruc);
                    db.AddParameter("@vi_va_direcion", DbType.String, ParameterDirection.Input, oDestinoBE.Direccion);
                    db.AddParameter("@vi_ch_cod_dpto", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_dpto);
                    db.AddParameter("@vi_ch_cod_prov", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_provincia);
                    db.AddParameter("@vi_ch_cod_dist", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_distrito);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_estado);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_usu_crea);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_usuario_red);

                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;

        }
        public Int32 Modificar(DestinoBE oDestinoBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[sgsnet_spu_destino]";

                    db.AddParameter("@vi_in_id_ubica", DbType.Int32, ParameterDirection.Input, oDestinoBE.Id_ubicacion);
                    db.AddParameter("@vi_va_nom_ubicacion", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_ubicacion);
                    db.AddParameter("@vi_va_nom_ubicacion_corta", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_corto_ubicacion);
                    db.AddParameter("@vi_ch_cod_tipo_ubicacion", DbType.String, ParameterDirection.Input, oDestinoBE.Tipo_ubicacion);
                    db.AddParameter("@vi_va_ruc", DbType.String, ParameterDirection.Input, oDestinoBE.Nro_ruc);
                    db.AddParameter("@vi_va_direcion", DbType.String, ParameterDirection.Input, oDestinoBE.Direccion);
                    db.AddParameter("@vi_ch_cod_dpto", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_dpto);
                    db.AddParameter("@vi_ch_cod_prov", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_provincia);
                    db.AddParameter("@vi_ch_cod_dist", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_distrito);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_estado);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_usu_crea);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_usuario_red);

                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;

        }
        
        public Int32 Eliminar(DestinoBE oDestinoBE)
        {
            Int32 res = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[sgsnet_spd_destino]";

                    db.AddParameter("@vi_in_id_ubica", DbType.Int32, ParameterDirection.Input, oDestinoBE.Id_ubicacion);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oDestinoBE.Cod_usu_crea);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_estacion);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oDestinoBE.Nom_usuario_red);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch { throw; }
            return res;
        }
        public DestinoBEList Listar(DestinoBE oBE)
        {
            DestinoBEList Lista = new DestinoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_destino";
                    db.AddParameter("@vi_va_cod_tipo_ubicacion", DbType.String, ParameterDirection.Input, oBE.Tipo_ubicacion);
                    db.AddParameter("@vi_va_ruc", DbType.String, ParameterDirection.Input, oBE.Nro_ruc);
                    db.AddParameter("@vi_va_ubicacion", DbType.String, ParameterDirection.Input, oBE.Nom_ubicacion);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oBE.Cod_estado);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    Lista.Add(CrearEntidad(DReader));
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return Lista;

        }
        public DestinoBE ListarById(Int32 id_Destino)
        {
            DestinoBE oDestinoBE = null;

            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_destino_x_id";
                    db.AddParameter("@vi_in_id_ubica", DbType.Int32, ParameterDirection.Input, id_Destino);
                    DReader = db.GetDataReader();
                }

                if (DReader.Read())
                {
                    oDestinoBE = CrearEntidadXiD(DReader);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return oDestinoBE;

        }
        private DestinoBE CrearEntidad(IDataReader DReader)
        {
            DestinoBE oDestinoBE = new DestinoBE();
            int indice;

            indice = DReader.GetOrdinal("id_ubicacion");
            oDestinoBE.Id_ubicacion = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("tipo_ubicacion");
            oDestinoBE.Tipo_ubicacion = DReader.IsDBNull(indice) ? null : DReader.GetString(indice);
            indice = DReader.GetOrdinal("direccion");
            oDestinoBE.Direccion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_ubicacion");
            oDestinoBE.Nom_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_corto_ubicacion");
            oDestinoBE.Nom_corto_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nro_ruc");
            oDestinoBE.Nro_ruc = DReader.IsDBNull(indice) ? null : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_dpto");
            oDestinoBE.Nom_dpto = DReader.IsDBNull(indice) ? null : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_dpto");
            oDestinoBE.Cod_dpto = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_provincia");
            oDestinoBE.Nom_provincia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_provincia");
            oDestinoBE.Cod_provincia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_distrito");
            oDestinoBE.Cod_distrito = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nom_distrito");
            oDestinoBE.Nom_distrito = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("fec_creacion");
            if (!DReader.IsDBNull(indice)) oDestinoBE.Fec_creacion = DReader.GetDateTime(indice);
            indice = DReader.GetOrdinal("cod_usu_crea");
            oDestinoBE.Cod_usu_crea = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("estado");
            if (!DReader.IsDBNull(indice)) oDestinoBE.Dsc_estado = DReader.GetString(indice);
            indice = DReader.GetOrdinal("cod_estado");
            oDestinoBE.Cod_estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("nombredireccion");
            oDestinoBE.nombredireccion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oDestinoBE;
        }
        private DestinoBE CrearEntidadXiD(IDataReader DReader)
        {
            DestinoBE oDestinoBE = new DestinoBE();
            int indice;

            indice = DReader.GetOrdinal("id_ubicacion");
            oDestinoBE.Id_ubicacion = DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("tipo_ubicacion");
            oDestinoBE.Tipo_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("direccion");
            oDestinoBE.Direccion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_ubicacion");
            oDestinoBE.Nom_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_corto_ubicacion");
            oDestinoBE.Nom_corto_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nro_ruc");
            oDestinoBE.Nro_ruc = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_dpto");
            oDestinoBE.Nom_dpto = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_dpto");
            oDestinoBE.Cod_dpto = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_provincia");
            oDestinoBE.Nom_provincia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_provincia");
            oDestinoBE.Cod_provincia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_distrito");
            oDestinoBE.Cod_distrito = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_distrito");
            oDestinoBE.Nom_distrito = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("fec_creacion");
            if (!DReader.IsDBNull(indice)) oDestinoBE.Fec_creacion = DReader.GetDateTime(indice);

            indice = DReader.GetOrdinal("cod_usu_crea");
            oDestinoBE.Cod_usu_crea = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("estado");
            if (!DReader.IsDBNull(indice)) oDestinoBE.Dsc_estado = DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_estado");
            oDestinoBE.Cod_estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("fec_modi");
            if (!DReader.IsDBNull(indice))
            {
                oDestinoBE.Fec_modi = DReader.GetDateTime(indice);
                oDestinoBE.sfe_modi = oDestinoBE.Fec_modi.ToShortDateString();
            }
            else oDestinoBE.sfe_modi = String.Empty;

            indice = DReader.GetOrdinal("Cod_usu_modi");
            oDestinoBE.Cod_usu_modi = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_estacion");
            oDestinoBE.Nom_estacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_usuario_red");
            oDestinoBE.Nom_usuario_red = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oDestinoBE;
        }
    }
}