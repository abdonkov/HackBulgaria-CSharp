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

            Pair pair1 = new Pair(20, "asd");
            Pair pair2 = new Pair(20, "asd");
            Pair pair3 = new Pair(20, "asdf");

            Console.WriteLine(pair1.Equals(pair2));
            Console.WriteLine(pair1 == pair2);
            Console.WriteLine(pair1 != pair2);

            Console.WriteLine();

            Console.WriteLine(pair1.Equals(pair3));
            Console.WriteLine(pair1 == pair3);
            Console.WriteLine(pair1 != pair3);

            Console.ReadKey();
        }
    }
}
