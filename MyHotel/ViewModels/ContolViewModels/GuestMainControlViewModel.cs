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
        private GuestProfileViewModel _profileViewModel;

        public ICommand LogoutCommand { get; set; }
        public ICommand SearchFreeRooms { get; set; }
        public ICommand ShowProfileCommand { get; set; }

        public GuestProfileViewModel ProfileViewModel
        {
            get => _profileViewModel;
            set
            {
                _profileViewModel = value;
                NotifyPropertyChanged(() => ProfileViewModel);
                NotifyPropertyChanged(() => IsProfileViewModelVisible);
            }
        }

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

        public bool IsProfileViewModelVisible => ProfileViewModel != null;

        public GuestMainControlViewModel(IShellViewModel shell) : base(shell)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            SearchFreeRooms = new DelegateCommand(SearchFreeRoomsDelegate, CanSearchFreeRoomsDelegate);
            ShowProfileCommand = new DelegateCommand(ShowProfileCommandDelegate);

            CheckIn = DateTime.Today;
            CheckOut = DateTime.Today.AddDays(1);
        }

        public override void SetClose()
        {
            LogoutCommand = null;
            SearchFreeRooms = null;
            ShowProfileCommand = null;

            base.SetClose();
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

        private void ShowProfileCommandDelegate(object o)
        {
            var currentGuest = CoreManager.UserManager.TryFindGuests(CurrentUser.Email);

            ProfileViewModel = new GuestProfileViewModel(_shell, currentGuest);
        }
    }
}
