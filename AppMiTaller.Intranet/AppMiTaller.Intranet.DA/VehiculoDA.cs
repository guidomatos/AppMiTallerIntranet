using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class VehiculoDA
    {
        #region " Variable Global de la Clase "
        string var1 = "";
        int var2 = 0;
        #endregion

        public VehiculoBEList GETListarVehiculos(VehiculoBE ent, Int32 Nid_usuario)
        {
            VehiculoBEList lista = new VehiculoBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_VEHICULO]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, ent.nu_placa);
                    db.AddParameter("@nu_vin", DbType.String, ParameterDirection.Input, ent.nu_vin);
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, ent.nid_marca);
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, ent.nid_modelo);
                    db.AddParameter("@qt_km_actual", DbType.Int64, ParameterDirection.Input, ent.qt_km_actual);
                    db.AddParameter("@Estado", DbType.String, ParameterDirection.Input, ent.fl_activo);
                    db.AddParameter("@Nid_usuario", DbType.Int32, ParameterDirection.Input, Nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    VehiculoBE oBE = CrearEntidad_Parametros(DReader);
                    lista.Add(oBE);
                }

                DReader.Close();
            }
            catch //(Exception Ex)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }
        public CombosBEList GETListarModelosXMarca(VehiculoBE ent, int Nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DDL_MODELO]";
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, ent.nid_marca);
                    db.AddParameter("@Nid_usuario", DbType.String, ParameterDirection.Input, Nid_usuario);
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
        public CombosBEList GETListarTipoPersona()
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DDL_TIPOPERSONA]";
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
        public int GETInserActuVehiculo(VehiculoBE ent)
        {
            //IDataReader DReader = null;
            int r = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_INS_UPD_VEHICULO";
                    db.AddParameter("@nid_vehiculo", DbType.Int32, ParameterDirection.Input, ent.nid_vehiculo);
                    db.AddParameter("@nid_propietario", DbType.Int32, ParameterDirection.Input, ent.nid_propietario);
                    db.AddParameter("@nid_cliente", DbType.Int32, ParameterDirection.Input, ent.nid_cliente);
                    db.AddParameter("@nid_contacto", DbType.Int32, ParameterDirection.Input, ent.nid_contacto);
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, ent.nu_placa.Trim());
                    db.AddParameter("@nu_vin", DbType.String, ParameterDirection.Input, ent.nu_vin.Trim());
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, ent.nid_marca);
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, ent.nid_modelo);
                    db.AddParameter("@qt_km_actual", DbType.Int64, ParameterDirection.Input, ent.qt_km_actual);
                    db.AddParameter("@fl_reg_manual", DbType.String, ParameterDirection.Input, ent.fl_reg_manual.Trim());

                    if (ent.nu_anio == 0 || string.IsNullOrEmpty(ent.co_tipo))
                    {
                        db.AddParameter("@nu_anio", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                        db.AddParameter("@co_tipo", DbType.String, ParameterDirection.Input, DBNull.Value);
                    }
                    else
                    {
                        db.AddParameter("@nu_anio", DbType.Int32, ParameterDirection.Input, ent.nu_anio);
                        db.AddParameter("@co_tipo", DbType.String, ParameterDirection.Input, ent.co_tipo);
                    }
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, (ent.co_usuario == null ? "" : ent.co_usuario.ToString().Trim()));
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, (ent.co_usuario_red == null ? "" : ent.co_usuario_red.ToString().Trim()));
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, (ent.no_estacion_red == null ? "" : ent.no_estacion_red.ToString().Trim()));
                    db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, (ent.fl_activo == null ? "" : ent.fl_activo.ToString().Trim()));
                    db.AddParameter("@ind_accion", DbType.String, ParameterDirection.Input, (ent.ind_accion == null ? "" : ent.ind_accion.ToString().Trim()));

                    r = db.Execute();
                }
            }
            catch //(Exception)
            {
                r = 0;
            }
            return r;
        }
        public int GETVAL_EXIS_VEH(VehiculoBE ent)
        {
            IDataReader DReader = null;
            int r = 0;
            int indice;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SEL_VAL_EXISTE_VEHICULO]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, (ent.nu_placa == null ? "" : ent.nu_placa.ToString()));
                    db.AddParameter("@nu_vin", DbType.String, ParameterDirection.Input, (ent.nu_vin == null ? "" : ent.nu_vin.ToString()));

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    indice = DReader.GetOrdinal("RESULTADO");
                    var2 = DReader.GetInt32(indice);
                    r = var2;
                }
            }
            catch (Exception)
            {
                r = 0;
            }
            return r;
        }
        public string GETVIN_X_PLACA(VehiculoBE ent)
        {
            IDataReader DReader = null;
            string r = "";
            int indice;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_VIN_X_PLACA]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, (ent.nu_placa == null ? "" : ent.nu_placa.ToString()));

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    indice = DReader.GetOrdinal("nu_vin");
                    var1 = DReader.GetString(indice);
                    r = (var1 == null ? "" : var1);
                }
            }
            catch (Exception)
            {
                r = "";
            }
            return r;
        }
        public VehiculoBE PROP_CLIE_CONT_X_PLACA(VehiculoBE ent)
        {
            VehiculoBE objEntBus = null;
            IDataReader DReader = null;
            int indice;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DATOS_PRO_CLI_CONT_X_PLACA]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, (ent.nu_placa == null ? "" : ent.nu_placa.ToString()));

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    objEntBus = new VehiculoBE();

                    indice = DReader.GetOrdinal("per_pro");
                    var1 = DReader.GetString(indice);
                    objEntBus.per_pro = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("tipodoc_pro");
                    var1 = DReader.GetString(indice);
                    objEntBus.tipodoc_pro = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("nrodoc_pro");
                    var1 = DReader.GetString(indice);
                    objEntBus.nrodoc_pro = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("per_clie");
                    var1 = DReader.GetString(indice);
                    objEntBus.per_clie = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("tipodoc_clie");
                    var1 = DReader.GetString(indice);
                    objEntBus.tipodoc_clie = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("nrodoc_clie");
                    var1 = DReader.GetString(indice);
                    objEntBus.nrodoc_clie = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("per_cont");
                    var1 = DReader.GetString(indice);
                    objEntBus.per_cont = var1;

                    indice = DReader.GetOrdinal("tipodoc_cont");
                    var1 = DReader.GetString(indice);
                    objEntBus.tipodoc_cont = var1;

                    indice = DReader.GetOrdinal("nrodoc_cont");
                    var1 = DReader.GetString(indice);
                    objEntBus.nrodoc_cont = var1;
                }
            }
            catch (Exception)
            {
                objEntBus = null;
            }
            return objEntBus;
        }

        public VehiculoBE PROP_CLIE_CONT_X_NRO_VIN(VehiculoBE ent)
        {
            VehiculoBE objEntBus = null;
            IDataReader DReader = null;
            int indice;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DATOS_PRO_CLI_CONT_X_NRO_VIN]";
                    db.AddParameter("@nu_vin", DbType.String, ParameterDirection.Input, (ent.nu_vin == null ? "" : ent.nu_vin.ToString()));

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    objEntBus = new VehiculoBE();

                    indice = DReader.GetOrdinal("per_pro");
                    var1 = DReader.GetString(indice);
                    objEntBus.per_pro = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("tipodoc_pro");
                    var1 = DReader.GetString(indice);
                    objEntBus.tipodoc_pro = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("nrodoc_pro");
                    var1 = DReader.GetString(indice);
                    objEntBus.nrodoc_pro = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("per_clie");
                    var1 = DReader.GetString(indice);
                    objEntBus.per_clie = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("tipodoc_clie");
                    var1 = DReader.GetString(indice);
                    objEntBus.tipodoc_clie = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("nrodoc_clie");
                    var1 = DReader.GetString(indice);
                    objEntBus.nrodoc_clie = (var1 == null ? "" : var1);

                    indice = DReader.GetOrdinal("per_cont");
                    var1 = DReader.GetString(indice);
                    objEntBus.per_cont = var1;

                    indice = DReader.GetOrdinal("tipodoc_cont");
                    var1 = DReader.GetString(indice);
                    objEntBus.tipodoc_cont = var1;

                    indice = DReader.GetOrdinal("nrodoc_cont");
                    var1 = DReader.GetString(indice);
                    objEntBus.nrodoc_cont = var1;
                }
            }
            catch (Exception)
            {
                objEntBus = null;
            }
            return objEntBus;
        }

        public VehiculoBE ListarDatosClientesPorIDVehiculo(VehiculoBE ent)
        {
            VehiculoBE Entidad = null;
            IDataReader DReader = null;
            int indice;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_CLIENTES_POR_VEHICULO_BO]";
                    db.AddParameter("@VI_NID_VEHICULO", DbType.Int32, ParameterDirection.Input, ent.nid_vehiculo);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    Entidad = new VehiculoBE();

                    indice = DReader.GetOrdinal("nid_propietario");
                    Entidad.nid_propietario = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

                    indice = DReader.GetOrdinal("co_tipo_cliente_prop");
                    Entidad.per_pro = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("co_tipo_doc_prop");
                    Entidad.tipodoc_pro = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("nu_doc_prop");
                    Entidad.nrodoc_pro = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("nid_cliente");
                    Entidad.nid_cliente = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

                    indice = DReader.GetOrdinal("co_tipo_cliente_cli");
                    Entidad.per_clie = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("co_tipo_doc_cli");
                    Entidad.tipodoc_clie = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("nu_doc_cli");
                    Entidad.nrodoc_clie = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("nid_contacto");
                    Entidad.nid_contacto = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

                    indice = DReader.GetOrdinal("co_tipo_cliente_cont");
                    Entidad.per_cont = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("co_tipo_doc_cont");
                    Entidad.tipodoc_cont = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                    indice = DReader.GetOrdinal("nu_doc_cont");
                    Entidad.nrodoc_cont = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

                }
            }
            catch (Exception)
            {
                Entidad = null;
            }
            return Entidad;
        }

        public VehiculoBEList GETListarBuscarCliente(VehiculoBE ent)
        {
            VehiculoBEList lista = new VehiculoBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_CLIENTE_x_VEHICULO]";
                    db.AddParameter("@vi_co_tipo_cliente", DbType.String, ParameterDirection.Input, ent.DET_co_tipo_cliente);
                    db.AddParameter("@vi_co_tipo_documento", DbType.String, ParameterDirection.Input, ent.DET_co_tipo_documento);
                    db.AddParameter("@vi_nu_documento", DbType.String, ParameterDirection.Input, ent.DET_nu_documento);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    VehiculoBE oBE = CrearEntidad_BusCliente(DReader);
                    lista.Add(oBE);
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

        #region " Populate "
        private ComboBE CrearEntidad_Combo(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            int indice;

            indice = DReader.GetOrdinal("ID");
            Entidad.ID = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //var1 = DReader.GetString(indice);
            //Entidad.ID = (var1 == null ? "" : var1);


            indice = DReader.GetOrdinal("DES");
            Entidad.DES = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //var1 = DReader.GetString(indice);
            //Entidad.DES = (var1 == null ? "" : var1);

            return Entidad;
        }
        private VehiculoBE CrearEntidad_BusCliente(IDataRecord DReader)
        {
            VehiculoBE Entidad = new VehiculoBE();
            int indice;

            indice = DReader.GetOrdinal("cod_cliente");
            Entidad.DET_cod_cliente = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("co_tipo_cliente");
            Entidad.DET_co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("co_tipo_documento");
            Entidad.DET_co_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("NOMBRES_RZ");
            Entidad.DET_NOMBRES_RZ = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_cliente");
            Entidad.DET_no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_pat");
            Entidad.DET_no_ape_pat = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_mat");
            Entidad.DET_no_ape_mat = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.DET_nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.DET_nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_tel_oficina");
            Entidad.DET_nu_telefono2 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_celular");
            Entidad.DET_nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_alter");
            Entidad.DET_nu_celular2 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_correo");
            Entidad.DET_no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trabajo");
            Entidad.DET_no_correo_trab = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter");
            Entidad.DET_no_correo_alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //@001 I
            indice = DReader.GetOrdinal("fl_identidad_validada");
            Entidad.fl_identidad_validada = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            //@001 F
            //@002 I
            indice = DReader.GetOrdinal("nid_pais_celular");
            Entidad.nid_pais_celular = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_pais_telefono");
            Entidad.nid_pais_telefono = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nu_anexo_telefono");
            Entidad.nu_anexo_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            //@002 F
            return Entidad;
        }
        private VehiculoBE CrearEntidad_Parametros(IDataRecord DReader)
        {
            VehiculoBE Entidad = new VehiculoBE();
            int indice;

            indice = DReader.GetOrdinal("nid_vehiculo");
            Entidad.nid_vehiculo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_vin");
            Entidad.nu_vin = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_marca");
            Entidad.nid_marca = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nid_modelo");
            Entidad.nid_modelo = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("qt_km_actual");
            Entidad.qt_km_actual = Int64.Parse(DReader.GetString(indice));

            indice = DReader.GetOrdinal("Estado");
            Entidad.fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("nu_anio");
            Entidad.nu_anio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("co_tipo");
            Entidad.co_tipo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("no_tipo");
            Entidad.no_tipo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("pro_co_tipo_cliente");
            Entidad.pro_co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("pro_co_tipo_documento");
            Entidad.pro_co_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("pro_nu_documento");
            Entidad.pro_nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("clie_co_tipo_cliente");
            Entidad.clie_co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("clie_co_tipo_documento");
            Entidad.clie_co_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("clie_nu_documento");
            Entidad.clie_nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("cont_co_tipo_cliente");
            Entidad.cont_co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("cont_co_tipo_documento");
            Entidad.cont_co_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("cont_nu_documento");
            Entidad.cont_nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        #endregion
    }
}