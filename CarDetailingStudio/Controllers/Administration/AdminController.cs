using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDetailingStudio.Controllers.Administration
{
    public partial class AdminController : Controller
    {
        private IPriceTireFittingAdditionalServices _priceTireFittingAdditionalServices;
        private IGroupWashServices _groupWashServices;

        public AdminController(
               IPriceTireFittingAdditionalServices priceTireFittingAdditionalServices,
               IGroupWashServices groupWashServices)
        {
            _priceTireFittingAdditionalServices = priceTireFittingAdditionalServices;
            _groupWashServices = groupWashServices;
        }
    }
}