using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackCompanyLibrary
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }

        public Order(int id, DateTime dateOfOrder, int customerId, double totalPrice)
        {
            Id = id;
            DateOfOrder = dateOfOrder;
            CustomerId = customerId;
            TotalPrice = totalPrice;
        }
    }
}
