using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class AdminCitasBL
    {
        public CombosBEList GETListarEstCitas()
        {
            return new AdminCitasDA().GETListarEstCitas();
        }
        public CombosBEList GETListarTalleres(int PuntoRed, int nid_usuario)
        {
            return new AdminCitasDA().GETListarTalleres(PuntoRed, nid_usuario);
        }
        public AdminCitaBEList GETListaAdminCitasP(AdminCitaBE ent, int nid_usuario)
        {
            return new AdminCitasDA().GETListaAdminCitasP(ent, nid_usuario);
        }
        public AdminCitaBEList GETListaAdminCitasDetalle(AdminCitaBE ent)
        {
            return new AdminCitasDA().GETListaAdminCitasDetalle(ent);
        }
        public int GETListaAdminCitasDetalleUpdCliente(AdminCitaBE ent, ClienteBE entCliente)
        {
            return new AdminCitasDA().GETListaAdminCitasDetalleUpdCliente(ent, entCliente);
        }
        public AdminCitaBEList GETAdminCitasVehPropietario(AdminCitaBE ent)
        {
            return new AdminCitasDA().GETAdminCitasVehPropietario(ent); 
        }
        public int GETAdminCitasVehPropietarioUpd(AdminCitaBE ent)
        {
            return new AdminCitasDA().GETAdminCitasVehPropietarioUpd(ent);
        }
        public int UPDAdminCitaEstPendiente(AdminCitaBE ent)
        {
            return new AdminCitasDA().UPDAdminCitaEstPendiente(ent);
        }
        public int UPDAdminCitaReasignar(AdminCitaBE ent)
        {
            return new AdminCitasDA().UPDAdminCitaReasignar(ent);
        }
        public AdminCitaBE INSAdminCitaColaEspera(AdminCitaBE ent)
        {
            return new AdminCitasDA().INSAdminCitaColaEspera(ent);
        }
        
        public int UPDAdminCitaObservacion(AdminCitaBE ent)
        {
            return new AdminCitasDA().UPDAdminCitaObservacion(ent);
        }
        
    }
}
