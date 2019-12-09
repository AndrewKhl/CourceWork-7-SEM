using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Commons
{
    public static class Time
    {
        public static DateTime ToTime(string str) => DateTime.ParseExact(str, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
    }
}
