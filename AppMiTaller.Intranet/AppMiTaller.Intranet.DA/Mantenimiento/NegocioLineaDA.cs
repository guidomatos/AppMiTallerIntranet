using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class NegocioLineaDA
    {
        public NegocioLineaBEList GetListaLineaComercial(Int32 idNegocioLinea)
        {
            NegocioLineaBEList lista = new NegocioLineaBEList();

            IDataReader DReader;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_sps_listado_linea_comercial";

                db.AddParameter("@vi_in_nid_negocio_linea", DbType.Int32, ParameterDirection.Input, idNegocioLinea);

                DReader = db.GetDataReader();
            }
            while (DReader.Read())
            {
                NegocioLineaBE oNegocioLineaBE = CrearEntidadListaComercial(DReader);
                lista.Add(oNegocioLineaBE);
            }
            DReader.Close();

            return lista;
        }
        public NegocioLineaBEList GetListaLineaImportacion(String codNegocio)
        {
            NegocioLineaBEList lista = new NegocioLineaBEList();

            IDataReader DReader;

            using (Database db = new Database())
            {
                db.ProcedureName = "sgsnet_sps_listado_linea";
                db.AddParameter("@vi_va_cod_negocio", DbType.String, ParameterDirection.Input, codNegocio);
                DReader = db.GetDataReader();
            }
            while (DReader.Read())
            {
                lista.Add(CrearEntidadListaImportacion(DReader));
            }
            DReader.Close();

            return lista;
        }
        private NegocioLineaBE CrearEntidadListaComercial(IDataReader DReader)
        {
            NegocioLineaBE oNegocioLineaBE = new NegocioLineaBE();
            int indice;

            //sfam.nid_negocio_subfamilia as id_linea_subfamilia,	
            indice = DReader.GetOrdinal("id_linea_subfamilia");
            if (!DReader.IsDBNull(indice)) oNegocioLineaBE.nid_cod_tipo_linea = DReader.GetInt32(indice);
            //sfam.co_familia as cod_linea,	
            indice = DReader.GetOrdinal("cod_linea");
            oNegocioLineaBE.co_familia = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            //fam.no_familia as nom_linea
            indice = DReader.GetOrdinal("nom_linea");
            oNegocioLineaBE.Nom_linea = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oNegocioLineaBE;
        }
        private NegocioLineaBE CrearEntidadListaImportacion(IDataReader DReader)
        {
            NegocioLineaBE oNegocioLineaBE = new NegocioLineaBE();
            int indice;
            //nl.nid_negocio_linea as cod_linea
            indice = DReader.GetOrdinal("cod_linea");
            oNegocioLineaBE.nid_negocio_linea = DReader.GetInt32(indice);
            //,fv.no_familia as nom_linea
            indice = DReader.GetOrdinal("nom_linea");
            oNegocioLineaBE.Nom_linea = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oNegocioLineaBE;
        }

    }
}
