using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLibrary
{
    public class CategoryWithProduct
    {
        public string CategoryName { get; set; }
        public List<string> ProductNames { get; set; }
        public int CategoryId { get; set; }

        public CategoryWithProduct(string categoryName, List<string> productNames, int categoryId)
        {
            CategoryName = categoryName;
            ProductNames = productNames;
            CategoryId = categoryId;
        }
    }
}
