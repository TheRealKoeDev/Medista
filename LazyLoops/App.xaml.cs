using LazyLoops.Utils;
using Microsoft.Extensions.DependencyInjection;
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
            AppData.Utils.Container.Register(AppContainerBuilder.RegisterViewModels);
            AppData.Utils.Container.Build();

            string currentCulture = LazyLoops.Properties.Settings.Default.CurrentCulture;
            if (currentCulture != null)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentCulture);
            }

            LazyLoops.Properties.Settings.Default.Save();

            new MainWindow().Show();

            base.OnStartup(e);
        }
    }
}
