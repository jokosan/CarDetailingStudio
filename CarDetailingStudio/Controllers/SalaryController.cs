using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDetailingStudio.Models.ModelViews;
using CarDetailingStudio.BLL.Services;
using CarDetailingStudio.BLL.Services.Modules.EmployeeSalary;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.Controllers
{
    public class SalaryController : Controller
    {
        
        private IOrderCarWashWorkersServices _workersServices;
        private ISalaryModules _salary;
        private ICostServices _costServices;

        public SalaryController(ISalaryModules salary,
                                IOrderCarWashWorkersServices workersServices, ICostServices costServices)
        {      
            _salary = salary;
            _workersServices = workersServices;
            _costServices = costServices;
        }

        private static List<OrderServicesCarWashView> orderServices = new List<OrderServicesCarWashView>();

        // GET: Salary

        public ActionResult InfoSalary(int? id, int? InterestRate, string IdName)
        {
            if (id != null && InterestRate != null)
            {

                orderServices.Clear();

                orderServices = Mapper.Map<List<OrderServicesCarWashView>>(_salary.TotalSalaryForToday(id.Value, InterestRate.Value));

                ViewBag.Sum = orderServices.Sum(x => x.TotalCostOfAllServices);
                
                TempData["idClient"] = id;
                TempData["Name"] = IdName;

                return View(orderServices);
            }

            return RedirectToAction("../ItogOrder/Salary");
            
        }

        [HttpPost]
        public ActionResult InfoSalary(bool confirmation = false)
        {
            if (confirmation && TempData.ContainsKey("idClient"))
            {
                string fio = TempData["Name"] as string;

                var PaySalary = Mapper.Map<IEnumerable<OrderServicesCarWashView>, IEnumerable<OrderServicesCarWashBll>>(orderServices);  //?????
                _workersServices.OrderServicesUpdate(int.Parse(TempData["idClient"].ToString()));
                _costServices.AddCost(orderServices.Sum(x => x.TotalCostOfAllServices), fio);

                return RedirectToAction("../ItogOrder/Salary");
            }

            return View(orderServices);
        }


    }
}
