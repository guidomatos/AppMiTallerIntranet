using System.Collections.Generic;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class TipoServicioMarcaBL
    {
        private readonly TipoServicioMarcaDA oMaestroTipoServicioMarcaDa = new TipoServicioMarcaDA();
        public IList<TipoServicioMarcaBE> GetAllMaestroTipoServicioMarca(int nidTipoServicio, string orderby, string orderbydirection)
        {
            return oMaestroTipoServicioMarcaDa.GetAllMaestroTipoServicioMarca(nidTipoServicio, orderby, orderbydirection);
        }
        public void AddMaestroTipoServicioMarca(TipoServicioMarcaBE oMaestroTipoServicioMarcaBE)
        {
            oMaestroTipoServicioMarcaDa.AddMaestroTipoServicioMarca(oMaestroTipoServicioMarcaBE);
        }

        public TipoServicioMarcaBE GetOneMaestroTipoServicioMarca(int nid_tipo_servicio_marca)
        {
            return oMaestroTipoServicioMarcaDa.GetOneMaestroTipoServicioMarca(nid_tipo_servicio_marca);
        }

        public TipoServicioMarcaBE GetOneMaestroTipoServicioByMarca(int nid_tipo_servicio, int nid_marca)
        {
            return oMaestroTipoServicioMarcaDa.GetOneMaestroTipoServicioByMarca(nid_tipo_servicio, nid_marca);
        }

        public void UpdateMaestroTipoServicioMarca(TipoServicioMarcaBE oMaestroTipoServicioMarcaBE)
        {
            oMaestroTipoServicioMarcaDa.UpdateMaestroTipoServicioMarca(oMaestroTipoServicioMarcaBE);
        }

        public void UpdateMaestroTipoServicioByMarca(TipoServicioMarcaBE oMaestroTipoServicioMarcaBE)
        {
            oMaestroTipoServicioMarcaDa.UpdateMaestroTipoServicioByMarca(oMaestroTipoServicioMarcaBE);
        }
    }
}