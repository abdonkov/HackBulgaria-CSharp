using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incrementable
{
    public class NonIncrementableTestClass
    {
        [Incrementable]
        public int X { get; set; }

        [Incrementable]
        public int Y { get; set; }

        [Incrementable]
        public int Z { get; private set; }

        public string Smth { get; set; }

        public NonIncrementableTestClass(int x, int y, int z, string smth)
        {
            X = x;
            Y = y;
            Z = z;
            Smth = smth;
        }
    }
}
