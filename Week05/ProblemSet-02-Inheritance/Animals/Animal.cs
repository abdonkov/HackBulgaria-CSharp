using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    abstract class Animal
    {
        public string Name { get; protected set; }
        public abstract string Move();
        public abstract string Eat();
        public abstract string GiveBirth();
    }
}
