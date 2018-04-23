using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseEntity
    {
        //List of basket items set to virtual so that Entity Framewok will know to load all the
        //Basket items from the database when we load the basket
        public virtual ICollection<BasketItem> BasketItems { get; set; }

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}