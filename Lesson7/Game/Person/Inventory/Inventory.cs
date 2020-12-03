using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7
{
    public class Inventory
    {
        public List<ICarriable> items;
        public Inventory()
        {
            items = new List<ICarriable>();
        }
    }
}
