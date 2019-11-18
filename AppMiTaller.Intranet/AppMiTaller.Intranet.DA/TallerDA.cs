using System;
using System.Data;
using AppMiTaller.Intranet.BE;


namespace AppMiTaller.Intranet.DA
{
    public class TallerDA
    {

        public String ExisteCodigo(TallerBE ent)
        {
            String existe = "0";
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Existe_Codigo_Taller_BO]";
                    db.AddParameter("@vi_co_taller", DbType.String, ParameterDirection.Input, ent.co_taller);
                    existe = db.ExecuteScalar().ToString();
                }
            }
            catch (Exception)
            {
                existe = "0";
            }
            return existe;
        }

        public TallerBEList GETListarTalleres(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_TALLERES_POR_PARAMETROS_BO]";
                    db.AddParameter("@vi_Nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_co_perfil_usuario", DbType.String, ParameterDirection.Input, ent.Co_perfil_usuario);
                    db.AddParameter("@vi_co_taller", DbType.String, ParameterDirection.Input, ent.co_taller);
                    db.AddParameter("@vi_no_taller", DbType.String, ParameterDirection.Input, ent.no_taller);
                    db.AddParameter("@vi_coddpto", DbType.String, ParameterDirection.Input, ent.coddpto);
                    db.AddParameter("@vi_codprov", DbType.String, ParameterDirection.Input, ent.codprov);
                    db.AddParameter("@vi_coddist", DbType.String, ParameterDirection.Input, ent.coddist);
                    db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.nid_ubica);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidadTaller1(DReader);
                    lista.Add(oMaestroTallerBE);
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

        public TallerBEList GETDetallePorPuntoRed(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_DetallePuntoRed_BO]";
                    db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.nid_ubica);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidad_DetallePuntoRed(DReader);
                    lista.Add(oMaestroTallerBE);
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

        private TallerBE CrearEntidadUbicacion(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;
            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_ubica");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        public TallerBEList GETListarUbicacion(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ubicacion_BO]";
                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_perfil_usuario);
                    db.AddParameter("@vi_Nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidadUbicacion(DReader);
                    lista.Add(oMaestroTallerBE);
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

        public TallerBEList GETDetalleTaller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_DetallePorTaller_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidad_DetalleTaller(DReader);
                    lista.Add(oMaestroTallerBE);
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

        public TallerBEList GETDiasExcepPorTaller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_DiasExceptuadosPorTaller_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidad_DiasExcepPotTaller(DReader);
                    lista.Add(oMaestroTallerBE);
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

        public TallerBEList GETDiasHabiles_Hora_PorTaller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_horario_x_idtaller_BO]";
                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidad_DiasHabiles_Hora_PorTaller(DReader);
                    lista.Add(oMaestroTallerBE);
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

        public TallerBEList GETModelosPorTaller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_modelos_x_idtaller_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidad_ModelosPorTaller(DReader);
                    lista.Add(oMaestroTallerBE);
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

        public TallerBEList GETServiciosPorTaller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SERVICIOS_X_IDTALLER_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidad_ServPorTaller(DReader);
                    lista.Add(oMaestroTallerBE);
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
        public Int32 GETInsertarTaller(TallerBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_taller_BO]";

                    db.AddParameter("@vi_co_taller", DbType.String, ParameterDirection.Input, ent.co_taller);
                    db.AddParameter("@vi_no_taller", DbType.String, ParameterDirection.Input, ent.no_taller);
                    db.AddParameter("@vi_descripcion", DbType.String, ParameterDirection.Input, ent.descripcion);
                    db.AddParameter("@vi_co_intervalo_atenc", DbType.Int32, ParameterDirection.Input, ent.Cod_intervalo);
                    db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.nid_ubica);
                    db.AddParameter("@vi_no_direccion", DbType.String, ParameterDirection.Input, ent.no_direccion);
                    db.AddParameter("@vi_tx_mapa_taller", DbType.String, ParameterDirection.Input, ent.tx_mapa_taller);
                    db.AddParameter("@vi_tx_url_taller", DbType.String, ParameterDirection.Input, ent.tx_url_taller);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);
                    db.AddParameter("@vo_nid_taller", DbType.Int32, ParameterDirection.Output, 0);

                    db.Execute();
                    nId = Convert.ToInt32(db.GetParameter("@vo_nid_taller"));
                    return nId;
                }
            }
            catch (Exception ex)
            {
                nId = 0;
            }
            return nId;
        }

        public Int32 GETInsertarTallerHorario(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_horario_BO]";

                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_taller);

                    if (ent.Dd_atencion == 0)
                        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);

                    if (String.IsNullOrEmpty(ent.HoraInicio))
                        db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, ent.HoraInicio);

                    if (String.IsNullOrEmpty(ent.HoraFin))
                        db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, ent.HoraFin);

                    if (String.IsNullOrEmpty(ent.Fl_tipo))
                        db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public Int32 GETInsertarTallerDiaExceptuado(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_dia_exceptuado_BO]";
                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_fe_exceptuada", DbType.DateTime, ParameterDirection.Input, ent.Fe_exceptuada);
                    db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, ent.Fl_tipo);
                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);
                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public Int32 GETInsertarTallerServicio(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPI_MAE_TALLER_SERVICIO_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_servicio", DbType.Int32, ParameterDirection.Input, ent.Nid_serv);
                    db.AddParameter("@vi_no_dias", DbType.String, ParameterDirection.Input, ent.no_dias);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return res;
        }

        public Int32 GETInsertarTallerModelo(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_taller_modelo_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_modelo", DbType.Int32, ParameterDirection.Input, ent.Nid_modelo);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return res;
        }

        public Int32 GETInsertarTallerModeloCapacidad(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPI_MAE_TALLER_MODELO_CAPACIDAD_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_modelo", DbType.Int32, ParameterDirection.Input, ent.Nid_modelo);
                    db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);
                    db.AddParameter("@vi_qt_capacidad", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }

        public Int32 ActualizarTaller(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_mae_taller_BO]";

                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);

                    if (String.IsNullOrEmpty(ent.co_taller))
                        db.AddParameter("@vi_co_taller", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_taller", DbType.String, ParameterDirection.Input, ent.co_taller);

                    if (String.IsNullOrEmpty(ent.no_taller))
                        db.AddParameter("@vi_no_taller", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_taller", DbType.String, ParameterDirection.Input, ent.no_taller);

                    if (ent.Cod_intervalo == 0)
                        db.AddParameter("@vi_co_intervalo_atenc", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_intervalo_atenc", DbType.Int32, ParameterDirection.Input, ent.Cod_intervalo);

                    if (ent.nid_ubica == 0)
                        db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.nid_ubica);

                    if (String.IsNullOrEmpty(ent.no_direccion))
                        db.AddParameter("@vi_no_direccion", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_direccion", DbType.String, ParameterDirection.Input, ent.no_direccion);

                    if (String.IsNullOrEmpty(ent.nu_telefono))
                        db.AddParameter("@vi_nu_telefono", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nu_telefono", DbType.String, ParameterDirection.Input, ent.nu_telefono);

                    if (String.IsNullOrEmpty(ent.tx_mapa_taller))
                        db.AddParameter("@vi_tx_mapa_taller", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_tx_mapa_taller", DbType.String, ParameterDirection.Input, ent.tx_mapa_taller);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);


                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    /*@001 I*/
                    if (String.IsNullOrEmpty(ent.co_valoracion))
                        db.AddParameter("@vi_co_valoracion", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_valoracion", DbType.String, ParameterDirection.Input, ent.co_valoracion);
                    /*@001 F*/

                    /*@002 I*/
                    if (String.IsNullOrEmpty(ent.fl_taxi_BO))
                        db.AddParameter("@vi_fl_taxi_BO", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_taxi_BO", DbType.String, ParameterDirection.Input, ent.fl_taxi_BO);
                    if (String.IsNullOrEmpty(ent.fl_taxi_FO))
                        db.AddParameter("@vi_fl_taxi_FO", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_taxi_FO", DbType.String, ParameterDirection.Input, ent.fl_taxi_FO);
                    /*@002 F*/

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 ActualizarHorario(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_mae_horario_BO]";
                    //db.AddParameter("@vi_nid_horario", DbType.Int32, ParameterDirection.Input, ent.Nid_horario);
                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);
                    db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, ent.HoraInicio);
                    db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, ent.HoraFin);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 ActualizarDiasExcep(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_mae_dia_exceptuado]";
                    db.AddParameter("@nid_dia_exceptuado", DbType.Int32, ParameterDirection.Input, ent.Nid_dia_exceptuado);
                    db.AddParameter("@nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@fe_exceptuada", DbType.DateTime, ParameterDirection.Input, ent.Fe_exceptuada);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 ActualizarTallerServicios(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_mae_taller_servicio]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@nid_servicio", DbType.Int32, ParameterDirection.Input, ent.Nid_serv);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public Int32 ActualizarTallerModelos(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_mae_taller_modelo]";
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, ent.Nid_modelo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 MantenimientoTallerServicios(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SP_MANT_MAE_TALLER_SERVICIO_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_servicio", DbType.Int32, ParameterDirection.Input, ent.Nid_serv);
                    db.AddParameter("@vi_no_dias", DbType.String, ParameterDirection.Input, ent.no_dias);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);


                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    db.AddParameter("@vi_op", DbType.String, ParameterDirection.Input, ent.Op);
                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 MantenimientoTallerDiasExceptuados(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sp_mant_mae_dia_exceptuado_BO]";

                    db.AddParameter("@vi_nid_dia_exceptuado", DbType.Int32, ParameterDirection.Input, ent.Nid_dia_exceptuado);
                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_fe_exceptuada", DbType.DateTime, ParameterDirection.Input, ent.Fe_exceptuada);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    db.AddParameter("@vi_op", DbType.String, ParameterDirection.Input, ent.Op);
                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 MantenimientoTallerModelos(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sp_mant_mae_taller_modelo_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_modelo", DbType.Int32, ParameterDirection.Input, ent.Nid_modelo);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.Co_usuario_modi))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_modi);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    db.AddParameter("@vi_op", DbType.String, ParameterDirection.Input, ent.Op);
                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 MantenimientoTallerModelosCapacidad(TallerBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_MAE_TALLER_MODELO_CAPACIDAD_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_modelo", DbType.Int32, ParameterDirection.Input, ent.Nid_modelo);
                    db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);
                    db.AddParameter("@vi_qt_capacidad", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad);

                    if (String.IsNullOrEmpty(ent.co_usuario))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);

                    if (String.IsNullOrEmpty(ent.co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    if (String.IsNullOrEmpty(ent.fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        public TallerBEList GETListarUbigeo(int Nid_usuario)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ubigeo_BO]";
                    db.AddParameter("@vi_Nid_usuario", DbType.Int32, ParameterDirection.Input, Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMaestroTallerBE = CrearEntidadUbigeo(DReader);
                    lista.Add(oMaestroTallerBE);
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

        private TallerBE CrearEntidadUbigeo(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nombre");
            Entidad.Ubigeo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        public CombosBEList GETListarDepartamento(int Nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_departamento]";
                    db.AddParameter("@Nid_usuario", DbType.String, ParameterDirection.Input, Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ComboBE oCombosBE = CrearEntidad_Combo3(DReader);
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
        public CombosBEList GETListarProvincia(TallerBE ent, int Nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_provincia]";
                    db.AddParameter("@coddpto", DbType.String, ParameterDirection.Input, ent.coddpto);
                    db.AddParameter("@Nid_usuario", DbType.String, ParameterDirection.Input, Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ComboBE oCombosBE = CrearEntidad_Combo3(DReader);
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
        public CombosBEList GETListarDistrito(TallerBE ent, int Nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_distrito]";
                    db.AddParameter("@coddpto", DbType.String, ParameterDirection.Input, ent.coddpto);
                    db.AddParameter("@codprov", DbType.String, ParameterDirection.Input, ent.codprov);
                    db.AddParameter("@Nid_usuario", DbType.String, ParameterDirection.Input, Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ComboBE oCombosBE = CrearEntidad_Combo3(DReader);
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

        public CombosBEList GETListarPuntoRed(TallerBE ent, int Nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_puntored]";
                    db.AddParameter("@vi_coddpto", DbType.String, ParameterDirection.Input, ent.coddpto);
                    db.AddParameter("@vi_codprov", DbType.String, ParameterDirection.Input, ent.codprov);
                    db.AddParameter("@vi_coddist", DbType.String, ParameterDirection.Input, ent.coddist);
                    db.AddParameter("@Nid_usuario", DbType.String, ParameterDirection.Input, Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ComboBE oCombosBE = CrearEntidad_Combo2(DReader);
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
        public CombosBEList GETListarDireccionPuntoRed(Int32 pnid_ubica)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_direccion_puntored]";
                    db.AddParameter("@nid_ubica", DbType.Int32, ParameterDirection.Input, pnid_ubica);
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
        public CombosBEList GETListarIntervaloCita()
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_IntervaloCita]";
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

        public TallerBEList GETListarDiasDisp()
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_DiasDisponibles]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oDiasDispBE = CrearEntidad_DiasDisp(DReader);
                    lista.Add(oDiasDispBE);
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

        public TallerBEList GETListarFeriados()
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Feriado_Taller_BO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oDiasDisp = CrearEntidad_Feriados(DReader);
                    lista.Add(oDiasDisp);
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

        public TallerBEList GETListarServicios()
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Listado_TiposServ_ServEspec_BO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oServiciosBE = CrearEntidad_Servicios(DReader);
                    lista.Add(oServiciosBE);
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
        public TallerBEList GETListarIntervalosAtencion()
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_INTERVALOS_HORAS_TALLER_BO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oIntervAtencBE = CrearEntidad_IntervAtenc(DReader);
                    lista.Add(oIntervAtencBE);
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
        public TallerBEList GETListarMarcaModelo(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_LISTADO_MARCA_MODELO_BO]";
                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_perfil_usuario);
                    db.AddParameter("@vi_Nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMarcaModeloBE = CrearEntidad_MarcaModelo(DReader);
                    lista.Add(oMarcaModeloBE);
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


        public TallerBEList GETListarCapacidadTallerModelo(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_MAE_TALLER_MODELO_CAPACIDAD_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.String, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oMarcaModeloBE = CrearEntidad_CapacidadTallerModelo(DReader);
                    lista.Add(oMarcaModeloBE);
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




        public CombosBEList GETListarModelo(Int32 pnid_marca)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_listado_modelo_x_marca]";
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, pnid_marca);
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

        /* ULTIMO */

        public TallerBEList ListarDias_Taller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_DIAS_POR_TALLER_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oServiciosBE = CrearEntidad_DiasTaller(DReader);
                    lista.Add(oServiciosBE);
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

        private TallerBE CrearEntidad_DiasTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("dd_atencion");
            Entidad.Dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }
        public TallerBEList ListarHorario_Taller(TallerBE ent)
        {
            TallerBEList lista = new TallerBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_HORARIO_TALLER_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TallerBE oServiciosBE = CrearEntidad_HorariosTaller(DReader);
                    lista.Add(oServiciosBE);
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
        private ComboBE CrearEntidad_Combo3(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            Int32 indice;

            indice = DReader.GetOrdinal("ID");
            Entidad.ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private TallerBE CrearEntidad_Feriados(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("ID");
            Entidad.ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private TallerBE CrearEntidad_DiasDisp(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("no_valor1");
            Entidad.No_valor1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private TallerBE CrearEntidad_IntervAtenc(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("Cod_intervalo");
            Entidad.Cod_intervalo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("NUM_INTERVALO");
            Entidad.Num_intervalo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private TallerBE CrearEntidad_Servicios(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.Nid_tserv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.No_tserv = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_servicio");
            Entidad.Nid_serv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_servicio");
            Entidad.No_serv = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private TallerBE CrearEntidad_MarcaModelo(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_marca");
            Entidad.Nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_marca");
            Entidad.No_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.Nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_modelo");
            Entidad.No_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private ComboBE CrearEntidad_Combo(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            Int32 indice;

            indice = DReader.GetOrdinal("ID");
            Entidad.ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private ComboBE CrearEntidad_Combo2(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            Int32 indice;
            //Int32 var;

            indice = DReader.GetOrdinal("ID");
            Entidad.IntID = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_DiasHabiles_Hora_PorTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("dd_atencion");
            Entidad.Dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.HoraInicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("ho_fin");
            Entidad.HoraFin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_ModelosPorTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_marca");
            Entidad.Nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_marca");
            Entidad.No_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.Nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_modelo");
            Entidad.No_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_ServPorTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.Nid_tserv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.No_tserv = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_servicio");
            Entidad.Nid_serv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.No_serv = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_dias");
            Entidad.no_dias = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_DiasExcepPotTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("fe_exceptuada");
            Entidad.Fe_exceptuada1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_DetallePuntoRed(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("di_ubica");
            Entidad.Di_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            //indice = DReader.GetOrdinal("nu_telefono");
            //Entidad.Nu_telefono_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_DetalleTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("co_taller");
            Entidad.co_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("co_intervalo_atenc");
            Entidad.co_intervalo_atenc = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_direccion");
            Entidad.no_direccion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("tx_mapa_taller");
            Entidad.tx_mapa_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fl_activo");
            Entidad.fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_valoracion");
            Entidad.co_valoracion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fl_taxi_bo");
            Entidad.fl_taxi_BO = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fl_taxi_fo");
            Entidad.fl_taxi_FO = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }
        private TallerBE CrearEntidadTaller1(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("co_taller");
            Entidad.co_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nomdpto");
            Entidad.nomdpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nomprov");
            Entidad.nomprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nomdist");
            Entidad.nomdist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_ubica");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fl_activo");
            Entidad.fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_direccion");
            Entidad.no_direccion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        
        private TallerBE CrearEntidad_HorariosTaller(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.HoraInicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("ho_fin");
            Entidad.HoraFin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private TallerBE CrearEntidad_CapacidadTallerModelo(IDataRecord DReader)
        {
            TallerBE Entidad = new TallerBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.Nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("dd_atencion");
            Entidad.Dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("qt_capacidad");
            Entidad.qt_capacidad = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }
        #endregion
    }
}