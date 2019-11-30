using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class LivingRoom : Room
    {
        public bool Status { get; set; }

        public int Cost { get; set; }

        public string ServicesStr { get; set; }

        public List<string> Services { get; set; }

        public string PhotoStr { get; set; }

        public List<string> Photo { get; set; }

        public void AddService(int id)
        {
            if (Services == null)
                Services = StringHelper.GetIdList(ServicesStr) ?? new List<string>();

            if (!Services.Contains(id.ToString()))
            {
                Services.Add(id.ToString());
                ServicesStr = StringHelper.AddIdItem(ServicesStr, id);
            }
        }
    }
}
