using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class ParametrosBackOffieBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public ParametrosBackOffieBEList ListarConfigTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            try
            {
                return new ParametrosBackOffieDA().ListarConfigTallerEmpresa(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }
        public ParametrosBackOffieBEList ListarDatosTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            return new ParametrosBackOffieDA().ListarDatosTallerEmpresa(ent);
        }
        public int InsertarConfigTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            return new ParametrosBackOffieDA().InsertarConfigTallerEmpresa(ent);
        }
        public int ActualizarConfigTallerEmpresa(ParametrosBackOfficeBE ent)
        {
            return new ParametrosBackOffieDA().ActualizarConfigTallerEmpresa(ent);
        }
        public string GETListarParametrosxCodigo(string valor)
        {
            ParametrosBackOffieDA objDAT_Parametros = new ParametrosBackOffieDA();
            return objDAT_Parametros.GETListarParametrosxCodigo(valor);
        }
        public ParametrosBackOffieBEList GETListarParametros()
        {
            return new ParametrosBackOffieDA().GETListarParametros();
        }
        public int GETActualizarParametro(ParametrosBackOfficeBE ent)
        {
            return new ParametrosBackOffieDA().GETActualizarParametro(ent);
        }
        public string GetFechaActual()
        {
            ParametrosBackOffieDA objDAT_Parametros = new ParametrosBackOffieDA();
            return objDAT_Parametros.GetFechaActual();
        }
        public ParametrosBackOfficeBE GetHorarioXDefecto()
        {
            return new ParametrosBackOffieDA().GetHorarioXDefecto();
        }
        public ParametrosBackOffieBEList GetHorasSRC()
        {
            return new ParametrosBackOffieDA().GetHorasSRC();
        }
    }
}