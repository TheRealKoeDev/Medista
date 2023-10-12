using Medista.ViewModels;
using Medista.Components.MainWindowSidenavElement;
using Medista.Commands;
using AppData.Utils;
using Medista.Components.Dashboard;
using System.Collections.Generic;

namespace Medista.Components.MainWindowSidenav
{
    public sealed class MainWindowSidenavViewModel : ViewModel
    {

        private readonly List<MainWindowSidenavElementViewModel> elements =new() {
            new MainWindowSidenavElementViewModel() {
                Command = new SetMainWindowViewModelCommand(Injector.Get<DashboardViewModel>()),
            }
        };

        public List<MainWindowSidenavElementViewModel> Elements { get => elements; }
    }
}
