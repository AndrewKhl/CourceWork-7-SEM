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

        public BaseViewModel(CoreManager coreManager)
        {
            CoreManager = coreManager;
        }

        public virtual void SetClose()
        {
            IsDialogClose = true;
        }
    }
}
