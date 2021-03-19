using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.EmployeeRate;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class BrigadeForTodayServices : IBrigadeForTodayServices
    {
        private IUnitOfWork _unitOfWork;
        private IEmployeeRateModules _employeeRate;

        public BrigadeForTodayServices(
            IUnitOfWork unitOfWork,
            IEmployeeRateModules employeeRate)
        {
            _unitOfWork = unitOfWork;
            _employeeRate = employeeRate;
        }

        public async Task<BrigadeForTodayBll> CurrentShift(DateTime date, int id) =>
         Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeForTodayUnitOfWork.GetWhere(x => 
                                                              (DbFunctions.TruncateTime(x.Date.Value) == date.Date) && 
                                                              (x.EarlyTermination == true) &&
                                                              x.StatusId == id)).LastOrDefault();
        

        public async Task<IEnumerable<BrigadeForTodayBll>> GetDateTimeNow()
        {
            var date = DateTime.Now;
            return Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeUnitOfWork.GetWhere(x => x.EarlyTermination == true 
                                                                               && DbFunctions.TruncateTime(x.Date.Value) == date.Date));
        }

        public async Task<IEnumerable<BrigadeForTodayBll>> GetDateTimeNow(DateTime date)
        {
            List<BrigadeForTodayBll> brigadeForTodays = new List<BrigadeForTodayBll>();

            var result = await GetDateTimeNow();

            foreach (var item in result)
            {
                item.EndTime = date;
                item.EarlyTermination = false;

                brigadeForTodays.Add(item);
                await Update(item);
            }

            return brigadeForTodays.AsEnumerable();
        }

        public async Task<bool> AdminTrue(DateTime date, int ststus)
        {
            var resultBrigade = Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeForTodayUnitOfWork.GetWhere(x => DbFunctions.TruncateTime(x.Date.Value) == date.Date && x.StatusId >= ststus));
            return resultBrigade.Any(x => x.StatusId == ststus && x.EarlyTermination == true);
        }
        
        public async Task RemoveFromBrigade(int id)
        {
            BrigadeForTodayBll brigadeForTodayBll = await GetId(id);

            brigadeForTodayBll.EndTime = DateTime.Now;
            brigadeForTodayBll.EarlyTermination = false;

            await _employeeRate.EndDayRateCalculation(brigadeForTodayBll);
            await Update(brigadeForTodayBll);
        }

        public async Task<BrigadeForTodayBll> GetId(int id) => Mapper.Map<BrigadeForTodayBll>(await _unitOfWork.BrigadeUnitOfWork.GetById(id));

        public async Task<IEnumerable<BrigadeForTodayBll>> Reports(DateTime datepresentDay) =>
            Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeForTodayUnitOfWork.GetWhere(x => 
                DbFunctions.TruncateTime(x.Date.Value) == datepresentDay.Date));

        public async Task<IEnumerable<BrigadeForTodayBll>> Reports(DateTime startDate, DateTime finalDate) =>
            Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeForTodayUnitOfWork.GetWhere(x => 
                (DbFunctions.TruncateTime(x.Date.Value) >= startDate.Date) && (DbFunctions.TruncateTime(x.Date.Value) <= finalDate.Date)));

        public brigadeForToday TransformAnEntity(BrigadeForTodayBll entity) => Mapper.Map<BrigadeForTodayBll, brigadeForToday>(entity);

        public Task Insert(BrigadeForTodayBll element)
        {
            throw new NotImplementedException();
        }

        public async Task Update(BrigadeForTodayBll elementToUpdate)
        {
            _unitOfWork.BrigadeForTodayUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }
    }
}
