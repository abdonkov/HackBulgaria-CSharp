using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsStringAAnAnagramOfStringB
{
    class Program
    {
        static bool Anagram(string A, string B)
        {
            if (A.Length != B.Length) return false;

            for (int i = 0; i < A.Length;i++ )
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

        static void Main(string[] args)
        {
            string A = Console.ReadLine();
            string B = Console.ReadLine();
            if (Anagram(A, B)) Console.WriteLine("{0} IS anagram of {1}", B, A);
            else Console.WriteLine("{0} IS NOT anagram of {1}", B, A);
            Console.ReadKey();
        }
    }
}
