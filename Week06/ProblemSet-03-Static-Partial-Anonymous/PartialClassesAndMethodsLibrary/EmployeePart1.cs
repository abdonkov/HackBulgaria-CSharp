using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialClassesAndMethodsLibrary
{
    public partial class Employee
    {
        public string FirstName { get; }
        public string LastName { get; }
        public decimal Salary { get; }
        public string Position { get; }
        public decimal Bonus { get; }

        partial void Print();
    }
}
