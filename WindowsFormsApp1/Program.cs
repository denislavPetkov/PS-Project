using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        private static OptionFilter.FilterOptions optionsFilter = OptionFilter.FilterOptionsImpl;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FormMain(optionsFilter(new OptionContext().Options.ToList<Option>())));
        }


    }
}
