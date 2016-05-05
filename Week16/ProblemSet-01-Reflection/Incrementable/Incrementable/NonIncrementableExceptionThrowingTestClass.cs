using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incrementable
{
    class NonIncrementableExceptionThrowingTestClass
    {
        [Incrementable]
        public int X { get; set; }

        [Incrementable]
        public int Y { get; set; }

        [Incrementable]
        public int Z { get; private set; }

        [Incrementable]
        public string Smth { get; set; }

        public NonIncrementableExceptionThrowingTestClass(int x, int y, int z, string smth)
        {
            X = x;
            Y = y;
            Z = z;
            Smth = smth;
        }
    }
}
