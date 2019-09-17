using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class TallerHorariosExcepcionalDA
    {
        public TallerHorariosExcepcionalBEList GetListHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            TallerHorariosExcepcionalBEList lista = new TallerHorariosExcepcionalBEList();
            IDataReader DReader=null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_TllrHorExcepcional";
                    db.AddParameter("@VI_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_propietario);
                    db.AddParameter("@VI_no_descripcion", DbType.String, ParameterDirection.Input, ent.VI_no_descripcion);
                    db.AddParameter("@VI_fe_inicio", DbType.String, ParameterDirection.Input, ent.VI_fe_inicio);
                    db.AddParameter("@VI_fe_fin", DbType.String, ParameterDirection.Input, ent.VI_fe_fin);
                    db.AddParameter("@VI_fl_activo", DbType.String, ParameterDirection.Input, ent.VI_fl_activo);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosExcepcionalBE oMaestroTallerHorariosExcepcionalBE = CrearGetListHorarioExcepcional(DReader);
                    lista.Add(oMaestroTallerHorariosExcepcionalBE);
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

        public TallerHorariosExcepcionalBEList GetListHorarioExcepcionalXHorario(TallerHorariosExcepcionalBE ent)
        {
            TallerHorariosExcepcionalBEList lista = new TallerHorariosExcepcionalBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_HorExcepCabeXHorario";
                    db.AddParameter("@VI_nid_horario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_horario_HECabe);                    
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosExcepcionalBE oMaestroTallerHorariosExcepcionalBE = CrearGetListHorarioExcepcional(DReader);
                    lista.Add(oMaestroTallerHorariosExcepcionalBE);
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

        public TallerHorariosExcepcionalBEList GetListDetaHorarioExcepcionalXHorario(TallerHorariosExcepcionalBE ent)
        {
            TallerHorariosExcepcionalBEList lista = new TallerHorariosExcepcionalBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_HorExcepDetaXHorario";
                    db.AddParameter("@VI_nid_horario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_horario_HECabe);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosExcepcionalBE oMaestroTallerHorariosExcepcionalBE = CrearGetListHorExcepDetaXHorario(DReader);
                    lista.Add(oMaestroTallerHorariosExcepcionalBE);
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

        public TallerHorariosExcepcionalBEList GetListDiasXTllrHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            TallerHorariosExcepcionalBEList lista = new TallerHorariosExcepcionalBEList();
            IDataReader DReader=null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_DiasXTallerHorExcep";
                    db.AddParameter("@VI_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_propietario);                   
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosExcepcionalBE oMaestroTallerHorariosExcepcionalBE = CrearGetListDiasXTllrHorarioExcepcional(DReader);
                    lista.Add(oMaestroTallerHorariosExcepcionalBE);
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

        public TallerHorariosExcepcionalBEList GetListHorXDiaXTllrHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            TallerHorariosExcepcionalBEList lista = new TallerHorariosExcepcionalBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_HoraXDiaXTallerHorExcep";
                    db.AddParameter("@VI_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_propietario);
                    db.AddParameter("@VI_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.VI_dd_atencion); 
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerHorariosExcepcionalBE oMaestroTallerHorariosExcepcionalBE = CrearGetListHorXDiaXTllrHorarioExcepcional(DReader);
                    lista.Add(oMaestroTallerHorariosExcepcionalBE);
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

        public Int32 InsertarCabeHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPI_HorExcepCabeXTaller";
                    db.AddParameter("@VI_nid_horario", DbType.Int32, ParameterDirection.Output, 0);
                    db.AddParameter("@VI_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_propietario_HECabe);
                    db.AddParameter("@VI_no_descripcion", DbType.String, ParameterDirection.Input, ent.VI_no_descripcion_HECabe);
                    db.AddParameter("@VI_fe_inicio", DbType.String, ParameterDirection.Input, ent.VI_fe_inicio_HECabe);
                    db.AddParameter("@VI_fe_fin", DbType.String, ParameterDirection.Input, ent.VI_fe_fin_HECabe);
                    db.AddParameter("@VI_fl_tipo", DbType.String, ParameterDirection.Input, ent.VI_fl_tipo_HECabe);
                    db.AddParameter("@VI_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_crea_HECabe);
                    db.AddParameter("@VI_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_modi_HECabe);
                    db.AddParameter("@VI_co_usuario_red", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_red_HECabe);
                    db.AddParameter("@VI_no_estacion_red", DbType.String, ParameterDirection.Input, ent.VI_no_estacion_red_HECabe);
                    db.AddParameter("@VI_fl_activo", DbType.String, ParameterDirection.Input, ent.VI_fl_activo_HECabe);
                    db.Execute();
                    nId = Convert.ToInt32(db.GetParameter("@VI_nid_horario"));
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 ActualizarCabeHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPU_HorExcepCabeXTaller";
                    db.AddParameter("@VI_nid_horario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_horario_HECabe);
                    db.AddParameter("@VI_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_propietario_HECabe);
                    db.AddParameter("@VI_no_descripcion", DbType.String, ParameterDirection.Input, ent.VI_no_descripcion_HECabe);
                    db.AddParameter("@VI_fe_inicio", DbType.String, ParameterDirection.Input, ent.VI_fe_inicio_HECabe);
                    db.AddParameter("@VI_fe_fin", DbType.String, ParameterDirection.Input, ent.VI_fe_fin_HECabe);
                    db.AddParameter("@VI_fl_tipo", DbType.String, ParameterDirection.Input, ent.VI_fl_tipo_HECabe);
                    db.AddParameter("@VI_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_crea_HECabe);
                    db.AddParameter("@VI_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_modi_HECabe);
                    db.AddParameter("@VI_co_usuario_red", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_red_HECabe);
                    db.AddParameter("@VI_no_estacion_red", DbType.String, ParameterDirection.Input, ent.VI_no_estacion_red_HECabe);
                    db.AddParameter("@VI_fl_activo", DbType.String, ParameterDirection.Input, ent.VI_fl_activo_HECabe);
                    nId=db.Execute();                     
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 InsertarDetaHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPI_HorExcepDetaXTaller";
                    db.AddParameter("@VI_nid_horario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_horario_HEDeta);
                    db.AddParameter("@VI_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.VI_dd_atencion_HEDeta);
                    db.AddParameter("@VI_ho_rango1", DbType.String, ParameterDirection.Input, ent.VI_ho_rango1_HEDeta);
                    db.AddParameter("@VI_ho_rango2", DbType.String, ParameterDirection.Input, ent.VI_ho_rango2_HEDeta);
                    db.AddParameter("@VI_ho_rango3", DbType.String, ParameterDirection.Input, ent.VI_ho_rango3_HEDeta);
                    db.AddParameter("@VI_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_crea_HEDeta);
                    db.AddParameter("@VI_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_modi_HEDeta);
                    db.AddParameter("@VI_co_usuario_red", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_red_HEDeta);
                    db.AddParameter("@VI_no_estacion_red", DbType.String, ParameterDirection.Input, ent.VI_no_estacion_red_HEDeta);
                    db.AddParameter("@VI_fl_activo", DbType.String, ParameterDirection.Input, ent.VI_fl_activo_HEDeta);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 EliminarDetaHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPD_HorExcepDetaXTaller";
                    db.AddParameter("@VI_nid_horario", DbType.Int32, ParameterDirection.Input, ent.VI_nid_horario_HEDeta);
                    db.AddParameter("@VI_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_modi_HEDeta);
                    db.AddParameter("@VI_co_usuario_red", DbType.String, ParameterDirection.Input, ent.VI_co_usuario_red_HEDeta);
                    db.AddParameter("@VI_no_estacion_red", DbType.String, ParameterDirection.Input, ent.VI_no_estacion_red_HEDeta);
                    db.AddParameter("@VI_fl_activo", DbType.String, ParameterDirection.Input, ent.VI_fl_activo_HEDeta);
                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        #region "POPULATE"

        private TallerHorariosExcepcionalBE CrearGetListHorXDiaXTllrHorarioExcepcional(IDataRecord DReader)
        {
            TallerHorariosExcepcionalBE Entidad = new TallerHorariosExcepcionalBE();
            int indice;
            string var1 = "";
            //int var2 = 0;

            indice = DReader.GetOrdinal("TllrHorIniAten");
            var1 = DReader.GetString(indice);
            Entidad.TllrHorIniAten = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("TllrHorfinAten");
            var1 = DReader.GetString(indice);
            Entidad.TllrHorfinAten = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("TllrIntervaloAten");
            var1 = DReader.GetString(indice);
            Entidad.TllrIntervaloAten = (var1 == null ? "" : var1);

            return Entidad;
        }

        private TallerHorariosExcepcionalBE CrearGetListDiasXTllrHorarioExcepcional(IDataRecord DReader)
        {
            TallerHorariosExcepcionalBE Entidad = new TallerHorariosExcepcionalBE();
            int indice;

            indice = DReader.GetOrdinal("dd_atencion");
            Entidad.VI_nid_propietario = DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice);

            return Entidad;
        }

        private TallerHorariosExcepcionalBE CrearGetListHorExcepDetaXHorario(IDataRecord DReader)
        {
            TallerHorariosExcepcionalBE Entidad = new TallerHorariosExcepcionalBE();
            int indice;
            //string var1 = "";
            //int var2 = 0;

            indice = DReader.GetOrdinal("DetIdDia");
            Entidad.DetIdDia = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("DetHo_rango1");
            Entidad.DetHo_rango1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("DetHo_rango2");
            Entidad.DetHo_rango2 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("DetHo_rango3");
            Entidad.DetHo_rango3 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerHorariosExcepcionalBE CrearGetListHorarioExcepcional(IDataRecord DReader)
        {
            TallerHorariosExcepcionalBE Entidad = new TallerHorariosExcepcionalBE();
            int indice;
            //string var1 = "";
            //int var2 = 0;

            indice = DReader.GetOrdinal("grid_cod_hor_excep");
            Entidad.grid_cod_hor_excep = (DReader.IsDBNull(indice) ? "0" : DReader.GetInt32(indice).ToString());

            indice = DReader.GetOrdinal("grid_codTllr_hor_excep");
            Entidad.grid_codTllr_hor_excep = (DReader.IsDBNull(indice) ? "0" : DReader.GetInt32(indice).ToString());

            indice = DReader.GetOrdinal("grid_des_hor_excep");
            Entidad.grid_des_hor_excep = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("grid_fecini_hor_excep");
            Entidad.grid_fecini_hor_excep = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("grid_fecfin_hor_excep");
            Entidad.grid_fecfin_hor_excep = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("grid_idestado_hor_excep");
            Entidad.grid_idestado_hor_excep = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("grid_estado_hor_excep");
            Entidad.grid_estado_hor_excep = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        #endregion
    }
}
