using System;
using AppMiTaller.Intranet.BE;
using AppMiTaller.Intranet.DA;
using System.Collections.Generic;

namespace AppMiTaller.Intranet.BL
{
    public class MaestroVehiculoBL
    {
        public delegate void ErrorDelegate(object sender, Exception ex);
        public event ErrorDelegate ErrorEvent;

        public VehiculoBEList GETListarVehiculos(VehiculoBE ent, Int32 Nid_usuario)
        {
            try
            {
                return new VehiculoDA().GETListarVehiculos(ent, Nid_usuario);
            }
            catch (Exception ex)
            {
                this.ErrorEvent(this, ex);
                return null;
            }
        }

        public CombosBEList GETListarModelosXMarca(VehiculoBE ent, int Nid_usuario)
        {
            return new VehiculoDA().GETListarModelosXMarca(ent, Nid_usuario);
        }

        public int GETVAL_EXIS_VEH(VehiculoBE ent)
        {
            return new VehiculoDA().GETVAL_EXIS_VEH(ent);
        }

        public int GETInserActuVehiculo(VehiculoBE ent)
        {
            return new VehiculoDA().GETInserActuVehiculo(ent);
        }

        public string GETVIN_X_PLACA(VehiculoBE ent)
        {
            return new VehiculoDA().GETVIN_X_PLACA(ent);
        }

        public VehiculoBE PROP_CLIE_CONT_X_PLACA(VehiculoBE ent)
        {
            return new VehiculoDA().PROP_CLIE_CONT_X_PLACA(ent);
        }

        public VehiculoBE PROP_CLIE_CONT_X_NRO_VIN(VehiculoBE ent)
        {
            return new VehiculoDA().PROP_CLIE_CONT_X_NRO_VIN(ent);
        }

        public VehiculoBE ListarDatosClientesPorIDVehiculo(VehiculoBE ent)
        {
            return new VehiculoDA().ListarDatosClientesPorIDVehiculo(ent);
        }

        public CombosBEList GETListarTipoPersona()
        {
            return new VehiculoDA().GETListarTipoPersona();
        }

        public VehiculoBEList GETListarBuscarCliente(VehiculoBE ent)
        {
            return new VehiculoDA().GETListarBuscarCliente(ent);
        }
    }
}