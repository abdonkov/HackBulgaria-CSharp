using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    class Birds : Animal
    {
        public Birds()
        {
            Name = "bird";
        }

        public override string Eat()
        {
            return string.Format("The {0} is eating!", Name);
        }

        public override string GiveBirth()
        {
            return string.Format("The {0} is giving birth!", Name);
        }

        public override string Move()
        {
            return string.Format("The {0} is flying!", Name);
        }

        public virtual string MakeNest()
        {
            return string.Format("The {0} is making nest!", Name);
        }
    }
}
