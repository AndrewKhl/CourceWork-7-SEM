using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class Guest : Person
    {
        public string ReservationsStr { get; set; }

        public string OrdersStr { get; set; }

        public List<int> Reservations { get; set; }

        public List<int> Orders { get; set; }

        public void AddReservation(int id)
        {
            SetReservationCollection();

            if (!Reservations.Contains(id))
            {
                Reservations.Add(id);
                ReservationsStr = StringHelper.AddIdItem(ReservationsStr, id);
            }
        }

        public void RemoveReservation(int id)
        {
            SetReservationCollection();

            if (Reservations.Contains(id))
            {
                Reservations.Remove(id);
                ReservationsStr = StringHelper.JoinString(Reservations);
            }
        }

        public void AddOrders(int id)
        {
            SetOrdersCollection();

            if (!Orders.Contains(id))
            {
                Orders.Add(id);
                OrdersStr = StringHelper.AddIdItem(OrdersStr, id);
            }
        }

        public void RemoveOrders(int id)
        {
            SetOrdersCollection();

            if (Orders.Contains(id))
            {
                Orders.Remove(id);
                OrdersStr = StringHelper.JoinString(Orders);
            }
        }

        public void UpdateCollections()
        {
            SetOrdersCollection();
            SetReservationCollection();
        }

        private void SetReservationCollection()
        {
            if (Reservations == null)
                Reservations = StringHelper.GetIdList(ReservationsStr) ?? new List<int>();
        }

        private void SetOrdersCollection()
        {
            if (Orders == null)
                Orders = StringHelper.GetIdList(OrdersStr) ?? new List<int>();
        }
    }
}
