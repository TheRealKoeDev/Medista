using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using AppData.Persistance;
using Autofac.Core;

namespace AppData.Utils
{
    public static class Container
    {
        private static readonly ContainerBuilder _builder = new();
        private static IContainer _container;

        static Container()
        {
            RegisterMediatorClasses(Assembly.GetExecutingAssembly());

            _builder.RegisterType(typeof(AppLightDbDatabase)).SingleInstance();

            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        public static T Get<T>() where T : notnull
        {
            return _container.Resolve<T>(Array.Empty<Parameter>());
        }

        public static object Get(Type type)
        {
            return _container.Resolve(type);
        }

        public static void Build()
        {
            _container = _builder.Build();
        }

        public static void Register(Action<ContainerBuilder> action)
        {
            action(_builder);
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            _container?.Dispose();
        }

        private static void RegisterMediatorClasses(Assembly assembly)
        {
            _builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            Type[] mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            _builder.RegisterAssemblyOpenGenericTypes(assembly).AsImplementedInterfaces();

            foreach (Type mediatrOpenType in mediatrOpenTypes)
            {
                _builder
                    .RegisterAssemblyTypes(assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            _builder
                .RegisterAssemblyOpenGenericTypes(assembly)
                .AsImplementedInterfaces();
        }
    }
}