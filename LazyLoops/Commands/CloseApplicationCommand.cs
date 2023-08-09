// Test Header

using System.Windows;

namespace Medista.Commands
{
    public class CloseApplicationCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}