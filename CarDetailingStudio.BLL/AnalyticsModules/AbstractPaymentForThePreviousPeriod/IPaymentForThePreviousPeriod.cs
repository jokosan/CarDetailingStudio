using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractPaymentForThePreviousPeriod
{
    interface IPaymentForThePreviousPeriod
    {
        Task<IEnumerable<OrderServicesCarWashBll>> PaymentForThePreviousPeriodPerDay(DateTime date);
        Task<IEnumerable<OrderServicesCarWashBll>> PaymentForThePreviousPeriodPerDay(DateTime date, int paymentState);
        InformationPreviousPeriod AnalyticsFormationPaymentForThePreviousPeriod(IEnumerable<OrderServicesCarWashBll> orderCarWashes);
        IEnumerable<OrderServicesCarWashBll> InfoOrdersForThePreviousPeriod(IEnumerable<OrderServicesCarWashBll> orderCarWashes);
    }
}
