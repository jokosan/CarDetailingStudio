using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.AnalyticsModules.Models.WagesForCompletedOrders;
using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractWagesForCompletedOrders
{
    interface IWagesForCompletedOrders
    {
        Task<WagesForCompletedOrdersModels> WagesForCompletedOrdersPerDay(DateTime date);
        Task<WagesForCompletedOrdersModels> WagesForCompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final);
        Task<IEnumerable<OrderCarWashWorkersBll>> AnalyticsWages(DateTime date);
        Task<IEnumerable<OrderCarWashWorkersBll>> AnalyticsWages(DateTime date, DateTime final);
        IEnumerable<GroupingEmployeesWages> GroupingByDatesAndEmployees(IEnumerable<OrderCarWashWorkersBll> orderCarWashWorkers);
        IEnumerable<OrderCarWashWorkersBll> EarningsPerEmployee(int typeServices, int idEmployees, IEnumerable<OrderCarWashWorkersBll> orderCarWashWorkers);
        Task<IEnumerable<BonusToSalaryBll>> BonusToSalaryInfo(DateTime date);
        Task<IEnumerable<BonusToSalaryBll>> BonusToSalaryInfo(DateTime dateStaart, DateTime dateFinal);
        Task<IEnumerable<EmployeeRateBll>> EmployeeRateInfo(DateTime date);
        Task<IEnumerable<EmployeeRateBll>> EmployeeRateInfo(DateTime dateStaart, DateTime dateFinal);
    }
}
