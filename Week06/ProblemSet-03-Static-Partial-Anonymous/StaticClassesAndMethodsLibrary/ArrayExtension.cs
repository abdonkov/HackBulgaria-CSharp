using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticClassesAndMethodsLibrary
{
    public static class ArrayExtension
    {
        private static readonly char ReplacingValue;

        static ArrayExtension()
        {
            ReplacingValue = new Configuration().GetReplacingValue();
        }

        public static List<T> Intersect<T>(this List<T> firstList, List<T> secondList) where T : IComparable
        {
            Dictionary<T, bool> itemsInSecondList = new Dictionary<T, bool>();
            foreach (var item in secondList)
            {
                if (!itemsInSecondList.ContainsKey(item))
                    itemsInSecondList.Add(item, true);
            }
            var intersectedItems = from item in firstList
                                   where itemsInSecondList.ContainsKey(item)
                                   select item;
            return intersectedItems.ToList();
        }

        public static List<T> UnionAll<T>(List<T> firstList, List<T> secondList) where T : IComparable
        {
            List<T> unionAllList = new List<T>(firstList.Count + secondList.Count);
            unionAllList.AddRange(firstList);
            unionAllList.AddRange(secondList);
            return unionAllList;            
        }

        public static List<T> Union<T>(List<T> firstList, List<T> secondList) where T : IComparable
        {
            List<T> unionList = new List<T>(firstList.Count + secondList.Count);
            Dictionary<T, bool> itemsInFirstList = new Dictionary<T, bool>();
            foreach (var item in firstList)
            {
                if (!itemsInFirstList.ContainsKey(item))
                    itemsInFirstList.Add(item, true);
            }
            unionList.AddRange(firstList);
            unionList.AddRange(from item in secondList
                               where !itemsInFirstList.ContainsKey(item)
                               select item);
            return unionList;
        }

        public static string Join(List<string> list)
        {
            StringBuilder joined = new StringBuilder();
            var count = list.Count;
            foreach (var item in list)
            {
                joined.Append(item);
                if (--count > 0)
                    joined.Append(ReplacingValue);
            }
            return joined.ToString();
        }
    }
}
