using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace MvvmClean.Command
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }


    public class RelayCommand<T> : ICommand
    {
        readonly Action<T> _action;
        readonly Predicate<T> _canInvokeAction;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _action = execute ?? throw new ArgumentNullException(nameof(execute));
            _canInvokeAction = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (this._canInvokeAction == null)
                return true;
                if (parameter == null && typeof(T).GetTypeInfo().IsValueType)
                    return _canInvokeAction(default(T));
                if (parameter == null || parameter is T)
                    return _canInvokeAction((T)parameter);
            return false;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            if (typeof(T).IsPrimitive)
            {
                var converted = Convert.ChangeType(parameter, typeof(T));
                _action((T)converted);
            }
            else
            {
                _action((T)parameter);
            }
        }
    }
}