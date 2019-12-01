using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _name;
        private string _lastName;
        private DateTime _birthday;
        private string _email;
        private string _password;
        private bool _isPasswordShown;
        private string _confirmPassword;
        private bool _isConfirmPAsswordShown;
        private string _errorText;

        public ICommand RegistrationCommand { get; set; }
        public ICommand BackCommand { get; set; }

        [Required(ErrorMessage = "Field 'Name' is required")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(() => Name);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged(() => LastName);
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                NotifyPropertyChanged(() => Birthday);
            }
        }

        [Required(ErrorMessage = "Field 'Name' is required")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChanged(() => Email);

                ErrorText = null;
            }
        }

        [Required(ErrorMessage = "Field 'Password' is required")]
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(() => Password);
            }
        }

        public bool IsPasswordShown
        {
            get => _isPasswordShown;
            set
            {
                _isPasswordShown = value;
                NotifyPropertyChanged(() => IsPasswordShown);
            }
        }

        [Required(ErrorMessage = "Field 'Confirm password' is required")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                NotifyPropertyChanged(() => ConfirmPassword);
            }
        }

        public bool IsConfirmPasswordShown
        {
            get => _isConfirmPAsswordShown;
            set
            {
                _isConfirmPAsswordShown = value;
                NotifyPropertyChanged(() => IsConfirmPasswordShown);
            }
        }

        public string ErrorText
        {
            get => _errorText; 
            set
            {
                _errorText = value;
                NotifyPropertyChanged(() => ErrorText);
            }
        }

        public RegistrationViewModel(IShellViewModel shellViewModel)
            : base(shellViewModel)
        {
            RegistrationCommand = new DelegateCommand(RegistrationCommandDelegate, CanRegistartionCommandDelegate);
            BackCommand = new DelegateCommand(BackCommandDelegate);

            Birthday = DateTime.Today;
        }

        private bool CanRegistartionCommandDelegate(object o)
        {
            return !IsError;
        }

        private void RegistrationCommandDelegate(object o)
        {
            var newGuest = new Core.Guest()
            {
                Name = Name,
                BirthDay = Birthday.ToString(),
                SecondName = LastName,
                Email = Email,
                Password = Password,
            };

            var guest = CoreManager.UserManager.AddGuest(newGuest);
            if (guest != null)
            {
                SetClose();
                CurrentUser.AttachModel(guest);
                return;
            }

            ErrorText = $"User with email '{Email}' is exists";
        }

        private void BackCommandDelegate(object o)
        {
            SetClose();
        }
    }
}
