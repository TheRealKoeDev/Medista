using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Medista.Components.MainWindowSidenavElement
{
    /// <summary>
    /// Interaction logic for MainWindowSidenavElementView.xaml
    /// </summary>
    public partial class MainWindowSidenavElementView : UserControl
    {
        public string Title {
            get => TextComponent.Text;
            set => TextComponent.Text = value;
        }

        public ImageSource Icon
        {
            get => IconComponent.Source;
            set => IconComponent.Source = value;
        }

        public MainWindowSidenavElementView()
        {
            InitializeComponent();
        }
    }
}
