using Medista.Utils;
using Medista.ViewModels;
using System.Windows;

namespace Medista
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
