using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA.Mantenimiento;

namespace AppMiTaller.Intranet.BL
{
    public class TipoTablaDetalleBL : BaseBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public TipoTablaDetalleBEList ListarTipoTablaDetalle(String id_Tabla, String id_Tabla_Detalle, String valor1,
                                                                String valor2, String valor3, String valor4,
                                                                    String valor5)
        {
            TipoTablaDetalleDA oTipoTablaDetalleDA = new TipoTablaDetalleDA();
            try
            {
                return oTipoTablaDetalleDA.ListarTipoTablaDetalle(id_Tabla, id_Tabla_Detalle, valor1, valor2, valor3, valor4, valor5);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
        
        public TipoTablaDetalleBEList GetAll(Int32 idTablaPadre, String nomDescripcion, String codEstado)
        {
            TipoTablaDetalleDA oTipoTablaDetalleDA = new TipoTablaDetalleDA();
            try
            {
                return oTipoTablaDetalleDA.GetAll(idTablaPadre, nomDescripcion, codEstado);
            }
            catch (Exception ex)
            {
                ErrorEvent(this, ex);
            }
            return null;
        }
    }
}
