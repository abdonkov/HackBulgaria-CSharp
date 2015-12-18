using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    class Program
    {
        static void Main(string[] args)
        {
            Mammals m = new Mammals();
            Console.WriteLine(m.Move());
            Console.WriteLine(m.Greet());
            m = new Cat();
            Console.WriteLine(m.Move());
            Console.WriteLine(m.Greet());
            m = new Dog();
            Console.WriteLine(m.Move());
            Console.WriteLine(m.Greet());

            Console.WriteLine();

            Reptiles r = new Reptiles();
            Console.WriteLine(r.Move());
            Console.WriteLine(r.Temperature());
            r = new Crocodile();
            Console.WriteLine(r.Move());
            Console.WriteLine(r.Temperature());

            Console.WriteLine();

            Birds b = new Birds();
            Console.WriteLine(b.Move());
            Console.WriteLine(b.MakeNest());
            b = new Owl();
            Console.WriteLine(b.Move());
            Console.WriteLine(b.MakeNest());

            Console.WriteLine();

            Fish f = new Fish();
            Console.WriteLine(f.Move());
            f = new Shark();
            Console.WriteLine(f.Move());

            Console.ReadKey();
        }
    }
}
