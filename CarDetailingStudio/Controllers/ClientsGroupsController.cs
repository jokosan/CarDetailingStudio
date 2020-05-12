using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.Models;
using CarDetailingStudio.Models.ModelViews;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class ClientsGroupsController : Controller
    {
        private IClientsGroupsServices _clientsGroups;

        public ClientsGroupsController(IClientsGroupsServices clientsGroups)
        {
            _clientsGroups = clientsGroups;
        }

        // GET: ClientsGroups
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<ClientsGroupsView>>(_clientsGroups.GetClientsGroups()));
        }

        // GET: ClientsGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientsGroups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ClientsGroupsView clientsGroupsView)
        {
            if (ModelState.IsValid)
            {
                ClientsGroupsBll clientsGroups = Mapper.Map<ClientsGroupsView, ClientsGroupsBll>(clientsGroupsView);
                 _clientsGroups.Insert(clientsGroups);
             
                return RedirectToAction("Index");
            }

            return View(clientsGroupsView);
        }

        // GET: ClientsGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientsGroupsView clientsGroupsView = Mapper.Map<ClientsGroupsView>(_clientsGroups.GetId(id));

            if (clientsGroupsView == null)
            {
                return HttpNotFound();
            }

            return View(clientsGroupsView);
        }

        // POST: ClientsGroups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ClientsGroupsView clientsGroupsView)
        {
            if (ModelState.IsValid)
            {
                ClientsGroupsBll clientsGroups = Mapper.Map<ClientsGroupsView, ClientsGroupsBll>(clientsGroupsView);
                _clientsGroups.Update(clientsGroups);
                return RedirectToAction("Index");
            }
            return View(clientsGroupsView);
        }
    }
}
