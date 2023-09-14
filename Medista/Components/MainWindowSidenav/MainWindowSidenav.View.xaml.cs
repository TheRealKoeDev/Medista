using AppData.Utils;
using System.Windows.Controls;

namespace Medista.Components.MainWindowSidenav
{
    /// <summary>
    /// Interaction logic for MainWindowSidenavView.xaml
    /// </summary>
    public partial class MainWindowSidenavView : ListView
    {
        public MainWindowSidenavView()
        {
            DataContext = new MainWindowSidenavViewModel();
            InitializeComponent();
        }
    }
}
