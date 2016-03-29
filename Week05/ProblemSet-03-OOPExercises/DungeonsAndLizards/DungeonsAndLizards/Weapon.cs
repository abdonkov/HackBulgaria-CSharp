using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndLizards
{
    class Weapon
    {
        private readonly string name;
        private readonly int damage;
        public string Name { get { return name; } }
        public int Damage { get { return damage; } }

        public Weapon(string name, int damage)
        {
            this.name = name;
            this.damage = damage;
        }
    }
}
