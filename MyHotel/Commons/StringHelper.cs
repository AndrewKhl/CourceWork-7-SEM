using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public static class StringHelper
    {
        public static List<int> GetIdList(string str) => str?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(u => int.Parse(u.Trim())).ToList();

        public static List<string> GetStrList(string str) => str?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim()).ToList();

        public static string AddIdItem(string basic, int id) => basic + (string.IsNullOrEmpty(basic) ? $"{id}" : $",{id}");

        public static string AddIdItem(string basic, string id) => basic + (string.IsNullOrEmpty(basic) ? $"{id}" : $",{id}");

        public static string JoinString<T>(List<T> list) => string.Join("," , list);
    }
}
