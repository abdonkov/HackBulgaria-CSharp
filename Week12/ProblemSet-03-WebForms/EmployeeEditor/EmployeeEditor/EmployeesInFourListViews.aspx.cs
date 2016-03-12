using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmployeeEditor.Models;

namespace EmployeeEditor
{
    public partial class EmployeesInFourListViews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateListView();
            }
        }

        private void PopulateListView()
        {
            using (HackCompanyEntities db = new HackCompanyEntities())
            {
                var sortedEmployees = from e in db.Employees
                                orderby e.FirstName, e.LastName
                                select e;

                Dictionary<string, List<Employee>> employees = new Dictionary<string, List<Employee>>();
                employees.Add("A to G", new List<Employee>());
                employees.Add("H to N", new List<Employee>());
                employees.Add("O to T", new List<Employee>());
                employees.Add("U to Z", new List<Employee>());
                foreach (var emp in sortedEmployees)
                {
                    char firstLetter = char.ToUpper(emp.FirstName.First());
                    if (firstLetter >= 'A' && firstLetter <= 'G')
                        employees["A to G"].Add(emp);
                    else if (firstLetter >= 'H' && firstLetter <= 'N')
                        employees["H to N"].Add(emp);
                    else if(firstLetter >= 'O' && firstLetter <= 'T')
                        employees["O to T"].Add(emp);
                    else if (firstLetter >= 'U' && firstLetter <= 'Z')
                        employees["U to Z"].Add(emp);
                }

                employeeRepeater.DataSource = employees;
                employeeRepeater.DataBind();
            }
        }

        protected string CalculateBirthDateFormat(object item)
        {
            var employee = item as Employee;
            if (employee != null)
            {
                if (employee.BirthDate < new DateTime(1990, 1, 1, 0, 0, 0))
                    return "{0:yyyy-MM-dd}";
                else
                    return "{0:yyyy-MMM-dd}";
            }
            return "{0:yyyy-MM-dd}";
        }

        protected void defaulPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}