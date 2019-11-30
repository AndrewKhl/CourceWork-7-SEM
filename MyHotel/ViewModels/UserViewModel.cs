using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class UserViewModel : ValidationObservableModel
    {
        private string _name;
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

        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                NotifyPropertyChanged(() => IsAdmin);
            }
        }


        Person _model;
        public UserViewModel(Person user)
        {
            Name = user.Name;
            IsAdmin = user.IsAdmin;

            RefreshModel();
        }

        public void RefreshModel()
        {
            NotifyPropertyChanged(() => Name);
            NotifyPropertyChanged(() => IsAdmin);
        }
    }
}
