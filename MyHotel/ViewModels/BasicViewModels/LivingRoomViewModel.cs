using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class LivingRoomViewModel : ValidationObservableModel
    {
        private LivingRoom _model;
        private int _floor;
        private int _cost;

        public string NumberStr => $"Room №{_model.Id}";

        public string CostStr => $"Cost: {Cost}$";

        public int Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                NotifyPropertyChanged(() => Floor);
            }
        }

        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                NotifyPropertyChanged(() => Cost);
                NotifyPropertyChanged(() => CostStr);
            }
        }

        public LivingRoomViewModel() { }

        public LivingRoomViewModel(LivingRoom room) 
        {
            AttachModel(room);
        }

        public void AttachModel(LivingRoom room)
        {
            _model = room;
            Floor = room.Floor;
            Cost = room.Cost;

            RefreshModel();
        }

        public void RefreshModel()
        {
            NotifyPropertyChanged(() => Floor);
            NotifyPropertyChanged(() => Cost);
        }
    }
}
