using Medista.Commands;
using System.Windows;

namespace Medista.ViewModels
{
    public sealed class MainWindowTitleBarViewModel : ViewModel
    {
        public static CloseApplicationCommand CloseApplicationCommand => new CloseApplicationCommand();
        public static ToggleWindowMaximizationCommand ToggleWindowMaximizationCommand => new ToggleWindowMaximizationCommand(Application.Current.MainWindow);
        public static MinimizeWindowCommand MinimizeWindowCommand => new MinimizeWindowCommand(Application.Current.MainWindow);

        // TODO: Lern to use this as datacontext in ContextMenu
        public static SetCultureCommand SetGermanCultureCommand => new SetCultureCommand("de-DE");
        public static SetCultureCommand SetEnglishCultureCommand => new SetCultureCommand("en-US");

    }    
}
