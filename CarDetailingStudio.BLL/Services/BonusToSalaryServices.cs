using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Runtime.InteropServices;
using CarDetailingStudio.DAL;
using System.Data.Entity;

namespace CarDetailingStudio.BLL.Services
{
    public class BonusToSalaryServices : IBonusToSalary
    {
        private IUnitOfWork _unitOfWork;

        public BonusToSalaryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BonusToSalaryBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<BonusToSalaryBll>>(await _unitOfWork.BonusToSalaryUnitOfWork.GetInclude("CarWashWorkers"));
        }

        public async Task<IEnumerable<BonusToSalaryBll>> GetTableAll(int? elementId)
        {
            return Mapper.Map<IEnumerable<BonusToSalaryBll>>(await _unitOfWork.BonusToSalaryUnitOfWork.GetWhere(x => x.carWashWorkersId == elementId, "CarWashWorkers"));
        }

        public async Task<BonusToSalaryBll> SelectId(int? elementId)
        {
            return Mapper.Map<BonusToSalaryBll>(await _unitOfWork.BonusToSalaryUnitOfWork.GetById(elementId));
        }

        public async Task Insert(BonusToSalaryBll element)
        {
            bonusToSalary bonus = Mapper.Map<BonusToSalaryBll, bonusToSalary>(element);
            _unitOfWork.BonusToSalaryUnitOfWork.Insert(bonus);
            await _unitOfWork.Save();
        }

        public async Task Update(BonusToSalaryBll elementToUpdate)
        {
            bonusToSalary bonus = Mapper.Map<BonusToSalaryBll, bonusToSalary>(elementToUpdate);
            _unitOfWork.BonusToSalaryUnitOfWork.Update(bonus);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<BonusToSalaryGroupBll>> TableGroup()
        {
            var result = await GetTableAll();

            var resultGroup = result.GroupBy(x => x.carWashWorkersId)
                                    .Select(y => new BonusToSalaryGroupBll
                                    {
                                        idBonusToSalary = y.First().idBonusToSalary,
                                        carWashWorkersId = y.First().carWashWorkersId,
                                        Surname = y.First().CarWashWorkers.Surname,
                                        Name = y.First().CarWashWorkers.Name,
                                        Patronymic = y.First().CarWashWorkers.Patronymic,
                                        amount = y.Sum(c => c.amount),
                                        date = y.First().date
                                    });

            return resultGroup;

        }

        public async Task<IEnumerable<BonusToSalaryBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<BonusToSalaryBll>>(await _unitOfWork.BonusToSalaryUnitOfWork.GetWhere(x => DbFunctions.TruncateTime(x.date.Value) == datepresentDay.Date));
        }

        public async Task<IEnumerable<BonusToSalaryBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<BonusToSalaryBll>>(await _unitOfWork.BonusToSalaryUnitOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.date.Value) >= startDate.Date)
                                                                                                                && (DbFunctions.TruncateTime(x.date.Value) <= finalDate.Date)));
        }

        public async Task<IEnumerable<BonusToSalaryBll>> WhereMontsBonusToSalary()
        {
            return Mapper.Map<IEnumerable<BonusToSalaryBll>>(await _unitOfWork.BonusToSalaryUnitOfWork.GetWhere(x => (x.date.Value.Month == DateTime.Now.Month)
                                                                                                                  && (x.date.Value.Year == DateTime.Now.Year)));
        }
    }
}
