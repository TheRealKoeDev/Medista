// Test Header

using System.Windows;

namespace Medista.Commands
{
    public class CloseApplicationCommand : Command
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}