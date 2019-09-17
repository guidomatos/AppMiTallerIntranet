using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace AppMiTaller.Intranet.DA
{
    internal class Database : IDisposable
    {
        DbConnection connection;
        DbProviderFactory factory;
        DbCommand command;

        public Database()
        {
            factory = DbProviderFactories.GetFactory(DataBaseHelper.GetDbProvider());
        }

        public void Open()
        {
            connection = factory.CreateConnection();
            connection.ConnectionString = DataBaseHelper.GetDbConnectionString();
            try
            {
                connection.Open();
            }
            catch
            {
                throw;
            }
        }

        public void Close()
        {
            connection.Close();
        }

        public string CommandText
        {
            set
            {
                if (command == null)
                {
                    command = factory.CreateCommand();

                }
                if (connection == null)
                {
                    this.Open();
                }
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = value;
            }
        }

        public string ProcedureName
        {
            set
            {
                if (command == null)
                {
                    command = factory.CreateCommand();
                }
                if (connection == null)
                {
                    this.Open();
                }
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = value;
            }
        }

        public object GetParameter(int index)
        {
            if (command != null)
            {
                try { return command.Parameters[index].Value; }
                catch { return null; }
            }
            return null;
        }

        public object GetParameter(string name)
        {
            if (command != null)
            {
                try { return command.Parameters[name].Value; }
                catch { return null; }
            }
            return null;
        }

        public void AddParameter(string parameterName, DbType parameterType, ParameterDirection parameterDirection, Object parameterValue)
        {
            if (command != null)
            {
                DbParameter parameter = factory.CreateParameter();
                parameter.ParameterName = parameterName;
                parameter.DbType = parameterType;
                parameter.Direction = parameterDirection;
                parameter.Value = parameterValue;
                parameter.SourceColumnNullMapping = true;
                if (parameterType == DbType.String && parameterDirection == ParameterDirection.Output) parameter.Size = 1000;
                command.Parameters.Add(parameter);
            }
        }

        public IDataReader GetDataReader()
        {
            return command.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);
        }

        public int Execute()
        {
            return command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return command.ExecuteScalar();
        }

        ~Database()
        {
            this.Dispose();
        }


        #region IDisposable Members

        public void Dispose()
        {
            /*if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();*/
            connection = null;
            command = null;
            factory = null;
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        #endregion
    }
}