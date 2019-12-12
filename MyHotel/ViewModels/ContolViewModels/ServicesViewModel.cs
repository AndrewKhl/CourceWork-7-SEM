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

                EditableService = new ServiceItemViewModel(SelectedService.Model);
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

        }

        private bool CanEditServiceCommandDelegate(object o)
        {
            return EditableService != null && SelectedService != null && !IsEqual() && !EditableService.IsError;
        }

        private void EditServiceCommandDelegate(object o)
        {

        }

        private bool CanDeleteCOmmandDelegate(object o)
        {
            return EditableService != null && IsEqual();
        }

        private void DeleteCommandDelegate(object o)
        {

        }
    }
}
