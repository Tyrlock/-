using CoreLibarary.Interfaces;
using CoreLibarary.Models;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace CoreLibarary.Repositories
{
    public class SQLReviewsRepository : IReviewsRepository
    {
        string connectionString =
"Data Source=(local);Initial Catalog=Restaurateur;"
+ "Integrated Security=true";//Ссылка на подключение базы данных

        public int? Create(int creatidBy, string description,int RecepientId)
        {
            int? reviewid = null;
            var queryString =
"INSERT INTO [Reviews] ([CreatedBy] ,[Description] ,[RecepientId]) OUTPUT Inserted.ID VALUES (@CreatidBy ,@Description ,@Name)";//sql запрос

            // Создание и открытие соединения в блоке using.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                //Создание объеков команд и параметров.
                using (var command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@CreatidBy", creatidBy);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Name", RecepientId);
                    //Открытие подключения
                    connection.Open();
                    reviewid = (int?)command.ExecuteScalar();
                }
            }

            return reviewid;
        }

        public ObservableCollection<Reviews> Get()
        {
            Reviews reviews = null;

            var list = new ObservableCollection<Reviews>();
            var queryString = 
                "select r.Id, r.CreatedBy, r.Description, r.RecepientId, u.Name, ru.Name " +
                "from Reviews r " +
                "inner join Users u on r.RecepientId = u.Id " +
                "inner join Users ru on r.CreatedBy = ru.Id";
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

                        reviews = new Reviews();

                        reviews.Id = (int)reader.GetValue(0);

                        reviews.CreatedBy = (int)reader.GetValue(1);

                        dbVal = (string)reader.GetValue(2);
                        if (!(dbVal is DBNull))
                        {
                            reviews.Description = (dbVal as string).Trim();
                        }

                        reviews.RecepientId = (int)reader.GetValue(3);

                        dbVal = (string)reader.GetValue(4);
                        if (!(dbVal is DBNull))
                        {
                            reviews.RecepientName = (dbVal as string).Trim();
                        }

                        dbVal = (string)reader.GetValue(5);
                        if (!(dbVal is DBNull))
                        {
                            reviews.CreatedByName = (dbVal as string).Trim();
                        }

                        list.Add(reviews);
                    }
                }
            }
            return list;
        }
    }
}
