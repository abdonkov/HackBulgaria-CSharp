using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorialGenerator
{
    class Program
    {
        static IEnumerable<int> GenerateFactorials(int n)
        {
            int fact = 1;
            yield return 1;
            for (int i = 1; i <= n; i++)
            {
                fact *= i;
                yield return fact;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine(string.Join(", ", GenerateFactorials(10)));
            Console.ReadKey();
        }
    }
}
