using CarDetailingStudio.BLL.Services.Filters;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.Filters
{
    public class PreviousShiftStatusFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ShiftClosingStatus shiftClosingStatus = new ShiftClosingStatus();

            Task task = Task.Run(async () => await shiftClosingStatus.ShiftStatus());

            base.OnActionExecuted(filterContext);
        }
    }
}
