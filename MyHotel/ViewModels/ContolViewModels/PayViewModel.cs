using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class PayViewModel : BaseViewModel
    {
        private const double ProbabilityError = 0.3;
        private const int CurrentYear = 19;
        private const int MaxYear = 99;

        private string _cardNumber = "5536080010643892";
        private int _yearExpiration;
        private int _monthExpiration;
        private string _cardholderName;
        private string _cvs;

        public bool IsPaid = false;

        public ICommand BackCommand { get; set; }
        public ICommand OkCommand { get; set; }

        [DigitsLength(MinLength = 16, MaxLength = 16, ErrorMessage = "Card Number should have length = 16")]
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                NotifyPropertyChanged(() => CardNumber);
            }
        }

        [Range(CurrentYear, MaxYear, ErrorMessage = "Date should be more then 18.")]
        public int YearExpiration
        {
            get => _yearExpiration;
            set
            {
                _yearExpiration = value;
                NotifyPropertyChanged(() => YearExpiration);
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

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Cardholder name should be in format 'Name Lastname'")]
        public string CardholderName
        {
            get => _cardholderName;
            set
            {
                _cardholderName = value;
                NotifyPropertyChanged(() => CardholderName);
            }
        }

        [DigitsLength(MinLength = 3, MaxLength = 4, ErrorMessage = "Csv should have 3 or 4 digits.")]
        public string Cvs
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
            try
            {
                AlghoritmLuna();

                var rand = new Random(DateTime.Now.Millisecond);

                if (rand.NextDouble() < ProbabilityError)
                    throw new Exception("Not enough money in the card account!");

                IsPaid = true;

                SetClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackCommandDelegate(object o)
        {
            SetClose();
        }

        private void AlghoritmLuna()
        {
            var card = new string(_cardNumber.Reverse().ToArray());

            int ans = 0;

            for (int i = 0; i < card.Length; ++i)
                if ((i + 1) % 2 == 0)
                {
                    int n = card[i] - '0';
                    n *= 2;

                    ans += n > 9 ? (n / 10) + (n % 10) : n;
                }
                else
                    ans += card[i] - '0';

            if (ans % 10 != 0)
                throw new Exception("Card number is invalid!");
        }
    }
}
