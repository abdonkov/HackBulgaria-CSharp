using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incrementable
{
    [Incrementable]
    public class IncrementableTestClass
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; private set; }

        public string Smth { get; set; }

        public IncrementableTestClass(int x, int y, int z, string smth)
        {
            X = x;
            Y = y;
            Z = z;
            Smth = smth;
        }
    }
}
