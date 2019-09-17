using System;
using AppMiTaller.Intranet.DA;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.BL
{
    public class TipoServicioBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;
        public TipoServicioBEList BusqTServicioList(TipoServicioBE ent)
        {
            try
            {
                return new TipoServicioDA().BusqTServicioList(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }
        public Int32 InsertTServicio(TipoServicioBE ent)
        {
            try
            {
                return new TipoServicioDA().InsertTServicio(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 1;
            }

        }
        public Int32 ActualizarTServicio(TipoServicioBE ent)
        {
            try
            {
                return new TipoServicioDA().UpdateTServicio(ent);
            }
            catch //(Exception ex)
            {
                return 1;
            }
        }


        public TipoServicioBEList GETListarTiposServicio(TipoServicioBE ent)
        {
            return new TipoServicioDA().GETListarTiposServicio(ent);
        }

    }
}