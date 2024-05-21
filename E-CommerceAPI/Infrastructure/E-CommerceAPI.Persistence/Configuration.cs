using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence
{
    public static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();

               
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/E-CommerceAPI.API"));
                configurationManager.AddJsonFile("appsettings.Development.json");
                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
