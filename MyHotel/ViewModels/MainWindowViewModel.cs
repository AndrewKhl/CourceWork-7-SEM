using MyHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyHotel
{
    public class MainWindowViewModel : BaseViewModel, IDisposable
    {
        private readonly Window _mainWindow = Application.Current.MainWindow;
        private readonly CoreManager _coreManager;


        public MainWindowViewModel() : base(nameof(MainWindowViewModel))
        {
            _mainWindow.Closed += CloseWindow;

            _coreManager = new CoreManager();
        }

        public void Dispose()
        {
            _coreManager.Dispose();
        }
        
        private void CloseWindow(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
