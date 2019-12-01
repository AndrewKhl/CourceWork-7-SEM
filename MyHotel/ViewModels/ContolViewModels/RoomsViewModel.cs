using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel
{
    public class RoomsControlViewModel : BaseViewModel
    {
        private LivingRoomViewModel _selectRoom;

        public RoomsControlViewModel(IShellViewModel shell) : base(shell)
        {
            SelectRoom = LivingRooms.First();

            EditModeOn = new DelegateCommand(EditModeOnDelegate);
        }

        public bool IsEditMode { get; set; }

        public DelegateCommand EditModeOn { get; set; }

        public ObservableCollection<LivingRoomViewModel> LivingRooms => _shell.LivingRooms;

        public LivingRoomViewModel SelectRoom
        {
            get => _selectRoom;
            set
            {
                _selectRoom = value;

                NotifyPropertyChanged(() => SelectRoom);
            }
        }

        private void EditModeOnDelegate(object o)
        {
            IsEditMode = true;

            NotifyPropertyChanged(() => IsEditMode);
        }
    }
}
