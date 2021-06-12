using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.Models.ModelViews;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers
{
    [Authorize(Roles = "Admin, Owner, Manager, SuperUser")]
    public class DetailingsController : Controller
    {
        private IDetailingsServises _detailings;
        private IGroupWashServices _groupWashServices;

        public DetailingsController(IDetailingsServises detailings, IGroupWashServices groupWashServices)
        {
            _detailings = detailings;
            _groupWashServices = groupWashServices;
        }

        // GET: Detailings
        public async Task<ActionResult> PriceList(int page = 1)
        {
            var selectPrice = Mapper.Map<IEnumerable<DetailingsView>>(await _detailings.SelectTypeService(page));

            List<int> selectGroup = new List<int>();

            foreach (var item in selectPrice.GroupBy(x => x.IdGroupWashServices))
                selectGroup.Add(item.Key.Value);
            
            ViewBag.Grup = await _groupWashServices.SelectGroupWashServices(selectGroup);

            return View(selectPrice);
        }
         
        public async Task<ActionResult> AddServises(int? idGroup = null)
        {
           
            ViewBag.GroupServises = new SelectList(await _groupWashServices.GetAllTable(), "Id", "group");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddServises([Bind(Include = "Id,services_list,validity,note,S,M,L,XL,group,status,currency,mark,IdGroupWashServices,IdTypeService,forUnit,sorting")] DetailingsView price)
        {
            if (ModelState.IsValid)
            {
                DetailingsBll detailings = Mapper.Map<DetailingsView, DetailingsBll>(price);
                await _detailings.AddNewServices(detailings);
            }

            ViewBag.GroupServises = new SelectList(await _groupWashServices.GetAllTable(), "Id", "group");

            return RedirectToAction("PriceList");
        }

        public async Task<ActionResult> EditServises(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailingsView detailings = Mapper.Map<DetailingsView>(await _detailings.GetId(id));

            ViewBag.GroupServises = new SelectList(await _groupWashServices.GetAllTable(), "Id", "group");

            if (detailings == null)
            {
                return HttpNotFound();
            }

            return View(detailings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditServises([Bind(Include = "Id,services_list,validity,note,S,M,L,XL,group,status,currency,mark,IdGroupWashServices,IdTypeService,forUnit,sorting")] DetailingsView price)
        {
            if (ModelState.IsValid)
            {
                DetailingsBll detailings = Mapper.Map<DetailingsView, DetailingsBll>(price);
                await _detailings.UpdateServices(detailings);
            }

            ViewBag.GroupServises = new SelectList(await _groupWashServices.GetAllTable(), "Id", "group");

            return RedirectToAction("PriceList");
        }

        public async Task<ActionResult> EditOrder()//int OrderId
        {
            return View(Mapper.Map<IEnumerable<DetailingsView>>(await _detailings.Converter()));
        }

        [HttpPost]
        public void EditOrder(List<string> idServices)
        {
            var x = idServices.Count();
        }
    }
}
