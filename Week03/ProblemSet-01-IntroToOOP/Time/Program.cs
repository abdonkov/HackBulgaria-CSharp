using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            Time date1 = new Time(2015, 12, 5, 12, 33, 14);
            Time date2 = new Time(new DateTime(2014, 11, 7, 5, 45, 5));
            Time date3 = Time.Now();

            Console.WriteLine(date1);
            Console.WriteLine(date2);
            Console.WriteLine(date3);

            Console.ReadKey();
        }
    }
}
