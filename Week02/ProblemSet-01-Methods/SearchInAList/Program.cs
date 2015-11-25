using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchInAList
{
    class Program
    {
        static bool TryFindSubstring(List<string> list, string searched, out int foundIndex)
        {
            foundIndex = -1;
            for (int i = 0; i < list.Count;i++ )
            {
                if (list[i].Contains(searched))
                {
                    foundIndex = i;
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            List<string> list = new List<string>() { "qwertyuiop", "asdfghjkl", "zxcvbnm", "asd123wordiop", "test123asd" };
            string searched = "test";
            int foundIndex;
            Console.WriteLine("{0} -> {1}", TryFindSubstring(list, searched, out foundIndex), foundIndex);
            Console.ReadKey();
        }
    }
}
