using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.SQL
{
    public class DataContext: DbContext
    {
        //Constructor where we set the connection string (Will look in web.config or app.config)
        public DataContext()
            :base("DefaultConnection")
        {

        }
        //We will add these to the database
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientAddressType> ClientAddressType { get; set; }

        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
    }
}
