using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class ServiceItemViewModel : ValidationObservableModel
    {
        private Service _model;
        private int _cost;
        private string _shortDescription;
        private string _description;

        public Service Model => _model;

        public int Id => _model?.Id ?? -1;

        [Required(ErrorMessage = "Cost is required.")]
        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(() => Cost);
            }
        }

        [Required(ErrorMessage = "Short Description is required.")]
        public string ShortDescription
        {
            get => _shortDescription;
            set
            {
                _shortDescription = value;
                NotifyPropertyChanged(() => ShortDescription);
            }
        }

        [Required(ErrorMessage = "Description is required.")]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged(() => Description);
            }
        }

        public ServiceItemViewModel(Service service)
        {
            if (service == null)
                return;

            _model = service;
            Cost = service.Cost;
            ShortDescription = service.Name;
            Description = service.Description;            
        }

        public Service ToService()
        {
            return new Service()
            {
                Id = Id,
                Name = ShortDescription,
                Description = Description,
                Cost = Cost,
            };
        }

        public void RefreshModel()
        {
            NotifyPropertyChanged(() => ShortDescription);
            NotifyPropertyChanged(() => Description);
            NotifyPropertyChanged(() => Cost);
        }
    }
}
