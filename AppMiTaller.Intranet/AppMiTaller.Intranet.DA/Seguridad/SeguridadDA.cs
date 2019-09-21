using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Seguridad
{
    public class SeguridadDA
    {
        public UsuarioBE GetDatosUsuario(String usuario, String aplicaionID)
        {
            UsuarioBE oUsuario = null;
            int indice;
            IDataReader reader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_logeo";
                    db.AddParameter("@VI_VA_USUARIO", DbType.String, ParameterDirection.Input, usuario);
                    db.AddParameter("@vi_va_id_aplicacion", DbType.String, ParameterDirection.Input, aplicaionID);
                    reader = db.GetDataReader();
                }

                if (reader.Read())
                {
                    oUsuario = new UsuarioBE();

                    indice = reader.GetOrdinal("id_usuario");
                    if (!reader.IsDBNull(indice)) oUsuario.Nid_usuario = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("vusr_pass");
                    oUsuario.VPASSMD5 = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

                    indice = reader.GetOrdinal("vnomusr");
                    oUsuario.VNOMUSR = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

                    indice = reader.GetOrdinal("cestblq");
                    oUsuario.CESTBLQ = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

                    indice = reader.GetOrdinal("vmsgblq");
                    oUsuario.VMSGBLQ = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

                    indice = reader.GetOrdinal("nid_cod_tipo_usuario");
                    if (!reader.IsDBNull(indice)) oUsuario.NID_TIPO = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("id_perfil");
                    if (!reader.IsDBNull(indice)) oUsuario.NID_PERFIL = reader.GetInt32(indice);

                    indice = reader.GetOrdinal("fe_inicio_acceso");
                    oUsuario.SFE_INICIO_ACCESO = String.Empty;
                    if (!reader.IsDBNull(indice))
                    {
                        oUsuario.FE_INICIO_ACCESO = reader.GetDateTime(indice);
                        oUsuario.SFE_INICIO_ACCESO = String.Format("{0:" + ConstanteBE.formato_fecha_es + "}", oUsuario.FE_INICIO_ACCESO);
                    }

                    indice = reader.GetOrdinal("fe_fin_acceso");
                    oUsuario.SFE_FIN_ACCESO = String.Empty;
                    if (!reader.IsDBNull(indice))
                    {
                        oUsuario.FE_FIN_ACCESO = reader.GetDateTime(indice);
                        oUsuario.SFE_FIN_ACCESO = String.Format("{0:" + ConstanteBE.formato_fecha_es + "}", oUsuario.FE_FIN_ACCESO);
                    }

                    indice = reader.GetOrdinal("hr_inicio_acceso");
                    oUsuario.HR_INICIO_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("hr_fin_acceso");
                    oUsuario.HR_FIN_ACCESO = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);

                    indice = reader.GetOrdinal("fe_inicio_acceso_perfil");
                    oUsuario.SFE_INICIO_ACCESO_PERFIL = String.Empty;
                    if (!reader.IsDBNull(indice))
                    {
                        oUsuario.FE_INICIO_ACCESO_PERFIL = reader.GetDateTime(indice);
                        oUsuario.SFE_INICIO_ACCESO_PERFIL = String.Format("{0:" + ConstanteBE.formato_fecha_es + "}", oUsuario.FE_INICIO_ACCESO_PERFIL);
                    }

                    indice = reader.GetOrdinal("fe_fin_acceso_perfil");
                    oUsuario.SFE_FIN_ACCESO_PERFIL = String.Empty;
                    if (!reader.IsDBNull(indice))
                    {
                        oUsuario.FE_FIN_ACCESO_PERFIL = reader.GetDateTime(indice);
                        oUsuario.SFE_FIN_ACCESO_PERFIL = String.Format("{0:" + ConstanteBE.formato_fecha_es + "}", oUsuario.FE_FIN_ACCESO_PERFIL);
                    }
                    indice = reader.GetOrdinal("hr_inicio_acceso_perfil");
                    oUsuario.HR_INICIO_ACCESO_PERFIL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("hr_fin_acceso_perfil");
                    oUsuario.HR_FIN_ACCESO_PERFIL = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("fl_reset");
                    oUsuario.fl_reset = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("nid_ubicacion");
                    if (!reader.IsDBNull(indice)) oUsuario.NID_UBICA = reader.GetInt32(indice);
                    indice = reader.GetOrdinal("co_perfil_usuario");
                    oUsuario.co_perfil_usuario = reader.IsDBNull(indice) ? String.Empty : reader.GetString(indice);
                    indice = reader.GetOrdinal("int_dias_caducidad");
                    if (!reader.IsDBNull(indice)) oUsuario.int_dias_caducidad = reader.GetInt32(indice);
                }
                reader.Close();
            }
            catch
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                throw;
            }
            return oUsuario;
        }

        public bool UpdateLogeoBloqueo(String usuario, String indBloqueo, String msgBloqueo)
        {
            string retorno = String.Empty;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spu_logeo_bloqueo";
                    db.AddParameter("@VI_VA_USUARIO", DbType.String, ParameterDirection.Input, usuario);
                    db.AddParameter("@VI_VC_EST_BLOQUEO", DbType.String, ParameterDirection.Input, indBloqueo);
                    db.AddParameter("@VI_VC_MSG_BLOQUEO", DbType.String, ParameterDirection.Input, msgBloqueo);
                    retorno = db.ExecuteScalar().ToString();
                }

            }
            catch { throw; }
            if (!retorno.Equals("0")) return false;
            return true;
        }
        public Int32 InsertUsuarioOpcion(Int32 IdUsuario, String xml, String coUsuario, String noEstacioRed, String noUsuario)
        {
            Int32 res;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_spi_usuario_opcion";
                    db.AddParameter("@vi_Nid_usuario", DbType.Int32, ParameterDirection.Input, IdUsuario);
                    db.AddParameter("@vi_xml_menu", DbType.String, ParameterDirection.Input, xml);
                    db.AddParameter("@vi_vh_co_usuario", DbType.String, ParameterDirection.Input, coUsuario);
                    db.AddParameter("@vi_vh_estacion_red", DbType.String, ParameterDirection.Input, noEstacioRed);
                    db.AddParameter("@vi_vh_no_usuario", DbType.String, ParameterDirection.Input, noUsuario);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch //(Exception ex) 
            { throw; }
            return res;
        }
    }
}