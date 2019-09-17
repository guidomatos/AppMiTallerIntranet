using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class CitasHistorialesDA
    {
        public HistorialCitasxVehiculoBE GetListarHistorialCitasxVehiculo(String pPlaca)
        {
            HistorialCitasxVehiculoBE oHistorial = new HistorialCitasxVehiculoBE();

            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_GET_DATOS_VEH_PROP]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, pPlaca);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    oHistorial = CrearEntidadHistorialCita(DReader);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return oHistorial;
        }
        public HistorialServiciosxVehiculoBE GetListarHistorialServiciosxVehiculoBE(String pPlaca)
        {
            HistorialServiciosxVehiculoBE oHistorial = new HistorialServiciosxVehiculoBE();

            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_GET_DATOS_VEH_PROP]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, pPlaca);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    oHistorial = CrearEntidadHistorialServ(DReader);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return oHistorial;
        }
        public CalculadoraBE GetListaCalculadora(String pPlaca)
        {
            CalculadoraBE Calc = new CalculadoraBE();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_GET_CALCULADORA]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, pPlaca);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    Calc = CrearEntidadCalculadora(DReader);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return Calc;
        }

        private HistorialCitasxVehiculoBE CrearEntidadHistorialCita(IDataRecord DReader)
        {
            HistorialCitasxVehiculoBE Entidad = new HistorialCitasxVehiculoBE();
            int indice;

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_cliente");
            Entidad.co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("doc_cliente");
            Entidad.doc_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            

            Entidad.lstcitas = GetListaCitas(Entidad.nu_placa);

            return Entidad;
        }
        private HistorialServiciosxVehiculoBE CrearEntidadHistorialServ(IDataRecord DReader)
        {
            HistorialServiciosxVehiculoBE Entidad = new HistorialServiciosxVehiculoBE();
            int indice;

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_documento");
            Entidad.nu_documento = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_cliente");
            Entidad.no_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_telefono");
            Entidad.nu_telefono = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nu_celular");
            Entidad.nu_celular = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_correo");
            Entidad.no_correo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("co_tipo_cliente");
            Entidad.co_tipo_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("doc_cliente");
            Entidad.doc_cliente = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            return Entidad;
        }

        private CitasxVehiculoBEList GetListaCitas(String pPlaca)
        {
            CitasxVehiculoBEList lista = new CitasxVehiculoBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_GET_HIST_CITAS_X_VEH]";
                    db.AddParameter("@nu_placa", DbType.String, ParameterDirection.Input, pPlaca);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    CitasxVehiculoBE oCitaxVehBE = CrearEntidadCita(DReader);
                    lista.Add(oCitaxVehBE);
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
        private CitasxVehiculoBE CrearEntidadCita(IDataRecord DReader)
        {
            CitasxVehiculoBE Entidad = new CitasxVehiculoBE();
            int indice;

            indice = DReader.GetOrdinal("Itm");
            Entidad.Itm = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("nid_cita");
            Entidad.nid_cita = (DReader.IsDBNull(indice) ? 0: DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_estado_cita");
            Entidad.co_estado_cita = (DReader.IsDBNull(indice) ? 0: DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_dpto");
            Entidad.no_dpto = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_ubica");
            Entidad.no_ubica = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_taller");
            Entidad.no_taller = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nom_estado");
            Entidad.nom_estado = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("AsesorServ");
            Entidad.AsesorServ = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("Fecha");
            Entidad.Fecha = (DReader.IsDBNull(indice) ? "" : Convert.ToDateTime(DReader.GetString(indice)).ToString("dd/MM/yyyy"));
            indice = DReader.GetOrdinal("hora");
            Entidad.hora = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.no_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.no_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fecultser");
            Entidad.fecultser = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("ultkm");
            Entidad.ultkm = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            return Entidad;
        }
        private CalculadoraBE CrearEntidadCalculadora(IDataRecord DReader)
        {
            CalculadoraBE Entidad = new CalculadoraBE();
            int indice;

            indice = DReader.GetOrdinal("nu_placa");
            Entidad.nu_placa = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_marca");
            Entidad.no_marca = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_modelo");
            Entidad.no_modelo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("OT");
            Entidad.ot = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("km_ult_serv");
            Entidad.km_ult_serv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("fec_ult_serv");
            Entidad.fec_ult_serv = (DReader.IsDBNull(indice) ? "" : Convert.ToDateTime(DReader.GetString(indice)).ToString("dd/MM/yyyy"));
            indice = DReader.GetOrdinal("km_prm_ult_serv");
            Entidad.km_prm_ult_serv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("km_prox_serv");
            Entidad.km_prox_serv = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("fecproxServ");
            Entidad.fecproxServ = (DReader.IsDBNull(indice) ? "" : Convert.ToDateTime(DReader.GetString(indice)).ToString("dd/MM/yyyy"));

            return Entidad;
        }
    }
}