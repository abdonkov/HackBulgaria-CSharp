using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmployeeEditor.Models;

namespace EmployeeEditor
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateEmployeeGridView();
            }
        }

        private void PopulateEmployeeGridView()
        {
            using (HackCompanyEntities db = new HackCompanyEntities())
            {
                var employees = (from e in db.Employees
                                 select e).ToList();

                if (employees == null || employees.Count == 0)
                {
                    employees.Add(new Employee());
                    employeeGridView.DataSource = employees;
                    employeeGridView.DataBind();
                    employeeGridView.Rows[0].Visible = false;
                }
                else
                {
                    employeeGridView.DataSource = employees;
                    employeeGridView.DataBind();
                    employeeGridView.Rows[0].Visible = true;
                }

                if (employeeGridView.Rows.Count > 0)
                {
                    var fRow = employeeGridView.FooterRow;
                    DropDownList managersDDL = (DropDownList)fRow.FindControl("managerList");
                    DropDownList departmentsDDL = (DropDownList)fRow.FindControl("departmentList");
                    BindManagers(managersDDL, GetManagers());
                    BindDeparments(departmentsDDL, GetDepartments());
                }

            }
        }

        private List<Employee> GetManagers()
        {
            List<Employee> managers;

            using (HackCompanyEntities db = new HackCompanyEntities())
            {
                var boss = (from e in db.Employees
                            where e.Manager == null
                            select e).SingleOrDefault();
                if (boss == null)
                {
                    managers = new List<Employee>();
                    return managers;
                }

                managers = (from e in db.Employees
                            where e.Manager == boss.EmployeeID
                            select e).ToList();

                managers.Add(boss);

                return managers;
            }
        }

        private List<Department> GetDepartments()
        {
            List<Department> departments;

            using (HackCompanyEntities db = new HackCompanyEntities())
            {
                departments = (from d in db.Departments
                               select d).ToList();

                return departments;
            }
        }

        private void BindManagers(DropDownList managerDDL, List<Employee> managers)
        {
            managerDDL.Items.Clear();
            managerDDL.Items.Add(new ListItem() { Text = "Select manager:", Value = "0" });
            managerDDL.AppendDataBoundItems = true;

            managerDDL.DataTextField = "FullName";
            managerDDL.DataValueField = "EmployeeID";
            managerDDL.DataSource = managers;
            managerDDL.DataBind();
        }

        private void BindDeparments(DropDownList departmentDDL, List<Department> departments)
        {
            departmentDDL.Items.Clear();
            departmentDDL.Items.Add(new ListItem() { Text = "Select department:", Value = "0" });
            departmentDDL.AppendDataBoundItems = true;

            departmentDDL.DataTextField = "Name";
            departmentDDL.DataValueField = "DepartmentID";
            departmentDDL.DataSource = departments;
            departmentDDL.DataBind();
        }

        protected void employeeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                Page.Validate("Save");
                if (Page.IsValid)
                {
                    var fRow = employeeGridView.FooterRow;
                    TextBox firstNameTB = (TextBox)fRow.FindControl("firstNameTextBox");
                    TextBox lastNameTB = (TextBox)fRow.FindControl("lastNameTextBox");
                    TextBox emailTB = (TextBox)fRow.FindControl("emailTextBox");
                    TextBox birthDateTB = (TextBox)fRow.FindControl("birthDateTextBox");
                    DropDownList managersDDL = (DropDownList)fRow.FindControl("managerList");
                    DropDownList departmentsDDL = (DropDownList)fRow.FindControl("departmentList");
                    using (HackCompanyEntities db = new HackCompanyEntities())
                    {
                        db.Employees.Add(new Employee()
                        {
                            FirstName = firstNameTB.Text.Trim(),
                            LastName = lastNameTB.Text.Trim(),
                            Email = emailTB.Text,
                            BirthDate = DateTime.Parse(birthDateTB.Text),
                            Manager = int.Parse(managersDDL.SelectedItem.Value),
                            Department = int.Parse(departmentsDDL.SelectedItem.Value)
                        });

                        db.SaveChanges();
                        PopulateEmployeeGridView();
                    }
                }
            }
        }

        protected void employeeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            employeeGridView.EditIndex = e.NewEditIndex;
            PopulateEmployeeGridView();
        }

        protected void employeeGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            employeeGridView.EditIndex = -1;
            PopulateEmployeeGridView();
        }

        protected void employeeGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Page.Validate("Edit");

            if (!Page.IsValid) return;

            int employeeID = (int)employeeGridView.DataKeys[e.RowIndex]["EmployeeID"];

            var curRow = employeeGridView.Rows[e.RowIndex];

            TextBox firstNameTB = (TextBox)curRow.FindControl("firstNameTextBoxEdit");
            TextBox lastNameTB = (TextBox)curRow.FindControl("lastNameTextBoxEdit");
            TextBox emailTB = (TextBox)curRow.FindControl("emailTextBoxEdit");
            TextBox birthDateTB = (TextBox)curRow.FindControl("birthDateTextBoxEdit");

            using (HackCompanyEntities db = new HackCompanyEntities())
            {
                var employeeToUpdate = (from emp in db.Employees
                                        where emp.EmployeeID == employeeID
                                        select emp).SingleOrDefault();

                if (employeeToUpdate != null)
                {
                    employeeToUpdate.FirstName = firstNameTB.Text.Trim();
                    employeeToUpdate.LastName = lastNameTB.Text.Trim();
                    employeeToUpdate.Email = emailTB.Text;
                    employeeToUpdate.BirthDate = DateTime.Parse(birthDateTB.Text);

                    db.SaveChanges();
                    employeeGridView.EditIndex = -1;
                    PopulateEmployeeGridView();
                }
            }
        }

        protected void employeeGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int employeeID = (int)employeeGridView.DataKeys[e.RowIndex]["EmployeeID"];

            using (HackCompanyEntities db = new HackCompanyEntities())
            {
                var employeeToRemove = (from emp in db.Employees
                                        where emp.EmployeeID == employeeID
                                        select emp).SingleOrDefault();

                if (employeeToRemove != null)
                {
                    db.Employees.Remove(employeeToRemove);

                    db.SaveChanges();
                    PopulateEmployeeGridView();
                }
            }
        }

        protected void employeeListViewPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeesInFourListViews.aspx");
        }
    }
}