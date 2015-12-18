using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    abstract class Person
    {
        public string Name { get; protected set; }
        public string Gender { get; protected set; }
        public abstract string DailyStuff();
    }
}
