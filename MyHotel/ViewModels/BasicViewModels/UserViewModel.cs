using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class UserViewModel : ValidationObservableModel
    {
        private Person _model;
        private string _name;
        private string _email;
        private string _secondName;
        private DateTime _birthday;
        private string _password;
        private bool _isAdmin;
        private Roles _role;

        public enum Roles
        {
            Guests,
            Staff,
        };

        [Required(ErrorMessage = "Field 'Name' is required")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(() => Name);
            }
        }

        [CustomEmailAddress(ErrorMessage = "Incorrect Email address")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChanged(() => Email);
            }
        }

        public string LastName 
        { 
            get => _secondName; 
            set
            {
                _secondName = value;
                NotifyPropertyChanged(() => LastName);
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                NotifyPropertyChanged(() => Birthday);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(() => Password);
            }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                NotifyPropertyChanged(() => IsAdmin);
            }
        }

        public Roles Role
        {
            get => _role;
            set
            {
                _role = value;
                NotifyPropertyChanged(() => Role);
            }
        }

        public bool UnknownUser => _model == null;

        public bool UserAuth => _model != null && !IsAdmin;

        public bool AdminAuth => _model != null && IsAdmin;

        public UserViewModel() { }

        public void AttachModel(Person user, Roles role = Roles.Guests)
        {
            _model = user;
            Name = user?.Name;
            LastName = user?.SecondName;
            Email = user?.Email;
            Password = user?.Password;
            if (DateTime.TryParse(user?.BirthDay, out var birthday))
                Birthday = birthday;
            IsAdmin = user?.IsAdmin ?? false;
            Role = role;

            RefreshModel();
        }

        public virtual void RefreshModel()
        {
            NotifyPropertyChanged(() => Name);
            NotifyPropertyChanged(() => Email);
            NotifyPropertyChanged(() => LastName);
            NotifyPropertyChanged(() => Birthday);
            NotifyPropertyChanged(() => Role);
            NotifyPropertyChanged(() => IsAdmin);
            NotifyPropertyChanged(() => UserAuth);
            NotifyPropertyChanged(() => AdminAuth);
            NotifyPropertyChanged(() => UnknownUser);
        }

        public UserViewModel Copy()
        {
            return new UserViewModel()
            {
                Name = Name,
                LastName = LastName, 
                Birthday = Birthday,
                Email = Email,
                Password = Password,
                Role = Role,
                IsAdmin = IsAdmin,
            };
        }

        public Person ToPerson()
        {
            return new Person()
            {
                Name = Name,
                SecondName = LastName,
                BirthDay = Birthday.ToString(),
                IsAdmin = IsAdmin,
                Email = Email,
                Password = Password,
            };
        }
    }
}
