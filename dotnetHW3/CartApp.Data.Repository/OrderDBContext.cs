using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CartApp.Data.Repository
{
    class OrderDBContext
    {
        public IDbConnection GetConnection()
        {
            string con = "Data Source=.;Initial Catalog=Mydatabase;Integrated Security=True";//new ConfigurationBuilder().AddJsonFile("UI/appSettings.json").Build().GetConnectionString("MyDB");
            return new SqlConnection(con);
        }
    }
}
