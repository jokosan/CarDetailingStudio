using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderCarWashWorkersServices
    {
        Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(DateTime dateTime);
        Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(int? IdCarWashWorkers);
        Task<IEnumerable<OrderCarWashWorkersBll>> СontractorAllId(int? id);
        Task SaveOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash);
        Task UpdateOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash);
        Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud();
        Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(int id, System.DateTime date);
        Task<IEnumerable<OrderCarWashWorkersBll>> TableCalculationStatusFolse();
        Task<IEnumerable<OrderCarWashWorkersBll>> GetClosedDay();
        Task<IEnumerable<OrderCarWashWorkersBll>> GetClosedDay(int? id, DateTime? date);
    }
}