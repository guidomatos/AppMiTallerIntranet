using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class UsuarioTallerBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public Int32 InsertarAsesorModulo(CitasBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarAsesorModulo(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return 0;
            }
        }
        public Int32 ActualizarAsesorModulo(CitasBE ent)
        {
            return new UsuarioTallerDA().ActualizarAsesorModulo(ent);
        }
        public UsuarioBEList GETListarUsuarios(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarUsuarios(ent);
        }

        public String ExisteLogin(UsuarioBE ent)
        {
            return new UsuarioTallerDA().ExisteLogin(ent);
        }

        public UsuarioBEList GETListarHorario_Por_Taller_Dia(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarHorario_Por_Taller_Dia(ent);
        }

        public UsuarioBEList GETListarDetalleUsuarioPorCodigo(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarDetalleUsuarioPorCodigo(ent);
        }

        public UsuarioBEList GETListarPerfiles(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarPerfiles(ent);
        }

        public UsuarioBEList GETListarUbigeo(int Nid_usuario)
        {
            return new UsuarioTallerDA().GETListarUbigeo(Nid_usuario);
        }

        public UsuarioBEList GETListarTalleresDistrito(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarTalleresDistrito(ent);
        }

        // USUARIOS DETALLE

        // DATOS GENERALES

        public UsuarioBEList GETListarTipoDocumento()
        {
            return new UsuarioTallerDA().GETListarTipoDocumento();
        }

        public UsuarioBEList GETListarTipo()
        {
            return new UsuarioTallerDA().GETListarTipo();
        }

        public UsuarioBEList GETListarUbicacion(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarUbicacion(ent);
        }

        // PARAMETROS TIENDA

        public UsuarioBEList GETListarTipoPuntosRedPorDistrito()
        {
            return new UsuarioTallerDA().GETListarTipoPuntosRedPorDistrito();
        }

        public UsuarioBEList GETListarMarcaEmpresa(int Nid_usuario)
        {
            return new UsuarioTallerDA().GETListarMarcaEmpresa(Nid_usuario);
        }

        public UsuarioBEList GETListarLineaComercialMarca(int Nid_usuario)
        {
            return new UsuarioTallerDA().GETListarLineaComercialMarca(Nid_usuario);
        }

        public UsuarioBEList GETListarModelo_LineaMarca(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarModelo_LineaMarca(ent);
        }

        public UsuarioBEList GETListarModelos_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarModelos_PorUsuario(ent);
        }

        public UsuarioBEList GETListarPtoRed_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarPtoRed_PorUsuario(ent);
        }

        //PARAMETROS TALLER

        public UsuarioBEList GETListarPtoRedTaller_PorDistrito(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarPtoRedTaller_PorDistrito(ent);
        }

        public UsuarioBEList GETListarTalleres_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarTalleres_PorUsuario(ent);
        }

        public UsuarioBEList GETListarTaller_PorUsuario_AsesServ(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarTaller_PorUsuario_AsesServ(ent);
        }


        //PARAMETROS SERVICIOS HORARIO

        public UsuarioBEList GETListarDiasDisp()
        {
            return new UsuarioTallerDA().GETListarDiasDisp();
        }

        public UsuarioBEList GETListarFeriados()
        {
            return new UsuarioTallerDA().GETListarFeriados();
        }

        public UsuarioBEList GETListarHorario_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarHorario_PorUsuario(ent);
        }

        public UsuarioBEList GETListarDiasExcep_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarDiasExcep_PorUsuario(ent);
        }

        //PARAMETROS SERVICIOS SERV

        public UsuarioBEList GETListarTipoServ_Especifico()
        {
            return new UsuarioTallerDA().GETListarTipoServ_Especifico();
        }

        public UsuarioBEList GETListarServicios_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarServicios_PorUsuario(ent);
        }

        public Int32 InsertarUsuario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuario(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioPerfil(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioPerfil(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioHorario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioHorario(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioModelo(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioModelo(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioDiaExceptuado(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioDiaExceptuado(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioTaller(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioTaller(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioServicio(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioServicio(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 InsertarUsuarioUbicacion(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InsertarUsuarioUbicacion(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }


        //ACTUALIZAR

        public Int32 ActualizarUsuario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().ActualizarUsuario(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 ActualizarUsuarioPerfil(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().ActualizarUsuarioPerfil(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public Int32 MantenimientoUsuarioUbicacion(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoUsuarioUbicacion(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoUsuarioTaller(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoUsuarioTaller(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoUsuarioServicio(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoUsuarioServicio(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoUsuarioModelo(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoUsuarioModelo(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoUsuarioHorario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoUsuarioHorario(ent);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 MantenimientoDiasExceptuados_Usuario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoDiasExceptuados_Usuario(ent);
            }
            catch
            {
                return 0;
            }
        }

        // VERSION 2.0


        public Int32 InhabilitarCapacidadAtencion_Usuario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().InhabilitarCapacidadAtencion_Usuario(ent);
            }
            catch //(Exception ex)
            {
                return 0;
            }
        }

        public Int32 MantenimientoCapacidadAtencion_Usuario(UsuarioBE ent)
        {
            try
            {
                return new UsuarioTallerDA().MantenimientoCapacidadAtencion_Usuario(ent);
            }
            catch //(Exception ex)
            {
                //ErrorEvent(this, ex);
                return 0;
            }
        }

        public UsuarioBEList GETListarCapacidadAtencion_PorUsuario(UsuarioBE ent)
        {
            return new UsuarioTallerDA().GETListarCapacidadAtencion_PorUsuario(ent);
        }

    }
}