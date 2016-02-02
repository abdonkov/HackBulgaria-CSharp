using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackCompanyLibrary
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }

        public Product(int id, string name, decimal price, string categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
