using System;
using System.Collections.Generic;
using System.Text;
using Lesson7.Game.Equipment;

namespace Lesson7
{
    public abstract class Armor : ICarriable, IArmorElement
    {
        private int commonResistance;
        public string Name { get; set; }
        public int CommonResistance { get { return commonResistance; } }
        public int Durability { get; set; }
        public char Icon { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int AdditDamage => throw new NotImplementedException();

        public void BlockDamage(int damage)
        {
            Durability -= (int)(damage * 0.15);
        }
    }
}
