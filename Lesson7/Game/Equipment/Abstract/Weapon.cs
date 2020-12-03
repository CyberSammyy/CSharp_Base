using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7
{
    public abstract class Weapon : ICarriable
    {
        private int additDamage;
        public string Name { get; set; }
        public char Icon { get; set; }
        public int AdditDamage { get { return additDamage; } }
        public int X { get; set; }
        public int Y { get; set; }
        public Weapon(string name, char icon, int additDamage)
        {
            Name = name;
            //Icon = icon;
            Icon = 'W';
            this.additDamage = additDamage;
        }
    }
}
