﻿using System;
using System.Collections.Generic;
using System.Text;
using Lesson7.Game.Equipment;

namespace Lesson7
{
    public abstract class Helmet : ICarriable, IArmorElement
    {
        public int Weight { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int CommonResistance { get; set; }
        public int Durability { get; set; }
        public char Icon { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int X { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int AdditDamage => throw new NotImplementedException();

        public void BlockDamage(int damage)
        {
            Durability -= (int)(damage * 0.1);
        }
    }
}
