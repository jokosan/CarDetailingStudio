using System.Collections.Generic;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface ICarWashWorkersServices
    {
        void AddToCurrentShift(FormCollection collection);
        void Dispose();
        IEnumerable<CarWashWorkersBll> GetChooseEmployees();
        IEnumerable<CarWashWorkersBll> GetStaffAll();
        bool HomeEntryCondition();
        void InsertEmployee(CarWashWorkersBll carWashWorkersBll);
        CarWashWorkersBll CarWashWorkersId(int? id);
        void UpdateEmploee(CarWashWorkersBll carWashWorkersId, string action);
    }
}