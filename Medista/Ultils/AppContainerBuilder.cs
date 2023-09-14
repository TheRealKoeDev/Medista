// Test Header

using AppData.Utils;
using Medista.Ultils;
using Medista.ViewModels;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using FluentMigrator.Runner;
using Medista.Components.MainWindow;
using Medista.Components.MainWindowTitlebar;
using Medista.Components.MainWindowSidenav;

namespace Medista.Utils
{
    public static class AppContainerBuilder
    {
        private static Type[] SingletonTypes => new Type[] {
            typeof(MainWindowViewModel),
            typeof(MainWindowTitleBarViewModel),
            typeof(MainWindowSidenavViewModel),
            typeof(DashboardViewModel),
        };

        public static void RegisterDatabase(IServiceCollection serviceCollection)
        {
            string connectionString = $"Data Source={Path.Combine(AppFolderHandler.DataPath, "database.sqlite")}";

            Directory.CreateDirectory(AppFolderHandler.DataPath);
            serviceCollection.AddTransient(typeof(AppDatabaseConnection), (_services) =>
                new AppDatabaseConnection(ProviderName.SQLite, connectionString)
            );

            serviceCollection
                .AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                    builder.AddSQLite().WithGlobalConnectionString(connectionString).ScanIn(AppDomain.CurrentDomain.Load(nameof(AppData)))
                 )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        public static void RegisterViewModels(IServiceCollection serviceCollection)
        {
            foreach (Type singletonType in SingletonTypes)
            {
                serviceCollection.AddSingleton(singletonType);
            }
        }
    }
}
