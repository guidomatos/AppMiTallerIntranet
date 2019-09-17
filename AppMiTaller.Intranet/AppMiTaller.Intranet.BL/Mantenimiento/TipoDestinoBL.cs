using System;
using System.Collections.Generic;
using System.Text;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class TipoDestinoBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public TipoDestinoBEList ListarDestino(String tipUbica)
        {
            try
            {
                TipoDestinoDA oTipoDestinoDA = new TipoDestinoDA();
                return oTipoDestinoDA.ListarDestino(tipUbica);
            }
            catch (Exception ex) 
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }

        public TipoDestinoBEList ListarDestinoCanalVin(String tipUbica, Int32 nidVin)
        {
            TipoDestinoDA oTipoDestinoDA = new TipoDestinoDA();
            return oTipoDestinoDA.ListarDestinoCanalVin(tipUbica, nidVin);
        }
        //I - FPINCO 14/09/2010
        public TipoDestinoBEList ListarDestinoUsuario(String pcTipoUbica, Int64 pnUsuario)
        {
            TipoDestinoDA oTipoDestinoDA = new TipoDestinoDA();
            return oTipoDestinoDA.ListarDestinoUsuario(pcTipoUbica, pnUsuario);
        }
        //F - FPINCO
    }
}
