using System;
using System.Data;
using AppMiTaller.Intranet.BE;


namespace AppMiTaller.Intranet.DA.Seguridad
{
    public class UsuarioDA
    {
        #region "Usuario"
        public UsuarioBEList GetAllUsuarioBandeja(UsuarioBE oUsuario, String aplicacionID)
        {
            UsuarioBEList oUsuarioList = new UsuarioBEList();
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_usuario";
                    db.AddParameter("@vi_va_apaterno", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_MATERNO);
                    db.AddParameter("@vi_va_amaterno", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_PATERNO);
                    db.AddParameter("@vi_va_nombre", DbType.String, ParameterDirection.Input, oUsuario.VNOMUSR);
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, oUsuario.NID_PERFIL);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oUsuario.FL_INACTIVO);
                    db.AddParameter("@vi_in_id_ubicacion", DbType.Int32, ParameterDirection.Input, oUsuario.NID_UBICA);
                    db.AddParameter("@vi_in_id_rol", DbType.Int32, ParameterDirection.Input, oUsuario.NID_ROL);
                    db.AddParameter("@vi_va_id_aplicacion", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_va_nu_tipo_documento", DbType.String, ParameterDirection.Input, (oUsuario.NU_TIPO_DOCUMENTO == null) ? "" : oUsuario.NU_TIPO_DOCUMENTO);
                    db.AddParameter("@vi_va_login", DbType.String, ParameterDirection.Input, (oUsuario.VNOMUSR_CUSR_ID == null) ? "" : oUsuario.VNOMUSR_CUSR_ID);

                    reader = db.GetDataReader();
                }
                while (reader.Read())
                {
                    oUsuarioList.Add(ConstructSoloUsuarioBandeja(reader));
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }

            return oUsuarioList;
        }

        public Int32 EliminarUsuarioMasivo(UsuarioBE oUsuario, String cadena, Int32 nuRegistros)
        {
            Int32 retorno = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spd_usuario_masivo";
                    db.AddParameter("@vi_va_nid_usuarios", DbType.String, ParameterDirection.Input, cadena);
                    db.AddParameter("@vi_in_contador", DbType.Int32, ParameterDirection.Input, nuRegistros);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oUsuario.CO_USUARIO_CREA);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oUsuario.NO_USUARIO_RED);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, oUsuario.NO_ESTACION_RED);
                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }

        public Int32 ActivarUsuarioMasivo(UsuarioBE oUsuario, String cadena, Int32 nuRegistros)
        {
            Int32 retorno = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_activar_usuario_masivo";
                    db.AddParameter("@vi_va_nid_usuarios", DbType.String, ParameterDirection.Input, cadena);
                    db.AddParameter("@vi_in_contador", DbType.Int32, ParameterDirection.Input, nuRegistros);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oUsuario.CO_USUARIO_CREA);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oUsuario.NO_USUARIO_RED);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, oUsuario.NO_ESTACION_RED);
                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }

        public Int32 InsertarUsuario(UsuarioBE oUsuario, String aplicacionID)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_usuario";
                    db.AddParameter("@vi_va_apaterno", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_PATERNO);
                    db.AddParameter("@vi_va_amterno", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_MATERNO);
                    db.AddParameter("@vi_va_nombre", DbType.String, ParameterDirection.Input, oUsuario.VNOMUSR);
                    db.AddParameter("@vi_va_nro_documento", DbType.String, ParameterDirection.Input, oUsuario.NU_TIPO_DOCUMENTO);
                    db.AddParameter("@vi_va_login", DbType.String, ParameterDirection.Input, oUsuario.CUSR_ID);
                    db.AddParameter("@vi_ch_bloqueado", DbType.String, ParameterDirection.Input, oUsuario.CESTBLQ);
                    db.AddParameter("@vi_va_password", DbType.String, ParameterDirection.Input, oUsuario.VPASSMD5);
                    db.AddParameter("@vi_va_passwordEnc", DbType.String, ParameterDirection.Input, oUsuario.VUSR_PASS);
                    db.AddParameter("@vi_va_passwordDesEnc", DbType.String, ParameterDirection.Input, oUsuario.passwordDesEnc);
                    db.AddParameter("@vi_va_correo", DbType.String, ParameterDirection.Input, oUsuario.VCORREO);
                    db.AddParameter("@vi_va_telefono", DbType.String, ParameterDirection.Input, oUsuario.VTELEFONO);
                    db.AddParameter("@vi_in_id_ubicacion", DbType.Int32, ParameterDirection.Input, oUsuario.NID_UBICA);
                    db.AddParameter("@vi_va_id_aplicacion", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, oUsuario.NID_PERFIL);
                    db.AddParameter("@vi_in_id_tipo", DbType.Int32, ParameterDirection.Input, oUsuario.NID_TIPO);
                    db.AddParameter("@vi_ch_fec_inicio", DbType.String, ParameterDirection.Input, oUsuario.SFE_INICIO_ACCESO);
                    db.AddParameter("@vi_ch_fec_fin", DbType.String, ParameterDirection.Input, oUsuario.SFE_FIN_ACCESO);
                    db.AddParameter("@vi_va_hora_ini", DbType.String, ParameterDirection.Input, oUsuario.HR_INICIO_ACCESO);
                    db.AddParameter("@vi_va_hora_fin", DbType.String, ParameterDirection.Input, oUsuario.HR_FIN_ACCESO);
                    db.AddParameter("@vi_va_mensaje", DbType.String, ParameterDirection.Input, oUsuario.VMSGBLQ);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oUsuario.FL_INACTIVO);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oUsuario.CO_USUARIO_CREA);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oUsuario.NO_USUARIO_RED);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, oUsuario.NO_ESTACION_RED);

                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }

        public Int32 ModificarUsuario(UsuarioBE oUsuario, String aplicacionID)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_usuario";
                    db.AddParameter("@vi_in_id_usuario", DbType.Int32, ParameterDirection.Input, oUsuario.NID_USUARIO);
                    db.AddParameter("@vi_va_apaterno", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_PATERNO);
                    db.AddParameter("@vi_va_amterno", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_MATERNO);
                    db.AddParameter("@vi_va_nombre", DbType.String, ParameterDirection.Input, oUsuario.VNOMUSR);
                    db.AddParameter("@vi_va_nro_documento", DbType.String, ParameterDirection.Input, oUsuario.NU_TIPO_DOCUMENTO);
                    db.AddParameter("@vi_va_login", DbType.String, ParameterDirection.Input, oUsuario.CUSR_ID);
                    db.AddParameter("@vi_ch_bloqueado", DbType.String, ParameterDirection.Input, oUsuario.CESTBLQ);
                    db.AddParameter("@vi_va_password", DbType.String, ParameterDirection.Input, oUsuario.VPASSMD5);
                    db.AddParameter("@vi_va_passwordEnc", DbType.String, ParameterDirection.Input, oUsuario.VUSR_PASS);
                    db.AddParameter("@vi_va_passwordDesEnc", DbType.String, ParameterDirection.Input, oUsuario.passwordDesEnc);
                    db.AddParameter("@vi_va_correo", DbType.String, ParameterDirection.Input, oUsuario.VCORREO);
                    db.AddParameter("@vi_va_telefono", DbType.String, ParameterDirection.Input, oUsuario.VTELEFONO);
                    db.AddParameter("@vi_in_id_ubicacion", DbType.Int32, ParameterDirection.Input, oUsuario.NID_UBICA);
                    db.AddParameter("@vi_va_id_aplicacion", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, oUsuario.NID_PERFIL);
                    db.AddParameter("@vi_in_id_tipo", DbType.Int32, ParameterDirection.Input, oUsuario.NID_TIPO);
                    db.AddParameter("@vi_ch_fec_inicio", DbType.String, ParameterDirection.Input, oUsuario.SFE_INICIO_ACCESO);
                    db.AddParameter("@vi_ch_fec_fin", DbType.String, ParameterDirection.Input, oUsuario.SFE_FIN_ACCESO);
                    db.AddParameter("@vi_va_hora_ini", DbType.String, ParameterDirection.Input, oUsuario.HR_INICIO_ACCESO);
                    db.AddParameter("@vi_va_hora_fin", DbType.String, ParameterDirection.Input, oUsuario.HR_FIN_ACCESO);
                    db.AddParameter("@vi_va_mensaje", DbType.String, ParameterDirection.Input, oUsuario.VMSGBLQ);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oUsuario.FL_INACTIVO);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oUsuario.CO_USUARIO_CREA);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oUsuario.NO_USUARIO_RED);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, oUsuario.NO_ESTACION_RED);

                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }

        public UsuarioBE GetUsuarioById(Int32 usuarioID)
        {
            UsuarioBE oUsuarioBE = new UsuarioBE();
            int indice;
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_usuario_x_id";
                    db.AddParameter("@vi_in_id_usuario", DbType.Int32, ParameterDirection.Input, usuarioID);
                    reader = db.GetDataReader();
                }
                if (reader.Read())
                {
                    indice = reader.GetOrdinal("id_usuario");
                    if (!reader.IsDBNull(indice)) oUsuarioBE.NID_USUARIO = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("apaterno");
                    oUsuarioBE.NO_APE_PATERNO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("amaterno");
                    oUsuarioBE.NO_APE_MATERNO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("nom_usuario");
                    oUsuarioBE.VNOMUSR = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("login_usuario");
                    oUsuarioBE.CUSR_ID = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("password");
                    oUsuarioBE.VPASSMD5 = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("VUSR_PASS");
                    oUsuarioBE.VUSR_PASS = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("nro_documento");
                    oUsuarioBE.NU_TIPO_DOCUMENTO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("correo");
                    oUsuarioBE.VCORREO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("telefono");
                    oUsuarioBE.VTELEFONO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("tipo_usuario");
                    if (!reader.IsDBNull(indice)) oUsuarioBE.NID_TIPO = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("id_perfil");
                    if (!reader.IsDBNull(indice)) oUsuarioBE.NID_PERFIL = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("id_ubicacion");
                    if (!reader.IsDBNull(indice)) oUsuarioBE.NID_UBICA = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("est_bloqueo");
                    oUsuarioBE.CESTBLQ = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("mensaje");
                    oUsuarioBE.VMSGBLQ = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

                    indice = reader.GetOrdinal("fecha_inicio_Acceso");
                    if (!reader.IsDBNull(indice))
                    {
                        oUsuarioBE.FE_INICIO_ACCESO = reader.GetDateTime(indice);
                        oUsuarioBE.SFE_INICIO_ACCESO = oUsuarioBE.FE_INICIO_ACCESO.ToShortDateString();
                    }

                    indice = reader.GetOrdinal("fecha_fin_acceso");
                    if (!reader.IsDBNull(indice))
                    {
                        oUsuarioBE.FE_FIN_ACCESO = reader.GetDateTime(indice);
                        oUsuarioBE.SFE_FIN_ACCESO = oUsuarioBE.FE_FIN_ACCESO.ToShortDateString();
                    }

                    indice = reader.GetOrdinal("hora_ini_acceso");
                    oUsuarioBE.HR_INICIO_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("hora_fin_acceso");
                    oUsuarioBE.HR_FIN_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("estado");
                    oUsuarioBE.SFL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("cod_estado");
                    oUsuarioBE.FL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("co_perfil_usuario");
                    oUsuarioBE.co_perfil_usuario = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("di_ubica");
                    oUsuarioBE.VUBICACION = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("no_ubica_corto");
                    oUsuarioBE.no_ubica_corto = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }

            return oUsuarioBE;
        }

        public Int32 ModificarPassWord(String usuario, String passEnc, String passMD5
                , String codUsuario, String nomUsuarioRed, String estacioRed, String indActReset
                , String passDesencriptado)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_usuario_password";
                    db.AddParameter("@vi_va_login", DbType.String, ParameterDirection.Input, usuario);
                    db.AddParameter("@vi_va_password", DbType.String, ParameterDirection.Input, passEnc);
                    db.AddParameter("@vi_va_password_MD5", DbType.String, ParameterDirection.Input, passMD5);
                    db.AddParameter("@vi_va_password_des", DbType.String, ParameterDirection.Input, passDesencriptado);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, codUsuario);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, nomUsuarioRed);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, estacioRed);
                    db.AddParameter("@vi_va_ind_act_reset", DbType.String, ParameterDirection.Input, indActReset);
                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }
        #endregion

        #region "Constructores"

        private UsuarioBE ConstructUsuarioBandeja(IDataReader reader)
        {
            UsuarioBE oUsuarioBE = new UsuarioBE();
            int indice;

            indice = reader.GetOrdinal("id_usuario");
            if (!reader.IsDBNull(indice)) oUsuarioBE.NID_USUARIO = reader.GetInt32(indice);

            indice = reader.GetOrdinal("apaterno");
            oUsuarioBE.NO_APE_PATERNO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("amaterno");
            oUsuarioBE.NO_APE_MATERNO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("nom_usuario");
            oUsuarioBE.VNOMUSR = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            oUsuarioBE.VNOMUSR = String.Format("{0} {1} {2}", oUsuarioBE.VNOMUSR, oUsuarioBE.NO_APE_PATERNO, oUsuarioBE.NO_APE_MATERNO);

            indice = reader.GetOrdinal("login_usuario");
            oUsuarioBE.CUSR_ID = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("tipo_usuario");
            oUsuarioBE.VUSR_TIPO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("perfil");
            oUsuarioBE.VUSR_PERFIL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fecha_inicio_Acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_INICIO_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_INICIO_ACCESO = oUsuarioBE.FE_INICIO_ACCESO.ToShortDateString();
            }
            indice = reader.GetOrdinal("fecha_fin_acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_FIN_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_FIN_ACCESO = oUsuarioBE.FE_FIN_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("hora_ini_acceso");
            oUsuarioBE.HR_INICIO_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("hora_fin_acceso");
            oUsuarioBE.HR_FIN_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            oUsuarioBE.HR_INICIO_ACCESO = String.Format("{0} - {1}", oUsuarioBE.HR_INICIO_ACCESO, oUsuarioBE.HR_FIN_ACCESO);

            indice = reader.GetOrdinal("estado");
            oUsuarioBE.SFL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("cod_estado");
            oUsuarioBE.FL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            oUsuarioBE.VNOMUSR_CUSR_ID = String.Format("{0} {1} {2}", oUsuarioBE.VNOMUSR, " - ", oUsuarioBE.CUSR_ID);
            return oUsuarioBE;
        }

        private UsuarioBE ConstructSoloUsuarioBandeja(IDataReader reader)
        {
            UsuarioBE oUsuarioBE = new UsuarioBE();
            int indice;

            indice = reader.GetOrdinal("id_usuario");
            if (!reader.IsDBNull(indice)) oUsuarioBE.NID_USUARIO = reader.GetInt32(indice);

            indice = reader.GetOrdinal("apaterno");
            oUsuarioBE.NO_APE_PATERNO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("amaterno");
            oUsuarioBE.NO_APE_MATERNO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("nom_usuario");
            oUsuarioBE.VNOMUSR = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            oUsuarioBE.VNOMUSR = String.Format("{0} {1} {2}", oUsuarioBE.VNOMUSR, oUsuarioBE.NO_APE_PATERNO, oUsuarioBE.NO_APE_MATERNO);

            indice = reader.GetOrdinal("login_usuario");
            oUsuarioBE.CUSR_ID = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("tipo_usuario");
            oUsuarioBE.VUSR_TIPO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("perfil");
            oUsuarioBE.VUSR_PERFIL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fecha_inicio_Acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_INICIO_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_INICIO_ACCESO = oUsuarioBE.FE_INICIO_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("fecha_fin_acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_FIN_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_FIN_ACCESO = oUsuarioBE.FE_FIN_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("hora_ini_acceso");
            oUsuarioBE.HR_INICIO_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("hora_fin_acceso");
            oUsuarioBE.HR_FIN_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            oUsuarioBE.HR_INICIO_ACCESO = String.Format("{0} - {1}", oUsuarioBE.HR_INICIO_ACCESO, oUsuarioBE.HR_FIN_ACCESO);

            indice = reader.GetOrdinal("estado");
            oUsuarioBE.SFL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("cod_estado");
            oUsuarioBE.FL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("nu_tipo_documento");
            oUsuarioBE.NU_TIPO_DOCUMENTO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            oUsuarioBE.VNOMUSR_CUSR_ID = String.Format("{0} {1} {2}", oUsuarioBE.VNOMUSR, " - ", oUsuarioBE.CUSR_ID);

            return oUsuarioBE;
        }

        #endregion
    }
}