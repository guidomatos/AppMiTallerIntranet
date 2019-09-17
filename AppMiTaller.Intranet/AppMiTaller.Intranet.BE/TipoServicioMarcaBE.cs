using System;

namespace AppMiTaller.Intranet.BE
{
    [Serializable]
    public class TipoServicioMarcaBE
    {
        public int nid_tipo_servicio_marca { get; set; }
        public int nid_tipo_servicio { get; set; }
        public int nid_marca { get; set; }
        public int nid_tabla { get; set; }
        public string fl_visible { get; set; }
        public string tx_informativo { get; set; }
        public string co_usuario_crea { get; set; }
        public string co_usuario_modi { get; set; }
        public string co_usuario_red { get; set; }
        public string no_estacion_red { get; set; }
        public string TipoServicioXml { get; set; }
        public long nu_item { get; set; }
        public string no_marca { get; set; }
    }
}
