using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppMiTaller.Intranet.DA;
using AppMiTaller.Intranet.BE;


namespace AppMiTaller.Intranet.BL
{
    public class ComboBL
    {
        private readonly ComboDA oComboDa = new ComboDA();

        public IList<ComboBE> GetAllComboBe(int nid_padre,int nid_tabla) 
        {
            var oComboBeList =  oComboDa.GetAllComboBe(nid_tabla);
            if (nid_padre != 0) 
            {
                oComboBeList = oComboBeList.Where(x=>x.nid_padre == nid_padre).ToList();
            }
          
            return oComboBeList;
        }
    }
}
