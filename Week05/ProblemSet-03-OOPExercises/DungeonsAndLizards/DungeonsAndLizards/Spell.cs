using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndLizards
{
    class Spell
    {
        private readonly string name;
        private readonly int damage;
        private readonly int manaCost;
        private readonly int castRange;
        public string Name { get { return name; } }
        public int Damage { get { return damage; } }
        public int ManaCost { get { return manaCost; } }
        public int CastRange { get { return castRange; } }

        public Spell(string name, int damage, int manaCost, int castRange)
        {
            this.name = name;
            this.damage = damage;
            this.manaCost = manaCost;
            this.castRange = castRange;
        }
    }
}
