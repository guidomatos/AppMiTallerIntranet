using System.Collections.Generic;
using AppMiTaller.Intranet.BE;
using System.Data;

namespace AppMiTaller.Intranet.DA
{
    public class TipoServicioMarcaDA : SqlRepository
    {
        public TipoServicioMarcaDA()
            : base()
        {

        }

        public IList<TipoServicioMarcaBE> GetAllMaestroTipoServicioMarca(int nidTipoServicio, string orderby, string orderbydirection)
        {
            var oMaestroTipoServicioMarcaBeList = new List<TipoServicioMarcaBE>();
            command = GetCommand("sgsnet_sps_mae_tipo_servicio_marca");
            command.Parameters.Add("@nid_tipo_servicio", SqlDbType.Int).Value = nidTipoServicio;
            command.Parameters.Add("@orderby", SqlDbType.VarChar, 50).Value = orderby;
            command.Parameters.Add("@orderbydirection", SqlDbType.Char, 1).Value = orderbydirection;
            Open();
            var reader = command.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                var entity = GetEntity<TipoServicioMarcaBE>(reader);
                entity.nid_tabla = i;
                i++;
                oMaestroTipoServicioMarcaBeList.Add(entity);
            }
            Close();
            return oMaestroTipoServicioMarcaBeList;
        }
        public void AddMaestroTipoServicioMarca(TipoServicioMarcaBE oMaestroTipoServicioMarcaBE)
        {
            command = GetCommand("sgsnet_spi_mae_tipo_servicio_marca");
            command.Parameters.Add("@nid_tipo_servicio", SqlDbType.Int).Value = oMaestroTipoServicioMarcaBE.nid_tipo_servicio;
            command.Parameters.Add("@nid_marca", SqlDbType.Int).Value = oMaestroTipoServicioMarcaBE.nid_marca;
            command.Parameters.Add("@fl_visible", SqlDbType.Char, 1).Value = oMaestroTipoServicioMarcaBE.fl_visible;
            command.Parameters.Add("@tx_informativo", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.tx_informativo;
            command.Parameters.Add("@co_usuario_crea", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.co_usuario_crea;
            command.Parameters.Add("@co_usuario_red", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.co_usuario_red;
            command.Parameters.Add("@no_estacion_red", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.no_estacion_red;
            Open();
            ExecuteNonQuery();
            Close();
        }

        public TipoServicioMarcaBE GetOneMaestroTipoServicioMarca(int nid_tipo_servicio_marca)
        {
            TipoServicioMarcaBE oMaestroTipoServicioMarcaBe = null;
            command = GetCommand("sgsnet_sps_mae_tipo_servicio_marca_by_id");
            command.Parameters.Add("@nid_tipo_servicio_marca", SqlDbType.Int).Value = nid_tipo_servicio_marca;
            Open();
            var reader = command.ExecuteReader();
            //int i = 1;
            while (reader.Read())
            {
                oMaestroTipoServicioMarcaBe = GetEntity<TipoServicioMarcaBE>(reader);
            }
            Close();
            return oMaestroTipoServicioMarcaBe;
        }

        public TipoServicioMarcaBE GetOneMaestroTipoServicioByMarca(int nid_tipo_servicio, int nid_marca)
        {
            TipoServicioMarcaBE oMaestroTipoServicioMarcaBe = null;
            command = GetCommand("sgsnet_sps_mae_tipo_servicio_marca_by_marca");
            command.Parameters.Add("@nid_tipo_servicio", SqlDbType.Int).Value = nid_tipo_servicio;
            command.Parameters.Add("@nid_marca", SqlDbType.Int).Value = nid_marca;
            Open();
            var reader = command.ExecuteReader();
            //int i = 1;
            while (reader.Read())
            {
                oMaestroTipoServicioMarcaBe = GetEntity<TipoServicioMarcaBE>(reader);
            }
            Close();
            return oMaestroTipoServicioMarcaBe;
        }



        public void UpdateMaestroTipoServicioMarca(TipoServicioMarcaBE oMaestroTipoServicioMarcaBE)
        {
            command = GetCommand("sgsnet_spu_mae_tipo_servicio_marca");
            command.Parameters.Add("@nid_tipo_servicio_marca", SqlDbType.Int).Value = oMaestroTipoServicioMarcaBE.nid_tipo_servicio_marca;
            command.Parameters.Add("@nid_tipo_servicio", SqlDbType.Int).Value = oMaestroTipoServicioMarcaBE.nid_tipo_servicio;
            command.Parameters.Add("@nid_marca", SqlDbType.Int).Value = oMaestroTipoServicioMarcaBE.nid_marca;
            command.Parameters.Add("@fl_visible", SqlDbType.Char, 1).Value = oMaestroTipoServicioMarcaBE.fl_visible;
            command.Parameters.Add("@tx_informativo", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.tx_informativo;
            command.Parameters.Add("@co_usuario_modi", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.co_usuario_crea;
            command.Parameters.Add("@co_usuario_red", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.co_usuario_red;
            command.Parameters.Add("@no_estacion_red", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.no_estacion_red;

            Open();
            ExecuteNonQuery();
            Close();
        }

        public void UpdateMaestroTipoServicioByMarca(TipoServicioMarcaBE oMaestroTipoServicioMarcaBE)
        {
            command = GetCommand("sgsnet_spu_mae_tipo_servicio_marca_fl_activo");
            command.Parameters.Add("@TipoServicioXml", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.TipoServicioXml;
            command.Parameters.Add("@co_usuario_modi", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.co_usuario_modi;
            command.Parameters.Add("@co_usuario_red", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.co_usuario_red;
            command.Parameters.Add("@no_estacion_red", SqlDbType.VarChar).Value = oMaestroTipoServicioMarcaBE.no_estacion_red;

            Open();
            ExecuteNonQuery();
            Close();
        }
    }
}