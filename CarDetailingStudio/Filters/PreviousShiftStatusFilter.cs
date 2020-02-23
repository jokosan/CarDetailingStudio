using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Filters;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
