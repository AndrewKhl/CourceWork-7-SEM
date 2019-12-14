using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class ServicesViewModel : BaseViewModel
    {
        private ServiceItemViewModel _selectedService;
        private ServiceItemViewModel _editableService;

        public ICommand AddServiceCommand { get; set; }
        public ICommand EditSeviceCommand { get; set; }
        public ICommand DeleteServiceCommand { get; set; }

        public ObservableCollection<ServiceItemViewModel> Services { get; set; }

        public ServiceItemViewModel SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                NotifyPropertyChanged(() => SelectedService);

                EditableService = new ServiceItemViewModel(SelectedService?.Model);
            }
        }

        public ServiceItemViewModel EditableService
        {
            get => _editableService;
            set
            {
                _editableService = value;
                NotifyPropertyChanged(() => EditableService);
            }
        }

        public ServicesViewModel(IShellViewModel shell) : base(shell)
        {
            AddServiceCommand = new DelegateCommand(AddServiceCommandDelegate, CanAddServiceCommandDelegate);
            EditSeviceCommand = new DelegateCommand(EditServiceCommandDelegate, CanEditServiceCommandDelegate);
            DeleteServiceCommand = new DelegateCommand(DeleteCommandDelegate, CanDeleteCOmmandDelegate);

            var services = CoreManager.OrderManager.Services.AsEnumerable();
            Services = new ObservableCollection<ServiceItemViewModel>(services.Select(s => new ServiceItemViewModel(s)));

            EditableService = new ServiceItemViewModel(null);
        }

        private bool IsEqual()
        {
            return EditableService.Cost == SelectedService?.Cost
                && EditableService.ShortDescription == SelectedService?.ShortDescription
                && EditableService.Description == SelectedService?.Description;
        }

        private bool CanAddServiceCommandDelegate(object o)
        {
            return EditableService != null && !IsEqual() && !EditableService.IsError;
        }

        private void AddServiceCommandDelegate(object o)
        {
            var service = CoreManager.OrderManager.AddService(EditableService.ToService());

            Services.Add(new ServiceItemViewModel(service));
            SelectedService = Services.Last();
        }

        private bool CanEditServiceCommandDelegate(object o)
        {
            return EditableService != null && SelectedService != null && !IsEqual() && !EditableService.IsError;
        }

        private void EditServiceCommandDelegate(object o)
        {
            CoreManager.OrderManager.ModifyService(EditableService.ToService());

            var index = Services.IndexOf(SelectedService);
            Services.Insert(index + 1, EditableService);
            Services.RemoveAt(index);

            SelectedService = Services[index];
            SelectedService.RefreshModel();
        }

        private bool CanDeleteCOmmandDelegate(object o)
        {
            return EditableService != null && IsEqual();
        }

        private void DeleteCommandDelegate(object o)
        {
            CoreManager.OrderManager.RemoveService(SelectedService.Id);

            Services.Remove(SelectedService);
            SelectedService = null;
        }
    }
}
