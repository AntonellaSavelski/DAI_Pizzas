using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using Pizzas.API.Models;

namespace Pizzas.API.Utils
{
    public static class basededatos {
        public static SqlConnection GetConnection(){
            SqlConnection db;
            string connectionString;

            connectionString = ConfigurationHelper.GetConfiguration().GetValue<string>("DatabaseSettings: ConnectionString");
            db= new SqlConnection(connectionString);
            return db;
        }

    }
}
