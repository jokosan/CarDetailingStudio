
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;

namespace CarDetailingStudio.BLL.Services
{
    public class EmployeeRateServises : IEmployeeRate
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRateServises(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeRateBll>> GetTableAll() => Mapper.Map<IEnumerable<EmployeeRateBll>>(await _unitOfWork.EmployeeRateUnitOfWork.GetInclude("brigadeForToday", "brigadeForToday.CarWashWorkers"));

        public async Task<EmployeeRateBll> SelectId(int? elementId) => Mapper.Map<EmployeeRateBll>(await _unitOfWork.EmployeeRateUnitOfWork.GetById(elementId));

        public async Task<IEnumerable<EmployeeRateBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<EmployeeRateBll>>(await _unitOfWork.EmployeeRateUnitOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.brigadeForToday.Date.Value) >= datepresentDay.Date), "brigadeForToday"));
        }

        public async Task<IEnumerable<EmployeeRateBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<EmployeeRateBll>>(await _unitOfWork.EmployeeRateUnitOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.brigadeForToday.Date.Value) >= startDate.Date)
                                                                                                              && (DbFunctions.TruncateTime(x.brigadeForToday.Date.Value) <= finalDate.Date), "brigadeForToday"));
        }      

        public async Task Update(EmployeeRateBll elementToUpdate)
        {
            _unitOfWork.EmployeeRateUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        public async Task Insert(EmployeeRateBll element)
        {
            _unitOfWork.EmployeeRateUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        private DAL.EmployeeRate TransformAnEntity(EmployeeRateBll entity) => Mapper.Map<EmployeeRateBll, DAL.EmployeeRate>(entity);

        public async Task<IEnumerable<EmployeeRateBll>> WherySumRate(int idCarWash)
        {
            return Mapper.Map<IEnumerable<EmployeeRateBll>>(await _unitOfWork.EmployeeRateUnitOfWork.GetWhere(x => (x.brigadeForToday.Date.Value.Month == DateTime.Now.Month
                                                                                                                  && x.brigadeForToday.Date.Value.Year == DateTime.Now.Year)
                                                                                                                  && x.brigadeForToday.IdCarWashWorkers == idCarWash, "brigadeForToday"));
        }
        
    }
}
