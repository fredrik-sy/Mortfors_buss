using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Mortfors_buss.Lib
{
    public class PreparedStatement : IDisposable
    {
        private readonly NpgsqlCommand command;

        public PreparedStatement(NpgsqlConnection connection)
        {
            command = new NpgsqlCommand
            {
                Connection = connection
            };
        }

        public string CommandText
        {
            get { return command.CommandText; }
            set { command.CommandText = value; }
        }

        public NpgsqlTransaction Transaction
        {
            get { return command.Transaction; }
            set { command.Transaction = value; }
        }
        
        public void AddParameter(string parameterName, object parameterValue, NpgsqlDbType parameterType)
        {
            NpgsqlParameter parameter = new NpgsqlParameter(parameterName, parameterType)
            {
                Value = parameterValue
            };

            command.Parameters.Add(parameter);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Execute()
        {
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public DataSet GetDataSet()
        {
            DataSet dataSet = new DataSet();
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                command?.Dispose();
            }
        }
    }
}