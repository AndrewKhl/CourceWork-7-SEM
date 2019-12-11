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

        public ICommand LogoutCommand { get; set; }
        public ICommand AllUsersCommand { get; set; }
        public ICommand ShowRoomsCommand { get; set; }
        public ICommand ShowStatisticCommand { get; set; }


        public bool IsUsersAndStaffVisibility { get; set; }

        public bool IsRoomVisible { get; set; }

        public bool IsStatisticVisible { get; set; }


        public MainAdminControlViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            AllUsersCommand = new DelegateCommand(AllUsersCommandDelegate);
            ShowRoomsCommand = new DelegateCommand(AllRoomsCommandDelegate);
            ShowStatisticCommand = new DelegateCommand(StatisticCommandDelegate);

            VisualViewModel(false, true, false);
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


        public override void SetClose()
        {
            LogoutCommand = null;
            AllUsersCommand = null;

            UsersAndStaffViewModel = null;

            base.SetClose();
        }

        private void LogoutCommandDelegate(object o)
        {
            VisualViewModel(false, true, false);

            CurrentUser.AttachModel(null);
            SetClose();
        }

        private void AllRoomsCommandDelegate(object o)
        {
            UsersAndStaffViewModel = null;

            VisualViewModel(false, true, false);
        }

        private void AllUsersCommandDelegate(object o)
        {
            UsersAndStaffViewModel = new UsersAndStaffViewModel(_shell);
            VisualViewModel(true, false, false);
        }

        private void StatisticCommandDelegate(object o)
        {
            StatisticViewModel = new StatisticControlViewModel(_shell);
            VisualViewModel(false, false, true);
        }

        private void VisualViewModel(bool isUsers, bool isRooms, bool isStatistic)
        {
            IsUsersAndStaffVisibility = isUsers;
            IsRoomVisible = isRooms;
            IsStatisticVisible = isStatistic;

            NotifyPropertyChanged(() => IsUsersAndStaffVisibility);
            NotifyPropertyChanged(() => IsRoomVisible);
            NotifyPropertyChanged(() => IsStatisticVisible);
        }
    }
}
