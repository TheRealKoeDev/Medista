using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LazyLoops.Commands;
using LazyLoops.Utils;
using System.Threading;
using LazyLoops.ViewModels;
using AppData.Utils;

namespace LazyLoops.Views
{
    /// <summary>
    /// Interaction logic for MainWindowTitleBar.xaml
    /// </summary>
    public partial class MainWindowTitleBar: UserControl
    {
        private Window? Window;

        public MainWindowTitleBar()
        {
            DataContext = Injector.Get<MainWindowTitleBarViewModel>();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Loaded += TitleBarLoaded;
            }

            InitializeComponent();
        }

        public void TitleBarLoaded(object? sender, EventArgs? args)
        {
            Window = Window.GetWindow(this);

            UpdateMaximizationToggleIcon();

            Window.StateChanged += UpdateMaximizationToggleIcon;
        }

        public void UpdateMaximizationToggleIcon(object? sender = null, EventArgs? args = null)
        {
            if (Window == null)
            {
                return;
            }

            PackIconKind toggleMaximizationIcon = Window.WindowState == WindowState.Maximized ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
            MaxButton.Content = new PackIcon { Kind = toggleMaximizationIcon };
        }

        private void TitleBarMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton != MouseButton.Left)
            {
                return;
            }
            else if (args.ClickCount == 2)
            {
                WindowStateHandler.ToggleMaximization();
            }
            else
            {
                Application.Current.MainWindow.DragMove();
            }
        }
    }
}