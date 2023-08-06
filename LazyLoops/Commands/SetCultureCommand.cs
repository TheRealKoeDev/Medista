// Test Header

using LazyLoops.Localization.MainWindowTitleBar;
using LazyLoops.Utils;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using WPFLocalizeExtension.Extensions;

namespace LazyLoops.Commands
{
    public class SetCultureCommand : BaseCommand
    {
        private readonly Window _oldWindow = Application.Current.MainWindow;
        private readonly string _culture;

        public SetCultureCommand(string? culture)
        {
            _culture = culture ?? "en-US";
        }

        public override void Execute(object? parameter)
        {
            if (_culture == Properties.Settings.Default.CurrentCulture)
            {
                return;
            }

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(_culture);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(_culture);


            Properties.Settings.Default.CurrentCulture = _culture;
            Properties.Settings.Default.Save();

            var test123 = Application.ResourceAssembly.GetManifestResourceNames();


            foreach (string resourceName in Application.ResourceAssembly.GetManifestResourceNames())
            {
                if (!resourceName.StartsWith($"{nameof(LazyLoops)}.{nameof(Localization)}"))
                {
                    continue;
                }
            }
        }
    }
}