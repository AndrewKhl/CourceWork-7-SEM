using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class GuestProfileViewModel : BaseViewModel
    {
        private Core.Guest _currentGuest;
        private bool _isPasswordShown;

        public ICommand EditProfileCommand { get; set; }

        public GuestViewModel Guest { get; set; }

        public bool IsPasswordShown
        {
            get => _isPasswordShown;
            set
            {
                _isPasswordShown = value;
                NotifyPropertyChanged(() => IsPasswordShown);
            }
        }

        public GuestProfileViewModel(IShellViewModel shellViewModel, Core.Guest guest) 
            : base(shellViewModel)
        {
            _currentGuest = guest;
            Guest = new GuestViewModel(guest);
            Guest.RefreshModel();

            EditProfileCommand = new DelegateCommand(EditProfileCommandDelegate, CanEditProfileCommandDelegate);
        }

        private bool IsEqual()
        {
            var equal = true;
            equal &= _currentGuest.Name == Guest.Name;
            equal &= _currentGuest.SecondName == Guest.LastName;
            equal &= DateTime.Parse(_currentGuest.BirthDay) == Guest.Birthday;

            return equal;
        }

        private bool CanEditProfileCommandDelegate(object o)
        {
            return !Guest.IsError && !IsEqual();
        }

        private void EditProfileCommandDelegate(object o)
        {

        }
    }
}
