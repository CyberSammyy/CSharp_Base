using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7
{
    public class Enemy : GamePerson
    {
        private Random ran = new Random();
        public Inventory Inventory { get; set; }
        public int oldX { get; set; }
        public int oldY { get; set; }
        public Enemy(int x, int y, int health, int damage, Inventory inventory) 
            : base(x, y, health, damage, 'E')
        {
            Inventory = inventory;
        }
    }
}
