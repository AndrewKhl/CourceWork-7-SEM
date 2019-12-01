using System;
using System.Collections.Generic;
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
        private bool _isAdmin;

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
            Email = user?.Email;
            IsAdmin = user?.IsAdmin ?? false;

            RefreshModel();
        }

        public void RefreshModel()
        {
            NotifyPropertyChanged(() => Name);
            NotifyPropertyChanged(() => Email);
            NotifyPropertyChanged(() => IsAdmin);
            NotifyPropertyChanged(() => UserAuth);
        }
    }
}
