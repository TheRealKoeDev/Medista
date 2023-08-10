// Test Header

using System.Threading;
using WPFLocalizeExtension.Engine;

namespace Medista.Commands
{
    public class SetCultureCommand : BaseCommand
    {
        private static readonly string _fallbackCulture = "en-US";

        private readonly string _cultureCode;

        public SetCultureCommand(string cultureCode)
        {
            _cultureCode = cultureCode;
        }

        public override void Execute(object? parameter = null)
        {
            System.Globalization.CultureInfo cuture = GetAvailableCulture(_cultureCode);

            Thread.CurrentThread.CurrentUICulture = cuture;
            Thread.CurrentThread.CurrentCulture = cuture;

            LocalizeDictionary.Instance.Culture = cuture;

            Properties.Settings.Default.CurrentCulture = cuture.Name;
            Properties.Settings.Default.Save();
        }

        private static System.Globalization.CultureInfo GetAvailableCulture(string? cultureCode)
        {
            if(cultureCode == null)
            {
                return new System.Globalization.CultureInfo(_fallbackCulture);
            }

            bool cultureIsAvailable = Properties.Settings.Default.AvailableLanguages.Contains(cultureCode);
            if (cultureIsAvailable)
            {
                return new System.Globalization.CultureInfo(cultureCode);
            }

            string culturePrefix = cultureCode.Split("-")[0];
            foreach (string? availableCulture in Properties.Settings.Default.AvailableLanguages)
            {
                if (availableCulture != null && availableCulture.StartsWith(culturePrefix))
                {
                    return new System.Globalization.CultureInfo(cultureCode);
                }
            }

            return new System.Globalization.CultureInfo(_fallbackCulture);
        }
    }
}