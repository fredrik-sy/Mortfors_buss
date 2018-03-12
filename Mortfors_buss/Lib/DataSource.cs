using System;
using System.Data;
using Npgsql;
using NpgsqlTypes;

namespace Mortfors_buss.Lib
{
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
            if (connection?.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public bool Open()
        {
            try
            {
                string connectionString = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
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

        public bool RegisterCustomer(string email, string name, string address, string phone)
        {
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                using (PreparedStatement preparedStatement = new PreparedStatement(connection))
                {
                    preparedStatement.Transaction = transaction;
                    preparedStatement.CommandText = "insert into customer values (@email, @name, @address)";
                    preparedStatement.AddParameter("email", email, NpgsqlDbType.Varchar);
                    preparedStatement.AddParameter("name", name, NpgsqlDbType.Varchar);
                    preparedStatement.AddParameter("address", address, NpgsqlDbType.Varchar);
                    preparedStatement.Execute();
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    using (PreparedStatement preparedStatement = new PreparedStatement(connection))
                    {
                        preparedStatement.Transaction = transaction;
                        preparedStatement.CommandText = "insert into phone values (@number, @email)";
                        preparedStatement.AddParameter("number", phone, NpgsqlDbType.Varchar);
                        preparedStatement.AddParameter("email", email, NpgsqlDbType.Varchar);
                        preparedStatement.Execute();
                    }
                }
            }
            catch
            {
                transaction.Rollback();
                return false;
            }

            transaction.Commit();
            return true;
        }

        public bool RegisterBookingSchedule(int weekNumber, string customerId, int busTripId, int numberOfSeats)
        {
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                using (PreparedStatement preparedStatement = new PreparedStatement(connection))
                {
                    preparedStatement.Transaction = transaction;
                    preparedStatement.CommandText =
                        "insert into bookingschedule(weeknumber, customer_id, bustrip_id, numberofseats) " +
                        "values (@weeknumber, @customer_id, @bustrip_id, @numberofseats)";
                    preparedStatement.AddParameter("weeknumber", weekNumber, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("customer_id", customerId, NpgsqlDbType.Varchar);
                    preparedStatement.AddParameter("bustrip_id", busTripId, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("numberofseats", numberOfSeats, NpgsqlDbType.Integer);
                    preparedStatement.Execute();
                }
            }
            catch
            {
                transaction.Rollback();
                return false;
            }

            transaction.Commit();
            return true;
        }

        public DataSet RetrieveBusTrip(int weekNumber)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText =
                    "select bustrip.*, departure.country as departurecountry, departure.street as departurestreet, arrival.country as arrivalcountry, arrival.street as arrivalstreet " +
                    "from bustrip " +
                    "join busstop as departure on departurestop=departure.city " +
                    "join busstop as arrival on arrivalstop=arrival.city " +
                    "where bustrip_id not in (" +
                        "select bustrip.bustrip_id " +
                        "from bustrip " +
                        "left outer join cancelled on bustrip.bustrip_id=cancelled.bustrip_id " +
                        "where weeknumber=@weeknumber and yearstamp=date_part('year', now())" +
                    ")";
                preparedStatement.AddParameter("weeknumber", weekNumber, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveBookingSchedule(int weekNumber)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText = "select * from bookingschedule " +
                                                "where weeknumber=@weeknumber";
                preparedStatement.AddParameter("weeknumber", weekNumber, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveCustomerEmail()
        {
            using (Statement statement = new Statement(connection))
            {
                statement.CommandText = "select email from customer";
                statement.Execute();
                return statement.GetDataSet();
            }
        }

        public DataSet RetrieveCustomerNumberOfTrip(int? lessThan, int? equal, int? greaterThan)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                /*
                string commandText = "select distinct email as epost, name as namn, address as adress, count(customer_id) as antalturer " +
                                     "from (" +
                                        "select distinct on (weeknumber, email, bustrip.bustrip_id) * " +
                                        "from customer " +
                                        "left join bookingschedule on email=customer_id " +
                                        "left join bustrip on bookingschedule.bustrip_id=bustrip.bustrip_id " +
                                        "where (yearstamp=date_part('year', now()) and weeknumber<date_part('week', now())) " +
                                        "or (yearstamp=date_part('year', now()) and weeknumber=date_part('week', now()) and dayofweek<date_part('isodow', now())) " +
                                        "or bookingschedule_id is null " +
                                        "group by email, bookingschedule_id, bustrip.bustrip_id" +
                                     ") as traveled " +
                                     "group by email, name, address ";
                                     */
                string commandText =
                    "select distinct customer.email as epost, name as namn, address as adress, number as telefonnummer, count(customer_id) as antalturer " +
                    "from customer " +
                    "left join phone on customer.email=phone.email " +
                    "left join (" +
                        "select weeknumber, customer_id, bookingschedule.bustrip_id " +
                        "from bookingschedule " +
                        "join bustrip on bookingschedule.bustrip_id=bustrip.bustrip_id " +
                        "where (yearstamp=date_part('year', now()) and weeknumber<date_part('week', now())) " +
                        "or (yearstamp=date_part('year', now()) and weeknumber=date_part('week', now()) and dayofweek<date_part('isodow', now())) " +
                        "group by weeknumber, customer_id, bookingschedule.bustrip_id" +
                    ") as booking on customer.email=customer_id " +
                    "group by customer.email, number ";


                if (lessThan.HasValue || equal.HasValue || greaterThan.HasValue)
                {
                    commandText += "having ";
                }

                if (lessThan.HasValue)
                {
                    commandText += "count(bookingschedule_id)<@lessthan ";
                    preparedStatement.AddParameter("lessthan", lessThan.Value, NpgsqlDbType.Integer);
                }

                if (equal.HasValue)
                {
                    commandText += lessThan.HasValue ? "or count(bookingschedule_id)=@equal " : "count(bookingschedule_id)=@equal ";
                    preparedStatement.AddParameter("equal", equal.Value, NpgsqlDbType.Integer);
                }

                if (greaterThan.HasValue)
                {
                    commandText += lessThan.HasValue || equal.HasValue ? "or count(bookingschedule_id)>@greaterthan " : "count(bookingschedule_id)>@greaterthan ";
                    preparedStatement.AddParameter("greaterthan", greaterThan.Value, NpgsqlDbType.Integer);
                }

                preparedStatement.CommandText = commandText + "order by customer.email asc";
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveCustomerBusTrip(string email)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText = "select distinct departurestop, arrivalstop, count(*) as antalturer " +
                                                "from (" +
                                                    "select distinct on (weeknumber, customer_id, bustrip.bustrip_id) bustrip.departurestop, bustrip.arrivalstop, count(distinct (weeknumber, customer_id, bustrip.bustrip_id)) " +
                                                    "from bookingschedule join bustrip on bookingschedule.bustrip_id=bustrip.bustrip_id " +
                                                    "where customer_id=@email " +
                                                    "and ((yearstamp=date_part('year', now()) and weeknumber<date_part('week', now())) " +
                                                    "or (yearstamp=date_part('year', now()) and weeknumber=date_part('week', now()) and dayofweek<date_part('isodow', now()))) " +
                                                    "group by bustrip.bustrip_id, weeknumber, bookingschedule.customer_id" +
                                                ") as traveled " +
                                                "group by departurestop, arrivalstop";
                preparedStatement.AddParameter("email", email, NpgsqlDbType.Varchar);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }
    }
}