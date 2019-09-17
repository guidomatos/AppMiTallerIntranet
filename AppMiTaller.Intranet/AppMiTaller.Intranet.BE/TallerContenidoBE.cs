using System;
using System.Collections.Generic;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TallerContenidoBE
    {
        public int nid_taller_contenido { get; set; }
        public int nid_taller { get; set; }
        public int nid_negocio_taller { get; set; }
        public string co_tipo { get; set; }
        public string co_taller_contenido { get; set; }
        public string no_tipo { get; set; }
        public string no_titulo { get; set; }
        public string tx_contenido { get; set; }
        public string no_negocio_taller { get; set; }
        public string fe_inicio { get; set; }
        public string fe_fin { get; set; }
        public string fe_actualizacion { get; set; }
        public string tx_observacion { get; set; }
        public string co_estado { get; set; }
        public string no_estado { get; set; }
        public string co_fotos { get; set; }
        public string co_descrip { get; set; }
        public string co_masivo { get; set; }
        public string fl_tipo { get; set; }
        public string no_taller { get; set; }
        public string tx_promociones { get; set; }
        public string tx_noticias { get; set; }
        public string tx_datos { get; set; }
        public string tx_fotos { get; set; }
        public string co_usuario { get; set; }
        public string no_usuario_red { get; set; }
        public string no_estacion_red { get; set; }

    }
    [Serializable]
    public class TallerContenidoBEList : List<TallerContenidoBE>
    {
    }
}