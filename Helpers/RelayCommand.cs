using System;
using System.Windows.Input;

namespace MyBudgetApp.Helpers
{
    public partial class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
    {
        private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public bool CanExecute(object? parameter) => canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();

        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public partial class RelayCommand<T>(Action<T> execute, Predicate<T>? canExecute = null) : ICommand
    {
        private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public bool CanExecute(object? parameter)
        {
            if (parameter is T tParam || parameter == null && typeof(T).IsClass)
                return canExecute?.Invoke((T?)parameter!) ?? true;
            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T tParam || parameter == null && typeof(T).IsClass)
                _execute((T?)parameter!);
        }

        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

