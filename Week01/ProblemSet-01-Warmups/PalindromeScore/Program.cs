using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalindromeScore
{
    class Program
    {
        static bool Palindrom(int n)
        {
            string str = n.ToString();
            bool pal = true;
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] != str[str.Length - i - 1])
                {
                    pal = false;
                    break;
                }
            }
            return pal;
        }

        static int PScore(int n)
        {
            if (Palindrom(n)) return 1;
            else
            {
                int oldN = n;
                int newN = 0;
                while(oldN > 0)
                {
                    newN = newN * 10 + oldN % 10;
                    oldN /= 10;
                }
                return 1 + PScore(n + newN);
            }
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(PScore(n));
            Console.ReadKey();
        }
    }
}
