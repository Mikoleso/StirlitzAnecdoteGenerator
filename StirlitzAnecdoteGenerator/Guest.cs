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
        public string Name2 { get; set; }
        public string Name5 { get; set; }
        public double Number { get; set; }

        public double Cost { get; set; }

        public bool isProperName { get; set; }
        public bool isDividable { get; set; }

        public Guest(string Name, string Name2, string Name5, double Number, double Cost, bool isProperName, bool isDividable)
        {
            this.Name = Name;
            this.Name2 = Name2;
            this.Name5 = Name5;
            this.Number = Number;
            this.Cost = Cost;
            this.isProperName = isProperName;
            this.isDividable = isDividable;
        }
        public Guest(string Name, string Name2, string Name5, double Number, double Cost)
        {
            this.Name = Name;
            this.Name2 = Name2;
            this.Name5 = Name5;
            this.Number = Number;
            this.Cost = Cost;
            this.isProperName = false;
            this.isDividable = true;
        }
    }
}
