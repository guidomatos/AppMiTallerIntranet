using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class TallerDiasExceptuadosDA
    {

        public TallerDiasExceptuadosBEList GETListarDiasExceptuadosxTaller(int pnid_propietario)
        {
            TallerDiasExceptuadosBEList lista = new TallerDiasExceptuadosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_diasexceptuados_x_idtaller]";
                    db.AddParameter("@nid_propietario", DbType.Int32, ParameterDirection.Input, pnid_propietario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerDiasExceptuadosBE oMaestroTallerDiasExceptuadosBE = CrearTallerDiasExceptuados(DReader);
                    lista.Add(oMaestroTallerDiasExceptuadosBE);
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
        public int GETInsertDiasExceptuadosxTaller(TallerDiasExceptuadosBE ent)
        {
            int nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ins_mae_dia_exceptuado]";

                    db.AddParameter("@nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_propietario);
                    db.AddParameter("@fe_exceptuada", DbType.String, ParameterDirection.Input, ent.fe_exceptuada);
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);
                    db.AddParameter("@nid_dia_exceptuado", DbType.Int32, ParameterDirection.Output, 0);

                    nId = db.Execute();

                }
            }
            catch (Exception)
            {
                //r = 0;
            }
            return 0;
        }
        public int GETDeleteDiasExceptuadosxTaller(int pnid_dia_exceptuado)
        {
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_del_mae_dia_exceptuado]";
                    db.AddParameter("@nid_dia_exceptuado", DbType.Int32, ParameterDirection.Input, pnid_dia_exceptuado);

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
        private TallerDiasExceptuadosBE CrearTallerDiasExceptuados(IDataRecord DReader)
        {
            TallerDiasExceptuadosBE Entidad = new TallerDiasExceptuadosBE();
            int indice;
            //string var1 = "";
            //int var2 = 0;

            indice = DReader.GetOrdinal("nid_dia_exceptuado");
            Entidad.nid_dia_exceptuado = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_propietario");
            Entidad.nid_propietario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("fe_exceptuada");
            Entidad.fe_exceptuada = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fl_tipo");
            Entidad.fl_tipo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fl_activo");
            Entidad.fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        #endregion
    }
}