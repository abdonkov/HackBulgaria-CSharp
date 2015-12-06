using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class Order
    {
        private Dictionary<int, int> orderedItems;
        public Dictionary<int, int> OrderedItems { get { return orderedItems; } }
        public Order(Dictionary<int, int> orderedItems)
        {
            this.orderedItems = new Dictionary<int, int>();
            foreach (var item in orderedItems)
            {
                this.orderedItems.Add(item.Key, item.Value);
            }
        }
    }
}
