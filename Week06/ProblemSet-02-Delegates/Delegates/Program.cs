using System;
using System.Collections.Generic;

namespace Delegates
{
    public delegate bool FilterDelegate<T>(T item);
    public delegate T AggregationDelegate<T>(T aggregation, T newItem);

    class Program
    {
        static List<int> FilterCollection(List<int> original, FilterDelegate<int> filter)
        {
            List<int> filteredNums = new List<int>();
            foreach (var item in original)
            {
                if (filter(item)) filteredNums.Add(item);
            }
            return filteredNums;
        }

        static bool EvenFilter(int item)
        {
            return item % 2 == 0;
        }

        static bool OddFilter(int item)
        {
            return item % 2 != 0;
        }

        static int AggregateCollection(List<int> original, AggregationDelegate<int> aggregate)
        {
            if (original.Count == 0) return 0;
            if (original.Count == 1) return original[0];
            int curAggregation = aggregate(original[0], original[1]);
            if (original.Count == 1) return curAggregation;
            for (int i = 2; i < original.Count; i++)
            {
                curAggregation = aggregate(curAggregation, original[i]);
            }
            return curAggregation;
        }

        static int SumAggregate(int aggregation, int newNum)
        {
            return aggregation + newNum;
        }

        static int ProductAggregate(int aggregation, int newNum)
        {
            return aggregation * newNum;
        }

        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine("All numbers: " + string.Join(", ", numbers));

            FilterDelegate<int> myIntFilter = EvenFilter;
            Console.WriteLine("Even numbers: " + string.Join(", ", FilterCollection(numbers, myIntFilter)));
            myIntFilter = OddFilter;
            Console.WriteLine("Odd numbers: " + string.Join(", ", FilterCollection(numbers, myIntFilter)));

            AggregationDelegate<int> myIntAggregator = SumAggregate;
            Console.WriteLine("Sum of numbers: " + string.Join(", ", AggregateCollection(numbers, myIntAggregator)));
            myIntAggregator = ProductAggregate;
            Console.WriteLine("Product of numbers: " + string.Join(", ", AggregateCollection(numbers, myIntAggregator)));

            Console.ReadKey();
        }
    }
}
