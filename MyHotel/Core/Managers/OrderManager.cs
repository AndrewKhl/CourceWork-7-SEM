using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class OrderManager : DbContext, IModelManager
    {
        public OrderManager() : base("DbConnection")
        {
        }

        #region Housing orders

        public DbSet<HousingOrder> HousingOrders { get; set; }

        public void AddHouseOrder(HousingOrder order)
        {
            HousingOrders.Add(order);

            SaveChanges();

            if (order.IsPaid)
                AddPayments(order);
        }

        public void RemoveHouseOrder(int id)
        {
            var order = TryFindHouseOrder(id);

            if (order == null)
                return;

            HousingOrders.Remove(order);

            SaveChangesAsync();
        }


        public HousingOrder TryFindHouseOrder(int id) => HousingOrders.Where(u => u.Id == id).FirstOrDefault();
        
        #endregion


        #region Service orders

        public DbSet<ServiceOrder> ServiceOrders { get; set; }

        public void AddServiceOrder(ServiceOrder order)
        {
            ServiceOrders.Add(order);

            if (order.IsPaid)
                AddPayments(order);

            SaveChangesAsync();
        }

        public void RemoveServiceOrder(int id)
        {
            var order = TryFindServiceOrder(id);

            if (order == null)
                return;

            ServiceOrders.Remove(order);

            SaveChangesAsync();
        }


        public ServiceOrder TryFindServiceOrder(int id) => ServiceOrders.Where(u => u.Id == id).FirstOrDefault();

        #endregion

        #region Payments
        public DbSet<Payment> Payments { get; set; }

        public void AddPayments(Order order)
        {
            var payment = new Payment()
            {
                IsHousingOrder = order is HousingOrder,
                OrderId = order.Id,
                Cost = order.Cost,
                CreateTime = DateTime.Now.ToString(),
            };

            Payments.Add(payment);
            SaveChanges();
        }

        #endregion

        private void UpdatePayments()
        {
            foreach (var h in HousingOrders)
                if (!h.IsPaid)
                {

                }
        }

        public void CloseConnection()
        {
            Dispose();
        }

        public void InitialCommit()
        {
        }
    }
}
