// Test Header

using System.Windows;

namespace LazyLoops.Commands
{
    public class CloseApplicationCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}