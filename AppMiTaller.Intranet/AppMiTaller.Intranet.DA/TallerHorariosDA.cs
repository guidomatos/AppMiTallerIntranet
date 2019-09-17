using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class TallerHorariosDA
    {

        public TallerHorariosBEList GETListarHorariosxTaller(int pnid_propietario)
        {
            TallerHorariosBEList lista = new TallerHorariosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_horario_x_idtaller]";
                    db.AddParameter("@nid_propietario", DbType.Int32, ParameterDirection.Input, pnid_propietario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosBE oMaestroTallerHorariosBE = CrearTallerHorario(DReader);
                    lista.Add(oMaestroTallerHorariosBE);
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
        public int GETInsertHorariosxTaller(TallerHorariosBE ent)
        {
            //IDataReader DReader = null;
            int nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ins_mae_horario]";
                    db.AddParameter("@nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_propietario);
                    db.AddParameter("@dd_atencion", DbType.Int32, ParameterDirection.Input, ent.dd_atencion);
                    db.AddParameter("@ho_inicio", DbType.String, ParameterDirection.Input, ent.ho_inicio);
                    db.AddParameter("@ho_fin", DbType.String, ParameterDirection.Input, ent.ho_fin);
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);
                    db.AddParameter("@nid_horario", DbType.Int32, ParameterDirection.Output, 0);

                    nId = db.Execute();

                }
            }
            catch (Exception)
            {
                //r = 0;
            }
            return 0;
        }
        public int GETDeleteHorariosxTaller(int pnid_horario)
        {
            //IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_del_mae_horario]";
                    db.AddParameter("@nid_horario", DbType.Int32, ParameterDirection.Input, pnid_horario);

                    db.Execute();

                }
            }
            catch (Exception)
            {
                //r = 0;
            }
            return 0;
        } 

        // v2.0

        public Int32 InhabilitarCapacidadAtencion_Taller(TallerHorariosBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_INHABILITAR_CAPACIDAD_ATENCION_BO]";

                    db.AddParameter("@VI_NID_PROPIETARIO", DbType.Int32, ParameterDirection.Input, ent.nid_propietario);
                    db.AddParameter("@VI_FL_TIPO", DbType.String, ParameterDirection.Input, "T");

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }

            return res;
        }

        public Int32 MantenimientoCapacidadAtencion_Taller(TallerHorariosBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPI_CAPACIDAD_ATENCION_TALLER_BO]";

                    db.AddParameter("@VI_NID_TALLER", DbType.Int32, ParameterDirection.Input, ent.nid_propietario);
                    db.AddParameter("@VI_DIA_ATENC", DbType.Int32, ParameterDirection.Input, ent.dd_atencion);
                    db.AddParameter("@VI_FL_CONTROL", DbType.String, ParameterDirection.Input, ent.fl_control);
                   
                    if (ent.qt_capacidad_fo.Equals(-1))
                        db.AddParameter("@VI_CAPACIDAD_FO", DbType.Int32, ParameterDirection.Input, DBNull.Value );                    
                    else
                        db.AddParameter("@VI_CAPACIDAD_FO", DbType.Int32, ParameterDirection.Input,   ent.qt_capacidad_fo);

                    if (ent.qt_capacidad_bo.Equals(-1))
                        db.AddParameter("@VI_CAPACIDAD_BO", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CAPACIDAD_BO", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad_bo);

                    if (ent.qt_capacidad.Equals(-1))
                        db.AddParameter("@VI_CAPACIDAD", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CAPACIDAD", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad);


                    db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@VI_NO_USUARIO_RED", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@VI_NO_ESTACION_RED", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }

            return res;
        }

        public TallerHorariosBEList GETListarCapacidadAtencion_PorTaller(TallerHorariosBE ent)
        {
            TallerHorariosBEList lista = new TallerHorariosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_LISTAR_CAPACIDAD_DIAS_ATENCION_BO]";
                    db.AddParameter("@VI_NID_PROPIETARIO", DbType.Int32, ParameterDirection.Input, ent.nid_propietario);
                    db.AddParameter("@VI_FL_TIPO", DbType.String, ParameterDirection.Input, "T");
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosBE oMaestroUsuariosBE = CrearCapacidadAtencion(DReader);
                    lista.Add(oMaestroUsuariosBE);
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


        #region "Populate"

        private TallerHorariosBE CrearTallerHorario(IDataRecord DReader)
        {
            TallerHorariosBE Entidad = new TallerHorariosBE();
            int indice;
            //string var1 = "";
            //int var2 = 0;
            
            indice = DReader.GetOrdinal("nid_horario");
            Entidad.nid_horario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_propietario");
            Entidad.nid_propietario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("dd_atencion");
            Entidad.dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("ho_fin");
            Entidad.ho_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fl_tipo");
            Entidad.fl_tipo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fl_activo");
            Entidad.fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerHorariosBE CrearCapacidadAtencion(IDataRecord DReader)
        {
            TallerHorariosBE Entidad = new TallerHorariosBE();
            Int32 indice;

            indice = DReader.GetOrdinal("DD_ATENCION");
            Entidad.dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("FL_CONTROL");
            Entidad.fl_control = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("QT_CAPACIDAD_FO");
            Entidad.qt_capacidad_fo = (DReader.IsDBNull(indice) ? -1 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("QT_CAPACIDAD_BO");
            Entidad.qt_capacidad_bo = (DReader.IsDBNull(indice) ? -1 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("QT_CAPACIDAD");
            Entidad.qt_capacidad= (DReader.IsDBNull(indice) ? -1 : DReader.GetInt32(indice));

            return Entidad;
        }
        #endregion
    }
}