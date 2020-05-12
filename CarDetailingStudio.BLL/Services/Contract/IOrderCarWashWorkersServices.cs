using System;
using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderCarWashWorkersServices
    {      
        IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(DateTime dateTime);
        IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(int? IdCarWashWorkers);
        IEnumerable<OrderCarWashWorkersBll> СontractorAllId(int? id);
        void SaveOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash);
        void UpdateOrderCarWashWorkers(OrderCarWashWorkersBll orderCarWash);
        IEnumerable<OrderCarWashWorkersBll> GetTableInclud();
        IEnumerable<OrderCarWashWorkersBll> SampleForPayroll(int id, System.DateTime date);
        IEnumerable<OrderCarWashWorkersBll> TableCalculationStatusFolse();
        IEnumerable<OrderCarWashWorkersBll> GetClosedDay();
        IEnumerable<OrderCarWashWorkersBll> GetClosedDay(int? id, DateTime? date);
    }
}