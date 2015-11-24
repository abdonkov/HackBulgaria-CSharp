using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelsInAString
{
    class Program
    {
        static int CountVowels(string str)
        {
            int br = 0;
            str = str.ToLower();
            for (int i = 0; i < str.Length; i++)
            {
                switch(str[i])
                {
                    case 'a': br++; break;
                    case 'e': br++; break;
                    case 'i': br++; break;
                    case 'o': br++; break;
                    case 'u': br++; break;
                    case 'y': br++; break;
                    default: break;
                }
            }
            return br;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(CountVowels(Console.ReadLine()));
            Console.ReadKey();
        }
    }
}
