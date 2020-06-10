using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IWagesForDaysWorkedGroup
    {
        
        Task<IEnumerable<WagesForDaysWorkedBll>> DayOrderResult(int? Id);
        Task PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, bool closeMonth, bool NegativeBalance = false);
    }
}
