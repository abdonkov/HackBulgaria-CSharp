using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incrementable
{
    class Program
    {
        static void Main(string[] args)
        {
            var itc = new IncrementableTestClass(1, 1, 1, "qwerty");
            var nitc = new NonIncrementableTestClass(1, 1, 1, "asdfgh");
            var niettc = new NonIncrementableExceptionThrowingTestClass(1, 1, 1, "zxcvbn");

            itc.Increment();
            Console.WriteLine($"itc incremented: X={itc.X}, Y={itc.Y}, Z(private set)={itc.Z}");

            Console.WriteLine();

            nitc.Increment();
            Console.WriteLine($"nitc incremented: X={itc.X}, Y={itc.Y}, Z(private set)={itc.Z}");

            Console.WriteLine();

            try
            {
                niettc.Increment();
            }
            catch (NonIncrementablePropertyTypeException niptex)
            {
                Console.WriteLine($"exception caugth in niettc: {niptex.Message}");
            }

            Console.ReadKey();
        }
    }
}
