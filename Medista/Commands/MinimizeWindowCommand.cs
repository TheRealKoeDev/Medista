// Test Header

using System.Windows;

namespace Medista.Commands
{
    public class MinimizeWindowCommand : Command
    {
        private readonly Window _window;

        public MinimizeWindowCommand(Window window)
        {
            _window = window;
        }

        public override void Execute(object? parameter)
        {
            _window.WindowState = WindowState.Minimized;
        }
    }
}