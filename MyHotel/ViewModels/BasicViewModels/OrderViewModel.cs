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
        private ServiceOrderViewModel _delectedService;

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

        public string CostStr => $"{Cost}$";

        public string IsPaidStr => IsPaid ? "Yes" : "No";

        public string CheckInStr => $"{CheckIn.ToShortDateString()} 14:00";

        public string CheckOutStr => $"{CheckOut.ToShortDateString()} 12:00";

        public string ServicesStr => Services.Count == 0 ? "Nothing"
            : string.Join(", ", Services.Select(s => s.ShortDescription));

        public OrderViewModel(Order order, LivingRoomViewModel room, List<ServiceOrderViewModel> services)
        {           
            _order = order;
            Comment = order.Comment;
            IsPaid = order.IsPaid;
            Cost = order.Cost;
            Room = room;
            Services = new ObservableCollection<ServiceOrderViewModel>(services);
        }
    }
}
