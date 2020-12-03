using Lesson7.Game.Abstract;
using Lesson7.Game.Equipment.Interfaces;
using Lesson7.Game.Person.Abstarct;

namespace Lesson7
{
    public abstract class GamePerson : IDamagable, ICoordinate
    {
        private int healthPoints;
        public bool IsAlive { get; set; }
        public int Damage { get; set; }
        public char Icon { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public GamePerson(int x, int y, int hp, int dmg, char icon)
        {
            X = x;
            Y = y;
            HealthPoints = hp;
            Damage = dmg;
            Icon = icon;
        }
        public int HealthPoints
        {
            get
            {
                return healthPoints;
            }
            set
            {
                if (value > 100)
                {
                    healthPoints = 100;
                    IsAlive = true;
                }
                else if (value < 1)
                {
                    healthPoints = 0;
                    IsAlive = false;
                }
                else
                {
                    healthPoints = value;
                    IsAlive = true;
                }
            }
        }
        public uint Level { get; set; }
        public Weapon Weapon { get; set; }
        public void ReceiveDamage(int damage, int attackType)
        {
            HealthPoints -= damage;
        }
    }
}
