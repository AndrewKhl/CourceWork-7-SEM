using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class MainWindowViewModel : ObservableModel, IDisposable
    {
        private readonly Window _mainWindow = Application.Current.MainWindow;
        private readonly CoreManager _coreManager;

        public ICommand LoginCommand { get; set; }

        public UserViewModel CurrentUser { get; set; } = new UserViewModel();


        public GuestMainControlViewModel GuestControlViewModel { get; set; }

        public MainWindowViewModel()
        {
            _coreManager = new CoreManager();

            GuestControlViewModel = new GuestMainControlViewModel(_coreManager, CurrentUser);

            LoginCommand = new DelegateCommand(LoginCommandDelegate);

            _mainWindow.Closed += CloseWindow;
        }

        public void Dispose()
        {
            _coreManager.Dispose();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Dispose();
        }

        private void LoginCommandDelegate(object o)
        {
            var loginViewModel = new LoginViewModel(_coreManager, CurrentUser);
            var loginDialog = new LoginDialog()
            {
                DataContext = loginViewModel,
                Owner = _mainWindow,
            };

            loginDialog.ShowDialog();

            if (CurrentUser != null)// && !User.IsAdmin)
                ShowGuestMainControl();
        }

        private void ShowGuestMainControl()
        {
            NotifyPropertyChanged(() => GuestControlViewModel);
        }
    }
}
