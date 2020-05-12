using CarDetailingStudio.BLL.Model.ModelViewBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract
{
    public interface IWagesForDaysWorkedGroup
    {
        IEnumerable<WagesForDaysWorkedBll> DayOrderResult(int? Id);
        void PaymentOfPartOfTheSalary(int? employeeId, double payoutAmount, double totalPayable, bool closeMonth, bool NegativeBalance = false);
    }
}
