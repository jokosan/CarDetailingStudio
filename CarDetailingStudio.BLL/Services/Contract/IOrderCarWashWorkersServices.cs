using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderCarWashWorkersServices
    {
        void AddEmployeeToOrder(List<string> idBrigade, int idOrder);
        IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(int IdCarWashWorkers);
    }
}