using CarDetailingStudio.BLL.Services;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Filters
{
    public class WorkShiftFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            CarWashWorkersServices services = new CarWashWorkersServices();

            Task<bool> task = Task.Run<bool>(async () => await services.HomeEntryCondition());
            bool redirect = task.Result;

            //bool redirect = AsyncContext services.HomeEntryCondition();

            if (filterContext.HttpContext.Session["UserId"] == null)
            {
                if (!redirect)
                {
                    filterContext.Result = new RedirectToRouteResult(
                      new RouteValueDictionary
                      {
                            { "controller", "CarWashWorkersViews" },
                            { "action", "Index" }
                      });
                }
            }
        }
    }
}