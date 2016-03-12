using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeEditor.Models
{
    public partial class Employee
    {
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string ManagerName
        {
            get
            {
                return Employee1?.FullName ?? "None";
            }
        }

        public string DepartmentName
        {
            get
            {
                return Department1?.Name ?? "None";
            }
        }
    }

    public partial class Department
    {
        public string DepartmentName
        {
            get
            {
                return Name;
            }
        }
    }
}