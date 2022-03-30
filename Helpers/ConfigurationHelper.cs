using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pizzas.API.Helpers {
    public class ConfigurationHelper {
        public static IConfiguration GetConfiguration(){
            IConfiguration config;
            var builder = new ConfigurationBuilder()
            .setBathPath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true,reloadOnchange: true);
            config = builder.Build();
            return config;
        }
    }
}