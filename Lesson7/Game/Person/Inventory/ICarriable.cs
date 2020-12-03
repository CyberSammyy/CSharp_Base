using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7
{
    public interface ICarriable
    {
        public string Name { get; set; }
        public char Icon { get; set; }
        public int AdditDamage { get; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
