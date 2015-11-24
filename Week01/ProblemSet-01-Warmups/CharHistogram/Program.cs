using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharHistogram
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

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Dictionary<char, int> hist = CharHistogram(input);

            Console.Write("{");
            Console.Write(string.Join<string>(" , ", hist.Select(x => string.Format("{0} : {1}", x.Key, x.Value))));
            Console.WriteLine("}");

            Console.ReadKey();
        }
    }
}
