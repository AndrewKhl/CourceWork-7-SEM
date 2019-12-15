using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static MyHotel.ReservationDialogViewModel;

namespace MyHotel
{
    public class RoomServicesViewModel : BaseViewModel
    {
        private ServiceItemViewModel _selectedService;
        private ServiceOrderViewModel _newService;

        public List<PaymentTypesEnum> PaymentTypes =>
            Enum.GetValues(typeof(PaymentTypesEnum)).Cast<PaymentTypesEnum>().ToList();

        public ICommand AddServiceCommand { get; set; }
        public ICommand DeleteServiceCommand { get; set; }

        public ObservableCollection<ServiceItemViewModel> Services { get; set; }

        public OrderViewModel Order { get; set; }

        public ServiceItemViewModel SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                NotifyPropertyChanged(() => SelectedService);
            }
        }

        public ServiceOrderViewModel NewService
        {
            get => _newService;
            set
            {
                _newService = value;
                NotifyPropertyChanged(() => NewService);
            }
        }

        public RoomServicesViewModel(IShellViewModel shell, OrderViewModel order) : base(shell)
        {
            AddServiceCommand = new DelegateCommand(AddServiceCommandDelegate, CanAddServiceCommandDelegate);
            DeleteServiceCommand = new DelegateCommand(DeleteCommandDelegate, CanDeleteCOmmandDelegate);

            var services = CoreManager.OrderManager.Services.AsEnumerable();
            Services = new ObservableCollection<ServiceItemViewModel>(services.Select(s => new ServiceItemViewModel(s)));
            SelectedService = Services.First();

            Order = order;
            NewService = new ServiceOrderViewModel(null, null) 
            { 
                StartTime = Order.CheckIn,
            };
        }

        private bool CanAddServiceCommandDelegate(object o)
        {
            return NewService.StartTime >= Order.CheckIn &&
                NewService.StartTime <= Order.CheckOut;
        }

        private void AddServiceCommandDelegate(object o)
        {
            var order = new Core.ServiceOrder()
            {
                StartTime = NewService.StartTime.ToString(),
                UserId = CurrentUser.Id,
                RoomId = Order.Room.Id,
                Comment = NewService.Comment,
                Cost = SelectedService.Cost,
                ServiceId = SelectedService.Id,
            };

            if (NewService.SelectedPayment == PaymentTypesEnum.Card)
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

            var serviceId = CoreManager.AddServiceOrder(order);

            Order.Services.Add(new ServiceOrderViewModel(CoreManager.OrderManager.TryFindServiceOrder(serviceId),
                Services.FirstOrDefault(s => s.Id == SelectedService.Id).Model));
            NotifyPropertyChanged(() => Order.ServicesStr);
        }

        private bool CanDeleteCOmmandDelegate(object o)
        {
            return Order.SelectedService != null;
        }

        private void DeleteCommandDelegate(object o)
        {
            CoreManager.RemoveServiceOrder(Order.SelectedService.ServiceOrderId, CurrentUser.Id);

            Order.Services.Remove(Order.SelectedService);
            NotifyPropertyChanged(() => Order.ServicesStr);
        }
    }
}
