using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileScanner.Commands
{
    public class AsyncDelegateCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Func<T, Task> _execute;

        public AsyncDelegateCommand(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        public AsyncDelegateCommand(Func<T, Task> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            await _execute((T)parameter);
        }

        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
