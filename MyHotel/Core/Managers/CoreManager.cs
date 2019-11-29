using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class CoreManager : IDisposable
    {
        public DataBaseManager DataBase { get; }

        public CoreManager()
        {
            DataBase = new DataBaseManager();
        }

        public void Dispose()
        {
            DataBase.CloseConnection();
        }
    }
}
