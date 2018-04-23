using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.DataAccess.SQL;
using MyShop.Core.Models;
using System.Web;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;

namespace MyShop.Services
{
    public class BasketService: IBasketService
    {
        //Read a cookie from the user and look for a basket ID. The load the basket
        SQLRepository<Basket> basketContext;
        SQLRepository<Product> productContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(SQLRepository<Basket> basketContext, SQLRepository<Product> productContext)
        {
            this.basketContext = basketContext;
            this.productContext = productContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!String.IsNullOrEmpty(basketId))
                {
                    //Load basket from the basket context
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            //Write cookie to users machine
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            //Cookie expires tommorow
            cookie.Expires = DateTime.Now.AddDays(1);

            //Add cookie to browser
            httpContext.Response.Cookies.Add(cookie);
            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            //Check if item exists in basket
            if (item == null)
            {
                //create new item
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = 1
                };

                //Add the item to the basket
                basket.BasketItems.Add(item);
            }
            else
            {
                //Increment Item count
                item.Quantity = item.Quantity + 1;
            }

            //Commit all changes to the database
            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            //Get basket from the database if there are items in it
            Basket basket = GetBasket(httpContext, false);
            if (basket != null)
            {
                var result = (from b in basket.BasketItems
                              join p in productContext.Collection() on
                              b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ProductName = p.Name,
                                  Image = p.Image,
                                  Price = p.Price
                              }).ToList();
                return result;
            }
            else
            {
                //No basket found. Return empty basket object
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel GetBucketSummary(HttpContextBase httpContext)
        {
            //Get our basket only if it contains items
            Basket basket = GetBasket(httpContext, false);

            //We are setting it to 0 by default. 0 items with a basket total of 0
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                //Calculate amount of items in basket.[? will allow us to store null value in variable. Wont break if null returned]
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();
                //Calculate the total price of all the items in the basket
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection() on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }
    }
}
