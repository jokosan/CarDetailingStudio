using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICreditServices
    {
        IEnumerable<CreditBll> GetAll();
        IEnumerable<CreditBll> GetCreditWhere();
        CreditBll IdCredit(int id);
        void Create(CreditBll credit);
    }
}
