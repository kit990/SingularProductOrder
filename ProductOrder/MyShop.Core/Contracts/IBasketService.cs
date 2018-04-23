﻿using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService
    {
        //My method signatures
        void AddToBasket(HttpContextBase httpContext, string productId);

        void RemoveFromBasket(HttpContextBase httpContext, string itemId);

        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);

        BasketSummaryViewModel GetBucketSummary(HttpContextBase httpContext);
    }
}
