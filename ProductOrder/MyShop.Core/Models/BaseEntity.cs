using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    //Setting to abstract to prevent an instance of it being created. It is a class that I want must only be implemented by other classes.
    public abstract class BaseEntity
    {
        //Properties
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        //Constructor
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
