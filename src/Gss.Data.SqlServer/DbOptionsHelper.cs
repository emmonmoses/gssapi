using Microsoft.Extensions.Configuration;

namespace Gss.Data.SqlServer
{
    public class DbOptionsHelper
    {
        public static DatabaseOptions GetDatabaseOptions()
        {
            var parentDir = Directory.GetParent(Directory.GetCurrentDirectory());
            var basePath = Path.Combine(parentDir!.FullName, "Gss.Api");

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
        }
    }
}
