using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeElementsInAList
{
    class Program
    {
        static int Min(int[] items)
        {
            int min = int.MaxValue;
            foreach(int item in items)
            {
                if (min > item) min = item;
            }
            return min;
        }

        static int Max(int[] items)
        {
            int max = int.MinValue;
            foreach (int item in items)
            {
                if (max < item) max = item;
            }
            return max;
        }

        static int NthMin(int n, int[] items)
        {
            int nthMin = int.MaxValue;
            int[] itemsCopy = new int[items.Length];

            for (int i = 0; i < itemsCopy.Length; i++)
            {
                itemsCopy[i] = items[i];
            }

            for (int i = 0; i < n; i++)
            {
                int curMin = Min(itemsCopy);

                for (int j = 0; j < itemsCopy.Length; j++)
                {
                    if (itemsCopy[j] == curMin)
                    {
                        itemsCopy[j] = int.MaxValue;
                        break;
                    }
                }

                nthMin = curMin;
            }

            return nthMin;
        }

        static int NthMax(int n, int[] items)
        {
            int nthMax = int.MinValue;
            int[] itemsCopy = new int[items.Length];

            for (int i = 0; i < itemsCopy.Length; i++)
            {
                itemsCopy[i] = items[i];
            }

            for (int i = 0; i < n; i++)
            {
                int curMax = Max(itemsCopy);

                for (int j = 0; j < itemsCopy.Length; j++)
                {
                    if (itemsCopy[j] == curMax)
                    {
                        itemsCopy[j] = int.MinValue;
                        break;
                    }
                }

                nthMax = curMax;
            }

            return nthMax;
        }

        static void Main(string[] args)
        {
            int[] test = new int[6] { 11, 14, 4, 7, 2, 7 };

            Console.WriteLine(Min(test));
            Console.WriteLine(Max(test));
            Console.WriteLine(NthMin(2, test));
            Console.WriteLine(NthMax(3, test));
            Console.ReadKey();
        }
    }
}
