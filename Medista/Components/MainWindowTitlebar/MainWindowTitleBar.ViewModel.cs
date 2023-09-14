using Medista.Commands;
using Medista.ViewModels;
using System.Windows;

namespace Medista.Components.MainWindowTitlebar
{
    public sealed class MainWindowTitleBarViewModel : ViewModel
    {
        public static CloseApplicationCommand CloseApplicationCommand => new ();
        public static ToggleWindowMaximizationCommand ToggleWindowMaximizationCommand => new(Application.Current.MainWindow);
        public static MinimizeWindowCommand MinimizeWindowCommand => new (Application.Current.MainWindow);

        // TODO: Lern to use this as datacontext in ContextMenu
        public static SetCultureCommand SetGermanCultureCommand => new ("de-DE");
        public static SetCultureCommand SetEnglishCultureCommand => new ("en-US");

    }    
}
