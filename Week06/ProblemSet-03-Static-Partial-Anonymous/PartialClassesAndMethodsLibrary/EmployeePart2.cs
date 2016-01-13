using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialClassesAndMethodsLibrary
{
    public partial class Employee
    {
        partial void Print()
        {
            Console.WriteLine("{0} {1}", FirstName, LastName);
        }

        public decimal CalculateAllIncome()
        {
            return Salary + Bonus;
        }

        public decimal CalculateBalance()
        {
            return (Salary + Bonus) * 4 / 5;
        }
    }
}
