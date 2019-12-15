using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class OrderViewModel : ValidationObservableModel
    {
        private Order _order;
        private LivingRoomViewModel _room;
        private string _comment;
        private bool _isPaid;
        private int _cost;
        private DateTime _checkIn;
        private DateTime _checkOut;
        private DateTime _startTime;
        private ServiceOrderViewModel _delectedService;
        private int _roomId;
        private int _guestId;

        public int Id => _order?.Id ?? -1;

        public LivingRoomViewModel Room
        {
            get => _room;
            set
            {
                _room = value;
                NotifyPropertyChanged(() => Room);
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                NotifyPropertyChanged(() => Comment);
            }
        }

        public bool IsPaid
        {
            get => _isPaid;
            set
            {
                _isPaid = value;
                NotifyPropertyChanged(() => IsPaid);
            }
        }

        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(() => Cost);
            }
        }

        public DateTime CheckIn
        {
            get => _checkIn;
            set
            {
                _checkIn = value;
                NotifyPropertyChanged(() => CheckIn);
                NotifyPropertyChanged(() => CheckInStr);
            }
        }

        public DateTime CheckOut
        {
            get => _checkOut;
            set
            {
                _checkOut = value;
                NotifyPropertyChanged(() => CheckOut);
                NotifyPropertyChanged(() => CheckOutStr);
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                NotifyPropertyChanged(() => StartTime);
            }
        }

        public int ServiceId { get; set; }

        public ObservableCollection<ServiceOrderViewModel> Services { get; set; }

        public ServiceOrderViewModel SelectedService
        {
            get => _delectedService;
            set
            {
                _delectedService = value;
                NotifyPropertyChanged(() => SelectedService);
            }
        }

        public int RoomId
        {
            get => _roomId;
            set
            {
                _roomId = value;
                NotifyPropertyChanged(() => RoomId);
            }
        }

        public int GuestId
        {
            get => _guestId;
            set
            {
                _guestId = value;
                NotifyPropertyChanged(() => GuestId);
            }
        }

        public string CostStr => $"{Cost}$";

        public string IsPaidStr => IsPaid ? "Yes" : "No";

        public string CheckInStr => $"{CheckIn.ToShortDateString()} 14:00";

        public string CheckOutStr => $"{CheckOut.ToShortDateString()} 12:00";

        public string ServicesStr => Services.Count == 0 ? "Nothing"
            : string.Join(", ", Services.Select(s => s.ShortDescription));

        public string StartTimeStr => $"{StartTime.ToShortDateString()}";

        public string RoomNumberStr => $"Room №{RoomId}";

        public string CheckInString => $"Check In: {CheckInStr}";

        public string CheckOutString => $"Check Out: {CheckOutStr}";

        public string StartTimeString => $"Start Time: {StartTimeStr}";

        public string CostString => $"Cost: {CostStr}";

        public string IsPaidString => $"Paid: {IsPaidStr}";

        public OrderViewModel(Order order, LivingRoomViewModel room, List<ServiceOrderViewModel> services)
        {           
            _order = order;
            Comment = order.Comment;
            IsPaid = order.IsPaid;
            Cost = order.Cost;
            RoomId = order.RoomId;
            GuestId = order.UserId;
            Room = room;
            Services = new ObservableCollection<ServiceOrderViewModel>(services);
        }

        public void RefreshModel()
        {
            NotifyPropertyChanged(() => CheckInString);
            NotifyPropertyChanged(() => CheckOutString);
            NotifyPropertyChanged(() => StartTimeString);
            NotifyPropertyChanged(() => IsPaidString);
            NotifyPropertyChanged(() => CostString);
        }
    }
}
