using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncreasingAndDecreasingSequences
{
    class Program
    {
        static bool IsIncreasing(int[] sequence)
        {
            int last = int.MinValue;
            foreach(int item in sequence)
            {
                if (last >= item) return false;
                last = item;
            }
            return true;
        }

        static bool IsDecreasing(int[] sequence)
        {
            int last = int.MaxValue;
            foreach (int item in sequence)
            {
                if (last <= item) return false;
                last = item;
            }
            return true;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n]; 
            for (int i = 0; i < n; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("IsIncreasing: {0}", IsIncreasing(arr));
            Console.WriteLine("IsDecreasing: {0}", IsDecreasing(arr));
            Console.ReadKey();
        }
    }
}
