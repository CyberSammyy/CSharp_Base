using Lesson7.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7
{
    public class Player : GamePerson
    {
        public Inventory inventory;
        public Player(int x, int y, int health, int damage)
            : base(x, y, health, damage, 'P')
        {
            inventory = new Inventory();
        }
        public void Set(char[,] symbols, Player player, Enemy[] enemies, ref Map map, ICarriable[] items)
        {
            map = new Map(symbols.GetLength(0), symbols.GetLength(1), player, enemies, items);
            map.CreateMap(symbols);
        }

    }
}
