using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithDigits
{
    class Program
    {
        static int CountDigits(int n)
        {
            int br = 0;
            n = Math.Abs(n);
            while(n > 0)
            {
                br++;
                n /= 10;
            }
            return br;
        }

        static int SumDigits(int n)
        {
            int Sum = 0;
            n = Math.Abs(n);
            while (n > 0)
            {
                Sum += n % 10;
                n /= 10;
            }
            return Sum;
        }

        static int Fact(int n)
        {
            if (n == 0) return 1;
            return n * Fact(n - 1);
        }

        static int FactorialDigits(int n)
        {
            int result = 0;
            n = Math.Abs(n);
            while(n > 0)
            {
                result += Fact(n%10);
                n /= 10;
            }
            return result;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(CountDigits(n));
            Console.WriteLine(SumDigits(n));
            Console.WriteLine(FactorialDigits(n));
            Console.ReadKey();
        }
    }
}
