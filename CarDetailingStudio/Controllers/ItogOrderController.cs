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
            var ItorSalary = Mapper.Map<IEnumerable<OrderInfoViewModel>>(_orderInfo.GetOrderInfo());
            ViewBag.Sum = ItorSalary.Sum(x => x.Expr1);

            return View(Mapper.Map<IEnumerable<OrderInfoViewModel>>(_orderInfo.GetOrderInfo()));
        }

    }
}
