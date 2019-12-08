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

        public ICommand LogoutCommand { get; set; }
        public ICommand AllUsersCommand { get; set; }
        public ICommand ShowRoomsCommand { get; set; }

        public UsersAndStaffViewModel UsersAndStaffViewModel
        {
            get => _usersAndStaffViewModel;
            set
            {
                _usersAndStaffViewModel = value;
                NotifyPropertyChanged(() => UsersAndStaffViewModel);
            }
        }
        
        public bool IsUsersAndStaffVisibility { get; set; }

        public bool IsRoomVisible { get; set; }

        public MainAdminControlViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            AllUsersCommand = new DelegateCommand(AllUsersCommandDelegate);
            ShowRoomsCommand = new DelegateCommand(AllRoomsCommandDelegate);

            VisualViewModel(false, true);
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
            VisualViewModel(false, true);

            CurrentUser.AttachModel(null);
            SetClose();
        }

        private void AllRoomsCommandDelegate(object o)
        {
            UsersAndStaffViewModel = null;

            VisualViewModel(false, true);
        }

        private void AllUsersCommandDelegate(object o)
        {
            UsersAndStaffViewModel = new UsersAndStaffViewModel(_shell);
            VisualViewModel(true, false);
        }

        private void VisualViewModel(bool isUsers, bool isRooms)
        {
            IsUsersAndStaffVisibility = isUsers;
            IsRoomVisible = isRooms;

            NotifyPropertyChanged(() => IsUsersAndStaffVisibility);
            NotifyPropertyChanged(() => IsRoomVisible);
        }
    }
}
