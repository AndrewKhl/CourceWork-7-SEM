using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyHotel.ReservationDialogViewModel;

namespace MyHotel
{
    public class ServiceOrderViewModel : ServiceItemViewModel
    {
        private ServiceOrder _serviceOrder;
        private Service _service;
        private PaymentTypesEnum _selectedPayment;
        private string _comment;
        private DateTime _startTime;

        public int ServiceOrderId => _serviceOrder?.Id ?? -1;

        [Required(ErrorMessage = "Payment is required.")]
        public PaymentTypesEnum SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged(() => SelectedPayment);
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

        [Required(ErrorMessage = "Start Time is required.")]
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                NotifyPropertyChanged(() => StartTime);
            }
        }

        public ServiceOrderViewModel(ServiceOrder serviceOrder, Service service) : base(service)
        {
            _service = service;
            _serviceOrder = serviceOrder;

            SelectedPayment = PaymentTypesEnum.Cash;
            if (serviceOrder != null)
                StartTime = DateTime.Parse(_serviceOrder.StartTime);
            Comment = _serviceOrder?.Comment;

        }
    }
}
