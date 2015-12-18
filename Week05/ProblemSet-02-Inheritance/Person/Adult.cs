using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    class Adult : Person
    {
        private List<Child> children;

        public Adult(string name, string gender, params Child[] children)
        {
            Name = name;
            Gender = gender;
            this.children = new List<Child>();
            foreach (Child child in children)
            {
                this.children.Add(child);
            }
        }

        public Adult(string name, string gender)
        {
            Name = name;
            Gender = gender;
            this.children = new List<Child>();
        }

        public override string DailyStuff()
        {
            return "Work!";
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
            foreach (Child child in children)
            {
                sb.AppendLine(string.Format("Child {0}:", i));
                sb.Append(child.ToString());
                i++;
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
