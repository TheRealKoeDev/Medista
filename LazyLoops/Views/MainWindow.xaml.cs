using LazyLoops.Utils;
using LazyLoops.ViewModels;
using System.Windows;

namespace LazyLoops
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStateHandler.Initialize(this);

            DataContext = AppData.Utils.Injector.Get<MainWindowViewModel>();

            InitializeComponent();
        }
    }
}
