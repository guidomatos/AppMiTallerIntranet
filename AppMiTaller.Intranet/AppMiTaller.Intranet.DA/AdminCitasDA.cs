using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppMiTaller.Intranet.BE;


namespace AppMiTaller.Intranet.DA
{
    public class AdminCitasDA
    {
        public CombosBEList GETListarEstCitas()
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DDL_ESTCITAS]";
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
        public CombosBEList GETListarTalleres(int PuntoRed, int nid_usuario)
        {
            CombosBEList lista = new CombosBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_SEL_DDL_TALLER]";
                    db.AddParameter("@NID_UBICA", DbType.Int32, ParameterDirection.Input, PuntoRed);
                    //db.AddParameter("@nid_usuario", DbType.String, ParameterDirection.Input, nid_usuario);
                    db.AddParameter("@nid_usuario", DbType.Int32, ParameterDirection.Input, nid_usuario);
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
        public AdminCitaBEList GETListaAdminCitasP(AdminCitaBE ent, int nid_usuario)
        {
            AdminCitaBEList lista = new AdminCitaBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_CITAS]";
                    db.AddParameter("@cod_reserva_cita", DbType.String, ParameterDirection.Input, ent.cod_reserva_cita);
                    db.AddParameter("@coddpto", DbType.String, ParameterDirection.Input, ent.coddpto);
                    db.AddParameter("@codprov", DbType.String, ParameterDirection.Input, ent.codprov);
                    db.AddParameter("@coddist", DbType.String, ParameterDirection.Input, ent.coddist);
                    db.AddParameter("@nid_ubica", DbType.Int32, ParameterDirection.Input, ent.nid_ubica);
                    db.AddParameter("@nid_taller", DbType.Int32, ParameterDirection.Input, ent.nid_taller);
                    db.AddParameter("@AsesorServicio", DbType.String, ParameterDirection.Input, ent.AsesorServicio);
                    db.AddParameter("@Estadoreserva", DbType.String, ParameterDirection.Input, ent.Estadoreserva);
                    db.AddParameter("@IndPendiente", DbType.String, ParameterDirection.Input, ent.IndPendiente);
                    db.AddParameter("@fecreg1", DbType.String, ParameterDirection.Input, ent.fecreg1);
                    db.AddParameter("@fecreg2", DbType.String, ParameterDirection.Input, ent.fecreg2);
                    db.AddParameter("@feccita1", DbType.String, ParameterDirection.Input, ent.feccita1);
                    db.AddParameter("@feccita2", DbType.String, ParameterDirection.Input, ent.feccita2);
                    db.AddParameter("@horacita1", DbType.String, ParameterDirection.Input, ent.horacita1);
                    db.AddParameter("@horacita2", DbType.String, ParameterDirection.Input, ent.horacita2);
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, ent.nu_placa);
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, ent.nid_marca);
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, ent.nid_modelo);
                    db.AddParameter("@co_tipo_documento", DbType.String, ParameterDirection.Input, ent.co_tipo_documento);
                    db.AddParameter("@nu_documento", DbType.String, ParameterDirection.Input, ent.nu_documento);
                    db.AddParameter("@no_cliente", DbType.String, ParameterDirection.Input, ent.no_cliente);
                    db.AddParameter("@no_apellidos", DbType.String, ParameterDirection.Input, ent.no_apellidos);
                    db.AddParameter("@nid_usuario", DbType.String, ParameterDirection.Input, nid_usuario);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    AdminCitaBE oAdminCitaBE = CrearEntidad_AdminCitasP(DReader);
                    lista.Add(oAdminCitaBE);
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
        public AdminCitaBEList GETListaAdminCitasDetalle(AdminCitaBE ent)
        {
            AdminCitaBEList lista = new AdminCitaBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_CITAS_VER_DETALLE]";
                    db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita));
                    db.AddParameter("@nid_estado", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_estado));
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    lista.Add(CrearEntidad_AdminCitaDet(DReader));
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
        public int GETListaAdminCitasDetalleUpdCliente(AdminCitaBE ent,ClienteBE entCliente)
        {
            int rpta = 0;            
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_CITAS_CLIENTE]";
                    db.AddParameter("@vi_nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita));
                    db.AddParameter("@vi_no_cliente", DbType.String, ParameterDirection.Input, entCliente.no_cliente.ToString().Trim());
                    db.AddParameter("@vi_no_ape_pat", DbType.String, ParameterDirection.Input, entCliente.no_ape_pat.ToString().Trim());
                    db.AddParameter("@vi_no_ape_mat", DbType.String, ParameterDirection.Input, entCliente.no_ape_mat.ToString().Trim());                    
                    db.AddParameter("@vi_nu_documento", DbType.String, ParameterDirection.Input, entCliente.nu_documento.ToString().Trim());
                    db.AddParameter("@vi_nid_pais_telefono", DbType.Int32, ParameterDirection.Input, entCliente.nid_pais_telefono);
                    db.AddParameter("@vi_nu_telefono", DbType.String, ParameterDirection.Input, entCliente.nu_telefono.ToString().Trim());
                    db.AddParameter("@vi_nu_telefono_anexo", DbType.String, ParameterDirection.Input, entCliente.nu_anexo_telefono);
                    db.AddParameter("@vi_nu_tel_oficina", DbType.String, ParameterDirection.Input, entCliente.nu_tel_oficina.ToString().Trim());
                    db.AddParameter("@vi_nid_pais_celular", DbType.Int32, ParameterDirection.Input, entCliente.nid_pais_celular);
                    db.AddParameter("@vi_nu_celular", DbType.String, ParameterDirection.Input, entCliente.nu_celular.ToString().Trim());
                    db.AddParameter("@vi_nu_celular_alter", DbType.String, ParameterDirection.Input, entCliente.nu_celular_alter.ToString().Trim());
                    db.AddParameter("@vi_no_correo", DbType.String, ParameterDirection.Input, entCliente.no_correo.ToString().Trim());
                    db.AddParameter("@vi_no_correo_trab", DbType.String, ParameterDirection.Input, entCliente.no_correo_trabajo.ToString().Trim());
                    db.AddParameter("@vi_no_correo_alter", DbType.String, ParameterDirection.Input, entCliente.no_correo_alter.ToString().Trim());
                    db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, entCliente.co_usuario_crea.ToString().Trim());
                    db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, entCliente.co_usuario_red.ToString().Trim());
                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, entCliente.no_estacion_red.ToString().Trim());
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
        public AdminCitaBEList GETAdminCitasVehPropietario(AdminCitaBE ent)
        {
            AdminCitaBEList lista = new AdminCitaBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_CITAS_VEH_PROPIETARIO]";
                    db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita.ToString().Trim()));
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    AdminCitaBE oAdminCitaBE = CrearEntidad_AdminCitaVehPropietario(DReader);
                    lista.Add(oAdminCitaBE);
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
        public int GETAdminCitasVehPropietarioUpd(AdminCitaBE ent)
        {
            int rpta = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_CITAS_VEH_PROPIETARIO]";
                    db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita.ToString().Trim()));
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, ent.DV_Placa.ToString().Trim());
                    db.AddParameter("@nid_marca", DbType.Int32, ParameterDirection.Input, int.Parse(ent.DV_Marca.ToString().Trim()));
                    db.AddParameter("@nid_modelo", DbType.Int32, ParameterDirection.Input, int.Parse(ent.DV_Modelo.ToString().Trim()));
                    db.AddParameter("@nid_vin", DbType.Int32, ParameterDirection.Input, int.Parse(ent.DV_NidVin.ToString().Trim()));
                    db.AddParameter("@nu_vin", DbType.String, ParameterDirection.Input, ent.DV_NroVin.ToString().Trim());
                    db.AddParameter("@no_ape_pat", DbType.String, ParameterDirection.Input, ent.DP_Paterno.ToString().Trim());
                    db.AddParameter("@no_ape_mat", DbType.String, ParameterDirection.Input, ent.DP_Materno.ToString().Trim());
                    db.AddParameter("@no_cliente", DbType.String, ParameterDirection.Input, ent.DP_Nombre.ToString().Trim());
                    db.AddParameter("@co_tipo_documento", DbType.String, ParameterDirection.Input, ent.DP_TipoDoc.ToString().Trim());
                    db.AddParameter("@nu_documento", DbType.String, ParameterDirection.Input, ent.DP_NroDoc.ToString().Trim());
                    db.AddParameter("@nu_telefono", DbType.String, ParameterDirection.Input, ent.DP_NroTel.ToString().Trim());
                    db.AddParameter("@nu_celular", DbType.String, ParameterDirection.Input, ent.DP_NroCel.ToString().Trim());
                    db.AddParameter("@no_correo", DbType.String, ParameterDirection.Input, ent.DP_Email.ToString().Trim());
                    db.AddParameter("@C_no_ape_pat", DbType.String, ParameterDirection.Input, ent.DC_Paterno.ToString().Trim());
                    db.AddParameter("@C_no_ape_mat", DbType.String, ParameterDirection.Input, ent.DC_Materno.ToString().Trim());
                    db.AddParameter("@C_no_cliente", DbType.String, ParameterDirection.Input, ent.DC_Nombre.ToString().Trim());
                    db.AddParameter("@C_co_tipo_documento", DbType.String, ParameterDirection.Input, ent.DC_TipoDoc.ToString().Trim());
                    db.AddParameter("@C_nu_documento", DbType.String, ParameterDirection.Input, ent.DC_NroDoc.ToString().Trim());
                    db.AddParameter("@C_nu_telefono", DbType.String, ParameterDirection.Input, ent.DC_NroTel.ToString().Trim());
                    db.AddParameter("@C_nu_celular", DbType.String, ParameterDirection.Input, ent.DC_NroCel.ToString().Trim());
                    db.AddParameter("@C_no_correo", DbType.String, ParameterDirection.Input, ent.DC_Email.ToString().Trim());
                    db.AddParameter("@co_usuario_modi", DbType.String, ParameterDirection.Input, ent.CTRECOR_co_usuario_modi.ToString().Trim());
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
        public int UPDAdminCitaEstPendiente(AdminCitaBE ent)
        {
            int rpta = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_CITAS_EST_PENDIENTE]";
                    db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita.ToString().Trim()));
                    db.AddParameter("@IndPendiente", DbType.String, ParameterDirection.Input, ent.grid_IndPendiente.ToString().Trim());
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
        public int UPDAdminCitaReasignar(AdminCitaBE ent)
        {
            //int EMAIL = 0;
            int rpta = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPI_REASIGNAR_CITA_BO]";
                    db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita.ToString().Trim()));
                    db.AddParameter("@nid_usuario", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_Id_Asesor.ToString().Trim()));
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.CTRECOR_co_usuario_modi.ToString().Trim());
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.CTRECOR_co_usuario_red.ToString().Trim());
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.CTRECOR_no_estacion_red.ToString().Trim());
                    rpta = db.Execute();
                }
                //if (rpta > 0)
                //{
                //    try
                //    {
                //        using (Database db = new Database())
                //        {
                //            db.ProcedureName = "[SRC_SPEMAIL_ASESOR_REASIGNAR]";
                //            db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita.ToString().Trim()));
                //            db.AddParameter("@nid_usuario", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_Id_Asesor.ToString().Trim()));
                //            db.Execute();
                //        }
                //    }
                //    catch (Exception)
                //    {
                //        EMAIL = 0;
                //    }                    
                //}
            }
            catch (Exception)
            {
                rpta = 0;
                throw;
            }
            return rpta;
        }
        public AdminCitaBE INSAdminCitaColaEspera(AdminCitaBE ent)
        {
            AdminCitaBE lista_ENT = new AdminCitaBE();
            IDataReader DReader = null;
            int indice;
            //string var1 = "";
            
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPI_CITAS_COLADEESPERA_BO]";
                    db.AddParameter("@nid_cita", DbType.Int32, ParameterDirection.Input, int.Parse(ent.grid_nid_cita.ToString().Trim()));
                    db.AddParameter("@nid_usuario", DbType.Int32, ParameterDirection.Input, int.Parse(ent.nid_usuario.ToString().Trim()));
                    db.AddParameter("@co_usuario_crea", DbType.String, ParameterDirection.Input, ent.CTRECOR_co_usuario_modi.ToString().Trim());
                    db.AddParameter("@co_usuario_red", DbType.String, ParameterDirection.Input, ent.CTRECOR_co_usuario_red.ToString().Trim());
                    db.AddParameter("@no_estacion_red", DbType.String, ParameterDirection.Input, ent.CTRECOR_no_estacion_red.ToString().Trim());
                    db.AddParameter("@fl_activo", DbType.String, ParameterDirection.Input, ent.fl_activo.ToString().Trim());
                    db.AddParameter("@ho_inicio", DbType.String, ParameterDirection.Input, ent.grid_HORA_CITA.ToString().Trim());
                    DReader = db.GetDataReader();
                }

                while (DReader.Read())
                {
                    indice = DReader.GetOrdinal("vo_resultado");
                    lista_ENT.bo_resultado = DReader.GetInt32(indice);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                lista_ENT.bo_resultado = 0;
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista_ENT;
        }
        public int UPDAdminCitaObservacion(AdminCitaBE ent)
        {
            int rpta = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPU_OBSERVACION_CITA_BO]";
                    db.AddParameter("@VI_NID_CITA", DbType.Int32, ParameterDirection.Input, ent.nid_cita  );
                    db.AddParameter("@VI_TXT_OBSER", DbType.String, ParameterDirection.Input, ent.DET_Observaciones);               
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


        #region "POPULATE"

        private ComboBE CrearEntidad_Combo(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            int indice;
            string var1 = "";

            indice = DReader.GetOrdinal("ID");
            var1 = DReader.GetString(indice);
            Entidad.ID = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DES");
            var1 = DReader.GetString(indice);
            Entidad.DES = (var1 == null ? "" : var1);

            return Entidad;
        }
        private ComboBE CrearEntidad_Combo2(IDataRecord DReader)
        {
            ComboBE Entidad = new ComboBE();
            int indice;
            string var1 = "";
            int var2 = 0;

            indice = DReader.GetOrdinal("ID");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.ID = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DES");
            var1 = DReader.GetString(indice);
            Entidad.DES = (var1 == null ? "" : var1);

            return Entidad;
        }
        private AdminCitaBE CrearEntidad_AdminCitasP(IDataRecord DReader)
        {
            AdminCitaBE Entidad = new AdminCitaBE();
            int indice;
            string var1 = "";
            int var2 = 0;

            indice = DReader.GetOrdinal("nid_cita");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.grid_nid_cita = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("nid_estado");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.grid_nid_estado = (var1 == null ? "" : var1.Trim()); 

            indice = DReader.GetOrdinal("cod_reserva_cita");
            var1 = DReader.GetString(indice);
            Entidad.grid_cod_reserva_cita = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("FE_HORA_REG");
            var1 = DReader.GetString(indice);
            Entidad.grid_FE_HORA_REG = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("FECHA_CITA");
            var1 = DReader.GetString(indice);
            Entidad.grid_FECHA_CITA = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("HORA_CITA");
            var1 = DReader.GetString(indice);
            Entidad.grid_HORA_CITA = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("ESTADO_CITA");
            var1 = DReader.GetString(indice);
            Entidad.grid_ESTADO_CITA = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("Departamento");
            var1 = DReader.GetString(indice);
            Entidad.grid_Departamento = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("Provincia");
            var1 = DReader.GetString(indice);
            Entidad.grid_Provincia = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("Distrito");
            var1 = DReader.GetString(indice);
            Entidad.grid_Distrito = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("Punto_RED");
            var1 = DReader.GetString(indice);
            Entidad.grid_Punto_RED = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("Taller");
            var1 = DReader.GetString(indice);
            Entidad.grid_Taller = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("AsesorServicio");
            var1 = DReader.GetString(indice);
            Entidad.grid_AsesorServicio = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("PlacaPatente");
            var1 = DReader.GetString(indice);
            Entidad.grid_PlacaPatente = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("NU_DOCUMENTO");
            var1 = DReader.GetString(indice);
            Entidad.grid_NumDocumento = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("NomCliente");
            var1 = DReader.GetString(indice);
            Entidad.grid_NomCliente = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("ApeCliente");
            var1 = DReader.GetString(indice);
            Entidad.grid_ApeCliente = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("IndPendiente");
            var1 = DReader.GetString(indice);
            Entidad.grid_IndPendiente = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("TelefonoCliente");
            var1 = DReader.GetString(indice);
            Entidad.grid_TelefonoCliente = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("EmailCliente");
            var1 = DReader.GetString(indice);
            Entidad.grid_EmailCliente = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("nid_tallerCita");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.grid_nid_tallerCita = (var1 == null ? "" : var1.Trim());
            
            indice = DReader.GetOrdinal("nid_servicioCita");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.grid_nid_servicioCita = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("no_dias_validos");
            var1 = DReader.GetString(indice);
            Entidad.grid_no_dias_validos = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("IntervaloTaller");
            var1 = DReader.GetString(indice);
            Entidad.grid_IntervaloTaller = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("Id_Asesor");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.grid_Id_Asesor = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("HORA_CITA_FIN");
            var1 = DReader.GetString(indice);
            Entidad.grid_HORA_CITA_FIN = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("nid_modelo");
            var2 = DReader.GetInt32(indice);
            var1 = var2.ToString();
            Entidad.grid_nid_modelo = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("CO_USUARIO_CREA");
            var1 = DReader.GetString(indice);
            Entidad.grid_co_usuario_crea = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("CO_USUARIO_MODI");
            var1 = DReader.GetString(indice);
            Entidad.grid_co_usuario_modi = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("no_servicio");
            var1 = DReader.GetString(indice);
            Entidad.grid_no_servicio = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("no_marca");
            var1 = DReader.GetString(indice);
            Entidad.grid_no_marca = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("no_modelo");
            var1 = DReader.GetString(indice);
            Entidad.grid_no_modelo = (var1 == null ? "" : var1.Trim());

            indice = DReader.GetOrdinal("nu_vin");
            var1 = DReader.GetString(indice);
            Entidad.grid_nu_vin = (var1 == null ? "" : var1.Trim()); 


            return Entidad;
        }
        private AdminCitaBE CrearEntidad_AdminCitaDet(IDataRecord DReader)
        {
            AdminCitaBE Entidad = new AdminCitaBE();
            Int32 indice = 0;

            indice = DReader.GetOrdinal("fe_programada");
            Entidad.DET_Fecha = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ho_inicio");
            Entidad.DET_Hora = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_asesor");
            Entidad.DET_AsesorServicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ubica_corto");
            Entidad.DET_Ubicacion = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("cod_reserva_cita");
            Entidad.DET_CodigoReserva = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.DET_Servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.DET_TipoServicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_estado");
            Entidad.DET_Estado = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.DET_Nombre = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_pat");
            Entidad.DET_ApePat = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ape_mat");
            Entidad.DET_ApeMat = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_documento");
            Entidad.co_tipo_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));            
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.DET_DNI = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.DET_TelfFijo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_tel_oficina");
            Entidad.DET_TelfOficina = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.DET_TelfMovil = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular_alter");
            Entidad.DET_TelfMovil2 = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.DET_Email = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_trabajo");
            Entidad.DET_Email_Trab = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo_alter");
            Entidad.DET_Email_Alter = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_placa");
            Entidad.DET_Placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.DET_Marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.DET_Modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("tx_observacion");
            Entidad.DET_Observaciones = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fl_identidad_validada");
            Entidad.fl_identidad_validada = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_pais_celular");
            Entidad.nid_pais_celular = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_pais_telefono");
            Entidad.nid_pais_telefono= (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nu_anexo_telefono");
            Entidad.nu_anexo_telefono= (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private AdminCitaBE CrearEntidad_AdminCitaVehPropietario(IDataRecord DReader)
        {
            AdminCitaBE Entidad = new AdminCitaBE();
            int indice;
            string var1 = "";
            int var2 = 0;

            indice = DReader.GetOrdinal("DV_Placa");
            var1 = DReader.GetString(indice);
            Entidad.DV_Placa=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DV_Marca");
            var2=DReader.GetInt32(indice);
            Entidad.DV_Marca=var2;

            indice = DReader.GetOrdinal("DV_Modelo");
            var2=DReader.GetInt32(indice);
            Entidad.DV_Modelo = var2;

            indice = DReader.GetOrdinal("DV_NidVin");
            var2=DReader.GetInt32(indice);
            Entidad.DV_NidVin= var2;

            indice = DReader.GetOrdinal("DV_NroVin");
            var1 = DReader.GetString(indice);
            Entidad.DV_NroVin = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_Paterno");
            var1 = DReader.GetString(indice);
            Entidad.DP_Paterno=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_Materno");
            var1 = DReader.GetString(indice);
            Entidad.DP_Materno=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_Nombre");
            var1 = DReader.GetString(indice);
            Entidad.DP_Nombre=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_TipoDoc");
            var1 = DReader.GetString(indice);
            Entidad.DP_TipoDoc=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_NroDoc");
            var1 = DReader.GetString(indice);
            Entidad.DP_NroDoc=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_NroTel");
            var1 = DReader.GetString(indice);
            Entidad.DP_NroTel=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_NroCel");
            var1 = DReader.GetString(indice);
            Entidad.DP_NroCel=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DP_Email");
            var1 = DReader.GetString(indice);
            Entidad.DP_Email=(var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_Paterno");
            var1 = DReader.GetString(indice);
            Entidad.DC_Paterno = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_Materno");
            var1 = DReader.GetString(indice);
            Entidad.DC_Materno = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_Nombre");
            var1 = DReader.GetString(indice);
            Entidad.DC_Nombre = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_TipoDoc");
            var1 = DReader.GetString(indice);
            Entidad.DC_TipoDoc = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_NroDoc");
            var1 = DReader.GetString(indice);
            Entidad.DC_NroDoc = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_NroTel");
            var1 = DReader.GetString(indice);
            Entidad.DC_NroTel = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_NroCel");
            var1 = DReader.GetString(indice);
            Entidad.DC_NroCel = (var1 == null ? "" : var1);

            indice = DReader.GetOrdinal("DC_Email");
            var1 = DReader.GetString(indice);
            Entidad.DC_Email = (var1 == null ? "" : var1);

            return Entidad;
        }
        #endregion
   
    }
}
