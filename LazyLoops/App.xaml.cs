using AppData.Utils;
using Medista.Commands;
using Medista.Utils;
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
            Injector.Register(AppContainerBuilder.RegisterDatase);
            Injector.Register(AppContainerBuilder.RegisterViewModels);
            Injector.Build();

            SetCultureCommand setCultureCommand = new (Medista.Properties.Settings.Default.CurrentCulture ?? Thread.CurrentThread.CurrentUICulture.Name);
            setCultureCommand.Execute();     

            new MainWindow().Show();

            base.OnStartup(e);
        }
    }  
}
