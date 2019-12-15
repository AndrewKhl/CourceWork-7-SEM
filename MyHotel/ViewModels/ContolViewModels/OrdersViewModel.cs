using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class OrdersViewModel : BaseViewModel
    {
        private OrderViewModel _selectedOrder;
        private OrderViewModel _selectedService;
        private bool _isServiceSelected;
        private string _guetString;

        public ICommand DeleteServiceCommand { get;set;}

        public ObservableCollection<OrderViewModel> Orders { get; set; }

        public ObservableCollection<OrderViewModel> Services { get; set; }

        public OrderViewModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                NotifyPropertyChanged(() => SelectedOrder);

                if (SelectedOrder == null)
                    return;

                SelectedOrder.RefreshModel();
                SelectedService = null;
            }
        }

        public OrderViewModel SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                NotifyPropertyChanged(() => SelectedService);

                if (SelectedService == null)
                {
                    IsServiceSelected = false;
                    return;
                }

                SelectedService.RefreshModel();
                IsServiceSelected = true;
                SelectedOrder = null;
            }
        }

        public bool IsServiceSelected
        {
            get => _isServiceSelected;
            set
            {
                _isServiceSelected = value;
                NotifyPropertyChanged(() => IsServiceSelected);

                if (value)
                {
                    Service = new ServiceItemViewModel(CoreManager.OrderManager.TryFindServices(SelectedService.ServiceId));
                }
                NotifyPropertyChanged(() => ServiceStr);
            }
        }

        public string ServiceStr => $"Service: {(Service?.ShortDescription)}";

        public ServiceItemViewModel Service { get; set; }

        public string GuestString
        {
            get => _guetString;
            set
            {
                _guetString = value;
                NotifyPropertyChanged(() => GuestString);
            }
        }

        public OrdersViewModel(IShellViewModel shell) : base(shell)
        {
            DeleteServiceCommand = new DelegateCommand(DeleteCommandDelegate, CanDeleteCOmmandDelegate);

            Orders = new ObservableCollection<OrderViewModel>();
            Services = new ObservableCollection<OrderViewModel>();

            var orders = CoreManager.OrderManager.HousingOrders;
            foreach (var order in orders)
            { 
                Orders.Add(new OrderViewModel(order, null, new List<ServiceOrderViewModel>()));
                Orders.Last().CheckIn = DateTime.Parse(order.InTime);
                Orders.Last().CheckOut = DateTime.Parse(order.OutTime);
            }

            SelectedOrder = Orders.First();

            var serv = CoreManager.OrderManager.ServiceOrders;
            foreach (var order in serv)
            {
                Services.Add(new OrderViewModel(order, null, new List<ServiceOrderViewModel>()));
                Services.Last().StartTime = DateTime.Parse(order.StartTime);
                Services.Last().ServiceId = order.ServiceId;
            }
        }

        private bool CanDeleteCOmmandDelegate(object o)
        {
            return SelectedService != null || SelectedOrder != null;
        }

        private void DeleteCommandDelegate(object o)
        {
            if (IsServiceSelected)
            {
                CoreManager.removeServiceOrder(SelectedService.Id, SelectedService.GuestId);
                Services.Remove(SelectedService);
                SelectedService = null;
                return;
            }

            CoreManager.RemoveReservedOrder(SelectedOrder.Id, SelectedOrder.GuestId);
            Orders.Remove(SelectedOrder);
            SelectedOrder = null;
        }
    }
}
