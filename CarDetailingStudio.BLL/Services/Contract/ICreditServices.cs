using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;

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
