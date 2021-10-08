using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Data.Repository
{
    class MyDBContext
    {
        public IDbConnection GetConnection()
        {
            string con = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build().GetConnectionString("MyDB");
            return new SqlConnection(con);
        }
    }
}
