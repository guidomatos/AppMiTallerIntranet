using System;
using System.Collections.Generic;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class ServicioDA : SqlRepository
    {
        public ServicioDA()
            : base()
        {

        }

        public ServicioBEList BusqServicioList(ServicioBE ent)
        {
            ServicioBEList lista = new ServicioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPS_SERVICIO_BY_PARAM_BO";
                    db.AddParameter("@vi_co_servicio", DbType.String, ParameterDirection.Input, ent.Co_Servicio);
                    db.AddParameter("@vi_no_servicio", DbType.String, ParameterDirection.Input, ent.No_Servicio);
                    db.AddParameter("@vi_nid_TipoServicio", DbType.Int32, ParameterDirection.Input, ent.Id_TipoServicio);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ServicioBE oEnt = CrearEntidad(DReader);
                    lista.Add(oEnt);
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
        public Int32 InsertServicio(ServicioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPI_SERVICIO_BO";

                    db.AddParameter("@vi_co_servicio", DbType.String, ParameterDirection.Input, ent.Co_Servicio);
                    db.AddParameter("@vi_no_servicio", DbType.String, ParameterDirection.Input, ent.No_Servicio);
                    db.AddParameter("@vi_nid_tipo_servicio", DbType.Int32, ParameterDirection.Input, ent.Id_TipoServicio);

                    if (ent.Qt_tiempo_prom == 0)
                        db.AddParameter("@vi_qt_tiempo_prom", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_qt_tiempo_prom", DbType.Int32, ParameterDirection.Input, ent.Qt_tiempo_prom);
                    db.AddParameter("@vi_fl_quick_service", DbType.String, ParameterDirection.Input, ent.Fl_quick_service);
                    db.AddParameter("@vi_no_dias_validos", DbType.String, ParameterDirection.Input, ent.no_dias_validos);
                    if (String.IsNullOrEmpty(ent.co_usuario_crea))
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_crea", DbType.String, ParameterDirection.Input, ent.co_usuario_crea);

                    if (String.IsNullOrEmpty(ent.no_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.no_usuario_red);

                    if (String.IsNullOrEmpty(ent.no_estacion_red))
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);

                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch
            {
                return res = 1;
                throw;
            }
            return res;
        }
        public Int32 UpdateServicio(ServicioBE ent)
        {
            Int32 res = 0;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "SRC_SPU_SERVICIO_BO";
                    db.AddParameter("@vi_nid_Servicio", DbType.String, ParameterDirection.Input, ent.Id_Servicio);
                    db.AddParameter("@vi_co_servicio", DbType.String, ParameterDirection.Input, ent.Co_Servicio);
                    db.AddParameter("@vi_no_servicio", DbType.String, ParameterDirection.Input, ent.No_Servicio);
                    db.AddParameter("@vi_nid_tipo_servicio", DbType.Int32, ParameterDirection.Input, ent.Id_TipoServicio);

                    if (ent.Qt_tiempo_prom == 0)
                        db.AddParameter("@qt_tiempo_prom", DbType.Int32, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@qt_tiempo_prom", DbType.Int32, ParameterDirection.Input, ent.Qt_tiempo_prom);

                    db.AddParameter("@fl_quick_service", DbType.String, ParameterDirection.Input, ent.Fl_quick_service);
                    db.AddParameter("@vi_no_dias_validos", DbType.String, ParameterDirection.Input, ent.no_dias_validos);

                    if (String.IsNullOrEmpty(ent.co_usuario_cambio))
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_modi", DbType.String, ParameterDirection.Input, ent.co_usuario_cambio);

                    if (String.IsNullOrEmpty(ent.no_usuario_red))
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, DBNull.Value);
                    else
                        db.AddParameter("@vi_co_usuario_red", DbType.String, ParameterDirection.Input, ent.no_usuario_red);


                    db.AddParameter("@vi_no_estacion_red", DbType.String, ParameterDirection.Input, ent.no_estacion_red);
                    db.AddParameter("@vi_fl_activo", DbType.String, ParameterDirection.Input, ent.Fl_activo);
                    res = Int32.Parse(db.ExecuteScalar().ToString());
                }
            }
            catch
            {
                throw;
                //return res = 1;
            }
            return res;
        }

        public ServicioBEList GETListarServiciosPorTipo(ServicioBE ent)
        {
            ServicioBEList lista = new ServicioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_LISTAR_SERVICIOS_POR_TIPO_BO]";
                    db.AddParameter("@vi_nid_tipo_servicio", DbType.String, ParameterDirection.Input, ent.Id_TipoServicio);
                    db.AddParameter("@vi_Nid_usuario", DbType.Int32, ParameterDirection.Input, ent.Nid_usuario);
                    db.AddParameter("@vi_nid_modelo", DbType.Int32, ParameterDirection.Input, ent.nid_modelo == 0 ? (object)DBNull.Value : ent.nid_modelo); // @0001 I/F

                    DReader = db.GetDataReader();
                }
                while (DReader.Read())
                {
                    ServicioBE oCitasBE = CrearEntidad3(DReader);
                    lista.Add(oCitasBE);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }
        public ServicioBEList GETListarDatosServicios(ServicioBE ent)
        {
            ServicioBEList lista = new ServicioBEList();
            IDataReader DReader = null;
            try
            {
                using (Database db = new Database())
                {
                    db.ProcedureName = "[SRC_SPS_LISTAR_DATOS_SERVICIOS_BO]";
                    db.AddParameter("@VI_NID_SERVICIO", DbType.String, ParameterDirection.Input, ent.Id_Servicio);
                    DReader = db.GetDataReader();

                }
                while (DReader.Read())
                {
                    ServicioBE oCitasBE = CrearEntidad4(DReader);
                    lista.Add(oCitasBE);
                }

                DReader.Close();
            }
            catch (Exception)
            {
                if (DReader != null && !DReader.IsClosed) DReader.Close();
                throw;
            }
            return lista;
        }
        private ServicioBE CrearEntidad(IDataRecord DReader)
        {
            ServicioBE Entidad = new ServicioBE();
            int indice;

            indice = DReader.GetOrdinal("nid_Servicio");
            Entidad.Id_Servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("co_servicio");
            Entidad.Co_Servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_servicio");
            Entidad.No_Servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("nid_tipo_servicio");
            Entidad.Id_TipoServicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("no_tipo_servicio");
            Entidad.No_tipo_servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("qt_tiempo_prom");
            Entidad.Qt_tiempo_prom = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));
            indice = DReader.GetOrdinal("fl_quick_service");
            Entidad.Fl_quick_service = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("no_dias_validos");
            Entidad.no_dias_validos = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));
            indice = DReader.GetOrdinal("fl_activo");
            Entidad.Fl_activo = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        private ServicioBE CrearEntidad3(IDataRecord DReader)
        {
            ServicioBE Entidad = new ServicioBE();
            int indice;

            indice = DReader.GetOrdinal("NID_SERVICIO");
            Entidad.Id_Servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("NO_SERVICIO");
            Entidad.No_Servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }

        private ServicioBE CrearEntidad4(IDataRecord DReader)
        {
            ServicioBE Entidad = new ServicioBE();
            int indice;

            indice = DReader.GetOrdinal("NID_SERVICIO");
            Entidad.Id_Servicio = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("NO_SERVICIO");
            Entidad.No_Servicio = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("QT_TIEMPO_PROM");
            Entidad.Qt_tiempo_prom = (DReader.IsDBNull(indice) ? 0 : DReader.GetInt32(indice));

            indice = DReader.GetOrdinal("FL_QUICK_SERVICE");
            Entidad.Fl_quick_service = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            indice = DReader.GetOrdinal("NO_DIAS_VALIDOS");
            Entidad.no_dias_validos = (DReader.IsDBNull(indice) ? "" : DReader.GetString(indice));

            return Entidad;
        }
        public IList<ServicioBE> GetAllServicioBe(int nid_marca, int nid_modelo, string orderby, string orderbydirection)
        {
            var oServicioBeList = new List<ServicioBE>();
            command = GetCommand("sgsnet_getall_servicios");
            command.Parameters.Add("@nid_marca", SqlDbType.Int).Value = nid_marca == 0 ? (object)DBNull.Value : nid_marca;
            command.Parameters.Add("@nid_modelo", SqlDbType.Int).Value = nid_modelo == 0 ? (object)DBNull.Value : nid_modelo;
            command.Parameters.Add("@orderby", SqlDbType.VarChar, 50).Value = orderby;
            command.Parameters.Add("@orderbydirection", SqlDbType.Char, 1).Value = orderbydirection;
            Open();
            var reader = command.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                var entity = GetEntity<ServicioBE>(reader);
                entity.nid_tabla = i;
                i++;
                oServicioBeList.Add(entity);
            }
            Close();
            return oServicioBeList;
        }

        public IList<ServicioBE> GetAllServicioAsignadoBe(int nid_marca, int nid_modelo, string orderby, string orderbydirection)
        {
            var oServicioBeList = new List<ServicioBE>();
            command = GetCommand("sgsnet_getall_servicios_asignados");
            command.Parameters.Add("@nid_marca", SqlDbType.Int).Value = nid_marca;
            command.Parameters.Add("@nid_modelo", SqlDbType.Int).Value = nid_modelo;
            command.Parameters.Add("@orderby", SqlDbType.VarChar, 50).Value = orderby;
            command.Parameters.Add("@orderbydirection", SqlDbType.Char, 1).Value = orderbydirection;
            Open();
            var reader = command.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                var entity = GetEntity<ServicioBE>(reader);
                entity.nid_tabla = i;
                i++;
                oServicioBeList.Add(entity);
            }
            Close();
            return oServicioBeList;
        }

        public IList<ServicioBE> GetAllServicioModelo(int nid_marca, int nid_modelo, int pagina_actual, ref int cantidadregistros, string orderby, string orderbydirection)
        {
            var oServicioBeList = new List<ServicioBE>();
            command = GetCommand("sgsnet_getall_servicio_modelo");
            command.Parameters.Add("@nid_marca", SqlDbType.Int).Value = nid_marca == 0 ? (object)DBNull.Value : nid_marca;
            command.Parameters.Add("@nid_modelo", SqlDbType.Int).Value = nid_modelo == 0 ? (object)DBNull.Value : nid_modelo;
            command.Parameters.Add("@pagina_actual", SqlDbType.Int).Value = pagina_actual;
            command.Parameters.Add("@orderby", SqlDbType.VarChar, 50).Value = orderby;
            command.Parameters.Add("@orderbydirection", SqlDbType.Char, 1).Value = orderbydirection;
            command.Parameters.Add("@cantidad_registros", SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

            Open();
            var reader = ExecuteReader();


            while (reader.Read())
            {
                var entity = GetEntity<ServicioBE>(reader);
                oServicioBeList.Add(entity);
            }
            Close();
            cantidadregistros = Convert.ToInt32(command.Parameters["@cantidad_registros"].Value);
            return oServicioBeList;
        }

        public void AddServicioModelo(ServicioBE oServicioBe)
        {
            command = GetCommand("sgsnet_spi_mae_servicio_especifico_modelo");
            command.Parameters.Add("@xmlServicios", SqlDbType.Xml).Value = oServicioBe.XmlServicios;
            command.Parameters.Add("@co_usuario_crea", SqlDbType.VarChar, 20).Value = oServicioBe.co_usuario_crea;
            command.Parameters.Add("@no_estacion_red", SqlDbType.VarChar, 50).Value = oServicioBe.no_estacion_red;
            command.Parameters.Add("@no_usuario_red", SqlDbType.VarChar, 50).Value = oServicioBe.no_usuario_red;
            Open();
            ExecuteNonQuery();
            Close();
        }
        public void UpdateServicioModelo(ServicioBE oServicioBe)
        {
            command = GetCommand("sgsnet_spu_mae_servicio_especifico_modelo");
            command.Parameters.Add("@xmlServicios", SqlDbType.Xml).Value = oServicioBe.XmlServicios;
            command.Parameters.Add("@co_usuario_cambio", SqlDbType.VarChar, 20).Value = oServicioBe.co_usuario_cambio;
            command.Parameters.Add("@no_estacion_red", SqlDbType.VarChar, 50).Value = oServicioBe.no_estacion_red;
            command.Parameters.Add("@no_usuario_red", SqlDbType.VarChar, 50).Value = oServicioBe.no_usuario_red;
            Open();
            ExecuteNonQuery();
            Close();
        }
    }
}