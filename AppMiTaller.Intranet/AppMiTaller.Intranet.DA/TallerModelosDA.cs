using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class TallerModelosDA
    {

        public TallerModelosBEList GETListarModelosxTaller(int pnid_taller)
        {
            TallerModelosBEList lista = new TallerModelosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_modelos_x_idtaller]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, pnid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerModelosBE oMaestroTallerModelosBE = CrearTallerModelo(DReader);
                    lista.Add(oMaestroTallerModelosBE);
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
        public int GETInsertModelosxTaller(TallerModelosBE ent)
        {
            int nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ins_mae_taller_modelo]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, ent.nid_modelo);
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return 0;
        }
        public int GETDeleteModelosxTaller(int pnid_taller, int pnid_modelo)
        {
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_del_mae_taller_modelo]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, pnid_taller);
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, pnid_modelo);

                    db.Execute();

                }
            }
            catch (Exception)
            {
                //r = 0;
            }
            return 0;
        } 
        #region "Populate"
        private TallerModelosBE CrearTallerModelo(IDataRecord DReader)
        {
            TallerModelosBE Entidad = new TallerModelosBE();
            int indice;
            //string var1 = "";
            //int var2 = 0;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("co_modelo");
            Entidad.co_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_marca");
            Entidad.nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("co_marca");
            Entidad.co_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        #endregion
    }
}