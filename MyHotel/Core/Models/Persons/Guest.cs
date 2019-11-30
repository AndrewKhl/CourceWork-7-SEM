using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class Guest : Person
    {
        string ReservationsStr { get; set; }

        string OrdersStr { get; set; }

        List<string> Reservations => StringHelper.GetIdList(ReservationsStr);

        List<string> Orders => StringHelper.GetIdList(OrdersStr);
    }
}
