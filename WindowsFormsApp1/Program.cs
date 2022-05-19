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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _InitApp.bForm0 = _SecurityDescriptor.DemandRight(typeof(Form0), "VIEW", false);
            _InitApp.bForm1 = _SecurityDescriptor.DemandRight(typeof(Form1), "VIEW", false);
            _InitApp.bForm3 = _SecurityDescriptor.DemandRight(typeof(Form2), "VIEW", false);
            _InitApp.bForm4 = _SecurityDescriptor.DemandRight(typeof(Form3), "VIEW", false);
            _InitApp.bForm5 = _SecurityDescriptor.DemandRight(typeof(Form4), "VIEW", false);
            _InitApp.bForm6 = _SecurityDescriptor.DemandRight(typeof(Form5), "VIEW", false);
            _InitApp.bForm2 = _SecurityDescriptor.DemandRight(typeof(Form6), "VIEW", false);
            _InitApp.bForm7 = _SecurityDescriptor.DemandRight(typeof(Form7), "VIEW", false);

            Application.Run(new FormMain());
        }
    }
}
