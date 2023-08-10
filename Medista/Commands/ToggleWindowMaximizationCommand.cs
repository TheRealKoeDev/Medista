// Test Header

using System.Windows;

namespace Medista.Commands
{
    public class ToggleWindowMaximizationCommand : BaseCommand
    {
        private readonly Window _window;

        public ToggleWindowMaximizationCommand(Window window)
        {
            _window = window;
        }

        public override void Execute(object? parameter)
        {
            bool isMaximized = _window.WindowState == WindowState.Maximized;
            _window.WindowState = isMaximized ? WindowState.Normal : WindowState.Maximized;
        }
    }
}