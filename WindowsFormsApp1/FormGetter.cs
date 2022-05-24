using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public static class FormGetter
    {
        private static Dictionary<int, Type> availableForms = new Dictionary<int, Type>()
        {
            { 0,typeof(TestFrom) },
            { 1,typeof(TestFrom) },
            { 2,typeof(TestForm2) },
            { 3,typeof(TestFrom) },
            { 4,typeof(TestFrom) },
            { 5,typeof(TestFrom) },
        };

        public static Type GetForm(int index)
        {
            return availableForms[index];
        }
    }
}
