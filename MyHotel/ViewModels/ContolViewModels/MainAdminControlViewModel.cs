using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyHotel
{
    public class MainAdminControlViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; set; }

        public MainAdminControlViewModel(IShellViewModel shellViewModel) : base(shellViewModel)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandDelegate);
        }

        public override void SetClose()
        {
            LogoutCommand = null;

            base.SetClose();
        }

        private void LogoutCommandDelegate(object o)
        {
            CurrentUser.AttachModel(null);
        }
    }
}
