﻿using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderCarWashWorkersServices : IReports<OrderCarWashWorkersBll>,  IReportsId<OrderCarWashWorkersBll>, IDatabaseOperations<OrderCarWashWorkersBll>
    {
        Task<IEnumerable<OrderCarWashWorkersBll>> SelectMonth(int? id, int month, int year);
        Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(DateTime dateTime);
        Task<IEnumerable<OrderCarWashWorkersBll>> SampleForPayroll(int? IdCarWashWorkers);
        Task<IEnumerable<OrderCarWashWorkersBll>> DailyEmployeeOrders(int? carWashWorkersId, DateTime date);
        Task<OrderCarWashWorkersBll> Change(int? Order, int? Employee);
        Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud(int? idCarWashWorkers);
        Task<IEnumerable<OrderCarWashWorkersBll>> GetTableInclud(int month, int year);
        Task<IEnumerable<OrderCarWashWorkersBll>> TableCalculationStatusFolse();
        Task<IEnumerable<OrderCarWashWorkersDayGroupBll>> OrderCarWashWorkers(int? id, DateTime startDate, DateTime? finalDate);
        Task<IEnumerable<OrderCarWashWorkersDayGroupBll>> WhereCarWashWorkers(int keyCarWashWorkers);
    }
}