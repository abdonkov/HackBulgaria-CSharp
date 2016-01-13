using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_SortingAndSearching
{
    class Program
    {
        class LastDigitComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x % 10 - y % 10;
            }
        }

        class StringLengthComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return (x ?? String.Empty).Length - (y ?? String.Empty).Length;
            }
        }

        class OddEvenComparer : IComparer<int?>
        {
            public int Compare(int? x, int? y)
            {
                return (x == null && y == null) ? 0 : (x == null) ? -1 : (y == null) ? 1 : ((x % 2 + y % 2) % 2 == 1) ? (int)(x % 2 - y % 2) : (x % 2 == 0) ? (int)(x - y) : (x % 2 == 1) ? (int)(y - x) : 0;

                //if (x == null && y == null) return 0;
                //else if (x == null) return -1;
                //else if (y == null) return 1;

                //if ((x % 2 + y % 2) % 2 == 1) return (int)(x % 2 - y % 2);
                //else if (x % 2 == 0) return (int)(x - y);
                //else if (x % 2 == 1) return (int)(y - x);

                //return 0;
            }
        }

        class ReverseComparer<T> : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return Comparer<T>.Default.Compare(y, x);
            }
        }

        static int ReverseIntComparer(int x, int y)
        {
            return y - x;
        }

        static void Main(string[] args)
        {
            int[] array1 = new int[] { 2, 4, 1, 6, 10 };
            int[] sortedArray1 = array1.BubbleSort().ToArray();
            Console.WriteLine("Bubble Sort using IComparer: {0}", string.Join(", ", sortedArray1));

            int[] array11 = new int[] { 2, 4, 1, 6, 10 };
            int[] sortedArray11 = array11.BubbleSort(ReverseIntComparer).ToArray();
            Console.WriteLine("Bubble Sort (reverse) using delegate: {0}", string.Join(", ", sortedArray11));

            Console.WriteLine();

            int[] array2 = new int[] { 2, 4, 1, 6, 10 };
            int[] sortedArray2 = array2.SelectionSort().ToArray();
            Console.WriteLine("Selection Sort: {0}", string.Join(", ", sortedArray2));

            Console.WriteLine();

            int[] array3 = new int[] { 2, 4, 1, 6, 10 };
            int[] sortedArray3 = array3.BubbleSort().ToArray();
            int index = sortedArray3.BSearch(6);
            Console.WriteLine("Binary Search for 6 in [{0}] returns index: {1}", string.Join(", ", sortedArray3), index);

            Console.WriteLine();

            int[] array4 = new int[] { 2, 4, 1, 6, 10 };
            int[] sortedArray4 = array4.QuickSort().ToArray();
            Console.WriteLine("Quick Sort: {0}", string.Join(", ", sortedArray4));

            Console.WriteLine();

            int[] array5 = new int[] { 2, 4, 1, 6, 10 };
            int[] sortedArray5 = array5.MergeSort().ToArray();
            Console.WriteLine("Merge Sort: {0}", string.Join(", ", sortedArray5));

            Console.WriteLine();

            int?[] array6 = new int?[] { 2, 4, 1, 6, 11, null, 2, 6, 4, 65, 7, 9, 3, 3, 56, 36, 8, 34, null };
            int?[] sortedArray6 = array6.QuickSort(new OddEvenComparer()).ToArray();
            Console.WriteLine("OddEvenComparer: {0}", string.Join(", ", sortedArray6));

            Console.WriteLine();

            int[] array7 = new int[] { 2, 1, 6, 10, 5, 6, 9, 1, 11, 25 , 25, 63};
            int[] sortedArray7 = array7.QuickSort(ReverseIntComparer).ToArray();
            Console.WriteLine("Quick Sort (reverse) using delegate: {0}", string.Join(", ", sortedArray7));

            Console.ReadKey();
        }
    }
}
