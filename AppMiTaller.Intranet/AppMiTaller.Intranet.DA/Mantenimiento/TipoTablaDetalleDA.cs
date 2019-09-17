using System;
using AppMiTaller.Intranet.BE;
using System.Data;

namespace AppMiTaller.Intranet.DA.Mantenimiento
{
    public class TipoTablaDetalleDA
    {
        public TipoTablaDetalleBEList ListarTipoTablaDetalle(String id_Tabla, String id_Tabla_Detalle, String valor1,String valor2, String valor3, String valor4,String valor5)
        {
            TipoTablaDetalleBEList ListarTipoTablaDetalle = new TipoTablaDetalleBEList();
            IDataReader DReader = null;
            Int32 idTablaDetalle;
            Int32 idTabla;

            if (id_Tabla.Equals(String.Empty))
                idTabla = -1;
            else idTabla = Int32.Parse(id_Tabla);

            if (id_Tabla_Detalle.Equals(String.Empty))
                idTablaDetalle = 0;
            else idTablaDetalle = Int32.Parse(id_Tabla_Detalle);

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_listado_tipo_tabla_detalle";
                    db.AddParameter("@vi_in_id_tabla", DbType.Int32, ParameterDirection.Input, idTabla);
                    db.AddParameter("@vi_in_id_tabla_detalle", DbType.Int32, ParameterDirection.Input, idTablaDetalle);
                    db.AddParameter("@vi_va_valor1", DbType.String, ParameterDirection.Input, valor1);
                    db.AddParameter("@vi_va_valor2", DbType.String, ParameterDirection.Input, valor2);
                    db.AddParameter("@vi_va_valor3", DbType.String, ParameterDirection.Input, valor3);
                    db.AddParameter("@vi_va_valor4", DbType.String, ParameterDirection.Input, valor4);
                    db.AddParameter("@vi_va_valor5", DbType.String, ParameterDirection.Input, valor5);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    TipoTablaDetalleBE oTipoTablaDetalleBE = CrearEntidadTablaDetalle(DReader);
                    ListarTipoTablaDetalle.Add(oTipoTablaDetalleBE);
                }
                DReader.Close();
            }
            catch
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return ListarTipoTablaDetalle;
        }
        public TipoTablaDetalleBEList GetAll(Int32 idTablaPadre, String nomDescripcion, String codEstado)
        {
            TipoTablaDetalleBEList lista = new TipoTablaDetalleBEList();
            IDataReader DReader = null;

            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "sgsnet_sps_bandeja_tabla_detalle";

                    db.AddParameter("@vi_in_id_tabla_padre", DbType.Int32, ParameterDirection.Input, idTablaPadre);
                    db.AddParameter("@vi_va_nom_descripcion", DbType.String, ParameterDirection.Input, nomDescripcion);
                    db.AddParameter("@vi_ch_cod_estado", DbType.String, ParameterDirection.Input, codEstado);

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    //Se añade todos los elementos del reader a la lista
                    TipoTablaDetalleBE oTipoTablaDetalleBE = CrearEntidad(DReader);
                    lista.Add(oTipoTablaDetalleBE);
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
        private TipoTablaDetalleBE CrearEntidad(IDataReader DReader)
        {
            TipoTablaDetalleBE oTipoTablaDetalleBE = new TipoTablaDetalleBE();
            int indice;

            indice = DReader.GetOrdinal("id_secuencial");//
            oTipoTablaDetalleBE.Id_tabla_detalle = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("id_codigo_padre");//
            oTipoTablaDetalleBE.Id_tabla = DReader.GetInt32(indice);
            indice = DReader.GetOrdinal("descripcion");//
            oTipoTablaDetalleBE.Valor1 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("valor3");//
            oTipoTablaDetalleBE.Valor3 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("valor4");//
            oTipoTablaDetalleBE.Valor4 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("valor5");//
            oTipoTablaDetalleBE.Valor5 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_estado");//
            oTipoTablaDetalleBE.Fl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);
            indice = DReader.GetOrdinal("estado");//
            oTipoTablaDetalleBE.Sfl_inactivo = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("valor2");//
            oTipoTablaDetalleBE.Valor2 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oTipoTablaDetalleBE;
        }
        private TipoTablaDetalleBE CrearEntidadTablaDetalle(IDataReader DReader)
        {
            TipoTablaDetalleBE oTipoTablaDetalleBE = new TipoTablaDetalleBE();
            int indice;

            indice = DReader.GetOrdinal("id_tabla_detalle");
            if (!DReader.IsDBNull(indice)) oTipoTablaDetalleBE.Id_tabla_detalle = DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("id_tabla");
            if (!DReader.IsDBNull(indice)) oTipoTablaDetalleBE.Id_tabla = DReader.GetInt32(indice);

            indice = DReader.GetOrdinal("valor1");
            oTipoTablaDetalleBE.Valor1 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("valor2");
            oTipoTablaDetalleBE.Valor2 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("valor3");
            oTipoTablaDetalleBE.Valor3 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("valor4");
            oTipoTablaDetalleBE.Valor4 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("valor5");
            oTipoTablaDetalleBE.Valor5 = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("cod_estado");
            oTipoTablaDetalleBE.Cod_estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            indice = DReader.GetOrdinal("nom_estado");
            oTipoTablaDetalleBE.Dsc_estado = DReader.IsDBNull(indice) ? String.Empty : DReader.GetString(indice);

            return oTipoTablaDetalleBE;

        }
    }
}