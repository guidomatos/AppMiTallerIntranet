using System;
using System.Collections.Generic;
using System.Text;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class UbigeoBE
    {
        #region "Atributos"        
        private String _coddpto;
        private String _codprov;
        private String _coddist;
        private String _nombre;        
        #endregion

        #region "Propiedades"        
        public String coddpto
        {
            get { return _coddpto; }
            set { _coddpto = value; }
        }
        public String codprov
        {
            get { return _codprov; }
            set { _codprov = value; }
        }
        public String coddist
        {
            get { return _coddist; }
            set { _coddist = value; }
        }
        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }        
        #endregion

        #region "Metodos"
        public UbigeoBE()
        {
        }
        #endregion
    }

    [Serializable]
    public class UbigeoBEList : List<UbigeoBE>
    {

    }
}
