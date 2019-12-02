using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class GuestViewModel : UserViewModel
    {
        public GuestViewModel() { }

        public GuestViewModel(Guest user)
        {
            Name = user.Name;
            LastName = user.SecondName;
            Birthday = DateTime.Parse(user.BirthDay);
            Email = user.Email;
            Password = user.Password;
        }

        public override void RefreshModel()
        {
            base.RefreshModel();
        }
    }
}
