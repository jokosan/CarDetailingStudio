using CarDetailingStudio.BLL.Services;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Filters
{
    public class WorkShiftFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            CarWashWorkersServices services = new CarWashWorkersServices();

            bool redirect = services.HomeEntryCondition();

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