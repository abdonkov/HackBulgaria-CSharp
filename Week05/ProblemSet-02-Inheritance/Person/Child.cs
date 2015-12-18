using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Child : Person
    {
        private List<Toy> toys;
        public Child(string name, string gender, params Toy[] toys)
        {
            Name = name;
            Gender = gender;
            this.toys = new List<Toy>();
            foreach (Toy toy in toys)
            {
                this.toys.Add(toy);
            }
        }

        public Child(string name, string gender)
        {
            Name = name;
            Gender = gender;
            this.toys = new List<Toy>();
        }

        public override string DailyStuff()
        {
            return "Play!";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Name: ");
            sb.AppendLine(Name);
            sb.Append("Gender: ");
            sb.AppendLine(Gender);
            sb.Append("Daily stuff: ");
            sb.AppendLine(DailyStuff());
            int i = 1;
            foreach (Toy toy in toys)
            {
                sb.AppendLine(string.Format("Toy {0}:", i));
                sb.Append(toy.ToString());
                i++;
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
