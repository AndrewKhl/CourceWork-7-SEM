using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class CoreManager : IDisposable
    {
        private readonly List<IModelManager> _managers;

        public CoreManager()
        {
            MailManager = new MailManager();

            UserManager = new UserManager(MailManager);
            RoomManager = new RoomManager();
            OrderManager = new OrderManager();
            
            _managers = new List<IModelManager>()
            {
                UserManager,
                RoomManager,
                OrderManager
            };

            //InitialDataBase();
            OrderManager.UpdatePayments();
        }

        public UserManager UserManager { get; }

        public RoomManager RoomManager { get; }

        public OrderManager OrderManager { get; }

        public MailManager MailManager { get; }

        public void AddReservedOrder(HousingOrder order)
        {
            int id = OrderManager.AddHouseOrder(order);
            UserManager.AddReservation(order.UserId, id);
        }

        public void AddServiceOrder(ServiceOrder order)
        {
            int id = OrderManager.AddServiceOrder(order);
            UserManager.AddOrder(order.UserId, id);
        }

        public void RemoveReservedOrder(int orderId, int userId)
        {
            OrderManager.RemoveHouseOrder(orderId);
            UserManager.RemoveReservation(userId, orderId);
        }

        public void removeServiceOrder(int orderId, int userId)
        {
            OrderManager.RemoveServiceOrder(orderId);
            UserManager.RemoveOrder(userId, orderId);
        }

        private void InitialDataBase()
        {
            foreach (var m in _managers)
                m.InitialCommit();
        }

        public void Dispose()
        {
            foreach (var m in _managers)
                m.CloseConnection();
        }
    }

    public interface IModelManager : IDisposable
    {
        void InitialCommit();

        void CloseConnection();
    }
}
