using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDesk
{
    public class Bill
    {
        private readonly int amount;
        public int Amount { get { return amount; } }
        public Bill(int amount)
        {
            this.amount = amount;
        }

        public override string ToString()
        {
            return string.Format("A {0}$ bill", amount);
        }

        public override bool Equals(object obj)
        {
            if (obj is Bill)
            {
                Bill bill = obj as Bill;
                if (!amount.Equals(bill.amount)) return false;
                
                return true;
            }
            else return false;
        }

        public static bool operator ==(Bill b1, Bill b2)
        {
            return object.Equals(b1, b2);
        }

        public static bool operator !=(Bill b1, Bill b2)
        {
            return !object.Equals(b1, b2);
        }

        public override int GetHashCode()
        {
            return 4 + amount.GetHashCode() * 3;
        }

        public static explicit operator int(Bill b)
        {
            return b.amount;
        }
    }
}
