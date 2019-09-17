using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;

namespace AppMiTaller.Intranet.BL
{
    public class ClienteBL
    {

        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public static ClienteBE ListarDatosClienteDireccion(int nid_cliente)
        {
            return ClienteDA.ListarDatosClienteDireccion(nid_cliente);
        }
        public static string GuardarDireccionTaller(ClienteBE oMestroClienteBE)
        {
            return ClienteDA.GuardarDireccionTaller(oMestroClienteBE);
        }

        public ClienteBE ListarDatosCliente(int nid_cliente)
        {
            return new ClienteDA().ListarDatosCliente(nid_cliente);
        }

        public ClienteBEList GETListarClientes(ClienteBE ent)
        {
            try
            {
                return new ClienteDA().GETListarClientes(ent);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }
        public CombosBEList GETListarTipoDocumento(string oPersona)
        {
            return new ClienteDA().GETListarTipoDocumento(oPersona);
        }

        public int GETInserActuCliente(ClienteBE ent)
        {
            return new ClienteDA().GETInserActuCliente(ent);
        }

        public int SRC_SPS_VAL_CLIENTE_X_DOC(ClienteBE ent)
        {
            return new ClienteDA().SRC_SPS_VAL_CLIENTE_X_DOC(ent);
        }
    }
}