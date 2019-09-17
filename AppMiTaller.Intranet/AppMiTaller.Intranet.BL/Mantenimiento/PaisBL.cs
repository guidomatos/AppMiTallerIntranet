using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class PaisBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public PaisBEList GetAll(String nomPais, String codEstado)
        {
            PaisDA oPaisDA = new PaisDA();
            try
            {
                return oPaisDA.GetAll(nomPais, codEstado);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;            
        }
        
        public PaisBEList GetListaPaisTelefono()
        {
            PaisDA oPaisDA = new PaisDA();
            try
            {
                return oPaisDA.GetListaPaisTelefono();
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
                
    }
}