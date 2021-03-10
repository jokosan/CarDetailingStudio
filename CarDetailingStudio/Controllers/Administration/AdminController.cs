using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
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
        private readonly IExpenseCategory _expenseCategory;
        private readonly IUtilityCostsCategory _utilityCostsCategory;
        private readonly ICostCategories _costCategories;

        public AdminController(
               IPriceTireFittingAdditionalServices priceTireFittingAdditionalServices,
               IGroupWashServices groupWashServices,
               IExpenseCategory expenseCategory,
               IUtilityCostsCategory utilityCostsCategory,
               ICostCategories costCategories)
        {
            _priceTireFittingAdditionalServices = priceTireFittingAdditionalServices;
            _groupWashServices = groupWashServices;
            _costCategories = costCategories;
            _expenseCategory = expenseCategory;
            _utilityCostsCategory = utilityCostsCategory;
        }
    }
}