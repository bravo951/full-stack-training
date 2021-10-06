using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace dotnetHW1.Data.Respository
{
    class DbContext
    {
        public int excute(string cmdText, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Mydatabase;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            try
            {
                connection.Open();

                cmd.CommandText = cmdText;//"insert into Customer values(@id,@firstname,@lastname,@mobile,@email,@city,@state)";
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                cmd.Connection = connection;
                //cmd.Parameters.AddWithValue("@id", item.Id);
                //cmd.Parameters.AddWithValue("@firstname", item.FirstName);
                //cmd.Parameters.AddWithValue("@lastname", item.LastName);
                //cmd.Parameters.AddWithValue("@mobile", item.Mobile);
                int r = cmd.ExecuteNonQuery();//call this to insert,update,delete
                
                return r;
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            finally
            {
                connection.Close();
                connection.Dispose();
                cmd.Dispose();

            }
            return 0;
        }
    }
}
