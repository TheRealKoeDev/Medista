using LazyLoops.Commands;
using LazyLoops.Utils;
using System.Threading;
using System.Windows;

namespace LazyLoops
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppData.Utils.Injector.Register(AppContainerBuilder.RegisterViewModels);
            AppData.Utils.Injector.Build();

            SetCultureCommand setCultureCommand = new(LazyLoops.Properties.Settings.Default.CurrentCulture ?? Thread.CurrentThread.CurrentUICulture.Name);
            setCultureCommand.Execute();     

            new MainWindow().Show();

            base.OnStartup(e);
        }
    }
}
