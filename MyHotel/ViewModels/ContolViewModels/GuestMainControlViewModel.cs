using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class GuestMainControlViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }

        public GuestMainControlViewModel(IShellViewModel shell) : base(shell)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
        }

        public void RefreshModel()
        {
           
        }

        private void LogoutCommandDelegate(object o)
        {
            CurrentUser.AttachModel(null);
        }
    }
}
