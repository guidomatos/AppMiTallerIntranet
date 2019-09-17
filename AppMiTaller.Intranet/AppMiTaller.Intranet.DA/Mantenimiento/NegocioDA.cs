using System;
using System.Collections.Generic;
using System.Text;
using AppMiTaller.Intranet.BE;
using System.Data;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class NegocioDA
    {
        public NegocioBEList ListarNegocio()
        {
            NegocioBEList ListaNegocio = new NegocioBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_negocio";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ListaNegocio.Add(CrearEntidadNegocio(DReader));
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return ListaNegocio;

        }
        private NegocioBE CrearEntidadNegocio(IDataReader DReader)
        {
            NegocioBE oNegocioBE = new NegocioBE();
            int indice;

            indice = DReader.GetOrdinal("cod_negocio");
            oNegocioBE.Cod_negocio = DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_negocio");
            oNegocioBE.Nom_negocio = DReader.GetString(indice);
            return oNegocioBE;
        }
    }
}
