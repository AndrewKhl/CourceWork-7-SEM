using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class UsersAndStaffViewModel : BaseViewModel
    {
        private UserViewModel _selectedPerson;
        private UserViewModel _editablePerson;

        public List<UserViewModel.Roles> RolesList =>
            Enum.GetValues(typeof(UserViewModel.Roles)).Cast<UserViewModel.Roles>().ToList();

        public ICommand AddPersonCommand { get; set; }
        public ICommand EditPersonCommand { get; set; }
        public ICommand DeletePersonCommand { get; set; }

        public ObservableCollection<UserViewModel> People { get; set; }

        public UserViewModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                NotifyPropertyChanged(() => SelectedPerson);

                EditablePerson = SelectedPerson.Copy();
            }
        }

        public UserViewModel EditablePerson
        {
            get => _editablePerson;
            set
            {
                _editablePerson = value;
                NotifyPropertyChanged(() => EditablePerson);
            }
        }

        public UsersAndStaffViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {
            AddPersonCommand = new DelegateCommand(AddCommandDelegate, CanAddCommandDelegate);
            EditPersonCommand = new DelegateCommand(EditCommandDelegate, CanEditCommandDelegate);
            DeletePersonCommand = new DelegateCommand(DeleteCommandDelegate, CanDeleteCommandDelegate);

            LoadData();
        }

        private void LoadData()
        {
            var guests = CoreManager.UserManager.Guests.ToList();
            var staff = CoreManager.UserManager.Staffs.ToList();

            var people = new List<UserViewModel>();

            foreach (var guest in guests)
            {
                var person = new UserViewModel();
                person.AttachModel(guest);
                people.Add(person);
            }

            foreach (var st in staff)
            {
                var person = new UserViewModel();
                person.AttachModel(st);
                people.Add(person);
            }

            People = new ObservableCollection<UserViewModel>(people);

            EditablePerson = new UserViewModel();
        }

        private bool IsEqual()
        {
            var equal = true;
            equal &= SelectedPerson?.Name == EditablePerson.Name;
            equal &= SelectedPerson?.LastName == EditablePerson.LastName;
            equal &= SelectedPerson?.Birthday == EditablePerson.Birthday;
            equal &= SelectedPerson?.Role == EditablePerson.Role;
            equal &= SelectedPerson?.Email == EditablePerson.Email;

            return equal;
        }

        private bool CanAddCommandDelegate(object o)
        {
            return EditablePerson != null
                && SelectedPerson?.Email != EditablePerson.Email 
                && !EditablePerson.IsError;
        }

        private void AddCommandDelegate(object o)
        {
            if (EditablePerson.Role == UserViewModel.Roles.Guests)
                CoreManager.UserManager.AddGuest((Guest)EditablePerson.ToPerson());
            else if (EditablePerson.Role == UserViewModel.Roles.Staff)
                CoreManager.UserManager.AddStaff((Staff)EditablePerson.ToPerson());
        }

        private bool CanEditCommandDelegate(object o)
        {
            if (EditablePerson == null || SelectedPerson?.Email != EditablePerson.Email)
                return false;

            return !IsEqual() && !EditablePerson.IsError;
        }

        private void EditCommandDelegate(object o)
        {
            if (EditablePerson.Role == UserViewModel.Roles.Guests)
                CoreManager.UserManager.ModifyGuest((Guest)EditablePerson.ToPerson());
            else if (EditablePerson.Role == UserViewModel.Roles.Staff)
                CoreManager.UserManager.ModifyStaff((Staff)EditablePerson.ToPerson());
        }

        private bool CanDeleteCommandDelegate(object o)
        {
            return EditablePerson != null && IsEqual();
        }

        private void DeleteCommandDelegate(object o)
        {
            if (EditablePerson.Role == UserViewModel.Roles.Guests)
                CoreManager.UserManager.RemoveGuest(EditablePerson.Email);
            else if (EditablePerson.Role == UserViewModel.Roles.Staff)
                CoreManager.UserManager.RemoveStaff(EditablePerson.Email);
        }
    }
}
