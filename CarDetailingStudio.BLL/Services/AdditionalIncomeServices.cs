using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;
using System.Data.Entity;

namespace CarDetailingStudio.BLL.Services
{
    public class AdditionalIncomeServices : IAdditionalIncome
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdditionalIncomeServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> GetTableAll() => Mapper.Map<IEnumerable<AdditionalIncomeBll>>(await _unitOfWork.AdditionalIncomeUnitOfWork.Get());

        public async Task<AdditionalIncomeBll> SelectId(int? elementId) => Mapper.Map<AdditionalIncomeBll>(await _unitOfWork.AdditionalIncomeUnitOfWork.GetById(elementId));

        public async Task<IEnumerable<AdditionalIncomeBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<AdditionalIncomeBll>>(await _unitOfWork.AdditionalIncomeUnitOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.Date) >= datepresentDay.Date)));
        }

        public async Task<IEnumerable<AdditionalIncomeBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<AdditionalIncomeBll>>(await _unitOfWork.AdditionalIncomeUnitOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.Date) >= startDate.Date)
                                                                                                                      && (DbFunctions.TruncateTime(x.Date) <= finalDate.Date)));
        }

        public async Task Insert(AdditionalIncomeBll element)
        {
            _unitOfWork.AdditionalIncomeUnitOfWork.Insert(TransformEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(AdditionalIncomeBll elementToUpdate)
        {
            _unitOfWork.AdditionalIncomeUnitOfWork.Update(TransformEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        private AdditionalIncome TransformEntity(AdditionalIncomeBll entity) => Mapper.Map<AdditionalIncomeBll, AdditionalIncome>(entity);
    }
}
