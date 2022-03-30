using System.Data.SqlClient;
using Dapper;

namespace Pizzas.API.Controllers
{
    public class basededatos {
        public static sqlConnection GetConnection(){
            SqlConnection db;
            string connectionString;

            connectionString = ConfigurationHelper.GetConfiguration().GetValue<string>("DatabaseSettings: ConnectionString");
            db= new SqlConnection(connectionString);
            return db;
        }

    }
}
