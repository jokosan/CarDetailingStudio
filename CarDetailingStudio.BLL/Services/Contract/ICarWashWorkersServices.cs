using System.Collections.Generic;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarWashWorkersServices
    {
        IEnumerable<CarWashWorkersBll> GetTable();
        void AddToCurrentShift(int? adminCarWosh, int? adminDetailing, List<int> chkRow);
        void Dispose();
        IEnumerable<CarWashWorkersBll> GetChooseEmployees();
        IEnumerable<CarWashWorkersBll> GetStaffAll();
        bool HomeEntryCondition();
        void InsertEmployee(CarWashWorkersBll carWashWorkersBll);
        CarWashWorkersBll CarWashWorkersId(int? id);
        void UpdateEmploee(CarWashWorkersBll carWashWorkersId, string action);
    }
}