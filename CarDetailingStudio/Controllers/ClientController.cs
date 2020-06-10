
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Controllers
{
    public class ClientController : Controller
    {
        private IClientModules _clientModules;
        private ICarBodyServices _carBody;
        private ICarMarkServices _carMark;
        private ICarModelServices _carModel;
        private IClientsGroupsServices _clientsGroups;

        public ClientController(IClientModules clientModules, ICarBodyServices carBody, ICarMarkServices carMark,
                               ICarModelServices carModel, IClientsGroupsServices clientsGroupsServices)
        {
            _clientModules = clientModules;
            _carBody = carBody;
            _carMark = carMark;
            _carModel = carModel;
            _clientsGroups = clientsGroupsServices;
        }


        // GET: Client/Create
        public async Task<ActionResult> NewClient(string idPage)
        {
            ViewBag.OpenPage = idPage;
            ViewBag.Body = new SelectList(await _carBody.GetTableAll(), "Id", "Name");
            ViewBag.Group = new SelectList(await _clientsGroups.GetClientsGroups(), "Id", "Name");

            if ("Checkout" == idPage)
            {
                TempData["Checkout"] = idPage;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewClient([Bind(Include = "Id,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumberCar,IdClientsGroups,IdBody,note,barcode")] ClientView clientView, bool Page, int? Service)
        {
            if (TempData.ContainsKey("Mark") && TempData.ContainsKey("Model") && clientView.IdBody != null)
            {
                clientView.Idmark = Int32.Parse(TempData["Mark"].ToString());
                clientView.Idmodel = Int32.Parse(TempData["Model"].ToString());

                if (ModelState.IsValid)
                {
                    if ((TempData.ContainsKey("Checkout")))
                    {
                        string typeService = TempData["Checkout"].ToString();

                        if (Service != null)
                        {
                            ClientViewsBll client = Mapper.Map<ClientView, ClientViewsBll>(clientView);
                            int idNewClient =  await _clientModules.Distribute(client);

                            var carBody = Mapper.Map<CarBodyView>(await _carBody.SelectId(Convert.ToInt32(clientView.IdBody)));
                            return RedirectToAction("NewOrder", "ClientsOfCarWashViews", new RouteValueDictionary(new { id = idNewClient, body = carBody.Name, Services = Service }));
                        }
                        else
                        {
                            return RedirectToAction("NewClient", "Client", new RouteValueDictionary(new { idPage = typeService }));
                        }
                    }
                    else
                    {
                        if (Page)
                        {
                            ClientViewsBll client = Mapper.Map<ClientView, ClientViewsBll>(clientView);
                            int idNewClient = await _clientModules.Distribute(client);

                            var carBody = Mapper.Map<CarBodyView>(await _carBody.SelectId(Convert.ToInt32(clientView.IdBody)));

                            return RedirectToAction("../ClientsOfCarWashViews/Client");
                        }
                        else
                        {
                            return RedirectToAction("../Client/NewOrder");
                        }
                    }
                }
            }

            ViewBag.Body = new SelectList(await _carBody.GetTableAll(), "Id", "Name");
            ViewBag.Group = new SelectList(await _clientsGroups.GetClientsGroups(), "Id", "Name");

            if ((TempData.ContainsKey("Checkout")))
            {
                string typeService = TempData["Checkout"].ToString();
                return RedirectToAction("NewClient", "Client", new RouteValueDictionary(new { idPage = typeService }));
            }
            else
            {
                return View();
            }
        }
    }
}
