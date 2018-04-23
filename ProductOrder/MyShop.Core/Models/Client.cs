using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Client : BaseEntity
    {
        //Properties
        [DisplayName("First Name")]
        public string Name { get; set; }

        [DisplayName("Last Name")]
        public string Surname { get; set; }

        [DisplayName("Address Type")]
        public string AddressType { get; set; }

        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }

        public string Suburb { get; set; }

        [DisplayName("City/Town")]
        public string City { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

    }
}
