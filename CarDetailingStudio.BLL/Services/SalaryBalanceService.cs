using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class SalaryBalanceService : ISalaryBalanceService
    {
        private IUnitOfWork _unitOfWork;

        public SalaryBalanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SalaryBalanceBll>> GetTableAll() => 
            Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetInclude("CarWashWorkers"));

        public async Task<SalaryBalanceBll> SelectId(int? elementId) => 
            Mapper.Map<SalaryBalanceBll>(await _unitOfWork.SalaruBalanceUnitOfWork.GetById(elementId));

        public async Task<IEnumerable<SalaryBalanceBll>> EmployeeBenefits(int idEmployee) => 
            Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.QueryObjectGraph(x => x.CarWashWorkersId == idEmployee));

        public async Task<IEnumerable<SalaryBalanceBll>> SelectIdToDate(int? idCarWash, int month, int year)
        {
             return Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.QueryObjectGraph(x => x.CarWashWorkersId == idCarWash
                                                                        && x.dateOfPayment.Value.Month == month
                                                                        && x.dateOfPayment.Value.Year == year));
        }

        public async Task<IEnumerable<SalaryBalanceBll>> SelectIdToDate(int? idCarWash, DateTime date)
        {
            var salaryBalanceLast = await LastMonthBalance(idCarWash);

            if (salaryBalanceLast == null)
            {
                var result = Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetWhere(x => x.CarWashWorkersId == idCarWash));

                return result.Where(x => x.dateOfPayment.Value.ToString("MM.yyyy") == date.ToString("MM.yyyy"));
            }
            else
            {
                return Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetWhere(x =>
                                                                 (x.CarWashWorkersId == idCarWash) &&
                                                                 (x.dateOfPayment > salaryBalanceLast.dateOfPayment)));
            }
        }

        public async Task Insert(SalaryBalanceBll element)
        {
            salaryBalance salaryBalance = Mapper.Map<SalaryBalanceBll, salaryBalance>(element);

            _unitOfWork.SalaruBalanceUnitOfWork.Insert(salaryBalance);
            await _unitOfWork.Save();
        }

        public async Task Update(SalaryBalanceBll elementToUpdate)
        {
            salaryBalance salaryBalance = Mapper.Map<SalaryBalanceBll, salaryBalance>(elementToUpdate);

            _unitOfWork.SalaruBalanceUnitOfWork.Update(salaryBalance);
            await _unitOfWork.Save();
        }

        public async Task<SalaryBalanceBll> LastMonthBalance(int? id)
        {
            try
            {
                var balance = Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetWhere(x => (x.currentMonthStatus == true) && (x.CarWashWorkersId == id)));
                return balance.Last(x => (x.currentMonthStatus == true) && (x.CarWashWorkersId == id));
            }
            catch
            {
                return null;
            }
        }
    }
}
