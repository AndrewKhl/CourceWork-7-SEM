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

        public virtual DisplayMessageDelegate MessagePresenter { get; set; }

        protected string ViewModelName = nameof(BaseViewModel);

        protected CoreManager CoreManager;

        public virtual bool IsDialogClose
        {
            get { return _isDialogClose; }
            set
            {
                _isDialogClose = value;
                NotifyPropertyChanged(() => IsDialogClose);
            }
        }

        public UserViewModel CurrentUser { get; set; }

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
