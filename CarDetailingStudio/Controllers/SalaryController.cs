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

namespace CarDetailingStudio.Controllers
{
    public class SalaryController : Controller
    {
        private IOrderServicesCarWashServices _order;
        private SalaryModules _salary;

        public SalaryController(IOrderServicesCarWashServices orderServices, SalaryModules salary)
        {
            _order = orderServices;
            _salary = salary;
        }

        // GET: Salary
      
        public ActionResult InfoSalary(int id, int InterestRate)
        {
            var SalaryResult = Mapper.Map<List<OrderServicesCarWashView>>(_salary.TotalSalaryForToday(id, InterestRate));
                        
            ViewBag.Sum = SalaryResult.Sum(x => x.TotalCostOfAllServices);

            return View(SalaryResult);
        }

      
    }
}
