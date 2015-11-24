using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalindromeScore
{
    class Program
    {
        static bool IsIntPalindrome(int n)
        {
            int oldN = n;
            int newN = 0;
            while (oldN > 0)
            {
                newN = newN * 10 + oldN % 10;
                oldN /= 10;
            }
            if (n == newN) return true;
            else return false;
        }

        static int GetLargestPalindrome(int n)
        {
            int prev = n - 1;
            while (IsIntPalindrome(prev) != true)
            {
                prev--;
            }
            return prev;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(IsIntPalindrome(n));
            Console.WriteLine(GetLargestPalindrome(n));
            Console.ReadKey();

        }
    }
}