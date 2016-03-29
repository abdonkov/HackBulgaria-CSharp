using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndLizards
{
    class Enemy : Fighter
    {
        private int startingDamage;

        public Enemy(int maxHealth, int maxMana, int startingDamage)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            this.maxMana = maxMana;
            mana = maxMana;
            weapon = noWeapon;
            spell = noSpell;
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
