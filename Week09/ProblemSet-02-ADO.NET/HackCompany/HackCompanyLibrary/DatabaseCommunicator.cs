using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackCompanyLibrary
{
    public class DatabaseCommunicator
    {
        private string connectionString;

        public DatabaseCommunicator(string connectionString)
        {
            this.connectionString = connectionString;

            var sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            sqlCon.Close();
            sqlCon.Dispose();
        }

        private int ExecuteNonQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                return cmd.ExecuteNonQuery();
            }
        }

        public void UpdateOrInsert(Category cat)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM Categories
                                        WHERE CategoryID = '{cat.Id}')
                                BEGIN
                                    UPDATE Categories
                                    SET Name = '{cat.Name}'
                                    WHERE CategoryID = '{cat.Id}'
                                END
                              ELSE
                                BEGIN
                                    INSERT INTO Categories
                                    VALUES ('{cat.Id}', '{cat.Name}');
                                END";

            ExecuteNonQuery(query);
        }

        public void UpdateOrInsert(Customer cust)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM Customers
                                        WHERE CustomerID = {cust.Id})
                                BEGIN
                                    UPDATE Customers
                                    SET FirstName = '{cust.FirstName}'
                                    SET LastName = '{cust.LastName}'
                                    SET Email = '{cust.Email}'
                                    SET Address = '{cust.Address}'
                                    SET Discount = {cust.Discount}
                                    WHERE CustomerID = {cust.Id}
                                END
                              ELSE
                                BEGIN
                                    SET IDENTITY_INSERT Customers ON
                                    INSERT INTO Customers
                                    VALUES ({cust.Id}, '{cust.FirstName}', '{cust.LastName}', '{cust.Email}', '{cust.Address}', {cust.Discount});
                                    SET IDENTITY_INSERT Customers OFF
                                END";

            ExecuteNonQuery(query);
        }

        public void UpdateOrInsert(Department dep)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM Departments
                                        WHERE DepartmentID = {dep.Id})
                                BEGIN
                                    UPDATE Departments
                                    SET Name = '{dep.Name}'
                                    WHERE DepartmentID = {dep.Id}
                                END
                              ELSE
                                BEGIN
                                    SET IDENTITY_INSERT Departments ON
                                    INSERT INTO Departments
                                    VALUES ({dep.Id}, '{dep.Name}');
                                    SET IDENTITY_INSERT Departments OFF
                                END";

            ExecuteNonQuery(query);
        }

        public void UpdateOrInsert(Employee emp)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM Employees
                                        WHERE EmployeeID = {emp.Id})
                                BEGIN
                                    UPDATE Employees
                                    SET FirstName = '{emp.FirstName}'
                                    SET LastName = '{emp.LastName}'
                                    SET Email = '{emp.Email}'
                                    SET BirthDate = '{emp.BirthDate.ToString(@"yyyy-MM-dd")}'
                                    SET Manager = {emp.ManagerId}
                                    SET Department = {emp.ManagerId}
                                    WHERE EmployeeID = {emp.Id}
                                END
                              ELSE
                                BEGIN
                                    SET IDENTITY_INSERT Employees ON
                                    INSERT INTO Employees
                                    VALUES ({emp.Id}, '{emp.FirstName}', '{emp.LastName}', '{emp.Email}', '{emp.BirthDate.ToString(@"dd-MM-yyyy")}', {emp.ManagerId}, {emp.DepartmentId});
                                    SET IDENTITY_INSERT Employees OFF
                                END";

            ExecuteNonQuery(query);
        }

        public void UpdateOrInsert(OrderProduct ordProd)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM OrderProducts
                                        WHERE OrderID = {ordProd.OrderId} && ProductID = {ordProd.ProductId})
                                BEGIN
                                    UPDATE OrderProducts
                                    SET Quantity = {ordProd.Quantity}
                                    WHERE OrderID = {ordProd.OrderId} && ProductID = {ordProd.ProductId}
                                END
                              ELSE
                                BEGIN
                                    INSERT INTO OrderProducts
                                    VALUES ({ordProd.OrderId}, {ordProd.ProductId}, {ordProd.Quantity});
                                END";

            ExecuteNonQuery(query);
        }

        public void UpdateOrInsert(Order ord)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM Orders
                                        WHERE OrderID = {ord.Id})
                                BEGIN
                                    UPDATE Orders
                                    SET DateOfOrder = '{ord.DateOfOrder.ToString(@"yyyy-MM-dd HH:mm:ss")}'
                                    SET Customer = {ord.CustomerId}
                                    SET TotalPrice = {ord.TotalPrice}
                                    WHERE OrderID = {ord.Id}
                                END
                              ELSE
                                BEGIN
                                    SET IDENTITY_INSERT Orders ON
                                    INSERT INTO Orders
                                    VALUES ({ord.Id}, '{ord.DateOfOrder.ToString(@"yyyy-MM-dd HH:mm:ss")}', {ord.CustomerId}, {ord.TotalPrice});
                                    SET IDENTITY_INSERT Orders OFF
                                END";

            ExecuteNonQuery(query);
        }

        public void UpdateOrInsert(Product prod)
        {
            string query = $@"IF EXISTS (SELECT TOP 1 *
                                        FROM Products
                                        WHERE ProductID = {prod.Id})
                                BEGIN
                                    UPDATE Products
                                    SET Name = '{prod.Name}'
                                    SET Price = {prod.Price}
                                    SET Category = {prod.CategoryId}
                                    WHERE ProductID = {prod.Id}
                                END
                              ELSE
                                BEGIN
                                    SET IDENTITY_INSERT Products ON
                                    INSERT INTO Products
                                    VALUES ({prod.Id}, '{prod.Name}', {prod.Price}, {prod.CategoryId});
                                    SET IDENTITY_INSERT Products OFF
                                END";

            ExecuteNonQuery(query);
        }

        public bool DeleteCategory(int Id)
        {
            var query = $@"DELETE FROM Categories
                           WHERE CategoryID = {Id}";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch(SqlException)
            {
                return false;
            }
        }

        public bool DeleteCustomer(string Id)
        {
            var query = $@"DELETE FROM Customers
                           WHERE CustomerID = '{Id}'";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteDepartment(int Id)
        {
            var query = $@"DELETE FROM Departments
                           WHERE DepartmentID = {Id}";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteEmployee(int Id)
        {
            var query = $@"DELETE FROM Employees
                           WHERE EmployeeID = {Id}";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteOrderProduct(int orderId, int productId)
        {
            var query = $@"DELETE FROM OrderProducts
                           WHERE OrderID = {orderId} && ProductId = {productId}";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteOrder(int Id)
        {
            var query = $@"DELETE FROM Orders
                           WHERE OrderID = {Id}";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteProduct(int Id)
        {
            var query = $@"DELETE FROM Products
                           WHERE ProductID = {Id}";

            try
            {
                int affectedRows = ExecuteNonQuery(query);
                return affectedRows > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        private object GetObjectOrNull(object dbObj)
        {
            if (dbObj is DBNull) return null;
            else return dbObj;
        }

        public List<Category> GetCategories()
        {
            var rows = new List<Category>();

            var query = @"SELECT * FROM Categories";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        rows.Add(new Category(
                                (string)reader["CategoryID"],
                                (string)reader["Name"]
                                ));
                    }
                }
            }

            return rows;
        }

        public List<Customer> GetCustomers()
        {
            var rows = new List<Customer>();

            var query = @"SELECT * FROM Customers";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new Customer(
                                (int)reader["CustomerID"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)GetObjectOrNull(reader["Email"]),
                                (string)GetObjectOrNull(reader["Address"]),
                                (double?)GetObjectOrNull(reader["Discount"])
                                ));
                    }
                }
            }

            return rows;
        }

        public List<Department> GetDepartments()
        {
            var rows = new List<Department>();

            var query = @"SELECT * FROM Departments";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new Department(
                                (int)reader["DepartmentID"],
                                (string)reader["Name"]
                                ));
                    }
                }
            }

            return rows;
        }

        public List<Employee> GetEmployees()
        {
            var rows = new List<Employee>();

            var query = @"SELECT * FROM Employees";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new Employee(
                                (int)reader["EmployeeID"],
                                (string)reader["FirstName"],
                                (string)reader["LastName"],
                                (string)GetObjectOrNull(reader["Email"]),
                                (DateTime)reader["BirthDate"],
                                (int?)GetObjectOrNull(reader["Manager"]),
                                (int?)GetObjectOrNull(reader["Department"])
                                ));
                    }
                }
            }

            return rows;
        }

        public List<OrderProduct> GetOrderProducts()
        {
            var rows = new List<OrderProduct>();

            var query = @"SELECT * FROM OrderProducts";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new OrderProduct(
                                (int)reader["OrderID"],
                                (int)reader["ProductID"],
                                (int)reader["Quantity"]
                                ));
                    }
                }
            }

            return rows;
        }

        public List<Order> GetOrders()
        {
            var rows = new List<Order>();

            var query = @"SELECT * FROM Orders";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new Order(
                                (int)reader["OrderID"],
                                (DateTime)reader["DateOfOrder"],
                                (int)reader["Customer"],
                                (double)reader["TotalPrice"]
                                ));
                    }
                }
            }

            return rows;
        }

        public List<Product> GetProducts()
        {
            var rows = new List<Product>();

            var query = @"SELECT * FROM Products";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new Product(
                                (int)reader["ProductID"],
                                (string)reader["Name"],
                                (decimal)reader["Price"],
                                (string)GetObjectOrNull(reader["Category"])
                                ));
                    }
                }
            }

            return rows;
        }

        public Category GetCategory(string Id)
        {
            var query = $@"SELECT TOP 1 * FROM Categories
                           WHERE CategoryID = '{Id}'";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Category(
                                            (string)reader["CategoryID"],
                                            (string)reader["Name"]
                                            );
                    else return null;
                }
            }
        }

        public Customer GetCustomer(int Id)
        {
            var query = $@"SELECT TOP 1 * FROM Customers
                           WHERE CustomerID = {Id}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Customer(
                                            (int)reader["CustomerID"],
                                            (string)reader["FirstName"],
                                            (string)reader["LastName"],
                                            (string)GetObjectOrNull(reader["Email"]),
                                            (string)GetObjectOrNull(reader["Address"]),
                                            (double?)GetObjectOrNull(reader["Discount"])
                                            );
                    else return null;
                }
            }
        }

        public Department GetDepartment(int Id)
        {
            var query = $@"SELECT TOP 1 * FROM Departments
                           WHERE DepartmentID = {Id}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Department(
                                            (int)reader["DepartmentID"],
                                            (string)reader["Name"]
                                            );
                    else return null;
                }
            }
        }

        public Employee GetEmployee(int Id)
        {
            var query = $@"SELECT TOP 1 * FROM Employees
                           WHERE EmployeeID = {Id}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Employee(
                                            (int)reader["EmployeeID"],
                                            (string)reader["FirstName"],
                                            (string)reader["LastName"],
                                            (string)GetObjectOrNull(reader["Email"]),
                                            (DateTime)reader["BirthDate"],
                                            (int?)GetObjectOrNull(reader["Manager"]),
                                            (int?)GetObjectOrNull(reader["Department"])
                                            );
                    else return null;
                }
            }
        }

        public OrderProduct GetOrderProduct(int orderId, int productId)
        {
            var query = $@"SELECT TOP 1 * FROM OrderProducts
                           WHERE OrderID = {orderId} && ProductID = {productId}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new OrderProduct(
                                            (int)reader["OrderID"],
                                            (int)reader["ProductID"],
                                            (int)reader["Quantity"]
                                            );
                    else return null;
                }
            }
        }

        public Order GetOrder(int Id)
        {
            var query = $@"SELECT TOP 1 * FROM Orders
                           WHERE OrderID = {Id}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Order(
                                        (int)reader["OrderID"],
                                        (DateTime)reader["DateOfOrder"],
                                        (int)reader["Customer"],
                                        (double)reader["TotalPrice"]
                                        );
                    else return null;
                }
            }
        }

        public Product GetProduct(int Id)
        {
            var query = $@"SELECT TOP 1 * FROM Orders
                           WHERE OrderID = {Id}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return new Product(
                                        (int)reader["ProductID"],
                                        (string)reader["Name"],
                                        (decimal)reader["Price"],
                                        (string)GetObjectOrNull(reader["Category"])
                                        );
                    else return null;
                }
            }
        }

    }
}
