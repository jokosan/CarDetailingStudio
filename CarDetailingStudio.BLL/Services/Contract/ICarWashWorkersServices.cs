using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarWashWorkersServices
    {
        Task<IEnumerable<CarWashWorkersBll>> EmployeeName();
        Task<IEnumerable<CarWashWorkersBll>> GetTable();
        Task AddToCurrentShift(List<int> chkRow, IEnumerable<BrigadeForTodayBll> currentShiftResult = null);
        Task AddToCurrentShift(int? adminCarWosh, int? adminDetailing, IEnumerable<BrigadeForTodayBll> currentShiftResult = null);
        Task AddToCurrentShift(int? adminCarWosh, int? adminDetailing, List<int> chkRow);
        void Dispose();
        Task<IEnumerable<CarWashWorkersBll>> GetChooseEmployees(string arxiv = "true");
        Task<IEnumerable<CarWashWorkersBll>> GetStaffAll();
        Task<bool> HomeEntryCondition();
        Task<int> InsertEmployee(CarWashWorkersBll carWashWorkersBll);
        Task<CarWashWorkersBll> CarWashWorkersId(int? id);
        Task UpdateEmploee(CarWashWorkersBll carWashWorkersId, string action);
    }
}