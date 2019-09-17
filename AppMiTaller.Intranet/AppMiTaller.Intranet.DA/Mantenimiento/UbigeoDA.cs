using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class UbigeoDA
    {
        #region "Departamento"
        public UbigeoBEList GetListaDepartamento()
        {
            UbigeoBEList lista = new UbigeoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_departamento";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UbigeoBE oUbigeoBE = CrearEntidadDepartamento(DReader);
                    lista.Add(oUbigeoBE);
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
        private UbigeoBE CrearEntidadDepartamento(IDataReader DReader)
        {
            UbigeoBE oUbigeoBE = new UbigeoBE();
            int indice;
            //u.coddpto AS cod_departamento 
            indice = DReader.GetOrdinal("cod_departamento");
            oUbigeoBE.coddpto = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            //RTRIM(LTRIM(u.nombre)) AS departamento
            indice = DReader.GetOrdinal("departamento");
            oUbigeoBE.nombre = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oUbigeoBE;
        }
        #endregion

        #region "Provincia"
        public UbigeoBEList GetListaProvincia(String idDepartamento)
        {
            UbigeoBEList lista = new UbigeoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_provincia";
                    db.AddParameter("@vi_ch_cod_dpto", DbType.String, ParameterDirection.Input, idDepartamento);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UbigeoBE oUbigeoBE = CrearEntidadProvincia(DReader);
                    lista.Add(oUbigeoBE);
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
        private UbigeoBE CrearEntidadProvincia(IDataReader DReader)
        {
            UbigeoBE oUbigeoBE = new UbigeoBE();
            int indice;

            indice = DReader.GetOrdinal("codprovincia");
            oUbigeoBE.codprov = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("provincia");
            oUbigeoBE.nombre = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oUbigeoBE;
        }
        #endregion

        #region "Distrito"
        public UbigeoBEList GetListaDistrito(String idDepartamento, String idProvincia)
        {
            UbigeoBEList lista = new UbigeoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_distrito";
                    db.AddParameter("@vi_ch_cod_dpto", DbType.String, ParameterDirection.Input, idDepartamento);
                    db.AddParameter("@vi_ch_cod_provincia", DbType.String, ParameterDirection.Input, idProvincia);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UbigeoBE oUbigeoBE = CrearEntidadDistrito(DReader);
                    lista.Add(oUbigeoBE);
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
        private UbigeoBE CrearEntidadDistrito(IDataReader DReader)
        {
            UbigeoBE oUbigeoBE = new UbigeoBE();
            int indice;

            indice = DReader.GetOrdinal("cod_distrito");
            oUbigeoBE.coddist = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_distrito");
            oUbigeoBE.nombre = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oUbigeoBE;
        }
        #endregion

        #region "Lista Leyenda Ubigeo"
        public UbigeoBEList GetListaLeyendaUbigeo()
        {
            UbigeoBEList lista = new UbigeoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_leyenda_ubigeo";
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    UbigeoBE oUbigeoBE = CrearEntidadLeyendaUbigeo(DReader);
                    lista.Add(oUbigeoBE);
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
        private UbigeoBE CrearEntidadLeyendaUbigeo(IDataReader DReader)
        {
            UbigeoBE oUbigeoBE = new UbigeoBE();
            int indice;

            indice = DReader.GetOrdinal("cod_departamento");
            oUbigeoBE.coddpto = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_provincia");
            oUbigeoBE.codprov = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_distrito");
            oUbigeoBE.coddist = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("departamento");
            oUbigeoBE.nombre = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oUbigeoBE;
        }
        #endregion

    }
}
