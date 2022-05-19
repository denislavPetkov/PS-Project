using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public class Option
    {
        public Option(){}

        public Option(int optionId, string name, int number, byte[] image)
        {
            OptionId = optionId;
            Name = name;
            Number = number;
            Image = image;
        }

        public int OptionId { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
        public byte[] Image { get; set; }
    }
}
