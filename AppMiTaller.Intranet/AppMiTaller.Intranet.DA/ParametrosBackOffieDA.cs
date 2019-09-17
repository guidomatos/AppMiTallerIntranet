using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppMiTaller.Intranet.BE;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;


namespace AppMiTaller.Intranet.DA
{
    public class ParametrosBackOffieDA
    {

        String var1 = "";
        

        public ParametrosBackOffieBEList ListarConfigTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            ParametrosBackOffieBEList lista = new ParametrosBackOffieBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_DATOS_TALLER_EMPRESA_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller );
                    db.AddParameter("@vi_nid_empresa", DbType.Int32, ParameterDirection.Input, ent.nid_empresa);
                    db.AddParameter("@vi_no_banco", DbType.String, ParameterDirection.Input, ent.no_banco);
                    db.AddParameter("@vi_nu_cuenta", DbType.String, ParameterDirection.Input, ent.nu_cuenta);
                    db.AddParameter("@vi_no_correo_callcenter", DbType.String, ParameterDirection.Input, ent.no_correo_callcenter);
                    db.AddParameter("@vi_nu_callcenter", DbType.String, ParameterDirection.Input, ent.nu_callcenter);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);
                    
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ParametrosBackOfficeBE oParametrosBackOffieBE = Entidad_ListarConfigTallerEmpresa(DReader);
                    lista.Add(oParametrosBackOffieBE);
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
        public ParametrosBackOffieBEList ListarDatosTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            ParametrosBackOffieBEList lista = new ParametrosBackOffieBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_DATOS_TALLER_EMPRESA_ID_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@vi_nid_empresa", DbType.Int32, ParameterDirection.Input, ent.nid_empresa);                    
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ParametrosBackOfficeBE oParametrosBackOffieBE = Entidad_ListarDatosTallerEmpresa(DReader);
                    lista.Add(oParametrosBackOffieBE);
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
        public Int32 InsertarConfigTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            int rpta = 0;

            try
            {
                using (Database db = new Database())
                {

                    db.ProcedureName = "[SRC_SPI_DATOS_TALLER_EMPRESA_BO]";
                    db.AddParameter("@vi_nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller );
                    db.AddParameter("@vi_nid_empresa", DbType.Int32, ParameterDirection.Input, ent.nid_empresa);
                    db.AddParameter("@vi_no_banco", DbType.String, ParameterDirection.Input, ent.no_banco);
                    db.AddParameter("@vi_nu_cuenta", DbType.String, ParameterDirection.Input, ent.nu_cuenta);
                    db.AddParameter("@vi_no_correo_callcenter", DbType.String, ParameterDirection.Input, ent.no_correo_callcenter);
                    db.AddParameter("@vi_nu_callcenter", DbType.String, ParameterDirection.Input, ent.nu_callcenter);
                    //-------------------------
                    db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario_crea);
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    rpta = db.Execute();
                }
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            return rpta;
        }
        public Int32 ActualizarConfigTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            int rpta = 0;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_DATOS_TALLER_EMPRESA_BO]";
                    db.AddParameter("@vi_nid_taller_empresa", DbType.Int32, ParameterDirection.Input, ent.nid_taller_empresa);
                    db.AddParameter("@vi_no_banco", DbType.String, ParameterDirection.Input, ent.no_banco);
                    db.AddParameter("@vi_nu_cuenta", DbType.String, ParameterDirection.Input, ent.nu_cuenta);
                    db.AddParameter("@vi_no_correo_callcenter", DbType.String, ParameterDirection.Input, ent.no_correo_callcenter);
                    db.AddParameter("@vi_nu_callcenter", DbType.String, ParameterDirection.Input, ent.nu_callcenter);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo);
                    //-------------------------
                    db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.co_usuario_modi);
                    rpta = db.Execute();
                }
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            return rpta;
        }



        public string GetFechaActual()
        {
            string fecha = string.Empty ;              
              
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_FECHAACTUAL_BO]";
                    fecha = db.ExecuteScalar().ToString();
                }
                                   
          }catch (Exception ex) {
                throw ex;
                //return null;
          }
            return fecha;
        }
        public ParametrosBackOfficeBE GetHorarioXDefecto()
        {
            ParametrosBackOfficeBE objBeParam = new ParametrosBackOfficeBE();
            IDataReader DReader = null;
            int i = 0;
            int indice = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_HORARIO_X_DEFECTO]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    if (i == 0)
                    {
                        indice = DReader.GetOrdinal("no_valor2");
                        objBeParam.HoraInicio = DReader.GetString(indice);
                    }
                    else if (i == 1)
                    {
                        indice = DReader.GetOrdinal("no_valor2");
                        objBeParam.HoraFinal = DReader.GetString(indice);
                    }
                    else if (i == 2)
                    {
                        indice = DReader.GetOrdinal("no_valor2");
                        objBeParam.IntervaloTime = DReader.GetString(indice);
                    }
                    i += 1;
                }

                DReader.Close();
            }
            catch //(Exception ex)
            {
                objBeParam = null;
            }
            return objBeParam;
        }
        public ParametrosBackOffieBEList GetHorasSRC()
        {
            ParametrosBackOfficeBE objBeParam=new ParametrosBackOfficeBE();
            ParametrosBackOffieBEList objBeListParam = new ParametrosBackOffieBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_Listado_horas]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    objBeParam = CrearEntidad_HoraSRC(DReader);
                    objBeListParam.Add(objBeParam);
                }

                DReader.Close();
            }
            catch //(Exception ex)
            {
                objBeListParam = null;
            }
            return objBeListParam;
        }
        public string GETListarParametrosxCodigo(string  valores)
        {
            //ParametrosBackOffieBEList lista = new ParametrosBackOffieBEList();
            //IDataReader DReader = null;
            //MANTRA 27/06/2012: string valor=string.Empty ;
            try
            {
                //MANTRA 27/06/2012: Se cambia la forma de leer parámetros, en lugar de leerlos de la BD se leerán del Web.config
                /*
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_PARAMETROS_BO]";
                    db.AddParameter("@vi_nid_parametro", DbType.Int64, ParameterDirection.Input, Convert.ToInt32(valores));
                    valor = db.ExecuteScalar().ToString();
                }
                 */
                return ConfigurationManager.AppSettings[valores].ToString();
                //while (DReader.Read())
                //{
                //    ParametrosBackOfficeBE oParametrosBE = CrearEntidad_Parametros(DReader);
                //    lista.Add(oParametrosBE);
                //}

                //DReader.Close();
            }
            catch (SqlException ex)
            {
                //if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw ex;
                //return null;
            }
            //MANTRA 27/06/2012 return valor;
        }
        public ParametrosBackOffieBEList GETListarParametros()
        {
            ParametrosBackOffieBEList lista = new ParametrosBackOffieBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_PARAMETROS]";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ParametrosBackOfficeBE oParametrosBE = CrearEntidad_Parametros(DReader);
                    lista.Add(oParametrosBE);
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
        public int GETActualizarParametro(ParametrosBackOfficeBE ent)
        {
            //IDataReader DReader = null;
            int r = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_INS_PARAMETROS]";
                    db.AddParameter("@nid_parametro", DbType.Int64, ParameterDirection.Input, ent.nid_parametro);
                    db.AddParameter("@no_tipo_valor", DbType.String, ParameterDirection.Input, ent.no_tipo_valor);
                    db.AddParameter("@valor", DbType.String, ParameterDirection.Input, ent.valor);
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario);
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.co_usuario_red);
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    r = db.Execute();
                }
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }        

        #region " Populate "

        private string Valor_TipoConsolHora(string valor)
        {
            string v_Cod_Pais = ConfigurationManager.AppSettings["CodPais"].ToString().Trim();
            string v_valor = "";
            if (v_Cod_Pais.Equals("1"))
            {
                if (valor.Equals("1"))
                {
                    v_valor = ConfigurationManager.AppSettings["nDepartamento_1"].ToString().Trim();
                }
                else if (valor.Equals("2")) { v_valor = ConfigurationManager.AppSettings["nProvincia_1"].ToString().Trim(); }
                else if (valor.Equals("3")) { v_valor = ConfigurationManager.AppSettings["nDistrito_1"].ToString().Trim(); }
            }
            else
            {
                if (v_Cod_Pais.Equals("2"))
                {
                    if (valor.Equals("1"))
                    {
                        v_valor = ConfigurationManager.AppSettings["nDepartamento_2"].ToString().Trim();
                    }
                    else if (valor.Equals("2")) { v_valor = ConfigurationManager.AppSettings["nProvincia_2"].ToString().Trim(); }
                    else if (valor.Equals("3")) { v_valor = ConfigurationManager.AppSettings["nDistrito_2"].ToString().Trim(); }
                }
            }

            return v_valor;
        }
        private ParametrosBackOfficeBE CrearEntidad_Parametros(IDataRecord DReader)
        {
            ParametrosBackOfficeBE Entidad = new ParametrosBackOfficeBE();
            int indice;
            string var3 = "";

            indice = DReader.GetOrdinal("nid_parametro");
            Entidad.nid_parametro = DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("co_parametro");
            Entidad.co_parametro = DReader.IsDBNull(indice) ? "" : DReader.GetString(indice);

            indice = DReader.GetOrdinal("no_tipo_valor");
            Entidad.no_tipo_valor = DReader.IsDBNull(indice) ? "" : DReader.GetString(indice);

            var3 = Entidad.no_tipo_valor;

            indice = DReader.GetOrdinal("valor");
            Entidad.valor = DReader.IsDBNull(indice) ? "" : DReader.GetString(indice);

            indice = DReader.GetOrdinal("valor_texto");
            if (var3.Equals("STRING_DISPLAY"))
            {
                Entidad.valor_texto = DReader.IsDBNull(indice) ? "" : Valor_TipoConsolHora(Entidad.valor);
            }
            else
            {
                Entidad.valor_texto = DReader.IsDBNull(indice) ? "" : DReader.GetString(indice);
            }

            indice = DReader.GetOrdinal("no_parametro");
            Entidad.no_parametro = DReader.IsDBNull(indice) ? "" : DReader.GetString(indice);

            return Entidad;
        }
        private ParametrosBackOfficeBE CrearEntidad_HoraSRC(IDataRecord DReader)
        {
            ParametrosBackOfficeBE Entidad = new ParametrosBackOfficeBE();
            int indice;

            indice = DReader.GetOrdinal("ID");
            var1 = DReader.GetString(indice);
            Entidad.ID = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("Des");
            var1 = DReader.GetString(indice);
            Entidad.DES = (var1 == null ? "" : var1);

            return Entidad;
        }


        private ParametrosBackOfficeBE Entidad_ListarConfigTallerEmpresa(IDataRecord DReader)
        {
            ParametrosBackOfficeBE Entidad = new ParametrosBackOfficeBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_taller_empresa");
            Entidad.nid_taller_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_empresa");
            Entidad.nid_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_empresa");
            Entidad.no_empresa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_banco");
            Entidad.no_banco = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_cuenta");
            Entidad.nu_cuenta = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_correo_callcenter");
            Entidad.no_correo_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_callcenter");
            Entidad.nu_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private ParametrosBackOfficeBE Entidad_ListarDatosTallerEmpresa(IDataRecord DReader)
        {
            ParametrosBackOfficeBE Entidad = new ParametrosBackOfficeBE();
            Int32 indice;

            indice = DReader.GetOrdinal("nid_taller_empresa");
            Entidad.nid_taller_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nid_taller");
            Entidad.nid_taller = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_empresa");
            Entidad.nid_empresa = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_empresa");
            Entidad.no_empresa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_banco");
            Entidad.no_banco = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_cuenta");
            Entidad.nu_cuenta = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_correo_callcenter");
            Entidad.no_correo_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_callcenter");
            Entidad.nu_callcenter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }


        #endregion

    }
}
