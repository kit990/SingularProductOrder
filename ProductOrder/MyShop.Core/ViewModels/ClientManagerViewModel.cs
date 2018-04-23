using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.ViewModels
{
    public class ClientManagerViewModel
    {
        public Client Client { get; set; }
        public IEnumerable<ClientAddressType> ClientAddressTypes { get; set; }
    }
}
