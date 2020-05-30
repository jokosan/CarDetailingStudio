using CarDetailingStudio.BLL.Services.Filters;
using CarDetailingStudio.BLL.Services.Modules;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDetailingStudio.Filters
{
    public class MonitoringTheNumberOfEmployeesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TeamMonitoringFilters team = new TeamMonitoringFilters();
            ApiPrivatBank apiPrivat = new ApiPrivatBank();

            apiPrivat.ApiPrivat();

            if (team.Monitoring() == 0)
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