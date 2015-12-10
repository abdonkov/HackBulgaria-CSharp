using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pair
{
    class Pair<T1, T2>
    {
        private readonly T1 firstMember;
        private readonly T2 secondMember;
        public T1 FirstMember { get { return firstMember; } }
        public T2 SecondMember { get { return secondMember; } }

        public Pair(T1 firstMember, T2 secondMember)
        {
            this.firstMember = firstMember;
            this.secondMember = secondMember;
        }

        public override string ToString()
        {
            string firstMemberString = null;
            string secondMemberString = null;

            if (firstMember == null) firstMemberString = "null";
            if (secondMember == null) secondMemberString = "null";

            if (firstMember is char || firstMember is string) firstMemberString = firstMember.ToString();
            if (secondMember is char || secondMember is string) secondMemberString = secondMember.ToString();

            if (firstMember is ValueType) firstMemberString = firstMember.ToString();
            if (secondMember is ValueType) secondMemberString = secondMember.ToString();

            if (firstMemberString == null) firstMemberString = firstMember.ToString();
            if (secondMemberString == null) secondMemberString = secondMember.ToString();

            return string.Format("Pair[ {0}, {1}]", firstMemberString, secondMemberString);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pair<T1, T2>)
            {
                Pair<T1, T2> pair = obj as Pair<T1, T2>;
                if (!firstMember.Equals(pair.firstMember)) return false;
                if (!secondMember.Equals(pair.secondMember)) return false;

                return true;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + firstMember.GetHashCode();
                hash = hash * 23 + secondMember.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Pair<T1, T2> pair1, Pair<T1, T2> pair2)
        {
            return object.Equals(pair1, pair2);
        }

        public static bool operator !=(Pair<T1, T2> pair1, Pair<T1, T2> pair2)
        {
            return !object.Equals(pair1, pair2);
        }
    }
}
