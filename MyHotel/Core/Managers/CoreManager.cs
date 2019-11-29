using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class CoreManager : IDisposable
    {
        private readonly DataBaseManager _dbContext;

        public CoreManager()
        {
            _dbContext = new DataBaseManager();
        }

        public void Dispose()
        {
            _dbContext.CloseConnection();
        }
    }
}
