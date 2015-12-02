using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractions
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction frac1 = new Fraction(2, 5);
            Fraction frac2 = new Fraction(3, 4);
            double number = 0.234;

            Console.WriteLine("{0} + {1} = {2}", frac1, frac2, frac1 + frac2);
            Console.WriteLine("{0} - {1} = {2}", frac1, frac2, frac1 - frac2);
            Console.WriteLine("{0} * {1} = {2}", frac1, frac2, frac1 * frac2);
            Console.WriteLine("{0} / {1} = {2}", frac1, frac2, frac1 / frac2);

            Console.WriteLine();

            Console.WriteLine("{0} + {1} = {2}", number, frac2, number + frac2);
            Console.WriteLine("{0} - {1} = {2}", number, frac2, number - frac2);
            Console.WriteLine("{0} * {1} = {2}", number, frac2, number * frac2);
            Console.WriteLine("{0} / {1} = {2}", number, frac2, number / frac2);

            Console.WriteLine();

            Console.WriteLine("{0} + {1} = {2}", frac1, number, frac1 + number);
            Console.WriteLine("{0} - {1} = {2}", frac1, number, frac1 - number);
            Console.WriteLine("{0} * {1} = {2}", frac1, number, frac1 * number);
            Console.WriteLine("{0} / {1} = {2}", frac1, number, frac1 / number);

            Console.ReadKey();
        }
    }
}
