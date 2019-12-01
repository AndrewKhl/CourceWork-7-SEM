using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyHotel
{
    public class BindingProp : Binding
    {
        public BindingProp() : base()
        {
            SettingsOn();
        }

        public BindingProp(string path) : base(path)
        {
            SettingsOn();
        }

        private void SettingsOn()
        {
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            ValidatesOnDataErrors = true;
            ValidatesOnExceptions = true;
            ValidatesOnNotifyDataErrors = true;
        }
    }
}
