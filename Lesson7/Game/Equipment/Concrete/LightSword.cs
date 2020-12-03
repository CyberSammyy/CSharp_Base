using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7.Game.Equipment.Concrete
{
    class LightSword : Weapon
    {
        public LightSword(char icon, string name) 
            : base(name, icon, 10)
        {

        }
    }
}
