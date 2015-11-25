using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseAList
{
    class Program
    {
        static void ReverseList(List<int> list)
        {
            for (int i = 0; i < list.Count / 2; i++)
            {
                int temp = list[i];
                list[i] = list[list.Count - i - 1];
                list[list.Count - i - 1] = temp;
            }
        }

        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ReverseList(list);
            Console.WriteLine(string.Join(", ", list));
            Console.ReadKey();
        }
    }
}
