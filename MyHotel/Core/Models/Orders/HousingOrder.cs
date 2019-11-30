using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class HousingOrder : Order
    {
        public string InTime { get; set; }

        public string OutTime { get; set; }
    }
}
