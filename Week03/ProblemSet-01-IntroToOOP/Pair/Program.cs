using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pair
{
    class Program
    {
        static void Main(string[] args)
        {

            Pair<int, string> pair1 = new Pair<int, string>(20, "asd");
            Pair<int, string> pair2 = new Pair<int, string>(20, "asd");
            Pair<int, string> pair3 = new Pair<int, string>(20, "asdf");

            Console.WriteLine(pair1.Equals(pair2));
            Console.WriteLine(pair1 == pair2);
            Console.WriteLine(pair1 != pair2);

            Console.WriteLine();

            Console.WriteLine(pair1.Equals(pair3));
            Console.WriteLine(pair1 == pair3);
            Console.WriteLine(pair1 != pair3);

            Console.WriteLine();

            Pair<char, string> pair4 = new Pair<char, string>('a', "blabla");
            Console.WriteLine(pair4);

            Console.ReadKey();
        }
    }
}
