using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace nachos.io
{
    public class Client : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public Client(String connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public Boolean Upsert(String commandName, List<SqlParameter> parameters)
        {
            Boolean result = false;

            SqlCommand command = new SqlCommand(commandName, Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.ToArray());

            result = command.ExecuteNonQuery() > 0;

            return result;
        }

        public T FindOne<T>(String commandName, List<SqlParameter> parameters, Func<SqlDataReader, T> mapper)
        {
            T result = default(T);

            SqlCommand command = new SqlCommand(commandName, Connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.ToArray());

            SqlDataReader reader = command.ExecuteReader();
            
            if (reader.HasRows)
            {
                reader.Read();
                result = mapper(reader);
            }

            return result;
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                if (Connection.State != System.Data.ConnectionState.Closed)
                {
                    Connection.Close();
                }
                Connection.Dispose();
                Connection = null;
            }
        }
    }
}
