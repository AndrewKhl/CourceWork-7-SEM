using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class GuestMainControlViewModel : BaseViewModel
    {
        public GuestMainControlViewModel(CoreManager coremanager, UserViewModel currentUser) 
            : base(coremanager, currentUser)
        {
        }

        public void RefreshModel()
        {
           
        }
    }
}
