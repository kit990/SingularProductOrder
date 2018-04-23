using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.SQL;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        DataContext dc;
        SQLRepository<Product> context;
        SQLRepository<ProductCategory> productCategories;

        //Constructor
        public ProductManagerController()
        {
            dc = new DataContext();
            context = new SQLRepository<Product>(dc);
            productCategories = new SQLRepository<ProductCategory>(dc);
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //To display the page
        public ActionResult Create()
        {
            //Here I return both the Product and Product Category to the view to populate the dropdown list
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            //Product product = new Product();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //Add product
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            //Check if validation has failed
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load product onto the edit page
        public ActionResult Edit(string Id)
        {
            //Load the product from the DB by ID
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Return to view
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        //Carry out the update
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            //Load product we are editing from the DB
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Check if validation has been passed
                if (!ModelState.IsValid)
                {
                    //Return main page with product that will indicate validation issues.
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Load product onto the delete page
        public ActionResult Delete(string Id)
        {
            //Load the product from the DB by ID
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            //Check if the record to delete exists.
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}