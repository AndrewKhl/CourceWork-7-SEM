using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public abstract class ViewCommandSuggestionRequired
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public delegate void ExecuteDelegate(object param);
    public delegate bool CanExecuteDelegate(object param);

    public class DelegateCommand : ViewCommandSuggestionRequired, ICommand
    {

        private readonly ExecuteDelegate _execDelegate;
        private readonly CanExecuteDelegate _canExecuteDelegate;

        public DelegateCommand(ExecuteDelegate executeDelegate)
        {
            _canExecuteDelegate = null;
            _execDelegate = executeDelegate;
        }

        public DelegateCommand(ExecuteDelegate executeDelegate, CanExecuteDelegate canExecuteDelegate)
        {
            _canExecuteDelegate = canExecuteDelegate;
            _execDelegate = executeDelegate;
        }

        #region Implementation of ICommand

        public void Execute(object parameter)
        {
            _execDelegate(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteDelegate != null)
                return _canExecuteDelegate(parameter);
            return true;
        }

        #endregion
    }
}
