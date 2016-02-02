using HackCompanyLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackCompanyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var conString = ConfigurationManager.ConnectionStrings["HackCompanyDB"].ConnectionString;
            var dbCom = new DatabaseCommunicator(conString);

            Console.WriteLine(dbCom.GetCategories().Count);
            Console.WriteLine(dbCom.GetCustomers().Count);
            Console.WriteLine(dbCom.GetDepartments().Count);
            Console.WriteLine(dbCom.GetEmployees().Count);
            Console.WriteLine(dbCom.GetOrderProducts().Count);
            Console.WriteLine(dbCom.GetOrders().Count);
            Console.WriteLine(dbCom.GetProducts().Count);

            Console.WriteLine("Retrieved all database entries successfully!");
            Console.ReadKey();
        }
    }
}
