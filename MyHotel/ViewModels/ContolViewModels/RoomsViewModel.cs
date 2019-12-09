using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyHotel
{
    public class RoomsControlViewModel : BaseViewModel
    {
        private LivingRoomViewModel _selectRoom;

        public RoomsControlViewModel(IShellViewModel shell) : base(shell)
        {
            SelectRoom = LivingRooms.First();
        }

        public LivingRoomViewModel SelectRoom
        {
            get => _selectRoom;
            set
            {
                _selectRoom = value;

                NotifyPropertyChanged(() => SelectRoom);
            }
        }

        public ObservableCollection<string> MainPhotos { get; set; }
    }
}
