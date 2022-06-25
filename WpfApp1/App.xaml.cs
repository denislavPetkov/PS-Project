using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WindowsFormsApp1;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static OptionFilter.FilterOptions optionsFilter = OptionFilter.FilterOptionsImpl;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            List<Option> options = optionsFilter(new OptionContext().Options.ToList<Option>());

            new MainWindow(options).Show();
        }

    }


}
