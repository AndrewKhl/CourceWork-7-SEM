using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class LoginViewModel : BaseViewModel
    {
        private GuestMainControlViewModel _guestControl;
        private string _login;
        private string _password;
        private bool _isPasswordShown;
        private string _errorText;

        public ICommand LoginCommand { get; set; }

        public ICommand RegistrationCommand { get; set; }

        public ICommand ShowPasswordCommand { get; set; }

        [CustomEmailAddress(ErrorMessage = "Incorrect Email address")]
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged(() => Login);

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

                ErrorText = null;
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

        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                NotifyPropertyChanged(() => ErrorText);
            }
        }

        public LoginViewModel(IShellViewModel shell, GuestMainControlViewModel guestControl) : base(shell)
        {
            _guestControl = guestControl;

            LoginCommand = new DelegateCommand(LoginCommandDelegate, CanLoginCommandDelegate);
            RegistrationCommand = new DelegateCommand(RegistartionCommandDelegate);
            ShowPasswordCommand = new DelegateCommand(ShowPasswordCommandDelegate);
        }

        public override void SetClose()
        {
            LoginCommand = null;
            RegistrationCommand = null;
            ShowPasswordCommand = null;

            base.SetClose();
        }

        private bool CanLoginCommandDelegate(object o)
        {
            return !IsError;
        }

        private void LoginCommandDelegate(object o)
        {
            var user = CoreManager.UserManager.TryGetUser(Login, Password);
            if (user == null)
            {
                ErrorText = $"Incorrect login or password";
                return;
            }

            CurrentUser.AttachModel(user);
            SetClose();
        }

        private void ShowPasswordCommandDelegate(object o)
        {
            IsPasswordShown = !IsPasswordShown;
        }

        private void RegistartionCommandDelegate(object o)
        {
            var registrationViewModel = new RegistrationViewModel(_shell);
            var registrationDialog = new RegistrationDialog()
            {
                DataContext = registrationViewModel,
                Owner = Application.Current.MainWindow
            };

            registrationDialog.ShowDialog();

            if (string.IsNullOrEmpty(CurrentUser.Email))
                return;

            SetClose();
        }
    }
}
