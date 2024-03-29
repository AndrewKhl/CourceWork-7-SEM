﻿using MyHotel.Core;
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
    public class ReservationDialogViewModel : BaseViewModel
    {
        private DateTime _checkIn;
        private DateTime _checkOut;
        private string _email;
        private string _name;
        private string _reservationComment;
        private bool _isContactInfoReadOnly;
        private int _cost;
        private PaymentTypesEnum _selectedPayment;
        private string _errorText;
        private int _roomId;

        public ICommand ReserveCommand { get; set; }

        public enum PaymentTypesEnum
        {
            Cash,
            Card,
        }

        public List<PaymentTypesEnum> PaymentTypes =>
            Enum.GetValues(typeof(PaymentTypesEnum)).Cast<PaymentTypesEnum>().ToList();

        public DateTime CheckIn
        {
            get => _checkIn;
            set
            {
                _checkIn = value;
                NotifyPropertyChanged(() => CheckIn);
                NotifyPropertyChanged(() => CostInfo);
            }
        }

        public DateTime CheckOut
        {
            get => _checkOut;
            set
            {
                _checkOut = value;
                NotifyPropertyChanged(() => CheckOut);
                NotifyPropertyChanged(() => CostInfo);
            }
        }

        [Required(ErrorMessage = "Field 'Name' is required")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(() => Name);
            }
        }

        [CustomEmailAddress(ErrorMessage = "Incorrect Email address")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChanged(() => Email);

                ErrorText = null;
            }
        }

        public string ReservationComment
        {
            get => _reservationComment;
            set
            {
                _reservationComment = value;
                NotifyPropertyChanged(() => ReservationComment);
            }
        }

        public bool IsContactInfoReadOnly
        {
            get => _isContactInfoReadOnly;
            set
            {
                _isContactInfoReadOnly = value;
                NotifyPropertyChanged(() => IsContactInfoReadOnly);
            }
        }

        public PaymentTypesEnum SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged(() => SelectedPayment);
            }
        }

        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                NotifyPropertyChanged(() => ErrorText);
            }
        }

        public string NumberStr { get; set; }

        public double FinishCost => (CheckOut - CheckIn).TotalDays < 0 ? 0 : (CheckOut - CheckIn).TotalDays * _cost;

        public string CostInfo => $"Finish cost: {FinishCost}$";

        public ReservationDialogViewModel(IShellViewModel shellViewModel, int roomId) : base(shellViewModel)
        {
            _roomId = roomId;
            ReserveCommand = new DelegateCommand(ReserveCommandDelegate, CanReserveCommandDelegate);

            LoadData();
        }

        public override void SetClose()
        {
            base.SetClose();
        }

        private void LoadData()
        {
            _cost = _shell.RoomsControlViewModel.SelectRoom.Cost;
            NumberStr = _shell.RoomsControlViewModel.SelectRoom.NumberStr;

            CheckIn = _shell.CheckIn;
            CheckOut = _shell.CheckOut;

            SelectedPayment = PaymentTypes.FirstOrDefault();

            if (!string.IsNullOrEmpty(_shell.CurrentUser.Email))
            {
                Name = _shell.CurrentUser.Name;
                Email = _shell.CurrentUser.Email;
                IsContactInfoReadOnly = true;
                return;
            }

            IsContactInfoReadOnly = false;
        }

        private bool CanReserveCommandDelegate(object o)
        {
            if (CheckIn.Date < DateTime.Today)
                return false;

            var freeRooms = CoreManager.OrderManager.GetFreeRoomsId(CheckIn, CheckOut);
            if (!freeRooms.Contains(_shell.RoomsControlViewModel.SelectRoom.Id))
                return false;

            return CheckIn <= CheckOut && !IsError;
        }

        private void ReserveCommandDelegate(object o)
        {
            var order = new HousingOrder()
            {
                RoomId = _roomId,
                CreateTime = DateTime.Now.ToString(),
                InTime = CheckIn.ToString(),
                OutTime = CheckOut.ToString(),
                Cost = (int)FinishCost,
                Comment = ReservationComment,
            };

            if (SelectedPayment == PaymentTypesEnum.Card)
            {
                var viewModel = new PayViewModel(_shell);

                var dialog = new PayDialog()
                {
                    DataContext = viewModel,
                    Owner = Application.Current.MainWindow,
                };
                dialog.ShowDialog();

                order.IsPaid = viewModel.IsPaid;
                if (!order.IsPaid)
                    return;

            }

            int userId = CurrentUser.Id;

            if (userId == -1)
            {
                var newGuest = new Core.Guest()
                {
                    Name = Name,
                    Email = Email,
                };

                var guest = CoreManager.UserManager.AddGuest(newGuest);
                if (guest == null)
                {
                    ErrorText = $"User with email '{Email}' is exists";
                    return;
                }

                CurrentUser.AttachModel(guest);
                userId = guest.Id;
            }

            order.UserId = userId;
            CoreManager.AddReservedOrder(order);
            CoreManager.MailManager.SendReservation(CurrentUser.ToPerson(), order);
            SetClose();
        }
    }
}
