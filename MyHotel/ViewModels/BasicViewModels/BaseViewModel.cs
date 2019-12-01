using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class BaseViewModel : ValidationObservableModel
    {
        private readonly IShellViewModel _shell;

        private bool _isDialogClose;


        public BaseViewModel(IShellViewModel shell)
        {
            _shell = shell;
        }


        public UserViewModel CurrentUser => _shell.CurrentUser;

        public CoreManager CoreManager => _shell.CoreManager;

        public IShellViewModel ShellViewModel => _shell;


        public virtual DisplayMessageDelegate MessagePresenter { get; set; }

        public virtual bool IsDialogClose
        {
            get { return _isDialogClose; }
            set
            {
                _isDialogClose = value;
                NotifyPropertyChanged(() => IsDialogClose);
            }
        }   

        public virtual void SetClose()
        {
            IsDialogClose = true;
        }
    }
}
