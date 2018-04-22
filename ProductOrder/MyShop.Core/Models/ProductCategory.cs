using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        //Properties
        public string Id { get; set; }
        public string CategoryName { get; set; }

        //Constructor
        public ProductCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
