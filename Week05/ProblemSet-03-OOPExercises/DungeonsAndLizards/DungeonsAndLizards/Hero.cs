using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndLizards
{
    class Hero : Fighter
    {
        private readonly string name;
        private readonly string title;
        private int manaRegenerationRate;

        public Hero(string name, string title, int maxHealth, int maxMana, int manaRegenerationRate)
        {
            this.name = name;
            this.title = title;
            this.maxHealth = maxHealth;
            health = maxHealth;
            this.maxMana = maxMana;
            mana = maxMana;
            this.manaRegenerationRate = manaRegenerationRate;
            weapon = noWeapon;
            spell = noSpell;
        }

        public string KnownAs()
        {
            return string.Format("{0} the {1}", name, title);
        }

        public void Equip(Weapon weapon)
        {
            this.weapon = weapon;
        }

        public void Learn(Spell spell)
        {
            this.spell = spell;
        }

        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
