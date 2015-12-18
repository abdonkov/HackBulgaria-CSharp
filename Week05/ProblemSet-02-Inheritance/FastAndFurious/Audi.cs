using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastAndFurious
{
    class Audi : Car
    {
        public Audi()
        {
            mileage = 0;
        }

        public int Mileage { get { return mileage; } }
        public override bool IsEcoFriendly(bool testing)
        {
            return true;
        }
    }
}
