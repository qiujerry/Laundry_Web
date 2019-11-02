using System;
using System.Data.SqlClient;
using System.Text;

namespace Laundry_Web
{
    public class LaundryDatabaseConnection
    {
        public void laundryDatabaseConnection()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "laundry-status.database.windows.net";
                builder.UserID = "hackohio";
                builder.Password = "Mason45040";
                builder.InitialCatalog = "laundry-status";
                builder.ConnectTimeout = 5;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append("SELECT * FROM [dbo].[laundry_status]");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2} {3}",
                                                                    reader.GetInt32(0),
                                                                    reader.GetString(1),
                                                                    reader.GetString(2),
                                                                    reader.GetInt32(3)
                                                                    );
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}
