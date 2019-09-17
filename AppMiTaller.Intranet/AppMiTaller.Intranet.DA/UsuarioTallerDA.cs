using System;
using System.Data;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;
using System.Data.SqlClient;

namespace AppMiTaller.Intranet.DA
{
    public class UsuarioTallerDA
    {
        public Int32 InsertarAsesorModulo(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SEU_SPI_ASESOR_MODULO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@vi_nid_modulo", ent.nid_modulo);
            cmd.Parameters.AddWithValue("@vi_co_usuario", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    rpta = 1;
            }
            catch //(Exception ex)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }
        public Int32 ActualizarAsesorModulo(CitasBE ent)
        {
            Int32 rpta = 0;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SEU_SPU_ASESOR_MODULO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vi_nid_usuario", ent.nid_usuario);
            cmd.Parameters.AddWithValue("@vi_nid_modulo", ent.nid_modulo);
            cmd.Parameters.AddWithValue("@vi_co_usuario", ent.co_usuario_crea);
            cmd.Parameters.AddWithValue("@vi_co_usuario_red", ent.co_usuario_red);
            cmd.Parameters.AddWithValue("@vi_no_estacion_red", ent.no_estacion_red);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    rpta = 1;
            }
            catch //(Exception ex)
            {
                rpta = 0;
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return rpta;
        }
        public UsuarioBEList GETListarUsuarios(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_usuarios_por_parametros_BO]";                    

                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_Perfil_Login);
                    db.AddParameter("@vi_nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_Usuario_Login);

                    db.AddParameter("@vi_CUSR_ID", DbType.String, ParameterDirection.Input, ent.CUSR_ID);

                    if (String.IsNullOrEmpty(ent.Nu_tipo_documento))
                        db.AddParameter("@vi_nu_tipo_documento", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_nu_tipo_documento", DbType.String, ParameterDirection.Input, ent.Nu_tipo_documento);
                
                        db.AddParameter("@vi_VNOMUSR", DbType.String, ParameterDirection.Input, ent.VNOMUSR);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                  
                        db.AddParameter("@vi_no_ape_paterno", DbType.String, ParameterDirection.Input, ent.No_ape_paterno);

                    if (String.IsNullOrEmpty(ent.No_ape_materno))
                        db.AddParameter("@vi_no_ape_materno", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_no_ape_materno", DbType.String, ParameterDirection.Input, ent.No_ape_materno);

                    if (String.IsNullOrEmpty(ent.Cod_perfil))
                        db.AddParameter("@vi_co_perfil_usuario", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_co_perfil_usuario", DbType.String, ParameterDirection.Input, ent.Cod_perfil);

                    if (String.IsNullOrEmpty(ent.Coddpto))
                        db.AddParameter("@vi_coddpto", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_coddpto", DbType.String, ParameterDirection.Input, ent.Coddpto);

                    if (String.IsNullOrEmpty(ent.Codprov))
                        db.AddParameter("@vi_codprov", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_codprov", DbType.String, ParameterDirection.Input, ent.Codprov);

                    if (String.IsNullOrEmpty(ent.Coddist))
                        db.AddParameter("@vi_coddist", DbType.String, ParameterDirection.Input, "");
                    else
                        db.AddParameter("@vi_coddist", DbType.String, ParameterDirection.Input, ent.Coddist);

                    if (ent.nid_taller == 0)
                        db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, 0);
                    else
                        db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);                                       
                    
                    DReader = db.GetDataReader();
                }

                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadUsuarios(DReader);
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

        public String ExisteLogin(UsuarioBE ent)
        {
            String existe = "0";
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Existe_Login_BO]";
                    db.AddParameter("@vi_CUSR_ID", DbType.String, ParameterDirection.Input, ent.CUSR_ID);
                    existe = db.ExecuteScalar().ToString();
                }
            }
            catch (Exception)
            {
                existe = "0";
            }
            return existe;
        }

        public UsuarioBEList GETListarDetalleUsuarioPorCodigo(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_usuario_por_Codigo_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadUsuarioPorCodigo(DReader);
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

        public UsuarioBEList GETListarTalleresDistrito(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_mae_talleres_mae_distrito_BO]";
                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_Perfil_Login);
                    db.AddParameter("@vi_nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_Usuario_Login);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadTalleresDistrito(DReader);
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

        public UsuarioBEList GETListarHorario_Por_Taller_Dia(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_horario_por_taller_dia_BO]";
                    db.AddParameter("@NID_TALLER", DbType.Int32, ParameterDirection.Input, ent.nid_taller );
                    db.AddParameter("@DIA_ATENC", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion );
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadHoraTallerDia(DReader);
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

        public UsuarioBEList GETListarUbigeo(int nid_usuario)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ubigeo_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadUbigeo(DReader);
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

        private UsuarioBE CrearEntidadHoraTallerDia(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;            
            
            indice = DReader.GetOrdinal("HO_INICIO");
            Entidad.Ho_inicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("HO_FIN");
            Entidad.Ho_fin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("INTERVALO");
            Entidad.No_valor1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private UsuarioBE CrearEntidadUbigeo(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
           
            indice = DReader.GetOrdinal("coddpto");          
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");         
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nombre");
            Entidad.Ubigeo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        public UsuarioBEList GETListarTipoPuntosRedPorDistrito()
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Tipos_Puntos_Red_PorDistrito_BO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadTipoPtoRedDistrito(DReader);
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

        public UsuarioBEList GETListarModelos_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_MODELOS_PORUSUARIO_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_2(DReader);
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

        public UsuarioBEList GETListarPtoRed_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_PuntosDeRed_PorUsuario_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_2(DReader);
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

        public UsuarioBEList GETListarModelo_LineaMarca(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_MODELO_NEGOCIOLINEA_MARCA_BO]";
                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_Perfil_Login);
                    db.AddParameter("@vi_nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_Usuario_Login);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadModelo_LineaMarca(DReader);
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

        public UsuarioBEList GETListarLineaComercialMarca(int nid_usuario)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_LINEACOMERCIAL_MARCA_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadLineaComercialMarca(DReader);
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

        public UsuarioBEList GETListarMarcaEmpresa(int nid_usuario)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_MARCA_EMPRESA_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadMarcaEmpresa(DReader);
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

        public UsuarioBEList GETListarUbicacion(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_ubicacion_BO]";
                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_Perfil_Login);
                    db.AddParameter("@vi_nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_Usuario_Login);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadUbicacion(DReader);
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

        public UsuarioBEList GETListarTipo()
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Tipo_Usuario_BO]";                    
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad(DReader);
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

        public UsuarioBEList GETListarTipoServ_Especifico()
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Tipo_Servicio_BO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oTipoServBE = CrearEntidad_TipoServ_Especifico(DReader);
                    lista.Add(oTipoServBE);
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

        public UsuarioBEList GETListarFeriados()
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Feriado_BO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oDiasDispBE = CrearEntidad(DReader);
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

        public UsuarioBEList GETListarDiasDisp()
        {
            UsuarioBEList lista = new UsuarioBEList();
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
                    UsuarioBE oDiasDispBE = CrearEntidad_DiasDisp(DReader);
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

        private UsuarioBE CrearEntidad_TipoServ_Especifico(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;            

            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.Nid_tipo_servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.No_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_servicio");
            Entidad.Nid_servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_servicio");
            Entidad.No_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            return Entidad;
        }

        private UsuarioBE CrearEntidad_DiasDisp(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;            

            indice = DReader.GetOrdinal("no_valor1");    
            Entidad.No_valor1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        public UsuarioBEList GETListarServicios_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Servicios_PorUsuario_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);                        
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_2(DReader);
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

        public UsuarioBEList GETListarDiasExcep_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Dias_Exceptuados_PorUsuario_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);                        
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad(DReader);
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

        public UsuarioBEList GETListarHorario_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Horarios_PorUsuario_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);                        
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad(DReader);
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

        public UsuarioBEList GETListarTaller_PorUsuario_AsesServ(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Talleres_PorUsuario_AsesServ_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_Talleres_PorUsuario_AsesServ(DReader);
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

        public UsuarioBEList GETListarTalleres_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Talleres_PorUsuario_BO]";
                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_2(DReader);
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

        public UsuarioBEList GETListarPtoRedTaller_PorDistrito(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_PtoRedTaller_PorDistrito_BO]";
                    db.AddParameter("@vi_co_perfil_login", DbType.String, ParameterDirection.Input, ent.Co_Perfil_Login);
                    db.AddParameter("@vi_nid_usuario_login", DbType.Int32, ParameterDirection.Input, ent.Nid_Usuario_Login);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadPtoRedTaller_PorDistrito(DReader);
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

        public UsuarioBEList GETListarTipoDocumento()
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_Tipo_Documento_BO]";                    
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_2(DReader);
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

        public UsuarioBEList GETListarPerfiles(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sps_perfil_usuario_BO]";
                    db.AddParameter("@vi_CUSR_ID", DbType.String, ParameterDirection.Input, ent.CUSR_ID);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidadPerfiles(DReader);
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

        private UsuarioBE CrearEntidadPtoRedTaller_PorDistrito(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;            

            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.Nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_ubica");
            Entidad.No_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
  
            indice = DReader.GetOrdinal("no_taller");
            Entidad.No_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");      
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private UsuarioBE CrearEntidadTipoPuntoRedPorDistrito(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;            

            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.Nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("no_ubica");
            Entidad.No_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private UsuarioBE CrearEntidadModelo_LineaMarca(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;            
            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.Nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.No_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("nid_negocio_linea");
            Entidad.Nid_negocio_linea = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));            

            indice = DReader.GetOrdinal("nid_marca");
            Entidad.Nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));            

            return Entidad;
        }

        private UsuarioBE CrearEntidadLineaComercialMarca(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("nid_negocio_linea");
            Entidad.Nid_negocio_linea = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
       
            indice = DReader.GetOrdinal("linea_comercial");
            Entidad.Linea_comercial = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_marca");
            Entidad.Nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }

        private UsuarioBE CrearEntidadMarcaEmpresa(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("nid_marca");
            Entidad.Nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_marca");
            Entidad.No_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("nid_empresa");
            Entidad.Nid_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("no_empresa");
            Entidad.No_empresa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
     
            return Entidad;
        }

        private UsuarioBE CrearEntidadTipoPtoRedDistrito(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("tip_ubica");
            Entidad.Tip_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_tip_ubica");
            Entidad.No_tip_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.Nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_ubica");
            Entidad.No_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("codprov");
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
           
            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private UsuarioBE CrearEntidadUbicacion(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.Nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("no_ubica");
            Entidad.No_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("codprov");
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            return Entidad;
        }

        private UsuarioBE CrearEntidad(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;

            indice = DReader.GetOrdinal("ID");
            Entidad.StrID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private UsuarioBE CrearEntidad_2(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("intID");
            Entidad.IntID = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
           
            return Entidad;
        }

        private UsuarioBE CrearEntidad_Talleres_PorUsuario_AsesServ(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("no_taller");
            Entidad.No_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.Nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("codprov");
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
                        
            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private UsuarioBE CrearEntidadPerfiles(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;
            
            indice = DReader.GetOrdinal("co_perfil_usuario");
            Entidad.Cod_perfil = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            indice = DReader.GetOrdinal("perfil");
            Entidad.Perfil = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }        

        private UsuarioBE CrearEntidadTalleresDistrito(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_taller");
            Entidad.No_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            
            return Entidad;
        }

        private UsuarioBE CrearEntidadUsuarioPorCodigo(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();            
            Int32 indice;

            indice = DReader.GetOrdinal("fl_inactivo");
            Entidad.Fl_inactivo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("CUSR_ID");
            Entidad.CUSR_ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("nu_tipo_documento");
            Entidad.Nu_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("no_ape_paterno");
            Entidad.No_ape_paterno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_ape_materno");
            Entidad.No_ape_materno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("VNOMUSR");
            Entidad.VNOMUSR = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("cod_perfil");
            //Entidad.Nid_cod_tipo_rol = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            Entidad.Cod_perfil = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddpto");
            Entidad.Coddpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("codprov");
            Entidad.Codprov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("coddist");
            Entidad.Coddist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_cod_tipo_usuario");
            Entidad.Nid_cod_tipo_usuario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_ubica");
            Entidad.Nid_ubica = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));          

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("VUSR_PASS");
            Entidad.VUSR_PASS = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("VCORREO");
            Entidad.VCORREO = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("VTELEFONO");
            Entidad.VTELEFONO = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("fe_inicio_acceso");
            Entidad.Fe_inicio_acceso1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("fe_fin_acceso");
            Entidad.Fe_fin_acceso1 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));                        

            indice = DReader.GetOrdinal("hr_inicio_acceso");
            Entidad.Hr_inicio_acceso = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            

            indice = DReader.GetOrdinal("hr_fin_acceso");
            Entidad.Hr_fin_acceso = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("VMSGBLQ");
            Entidad.VMSGBLQ = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("CESTBLQ");
            Entidad.CESTBLQ = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            


            indice = DReader.GetOrdinal("nu_modulo");
            Entidad.nu_modulo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }

        private UsuarioBE CrearEntidadUsuarios(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();            
            Int32 indice;

            indice = DReader.GetOrdinal("fl_inactivo");
            Entidad.Fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_usuario");
            Entidad.Nid_usuario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("CUSR_ID");
            Entidad.CUSR_ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_tipo_documento");
            Entidad.Nu_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_ape_paterno");
            Entidad.No_ape_paterno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_ape_materno");
            Entidad.No_ape_materno = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("VNOMUSR");
            Entidad.VNOMUSR = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("perfil");
            Entidad.Perfil = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("dpto");
            Entidad.Dpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("prov");
            Entidad.Prov = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("dist");
            Entidad.Dist = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_taller");
            Entidad.No_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("va_correo");
            Entidad.va_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nro_telf");
            Entidad.nro_telf = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private UsuarioBE CrearEntidad_CapacidadAtencion(IDataRecord DReader)
        {
            UsuarioBE Entidad = new UsuarioBE();
            Int32 indice;

            indice = DReader.GetOrdinal("DD_ATENCION");
            Entidad.Dd_atencion = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("FL_CONTROL");
            Entidad.fl_control = (DReader.IsDBNull(indice) ? string.Empty : DReader.GetString(indice));

            indice = DReader.GetOrdinal("QT_CAPACIDAD_FO");
            Entidad.qt_capacidad_fo = (DReader.IsDBNull(indice) ? -1 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("QT_CAPACIDAD_BO");
            Entidad.qt_capacidad_bo = (DReader.IsDBNull(indice) ? -1 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("QT_CAPACIDAD");
            Entidad.qt_capacidad = (DReader.IsDBNull(indice) ? -1 : DReader.GetInt32(indice));


            return Entidad;
        }

        

        public Int32 InsertarUsuarioHorario(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_horario_BO]";

                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);

                    if (ent.Dd_atencion == 0)
                        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);
                    

                    if (String.IsNullOrEmpty(ent.Ho_inicio))
                        db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, ent.Ho_inicio);

                    if (String.IsNullOrEmpty(ent.Ho_fin))
                        db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, ent.Ho_fin);

                    if (String.IsNullOrEmpty(ent.Fl_tipo))
                        db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }        

        public Int32 InsertarUsuarioModelo(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_usr_modelo_bo]";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_nid_modelo", DbType.Int32, ParameterDirection.Input, ent.Nid_modelo);
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    
                    nId =  db.Execute();                    
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 InsertarUsuarioTaller(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_usr_taller_BO]";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);

                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 InsertarUsuarioServicio(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_usr_servicio_BO]";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_nid_servicio", DbType.Int32, ParameterDirection.Input, ent.Nid_servicio);                 

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 InsertarUsuarioUbicacion(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_usr_ubicacion_BO]";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.Nid_ubica);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);                    

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }
        
        public Int32 InsertarUsuarioDiaExceptuado(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_dia_exceptuado_BO]";

                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);

                    if (ent.Fe_exceptuada == DateTime.Now)
                        db.AddParameter("@vi_fe_exceptuada", DbType.DateTime, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fe_exceptuada", DbType.DateTime, ParameterDirection.Input, ent.Fe_exceptuada);

                    db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        public Int32 InsertarUsuarioPerfil(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_PRFUSR_BO]";

                    db.AddParameter("@vi_CCOAPL", DbType.String, ParameterDirection.Input, ent.CCOAPL );                    

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);

                    //db.AddParameter("@vi_nid_perfil", DbType.Int32, ParameterDirection.Input, ent.Nid_perfil);

                    db.AddParameter("@vi_co_perfil_usuario", DbType.String, ParameterDirection.Input, ent.Cod_perfil);

                    if (String.IsNullOrEmpty(ent.Fl_inactivo))
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, ent.Fl_inactivo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.No_usuario_red))
                        db.AddParameter("@vi_no_usuario", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_usuario", DbType.String, ParameterDirection.Input, ent.No_usuario);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);                    

                    nId = db.Execute();
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }
            
        public Int32 InsertarUsuario(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spi_mae_usuarios_BO]";
                    db.AddParameter("@vi_no_ape_paterno", DbType.String, ParameterDirection.Input, ent.No_ape_paterno);
                    if (String.IsNullOrEmpty(ent.No_ape_materno))
                        db.AddParameter("@vi_no_ape_materno", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_ape_materno", DbType.String, ParameterDirection.Input, ent.No_ape_materno);                    
                    db.AddParameter("@vi_VNOMUSR", DbType.String, ParameterDirection.Input, ent.VNOMUSR);
                    db.AddParameter("@vi_CUSR_ID", DbType.String, ParameterDirection.Input, ent.CUSR_ID);
                    if (String.IsNullOrEmpty(ent.VUSR_PASS))
                        db.AddParameter("@vi_VUSR_PASS", DbType.String, ParameterDirection.Input, DBNull.Value);                    
                    else
                        db.AddParameter("@vi_VUSR_PASS", DbType.String, ParameterDirection.Input, ent.VUSR_PASS);

                    if (String.IsNullOrEmpty(ent.VTELEFONO))
                        db.AddParameter("@vi_VTELEFONO", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VTELEFONO", DbType.String, ParameterDirection.Input, ent.VTELEFONO);

                    if (ent.Nid_ubica == 0)
                        db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.Nid_ubica);

                    if (ent.Nid_cod_tipo_usuario==0)
                        db.AddParameter("@vi_nid_cod_tipo_usuario", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nid_cod_tipo_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_cod_tipo_usuario);

                    if (String.IsNullOrEmpty(ent.VMSGBLQ))
                        db.AddParameter("@vi_VMSGBLQ", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VMSGBLQ", DbType.String, ParameterDirection.Input, ent.VMSGBLQ);                    

                    if (String.IsNullOrEmpty(ent.CESTBLQ))
                        db.AddParameter("@vi_CESTBLQ", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_CESTBLQ", DbType.String, ParameterDirection.Input, ent.CESTBLQ);

                    if (ent.Fe_inicio_acceso == DateTime.MinValue)
                        db.AddParameter("@vi_fe_inicio_acceso", DbType.DateTime, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fe_inicio_acceso", DbType.DateTime, ParameterDirection.Input, ent.Fe_inicio_acceso);

                    if (ent.Fe_fin_acceso == DateTime.MinValue)
                        db.AddParameter("@vi_fe_fin_acceso", DbType.DateTime, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fe_fin_acceso", DbType.DateTime, ParameterDirection.Input, ent.Fe_fin_acceso);

                    if (String.IsNullOrEmpty(ent.Hr_inicio_acceso) )
                        db.AddParameter("@vi_hr_inicio_acceso", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_hr_inicio_acceso", DbType.String, ParameterDirection.Input, ent.Hr_inicio_acceso);

                    if (String.IsNullOrEmpty(ent.Hr_fin_acceso))
                        db.AddParameter("@vi_hr_fin_acceso", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_hr_fin_acceso", DbType.String, ParameterDirection.Input, ent.Hr_fin_acceso);

                    if (String.IsNullOrEmpty(ent.Nu_tipo_documento))
                        db.AddParameter("@vi_nu_tipo_documento", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nu_tipo_documento", DbType.String, ParameterDirection.Input, ent.Nu_tipo_documento);

                    if (String.IsNullOrEmpty(ent.VCORREO))
                        db.AddParameter("@vi_VCORREO", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VCORREO", DbType.String, ParameterDirection.Input, ent.VCORREO);

                    if (String.IsNullOrEmpty(ent.VPASSMD5))
                        db.AddParameter("@vi_VPASSMD5", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VPASSMD5", DbType.String, ParameterDirection.Input, ent.VPASSMD5);                    

                    if (String.IsNullOrEmpty(ent.Fl_inactivo))
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, ent.Fl_inactivo);

                    if(String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input,DBNull.Value );
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.No_usuario_red))
                        db.AddParameter("@vi_no_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_usuario_red", DbType.String, ParameterDirection.Input, ent.No_usuario_red);                                       
                    
                    db.AddParameter("@vo_nid_usuario", DbType.Int32, ParameterDirection.Output, 0);
                    db.Execute();
                    nId = Convert.ToInt32(db.GetParameter("@vo_nid_usuario"));                    
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }

        //ACTUALIZAR 

        public Int32 ActualizarUsuario(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_mae_usuarios_BO]";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);

                    db.AddParameter("@vi_no_ape_paterno", DbType.String, ParameterDirection.Input, ent.No_ape_paterno);

                    if (String.IsNullOrEmpty(ent.No_ape_materno))
                        db.AddParameter("@vi_no_ape_materno", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_ape_materno", DbType.String, ParameterDirection.Input, ent.No_ape_materno);

                    db.AddParameter("@vi_VNOMUSR", DbType.String, ParameterDirection.Input, ent.VNOMUSR);
                    db.AddParameter("@vi_CUSR_ID", DbType.String, ParameterDirection.Input, ent.CUSR_ID);

                    if (String.IsNullOrEmpty(ent.VUSR_PASS))
                        db.AddParameter("@vi_VUSR_PASS", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VUSR_PASS", DbType.String, ParameterDirection.Input, ent.VUSR_PASS);

                    if (String.IsNullOrEmpty(ent.VTELEFONO))
                        db.AddParameter("@vi_VTELEFONO", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VTELEFONO", DbType.String, ParameterDirection.Input, ent.VTELEFONO);

                    if (ent.Nid_ubica == 0)
                        db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.Nid_ubica);

                    if (ent.Nid_cod_tipo_usuario == 0)
                        db.AddParameter("@vi_nid_cod_tipo_usuario", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nid_cod_tipo_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_cod_tipo_usuario);

                    if (String.IsNullOrEmpty(ent.VMSGBLQ))
                        db.AddParameter("@vi_VMSGBLQ", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VMSGBLQ", DbType.String, ParameterDirection.Input, ent.VMSGBLQ);                    

                    if (String.IsNullOrEmpty(ent.CESTBLQ))
                        db.AddParameter("@vi_CESTBLQ", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_CESTBLQ", DbType.String, ParameterDirection.Input, ent.CESTBLQ);

                    if (ent.Fe_inicio_acceso == DateTime.MinValue)
                        db.AddParameter("@vi_fe_inicio_acceso", DbType.DateTime, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fe_inicio_acceso", DbType.DateTime, ParameterDirection.Input, ent.Fe_inicio_acceso);

                    if (ent.Fe_fin_acceso == DateTime.MinValue)
                        db.AddParameter("@vi_fe_fin_acceso", DbType.DateTime, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fe_fin_acceso", DbType.DateTime, ParameterDirection.Input, ent.Fe_fin_acceso);

                    if (String.IsNullOrEmpty(ent.Hr_inicio_acceso))
                        db.AddParameter("@vi_hr_inicio_acceso", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_hr_inicio_acceso", DbType.String, ParameterDirection.Input, ent.Hr_inicio_acceso);

                    if (String.IsNullOrEmpty(ent.Hr_fin_acceso))
                        db.AddParameter("@vi_hr_fin_acceso", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_hr_fin_acceso", DbType.String, ParameterDirection.Input, ent.Hr_fin_acceso);

                    if (String.IsNullOrEmpty(ent.Nu_tipo_documento))
                        db.AddParameter("@vi_nu_tipo_documento", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_nu_tipo_documento", DbType.String, ParameterDirection.Input, ent.Nu_tipo_documento);

                    if (String.IsNullOrEmpty(ent.VCORREO))
                        db.AddParameter("@vi_VCORREO", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VCORREO", DbType.String, ParameterDirection.Input, ent.VCORREO);

                    if (String.IsNullOrEmpty(ent.VPASSMD5))
                        db.AddParameter("@vi_VPASSMD5", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_VPASSMD5", DbType.String, ParameterDirection.Input, ent.VPASSMD5);

                    if (String.IsNullOrEmpty(ent.Fl_inactivo))
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, ent.Fl_inactivo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_cambio))
                        db.AddParameter("@vi_co_usuario_cambio", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_cambio", DbType.String, ParameterDirection.Input, ent.Co_usuario_cambio);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.No_usuario_red))
                        db.AddParameter("@vi_no_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_usuario_red", DbType.String, ParameterDirection.Input, ent.No_usuario_red);
                    
                    nId = db.Execute();                    
                }
            }
            catch (Exception)
            {
                //nId = 0;
            }
            return nId;
        }
        
        public Int32 MantenimientoUsuarioHorario(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sp_mant_mae_horario_usuario_BO]";

                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);


                    if (ent.Dd_atencion==0)
                        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_dd_atencion", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);

                    if (String.IsNullOrEmpty(ent.Ho_inicio))
                        db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_ho_inicio", DbType.String, ParameterDirection.Input, ent.Ho_inicio);

                    if (String.IsNullOrEmpty(ent.Ho_fin))
                        db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_ho_fin", DbType.String, ParameterDirection.Input, ent.Ho_fin);

                    if (String.IsNullOrEmpty(ent.Fl_tipo))
                        db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_cambio))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_cambio );

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    db.AddParameter("@vi_opcion", DbType.String, ParameterDirection.Input, ent.Op);
                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }


        public Int32 MantenimientoUsuarioTaller(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "src_sp_mant_usr_taller_bo";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_co_talleres", DbType.String, ParameterDirection.Input, ent.No_taller);
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);
                    db.AddParameter("@vi_fl_mant", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 MantenimientoUsuarioModelo(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "src_sp_mant_mae_usr_modelo_bo";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_co_modelos", DbType.String, ParameterDirection.Input, ent.No_modelo);
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);
                    db.AddParameter("@vi_fl_mant", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 MantenimientoUsuarioServicio(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "src_sp_mant_usr_servicio_bo";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_co_servicios", DbType.String, ParameterDirection.Input, ent.No_servicio);
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);
                    db.AddParameter("@vi_fl_mant", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }




        public Int32 MantenimientoUsuarioUbicacion(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sp_mant_usr_ubicacion_BO]";

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_nid_ubica", DbType.Int32, ParameterDirection.Input, ent.Nid_ubica);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_cambio))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_cambio );

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    db.AddParameter("@vi_opcion", DbType.String, ParameterDirection.Input, ent.Op);
                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        public Int32 ActualizarUsuarioPerfil(UsuarioBE ent)
        {
            Int32 nId = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_spu_PRFUSR_BO]";

                    db.AddParameter("@vi_CCOAPL", DbType.String, ParameterDirection.Input, ent.CCOAPL);

                    db.AddParameter("@vi_nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);

                    //db.AddParameter("@vi_nid_perfil", DbType.Int32, ParameterDirection.Input, ent.Nid_perfil);
                    db.AddParameter("@vi_co_perfil_usuario", DbType.String, ParameterDirection.Input, ent.Cod_perfil);

                    if (String.IsNullOrEmpty(ent.Fl_inactivo))
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_inactivo", DbType.String, ParameterDirection.Input, ent.Fl_inactivo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_cambio))
                        db.AddParameter("@vi_co_usuario_cambio", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_cambio", DbType.String, ParameterDirection.Input, ent.Co_usuario_cambio);

                    if (String.IsNullOrEmpty(ent.No_usuario))
                        db.AddParameter("@vi_no_usuario", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_usuario", DbType.String, ParameterDirection.Input, ent.No_usuario);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    nId = db.Execute();
                }
            }
            catch
            {
                //nId = 0;
            }
            return nId;
        } 

        public Int32 MantenimientoDiasExceptuados_Usuario(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[src_sp_mant_dia_exceptuado_usuario_BO]";

                    //db.AddParameter("@vi_nid_dia_exceptuado", DbType.Int32, ParameterDirection.Input, ent.Nid_dia_exceptuado);
                    db.AddParameter("@vi_nid_propietario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_fe_exceptuada", DbType.DateTime, ParameterDirection.Input, ent.Fe_exceptuada);
                    db.AddParameter("@vi_fl_tipo", DbType.String, ParameterDirection.Input, ent.Fl_tipo);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_cambio))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.Co_usuario_cambio);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    if (String.IsNullOrEmpty(ent.Fl_activo))
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);

                    db.AddParameter("@vi_opcion", DbType.String, ParameterDirection.Input, ent.Op);
                    res = db.Execute();
                }
            }
            catch
            {
                //res = 0;
            }
            return res;
        }

        // version 2.0

        public Int32 InhabilitarCapacidadAtencion_Usuario(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_INHABILITAR_CAPACIDAD_ATENCION_BO]";

                    db.AddParameter("@VI_NID_PROPIETARIO", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@VI_FL_TIPO", DbType.String, ParameterDirection.Input, "A");

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }

            return res;
        }

        public Int32 MantenimientoCapacidadAtencion_Usuario(UsuarioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPI_CAPACIDAD_ATENCION_ASESOR_BO]";

                    db.AddParameter("@VI_NID_USUARIO", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@VI_DIA_ATENC", DbType.Int32, ParameterDirection.Input, ent.Dd_atencion);

                    if (ent.qt_capacidad_fo.Equals(-1))
                        db.AddParameter("@VI_CAPACIDAD_FO", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CAPACIDAD_FO", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad_fo);

                    if (ent.qt_capacidad_bo.Equals(-1))
                        db.AddParameter("@VI_CAPACIDAD_BO", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CAPACIDAD_BO", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad_bo);

                    db.AddParameter("@VI_FL_CONTROL", DbType.String, ParameterDirection.Input, ent.fl_control);

                    if (ent.qt_capacidad.Equals(-1))
                        db.AddParameter("@VI_CAPACIDAD", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CAPACIDAD", DbType.Int32, ParameterDirection.Input, ent.qt_capacidad);

                    if (String.IsNullOrEmpty(ent.Co_usuario_crea))
                        db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_CO_USUARIO_CREA", DbType.String, ParameterDirection.Input, ent.Co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.Co_usuario_red))
                        db.AddParameter("@VI_NO_USUARIO_RED", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_NO_USUARIO_RED", DbType.String, ParameterDirection.Input, ent.Co_usuario_red);

                    if (String.IsNullOrEmpty(ent.No_estacion_red))
                        db.AddParameter("@VI_NO_ESTACION_RED", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@VI_NO_ESTACION_RED", DbType.String, ParameterDirection.Input, ent.No_estacion_red);

                    res = db.Execute();
                }
            }
            catch
            {
                res = 0;
            }

            return res;
        }

        public UsuarioBEList GETListarCapacidadAtencion_PorUsuario(UsuarioBE ent)
        {
            UsuarioBEList lista = new UsuarioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_LISTAR_CAPACIDAD_DIAS_ATENCION_BO]";
                    db.AddParameter("@VI_NID_PROPIETARIO", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@VI_FL_TIPO", DbType.String, ParameterDirection.Input, "A");
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UsuarioBE oMaestroUsuariosBE = CrearEntidad_CapacidadAtencion(DReader);
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

    }
}