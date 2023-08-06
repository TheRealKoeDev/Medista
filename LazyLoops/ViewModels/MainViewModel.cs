using AppData.Utils;
using System;
using System.Threading.Tasks;

namespace LazyLoops.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        private ViewModel _currentViewModel = Injector.Get<DashboardViewModel>();

        public static event EventHandler? CurrentViewModelChanged;

        public static async Task SetCurrentViewModel<T>() where T : ViewModel
        {
            await SetCurrentViewModel(Injector.Get<T>());
        }

        public static async Task SetCurrentViewModel<T>(T viewModel) where T : ViewModel
        {
            await viewModel.Initialize();

            MainViewModel mainViewModel = Injector.Get<MainViewModel>();
            mainViewModel.CurrentViewModel = viewModel ?? throw new ArgumentException($"The parameter {nameof(viewModel)} can't be null.");
            CurrentViewModelChanged?.Invoke(mainViewModel, new EventArgs());
        }

        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
    }    
}
