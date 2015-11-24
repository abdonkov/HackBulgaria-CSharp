using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static int Fib(int n)
        {
            if (n == 0) return 1;
            if (n == 1) return 1;
            return Fib(n - 2) + Fib(n - 1);
        }
        static long FibNum(int n)
        {
            string sum = "";
            for (int i = 0; i < n; i++)
            {
                sum += Fib(i).ToString();
            }
            return long.Parse(sum);
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(FibNum(n));
            Console.ReadKey();
        }
    }
}
