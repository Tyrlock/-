namespace CoreLibarary.Repositories
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;

    using CoreLibarary.Interfaces;
    using CoreLibarary.Models;


    public class SQLMenuRepository : IMenuRepository
    {
        string connectionString =
    "Data Source=(local);Initial Catalog=Restaurateur;"
    + "Integrated Security=true";//Ссылка на подключение базы данных

        public int? Add(string productName,
            int cost,
            string description,
            int categoryId,
            int updatedBy,
            DateTimeOffset updatedDate,
            byte[] imageBytes)
        {
            int? menuid = null;
            var queryString =
"INSERT INTO [Menu] ([ProductName] ,[Cost] ,[Description] ,[CategoryId], [UpdatedBy], [UpdatedDate],[Image]) OUTPUT Inserted.ID VALUES (@ProductName ,@Cost ,@Description, @categoryId, @UpdatedBy, @UpdatedDate, @Image)";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                //Создание объеков команд и параметров.
                using (var command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    command.Parameters.AddWithValue("@UpdatedBy", updatedBy);
                    command.Parameters.AddWithValue("@UpdatedDate", updatedDate);
                    command.Parameters.AddWithValue("@Image", imageBytes);
                    //command.Parameters.AddWithValue("@Image", name);
                    //Открытие подключения
                    connection.Open();
                    menuid = (int?)command.ExecuteScalar();
                }
            }

            return menuid;
        }

        public bool Update(Dish menu)
        {
            var queryString = "Update Menu Set Cost = @Cost, Description = @Description, CategoryId = @CategoryId where id = @id";//sql запрос
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", menu.Id);
                command.Parameters.AddWithValue("@Cost", menu.Cost);
                command.Parameters.AddWithValue("@Description", menu.Description);
                command.Parameters.AddWithValue("@CategoryId", menu.CategoryId);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
            }

            return true;
        }
    
        public ObservableCollection<Dish> Get()
        {
            Dish menu = null;

            var list = new ObservableCollection<Dish>();
            var queryString = "select Id,ProductName,Cost,Description,CategoryId,UpdatedBy,UpdatedDate,Image from Menu";
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

                        menu = new Dish();

                        menu.Id = (int)reader.GetValue(0);

                        dbVal = reader.GetValue(1);
                        if (!(dbVal is DBNull))
                        {
                            menu.ProductName = (dbVal as string).Trim();
                        }

                        menu.Cost = (int)reader.GetValue(2);


                        dbVal = (string)reader.GetValue(3);
                        if (!(dbVal is DBNull))
                        {
                            menu.Description = (dbVal as string).Trim();
                        }

                        menu.CategoryId = (Category)reader.GetValue(4);

                        menu.UpdatedBy = (int)reader.GetValue(5);

                        menu.UpdatedDate = (DateTimeOffset)reader.GetValue(6);

                        menu.image = (byte[])reader.GetValue(7);

                        list.Add(menu);
                    }
                }
            }
            return list;
        }

        public bool IsDishExists(string dishname)
        {
            var queryString = "select top (1) Menu.ProductName from Menu where Menu.ProductName = @Name";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Name", dishname);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string idVal = (string)reader.GetValue(0);

                        return dishname==idVal.Trim();
                    }
                }
            }
            return false;
        }

        public bool Delete(int menuId)
        {
            var queryString = "Delete from Menu where id=@id";//sql запрос
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                var command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", menuId);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
            }
            return true;
        }
    }
}
