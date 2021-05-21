
namespace CoreLibarary.Repositories
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;

    using CoreLibarary.Interfaces;
    using CoreLibarary.Models;

    public class SQLBookingsRepository : IBookingsRepository
    {
        string connectionString =
"Data Source=(local);Initial Catalog=Restaurateur;"
+ "Integrated Security=true";//Ссылка на подключение базы данных
        public int? Add_Booking(string ClientName,
            DateTimeOffset DataTimeBooking,
            int Seats,
            Tables TableId,
            string ClientPhoneNumber,
            int UpdatedBy,
            DateTimeOffset UpdatedDate)
        {
            int? bookingid = null;
            var queryString =
"INSERT INTO [Bookings] ([ClientName] ,[DataTimeBooking] ,[Seats] ,[TableId], [ClientPhoneNumber], [UpdatedBy], [UpdatedDate]) OUTPUT Inserted.Id VALUES (@ClientName ,@DataTimeBooking ,@Seats, @TableId, @ClientPhoneNumber, @UpdatedBy,@UpdatedDate)";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                //Создание объеков команд и параметров.
                using (var command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ClientName", ClientName);
                    command.Parameters.AddWithValue("@DataTimeBooking", DataTimeBooking);
                    command.Parameters.AddWithValue("@Seats", Seats);
                    command.Parameters.AddWithValue("@TableId", TableId);
                    command.Parameters.AddWithValue("@ClientPhoneNumber", ClientPhoneNumber);
                    command.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
                    command.Parameters.AddWithValue("@UpdatedDate", UpdatedDate);
                    //command.Parameters.AddWithValue("@Image", name);
                    //Открытие подключения
                    connection.Open();
                    bookingid = (int?)command.ExecuteScalar();
                }
            }

            return bookingid;
        }

        public ObservableCollection<Booking> GGetBookedTablesByDate(DateTimeOffset date)
        {
            Booking booking = null;

            var list = new ObservableCollection<Booking>();
            var queryString =
"select Bookings.Id,ClientName,DataTimeBooking,Seats,TableId,ClientPhoneNumber,UpdatedBy,UpdatedDate,Name " +
"from Tables " +
"inner join Bookings on Bookings.TableId = Tables.Id " +
"where Bookings.DataTimeBooking>@date and Bookings.DataTimeBooking<dateadd(hh, 23, @date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@date", date);
                // Создание объеков команд и параметров.
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object dbVal = null;

                        booking = new Booking();

                        booking.Id = (int)reader.GetValue(0);

                        dbVal = reader.GetValue(1);
                        if (!(dbVal is DBNull))
                        {
                            booking.ClientName = (dbVal as string).Trim();
                        }

                        booking.DataTimeBooking = (DateTimeOffset)reader.GetValue(2);

                        booking.Seats = (int)reader.GetValue(3);

                        booking.TableId = (Tables)reader.GetValue(4);

                        dbVal = reader.GetValue(5);
                        if (!(dbVal is DBNull))
                        {
                            booking.ClientPhoneNumber = (dbVal as string).Trim();
                        }

                        booking.UpdatedBy = (int)reader.GetValue(6);

                        booking.UpdatedDate = (DateTimeOffset)reader.GetValue(7);

                        booking.Number_of_table = (string)reader.GetValue(8);
                        list.Add(booking);
                    }
                }
            }
            return list;
        }

        public ObservableCollection<Booking> Get()
        {
            Booking booking = null;

            var list = new ObservableCollection<Booking>();
            var queryString = "select Bookings.Id,ClientName,DataTimeBooking,Seats,TableId,ClientPhoneNumber,UpdatedBy,UpdatedDate,Tables.Name from Tables inner join Bookings on Bookings.TableId = Tables.Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);

                // Создание объеков команд и параметров.
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object dbVal = null;

                        booking = new Booking();

                        booking.Id = (int)reader.GetValue(0);

                        dbVal = reader.GetValue(1);
                        if (!(dbVal is DBNull))
                        {
                            booking.ClientName = (dbVal as string).Trim();
                        }

                        booking.DataTimeBooking = (DateTimeOffset)reader.GetValue(2);

                        booking.Seats = (int)reader.GetValue(3);

                        booking.TableId = (Tables)reader.GetValue(4);

                        dbVal = reader.GetValue(5);
                        if (!(dbVal is DBNull))
                        {
                            booking.ClientPhoneNumber = (dbVal as string).Trim();
                        }

                        booking.UpdatedBy = (int)reader.GetValue(6);

                        booking.UpdatedDate = (DateTimeOffset)reader.GetValue(7);

                        booking.Number_of_table = (string)reader.GetValue(8);
                        list.Add(booking);
                    }
                }
            }
            return list;
        }

        public ObservableCollection<Table> GetTables()
        {
            Table table = null;

            var list = new ObservableCollection<Table>();
            var queryString = "select Id,Name,TotalSeats from Tables";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);

                // Создание объеков команд и параметров.
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object dbVal = null;

                        table = new Table();

                        table.Id = (int)reader.GetValue(0);

                        dbVal = reader.GetValue(1);
                        if (!(dbVal is DBNull))
                        {
                            table.Name = (dbVal as string).Trim();
                        }

                        table.TotalSeats = (int)reader.GetValue(2);

                        list.Add(table);
                    }
                }
            }
            return list;
        }
    }
}
