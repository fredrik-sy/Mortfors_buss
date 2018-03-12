using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Mortfors_buss.Lib
{
    internal class Statement : IDisposable
    {
        private readonly NpgsqlCommand command;

        public Statement(NpgsqlConnection connection)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Execute()
        {
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
