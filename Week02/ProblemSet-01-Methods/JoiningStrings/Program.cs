using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoiningStrings
{
    class Program
    {
        static string JoinStrings(string separator, params string[] strings)
        {
            StringBuilder joinedStrings = new StringBuilder();
            foreach (string curStr in strings)
            {
                joinedStrings.Append(curStr);
                joinedStrings.Append(separator);
            }
            joinedStrings.Remove(joinedStrings.Length - separator.Length, separator.Length);
            return joinedStrings.ToString();
        }

        static void Main(string[] args)
        {
            string sep = ", ";
            Console.WriteLine(JoinStrings(sep, "1", "2", "3", "asd", "qwe", "1q3", "asdf1", "test", "jbj", "sadfg", "bachka"));
            Console.ReadKey();
        }
    }
}
