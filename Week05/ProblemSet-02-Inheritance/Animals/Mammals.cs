using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    class Mammals : Animal
    {
        public Mammals()
        {
            Name = "mammal";
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
            return string.Format("The {0} is walking!", Name);
        }

        public virtual string Greet()
        {
            return string.Format("The {0} is greeting!", Name);
        }
    }
}
