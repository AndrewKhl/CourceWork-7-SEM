using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class PayViewModel : BaseViewModel
    {
        private string _cardNumber;
        private int _dateExpiration;
        private int _monthExpiration;
        private string _cardholderName;
        private int _cvs;

        public ICommand BackCommand { get; set; }
        public ICommand OkCommand { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                NotifyPropertyChanged(() => CardNumber);
            }
        }

        [Range(1, 31, ErrorMessage = "Date should be in range [1, 31]")]
        public int DateExpiration
        {
            get => _dateExpiration;
            set
            {
                _dateExpiration = value;
                NotifyPropertyChanged(() => DateExpiration);
            }
        }

        [Range(1, 12, ErrorMessage = "Month should be in range [1, 12]")]
        public int MonthExpiration
        {
            get => _monthExpiration;
            set
            {
                _monthExpiration = value;
                NotifyPropertyChanged(() => MonthExpiration);
            }
        }

        [Required(ErrorMessage = "Cardholder Name is required")]
        public string CardholderName
        {
            get => _cardholderName;
            set
            {
                _cardholderName = value;
                NotifyPropertyChanged(() => CardholderName);
            }
        }

        [Required(ErrorMessage = "Cvs is required")]
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
            OkCommand = new DelegateCommand(OkCommandDelegate, CanOkCommandDelegate);
            BackCommand = new DelegateCommand(BackCommandDelegate);
        }

        private bool CanOkCommandDelegate(object o)
        {
            return !IsError;
        }

        private void OkCommandDelegate(object o)
        {

        }

        private void BackCommandDelegate(object o)
        {
            SetClose();
        }
    }
}
