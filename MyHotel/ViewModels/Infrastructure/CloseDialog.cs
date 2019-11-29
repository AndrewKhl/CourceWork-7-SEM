using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace MyHotel
{
    public static class CloseDialog
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached("DialogResult", typeof(Boolean?), typeof(CloseDialog), new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject pDependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            if (!(bool)e.NewValue) return;

            var wndWindow = pDependencyObject as Window;
            if (wndWindow == null)
            {
                // try to find parent window
                var ctrl = pDependencyObject as FrameworkElement;

                while (ctrl != null)
                {
                    ctrl = ctrl.Parent as FrameworkElement;
                    if (ctrl is Window)
                    {
                        wndWindow = ctrl as Window;
                        break;
                    }
                }
            }

            Boolean? isModal = ComponentDispatcher.IsThreadModal;
            if (wndWindow != null && wndWindow.IsLoaded)
            {
                if (isModal.Value)
                {
                    try
                    {
                        wndWindow.DialogResult = e.NewValue as Boolean?;
                    }
                    catch (InvalidOperationException)
                    {
                        // DialogResult is set before a window is opened by calling ShowDialog
                        wndWindow.Close();
                    }
                }
                else
                    wndWindow.Close();
            }
        }

        public static void SetDialogResult(FrameworkElement pTarget, Boolean? pblnDialogResult)
        {
            pTarget.SetValue(DialogResultProperty, pblnDialogResult);
        }
    }
}
