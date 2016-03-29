using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndLizards
{
    abstract class Fighter
    {
        protected int maxHealth;
        protected int maxMana;
        protected int health;
        protected int mana;
        protected Weapon weapon;
        protected Spell spell;
        protected static readonly Weapon noWeapon = new Weapon("No weapon", 0);
        protected static readonly Spell noSpell = new Spell("No spell", 0, 0, 0);

        public virtual bool IsAlive()
        {
            return health <= 0;
        }

        public bool CanCast()
        {
            return mana >= spell.ManaCost;
        }

        public virtual int GetHealth()
        {
            return health;
        }
        public virtual int GetMana()
        {
            return mana;
        }
        public virtual void TakeDamage(int damagePoints)
        {
            health -= damagePoints;
            if (health < 0) health = 0;
        }

        public virtual bool TakeHealing(int healingPoints)
        {
            if (!IsAlive()) return false;

            health += healingPoints;
            if (health > maxHealth) health = maxHealth;
            return true;

        }

        public virtual void TakeMana(int manaPoints)
        {
            mana += manaPoints;
            if (mana > maxMana) mana = maxMana;
        }

        public abstract void Attack();
    }
}
