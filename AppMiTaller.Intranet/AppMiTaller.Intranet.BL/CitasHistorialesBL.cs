using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class CitasHistorialesBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public HistorialCitasxVehiculoBE GetListarHistorialCitasxVehiculo(String pPlaca)
        {
            try
            {
                return new CitasHistorialesDA().GetListarHistorialCitasxVehiculo(pPlaca);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }
        public HistorialServiciosxVehiculoBE GetListarHistorialServiciosxVehiculoBE(String pPlaca)
        {
            return new CitasHistorialesDA().GetListarHistorialServiciosxVehiculoBE(pPlaca);
        }
        public CalculadoraBE GetListarCalculadoraBE(String pPlaca) 
        {
            return new CitasHistorialesDA().GetListaCalculadora(pPlaca);
        }
    }
}
