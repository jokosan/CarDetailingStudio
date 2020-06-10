using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarWashWorkersServices
    {
        Task<IEnumerable<CarWashWorkersBll>> GetTable();
        Task AddToCurrentShift(int? adminCarWosh, int? adminDetailing, List<int> chkRow);
        void Dispose();
        Task<IEnumerable<CarWashWorkersBll>> GetChooseEmployees();
        Task<IEnumerable<CarWashWorkersBll>> GetStaffAll();
        Task<bool> HomeEntryCondition();
        Task InsertEmployee(CarWashWorkersBll carWashWorkersBll);
        Task<CarWashWorkersBll> CarWashWorkersId(int? id);
        Task UpdateEmploee(CarWashWorkersBll carWashWorkersId, string action);
    }
}