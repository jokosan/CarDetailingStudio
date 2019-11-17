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
using CarDetailingStudio.Models.ModelViews;
using CarDetailingStudio.BLL.Services.Modules;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class ItogOrderController : Controller
    {
        private OrderInfoViewServices _orderInfo;


        public ItogOrderController(OrderInfoViewServices orderInfo)
        {
            _orderInfo = orderInfo;
        }

        // GET: OrderInfoViewModels
        public ActionResult Salary()
        {
            var ItogSalary = Mapper.Map<IEnumerable<OrderInfoViewModel>>(_orderInfo.GetOrderInfo());
            var ItogSalarySum = ItogSalary.Sum(x => x.Expr1);

            ViewBag.Sum = ItogSalarySum;
            TempData["Calculate"] = ItogSalary;

            return View(Mapper.Map<IEnumerable<OrderInfoViewModel>>(_orderInfo.GetOrderInfo()));
        }

        [HttpPost]
        public ActionResult Salary(bool confirmation = false)
        {
            if (confirmation)
            {
                var resultBrigade = TempData["Calculate"] as IEnumerable<OrderInfoViewModel>;                
                _orderInfo.PayAllEmployees(Mapper.Map<IEnumerable<OrderInfoViewModel>, IEnumerable<OrderInfoViewBll>>(resultBrigade));
            }
            
            return Redirect("Salary");
        }

    }
}
