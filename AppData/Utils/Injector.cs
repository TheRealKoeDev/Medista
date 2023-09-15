using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AppData.Utils
{
    public static class Injector
    {
        private static IHost? _host;
        private static readonly IHostBuilder _hostBuilder = Host.CreateDefaultBuilder();

        static Injector()
        {
            RegisterMediatorClasses(Assembly.GetExecutingAssembly());
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        public static T Get<T>() where T : class
        {
            if (_host == null)
            {
                throw new TypeLoadException("Build must be called first");
            }

            return _host.Services.GetRequiredService<T>();
        }

        public static object Get(Type type)
        {
            if (_host == null)
            {
                throw new TypeLoadException("Build must be called first");
            }

            return _host.Services!.GetService(type)!;
        }

        public static void Build()
        {
            if (_host != null)
            {
                return;
            }

            _host = _hostBuilder?.Build();
            _host?.Start();
        }

        public static void Register(Action<IServiceCollection> action)
        {
            if (_host != null) 
            {
                return;
            }

            _hostBuilder.ConfigureServices(action);
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs? e)
        {
            _host?.Dispose();
        }

        private static void RegisterMediatorClasses(Assembly assembly)
        {
            _hostBuilder.ConfigureServices(context =>
            {
                context.AddMediatR((config) =>
                {
                    config.RegisterServicesFromAssembly(assembly);
                });
            });
        }
    }
}