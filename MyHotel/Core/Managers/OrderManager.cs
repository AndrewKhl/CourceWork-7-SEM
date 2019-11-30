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

        public async Task AddHouseOrder(HousingOrder order)
        {
            HousingOrders.Add(order);

            await SaveChangesAsync();
        }

        public async Task RemoveHouseOrder(int id)
        {
            var order = TryFindHouseOrder(id);

            if (order == null)
                return;

            HousingOrders.Remove(order);

            await SaveChangesAsync();
        }


        public HousingOrder TryFindHouseOrder(int id) => HousingOrders.Where(u => u.Id == id).FirstOrDefault();
        
        #endregion


        #region Service orders

        public DbSet<ServiceOrder> ServiceOrders { get; set; }

        public async Task AddServiceOrder(ServiceOrder order)
        {
            ServiceOrders.Add(order);

            await SaveChangesAsync();
        }

        public async Task RemoveServiceOrder(int id)
        {
            var order = TryFindServiceOrder(id);

            if (order == null)
                return;

            ServiceOrders.Remove(order);

            await SaveChangesAsync();
        }


        public ServiceOrder TryFindServiceOrder(int id) => ServiceOrders.Where(u => u.Id == id).FirstOrDefault();

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
