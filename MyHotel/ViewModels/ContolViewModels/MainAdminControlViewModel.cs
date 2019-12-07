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
        private bool _isUsersAndStaffVisibility;
        private UsersAndStaffViewModel _usersAndStaffViewModel;

        public ICommand LogoutCommand { get; set; }
        public ICommand AllUsersCommand { get; set; }

        public UsersAndStaffViewModel UsersAndStaffViewModel
        {
            get => _usersAndStaffViewModel;
            set
            {
                _usersAndStaffViewModel = value;
                NotifyPropertyChanged(() => UsersAndStaffViewModel);
            }
        }
        
        public bool IsUsersAndStaffVisibility 
        {
            get => _isUsersAndStaffVisibility;
            set
            {
                _isUsersAndStaffVisibility = value;
                NotifyPropertyChanged(() => IsUsersAndStaffVisibility);
            }
        }

        public MainAdminControlViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            AllUsersCommand = new DelegateCommand(AllUsersCommandDelegate);
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
            IsUsersAndStaffVisibility = false;

            CurrentUser.AttachModel(null);
            SetClose();
        }

        private void AllUsersCommandDelegate(object o)
        {
            VisualViewModel(true);
        }

        private void VisualViewModel(bool isUsers)
        {
            UsersAndStaffViewModel = new UsersAndStaffViewModel(_shell);
            IsUsersAndStaffVisibility = isUsers;

            NotifyPropertyChanged(() => IsUsersAndStaffVisibility);
        }
    }
}
