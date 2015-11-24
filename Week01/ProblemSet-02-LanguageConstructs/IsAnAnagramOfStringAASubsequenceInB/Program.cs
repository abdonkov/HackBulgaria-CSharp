using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsAnAnagramOfStringAASubsequenceInB
{
    class Program
    {
        static bool Anagram(string A, string B)
        {
            if (A.Length != B.Length) return false;

            for (int i = 0; i < A.Length; i++)
            {
                int countA = 0;
                for (int j = 0; j < A.Length; j++)
                {
                    if (A[i] == A[j]) countA++;
                }

                int countB = 0;
                for (int j = 0; j < B.Length; j++)
                {
                    if (A[i] == B[j]) countB++;
                }

                if (countA != countB) return false;
            }
            return true;
        }

        static bool HasAnagramOf(string A, string B)
        {
            if (A.Length > B.Length) return false;

            for (int i = 0; i < B.Length - A.Length + 1; i++)
            {
                if (Anagram(A, B.Substring(i, A.Length))) return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            string A = Console.ReadLine();
            string B = Console.ReadLine();

            if (HasAnagramOf(A, B)) Console.WriteLine("{0} HAS anagram of {1}", B, A);
            else Console.WriteLine("{0} DOESN'T HAVE anagram of {1}", B, A);
            Console.ReadKey();
        }
    }
}
