using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyEveryCharacterKTimes
{
    class Program
    {
        static string CopyEveryChar(string input, int k)
        {
            StringBuilder answer = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    answer.Append(input[i]);
                }
            }

            return answer.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter string to copy every char K times:");
            string input = Console.ReadLine();
            Console.Write("Enter K: ");
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("Answer: {0}", CopyEveryChar(input, k));
            Console.ReadKey();
        }
    }
}
