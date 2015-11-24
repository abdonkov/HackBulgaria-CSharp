using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNumbers
{
    class Program
    {
        static bool IsHack(int n)
        {
            int br = 0;
            string number = "";
            while (n > 0)
            {
                if (n % 2 == 0) number += "0";
                else
                {
                    number += "1";
                    br++;
                }
                n /= 2;
            }

            if (br % 2 == 1)
            {
                bool hack = true;
                for (int i = 0; i < number.Length / 2; i++)
                {
                    if (number[i] != number[number.Length - i - 1])
                    {
                        hack = false;
                        break;
                    }
                }
                return hack;
            }
            else return false;
        }

        static int NextHack(int n)
        {
            int next = n + 1;
            while (IsHack(next) != true)
            {
                next++;
            }
            return next;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(IsHack(n));
            Console.WriteLine(NextHack(n));
            Console.ReadKey();

        }
    }
}
