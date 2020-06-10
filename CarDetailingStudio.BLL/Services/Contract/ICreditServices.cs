using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICreditServices
    {
        Task<IEnumerable<CreditBll>> GetAll();
        Task<IEnumerable<CreditBll>> GetCreditWhere();
        Task<CreditBll> IdCredit(int id);
        Task Create(CreditBll credit);
    }
}
