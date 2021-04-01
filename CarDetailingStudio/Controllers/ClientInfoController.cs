using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.Models;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using CarDetailingStudio.BLL.Model;
using System.Web.Routing;
using CarDetailingStudio.BLL.Services.Modules.Clients.Contract;

namespace CarDetailingStudio.Controllers
{
    public class ClientInfoController : Controller
    {
        private IClientInfoServices _clientInfo;
        private IClientsOfCarWashServices _clientsOfCarWash;
        private IClientsGroupsServices _clientsGroups;
        private IRemoveClient _removeClient;

        public ClientInfoController(IClientInfoServices clientInfo, IClientsOfCarWashServices clientsOfCarWash,
                                    IClientsGroupsServices clientsGroups, IRemoveClient removeClient)
        {
            _clientInfo = clientInfo;
            _clientsOfCarWash = clientsOfCarWash;
            _clientsGroups = clientsGroups;
            _removeClient = removeClient;
        }

        public async Task<ActionResult> ClientInfo(int idPaymentState)
        {
            if (idPaymentState == 2)
            {
                var resultBll = Mapper.Map<IEnumerable<ClientInfoView>>(await _clientInfo.ClientInfoAll());
                return View(resultBll.Where(x => x.Phone != null));
            }
            else
            {
                return RedirectToAction("Client", "ClientsOfCarWashViews", new RouteValueDictionary(new { idPaymentState = 1 }));
            }
        }

        // GET: ClientInfo/Details/5
        public async Task<ActionResult> ClientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClientInfoView clientInfoView = Mapper.Map<ClientInfoView>(await _clientInfo.ClientInfoGetId(id));
            var clientCar = Mapper.Map<IEnumerable<ClientsOfCarWashView>>(await _clientsOfCarWash.GetAll(id));

            if (clientCar != null)
            {
                ViewBag.ClientCar = clientCar;
            }

            if (clientInfoView == null)
            {
                return HttpNotFound();
            }
            return View(clientInfoView);
        }


        // POST: ClientInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _removeClient.RemoveClientCar(id);
            return RedirectToAction("ClientInfo", "ClientInfo", new RouteValueDictionary(new { idPaymentState = 2 }));
        }

        public async Task<ActionResult> EditClient(int? id)
        {
            if (id == null)
            {
                var infoLog = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Client");
            }

            ClientInfoView clientInfo = Mapper.Map<ClientInfoView>(await _clientInfo.ClientInfoGetId(id));

            ViewBag.IdClient = id;

            if (clientInfo == null)
            {
                return HttpNotFound();
            }

            return View(clientInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditClient([Bind(Include = "id,Surname,Name,PatronymicName,Phone,DateRegistration,Email,barcode,note")] ClientInfoView client, int idClient)
        {
            if (ModelState.IsValid)
            {
                ClientInfoView clientInfo = Mapper.Map<ClientInfoView>(await _clientInfo.ClientInfoGetId(idClient));
                clientInfo = client;

                ClientInfoBll clientInfoBll = Mapper.Map<ClientInfoView, ClientInfoBll>(client);

                await _clientInfo.Update(clientInfoBll);

                return RedirectToAction("ClientDetails", "ClientInfo", new RouteValueDictionary(new
                {
                    id = idClient
                }));
            }

            return View(client);
        }
    }
}
