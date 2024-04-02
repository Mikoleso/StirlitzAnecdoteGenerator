using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StirlitzAnecdoteGenerator
{
    public class Guest
    {
        public string Name { get; set; }
        public double Number { get; set; }

        public double Cost { get; set; }

        public Guest(string Name, double Number, double Cost)
        {
            this.Name = Name;
            this.Number = Number;
            this.Cost = Cost;
        }
    }
}
