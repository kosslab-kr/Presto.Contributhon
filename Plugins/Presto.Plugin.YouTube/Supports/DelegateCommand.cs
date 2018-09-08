using System;
using System.Windows.Input;

namespace Presto.Plugin.YouTube.Supports
{
    public class DelegateCommand : ICommand
    {
        #region 상수
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        #endregion

        #region 생성자
        public DelegateCommand(Action<object> execute) : this(execute, null)
        {

        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand
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

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }
        #endregion
    }
}
