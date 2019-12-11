using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    class DigitsLengthAttribute : ValidationAttribute
    { 
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public override bool IsValid(object value)
        {
            string str = value as string;
            if (string.IsNullOrEmpty(str))
                return false;

            if (!(str.Length >= MinLength && str.Length <= MaxLength))
                return false;

            return long.TryParse(str, out _);
        }
    }
}
