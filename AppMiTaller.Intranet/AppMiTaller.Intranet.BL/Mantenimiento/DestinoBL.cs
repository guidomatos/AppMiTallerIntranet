using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class DestinoBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public Int32 Insertar(DestinoBE oDestinoBE)
        {
            DestinoDA oDestinoDA = new DestinoDA();
            try
            {
                return oDestinoDA.Insertar(oDestinoBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }

        public Int32 Modificar(DestinoBE oDestinoBE)
        {
            DestinoDA oDestinoDA = new DestinoDA();
            try
            {
                return oDestinoDA.Modificar(oDestinoBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }
        
        public Int32 Eliminar(DestinoBE oDestinoBE)
        {
            DestinoDA oDestinoDA = new DestinoDA();
            try
            {
                return oDestinoDA.Eliminar(oDestinoBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return 0;
        }
        public DestinoBEList Listar(DestinoBE oBE)
        {
            DestinoDA oDestinoDA = new DestinoDA();
            try
            {
                return oDestinoDA.Listar(oBE);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        public DestinoBE ListarById(Int32 id_Destino)
        {
            DestinoDA oDestinoDA = new DestinoDA();
            try
            {
                return oDestinoDA.ListarById(id_Destino);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
    }
}
