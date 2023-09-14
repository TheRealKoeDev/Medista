using AppData.Utils;
using FluentMigrator.Runner;
using Medista.Commands;
using Medista.Utils;
using Medista.Components.MainWindow;
using System.Threading;
using System.Windows;

namespace Medista
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Injector.Register(AppContainerBuilder.RegisterDatabase);
            Injector.Register(AppContainerBuilder.RegisterViewModels);
            Injector.Build();

            Injector.Get<IMigrationRunner>().MigrateUp();

            SetCultureCommand setCultureCommand = new (Medista.Properties.Settings.Default.CurrentCulture ?? Thread.CurrentThread.CurrentUICulture.Name);
            setCultureCommand.Execute();     

            new MainWindowView().Show();

            base.OnStartup(e);
        }
    }  
}
