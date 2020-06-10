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

        public async Task<IEnumerable<SalaryBalanceBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetInclude("CarWashWorkers"));
        }

        public async Task<IEnumerable<SalaryBalanceBll>> SelectIdToDate(int? idCarWash, DateTime date)
        {
            var salaryBalanceLast = await LastMonthBalance(idCarWash);

            if (salaryBalanceLast == null)
            {
                return Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetWhere(x =>
                                                                   (x.CarWashWorkersId == idCarWash) &&
                                                                   (x.dateOfPayment.Value.ToString("MM.yyyy") == date.ToString("MM.yyyy"))));
            }
            else
            {
                return Mapper.Map<IEnumerable<SalaryBalanceBll>>(await _unitOfWork.SalaruBalanceUnitOfWork.GetWhere(x =>
                                                                 (x.CarWashWorkersId == idCarWash) &&
                                                                 (x.dateOfPayment > salaryBalanceLast.dateOfPayment)));
            }
        }

        public async Task<SalaryBalanceBll> SelectId(int? elementId)
        {
            return Mapper.Map<SalaryBalanceBll>(await _unitOfWork.salaryExpensesUnitOfWork.GetById(elementId));
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
            //DateTime date = DateTime.Today.AddMonths(-1);
            //string test = date.ToString("MM.yyyy");

            try
            {
                //var balance = Mapper.Map<IEnumerable<SalaryBalanceBll>>(_unitOfWork.SalaruBalanceUnitOfWork.GetWhere(x => (x.currentMonthStatus == true) && (x.CarWashWorkersId == id)));
                //var getDate = balance.First(x => x.dateOfPayment?.ToString("MM.yyyy") == test);

                //return getDate.accountBalance.Value;
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
