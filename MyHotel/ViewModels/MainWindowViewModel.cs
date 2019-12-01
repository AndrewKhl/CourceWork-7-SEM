using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class MainWindowViewModel : ObservableModel, IShellViewModel
    {
        private readonly Window _mainWindow = Application.Current.MainWindow;


        public CoreManager CoreManager { get; set; }

        public UserViewModel CurrentUser { get; set; } = new UserViewModel();

        public ObservableCollection<LivingRoomViewModel> LivingRooms { get; set; }


        public RoomsControlViewModel RoomsControlViewModel { get; set; }

        public GuestMainControlViewModel GuestControlViewModel { get; private set; }


        public ICommand LoginCommand { get; set; }


        public MainWindowViewModel()
        {
            CoreManager = new CoreManager();

            LivingRooms = new ObservableCollection<LivingRoomViewModel>(CoreManager.RoomManager.LivingRooms.AsEnumerable()
                .Select(r => new LivingRoomViewModel(r)).ToList());

            GuestControlViewModel = new GuestMainControlViewModel(this);

            LoginCommand = new DelegateCommand(LoginCommandDelegate);

            _mainWindow.Closed += CloseWindow;
        }

        public void Dispose()
        {
            CoreManager.Dispose();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Dispose();
        }

        private void LoginCommandDelegate(object o)
        {
            var loginViewModel = new LoginViewModel(this);
            var loginDialog = new LoginDialog()
            {
                DataContext = loginViewModel,
                Owner = _mainWindow,
            };

            loginDialog.ShowDialog();
        }
    }

    public interface IShellViewModel : IDisposable
    {
        UserViewModel CurrentUser { get; set; }

        CoreManager CoreManager { get; set; }

        ObservableCollection<LivingRoomViewModel> LivingRooms { get; set; }
    }
}
