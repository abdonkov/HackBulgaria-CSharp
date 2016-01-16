using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLibrary
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public List<int> Products { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(int orderId, string name, List<int> products, DateTime orderDate)
        {
            OrderId = orderId;
            Name = name;
            Products = products;
            OrderDate = orderDate;
        }
    }
}
