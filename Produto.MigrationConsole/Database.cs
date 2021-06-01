using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Produto.MigrationConsole
{
    public class Database
    {
        private const string _connectionString = "Server=localhost\\SQLEXPRESS;Database=mercado;Trusted_Connection=True;";

        public static void RunMigrations()
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                RunMigrations(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rc => rc.AddSqlServer().WithGlobalConnectionString(_connectionString).ScanIn(typeof(Database).Assembly).For.Migrations())
                .AddLogging(lc => lc.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void RunMigrations(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
        }
    }
}
