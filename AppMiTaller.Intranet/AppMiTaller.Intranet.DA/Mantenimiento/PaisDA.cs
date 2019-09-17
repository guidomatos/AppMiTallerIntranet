using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class PaisDA
    {
        public PaisBE GetById(Int32 id)
        {
            PaisBE oPaisBE = new PaisBE();
            IDataReader DReader;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_sps_pais_x_id";
                db.AddParameter("@vi_int_id_pais", DbType.Int32, ParameterDirection.Input, id);
                DReader = db.GetDataReader();
            }
            if (DReader.Read())
           {
               oPaisBE = CrearEntidadByID(DReader);
            }
            DReader.Close();
            return oPaisBE;
        }
        public PaisBEList GetAll(String nomPais, String codEstado)
        {
            PaisBEList lista = new PaisBEList();

            IDataReader DReader;

            using (Database db = new Database())
            {
                db.ProcedureName = "[sgsnet_sps_bandeja_pais]";

                db.AddParameter("@vi_va_nom_pais", DbType.String, ParameterDirection.Input, nomPais);
                db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, codEstado);

                DReader = db.GetDataReader();
            }
            while (DReader.Read())
            {
                PaisBE oPaisBE = CrearEntidad(DReader);
                lista.Add(oPaisBE);
            }
            DReader.Close();

            return lista;
        }
        public PaisBEList GetListaPais()
        {
            PaisBEList lista = new PaisBEList();            

            IDataReader DReader;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_sps_listado_pais";                
                DReader = db.GetDataReader();
            }
            while (DReader.Read())
            {
                PaisBE oPaisBE = CrearEntidadLista(DReader);
                lista.Add(oPaisBE);
            }
            DReader.Close();

            return lista;
        }
        
        public PaisBEList GetListaPaisTelefono()
        {
            PaisBEList lista = new PaisBEList();

            IDataReader DReader;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_sps_listado_pais_telefono";
                DReader = db.GetDataReader();
            }
            int indice;
            PaisBE oPaisBE = new PaisBE();
            while (DReader.Read())
            {
                oPaisBE = new PaisBE();
                //p.nid_pais AS id_pais
                indice = DReader.GetOrdinal("id_pais");
                oPaisBE.nid_pais = DReader.GetInt32(indice);
                //p.no_pais AS nom_pais
                indice = DReader.GetOrdinal("nom_pais");
                oPaisBE.no_pais = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

                lista.Add(oPaisBE);
            }
            DReader.Close();

            return lista;
        }
        
        public Int32 Insertar(PaisBE oPaisBE)
        {
            Int32 res;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_spi_pais";

                db.AddParameter("@vi_va_nom_pais", DbType.String, ParameterDirection.Input, oPaisBE.no_pais);
                db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oPaisBE.fl_inactivo);
                db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oPaisBE.co_usuario_crea);
                db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oPaisBE.no_estacion);
                db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oPaisBE.no_usuario_red);
                db.AddParameter("@vi_va_nom_ingles", DbType.String, ParameterDirection.Input, oPaisBE.no_pais_ingles);                

                res = Int32.Parse(db.ExecuteScalar().ToString());
            }
            return res;
        }
        public Int32 Modificar(PaisBE oPaisBE)
        {
            Int32 res;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_spu_pais";

                db.AddParameter("@vi_in_id_pais", DbType.Int32, ParameterDirection.Input, oPaisBE.nid_pais);
                db.AddParameter("@vi_va_nom_pais", DbType.String, ParameterDirection.Input, oPaisBE.no_pais);
                db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oPaisBE.fl_inactivo);
                db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oPaisBE.co_usuario_crea);
                db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oPaisBE.no_estacion);
                db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oPaisBE.no_usuario_red);
                db.AddParameter("@vi_va_nom_ingles", DbType.String, ParameterDirection.Input, oPaisBE.no_pais_ingles);
                db.AddParameter("@vi_ch_fecha", DbType.String, ParameterDirection.Input, oPaisBE.sfe_cambio);
                db.AddParameter("@vi_ch_co_autopro", DbType.String, ParameterDirection.Input, oPaisBE.co_auto_ultra);

                res = Int32.Parse(db.ExecuteScalar().ToString());
            }
            return res;
        }
        public Int32 Eliminar(PaisBE oPaisBE)
        {
            Int32 res;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_spd_pais";

                db.AddParameter("@vi_in_id_pais", DbType.Int32, ParameterDirection.Input, oPaisBE.nid_pais);                
                db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oPaisBE.co_usuario_cambio);
                db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oPaisBE.no_estacion);
                db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oPaisBE.no_usuario_red);
                db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oPaisBE.fl_inactivo);
                db.AddParameter("@vi_ch_fecha", DbType.String, ParameterDirection.Input, oPaisBE.sfe_cambio);
                res = Int32.Parse(db.ExecuteScalar().ToString());
            }
            return res;
        }

        private PaisBE CrearEntidad(IDataReader DReader)
        {
            PaisBE oPaisBE = new PaisBE();
            int indice;
            
            indice = DReader.GetOrdinal("id_pais");
            oPaisBE.nid_pais = DReader.GetInt32(indice);
	        
            indice = DReader.GetOrdinal("nom_pais");
            oPaisBE.no_pais = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
	        
            indice = DReader.GetOrdinal("estado");
            oPaisBE.estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            
            indice = DReader.GetOrdinal("nom_pais_ingles");
            oPaisBE.no_pais_ingles = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            
            indice = DReader.GetOrdinal("cod_estado");
            oPaisBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("co_auto_ultra");
            oPaisBE.co_auto_ultra = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oPaisBE;
        }
        private PaisBE CrearEntidadByID(IDataReader DReader)
        {
            PaisBE oPaisBE = new PaisBE();
            int indice;

            indice = DReader.GetOrdinal("id_pais");
            oPaisBE.nid_pais = DReader.GetInt32(indice);
	        
            indice = DReader.GetOrdinal("nom_pais");
            oPaisBE.no_pais = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("fec_creacion");
            if (!DReader.IsDBNull(indice))
            {
                oPaisBE.fe_crea = DReader.GetDateTime(indice);
                oPaisBE.sfe_crea = oPaisBE.fe_crea.ToShortDateString();
            }
            else oPaisBE.sfe_crea = String.Empty;

            indice = DReader.GetOrdinal("cod_usu_crea");
            oPaisBE.co_usuario_crea = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            
            indice = DReader.GetOrdinal("cod_estado");
            oPaisBE.fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("fec_modi");
            if (!DReader.IsDBNull(indice))
            {
                oPaisBE.fe_cambio = DReader.GetDateTime(indice);
                oPaisBE.sfe_cambio = oPaisBE.fe_cambio.ToShortDateString();
            }
            else oPaisBE.sfe_cambio = String.Empty;

            indice = DReader.GetOrdinal("cod_usu_modi");
            oPaisBE.co_usuario_cambio = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_estacion");
            oPaisBE.no_estacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_usuario_red");
            oPaisBE.no_usuario_red = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            
            indice = DReader.GetOrdinal("nom_pais_ingles");
            oPaisBE.no_pais_ingles = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("co_auto_ultra");
            oPaisBE.co_auto_ultra = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oPaisBE;
        }
        private PaisBE CrearEntidadLista(IDataReader DReader)
        {            
            PaisBE oPaisBE = new PaisBE();
            int indice;
            //p.nid_pais AS id_pais
            indice = DReader.GetOrdinal("id_pais");
            oPaisBE.nid_pais = DReader.GetInt32(indice);
            //p.no_pais AS nom_pais
            indice = DReader.GetOrdinal("nom_pais");
            oPaisBE.no_pais = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oPaisBE;
        }
    }
}
