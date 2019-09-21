using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Seguridad
{
    public class PerfilDA
    {
        #region "Perfiles"

        //DAC - 22/12/2010 - Inicio
        public PerfilBEList GetPerfilesBandeja(String aplicacionID, String dscPerfil, String estado, String strFlConcesionario)
        {
            PerfilBEList oPerfilBEList = new PerfilBEList();
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_perfil";
                    db.AddParameter("@vi_ch_cod_apli", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_va_nom_perfil", DbType.String, ParameterDirection.Input, dscPerfil);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, estado);
                    //DAC - 22/12/2010 - Inicio
                    db.AddParameter("@vi_ch_fl_concesionario", DbType.String, ParameterDirection.Input, strFlConcesionario);
                    //DAC - 22/12/2010 - Fin
                    reader = db.GetDataReader();
                }

                while (reader.Read())
                {
                    oPerfilBEList.Add(ConstructBandejaPerfil(reader));
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            return oPerfilBEList;
        }
        public PerfilBEList GetPerfilesBandejaConcesionario(String aplicacionID, String strCodUsuario)
        {
            PerfilBEList oPerfilBEList = new PerfilBEList();
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_perfil_concesionario";
                    db.AddParameter("@vi_ch_cod_apli", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, strCodUsuario);
                    reader = db.GetDataReader();
                }

                while (reader.Read())
                {
                    oPerfilBEList.Add(this.ConstructBandejaPerfilConcesionario(reader));
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            return oPerfilBEList;
        }
        //DAC - 22/12/2010 - Fin


        public PerfilBE GetPerfilById(Int32 perfilID)
        {
            PerfilBE oPerfilBE = new PerfilBE();
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_perfil_x_id";
                    db.AddParameter("@VI_IN_PERFIL_ID", DbType.Int32, ParameterDirection.Input, perfilID);
                    reader = db.GetDataReader();
                }

                if (reader.Read())
                {
                    oPerfilBE = ConstructPerfil(reader);
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            return oPerfilBE;
        }

        public Int32 InsertarPerfil(PerfilBE oPerfilBE)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_perfil";
                    db.AddParameter("@VI_CH_APLICACION", DbType.String, ParameterDirection.Input, oPerfilBE.CCOAPL);
                    db.AddParameter("@VI_VH_DESCRIPCION", DbType.String, ParameterDirection.Input, oPerfilBE.VDEPRF);
                    db.AddParameter("@VI_CH_FEC_INI", DbType.String, ParameterDirection.Input, oPerfilBE.SFEINIVIG);
                    db.AddParameter("@VI_CH_FEC_FIN", DbType.String, ParameterDirection.Input, oPerfilBE.SFEFINVIG);
                    db.AddParameter("@VI_VH_HORA_INI", DbType.String, ParameterDirection.Input, oPerfilBE.CHORINI);
                    db.AddParameter("@VI_VH_HORA_FIN", DbType.String, ParameterDirection.Input, oPerfilBE.CHORFIN);
                    db.AddParameter("@VI_VH_CO_USUARIO", DbType.String, ParameterDirection.Input, oPerfilBE.CO_USUARIO_CREA);
                    db.AddParameter("@VI_VH_ESTACION_RED", DbType.String, ParameterDirection.Input, oPerfilBE.NO_ESTACION_RED);
                    db.AddParameter("@VI_VH_NO_USUARIO", DbType.String, ParameterDirection.Input, oPerfilBE.NO_USUARIO_RED);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oPerfilBE.FL_INACTIVO);

                    //DAC - 21/12/2010 - Inicio
                    db.AddParameter("@vi_ch_fl_concesionario", DbType.String, ParameterDirection.Input, oPerfilBE.fl_concesionario);
                    //DAC - 21/12/2010 - Fin

                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }

        public Int32 ModificarPerfil(PerfilBE oPerfilBE)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_perfil";
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, oPerfilBE.NID_PERFIL);
                    db.AddParameter("@vi_ch_cod_aplicativo", DbType.String, ParameterDirection.Input, oPerfilBE.CCOAPL);
                    db.AddParameter("@vi_va_nom_perfil", DbType.String, ParameterDirection.Input, oPerfilBE.VDEPRF);
                    db.AddParameter("@vi_ch_fec_inicio", DbType.String, ParameterDirection.Input, oPerfilBE.SFEINIVIG);
                    db.AddParameter("@vi_ch_fec_fin", DbType.String, ParameterDirection.Input, oPerfilBE.SFEFINVIG);
                    db.AddParameter("@vi_ch_hora_inicio", DbType.String, ParameterDirection.Input, oPerfilBE.CHORINI);
                    db.AddParameter("@vi_ch_hora_fin", DbType.String, ParameterDirection.Input, oPerfilBE.CHORFIN);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oPerfilBE.CO_USUARIO_CREA);
                    db.AddParameter("@vi_va_nom_estacion", DbType.String, ParameterDirection.Input, oPerfilBE.NO_ESTACION_RED);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oPerfilBE.NO_USUARIO_RED);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, oPerfilBE.FL_INACTIVO);

                    //DAC - 21/12/2010 - Inicio
                    db.AddParameter("@vi_ch_fl_concesionario", DbType.String, ParameterDirection.Input, oPerfilBE.fl_concesionario);
                    //DAC - 21/12/2010 - Fin

                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }

        public Int32 EliminarPerfil(PerfilBE oPerfilBE)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spd_perfil";
                    db.AddParameter("@VI_IN_PERFIL_ID", DbType.Int32, ParameterDirection.Input, oPerfilBE.NID_PERFIL);
                    db.AddParameter("@VI_CH_APLICACION", DbType.String, ParameterDirection.Input, oPerfilBE.CCOAPL);
                    db.AddParameter("@VI_VH_CO_USUARIO", DbType.String, ParameterDirection.Input, oPerfilBE.CO_USUARIO_CAMBIO);
                    db.AddParameter("@VI_VH_ESTACION_RED", DbType.String, ParameterDirection.Input, oPerfilBE.NO_ESTACION_RED);
                    db.AddParameter("@VI_VH_NO_USUARIO", DbType.String, ParameterDirection.Input, oPerfilBE.NO_USUARIO_RED);
                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }
        #endregion

        #region "Opciones"

        public OpcionSeguridadBEList GetAllOpciones(OpcionSeguridadBE oOpcionSeguridadBE, Int32 perfilID, Int32 idUsuario)
        {
            OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
            IDataReader reader;

            using (Database db = new Database())
            {
                db.ProcedureName = "SGSNET_SPS_MODULOS";
                db.AddParameter("@VI_CH_APLICA", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.CCOAPL);
                db.AddParameter("@VI_IN_NIVEL", DbType.Int32, ParameterDirection.Input, oOpcionSeguridadBE.NIVEL);
                db.AddParameter("@VI_VC_STRUCT", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.CSTRUCT);
                db.AddParameter("@VI_IN_PERFIL", DbType.Int32, ParameterDirection.Input, perfilID);
                db.AddParameter("@vi_in_Nid_usuario", DbType.Int32, ParameterDirection.Input, idUsuario);
                reader = db.GetDataReader();
            }

            while (reader.Read())
            {
                oOpcionSeguridadBEList.Add(ConstructOpcion(reader));
            }
            reader.Close();

            return oOpcionSeguridadBEList;
        }

        //DAC - 01/04/2011 - Inicio
        //public Int32 InsertarOpcionByPerfil(OpcionSeguridadBE oOpcionSeguridadBE, Int32 perfilID, String indLimp)
        public Int32 InsertarOpcionByPerfil(OpcionSeguridadBE oOpcionSeguridadBE)
        {
            Int32 retorno = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_perfil_vs_menu_sistema";
                    db.AddParameter("@vi_ch_aplicacion", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.CCOAPL);
                    db.AddParameter("@vi_nid_perfil", DbType.Int32, ParameterDirection.Input, oOpcionSeguridadBE.nid_perfil);
                    //I @001
                    db.AddParameter("@vi_xml_menu", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.cad_lista_opciones);
                    //F @001   
                    db.AddParameter("@vi_va_co_usuario", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.CO_USUARIO);
                    db.AddParameter("@vi_va_no_usuario", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.NO_USUARIO);
                    db.AddParameter("@vi_va_estacion_red", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.NO_ESTACION_RED);

                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }
            return retorno;
        }
        //DAC - 01/04/2011 - Fin

        //DAC - 30/03/2011 - Inicio
        public OpcionSeguridadBEList GetAllOpcionesConcesionario(OpcionSeguridadBE oOpcionSeguridadBE)
        {
            OpcionSeguridadBEList oOpcionSeguridadBEList = new OpcionSeguridadBEList();
            IDataReader reader;

            using (Database db = new Database())
            {
                db.ProcedureName = "SGSNET_SPS_MODULOS_CONCESIONARIO";
                db.AddParameter("@VI_CH_APLICA", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.CCOAPL);
                db.AddParameter("@VI_IN_NIVEL", DbType.Int32, ParameterDirection.Input, oOpcionSeguridadBE.NIVEL);
                db.AddParameter("@VI_VC_STRUCT", DbType.String, ParameterDirection.Input, oOpcionSeguridadBE.CSTRUCT);

                reader = db.GetDataReader();
            }

            while (reader.Read())
            {
                oOpcionSeguridadBEList.Add(this.ConstructOpcionesConcesionario(reader));
            }
            reader.Close();

            return oOpcionSeguridadBEList;
        }
        //DAC - 30/03/2011 - Fin

        #endregion

        #region "Usuarios Relacionados"

        public UsuarioBEList GetBandejaUsuariosRelacionados(Int32 perfilID, String aplicacionID)
        {
            UsuarioBEList oUsuarioListBE = new UsuarioBEList();
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_perfil_usuario";
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, perfilID);
                    db.AddParameter("@vi_ch_cod_aplicacion", DbType.String, ParameterDirection.Input, aplicacionID);
                    reader = db.GetDataReader();
                }

                while (reader.Read())
                {
                    oUsuarioListBE.Add(ConstructBandejaUsuariosRelacionados(reader));
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            return oUsuarioListBE;
        }

        public UsuarioBEList GetAsignacionUsuariosRelacionados(UsuarioBE oUsuario, String aplicacionID)
        {
            UsuarioBEList oUsuarioListBE = new UsuarioBEList();
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_asignacion_perfil_usuario";
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, oUsuario.NID_PERFIL);
                    db.AddParameter("@vi_ch_cod_aplicacion", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_vh_nombres", DbType.String, ParameterDirection.Input, oUsuario.VNOMUSR);
                    db.AddParameter("@vi_vh_ape_pat", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_PATERNO);
                    db.AddParameter("@vi_vh_ape_mat", DbType.String, ParameterDirection.Input, oUsuario.NO_APE_MATERNO);
                    db.AddParameter("@vi_in_tipo", DbType.Int32, ParameterDirection.Input, oUsuario.NID_TIPO);
                    reader = db.GetDataReader();
                }

                while (reader.Read())
                {
                    oUsuarioListBE.Add(ConstructAsignacionUsuariosRelacionados(reader));
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            return oUsuarioListBE;
        }

        public Int32 EliminarUsuariosRelacionados(UsuarioBE oUsuarioBE)
        {
            Int32 retorno;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spd_perfil_usuario";
                    db.AddParameter("@vi_in_id_perfil_usuario", DbType.Int32, ParameterDirection.Input, oUsuarioBE.NID_PERFIL);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oUsuarioBE.CO_USUARIO_CAMBIO);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, oUsuarioBE.NO_ESTACION_RED);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oUsuarioBE.NO_USUARIO_RED);
                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }

            return retorno;
        }

        public Int32 InsertarUsuariosRelacionado(UsuarioBE oUsuarioBE, String aplicacionID)
        {
            Int32 retorno;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_perfil_usuario";
                    db.AddParameter("@vi_in_id_perfil", DbType.Int32, ParameterDirection.Input, oUsuarioBE.NID_PERFIL);
                    db.AddParameter("@vi_ch_cod_aplicacion", DbType.String, ParameterDirection.Input, aplicacionID);
                    db.AddParameter("@vi_ch_id_usuario", DbType.Int32, ParameterDirection.Input, oUsuarioBE.Nid_usuario);
                    db.AddParameter("@vi_va_cod_usuario", DbType.String, ParameterDirection.Input, oUsuarioBE.CO_USUARIO_CREA);
                    db.AddParameter("@vi_va_nom_estacion_red", DbType.String, ParameterDirection.Input, oUsuarioBE.NO_ESTACION_RED);
                    db.AddParameter("@vi_va_nom_usuario_red", DbType.String, ParameterDirection.Input, oUsuarioBE.NO_USUARIO_RED);
                    retorno = (Int32)db.ExecuteScalar();
                }
            }
            catch { throw; }

            return retorno;
        }

        #endregion

        #region "Constructores"

        public UsuarioBE ConstructBandejaUsuariosRelacionados(IDataReader reader)
        {
            UsuarioBE oUsuarioBE = new UsuarioBE();
            int indice;

            indice = reader.GetOrdinal("nid_perfil_usuario");
            if (!reader.IsDBNull(indice)) oUsuarioBE.NID_PERFIL = reader.GetInt32(indice);

            indice = reader.GetOrdinal("nom_usuario");
            oUsuarioBE.VNOMUSR = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("cusr_id");
            oUsuarioBE.CUSR_ID = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("tipo_usuario");
            oUsuarioBE.VUSR_TIPO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fe_inicio_acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_INICIO_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_INICIO_ACCESO = oUsuarioBE.FE_INICIO_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("fe_fin_acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_FIN_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_FIN_ACCESO = oUsuarioBE.FE_FIN_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("hr_inicio_acceso");
            oUsuarioBE.HR_INICIO_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("hr_fin_acceso");
            oUsuarioBE.HR_FIN_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            return oUsuarioBE;
        }

        public UsuarioBE ConstructAsignacionUsuariosRelacionados(IDataReader reader)
        {
            UsuarioBE oUsuarioBE = new UsuarioBE();
            int indice;

            indice = reader.GetOrdinal("Nid_usuario");
            if (!reader.IsDBNull(indice)) oUsuarioBE.Nid_usuario = reader.GetInt32(indice);

            indice = reader.GetOrdinal("nom_usuario");
            oUsuarioBE.VNOMUSR = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("cusr_id");
            oUsuarioBE.CUSR_ID = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("tipo_usuario");
            oUsuarioBE.VUSR_TIPO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fe_inicio_acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_INICIO_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_INICIO_ACCESO = oUsuarioBE.FE_INICIO_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("fe_fin_acceso");
            if (!reader.IsDBNull(indice))
            {
                oUsuarioBE.FE_FIN_ACCESO = reader.GetDateTime(indice);
                oUsuarioBE.SFE_FIN_ACCESO = oUsuarioBE.FE_FIN_ACCESO.ToShortDateString();
            }

            indice = reader.GetOrdinal("hr_inicio_acceso");
            oUsuarioBE.HR_INICIO_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
            indice = reader.GetOrdinal("hr_fin_acceso");
            oUsuarioBE.HR_FIN_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            return oUsuarioBE;
        }

        private OpcionSeguridadBE ConstructOpcion(IDataReader reader)
        {
            OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();
            int indice;

            indice = reader.GetOrdinal("NID_OPCION");
            if (!reader.IsDBNull(indice)) oOpcionSeguridadBE.NID_OPCION = reader.GetInt32(indice);

            indice = reader.GetOrdinal("CCOAPL");
            oOpcionSeguridadBE.CCOAPL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("CCOOPM");
            oOpcionSeguridadBE.CCOOPM = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("CSTRUCT");
            oOpcionSeguridadBE.CSTRUCT = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("VDEMEN");
            oOpcionSeguridadBE.VDEMEN = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("no_pagina_html");
            oOpcionSeguridadBE.NO_URL_WEB = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("no_img_on");
            oOpcionSeguridadBE.NO_IMG_ON = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("NO_IMG_OFF");
            oOpcionSeguridadBE.NO_IMG_OFF = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("FE_CREA");
            if (!reader.IsDBNull(indice)) oOpcionSeguridadBE.FE_CREA = reader.GetDateTime(indice);

            indice = reader.GetOrdinal("IND_REL");
            oOpcionSeguridadBE.IND_REL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_ind_visible");
            oOpcionSeguridadBE.fl_ind_visible = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_ind_ver_hijos");
            oOpcionSeguridadBE.fl_ind_ver_hijos = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_icono");
            oOpcionSeguridadBE.fl_icono = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            //I @001
            indice = reader.GetOrdinal("nid_usr_opm");
            if (!reader.IsDBNull(indice)) oOpcionSeguridadBE.nid_usr_opm = reader.GetInt32(indice);

            indice = reader.GetOrdinal("nid_opcion_perfil");
            if (!reader.IsDBNull(indice)) oOpcionSeguridadBE.nid_opcion_perfil = reader.GetInt32(indice);
            //F @001

            return oOpcionSeguridadBE;
        }

        private PerfilBE ConstructPerfil(IDataReader reader)
        {
            PerfilBE oPerfilBE = new PerfilBE();
            int indice;

            indice = reader.GetOrdinal("nid_perfil");
            if (!reader.IsDBNull(indice)) oPerfilBE.NID_PERFIL = reader.GetInt32(indice);

            indice = reader.GetOrdinal("CCOAPL");
            oPerfilBE.CCOAPL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("VDEPRF");
            oPerfilBE.VDEPRF = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("DFEINIVIG");
            if (!reader.IsDBNull(indice))
            {
                oPerfilBE.DFEINIVIG = reader.GetDateTime(indice);
                oPerfilBE.SFEINIVIG = oPerfilBE.DFEINIVIG.ToShortDateString();
            }

            indice = reader.GetOrdinal("DFEFINVIG");
            if (!reader.IsDBNull(indice))
            {
                oPerfilBE.DFEFINVIG = reader.GetDateTime(indice);
                oPerfilBE.SFEFINVIG = oPerfilBE.DFEFINVIG.ToShortDateString();
            }

            indice = reader.GetOrdinal("CHORINI");
            oPerfilBE.CHORINI = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("CHORFIN");
            oPerfilBE.CHORFIN = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("co_perfil_usuario");
            oPerfilBE.co_perfil_usuario = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_inactivo");
            oPerfilBE.FL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            //DAC - 21/12/2010 - Inicio
            indice = reader.GetOrdinal("fl_concesionario");
            oPerfilBE.fl_concesionario = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
            //DAC - 21/12/2010 - Fin

            return oPerfilBE;
        }

        private PerfilBE ConstructBandejaPerfil(IDataReader reader)
        {
            PerfilBE oPerfilBE = new PerfilBE();
            int indice;

            indice = reader.GetOrdinal("id_perfil");
            if (!reader.IsDBNull(indice)) oPerfilBE.NID_PERFIL = reader.GetInt32(indice);

            indice = reader.GetOrdinal("nom_perfil");
            oPerfilBE.VDEPRF = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("cant_usuarios_asignados");
            if (!reader.IsDBNull(indice)) oPerfilBE.NRO_USUARIOS = reader.GetInt32(indice);

            indice = reader.GetOrdinal("nom_estado");
            oPerfilBE.VFL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("cod_estado");
            oPerfilBE.FL_INACTIVO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_obligatorio");
            oPerfilBE.fl_obligatorio = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("co_perfil_usuario");
            oPerfilBE.co_perfil_usuario = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            //DAC - 22/12/2010 - Inicio
            indice = reader.GetOrdinal("fl_concesionario");
            oPerfilBE.fl_concesionario = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
            //DAC - 22/12/2010 - Fin

            return oPerfilBE;
        }

        //DAC - 22/12/2010 - Inicio
        private PerfilBE ConstructBandejaPerfilConcesionario(IDataReader reader)
        {
            PerfilBE oPerfilBE = new PerfilBE();
            int indice;

            indice = reader.GetOrdinal("nid_perfil");
            if (!reader.IsDBNull(indice)) oPerfilBE.NID_PERFIL = reader.GetInt32(indice);

            indice = reader.GetOrdinal("VDEPRF");
            oPerfilBE.VDEPRF = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            return oPerfilBE;
        }
        //DAC - 22/12/2010 - Fin

        //DAC - 30/03/2011 - Inicio
        private OpcionSeguridadBE ConstructOpcionesConcesionario(IDataReader reader)
        {
            OpcionSeguridadBE oOpcionSeguridadBE = new OpcionSeguridadBE();
            int indice;

            indice = reader.GetOrdinal("NID_OPCION");
            if (!reader.IsDBNull(indice)) oOpcionSeguridadBE.NID_OPCION = reader.GetInt32(indice);

            indice = reader.GetOrdinal("CCOAPL");
            oOpcionSeguridadBE.CCOAPL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("CCOOPM");
            oOpcionSeguridadBE.CCOOPM = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("CSTRUCT");
            oOpcionSeguridadBE.CSTRUCT = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("VDEMEN");
            oOpcionSeguridadBE.VDEMEN = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("no_pagina_html");
            oOpcionSeguridadBE.NO_URL_WEB = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("no_img_on");
            oOpcionSeguridadBE.NO_IMG_ON = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("NO_IMG_OFF");
            oOpcionSeguridadBE.NO_IMG_OFF = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("FE_CREA");
            if (!reader.IsDBNull(indice)) oOpcionSeguridadBE.FE_CREA = reader.GetDateTime(indice);

            indice = reader.GetOrdinal("IND_REL");
            oOpcionSeguridadBE.IND_REL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_ind_visible");
            oOpcionSeguridadBE.fl_ind_visible = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_ind_ver_hijos");
            oOpcionSeguridadBE.fl_ind_ver_hijos = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            indice = reader.GetOrdinal("fl_icono");
            oOpcionSeguridadBE.fl_icono = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

            return oOpcionSeguridadBE;
        }
        //DAC - 30/03/2011 - Fin

        #endregion
    }
}