using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximalScalarProduct
{
    class Program
    {
        static int MaxScalarProduct(List<int> v1, List<int> v2)
        {
            var sortedV1 = v1.OrderByDescending(x => x).ToList();
            var sortedV2 = v2.OrderByDescending(x => x).ToList();

            int sum = 0;
            for (int i = 0; i < sortedV1.Count; i++ )
            {
                sum += sortedV1[i] * sortedV2[i];
            }
            return sum;
        }
        static void Main(string[] args)
        {
            List<int> v1 = new List<int>() { 4, 8, 0, 2, 4, 5, 7, 2 };
            List<int> v2 = new List<int>() { 5, 2, 1, 3, 2, 7, 4, 1 };

            Console.WriteLine(MaxScalarProduct(v1, v2));
            Console.ReadKey();
        }
    }
}
