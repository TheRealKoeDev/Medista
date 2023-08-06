using AppData.Utils;
using MediatR;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LazyLoops.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        protected readonly IMediator Mediator = Injector.Get<IMediator>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async virtual Task Initialize()
        {
            await Task.CompletedTask;
        }

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
