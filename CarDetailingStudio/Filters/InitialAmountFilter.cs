using CarDetailingStudio.BLL.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Filters
{
    public class InitialAmountFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            InitialAmountFiltersBll initialAmount = new InitialAmountFiltersBll();
            Task<bool> taskCashier = Task.Run<bool>(async () => await initialAmount.Cashier());

            bool resultRedirect = taskCashier.Result;

            if (resultRedirect == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                      new RouteValueDictionary
                      {
                            { "controller", "Cashier" },
                            { "action", "AmountAtTheBeginningOfTheDay" }
                      });
            }
        }      
    }
}