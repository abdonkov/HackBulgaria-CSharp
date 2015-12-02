using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pair
{
    class Pair
    {
        private readonly object firstMember;
        private readonly object secondMember;
        public object FirstMember { get { return firstMember; } }
        public object SecondMember { get { return secondMember; } }

        public Pair(params object[] pair)
        {
            if (pair.Length != 2) throw new ArgumentException("Pair can have only 2 objects!");

            firstMember = pair[0];
            secondMember = pair[1];
        }

        public override string ToString()
        {
            string firstMemberString = null;
            string secondMemberString = null;

            if (firstMember == null) firstMemberString = "null";
            if (secondMember == null) secondMemberString = "null";

            if (firstMember is char || firstMember is string) firstMemberString = (string)firstMember;
            if (secondMember is char || secondMember is string) secondMemberString = (string)secondMember;

            if (firstMember is ValueType) firstMemberString = firstMember.ToString();
            if (secondMember is ValueType) secondMemberString = secondMember.ToString();

            if (firstMemberString == null) firstMemberString = firstMember.ToString();
            if (secondMemberString == null) secondMemberString = secondMember.ToString();

            return string.Format("Pair[ {0}, {1}]", firstMemberString, secondMemberString);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pair)
            {
                Pair pair = obj as Pair;
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

        public static bool operator ==(Pair pair1, Pair pair2)
        {
            return pair1.Equals(pair2);
        }

        public static bool operator !=(Pair pair1, Pair pair2)
        {
            return !pair1.Equals(pair2);
        }
    }
}
