using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class NegocioLineaBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public NegocioLineaBEList GetListaLineaComercial(Int32 idNegocioLinea)
        {
            NegocioLineaDA oNegocioLineaDA = new NegocioLineaDA();
            try
            {
                return oNegocioLineaDA.GetListaLineaComercial(idNegocioLinea);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }

        public NegocioLineaBEList GetListaLineaImportacion(String codNegocio)
        {
            NegocioLineaDA oNegocioLineaDA = new NegocioLineaDA();
            try
            {
                return oNegocioLineaDA.GetListaLineaImportacion(codNegocio);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
    }
}
