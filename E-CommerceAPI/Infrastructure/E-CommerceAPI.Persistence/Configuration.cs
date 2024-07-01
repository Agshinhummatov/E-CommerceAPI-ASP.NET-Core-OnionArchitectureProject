using Microsoft.Extensions.Configuration;


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
