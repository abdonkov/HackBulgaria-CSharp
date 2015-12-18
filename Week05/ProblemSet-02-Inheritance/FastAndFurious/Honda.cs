using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastAndFurious
{
    class Honda : Car
    {
        public Honda()
        {
            mileage = 0;
        }

        public override bool IsEcoFriendly(bool testing)
        {
            return true;
        }
    }
}
