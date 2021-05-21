using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

using CoreLibarary.Interfaces;
using CoreLibarary.Models;

namespace CoreLibarary.Repositories
{
    public class SQLUsersRepository : IUsersRepository
    {
        string connectionString =
            "Data Source=(local);Initial Catalog=Restaurateur;"
            + "Integrated Security=true";//Ссылка на подключение базы данных
        //Регистрация
        public int? Create(
            string name,
            string phone,
            string email,
            string password,
            int roleId)
        {
            int? userId = null;
            var queryString =
"INSERT INTO [Users] ([Name] ,[Phone] ,[Email] ,[Password] ,[RoleId]) OUTPUT Inserted.ID VALUES (@Name ,@Phone,@Email,@Password,@RoleId)";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                //Создание объеков команд и параметров.
                using (var command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    //Открытие подключения
                    connection.Open();
                    userId = (int?)command.ExecuteScalar();
                }
            }

            return userId;
        }

        public bool Delete(int userId)//Удаление пользователей
        {
            var queryString = "Delete from Reviews where CreatedBy=@id;Delete from Reviews where RecepientId=@id;Delete from Bookings where UpdatedBy=@id;Delete from Menu where UpdatedBy=@id;Delete from Users where id=@id;";//sql запрос
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", userId);
                connection.Open();
                var reader = command.ExecuteReader();
                    reader.Read();
            }
            return true;
        }

        public User Get(int userId)
        {
            return new User();
        }

        public ObservableCollection<User> Get()//Создание списка пользователей
        {
            User user = null;

            var list = new ObservableCollection<User>();
            var queryString = "select id,name,Password,Phone,email,RoleId from Users";
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

                        user = new User();

                        user.Id = (int)reader.GetValue(0);

                        dbVal = reader.GetValue(1);
                        if (!(dbVal is DBNull))
                        {
                            user.Name = (dbVal as string).Trim();
                        }

                        dbVal = (string)reader.GetValue(2);
                        if (!(dbVal is DBNull))
                        {
                            user.Password = (dbVal as string).Trim();
                        }

                        dbVal = (string)reader.GetValue(3);
                        if (!(dbVal is DBNull))
                        {
                            user.Phone = (dbVal as string).Trim();
                        }

                            dbVal = (string)reader.GetValue(4);
                            if (!(dbVal is DBNull))
                            {
                                user.Email = (dbVal as string).Trim();
                            }
                            user.RoleId = (Roles)reader.GetValue(5);
                            
                            list.Add(user);
                    }
                }
            }
            return list;
        }

        public User Get(string name)//Вход
        {
            User user = null;
            var queryString = "select top 1 id,Name,Password,Phone,Email,RoleId from Users where name=@name";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", name);
                // Создание объеков команд и параметров.
                connection.Open();

                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        object dbVal = null;

                        user = new User();
                        user.Id = (int)reader.GetValue(0);

                        dbVal = reader.GetValue(1);
                        if(!(dbVal is DBNull))
                        {
                            user.Name = (dbVal as string).Trim();
                        }
                                                
                        dbVal = (string)reader.GetValue(2);
                        if (!(dbVal is DBNull))
                        {
                            user.Password = (dbVal as string).Trim();
                        }

                        dbVal = (string)reader.GetValue(3);
                        if (!(dbVal is DBNull))
                        {
                            user.Phone = (dbVal as string).Trim();
                        }

                        dbVal  = (string)reader.GetValue(4);
                        if (!(dbVal is DBNull))
                        {
                            user.Email = (dbVal as string).Trim();
                        }

                        user.RoleId = (Roles)reader.GetValue(5);
                    }
                }
            }

            return user;
        }

        public bool IsAdminExists()
        {
            var queryString = "select top (1) Users.Id from Users inner join Roles on Roles.Id = Users.RoleId where Roles.Name = 'Administrator'";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);

                // Создание объеков команд и параметров.
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var idVal = reader.GetValue(0);

                        return !((idVal is DBNull)
                                    || (int)idVal < 0);
                    }
                }
            }
            return false;
        }

        public bool IsUserExists(string userName)
        {
            var queryString = "select top (1) Users.Name from Users where Users.Name = @Name";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Name", userName);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string idVal = (string)reader.GetValue(0);
                        return userName == idVal.Trim();
                    }
                }
            }
            return false;
        }
            public bool Update(User user)//Обновление
        {
            var queryString = "Update Users Set Phone = @Phone, Email = @Email, RoleId = @RoleId where id = @id";//sql запрос
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
            }

            return true;
        }
    }
}
