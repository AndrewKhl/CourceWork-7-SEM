using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public string Comment { get; set; }

        public string CreateTime { get; set; }

        public bool IsPaid { get; set; }
    }
}
