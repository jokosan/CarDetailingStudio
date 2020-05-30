﻿using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.CloseShift.Contract;
using System;
using System.Linq;

namespace CarDetailingStudio.BLL.Services.Modules.CloseShift
{
    public class CloseShiftModule : ICloseShiftModule
    {
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private IDayResult _dayResult;

        public CloseShiftModule(IOrderCarWashWorkersServices orderCarWashWorkers, IDayResult dayResult)
        {
            _orderCarWashWorkers = orderCarWashWorkers;
            _dayResult = dayResult;
        }

        public void CurrentShift()
        {
            var ordersFulfilled = _orderCarWashWorkers.SampleForPayroll(DateTime.Now);
            var DayClose = _dayResult.DayResultViewInfo();

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            foreach (var itemShift in DayClose)
            {
                var resultShift = ordersFulfilled.Where(x => x.IdCarWashWorkers == itemShift.carWashWorkersId);

                foreach (var itemOrder in resultShift)
                {
                    orderCarWashWorkers.Id = itemOrder.Id;
                    orderCarWashWorkers.IdOrder = itemOrder.IdOrder;
                    orderCarWashWorkers.IdCarWashWorkers = itemOrder.IdCarWashWorkers;
                    orderCarWashWorkers.CalculationStatus = false;
                    orderCarWashWorkers.Payroll = itemOrder.Payroll;
                    orderCarWashWorkers.closedDayStatus = true;

                    _orderCarWashWorkers.UpdateOrderCarWashWorkers(orderCarWashWorkers);
                }
            }
        }

        //  выбираем количество дней за которые будет производиться расчет ЗП
        //public void PartPayroll(List<string> idCureentShift)
        //{
        //    foreach (var idShift in idCureentShift)
        //    {
        //        var pay = _orderCarWashWorkers.СontractorAllId(Convert.ToInt32(idShift));

        //        foreach (var idDey in pay)
        //        {
        //            idDey.salaryDate = DateTime.Now;
        //            idDey.CalculationStatus = true;

        //            _orderCarWashWorkers.UpdateOrderCarWashWorkers(idDey);
        //        }
        //    }
        //}
    }
}
