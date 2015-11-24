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
                switch (str[i])
                {
                    case 'a': break;
                    case 'e': break;
                    case 'i': break;
                    case 'o': break;
                    case 'u': break;
                    case 'y': break;
                    default:
                        {
                            if (char.IsLetter(str[i])) br++;
                        } break;
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