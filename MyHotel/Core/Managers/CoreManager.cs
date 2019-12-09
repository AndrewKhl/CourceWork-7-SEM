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
            UserManager = new UserManager();
            RoomManager = new RoomManager();
            OrderManager = new OrderManager();

            MailManager = new MailManager();

            _managers = new List<IModelManager>()
            {
                UserManager,
                RoomManager,
                OrderManager
            };

            //InitialDataBase();
        }

        public UserManager UserManager { get; }

        public RoomManager RoomManager { get; }

        public OrderManager OrderManager { get; }

        public MailManager MailManager { get; }

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
