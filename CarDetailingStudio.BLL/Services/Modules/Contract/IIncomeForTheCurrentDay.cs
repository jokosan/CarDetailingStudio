using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.Contract
{
    public interface IIncomeForTheCurrentDay
    {
        Task<double> SumOfAllExpenses(DateTime? startDate);
        Task<double> SumOfAllExpenses(DateTime? startDate, DateTime? finalDate);
        Task<EmployeeSalariesBll> EmployeeSalaries(DateTime? startDate);
        Task<EmployeeSalariesBll> EmployeeSalaries(DateTime? startDate, DateTime? finalDate);
        Task<OrderInformationWashingDetailingBll> AmountOfCompletedOrders(DateTime? startDate);
        Task<OrderInformationWashingDetailingBll> AmountOfCompletedOrders(DateTime? startDate, DateTime? finalDate);
        Task<IEnumerable<OrderServicesCarWashBll>> AboutOrders(int typeOrder, DateTime dateStaer, DateTime? dateFinis);
        Task<IEnumerable<OrderCarWashWorkersBll>> AboutWages(int typeOfEmployees, DateTime dateStaer, DateTime? dateFinis);
        Task<(OrderInformationWashingDetailingBll, EmployeeSalariesBll)> SelectCash(DateTime? startDate);
    }
}
