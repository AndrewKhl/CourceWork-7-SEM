using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class MainAdminControlViewModel : BaseViewModel
    {
        private UsersAndStaffViewModel _usersAndStaffViewModel;
        private StatisticControlViewModel _statisticViewModel;
        private ServicesViewModel _servicesViewModel;
        private OrdersViewModel _ordersViewModel;

        public ICommand LogoutCommand { get; set; }
        public ICommand AllUsersCommand { get; set; }
        public ICommand ShowRoomsCommand { get; set; }
        public ICommand ShowStatisticCommand { get; set; }
        public ICommand ShowServicesCommand { get; set; }
        public ICommand ShowOrdersCommand { get; set; }


        public bool IsUsersAndStaffVisibility { get; set; }

        public bool IsRoomVisible { get; set; }

        public bool IsStatisticVisible { get; set; }

        public bool IsServicesVisible { get; set; }

        public bool IsOrdersVisible { get; set; }


        public MainAdminControlViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            AllUsersCommand = new DelegateCommand(AllUsersCommandDelegate);
            ShowRoomsCommand = new DelegateCommand(AllRoomsCommandDelegate);
            ShowStatisticCommand = new DelegateCommand(StatisticCommandDelegate);
            ShowServicesCommand = new DelegateCommand(ShowServicesCommandDelegate);
            ShowOrdersCommand = new DelegateCommand(ShowOrdersCommandDelegate);

            VisualViewModel(false, true, false, false, false);
        }

        public UsersAndStaffViewModel UsersAndStaffViewModel
        {
            get => _usersAndStaffViewModel;
            set
            {
                _usersAndStaffViewModel = value;
                NotifyPropertyChanged(() => UsersAndStaffViewModel);
            }
        }

        public StatisticControlViewModel StatisticViewModel
        {
            get => _statisticViewModel;
            set
            {
                _statisticViewModel = value;
                NotifyPropertyChanged(() => StatisticViewModel);
            }
        }

        public ServicesViewModel ServicesViewModel
        {
            get => _servicesViewModel;
            set
            {
                _servicesViewModel = value;
                NotifyPropertyChanged(() => ServicesViewModel);
            }
        }

        public OrdersViewModel OrdersViewModel
        {
            get => _ordersViewModel;
            set
            {
                _ordersViewModel = value;
                NotifyPropertyChanged(() => OrdersViewModel);
            }
        }


        public override void SetClose()
        {
            LogoutCommand = null;
            AllUsersCommand = null;

            UsersAndStaffViewModel = null;
            ShowServicesCommand = null;
            ShowOrdersCommand = null;

            base.SetClose();
        }

        private void LogoutCommandDelegate(object o)
        {
            VisualViewModel(false, true, false, false, false);

            CurrentUser.AttachModel(null);
            SetClose();
        }

        private void AllRoomsCommandDelegate(object o)
        {
            UsersAndStaffViewModel = null;

            VisualViewModel(false, true, false, false, false);
        }

        private void AllUsersCommandDelegate(object o)
        {
            UsersAndStaffViewModel = new UsersAndStaffViewModel(_shell);
            VisualViewModel(true, false, false, false, false);
        }

        private void StatisticCommandDelegate(object o)
        {
            StatisticViewModel = new StatisticControlViewModel(_shell);
            VisualViewModel(false, false, true, false, false);
        }

        private void ShowServicesCommandDelegate(object o)
        {
            ServicesViewModel = new ServicesViewModel(_shell);
            VisualViewModel(false, false, false, true, false);
        }

        private void ShowOrdersCommandDelegate(object o)
        {
            OrdersViewModel = new OrdersViewModel(_shell);
            VisualViewModel(false, false, false, false, true);
        }

        private void VisualViewModel(bool isUsers, bool isRooms, bool isStatistic, bool isServices, bool isOrders)
        {
            IsUsersAndStaffVisibility = isUsers;
            IsRoomVisible = isRooms;
            IsStatisticVisible = isStatistic;
            IsServicesVisible = isServices;
            IsOrdersVisible = isOrders;

            NotifyPropertyChanged(() => IsUsersAndStaffVisibility);
            NotifyPropertyChanged(() => IsRoomVisible);
            NotifyPropertyChanged(() => IsStatisticVisible);
            NotifyPropertyChanged(() => IsServicesVisible);
            NotifyPropertyChanged(() => IsOrdersVisible);
        }
    }
}
