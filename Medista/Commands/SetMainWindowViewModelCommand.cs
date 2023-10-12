using AppData.Utils;
using Medista.Components.MainWindow;
using Medista.ViewModels;

namespace Medista.Commands
{
    public class SetMainWindowViewModelCommand : Command
    {
        private readonly ViewModel _viewModel;

        public SetMainWindowViewModelCommand(ViewModel viewModel) {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            Injector.Get<MainWindowViewModel>().CurrentViewModel = _viewModel;
        }
    }
}
