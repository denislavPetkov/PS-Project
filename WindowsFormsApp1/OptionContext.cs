using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public class OptionContext : DbContext
    {
        public OptionContext() : base(Properties.Settings.Default.DbConnect)
        { }


        public bool TestOptionsIfEmpty()
        {
            OptionContext context = new OptionContext();
            IEnumerable<Option> options = context.Options;
            return options.Count() == 0;
        }

        public DbSet<Option> Options { get; set; }
    }
}
