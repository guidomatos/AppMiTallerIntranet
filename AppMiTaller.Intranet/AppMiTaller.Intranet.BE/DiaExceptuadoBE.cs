using System;
using System.Collections.Generic;
using System.Text;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class DiaExceptuadoBE
    {
        private int _nid_dia_exceptuado = 0;

        public int Nid_dia_exceptuado
        {
            get { return _nid_dia_exceptuado; }
            set { _nid_dia_exceptuado = value; }
        }
        private int _nid_propietario = 0;

        public int Nid_propietario
        {
            get { return _nid_propietario; }
            set { _nid_propietario = value; }
        }
        private DateTime _fe_exceptuada;

        public DateTime Fe_exceptuada
        {
            get { return _fe_exceptuada; }
            set { _fe_exceptuada = value; }
        }

        public DiaExceptuadoBE()
        { 
        
        }
    }
   
}
