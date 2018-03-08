using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;

public class PreparedStatement : IDisposable
{
    private NpgsqlCommand command;

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

    public void AddParameter(string parameterName, string parameterValue, NpgsqlDbType parameterType)
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

    public DataSet Execute()
    {
        command.Prepare();
        command.ExecuteNonQuery();

        DataSet dataSet = new DataSet();
        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command);
        dataAdapter.Fill(dataSet);
        return dataSet;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (command != null)
            {
                command.Dispose();
            }
        }
    }
}