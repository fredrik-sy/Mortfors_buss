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
                    preparedStatement.CommandText = 
                        "insert into customer " +
                        "values (@email, @name, @address)";

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
                        preparedStatement.CommandText = "insert into phone " +
                                                        "values (@number, @email)";
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

        public bool RegisterBookingSchedule(int year, int week, string customerId, int tripId, int numberOfSeats)
        {
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                using (PreparedStatement preparedStatement = new PreparedStatement(connection))
                {
                    preparedStatement.Transaction = transaction;
                    preparedStatement.CommandText =
                        "insert into booking(year, week, customer_id, trip_id, numberofseats) " +
                        "values (@year, @week, @customer_id, @trip_id, @numberofseats)";

                    preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("customer_id", customerId, NpgsqlDbType.Varchar);
                    preparedStatement.AddParameter("trip_id", tripId, NpgsqlDbType.Integer);
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

        public DataSet RetrieveTrip(int year, int week)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText =
                    "select trip.*, " +
                    "departure.country as departurecountry, departure.street as departurestreet, " +
                    "arrival.country as arrivalcountry, arrival.street as arrivalstreet " +
                    "from trip " +
                    "join busstop as departure on departurestop=departure.city " +
                    "join busstop as arrival on arrivalstop=arrival.city " +
                    "where id not in (" +
                        "select id " +
                        "from trip " +
                        "left outer join cancelled on id=trip_id " +
                        "where year=@year and week=@week" +
                    ")";

                preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveBooking(int year, int week)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText =
                    "select * " +
                    "from booking " +
                    "where year=@year and week=@week";

                preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveCustomerEmail()
        {
            using (Statement statement = new Statement(connection))
            {
                statement.CommandText = 
                    "select email " +
                    "from customer";

                statement.Execute();
                return statement.GetDataSet();
            }
        }

        public DataSet RetrieveCustomerNumberOfTrip(int? lessThan, int? equal, int? greaterThan)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                string commandText =
                    "select distinct customer.email as epost, name as namn, address as adress, number as telefonnummer, count(customer_id) as antalturer " +
                    "from customer " +
                    "left join phone on customer.email=phone.email " +
                    "left join (" +
                        "select booking.week, customer_id, booking.trip_id " +
                        "from booking " +
                        "join trip on booking.trip_id=id " +
                        "left join cancelled on booking.week=cancelled.week and booking.trip_id=cancelled.trip_id " +
                        "where cancelled.week is null and cancelled.trip_id is null " +
                        "and ((booking.year=date_part('year', now()) and booking.week<date_part('week', now())) " +
                        "or (booking.year=date_part('year', now()) and booking.week=date_part('week', now()) and dayofweek<date_part('isodow', now()))) " +
                        "group by booking.week, customer_id, booking.trip_id" +
                    ") as booking on customer.email=customer_id " +
                    "group by customer.email, number ";
                
                if (lessThan.HasValue || equal.HasValue || greaterThan.HasValue)
                {
                    commandText += "having ";
                }

                if (lessThan.HasValue)
                {
                    commandText += "count(customer_id)<@lessthan ";
                    preparedStatement.AddParameter("lessthan", lessThan.Value, NpgsqlDbType.Integer);
                }

                if (equal.HasValue)
                {
                    commandText += lessThan.HasValue ? "or count(customer_id)=@equal " : "count(customer_id)=@equal ";
                    preparedStatement.AddParameter("equal", equal.Value, NpgsqlDbType.Integer);
                }

                if (greaterThan.HasValue)
                {
                    commandText += lessThan.HasValue || equal.HasValue ? "or count(customer_id)>@greaterthan " : "count(customer_id)>@greaterthan ";
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
                preparedStatement.CommandText =
                    "select distinct departurestop as avgång, arrivalstop as ankomst, count(*) as antalturer " +
                    "from (" +
                        "select distinct on (booking.week, customer_id, booking.trip_id) trip.departurestop, trip.arrivalstop " +
                        "from booking " +
                        "join trip on booking.trip_id=id " +
                        "left join cancelled on booking.week=cancelled.week and booking.trip_id=cancelled.trip_id " +
                        "where customer_id=@email " +
                        "and cancelled.week is null and cancelled.trip_id is null " +
                        "and ((booking.year=date_part('year', now()) and booking.week<date_part('week', now())) " +
                        "or (booking.year=date_part('year', now()) and booking.week=date_part('week', now()) and dayofweek<date_part('isodow', now()))) " +
                        "group by booking.week, booking.customer_id, booking.trip_id, trip.departurestop, trip.arrivalstop" +
                    ") as traveled " +
                    "group by departurestop, arrivalstop";

                preparedStatement.AddParameter("email", email, NpgsqlDbType.Varchar);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveDrivingSchedule(int year, int week)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText =
                    "select id, dayofweek, departurestop, departuretime, arrivalstop, arrivaltime, " +
                    "departure.country as departurecountry, departure.street as departurestreet, arrival.country as arrivalcountry, arrival.street as arrivalstreet " +
                    "from trip " +
                    "join busstop as departure on departurestop=departure.city " +
                    "join busstop as arrival on arrivalstop=arrival.city " +
                    "left join drive on id=trip_id and week=@week and year=@year " +
                    "where id not in (" +
                        "select id " +
                        "from trip " +
                        "left outer join cancelled on id=cancelled.trip_id " +
                        "where week=@week and year=@year" +
                    ") and driver_id is null";

                preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveDriverSchedule(int year, int week)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText =
                    "select driver_id, dayofweek, departuretime, arrivaltime " +
                    "from drive " +
                    "join trip on drive.trip_id=trip.id " +
                    "where year=@year and week=@week";

                preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public DataSet RetrieveDriver()
        {
            using (Statement statement = new Statement(connection))
            {
                statement.CommandText = "select personalnumber, name from driver";
                statement.Execute();
                return statement.GetDataSet();
            }
        }

        public bool RegisterDrivingSchedule(int year, int week, string driverId, int tripId)
        {
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable);

            try
            {
                using (PreparedStatement preparedStatement = new PreparedStatement(connection))
                {
                    preparedStatement.Transaction = transaction;
                    preparedStatement.CommandText =
                        "insert into drive (year, week, driver_id, trip_id) " +
                        "values (@year, @week, @driver_id, @trip_id)";

                    preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("driver_id", driverId, NpgsqlDbType.Varchar);
                    preparedStatement.AddParameter("trip_id", tripId, NpgsqlDbType.Integer);
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

        public DataSet GetNonCancelledTrip(int year, int week)
        {
            using (PreparedStatement preparedStatement = new PreparedStatement(connection))
            {
                preparedStatement.CommandText =
                    "select id, " +
                    "departurestop as avgång, to_char(departuretime, 'HH:MI') as avgångstid, " +
                    "arrivalstop as ankomst, to_char(arrivaltime, 'HH:MI') as ankomsttid " +
                    "from trip " +
                    "left join cancelled on trip.id=cancelled.trip_id and year=@year and week=@week " +
                    "where cancelled.trip_id is null";

                preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                preparedStatement.Execute();
                return preparedStatement.GetDataSet();
            }
        }

        public bool RegisterCancelledTrip(int year, int week, int id)
        {
            try
            {
                using (PreparedStatement preparedStatement = new PreparedStatement(connection))
                {
                    preparedStatement.CommandText =
                        "insert into cancelled (year, week, trip_id) " +
                        "values (@year, @week, @id)";

                    preparedStatement.AddParameter("year", year, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("week", week, NpgsqlDbType.Integer);
                    preparedStatement.AddParameter("id", id, NpgsqlDbType.Integer);
                    preparedStatement.Execute();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}