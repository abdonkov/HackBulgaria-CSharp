using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDesk
{
    public class Coin
    {
        private readonly int amount;
        public int Amount { get { return amount; } }
        public Coin(int amount)
        {
            this.amount = amount;
        }

        public override string ToString()
        {
            return string.Format("A {0}¢ coin", amount);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coin)
            {
                Coin coin = obj as Coin;
                if (!amount.Equals(coin.amount)) return false;
                
                return true;
            }
            else return false;
        }

        public static bool operator ==(Coin c1, Coin c2)
        {
            return object.Equals(c1, c2);
        }

        public static bool operator !=(Coin c1, Coin c2)
        {
            return !object.Equals(c1, c2);
        }

        public override int GetHashCode()
        {
            return 3 + amount.GetHashCode() * 4;
        }

        public static explicit operator int(Coin c)
        {
            return c.amount;
        }
    }
}
