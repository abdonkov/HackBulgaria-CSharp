using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    class Reptiles : Animal
    {
        protected int lowestTemperature;
        protected int highestTemperature;

        public Reptiles()
        {
            Name = "reptile";
            lowestTemperature = 0;
            highestTemperature = 100;
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
            return string.Format("The {0} is walking or swiming!", Name);
        }

        public virtual string Temperature()
        {
            Random rand = new Random();
            return string.Format("The {0} changed its temperature to {1:N2}°C!", Name, rand.Next(lowestTemperature, highestTemperature) + rand.NextDouble());
        }
    }
}
