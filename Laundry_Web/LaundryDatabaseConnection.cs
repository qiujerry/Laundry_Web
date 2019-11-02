using System;
using System.Collections;
using System.Data.SqlClient;
using System.Text;

namespace Laundry_Web
{
    public class LaundryDatabaseConnection
    {
        public ArrayList laundryDatabaseConnection()
        {
            ArrayList x = new ArrayList();
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
                                x.Add(new LaundryList
                                {
                                    MachineNumber = reader.GetInt32(0),
                                    Date = reader.GetString(1),
                                    Available = reader.GetString(2),
                                    TimeSet = reader.GetInt32(3)
                                }) ;
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return x;
        }
    }
}
