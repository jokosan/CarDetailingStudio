using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules.Contract
{
    public interface IAccrualOfPremium
    {
        Task CloseShift(int idPositionServises);
    }
}
