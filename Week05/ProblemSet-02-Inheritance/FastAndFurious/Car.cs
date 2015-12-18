using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastAndFurious
{
    abstract class Car
    {
        protected int mileage;
        public abstract bool IsEcoFriendly(bool testing);
        public virtual void MoveNKilometers(int N)
        {
            mileage += N;
        }
    }
}
