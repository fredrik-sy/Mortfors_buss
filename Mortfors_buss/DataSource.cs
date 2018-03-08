using Npgsql;
using System;

public class DataSource
{
    public const string Server = "pgserver.mah.se";
    public const int Port = 5432;
    public const string UserId = "ag3339";
    public const string Password = "3339ag";
    public const string Database = "ag3339";

    private NpgsqlConnection connection;

    public DataSource()
    {
    }

    public void Close()
    {
        connection.Close();
    }
    
    public bool Open()
    {
        try
        {
            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                                    Server, Port, UserId, Password, Database);

            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}