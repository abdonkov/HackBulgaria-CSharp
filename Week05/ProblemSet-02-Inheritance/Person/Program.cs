using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Program
    {
        static void Main(string[] args)
        {
            Toy toy1 = new Toy("Kolicka", "Sinq", "10cm");
            Toy toy2 = new Toy("Kamionche", "Zeleno", "20cm");
            Child child1 = new Child("Ivan", "Male", new[] { toy1, toy2});
            Toy toy3 = new Toy("Barbi", "Rozovo", "15cm");
            Child child2 = new Child("Mariq", "Female", toy3);
            Child child3 = new Child("Georgi", "Male");

            Adult adult1 = new Adult("Petur", "Male", new[] { child1, child2 });
            Adult adult2 = new Adult("Katq", "Female", new[] { child3 });
            Adult adult3 = new Adult("Martin", "Male");

            Console.WriteLine(adult1.ToString());
            Console.WriteLine();
            Console.WriteLine(adult2.ToString());
            Console.WriteLine();
            Console.WriteLine(adult3.ToString());

            Console.ReadKey();
        }
    }
}
