using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AppMiTaller.Intranet.DA
{
    public class SqlRepository
    {
        protected  SqlConnection connection;
        protected SqlCommand command;

        public SqlRepository() 
        {
            var conecctionString = ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString;
            connection = new SqlConnection(conecctionString);
        }

        protected void Open() 
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        protected void Close()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        protected void ExecuteNonQuery()
        {
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                Close();
                throw ex;
            }
           
        }
        protected System.Data.SqlClient.SqlDataReader ExecuteReader()
        {
            SqlDataReader reader; 
            try
            {
                 reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
          
            return reader;
        }
        protected SqlCommand GetCommand(string storedProcedure)
        {
            command = new SqlCommand(storedProcedure,connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        protected TQ GetEntity<TQ>(IDataReader dataReader) where TQ : class, new()
        {
            var entity = new TQ();
            var fieldCount = dataReader.FieldCount;
            var properties = entity.GetType().GetProperties();
            var type = entity.GetType();
            for (var i = 0; i < fieldCount; i++)
            {
                foreach (var property in properties)
                {
                    if (!property.CanWrite)
                    {
                        continue;
                    }

                    if (property.Name != dataReader.GetName(i))
                    {
                        continue;
                    }

                    var value = dataReader.GetValue(i);
                    if (value is DBNull)
                    {
                        continue;
                    }

                    property.SetValue(entity, value, null);
                    break;
                }
            }

            return entity;
        }

    }
}
