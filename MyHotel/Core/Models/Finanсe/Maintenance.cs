using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class Maintenance
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public int Cost { get; set; }

        public string Comment { get; set; }

        public string Date { get; set; }
    }
}
