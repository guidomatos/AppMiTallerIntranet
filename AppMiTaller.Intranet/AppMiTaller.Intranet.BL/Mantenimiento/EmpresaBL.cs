using System;
using AppMiTaller.Intranet.DA.Mantenimiento;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.BL
{
    public class EmpresaBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public EmpresaBEList GetListaEmpresa()
        {
            EmpresaDA oEmpresaDA = new EmpresaDA();
            try
            {
                return oEmpresaDA.GetListaEmpresa();
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;            
        }
    }
}