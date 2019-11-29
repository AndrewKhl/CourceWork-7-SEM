using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class ErrorManager : PropertyChangedBase
    {
        public static int TotalErrors = 0;

        private readonly SortedSet<string> _errorProps = new SortedSet<string>();

        private readonly string ViewModelName;

        
        public ErrorManager(string viewModelName)
        {
            ViewModelName = viewModelName;
        }

        public int CountErrors => _errorProps.Count;

        public void AddError(string prop)
        {
            if (_errorProps.Add(prop))
                TotalErrors++;

            RefreshCount();
        }

        public bool RemoveError(string prop)
        {
            bool ok = _errorProps.Remove(prop);
            RefreshCount();

            return ok;
        }

        private string GetKey(string prop) => $"{ViewModelName}_{prop}";

        private void RefreshCount()
        {
            OnPropertyChanged(nameof(CountErrors));
        }
    }
}
