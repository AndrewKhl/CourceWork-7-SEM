using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class BaseViewModel : PropertyChangedBase
    {
        protected string ViewModelName = nameof(BaseViewModel);

        protected readonly ErrorManager ErrorCounter;

        public BaseViewModel(string viewModelName)
        {
            ViewModelName = viewModelName;

            ErrorCounter = new ErrorManager(viewModelName);
        }
    }

    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
