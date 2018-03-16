using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace MvvmClean.Command
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
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

        #endregion // ICommand Members
    }


    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _action;
        readonly Predicate<T> _canInvokeAction;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _action = execute ?? throw new ArgumentNullException(nameof(execute));
            _canInvokeAction = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

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
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
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

        #endregion // ICommand Members
    }
}