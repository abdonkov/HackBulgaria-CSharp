using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastAndFurious
{
    class Program
    {
        static void Main(string[] args)
        {
            Audi a = new Audi();
            Console.WriteLine(a.IsEcoFriendly(false));
            a.MoveNKilometers(23);
            Console.WriteLine(a.Mileage);
            BMW b = new BMW();
            Console.WriteLine(b.IsEcoFriendly(false));
            b.MoveNKilometers(45);
            Console.WriteLine(b.Mileage);
            Volkswagen v = new Volkswagen();
            Console.WriteLine(v.IsEcoFriendly(false));
            v.MoveNKilometers(88);
            Console.WriteLine(v.Mileage);
            Honda h = new Honda();
            Console.WriteLine(h.IsEcoFriendly(false));
            Skoda s = new Skoda();
            Console.WriteLine(s.IsEcoFriendly(false));

            Console.ReadKey();
        }
    }
}
