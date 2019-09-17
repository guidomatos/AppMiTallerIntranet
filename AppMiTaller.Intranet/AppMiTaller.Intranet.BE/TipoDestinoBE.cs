using System;
using System.Collections.Generic;
using System.Text;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
   public class TipoDestinoBE:BaseBE
   {
       #region Atributos
       private String cod_tipo_ubicacion;
        private String nom_tipo_ubicacion;
       #endregion
        #region propiedades de destino



        public String Nom_tipo_ubicacion
        {
            get { return nom_tipo_ubicacion; }
            set { nom_tipo_ubicacion = value; }
        }

        public String Cod_tipo_ubicacion
        {
            get { return cod_tipo_ubicacion; }
            set { cod_tipo_ubicacion = value; }
        }

        #endregion

    }

    [Serializable]
    public class TipoDestinoBEList : List<TipoDestinoBE> { }
}
