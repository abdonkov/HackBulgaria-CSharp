using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anagrams
{
    class Program
    {
        static Dictionary<char, int> CharHistogram(string str)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();

            for (int i = 0; i < str.Length; i++)
            {
                if (result.ContainsKey(str[i]))
                {
                    result[str[i]]++;
                }
                else
                {
                    result.Add(str[i], 1);
                }
            }
            return result;
        }

        static bool IsAnagram(string first, string second)
        {
            if (first.Length != second.Length) return false;

            var firstHistogram = CharHistogram(first);
            var secondHistogram = CharHistogram(second);

            if (firstHistogram.Count != secondHistogram.Count) return false;

            foreach(KeyValuePair<char, int> pair in firstHistogram)
            {
                if (secondHistogram.ContainsKey(pair.Key))
                {
                    if (secondHistogram[pair.Key] != pair.Value) return false;
                }
                else return false;
            }
            return true;
        }

        static bool HasAnagramOf(string first, string second)
        {
            if (first.Length > second.Length) return false;

            for (int i = 0; i < second.Length - first.Length + 1; i++)
            {
                if (IsAnagram(first, second.Substring(i, first.Length))) return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            string inputFirst = Console.ReadLine();
            string inputSecond = Console.ReadLine();

            if (IsAnagram(inputFirst, inputSecond)) Console.WriteLine("{0} IS anagram of {1}", inputFirst, inputSecond);
            else Console.WriteLine("{0} IS NOT anagram of {1}", inputFirst, inputSecond);

            if (HasAnagramOf(inputFirst, inputSecond)) Console.WriteLine("{0} HAS anagram of {1}", inputSecond, inputFirst);
            else Console.WriteLine("{0} DOESN'T HAVE anagram of {1}", inputSecond, inputFirst);

            Console.ReadKey();
        }
    }
}
