using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLibrary
{
    public class Database
    {
        List<Product> products;
        List<Order> orders;
        List<Category> categories;
        
        public Database()
        {
            products = new List<Product>();
            orders = new List<Order>();
            categories = new List<Category>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void AddCategory(Category category)
        {
            categories.Add(category);
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        public List<Category> GetCategories()
        {
            return categories;
        }
    }
}
