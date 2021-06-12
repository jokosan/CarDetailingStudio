using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.AnalyticsModules.Models.CompletedOrders;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractCompletedOrders
{
    interface ICompletedOrders
    {
        Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersPerDay(DateTime date);
        Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersPerDay(DateTime date, int paymentState);
        Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final);
        Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final, int paymentState);
        CompletedOrdersModels AnalyticsFormationCompletedOrders(IEnumerable<OrderServicesCarWashBll> orderServicesCarWashes);
        Task<List<IncomeModel>> ServiceIncome(IEnumerable<OrderServicesCarWashBll> orderServicesCarWashes);
        Task<List<IncomeModel>> ServiceIncome(DateTime start);
        Task<List<IncomeModel>> ServiceIncome(DateTime start, DateTime final);
        Task<IEnumerable<OrderServicesCarWashBll>> PaidServiceDebtInformation(DateTime start);
        Task<IEnumerable<OrderServicesCarWashBll>> PaidServiceDebtInformation(DateTime start, DateTime final);
    }
}
