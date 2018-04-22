using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;

        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: Product Category Manager
        public ActionResult Index()
        {
            List<ProductCategory> productCategory = context.Collection().ToList();
            return View(productCategory);
        }

        //To display the page
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        //Add product
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            //Check if validation has failed
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load product category onto the edit page
        public ActionResult Edit(string Id)
        {
            //Load the product category from the DB by ID
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        //Carry out the update
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            //Load product category we are editing from the DB
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Check if validation has been passed
                if (!ModelState.IsValid)
                {
                    //Return main page with product that will indicate validation issues.
                    return View(productCategoryToEdit);
                }

                productCategoryToEdit.CategoryName = productCategory.CategoryName;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load product onto the delete page
        public ActionResult Delete(string Id)
        {
            //Load the product from the DB by ID
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            //Check if the record to delete exists.
            if (productCategoryToDelete == null)
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