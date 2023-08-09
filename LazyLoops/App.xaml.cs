using AppData.Utils;
using LazyLoops.Commands;
using LazyLoops.Utils;
using LinqToDB;
using LinqToDB.Mapping;
using System.Threading;
using System.Windows;

namespace LazyLoops
{
    [Table("test_identity")]
    public partial class TestIdentity
    {
        [PrimaryKey, Identity] public int ID { get; set; } // integer
    }


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

            SetCultureCommand setCultureCommand = new (LazyLoops.Properties.Settings.Default.CurrentCulture ?? Thread.CurrentThread.CurrentUICulture.Name);
            setCultureCommand.Execute();     

            new MainWindow().Show();

            base.OnStartup(e);
        }
    }  
}
