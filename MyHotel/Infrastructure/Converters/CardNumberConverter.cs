using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyHotel
{
    public class CardNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var cardNumber = value as string;

            if (string.IsNullOrEmpty(cardNumber))
                return value;

            var converCardNumber = new StringBuilder();
            for (int i = 0; i < cardNumber.Length; i += 4)
                converCardNumber.Append($"{cardNumber.Substring(i, (cardNumber.Length - i - 1 >= 4 ? 4 : cardNumber.Length - i))} ");

            return converCardNumber.ToString().Trim();
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as string).Replace(" ", string.Empty);
        }
    }
}
