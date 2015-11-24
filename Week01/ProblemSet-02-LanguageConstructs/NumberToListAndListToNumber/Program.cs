using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToListAndListToNumber
{
    class Program
    {
        static List<int> NumberToList(int n)
        {
            List<int> digits = new List<int>();
            if (n == 0) digits.Add(0);
            while(n > 0)
            {
                digits.Add(n % 10);
                n /= 10;
            }

            digits.Reverse(0, digits.Count);

            return digits;
        }

        static int ListToNumber(List<int> digits)
        {
            int number = 0;
            foreach(int digit in digits)
            {
                number = number * 10 + digit;
            }
            return number;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> digits = NumberToList(n);
            foreach (int digit in digits)
            {
                Console.WriteLine(digit);
            }

            Console.WriteLine();

            int number = ListToNumber(digits);
            Console.WriteLine(number);
            Console.ReadKey();
        }
    }
}
