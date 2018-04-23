using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.SQL;

namespace MyShop.WebUI.Controllers
{
    public class ClientAddressTypeManagerController : Controller
    {
        DataContext dc;
        SQLRepository<ClientAddressType> context;
        public ClientAddressTypeManagerController()
        {
            dc = new DataContext();
            context = new SQLRepository<ClientAddressType>(dc);
        }

        // GET: ClientAddressTypeManager
        public ActionResult Index()
        {
            List<ClientAddressType> clientAddressType = context.Collection().ToList();
            return View(clientAddressType);
        }

        //To display the page. Return empty object
        public ActionResult Create()
        {
            ClientAddressType clientAddressType = new ClientAddressType();
            return View(clientAddressType);
        }

        //Add product
        [HttpPost]
        public ActionResult Create(ClientAddressType clientAddressType)
        {
            //Check if validation has failed
            if (!ModelState.IsValid)
            {
                return View(clientAddressType);
            }
            else
            {
                context.Insert(clientAddressType);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load client address type onto the edit page
        public ActionResult Edit(string Id)
        {
            //Load the client address type category from the DB by ID
            ClientAddressType clientAddressType = context.Find(Id);
            if (clientAddressType == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(clientAddressType);
            }
        }

        //Carry out the update
        [HttpPost]
        public ActionResult Edit(ClientAddressType clientAddressType, string Id)
        {
            //Load client we are editing from the DB
            ClientAddressType clientAddressTypeToEdit = context.Find(Id);

            if (clientAddressTypeToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Check if validation has been passed
                if (!ModelState.IsValid)
                {
                    //Return main page with client that will indicate validation issues.
                    return View(clientAddressTypeToEdit);
                }

                clientAddressTypeToEdit.AddressType = clientAddressType.AddressType;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load client address type onto the delete page
        public ActionResult Delete(string Id)
        {
            //Load the client from the DB by ID
            ClientAddressType clientAddressType = context.Find(Id);
            if (clientAddressType == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(clientAddressType);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ClientAddressType clientAddressTypeToDelete = context.Find(Id);
            //Check if the record to delete exists.
            if (clientAddressTypeToDelete == null)
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