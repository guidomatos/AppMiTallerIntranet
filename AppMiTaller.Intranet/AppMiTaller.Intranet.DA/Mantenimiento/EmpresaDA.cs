using System;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class EmpresaDA
    {
        public EmpresaBEList GetListaEmpresa()
        {
            EmpresaBEList lista = new EmpresaBEList();

            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_empresa";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    lista.Add(CrearEntidadLista(DReader));
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }
        
        private EmpresaBE CrearEntidadLista(IDataReader DReader)
        {
            EmpresaBE oEmpresaBE = new EmpresaBE();
            int indice;

            indice = DReader.GetOrdinal("id_empresa");
            oEmpresaBE.nid_empresa = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("nom_empresa");
            oEmpresaBE.no_empresa = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("no_empresa_corto");
            oEmpresaBE.prefijo_corto = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            return oEmpresaBE;
        }
    }
}