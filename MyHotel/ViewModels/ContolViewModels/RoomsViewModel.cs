using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyHotel
{
    public class RoomsControlViewModel : BaseViewModel
    {
        private LivingRoomViewModel _selectRoom;

        public RoomsControlViewModel(IShellViewModel shell) : base(shell)
        {
            SelectRoom = LivingRooms.First();

            FreeRooms = new ObservableCollection<LivingRoomViewModel>(LivingRooms);
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

        public ObservableCollection<LivingRoomViewModel> FreeRooms { get; set; }

        public void SetFreeRooms(List<int> id)
        {
            FreeRooms = new ObservableCollection<LivingRoomViewModel>(LivingRooms.Where(u => id.Contains(u.Id)));

            SelectRoom = FreeRooms.Count > 0 ? FreeRooms.First() : LivingRooms.First();

            NotifyPropertyChanged(() => FreeRooms);
        }
    }
}
