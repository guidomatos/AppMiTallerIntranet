using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class ReporteBL
    {

        public CitasBEList ListarCitasAtendidasPorMarca(Int32 nid_usuario)
        {
            return new ReporteDA().ListarCitasAtendidasPorMarca(nid_usuario);
        }

        public CitasBEList ListarCitasAtendidasPorAsesor(Int32 nid_usuario)
        {
            return new ReporteDA().ListarCitasAtendidasPorAsesor(nid_usuario);
        }

    }
}
