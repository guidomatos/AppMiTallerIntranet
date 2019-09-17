using System;
using System.Collections.Generic;
using System.Text;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class UbigeoBL : BaseBL
    {
        public UbigeoBEList GetListaDepartamento()
        {
            UbigeoDA oUbigeoDA = new UbigeoDA();
            return oUbigeoDA.GetListaDepartamento();
        }
        public UbigeoBEList GetListaProvincia(String idDepartamento)
        {
            UbigeoDA oUbigeoDA = new UbigeoDA();
            return oUbigeoDA.GetListaProvincia(idDepartamento);
        }
        public UbigeoBEList GetListaDistrito(String idDepartamento, String idProvincia)
        {
            UbigeoDA oUbigeoDA = new UbigeoDA();
            return oUbigeoDA.GetListaDistrito(idDepartamento, idProvincia);
        }
    }
}
