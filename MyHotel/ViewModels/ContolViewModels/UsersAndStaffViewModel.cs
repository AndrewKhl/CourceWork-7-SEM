using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
        public ICommand AddSalaryCommand { get; set; }

        public ObservableCollection<UserViewModel> People { get; set; }

        public UserViewModel SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                NotifyPropertyChanged(() => SelectedPerson);

                EditablePerson = SelectedPerson?.Copy();
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
            AddSalaryCommand = new DelegateCommand(AddSalaryCommandDelegate, CanAddSalaryCommandDelegate);

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
                var person = new UserViewModel(st, UserViewModel.Roles.Staff, 
                    st.Salary, st.EmploymentDate.ToString());
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

        private Staff GetCurrentStaffModel()
        {
            return new Staff()
            {
                Id = EditablePerson.Id,
                Name = EditablePerson.Name,
                SecondName = EditablePerson.LastName,
                BirthDay = EditablePerson.Birthday.ToString(),
                Email = EditablePerson.Email,
                IsAdmin = EditablePerson.IsAdmin,
                Salary = EditablePerson.Salary,
                EmploymentDate = EditablePerson.EmploymentDate.ToString(),
            };
        }

        private Guest GetCurrentGuestModel()
        {
            return new Guest()
            {
                Id = EditablePerson.Id,
                Name = EditablePerson.Name,
                SecondName = EditablePerson.LastName,
                BirthDay = EditablePerson.Birthday.ToString(),
                Email = EditablePerson.Email,
                Password = EditablePerson.Password,
                IsAdmin = EditablePerson.IsAdmin,
            };
        }

        private bool CanAddCommandDelegate(object o)
        {
            return EditablePerson != null
                && SelectedPerson?.Email != EditablePerson.Email 
                && !EditablePerson.IsError;
        }

        private void AddCommandDelegate(object o)
        {
            var person = new Person();

            if (EditablePerson.Role == UserViewModel.Roles.Guests)
                person = CoreManager.UserManager.AddGuest(GetCurrentGuestModel());
            else if (EditablePerson.Role == UserViewModel.Roles.Staff)
                person = CoreManager.UserManager.AddStaff(GetCurrentStaffModel());

            People.Add(EditablePerson.Copy(person.Id));
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
                CoreManager.UserManager.ModifyGuest(GetCurrentGuestModel());
            else if (EditablePerson.Role == UserViewModel.Roles.Staff)
                CoreManager.UserManager.ModifyStaff(GetCurrentStaffModel());

            var index = People.ToList().FindIndex(p => p.Email == EditablePerson.Email);
            People.Insert(index + 1, EditablePerson.Copy(EditablePerson.Id));
            People.RemoveAt(index);
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

            People.Remove(SelectedPerson);
        }

        private bool CanAddSalaryCommandDelegate(object o)
        {
            return EditablePerson != null && !IsError 
                && !EditablePerson.IsError && SelectedPerson?.Email == EditablePerson.Email
                && SelectedPerson?.Role == EditablePerson.Role;
        }

        private void AddSalaryCommandDelegate(object o)
        {

        }
    }
}
