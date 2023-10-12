using Medista.ViewModels;
using System.Windows.Input;

namespace Medista.Components.MainWindowSidenavElement
{
    public sealed class MainWindowSidenavElementViewModel : ViewModel
    {
        private bool _active = false;
        public bool Active
        {
            set { _active = value; OnPropertyChanged(); }
            get => _active;
        }

        private ICommand? _command;
        public ICommand? Command
        {
            set { _command = value; OnPropertyChanged(); }
            get => _command;
        }
    }
}
