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
            if (_model == null)
                return;

            _model = service;
            Cost = service.Cost;

            var descr = service.Description.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            ShortDescription = descr[0];
            Description = descr[1];            
        }
    }
}
