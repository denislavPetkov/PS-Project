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
            { 0,typeof(Form0) },
            { 1,typeof(Form1) },
            { 2,typeof(Form2) },
            { 3,typeof(Form3) },
            { 4,typeof(Form4) },
            { 5,typeof(Form5) },
        };

        public static Type GetForm(int index)
        {
            return availableForms[index];
        }
    }
}
