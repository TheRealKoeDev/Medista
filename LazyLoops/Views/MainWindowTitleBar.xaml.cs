using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LazyLoops.Commands;
using LazyLoops.Utils;
using System.Threading;

namespace LazyLoops.Views
{
    /// <summary>
    /// Interaction logic for WindowTitleBar.xaml
    /// </summary>
    public partial class WindowTitleBar : Border
    {
        private Window? Window;

        public static CloseApplicationCommand CloseApplicationCommand => new();
        public static ToggleWindowMaximizationCommand ToggleWindowMaximizationCommand => new(Application.Current.MainWindow);
        public static MinimizeWindowCommand MinimizeWindowCommand => new(Application.Current.MainWindow);

        public WindowTitleBar()
        {
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

        private void ChangeCultureClick(object sender, RoutedEventArgs args)
        {
            if (sender is not FrameworkElement selectedLanguageElement)
            {
                return;
            }

            string? selectedCulture = selectedLanguageElement.Tag as string;
            if (selectedCulture == null)
            {
                return;
            }

            // TODO: Refactor to use in xaml directly problem = DataContext
            new SetCultureCommand(selectedCulture ?? Thread.CurrentThread.CurrentUICulture.Name).Execute();
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