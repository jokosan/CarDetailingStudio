using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<ActionResult> Index()
        {
            return View(Mapper.Map<IEnumerable<ClientsGroupsView>>(await _clientsGroups.GetClientsGroups()));
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] ClientsGroupsView clientsGroupsView)
        {
            if (ModelState.IsValid)
            {
                ClientsGroupsBll clientsGroups = Mapper.Map<ClientsGroupsView, ClientsGroupsBll>(clientsGroupsView);
                await _clientsGroups.Insert(clientsGroups);

                return RedirectToAction("Index");
            }

            return View(clientsGroupsView);
        }

        // GET: ClientsGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientsGroupsView clientsGroupsView = Mapper.Map<ClientsGroupsView>(await _clientsGroups.GetId(id));

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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ClientsGroupsView clientsGroupsView)
        {
            if (ModelState.IsValid)
            {
                ClientsGroupsBll clientsGroups = Mapper.Map<ClientsGroupsView, ClientsGroupsBll>(clientsGroupsView);
                await _clientsGroups.Update(clientsGroups);
                return RedirectToAction("Index");
            }
            return View(clientsGroupsView);
        }
    }
}
