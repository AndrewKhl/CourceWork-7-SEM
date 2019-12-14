using MyHotel.Commons;
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
            UpdatePayments();
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
        
        public List<int> GetFreeRoomsId(DateTime tin, DateTime tout)
        {
            var ans = Enumerable.Range(1, 3).ToList();

            foreach (var h in HousingOrders)
            {
                if (Time.ToTime(h.InTime) <= tout && Time.ToTime(h.OutTime) >= tin)
                    ans.Remove(h.RoomId);
            }

            return ans;
        }

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

        private void UpdatePayments()
        {
            foreach (var h in HousingOrders)
                if (!h.IsPaid && Time.ToTime(h.OutTime) < DateTime.Now) //"10.12.2019 1:50:41"
                {
                    h.IsPaid = true;
                    AddPayments(h);
                }
            
            foreach (var s in ServiceOrders)
                if (!s.IsPaid && Time.ToTime(s.StartTime) < DateTime.Now)
                {
                    s.IsPaid = true;
                    AddPayments(s);
                }
        }

        #endregion


        #region Services

        public DbSet<Service> Services { get; set; }

        public Service AddService(Service newService)
        {
            Services.Add(newService);

            SaveChanges();

            return newService;
        }

        public void RemoveService(int id)
        {
            var service = TryFindServices(id);

            if (service == null)
                return;

            Services.Remove(service);

            SaveChangesAsync();
        }

        public void ModifyService(Service newService)
        {
            var old = Services.Where(u => u.Id == newService.Id).FirstOrDefault();

            if (old == null)
                return;

            old.Name = newService.Name;
            old.Cost = newService.Cost;
            old.Description = newService.Description;

            SaveChangesAsync();
        }

        public Service TryFindServices(int id) => Services.Where(u => u.Id == id).FirstOrDefault();

        #endregion

        public void CloseConnection()
        {
            Dispose();
        }

        public void InitialCommit()
        {
        }
    }
}
