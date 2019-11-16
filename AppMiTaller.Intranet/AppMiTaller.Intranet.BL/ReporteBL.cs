using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class ReporteBL
    {

        public CitasBEList ListarCitasAtendidasPorMarca()
        {
            return new ReporteDA().ListarCitasAtendidasPorMarca();
        }

        public CitasBEList ListarCitasAtendidasPorAsesor()
        {
            return new ReporteDA().ListarCitasAtendidasPorAsesor();
        }

    }
}
