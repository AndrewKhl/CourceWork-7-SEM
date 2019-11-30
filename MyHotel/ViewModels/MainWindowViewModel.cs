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
    public class MainWindowViewModel : IDisposable
    {
        private readonly Window _mainWindow = Application.Current.MainWindow;
        private readonly CoreManager _coreManager;

        public ICommand LoginCommand { get; set; }

        public MainWindowViewModel()
        {
            _mainWindow.Closed += CloseWindow;

            _coreManager = new CoreManager();

            LoginCommand = new DelegateCommand(LoginCommandDelegate);
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
            var loginViewModel = new LoginViewModel(_coreManager);
            var loginDialog = new LoginDialog()
            {
                DataContext = loginViewModel,
                Owner = _mainWindow,
            };

            loginDialog.ShowDialog();
        }
    }
}
