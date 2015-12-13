using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_GenericType
{
    class Combination<T1, T2>
    {
        private readonly T1 firstItem;
        private readonly T1 secondItem;
        private readonly T1 thirdItem;
        private readonly T2 forthItem;
        private readonly T2 fifthItem;
        private readonly T2 sixthItem;
        public T1 FirstItem { get { return firstItem; } }
        public T1 SecondItem { get { return secondItem; } }
        public T1 ThirdItem { get { return thirdItem; } }
        public T2 ForthItem { get { return forthItem; } }
        public T2 FifthItem { get { return fifthItem; } }
        public T2 SixthItem { get { return sixthItem; } }

        public Combination(T1 firstItem, T1 secondItem, T1 thirdItem, T2 forthItem, T2 fifthItem, T2 sixthItem)
        {
            this.firstItem = firstItem;
            this.secondItem = secondItem;
            this.thirdItem = thirdItem;
            this.forthItem = forthItem;
            this.fifthItem = fifthItem;
            this.sixthItem = sixthItem;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}, {4}, {5}]", firstItem, secondItem, thirdItem, forthItem, fifthItem, sixthItem);
        }

        public override bool Equals(object obj)
        {
            if (obj is Combination<T1, T2>)
            {
                var comb = obj as Combination<T1, T2>;
                if (!object.Equals(firstItem, comb.firstItem)) return false;
                if (!object.Equals(secondItem, comb.secondItem)) return false;
                if (!object.Equals(thirdItem, comb.thirdItem)) return false;
                if (!object.Equals(forthItem, comb.forthItem)) return false;
                if (!object.Equals(fifthItem, comb.fifthItem)) return false;
                if (!object.Equals(sixthItem, comb.sixthItem)) return false;

                return true;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash = hash * 31 + firstItem.GetHashCode();
                hash = hash * 31 + secondItem.GetHashCode();
                hash = hash * 31 + thirdItem.GetHashCode();
                hash = hash * 29 + forthItem.GetHashCode();
                hash = hash * 29 + fifthItem.GetHashCode();
                hash = hash * 29 + sixthItem.GetHashCode();
                return hash;
            }
        }
    }
}
