using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;
        private bool _isPasswordShown;
        private string _errorText;

        public ICommand LoginCommand { get; set; }

        public ICommand ShowPasswordCommand { get; set; }

        [Required(ErrorMessage = "Field 'Login' is required")]
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

        public LoginViewModel(CoreManager coreManager, UserViewModel user) 
            : base(coreManager, user)
        {
            LoginCommand = new DelegateCommand(LoginCommandDelegate, CanLoginCommandDelegate);
            ShowPasswordCommand = new DelegateCommand(ShowPasswordCommandDelegate);
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

            CurrentUser = new UserViewModel(user);
            SetClose();
        }

        private void ShowPasswordCommandDelegate(object o)
        {
            IsPasswordShown = !IsPasswordShown;
        }
    }
}
