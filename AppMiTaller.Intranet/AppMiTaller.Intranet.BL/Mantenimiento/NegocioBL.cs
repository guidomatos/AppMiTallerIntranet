using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class NegocioBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public NegocioBEList ListarNegocio()
        {
            try
            {
                NegocioDA oNegocioDA = new NegocioDA();
                return oNegocioDA.ListarNegocio();
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }

            return null;
        }
    }
}
