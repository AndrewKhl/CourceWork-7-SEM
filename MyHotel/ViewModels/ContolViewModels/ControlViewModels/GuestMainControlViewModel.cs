﻿using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class GuestMainControlViewModel : BaseViewModel
    {
        private DateTime _checkIn;
        private DateTime _checkOut;
        private GuestProfileViewModel _profileViewModel;
        private RoomsControlViewModel _roomsViewModel;

        public ICommand LogoutCommand { get; set; }
        public ICommand SearchFreeRooms { get; set; }
        public ICommand ShowProfileCommand { get; set; }
        public ICommand ShowRoomsCommand { get; set; }

        public bool IsProfileVisible { get; set;}
        public bool IsRoomVisible { get; set; }

        public GuestProfileViewModel ProfileViewModel
        {
            get => _profileViewModel;
            set
            {
                _profileViewModel = value;
                NotifyPropertyChanged(() => ProfileViewModel);
            }
        }

        public DateTime CheckIn
        {
            get => _checkIn;
            set
            {
                _checkIn = value;
                NotifyPropertyChanged(() => CheckIn);

                _shell.CheckIn = value;
            }
        }

        public DateTime CheckOut
        {
            get => _checkOut;
            set
            {
                _checkOut = value;
                NotifyPropertyChanged(() => CheckOut);

                _shell.CheckOut = value;
            }
        }

        public GuestMainControlViewModel(IShellViewModel shell, RoomsControlViewModel rvm) : base(shell)
        {
            _roomsViewModel = rvm;

            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
            SearchFreeRooms = new DelegateCommand(SearchFreeRoomsDelegate, CanSearchFreeRoomsDelegate);
            ShowProfileCommand = new DelegateCommand(ShowProfileCommandDelegate);
            ShowRoomsCommand = new DelegateCommand(ShowRoomsCommandDelegate);

            CheckIn = DateTime.Today;
            CheckOut = DateTime.Today.AddDays(1);

            VisualViewModel(false, true);
        }

        public override void SetClose()
        {
            LogoutCommand = null;
            SearchFreeRooms = null;
            ShowProfileCommand = null;

            base.SetClose();
        }

        private void LogoutCommandDelegate(object o)
        {
            RoomsControlViewModel.RefreshAllRooms();
            CurrentUser.AttachModel(null);
        }

        private bool CanSearchFreeRoomsDelegate(object o)
        {
            if (CheckIn.Date < DateTime.Today)
                return false;

            return CheckIn <= CheckOut;
        }

        private void SearchFreeRoomsDelegate(object o)
        {
            _roomsViewModel.SetFreeRooms(_shell.CoreManager.OrderManager.GetFreeRoomsId(CheckIn, CheckOut));
        }

        private void ShowProfileCommandDelegate(object o)
        {
            var currentGuest = CoreManager.UserManager.TryFindGuests(CurrentUser.Email);

            ProfileViewModel = new GuestProfileViewModel(_shell, currentGuest);

            VisualViewModel(true, false);
        }

        private void ShowRoomsCommandDelegate(object o)
        {
            VisualViewModel(false, true);
        }

        private void VisualViewModel(bool isProfile, bool isRooms)
        {
            IsProfileVisible = isProfile;
            IsRoomVisible = isRooms;

            NotifyPropertyChanged(() => IsRoomVisible);
            NotifyPropertyChanged(() => IsProfileVisible);
        }
    }
}
