using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowsFormsApp1;

namespace WpfApp1
{
    public class OptionsViewModel
    {
        public Option SelectedOption { get; set; }

        public List<Option> Options { get; set; }

        public OptionsViewModel(List<Option> options)
        {
                Options = options;
        }


        public void OpenWindow()
        {
            Type windowType = WindowGetter.GetWindow((int)SelectedOption.Number);

            if (windowType == null)
            {
                return;
            }

            Window mainWindow = Application.Current.MainWindow;

            foreach (Window window in mainWindow.OwnedWindows)
            {
                if (window.GetType().Equals(windowType))
                {
                    window.Focus();
                    return;
                }
                window.Close();
            }


            Window newWindow = (Window)Activator.CreateInstance(windowType);

            newWindow.Owner = mainWindow;

            newWindow.Left = newWindow.Owner.Width + newWindow.Owner.Left - 20;
            newWindow.Width = System.Windows.SystemParameters.WorkArea.Right - newWindow.Owner.Width;

            newWindow.Top = newWindow.Owner.Top;
            newWindow.Height = newWindow.Owner.Height;
            newWindow.Show();
        }

    }
}
