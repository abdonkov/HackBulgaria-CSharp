using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    class Program
    {
        static bool IsPrime(int n)
        {
            if (n <= 0) return false;
            if (n == 1 || n == 2) return true;

            int last = (int)Math.Sqrt(n);
            for (int i = 2; i <= last; i++ )
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        static void ListFirstPrimes(int n)
        {
            if (n <= 0) return;
            if (n == 1)
            {
                Console.WriteLine(1);
                return;
            }

            Console.Write("1, 2");
            int listed = 2;
            int curCheck = 3;

            while(listed != n)
            {
                if (IsPrime(curCheck))
                {
                    Console.Write(", {0}", curCheck);
                    curCheck += 2;
                    listed++;
                }
                else
                {
                    curCheck += 2;
                }
            }
            Console.WriteLine();
        }

        static void ListFirstPrimesSieveOfErat(int n)
        {
            if (n <= 0) return;
            if (n == 1)
            {
                Console.WriteLine(1);
                return;
            }
            if (n == 2)
            {
                Console.WriteLine("1, 2");
                return;
            }

            int max = (int)Math.Round(n * Math.Log(n * Math.Log(n))); // Using the formula for Nth prime number Pn:
                                                                      // Pn < n * ln( n * ln(n) )
            bool[] sieve = new bool[max + 1];

            for (int i = 0; i <= max; i++)
            {
                sieve[i] = true;
            }

            for (int i = 2; i <= max; i++)
            {
                if (sieve[i])
                {
                    for (int j = i * i; j <= max; j += i)
                    {
                        sieve[j] = false;
                    }
                }
            }

            Console.Write(1);

            int counter = 1;

            for (int i = 2; i <= max; i++)
            {
                if (sieve[i])
                {
                    Console.Write(", {0}", i);
                    counter++;
                }
                if (counter == n) break;
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine(IsPrime(number));
            ListFirstPrimes(number);
            ListFirstPrimesSieveOfErat(number);
            Console.ReadKey();
        }
    }
}
