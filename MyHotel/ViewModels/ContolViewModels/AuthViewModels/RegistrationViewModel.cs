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
        private string _password;
        private bool _isPasswordShown;
        private string _confirmPassword;
        private bool _isConfirmPAsswordShown;
        private string _errorText;

        public ICommand RegistrationCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand ShowConfirmPasswordCommand { get; set; }

        public GuestViewModel Guest { get; set; }

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
        [Compare(nameof(Password), ErrorMessage = "'Confirm password' should be same as 'Password'")]
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
            ShowPasswordCommand = new DelegateCommand(ShowPasswordCommandDelegate);
            ShowConfirmPasswordCommand = new DelegateCommand(ShowConfirmPasswordCommandDelegate);

            Guest = new GuestViewModel()
            {
                Birthday = DateTime.Today,
            };
        }

        public override void SetClose()
        {
            RegistrationCommand = null;
            BackCommand = null;
            ShowPasswordCommand = null;
            ShowConfirmPasswordCommand = null;

            base.SetClose();
        }

        private bool CanRegistartionCommandDelegate(object o)
        {
            return !IsError && !Guest.IsError;
        }

        private void RegistrationCommandDelegate(object o)
        {
            var newGuest = new Core.Guest()
            {
                Name = Guest.Name,
                BirthDay = Guest.Birthday.ToString(),
                SecondName = Guest.LastName,
                Email = Guest.Email,
                Password = Password,
            };

            var guest = CoreManager.UserManager.AddGuest(newGuest);
            if (guest != null)
            {
                SetClose();
                CurrentUser.AttachModel(guest);
                return;
            }

            ErrorText = $"User with email '{Guest.Email}' is exists";
        }

        private void BackCommandDelegate(object o)
        {
            SetClose();
        }

        private void ShowPasswordCommandDelegate(object o)
        {
            IsPasswordShown = !IsPasswordShown;
        }

        private void ShowConfirmPasswordCommandDelegate(object o)
        {
            IsConfirmPasswordShown = !IsConfirmPasswordShown;
        }
    }
}
