using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorial
{
    class Program
    {
        static  int FactorialLoop(int n)
        {
            int Sum = 1;
            for (int i = 1; i <= n; i++)
            {
                Sum *= i;
            }
            return Sum;
        }

        static int FactorialRecursion(int n)
        {
            if (n <= 0)
            {
                return 1;
            }
            return n*FactorialRecursion(n - 1);
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(FactorialLoop(n));
            Console.WriteLine(FactorialRecursion(n));
            Console.ReadKey();
        }
    }
}
