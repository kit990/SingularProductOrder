using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int BasketCount { get; set; }
        public decimal BasketTotal { get; set; }

        //Default Constructor to set some default values
        public BasketSummaryViewModel()
        {

        }
        public BasketSummaryViewModel(int basketCount, decimal basketTotal)
        {
            BasketCount = basketCount;
            BasketTotal = basketTotal;
        }
    }
}
