using System;
using System.Collections.Generic;
using System.Text;
using AppMiTaller.Intranet.BE;
using System.Data;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class TipoDestinoDA
    {
        #region Tipo Destino

        public TipoDestinoBEList ListarDestino(String tipUbica)
        {
            TipoDestinoBEList ListarDestino = new TipoDestinoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_tipo_destino";
                    db.AddParameter("@vi_va_tip_ubica", DbType.String, ParameterDirection.Input, tipUbica);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TipoDestinoBE oTipoDestinoBE = CrearEntidadDestino(DReader);
                    ListarDestino.Add(oTipoDestinoBE);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return ListarDestino;

        }

        private TipoDestinoBE CrearEntidadDestino(IDataReader DReader)
        {
            TipoDestinoBE oTipoDestinoBE = new TipoDestinoBE();
            int indice;

            indice = DReader.GetOrdinal("cod_tipo_ubicacion");
            oTipoDestinoBE.Cod_tipo_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_tipo_ubicacion");
            oTipoDestinoBE.Nom_tipo_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oTipoDestinoBE;

        }

        public TipoDestinoBEList ListarDestinoCanalVin(String tipUbica, Int32 nidVin)
        {
            TipoDestinoBEList ListarDestino = new TipoDestinoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_tipo_destino_guia_remision";
                    db.AddParameter("@vi_va_tip_ubica", DbType.String, ParameterDirection.Input, tipUbica);
                    db.AddParameter("@vi_in_nid_vin", DbType.String, ParameterDirection.Input, nidVin);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TipoDestinoBE oTipoDestinoBE = CrearEntidadDestinoCanalVin(DReader);
                    ListarDestino.Add(oTipoDestinoBE);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return ListarDestino;

        }
        //I - FPINCO 14/09/2010
        public TipoDestinoBEList ListarDestinoUsuario(String pcTipoUbica, Int64 pnUsuario)
        {
            TipoDestinoBEList ListarDestino = new TipoDestinoBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_tipo_destino_to_solicitar_despacho";
                    db.AddParameter("@vi_va_tip_ubica", DbType.String, ParameterDirection.Input, pcTipoUbica);
                    db.AddParameter("@vi_in_Nid_usuario", DbType.Int64, ParameterDirection.Input, pnUsuario);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TipoDestinoBE oTipoDestinoBE = CrearEntidadDestinoCanalVin(DReader);
                    ListarDestino.Add(oTipoDestinoBE);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return ListarDestino;

        }
        //F - FPINCO
        private TipoDestinoBE CrearEntidadDestinoCanalVin(IDataReader DReader)
        {
            TipoDestinoBE oTipoDestinoBE = new TipoDestinoBE();
            int indice;

            indice = DReader.GetOrdinal("cod_tipo_ubicacion");
            oTipoDestinoBE.Cod_tipo_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_tipo_ubicacion");
            oTipoDestinoBE.Nom_tipo_ubicacion = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oTipoDestinoBE;

        }
        #endregion
    }
}
