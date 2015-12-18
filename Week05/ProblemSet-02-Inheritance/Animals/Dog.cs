using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    class Dog : Mammals
    {
        public Dog()
        {
            Name = "dog";
        }

        public override string Greet()
        {
            return "Bau!";
        }
    }
}
