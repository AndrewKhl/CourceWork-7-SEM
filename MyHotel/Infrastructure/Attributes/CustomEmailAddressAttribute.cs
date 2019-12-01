using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class CustomEmailAddressAttribute : ValidationAttribute
    {
        private readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();

        public bool AllowEmpty { get; set; }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty(value as string) && AllowEmpty)
                return true;

            if (string.IsNullOrEmpty(value as string) && !AllowEmpty)
                return false;

            return _emailAddressAttribute.IsValid(value);
        }
    }
}
