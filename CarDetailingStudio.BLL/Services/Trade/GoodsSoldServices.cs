using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Contract;
using System.Data.Entity;

namespace CarDetailingStudio.BLL.Services.Trade
{
    public class GoodsSoldServices : IGoodsSold
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrigadeForTodayServices _brigadeForTodayServices;

        public GoodsSoldServices(
            IUnitOfWork unitOfWork,
            IBrigadeForTodayServices brigadeForTodayServices)
        {
            _unitOfWork = unitOfWork;
            _brigadeForTodayServices = brigadeForTodayServices;
        }

        public async Task<IEnumerable<GoodsSoldBll>> GetTableAll() => Mapper.Map<IEnumerable<GoodsSoldBll>>(await _unitOfWork.GoodsSoldUnitOfWork.GetInclude("listOfGoods"));

        public Task<GoodsSoldBll> SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public Task Update(GoodsSoldBll elementToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(GoodsSoldBll element)
        {
            _unitOfWork.GoodsSoldUnitOfWork.Insert(EntityTransformation(element));
            await _unitOfWork.Save();
        }

        public async Task InsertList(List<GoodsSoldBll> goodsSoldsList)
        {
            var brigade = await _brigadeForTodayServices.CurrentShift(DateTime.Now, 1);

            foreach (var item in goodsSoldsList)
            {
                item.carWashWorkersId = brigade.IdCarWashWorkers;
                await Insert(item);
            }
        }

        private goodsSold EntityTransformation(GoodsSoldBll entity) => Mapper.Map<GoodsSoldBll, goodsSold>(entity);

        public async Task<IEnumerable<GoodsSoldBll>> Reports(DateTime datepresentDay) =>
           Mapper.Map<IEnumerable<GoodsSoldBll>>(await _unitOfWork.GoodsSoldUnitOfWork.QueryObjectGraph(x => 
                (DbFunctions.TruncateTime(x.Date.Value) >= datepresentDay.Date), "listOfGoods"));

        public async Task<IEnumerable<GoodsSoldBll>> Reports(DateTime startDate, DateTime finalDate) =>
            Mapper.Map<IEnumerable<GoodsSoldBll>>(await _unitOfWork.GoodsSoldUnitOfWork.QueryObjectGraph(x => 
                (DbFunctions.TruncateTime(x.Date.Value) >= startDate.Date && (DbFunctions.TruncateTime(x.Date.Value) <= finalDate.Date)), "listOfGoods"));

        
    }
}
