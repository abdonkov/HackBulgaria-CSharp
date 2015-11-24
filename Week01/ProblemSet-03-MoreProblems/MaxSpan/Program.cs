using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSpan
{
    class Program
    {
        static int MaxSpan(List<int> numbers)
        {
            int max = 0;

            Dictionary<int, int> counted = new Dictionary<int, int>();

            for (int i = 0; i < numbers.Count; i++ )
            {
                if (!counted.ContainsKey(numbers[i]))
                {
                    int lastIndex = i;
                    for (int j = i + 1; j < numbers.Count; j++)
                    {
                        if (numbers[i] == numbers[j])
                        {
                            lastIndex = j;
                        }
                    }
                    int curSpan = lastIndex - i + 1;
                    counted.Add(numbers[i], curSpan);
                    if (max < curSpan) max = curSpan;
                }
            }
            return max;
        }

        static void Main(string[] args)
        {
            List<int> test1 = new List<int>() { 1, 2, 1, 1, 3 };
            List<int> test2 = new List<int>() { 1, 4, 2, 1, 4, 1, 4 };
            List<int> test3 = new List<int>() { 1, 4, 2, 1, 4, 4, 4 };

            Console.WriteLine(MaxSpan(test1));
            Console.WriteLine(MaxSpan(test2));
            Console.WriteLine(MaxSpan(test3));
            Console.ReadKey();
        }
    }
}
