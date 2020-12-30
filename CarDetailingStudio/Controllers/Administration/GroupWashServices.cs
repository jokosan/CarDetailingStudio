using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers.Administration
{
    public partial class AdminController
    {
        [HttpGet]
        public async Task<ActionResult> GroupWashTable()
        {
            return View(Mapper.Map<IEnumerable<GroupWashServicesView>>(await _groupWashServices.GetAllTable()));
        }

        [HttpGet]
        public ActionResult GroupWashAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GroupWashAdd([Bind(Include = "group")] GroupWashServicesView groupWash)
        {
            if (ModelState.IsValid)
            {
                GroupWashServicesBll groupWashServices = Mapper.Map<GroupWashServicesView, GroupWashServicesBll>(groupWash);
                await _groupWashServices.Insert(groupWashServices);
                return RedirectToAction("GroupWashTable");
            }

            return View();
        }
    }
}