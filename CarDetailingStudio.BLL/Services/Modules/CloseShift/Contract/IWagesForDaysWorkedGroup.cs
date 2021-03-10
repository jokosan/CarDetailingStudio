using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IWagesForDaysWorkedGroup
    {
        Task<IEnumerable<WagesForDaysWorkedBll>> DayOrderResult(int? Id);
        Task<IEnumerable<WagesForDaysWorkedBll>> MonthOrderResult(int? id, int month, int year);
        Task PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, double SalaryCurrentMonth, double Prize, double BalancLastMonth, double PaidMonth, int idPaymentState);
    }
}
