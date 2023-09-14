using AppData.Utils;
using Medista.ViewModels.MainWindowSidenav;
using System.Windows.Controls;

namespace Medista.Views.MainWindowSidenav
{
    /// <summary>
    /// Interaction logic for MainWindowSidenav.xaml
    /// </summary>
    public partial class MainWindowSidenav : ListView
    {
        public MainWindowSidenav()
        {
            DataContext = Injector.Get<MainWindowSidenavViewModel>();
            InitializeComponent();
        }
    }
}
