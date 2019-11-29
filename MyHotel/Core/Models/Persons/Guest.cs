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

        List<string> Reservations => GetIdList(ReservationsStr);

        List<string> Orders => GetIdList(OrdersStr);


        public void AddReservation(int id)
        {
            ReservationsStr += string.IsNullOrEmpty(ReservationsStr) ? $"{id}" : $",{id}";
        }

        private List<string> GetIdList(string str) => str?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()).ToList();
    }
}
