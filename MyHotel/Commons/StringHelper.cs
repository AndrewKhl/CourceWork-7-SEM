using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public static class StringHelper
    {
        public static List<string> GetIdList(string str) => str?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()).ToList();

        public static string AddIdItem(string basic, int id) => basic + (string.IsNullOrEmpty(basic) ? $"{id}" : $",{id}");

    }
}
