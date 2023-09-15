using Medista.Ultils;
using Medista.Utils;
using System.Windows;

namespace Medista.Components.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            this.TryInitDesignInjector();
            WindowStateHandler.Initialize(this);

            DataContext = AppData.Utils.Injector.Get<MainWindowViewModel>();

            InitializeComponent();
        }
    }
}
