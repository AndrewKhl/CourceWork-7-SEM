using MyHotel.Core;
using System;
using System.Collections.Generic;
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

        public ICommand LoginCommand { get; set; }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged(() => Login);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(() => Password);
            }
        }

        public LoginViewModel(CoreManager coreManager) : base(coreManager)
        {
            LoginCommand = new DelegateCommand(LoginCommandDelegate);
        }

        private void LoginCommandDelegate(object o)
        {
            var user = CoreManager.UserManager.TryGetUser(Login, Password);
            if (user != null)
            {
                SetClose();
                return;
            }

            
        }
    }
}
