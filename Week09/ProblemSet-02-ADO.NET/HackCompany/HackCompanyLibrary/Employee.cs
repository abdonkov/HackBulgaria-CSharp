using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackCompanyLibrary
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }

        public Employee(int id, string firstName, string lastName, string email, DateTime birthDate, int? managerId, int? departmentId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
            ManagerId = managerId;
            DepartmentId = departmentId;
        }
    }
}
