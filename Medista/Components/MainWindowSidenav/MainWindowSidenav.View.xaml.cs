using Medista.Ultils;
using System.Windows.Controls;

namespace Medista.Components.MainWindowSidenav
{
    /// <summary>
    /// Interaction logic for MainWindowSidenavView.xaml
    /// </summary>
    public partial class MainWindowSidenavView : UserControl
    {
        public MainWindowSidenavView()
        {
            this.TryInitDesignInjector();
            DataContext = new MainWindowSidenavViewModel();
            InitializeComponent();
        }
    }
}
