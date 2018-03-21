using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace MvvmClean.Command
{
    /// <summary>
    /// Defini une commande, basé sur System.Windows.Input.ICommand
    /// </summary>
    public class ActionCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        /// <summary>
        /// Création d'une instance de la commande avec l'action que doit executer la commande en paramètre
        /// </summary>
        /// <param name="actionToExecute">action a executer</param>
        public ActionCommand(Action<object> actionToExecute)
            : this(actionToExecute, null)
        {
            
        }

        /// <summary>
        /// Création d'une instance de la commande action l'action que doit executer la commande en paramètre
        /// ainsi que le prédicat déterminant si on peut executer cette commande
        /// </summary>
        /// <param name="actionToExecute">action a executer</param>
        /// <param name="canExecute">prédicat déterminant si on peut executer cette commande</param>
        public ActionCommand(Action<object> actionToExecute, Predicate<object> canExecute)
        {
            _execute = actionToExecute ?? throw new ArgumentNullException(nameof(actionToExecute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Détermine si la commande peut être executée avec le paramètre donné
        /// </summary>
        /// <param name="parameter">paramètre d'execution de la commande</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// evenement se produisant lorsque l'état de la possibilité d'execution change
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Execute la commande avec le paramètre donné
        /// </summary>
        /// <param name="parameter">paramètre d'execution de la commande</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }

    /// <summary>
    /// Defini une commande typée, basé sur System.Windows.Input.ICommand
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionCommand<T> : ICommand
    {
        readonly Action<T> _action;
        readonly Predicate<T> _canInvokeAction;

        /// <summary>
        /// Création d'une instance de la commande avec l'action que doit executer la commande en paramètre
        /// </summary>
        /// <param name="actionToExecute">action a executer</param>
        public ActionCommand(Action<T> actionToExecute)
            : this(actionToExecute, null)
        {
        }

        /// <summary>
        /// Création d'une instance de la commande action l'action que doit executer la commande en paramètre
        /// ainsi que le prédicat déterminant si on peut executer cette commande
        /// </summary>
        /// <param name="actionToExecute">action a executer</param>
        /// <param name="canExecute">prédicat déterminant si on peut executer cette commande</param>
        public ActionCommand(Action<T> actionToExecute, Predicate<T> canExecute)
        {
            _action = actionToExecute ?? throw new ArgumentNullException(nameof(actionToExecute));
            _canInvokeAction = canExecute;
        }

        /// <summary>
        /// Détermine si la commande peut être executée avec le paramètre donné
        /// </summary>
        /// <param name="parameter">paramètre d'execution de la commande</param>
        /// <returns></returns>
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

        /// <summary>
        /// evenement se produisant lorsque l'état de la possibilité d'execution change
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Execute la commande avec le paramètre donné
        /// </summary>
        /// <param name="parameter">paramètre d'execution de la commande</param>
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