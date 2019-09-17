using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class TallerServiciosDA
    {

        public TallerServiciosBEList GETListarServiciosxTaller(int pnid_taller)
        {
            TallerServiciosBEList lista = new TallerServiciosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_servicios_x_idtaller]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, pnid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerServiciosBE oMaestroTallerServiciosBE = CrearTallerServicios(DReader);
                    lista.Add(oMaestroTallerServiciosBE);
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
        public int GETInsertServiciosxTaller(TallerServiciosBE ent)
        {
            //IDataReader DReader = null;
            //int nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ins_mae_taller_servicio]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@nid_servicio", DbType.Int32, ParameterDirection.Input, ent.nid_servicio);
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    db.Execute();
                }
            }
            catch (Exception)
            {
                //r = 0;
            }
            return 0;
        }
        public int GETDeleteServiciosxTaller(int pnid_taller, int pnid_servicio)
        {
            //IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_del_mae_taller_servicio]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, pnid_taller);
                    db.AddParameter("@nid_servicio", DbType.Int32, ParameterDirection.Input, pnid_servicio);

                    db.Execute();

                }
            }
            catch //(Exception)
            {
                //r = 0;
            }
            return 0;
        } 




        #region "Populate"
        private TallerServiciosBE CrearTallerServicios(IDataRecord DReader)
        {
            TallerServiciosBE Entidad = new TallerServiciosBE();
            int indice;
            //string var1 = "";
            //int var2 = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_servicio");
            Entidad.nid_servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("co_servicio");
            Entidad.co_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_servicio");
            Entidad.no_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.nid_tipo_servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("co_tipo_servicio");
            Entidad.co_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.no_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        #endregion
    }
}