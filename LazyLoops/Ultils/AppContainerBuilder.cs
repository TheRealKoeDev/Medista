// Test Header

using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace LazyLoops.Utils
{
    public static class AppContainerBuilder
    {
        private static Type[] SingletonTypes => Array.Empty<Type>();

        public static void RegisterViewModels(ContainerBuilder builder)
        {
            foreach (Type singletonType in SingletonTypes)
            {
                builder.RegisterType(singletonType).SingleInstance();
            }

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(IsTransientViewModel);
            builder.RegisterAssemblyOpenGenericTypes(Assembly.GetExecutingAssembly()).Where(IsCommand);
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
