// Test Header

using AppData.Utils;
using LazyLoops.Ultils;
using LazyLoops.ViewModels;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LazyLoops.Utils
{
    public static class AppContainerBuilder
    {
        private static Type[] SingletonTypes => new Type[] {
            typeof(MainWindowViewModel),
            typeof(MainWindowTitleBarViewModel),
        };

        public static void RegisterDatase(IServiceCollection serviceCollection)
        {
            Directory.CreateDirectory(AppFolderHandler.DataPath);
            serviceCollection.AddTransient(typeof(AppDatabaseConnection), (_services) =>
                new AppDatabaseConnection(ProviderName.SQLite, $"Data Source={Path.Combine(AppFolderHandler.DataPath, "database.sqlite")}")
            );
        }

        public static void RegisterViewModels(IServiceCollection serviceCollection)
        {
            Type[] assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type singletonType in SingletonTypes)
            {
                serviceCollection.AddSingleton(singletonType);
            }

            foreach (Type transientType in assemblyTypes.Where(IsTransientViewModel))
            {
                serviceCollection.AddTransient(transientType);
            }

            foreach (Type commantType in assemblyTypes.Where(IsCommand))
            {
                serviceCollection.AddTransient(commantType);
            }
        }
        private static bool IsTransientViewModel(Type type)
        {
            bool isSingleton = SingletonTypes.Contains(type);
            bool containsViewModelNamespace = type.Namespace?.StartsWith($"{nameof(LazyLoops)}.{nameof(ViewModels)}") ?? false;

            return containsViewModelNamespace && !isSingleton && type.IsClass && !type.IsAbstract;
        }

        private static bool IsCommand(Type type)
        {
            bool containsCommandNamespace = type.Namespace?.StartsWith($"{nameof(LazyLoops)}.{nameof(Commands)}") ?? false;

            return containsCommandNamespace && type.IsClass && !type.IsAbstract;
        }
    }
}
