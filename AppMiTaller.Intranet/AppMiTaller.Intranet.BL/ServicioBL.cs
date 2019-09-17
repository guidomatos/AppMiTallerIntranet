using System;
using AppMiTaller.Intranet.DA;
using AppMiTaller.Intranet.BE;
using System.Collections.Generic;

namespace AppMiTaller.Intranet.BL
{
    public class ServicioBL
    {
        public ServicioBEList BusqServicioList(ServicioBE ent)

        {
            return new ServicioDA().BusqServicioList(ent);
        }
        public Int32 InsertServicio(ServicioBE ent)
        {
            try
            {
                return new ServicioDA().InsertServicio(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 1;
            }

        }
        public Int32 ActualizarServicio(ServicioBE ent)
        {
            try
            {
                return new ServicioDA().UpdateServicio(ent);
            }
            catch //(Exception ex)
            {
                return 1;
            }
        }

        public ServicioBEList GETListarServiciosPorTipo(ServicioBE ent)
        {
            return new ServicioDA().GETListarServiciosPorTipo(ent);
        }
        public ServicioBEList GETListarDatosServicios(ServicioBE ent)
        {
            return new ServicioDA().GETListarDatosServicios(ent);
        }
        public IList<ServicioBE> GetAllServicioBe(int nid_marca, int nid_modelo, string orderby, string orderbydirection)
        {
            return new ServicioDA().GetAllServicioBe(nid_marca, nid_modelo, orderby, orderbydirection);
        }
        public IList<ServicioBE> GetAllServicioAsignadoBe(int nid_marca, int nid_modelo, string orderby, string orderbydirection)
        {
            return new ServicioDA().GetAllServicioAsignadoBe(nid_marca, nid_modelo, orderby, orderbydirection);
        }
        public IList<ServicioBE> GetAllServicioModelo(int nid_marca, int nid_modelo, int pagina_actual, ref int cantidadregistros, string orderby, string orderbydirection)
        {
            return new ServicioDA().GetAllServicioModelo(nid_marca, nid_modelo, pagina_actual, ref cantidadregistros, orderby, orderbydirection);
        }
        public void AddServicioModelo(ServicioBE oServicioBe)
        {
            new ServicioDA().AddServicioModelo(oServicioBe);
        }
        public void UpdateServicioModelo(ServicioBE oServicioBe)
        {
            new ServicioDA().UpdateServicioModelo(oServicioBe);
        }
    }
}