using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class BaseViewModel : ObservableModel
    {
        private bool _isDialogClose;

        public virtual DisplayMessageDelegate MessagePresenter { get; set; }

        protected string ViewModelName = nameof(BaseViewModel);

        protected CoreManager CoreManager;

        //protected readonly ErrorManager ErrorCounter;
        public virtual bool IsDialogClose
        {
            get { return _isDialogClose; }
            set
            {
                _isDialogClose = value;
                NotifyPropertyChanged(() => IsDialogClose);
            }
        }

        public BaseViewModel(CoreManager coreManager)
        {
            CoreManager = coreManager;
            //ViewModelName = viewModelName;

            //ErrorCounter = new ErrorManager(viewModelName);
        }

        public virtual void SetClose()
        {
            IsDialogClose = true;
        }
    }
}
