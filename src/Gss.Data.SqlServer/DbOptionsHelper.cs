using Microsoft.Extensions.Configuration;

namespace Gss.Data.SqlServer
{
    public class DbOptionsHelper
    {
        public static DatabaseOptions GetDatabaseOptions()
        {
            try
            {
                //Console.WriteLine(Directory.GetCurrentDirectory());
                var parentDir = Directory.GetParent(Directory.GetCurrentDirectory());
                var basePath = Directory.GetCurrentDirectory(); // Path.Combine(parentDir!.FullName, "Gss.Api");

                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                var builder = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile($"appsettings.{env}.json", true)
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();

                var config = builder.Build();

                return config.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"shit \t {ex} \r\n");
                return new DatabaseOptions();
            }
        }
    }
}
