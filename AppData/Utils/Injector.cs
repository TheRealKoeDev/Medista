﻿using System.Reflection;
using AppData.Persistance;
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

            _hostBuilder.ConfigureServices(constext => {
                constext.AddSingleton(typeof(AppLightDbDatabase));
            });

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

#pragma warning disable CS8603 // Possible null reference return.
            return _host.Services!.GetService(type);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static void Build()
        {
            if (_host != null)
            {
                throw new TypeLoadException("Build can only be called once");
            }

            _host = _hostBuilder?.Build();
            _host?.Start();
        }

        public static void Register(Action<IServiceCollection> action)
        {
            if (_host != null) 
            {
                throw new TypeLoadException("Types can't be registered after build phase");
            }

            _hostBuilder.ConfigureServices(action);
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
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