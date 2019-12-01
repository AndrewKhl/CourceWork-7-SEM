using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class PayViewModel : BaseViewModel
    {
        private long _cardNumber;
        private int _dateExpiration;
        private int _monthExpiration;
        private string _cardholderName;
        private int _cvs;

        public long CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                NotifyPropertyChanged(() => CardNumber);
            }
        }

        public int DateExpiration
        {
            get => _dateExpiration;
            set
            {
                _dateExpiration = value;
                NotifyPropertyChanged(() => DateExpiration);
            }
        }

        public int MonthExpiration
        {
            get => _monthExpiration;
            set
            {
                _monthExpiration = value;
                NotifyPropertyChanged(() => MonthExpiration);
            }
        }

        public string CardholderName
        {
            get => _cardholderName;
            set
            {
                _cardholderName = value;
                NotifyPropertyChanged(() => CardholderName);
            }
        }

        public int Cvs
        {
            get => _cvs;
            set
            {
                _cvs = value;
                NotifyPropertyChanged(() => Cvs);
            }
        }

        public PayViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {

        }
    }
}
