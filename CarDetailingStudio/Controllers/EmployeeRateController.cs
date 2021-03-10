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
using CarDetailingStudio.BLL.Services.Contract;
using AutoMapper;

namespace CarDetailingStudio.Controllers
{
    public class EmployeeRateController : Controller
    {
        private readonly IEmployeeRate _employeeRate;

        public EmployeeRateController(
            IEmployeeRate employeeRate)
        {
            _employeeRate = employeeRate;
        }

        // GET: EmployeeRate
        public async Task<ActionResult> ArxivRate()
        {
            var resultArxiv = Mapper.Map<IEnumerable<EmployeeRateView>>(await _employeeRate.GetTableAll()).OrderByDescending(x => x.idEmployeeRate);

            return View(resultArxiv);
        }
    }
}
