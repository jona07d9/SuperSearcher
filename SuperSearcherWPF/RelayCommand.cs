using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperSearcherWPF
{
    /// <summary>
    /// ICommand that uses delegates
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The command action
        /// </summary>
        private Action<object> _execute;
        /// <summary>
        /// A predicate to check if the command can execute
        /// </summary>
        private Predicate<object> _canExecute;

        /// <summary>
        /// Occurs when the CommandManager detects conditions that might change the ability of the command to execute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Check if the command can execute
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns>Whether or not the command can execute</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Execute the command action
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="execute">The command action</param>
        /// <param name="canExecute">A predicate to check if the command can execute</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
    }
}
