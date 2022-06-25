using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public class OptionFilter
    {
        public delegate List<Option> FilterOptions(List<Option> options);

        // an implementation of the FilterOptions delegate
        public static List<Option> FilterOptionsImpl(List<Option> options)
        {
            List<Option> filteredOptions = new List<Option>();

            foreach (Option option in options)
            {
                if (option.Number % 2 == 0)
                {
                    filteredOptions.Add(option);
                }
            }

            return filteredOptions;
        }
    }
}
