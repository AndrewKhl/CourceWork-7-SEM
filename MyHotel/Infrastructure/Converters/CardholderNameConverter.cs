using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyHotel
{
    public class CardholderNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var cardholderName = value as string;

            if (string.IsNullOrEmpty(cardholderName))
                return value;

            return cardholderName.ToUpper();
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as string).ToLower();
        }
    }
}
