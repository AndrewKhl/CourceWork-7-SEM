using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class ServiceOrder : Order
    {
        public int ServiceId { get; set; }

        public string StartTime { get; set; }
    }
}
