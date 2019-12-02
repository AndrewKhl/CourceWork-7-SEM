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

        public bool UserAuth => _model != null;

        public UserViewModel()
        {

        }

        public void AttachModel(Person user)
        {
            _model = user;
            Name = user?.Name;
            LastName = user?.SecondName;
            Email = user?.Email;
            Password = user?.Password;
            if (DateTime.TryParse(user?.BirthDay, out var birthday))
                Birthday = birthday;
            IsAdmin = user?.IsAdmin ?? false;

            RefreshModel();
        }

        public virtual void RefreshModel()
        {
            NotifyPropertyChanged(() => Name);
            NotifyPropertyChanged(() => Email);
            NotifyPropertyChanged(() => IsAdmin);
            NotifyPropertyChanged(() => UserAuth);
        }
    }
}
