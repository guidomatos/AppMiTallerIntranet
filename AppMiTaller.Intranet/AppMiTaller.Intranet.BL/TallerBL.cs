using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;
using System.Linq;

namespace AppMiTaller.Intranet.BL
{
    public class TallerBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;


        public String ExisteCodigo(TallerBE ent)
        {
            return new TallerDA().ExisteCodigo(ent);
        }

        public TallerBEList GETListarTalleres(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETListarTalleres(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }

        public TallerBEList GETListarUbigeo(int Nid_usuario)
        {
            return new TallerDA().GETListarUbigeo(Nid_usuario);
        }

        public CombosBEList GETListarDepartamento(int Nid_usuario)
        {
            return new TallerDA().GETListarDepartamento(Nid_usuario);
        }


        public TallerBEList GETListarUbicacion(TallerBE ent)
        {
            return new TallerDA().GETListarUbicacion(ent);
        }

        public CombosBEList GETListarProvincia(TallerBE ent, int Nid_usuario)
        {
            return new TallerDA().GETListarProvincia(ent, Nid_usuario);
        }

        public CombosBEList GETListarDistrito(TallerBE ent, int Nid_usuario)
        {
            return new TallerDA().GETListarDistrito(ent, Nid_usuario);
        }

        public CombosBEList GETListarPuntoRed(TallerBE ent, int Nid_usuario)
        {
            return new TallerDA().GETListarPuntoRed(ent, Nid_usuario);
        }

        //HORARIOS

        public TallerBEList GETListarDiasDisp()
        {
            return new TallerDA().GETListarDiasDisp();
        }

        public TallerBEList GETListarIntervalosAtencion()
        {
            return new TallerDA().GETListarIntervalosAtencion();
        }

        public TallerBEList GETListarFeriados()
        {
            return new TallerDA().GETListarFeriados();
        }
        //SERVICIOS

        public TallerBEList GETListarServicios()
        {
            return new TallerDA().GETListarServicios();
        }

        //MARCAS Y MODElOS

        public TallerBEList GETListarMarcasModelos(TallerBE ent)
        {
            return new TallerDA().GETListarMarcaModelo(ent);
        }
        public TallerBEList GETListarCapacidadTallerModelo(TallerBE ent)
        {
            return new TallerDA().GETListarCapacidadTallerModelo(ent);
        }

        //PARA UPDATE

        public TallerBEList GETDetalleTaller(TallerBE ent)
        {
            return new TallerDA().GETDetalleTaller(ent);
        }

        public TallerBEList GETDetallePorPuntoRed(TallerBE ent)
        {
            return new TallerDA().GETDetallePorPuntoRed(ent);
        }

        public TallerBEList GETDiasExcepPorTaller(TallerBE ent)
        {
            return new TallerDA().GETDiasExcepPorTaller(ent);
        }

        public TallerBEList GETServiciosPorTaller(TallerBE ent)
        {
            return new TallerDA().GETServiciosPorTaller(ent);
        }

        public TallerBEList GETModelosPorTaller(TallerBE ent)
        {
            return new TallerDA().GETModelosPorTaller(ent);
        }

        public TallerBEList GETDiasHabiles_Hora_PorTaller(TallerBE ent)
        {
            return new TallerDA().GETDiasHabiles_Hora_PorTaller(ent);
        }

        //--------------

        public Int32 InsertTaller(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTaller(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerHorario(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerHorario(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerDiaExceptuado(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerDiaExceptuado(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerServicio(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerServicio(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertTallerModelo(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerModelo(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 InsertarTallerModeloCapacidad(TallerBE ent)
        {
            try
            {
                return new TallerDA().GETInsertarTallerModeloCapacidad(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarTaller(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarTaller(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarHorario(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarHorario(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarDiasExcep(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarDiasExcep(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarTallerServicios(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarTallerServicios(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 ActualizarTallerModelos(TallerBE ent)
        {
            try
            {
                return new TallerDA().ActualizarTallerModelos(ent);
            }
            catch
            {
                return 0;
            }
        }


        public Int32 MantenimientoTallerServicios(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerServicios(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoTallerModelos(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerModelos(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoTallerModelosCapacidad(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerModelosCapacidad(ent);
            }
            catch
            {
                return 0;
            }
        }


        public Int32 MantenimientoTallerDiasExceptuados(TallerBE ent)
        {
            try
            {
                return new TallerDA().MantenimientoTallerDiasExceptuados(ent);
            }
            catch
            {
                return 0;
            }
        }

        public TallerBEList ListarDias_Taller(TallerBE ent)
        {
            return new TallerDA().ListarDias_Taller(ent);
        }
        public TallerBEList ListarHorario_Taller(TallerBE ent)
        {
            return new TallerDA().ListarHorario_Taller(ent);
        }

        #region "AGREGADO POR VICTOR GALARZA"

        public TallerHorariosExcepcionalBEList GetListHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListHorarioExcepcional(ent);
        }

        public TallerHorariosExcepcionalBEList GetListDiasXTllrHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListDiasXTllrHorarioExcepcional(ent);
        }

        public TallerHorariosExcepcionalBEList GetListHorXDiaXTllrHorarioExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListHorXDiaXTllrHorarioExcepcional(ent);
        }

        public Int32 InsertarCabeHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().InsertarCabeHorExcepcional(ent);
        }

        public Int32 ActualizarCabeHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().ActualizarCabeHorExcepcional(ent);
        }

        public Int32 InsertarDetaHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().InsertarDetaHorExcepcional(ent);
        }

        public Int32 EliminarDetaHorExcepcional(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().EliminarDetaHorExcepcional(ent);
        }

        public TallerHorariosExcepcionalBEList GetListHorarioExcepcionalXHorario(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListHorarioExcepcionalXHorario(ent);
        }

        public TallerHorariosExcepcionalBEList GetListDetaHorarioExcepcionalXHorario(TallerHorariosExcepcionalBE ent)
        {
            return new TallerHorariosExcepcionalDA().GetListDetaHorarioExcepcionalXHorario(ent);
        }
        #endregion


        // V 2.0

        public Int32 InhabilitarCapacidadAtencion_Taller(TallerHorariosBE ent)
        {
            return new TallerHorariosDA().InhabilitarCapacidadAtencion_Taller(ent);
        }

        public Int32 MantenimientoCapacidadAtencion_Taller(TallerHorariosBE ent)
        {
            return new TallerHorariosDA().MantenimientoCapacidadAtencion_Taller(ent);
        }

        public TallerHorariosBEList GETListarCapacidadAtencion_PorTaller(TallerHorariosBE ent)
        {
            return new TallerHorariosDA().GETListarCapacidadAtencion_PorTaller(ent);

        }
    }
}