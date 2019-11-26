using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

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
        public ActionResult NewClient(string idPage)
        {
            ViewBag.OpenPage = idPage;
            ViewBag.Body = new SelectList(_carBody.WhereAllCarBody(), "Id", "Name");
            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewClient([Bind(Include = "Id,Surname,Name,PatronymicName,phone,DateRegistration,Email,discont,Recommendation,NumberCar,IdClientsGroups,IdBody,note,barcode")] ClientView clientView, bool Page)
        {
            if (TempData.ContainsKey("Mark") == true && TempData.ContainsKey("Model") == true)
            {
                clientView.Idmark = Int32.Parse(TempData["Mark"].ToString());
                clientView.Idmodel = Int32.Parse(TempData["Model"].ToString());

                if (ModelState.IsValid)
                {
                    ClientViewsBll client = Mapper.Map<ClientView, ClientViewsBll>(clientView);
                    _clientModules.Distribute(client);

                    if (Page)
                    {
                        return RedirectToAction("../ClientsOfCarWashViews/Client");
                    }
                    else
                    {
                        return RedirectToAction("../ClientsOfCarWashViews/NewOrder");
                    }
                }
            }

            ViewBag.Body = new SelectList(_carBody.WhereAllCarBody(), "Id", "Name");
            ViewBag.Group = new SelectList(_clientsGroups.GetClientsGroups(), "Id", "Name");

            return View();
        }
    }
}
