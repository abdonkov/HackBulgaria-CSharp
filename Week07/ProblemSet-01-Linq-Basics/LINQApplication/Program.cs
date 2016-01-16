using LINQLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.AddCategory(new Category(301, "Clothing"));
            db.AddCategory(new Category(306, "Drink"));
            db.AddCategory(new Category(308, "Food"));
            db.AddCategory(new Category(310, "Toilet Paper"));
            db.AddCategory(new Category(311, "Laptop"));
            db.AddCategory(new Category(314, "TV"));
            db.AddCategory(new Category(317, "Smartphone"));
            db.AddCategory(new Category(321, "Car"));
            db.AddCategory(new Category(325, "Glasses"));
            db.AddCategory(new Category(328, "Book"));

            db.AddProduct(new Product("Dress", 1, 301));
            db.AddProduct(new Product("T-shirt", 3, 301));
            db.AddProduct(new Product("Water", 5, 306));
            db.AddProduct(new Product("Coca-Cola", 8, 306));
            db.AddProduct(new Product("Cucumber", 12, 308));
            db.AddProduct(new Product("Tomato", 15, 308));
            db.AddProduct(new Product("Cabbage", 17, 308));
            db.AddProduct(new Product("Zebra", 20, 310));
            db.AddProduct(new Product("Emeka", 21, 310));
            db.AddProduct(new Product("Acer", 25, 311));
            db.AddProduct(new Product("Lenovo", 27, 311));
            db.AddProduct(new Product("Samsung", 29, 314));
            db.AddProduct(new Product("Filips", 32, 314));
            db.AddProduct(new Product("Samsung", 35, 317));
            db.AddProduct(new Product("Apple", 38, 317));
            db.AddProduct(new Product("Sony", 40, 317));
            db.AddProduct(new Product("Mercedes", 41, 321));
            db.AddProduct(new Product("Audi", 44, 321));
            db.AddProduct(new Product("Lada", 47, 321));
            db.AddProduct(new Product("Rayban", 51, 325));
            db.AddProduct(new Product("Harry Potter", 56, 328));
            db.AddProduct(new Product("Lord of the Rings", 58, 328));
            db.AddProduct(new Product("C# for Dummies", 61, 328));

            db.AddOrder(
                new Order
                (   
                    1,
                    "order1",
                    new List<int>() { 5, 17, 27, 21, 61 },
                    DateTime.Now.AddDays(-1).AddHours(2)
                ));
            db.AddOrder(
                new Order
                (
                    4,
                    "order4",
                    new List<int>() { 3, 25, 40, 58, 20, 1, 5 },
                    DateTime.Now.AddDays(1).AddHours(2)
                ));
            db.AddOrder(
                new Order
                (
                    9,
                    "order9",
                    new List<int>() { 41, 3, 47, 5, 15, 38, 5 },
                    DateTime.Now.AddDays(2).AddHours(-12)
                ));
            db.AddOrder(
                new Order
                (
                    12,
                    "order12",
                    new List<int>() { 32, 20, 44, 56, 12, 5 },
                    DateTime.Now.AddDays(1).AddHours(-5)
                ));

            //All products with ids between 15 and 30
            var query1 = from product in db.GetProducts()
                         where product.ProductId >= 15
                         && product.ProductId <= 30
                         select product;

            //All categories with ids between 105 and 125
            var query2 = from category in db.GetCategories()
                         where category.CategoryId >= 105
                         && category.CategoryId <= 125
                         select category;

            //First 15 orders sorted by order name
            var query3 = from order in db.GetOrders()
                         orderby order.Name
                         select order;

            //First 3 orders which contains
            //productId 5, sorted by order date
            var query4 = from order in db.GetOrders()
                         where order.Products.Contains(5)
                         orderby order.OrderDate
                         select order;

            Console.WriteLine("Orders containing productId 5, sorted by order date");
            Console.WriteLine("| OrederId |     Name     |         Products         |      OrderDate     |");
            foreach (var order in query4)
            {
                Console.WriteLine("| {0,-8} | {1,-12} | {2,-24} | {3,-14} |",
                    order.OrderId, order.Name, string.Join(", ", order.Products), order.OrderDate);
            }

            Console.WriteLine();


            //All product with the name of the category
            //which they belong to, order by CategoryName
            var query5 = from product in db.GetProducts()
                         join category in db.GetCategories() on product.CategoryId equals category.CategoryId
                         select new { ProductName = product.Name, ProductCategory = category.CategoryName };

            Console.WriteLine("Product name with their category name");
            Console.WriteLine("|     Product name     |     Category name     |");
            foreach (var product in query5)
            {
                Console.WriteLine("| {0,-20} | {1,-21} |",
                    product.ProductName, product.ProductCategory);
            }

            Console.WriteLine();

            //All categories together with their products
            var query6 = from category in db.GetCategories()
                         join product in db.GetProducts() on category.CategoryId equals product.CategoryId into productsGroup
                         let productNames = (from prod in productsGroup
                                             select prod.Name)
                         select new CategoryWithProduct(category.CategoryName, productNames.ToList(), category.CategoryId);

            //All orders with products with their category name
            //sorted by order date
            var query7 = from order in db.GetOrders()
                         let productsWithCategory = (from productId in order.Products
                                                     join product in db.GetProducts() on productId equals product.ProductId
                                                     join category in db.GetCategories() on product.CategoryId equals category.CategoryId
                                                     select new { ProductName = product.Name, CategoryName = category.CategoryName })
                         orderby order.OrderDate
                         select new
                         {
                             OrderId = order.OrderId,
                             OrderName = order.Name,
                             ProductsWithCategory = productsWithCategory,
                             OrderDate = order.OrderDate
                         };

            Console.WriteLine("Orders with product names and their category name, ordered by order date");
            Console.WriteLine("--------------------");
            foreach (var order in query7)
            {
                Console.WriteLine("OrderId: {0}", order.OrderId);
                Console.WriteLine("OrderName: {0}", order.OrderName);
                Console.WriteLine("OrderProducts:");
                foreach (var product in order.ProductsWithCategory)
                {
                    Console.WriteLine("Product: {0} -> {1}", product.ProductName, product.CategoryName);
                }
                Console.WriteLine("Order Date: {0}", order.OrderDate);
                Console.WriteLine("--------------------");
            }

            Console.ReadKey();
        }
    }
}
