using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IWagesForDaysWorkedGroup
    {
        IEnumerable<WagesForDaysWorkedBll> DayOrderResult(int? Id);
        void PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, bool closeMonth, bool NegativeBalance = false);
    }
}
