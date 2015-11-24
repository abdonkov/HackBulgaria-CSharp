using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseEachWordInASentence
{
    class Program
    {
        static string ReverseEveryWord(string sentence)
        {
            StringBuilder reversed = new StringBuilder();
            string[] words = sentence.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = words[i].Length - 1; j >= 0; j--)
                {
                    reversed.Append(words[i][j]);
                }

                reversed.Append(" ");
            }

            return reversed.ToString();
        }    
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(ReverseEveryWord(input));
            Console.ReadKey();
        }
    }
}
