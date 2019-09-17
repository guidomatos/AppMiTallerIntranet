using System;
using System.Collections.Generic;
using System.Text;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class NegocioBE : BaseBE
    {
        #region Atributos
        private String cod_negocio;
        private String nom_negocio;
        #endregion
        #region propiedades negocio

        public String Nom_negocio
        {
            get { return nom_negocio; }
            set { nom_negocio = value; }
        }
        public String Cod_negocio
        {
            get { return cod_negocio; }
            set { cod_negocio = value; }
        }
        #endregion
    }
    [Serializable]
    public class NegocioBEList : List<NegocioBE> { }

}
