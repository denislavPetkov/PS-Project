using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class WindowGetter
    {
        private static Dictionary<int, Type> availableWindows = new Dictionary<int, Type>()
        {
            { 0,typeof(Window1) },
            { 1,typeof(Window2) },
            { 2,typeof(Window1) },
            { 3,typeof(Window1) },
            { 4,typeof(Window1) },
            { 5,typeof(Window1) },
        };

        public static Type GetWindow(int index)
        {
            return availableWindows[index];
        }
    }
}
