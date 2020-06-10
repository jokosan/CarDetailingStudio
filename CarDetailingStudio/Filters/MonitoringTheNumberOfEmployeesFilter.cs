using CarDetailingStudio.BLL.Services.Filters;
using CarDetailingStudio.BLL.Services.Modules;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
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

            Task taskApiPrivate = Task.Run(async () => await apiPrivat.ApiPrivat());
            Task<int> taskMonitoring = Task.Run<int>(async () => await team.Monitoring());
            
            int result = taskMonitoring.Result;

            if (result == 0)
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