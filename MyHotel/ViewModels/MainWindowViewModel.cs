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
        private readonly List<BaseViewModel> _controlViewModels; 

        private readonly Window _mainWindow = Application.Current.MainWindow;

        public CoreManager CoreManager { get; set; }

        public UserViewModel CurrentUser { get; set; } = new UserViewModel();

        public ObservableCollection<LivingRoomViewModel> LivingRooms { get; set; }


        public RoomsControlViewModel RoomsControlViewModel { get; set; }

        public GuestMainControlViewModel GuestControlViewModel { get; private set; }

        public MainAdminControlViewModel AdminControlViewModel { get; private set; }


        public ICommand LoginCommand { get; set; }

        public ICommand RegistrationCommand { get; set; }


        public MainWindowViewModel()
        {
            CoreManager = new CoreManager();

            LivingRooms = new ObservableCollection<LivingRoomViewModel>(CoreManager.RoomManager.LivingRooms.AsEnumerable()
                .Select(r => new LivingRoomViewModel(r, this)).ToList());

            GuestControlViewModel = new GuestMainControlViewModel(this);
            RoomsControlViewModel = new RoomsControlViewModel(this);
            AdminControlViewModel = new MainAdminControlViewModel(this);

            _controlViewModels = new List<BaseViewModel>()
            {
                GuestControlViewModel,
                RoomsControlViewModel,
                AdminControlViewModel,
            };

            LoginCommand = new DelegateCommand(LoginCommandDelegate);
            RegistrationCommand = new DelegateCommand(RegistartionCommandDelegate);

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
            var loginViewModel = new LoginViewModel(this)
            {
<<<<<<< HEAD
                Login = "admin@gmail.com",
=======
                Login = "a@mail.ru",
>>>>>>> s
                Password="123456",
            };

            var loginDialog = new LoginDialog()
            {
                DataContext = loginViewModel,
                Owner = _mainWindow,
            };

            loginDialog.ShowDialog();
        }

        private void RegistartionCommandDelegate(object o)
        {
            var registrationViewModel = new RegistrationViewModel(this);
            var registrationDialog = new RegistrationDialog()
            {
                DataContext = registrationViewModel,
                Owner = _mainWindow,
            };

            registrationDialog.ShowDialog();
        }
    }

    public interface IShellViewModel : IDisposable
    {
        UserViewModel CurrentUser { get; set; }

        CoreManager CoreManager { get; set; }

        RoomsControlViewModel RoomsControlViewModel { get; set; }

        ObservableCollection<LivingRoomViewModel> LivingRooms { get; set; }
    }
}
