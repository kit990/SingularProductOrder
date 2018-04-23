using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class ClientManagerController : Controller
    {
        InMemoryRepository<Client> context;
        InMemoryRepository<ClientAddressType> clientAddressTypes;

        public ClientManagerController()
        {
            context = new InMemoryRepository<Client>();
            clientAddressTypes = new InMemoryRepository<ClientAddressType>();
        }

        // GET: ClientManager
        public ActionResult Index()
        {
            List<Client> client = context.Collection().ToList();
            return View(client);
        }

        //To display the page. Return empty object
        public ActionResult Create()
        {
            //Here I return both the Client and Client Address Type to the view to populate the dropdown list
            ClientManagerViewModel viewModel = new ClientManagerViewModel();
            viewModel.Client = new Client();
            viewModel.ClientAddressTypes = clientAddressTypes.Collection();
            return View(viewModel);
        }

        //Add product
        [HttpPost]
        public ActionResult Create(Client client)
        {
            //Check if validation has failed
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            else
            {
                context.Insert(client);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load client onto the edit page
        public ActionResult Edit(string Id)
        {
            //Load the client category from the DB by ID
            Client client = context.Find(Id);
            if (client == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Here I return both the Client and Client Address Type to the view to populate the dropdown list
                ClientManagerViewModel viewModel = new ClientManagerViewModel();
                viewModel.Client = client;
                viewModel.ClientAddressTypes = clientAddressTypes.Collection();
                return View(viewModel);
            }
        }

        //Carry out the update
        [HttpPost]
        public ActionResult Edit(Client client, string Id)
        {
            //Load client we are editing from the DB
            Client clientToEdit = context.Find(Id);

            if (clientToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Check if validation has been passed
                if (!ModelState.IsValid)
                {
                    //Return main page with client that will indicate validation issues.
                    return View(clientToEdit);
                }

                clientToEdit.Name = client.Name;
                clientToEdit.Surname = client.Surname;
                clientToEdit.AddressType = client.AddressType;
                clientToEdit.StreetAddress = client.StreetAddress;
                clientToEdit.Suburb = client.Suburb;
                clientToEdit.City = client.City;
                clientToEdit.PostalCode = client.PostalCode;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        //Load client onto the delete page
        public ActionResult Delete(string Id)
        {
            //Load the client from the DB by ID
            Client client = context.Find(Id);
            if (client == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(client);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Client clientToDelete = context.Find(Id);
            //Check if the record to delete exists.
            if (clientToDelete == null)
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