using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseAString
{
    class Program
    {
        static string ReverseMe(string str)
        {
            StringBuilder reversed = new StringBuilder();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                reversed.Append(str[i]);
            }

            return reversed.ToString();
        }

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(ReverseMe(input));
            Console.ReadKey();
        }
    }
}
