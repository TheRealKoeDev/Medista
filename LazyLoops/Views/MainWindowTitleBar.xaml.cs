using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LazyLoops.Commands;
using LazyLoops.Utils;

namespace LazyLoops.Views
{
    /// <summary>
    /// Interaction logic for WindowTitleBar.xaml
    /// </summary>
    public partial class WindowTitleBar : Border
    {
        private Window Window;

        public static CloseApplicationCommand CloseApplicationCommand => new();
        public static ToggleWindowMaximizationCommand ToggleWindowMaximizationCommand => new(Application.Current.MainWindow);

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

        public void MinimizeButtonClick(object sender, RoutedEventArgs args)
        {
            Window.WindowState = WindowState.Minimized;
        }

        private void CurrentCultureButtonClick(object sender, RoutedEventArgs args)
        {
            if (sender is not FrameworkElement currentCultureButton)
            {
                return;
            }

            currentCultureButton.ContextMenu.IsOpen = true;
        }

        private void ChangeCultureClick(object sender, RoutedEventArgs args)
        {
            if (sender is not FrameworkElement selectedLanguageElement)
            {
                return;
            }

            string? selectedCulture = selectedLanguageElement.Tag as string;
            // TODO: Refactor to use in xaml directly
            new SetCultureCommand(selectedCulture).Execute(null);
        }

        public void UpdateMaximizationToggleIcon(object? sender = null, EventArgs? args = null)
        {
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