using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class Room
    {
        public int Id { get; set; }

        public int Floor { get; set; }

        public string RoomStaffStr { get; set; }

        public string Descriptions { get; set; }

        List<string> RoomStaff { get; set; }
    }
}
