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
        private bool _isDialogClose;
        private UserViewModel _currentUser;

        public virtual DisplayMessageDelegate MessagePresenter { get; set; }

        protected string ViewModelName = nameof(BaseViewModel);

        protected CoreManager CoreManager;

        public bool UserAuth => CurrentUser != null;

        public bool IsAdmin => CurrentUser == null ? false : CurrentUser.IsAdmin;
        
        public UserViewModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                NotifyPropertyChanged(() => CurrentUser);
                NotifyPropertyChanged(() => UserAuth);
                NotifyPropertyChanged(() => IsAdmin);
            }
        }

        public virtual bool IsDialogClose
        {
            get { return _isDialogClose; }
            set
            {
                _isDialogClose = value;
                NotifyPropertyChanged(() => IsDialogClose);
            }
        }

        public BaseViewModel(CoreManager coreManager, UserViewModel currentUser)
        {
            CoreManager = coreManager;
            CurrentUser = currentUser;
        }

        public virtual void SetClose()
        {
            IsDialogClose = true;
        }
    }
}
