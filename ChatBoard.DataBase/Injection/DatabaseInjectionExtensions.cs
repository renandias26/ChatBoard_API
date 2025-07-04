using ChatBoard.DataBase.Context;
using ChatBoard.DataBase.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ChatBoard.DataBase.Injection
{
    public static class DatabaseInjectionExtensions
    {
        public static readonly string HealthCheckTag = "DataBase";
        private static string _ConnectionString = "";

        public static IServiceCollection SetDatabase(
       this IServiceCollection services)
        {
            string connectionString = GetConnectionString();

            services.AddDbContextPool<DataBaseContext>(options =>
                options.UseNpgsql(connectionString));

            var repositories = typeof(DatabaseInjectionExtensions).Assembly.DefinedTypes.Where(x => x.FullName!.EndsWith("Repository"));

            foreach (var type in repositories)
            {
                var interfaceType = type.GetInterface($"I{type.Name}");
                if (interfaceType == null) { continue; }
                services.AddScoped(interfaceType, type);
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IHealthChecksBuilder AddDatabaseHealthCheck(this IHealthChecksBuilder builder)
        {
            string connectionString = GetConnectionString();

            return builder.AddNpgSql(
                connectionString,
                name: "DataBase",
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                tags: [HealthCheckTag]
            );
        }

        private static string GetConnectionString()
        {
            if (string.IsNullOrWhiteSpace(_ConnectionString))
            {
                _ConnectionString = BuildConnectionString();
            }
            return _ConnectionString;
        }

        private static string BuildConnectionString()
        {
            var buildconnectionString = new StringBuilder();
            buildconnectionString.Append($"Host={Environment.GetEnvironmentVariable("DB_Host")};");
            buildconnectionString.Append($"Port={Environment.GetEnvironmentVariable("DB_Port")};");
            buildconnectionString.Append($"Database={Environment.GetEnvironmentVariable("DB_Database")};");
            buildconnectionString.Append($"Username={Environment.GetEnvironmentVariable("DB_Username")};");
            buildconnectionString.Append($"Password={Environment.GetEnvironmentVariable("DB_Password")};");
            return buildconnectionString.ToString();
        }
    }
}
