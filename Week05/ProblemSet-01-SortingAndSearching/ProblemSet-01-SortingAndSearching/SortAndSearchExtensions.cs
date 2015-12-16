using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSet_01_SortingAndSearching
{
    public static class SortAndSearchExtensions
    {
        public static IList<T> BubbleSort<T>(this IList<T> listToSort)
        {
            return listToSort.BubbleSort(Comparer<T>.Default);
        }

        public static IList<T> BubbleSort<T>(this IList<T> listToSort, IComparer<T> comparer)
        {
            bool hasChange;
            do
            {
                hasChange = false;
                for (int i = 0; i < listToSort.Count - 1; i++)
                {
                    if (comparer.Compare(listToSort[i], listToSort[i + 1]) > 0)
                    {
                        T temp = listToSort[i];
                        listToSort[i] = listToSort[i + 1];
                        listToSort[i + 1] = temp;
                        hasChange = true;
                    }
                }
            }
            while (hasChange);

            return listToSort;
        } 

        public static IList<T> SelectionSort<T>(this IList<T> listToSort)
        {
            return listToSort.SelectionSort(Comparer<T>.Default);
        }

        public static IList<T> SelectionSort<T>(this IList<T> listToSort, IComparer<T> comparer)
        {
            for (int i = 0; i < listToSort.Count; i++)
            {
                int minItemPos = i;
                for (int j = i + 1; j < listToSort.Count; j++)
                {
                    if (comparer.Compare(listToSort[minItemPos], listToSort[j]) > 0)
                    {
                        minItemPos = j;
                    }
                }
                T temp = listToSort[i];
                listToSort[i] = listToSort[minItemPos];
                listToSort[minItemPos] = temp;
            }

            return listToSort;
        }

        public static int BSearch<T>(this IList<T> listForBS, T key)
        {
            return listForBS.BSearch(key, Comparer<T>.Default);
        }

        public static int BSearch<T>(this IList<T> listForBS, T key, IComparer<T> comparer)
        {
            int minIndex = 0;
            int maxIndex = listForBS.Count - 1;
            while (minIndex <= maxIndex)
            {
                int midIndex = minIndex + (maxIndex - minIndex) / 2;

                if (comparer.Compare(listForBS[midIndex], key) == 0) return midIndex;
                if (comparer.Compare(listForBS[midIndex], key) < 0) minIndex = midIndex + 1;
                else maxIndex = midIndex - 1;

                if (minIndex < 0 || maxIndex > listForBS.Count - 1) return -1;
            }
            return -1;
        }

        public static IList<T> QuickSort<T>(this IList<T> listToSort)
        {
            return listToSort.QuickSort(Comparer<T>.Default);
        }

        public static IList<T> QuickSort<T>(this IList<T> listToSort, IComparer<T> comparer)
        {
            QuickSortRecursion(listToSort, 0, listToSort.Count - 1, comparer);
            return listToSort;
        }

        private static void QuickSortRecursion<T>(IList<T> listToSort, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            if (leftIndex < rightIndex)
            {
                int partitionPlace = QuickSortPartition(listToSort, leftIndex, rightIndex, comparer);

                QuickSortRecursion(listToSort, leftIndex, partitionPlace - 1, comparer);
                QuickSortRecursion(listToSort, partitionPlace + 1, rightIndex, comparer);
            }
        }

        private static int QuickSortPartition<T>(IList<T> listToSort, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            T pivot = listToSort[rightIndex];
            int indexToSwap = leftIndex;
            for (int j = leftIndex; j < rightIndex; j++)
            {
                if (comparer.Compare(listToSort[j], pivot) <= 0)
                {
                    T temp = listToSort[indexToSwap];
                    listToSort[indexToSwap] = listToSort[j];
                    listToSort[j] = temp;

                    indexToSwap++;
                }
            }
            T temp2 = listToSort[indexToSwap];
            listToSort[indexToSwap] = listToSort[rightIndex];
            listToSort[rightIndex] = temp2;

            return indexToSwap;
        }

        public static IList<T> MergeSort<T>(this IList<T> listToSort)
        {
            return listToSort.MergeSort(Comparer<T>.Default);
        }

        public static IList<T> MergeSort<T>(this IList<T> listToSort, IComparer<T> comparer)
        {
            var linked = new LinkedList<T>();
            foreach (T item in listToSort)
            {
                linked.AddLast(item);
            }

            linked = MergeSortLinkedListRecursion(linked, comparer);

            int i = 0;
            foreach (T item in linked)
            {
                listToSort[i++] = item;
            }

            return listToSort;
        }

        private static LinkedList<T> MergeSortLinkedListRecursion<T>(LinkedList<T> list, IComparer<T> comparer)
        {
            if (list.Count <= 1) return list;

            bool addLeft = true;
            var left = new LinkedList<T>();
            var right = new LinkedList<T>();
            foreach (T item in list)
            {
                if (addLeft)
                {
                    left.AddLast(item);
                    addLeft ^= true;
                }
                else
                {
                    right.AddLast(item);
                    addLeft ^= true;
                }
            }

            left = MergeSortLinkedListRecursion(left, comparer);
            right = MergeSortLinkedListRecursion(right, comparer);

            return MergeSortLinkedListMerge(left, right, comparer);
        }

        private static LinkedList<T> MergeSortLinkedListMerge<T>(LinkedList<T> left, LinkedList<T> right, IComparer<T> comparer)
        {
            var merged = new LinkedList<T>();
            while(left.Count !=0 && right.Count != 0)
            {
                if (comparer.Compare(left.First.Value, right.First.Value) <= 0)
                {
                    merged.AddLast(left.First.Value);
                    left.RemoveFirst();
                }
                else
                {
                    merged.AddLast(right.First.Value);
                    right.RemoveFirst();
                }
            }

            while(left.Count != 0)
            {
                merged.AddLast(left.First.Value);
                left.RemoveFirst();
            }
            while (right.Count != 0)
            {
                merged.AddLast(right.First.Value);
                right.RemoveFirst();
            }

            return merged;
        }
    }
}
