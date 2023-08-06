using AppData.Utils;
using MediatR;
using System;
using System.Windows.Input;

namespace LazyLoops.Commands
{
    public abstract class BaseCommand : ICommand, IDisposable
    {
        protected readonly IMediator _mediator = Injector.Get<IMediator>();

        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public abstract void Execute(object? parameter);
    }
}

