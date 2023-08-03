// Test Header

using System.Threading;
using System.Windows;

namespace LazyLoops.Commands
{
    public class SetCultureCommand : BaseCommand
    {
        private readonly Window _oldWindow = Application.Current.MainWindow;
        private readonly string _culture;

        public SetCultureCommand(string? culture)
        {
            _culture = culture ?? "de";
        }

        public override void Execute(object? parameter)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_culture);
        
            Properties.Settings.Default.CurrentCulture = _culture;
            Properties.Settings.Default.Save();

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();

            _oldWindow.Close();
        }
    }
}