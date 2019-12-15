using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class GuestViewModel : UserViewModel
    {
        public ObservableCollection<OrderViewModel> Orders { get; set; }

        public GuestViewModel() { }

        public GuestViewModel(Guest user, List<OrderViewModel> orders) : base(user)
        {
            Orders = new ObservableCollection<OrderViewModel>(orders);
        }

        public override void RefreshModel()
        {
            base.RefreshModel();
        }
    }
}
