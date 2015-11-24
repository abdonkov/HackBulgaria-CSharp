using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LucasSeries
{
    class Program
    {
        static int NthLucas(int n)
        {
            if (n == 0) return 2;
            if (n == 1) return 1;
            return NthLucas(n - 1) + NthLucas(n - 2);
        }

        static LinkedList<int> FirstNLucas(int n)
        {
            LinkedList<int> series = new LinkedList<int>();
            for (int i = 0; i <= n; i++)
            {
                series.AddLast(NthLucas(i));
            }
            return series;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine(NthLucas(n));
            LinkedList<int> series = FirstNLucas(n);

            Console.WriteLine(string.Join<int>(" -> ", series));
            Console.ReadKey();
        }
    }
}
