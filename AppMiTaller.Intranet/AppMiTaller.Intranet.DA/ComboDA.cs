using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using AppMiTaller.Intranet.BE;

namespace AppMiTaller.Intranet.DA
{
    public class ComboDA : SqlRepository
    {
        public ComboDA() : base()
        {
        
        }

        public IList<ComboBE> GetAllComboBe(int nid_tabla) 
        {
            var oComboBeList = new List<ComboBE>();
            command = GetCommand("sgsnet_getall_combo");
            command.Parameters.Add("@nid_tabla",SqlDbType.Int).Value = nid_tabla;
            Open();
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                var entity = GetEntity<ComboBE>(reader);
                oComboBeList.Add(entity);
            }
            Close();
            return oComboBeList;
        }

        
       
    }
}
