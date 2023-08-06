using LazyLoops.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading;
using System.Windows;
using WPFLocalizeExtension.Engine;

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
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentCulture);
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }

            LocalizeDictionary.Instance.Culture = Thread.CurrentThread.CurrentUICulture;


            LazyLoops.Properties.Settings.Default.Save();

            new MainWindow().Show();

            base.OnStartup(e);
        }
    }
}
