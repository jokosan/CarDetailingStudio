using CarDetailingStudio.BLL.Services.Filters;
using System.Web.Mvc;

namespace CarDetailingStudio.Filters
{
    public class PreviousShiftStatusFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ShiftClosingStatus shiftClosingStatus = new ShiftClosingStatus();
            shiftClosingStatus.ShiftStatus();


            base.OnActionExecuted(filterContext);
        }
    }
}
