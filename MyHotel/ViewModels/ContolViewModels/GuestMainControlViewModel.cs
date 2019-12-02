using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class GuestMainControlViewModel : BaseViewModel
    {
        private DateTime _checkIn;
        private DateTime _checkOut;

        public ICommand LogoutCommand { get; set; }
        public ICommand SearchFreeRooms { get; set; }

        public DateTime CheckIn
        {
            get => _checkIn;
            set
            {
                _checkIn = value;
                NotifyPropertyChanged(() => CheckIn);
            }
        }

        public DateTime CheckOut
        {
            get => _checkOut;
            set
            {
                _checkOut = value;
                NotifyPropertyChanged(() => CheckOut);
            }
        }

        public GuestMainControlViewModel(IShellViewModel shell) : base(shell)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            SearchFreeRooms = new DelegateCommand(SearchFreeRoomsDelegate, CanSearchFreeRoomsDelegate);

            CheckIn = DateTime.Today;
            CheckOut = DateTime.Today.AddDays(1);
        }

        public void RefreshModel()
        {
           
        }

        private void LogoutCommandDelegate(object o)
        {
            CurrentUser.AttachModel(null);
        }

        private bool CanSearchFreeRoomsDelegate(object o)
        {
            if (CheckIn.Date < DateTime.Today)
                return false;

            if (CheckIn > CheckOut)
                return false;

            return true;
        }

        private void SearchFreeRoomsDelegate(object o)
        {

        }
    }
}
