using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class GuestProfileViewModel : BaseViewModel
    {
        private Core.Guest _currentGuest;

        public ICommand EditProfileCommand { get; set; }
        public ICommand EditServicesCommand { get; set; }

        public GuestViewModel Guest { get; set; }

        public GuestProfileViewModel(IShellViewModel shellViewModel, Core.Guest guest) 
            : base(shellViewModel)
        {
            _currentGuest = guest;

            var serviceIds = guest.GetServices();
            var services = new Dictionary<int, List<ServiceOrderViewModel>>();
            foreach (var serviceId in serviceIds)
            {
                var serviceOrder = CoreManager.OrderManager.TryFindServiceOrder(serviceId);
                var service = CoreManager.OrderManager.TryFindServices(serviceOrder.ServiceId);

                if (!services.ContainsKey(serviceOrder.RoomId))
                    services[serviceOrder.RoomId] = new List<ServiceOrderViewModel>();
                services[serviceOrder.RoomId].Add(new ServiceOrderViewModel(serviceOrder, service));
            }

            var orders = new List<OrderViewModel>();
            foreach (var orderId in guest.GetReservations())
            {
                var order = CoreManager.OrderManager.TryFindHouseOrder(orderId);
                var room = new LivingRoomViewModel(CoreManager.RoomManager.TryFindRoom(order.RoomId), _shell);

                var orderViewModel = new OrderViewModel(order, room, 
                    services.ContainsKey(order.RoomId) ? services[order.RoomId] : new List<ServiceOrderViewModel>());
                orderViewModel.CheckIn = DateTime.Parse(order.InTime);
                orderViewModel.CheckOut = DateTime.Parse(order.OutTime);

                orders.Add(orderViewModel);
            }

            Guest = new GuestViewModel(guest, orders);
            Guest.RefreshModel();

            EditProfileCommand = new DelegateCommand(EditProfileCommandDelegate, CanEditProfileCommandDelegate);
            EditServicesCommand = new DelegateCommand(EditServicesCommandDelegate);
        }

        public override void SetClose()
        {
            EditProfileCommand = null;

            base.SetClose();
        }

        private bool IsEqual()
        {
            var equal = true;
            equal &= _currentGuest.Name == Guest.Name;
            equal &= _currentGuest.SecondName == Guest.LastName;
            equal &= DateTime.Parse(_currentGuest.BirthDay) == Guest.Birthday;

            return equal;
        }

        private bool CanEditProfileCommandDelegate(object o)
        {
            return !Guest.IsError && !IsEqual();
        }

        private void EditProfileCommandDelegate(object o)
        {

        }

        private void EditServicesCommandDelegate(object o)
        {
            var order = o as OrderViewModel;
            if (order == null)
                return;

            var model = new RoomServicesViewModel(_shell, order);
            var dialog = new RoomServicesDialog()
            {
                DataContext = model,
                Owner = Application.Current.MainWindow,
            };
            dialog.ShowDialog();

            NotifyPropertyChanged(() => order.ServicesStr);
        }
    }
}
