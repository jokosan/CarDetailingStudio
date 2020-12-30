using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class CashierServices : ICashier
    {
        private IUnitOfWork _unitOfWork;
        private IIncomeForTheCurrentDay _incomeForTheCurrentDay;

        public CashierServices(IUnitOfWork unitOfWork, IIncomeForTheCurrentDay incomeForTheCurrentDay)
        {
            _unitOfWork = unitOfWork;
            _incomeForTheCurrentDay = incomeForTheCurrentDay;
        }

        public async Task<IEnumerable<CashierBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<CashierBll>>(await _unitOfWork.CashierUtilOfWork.Get());
        }

        public async Task Insert(CashierBll element)
        {
            Cashier cashier = Mapper.Map<CashierBll, Cashier>(element);

            _unitOfWork.CashierUtilOfWork.Insert(cashier);
            await _unitOfWork.Save();
        }

        public async Task<CashierBll> SelectId(int? elementId)
        {
            return Mapper.Map<CashierBll>(await _unitOfWork.CashierUtilOfWork.GetById(elementId));
        }

        public async Task Update(CashierBll elementToUpdate)
        {
            Cashier cashier = Mapper.Map<CashierBll, Cashier>(elementToUpdate);

            _unitOfWork.CashierUtilOfWork.Update(cashier);
            await _unitOfWork.Save();
        }

        #region IReports

        public async Task<IEnumerable<CashierBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<CashierBll>>(await _unitOfWork.CashierUtilOfWork.GetWhere(x => DbFunctions.TruncateTime(x.date) == datepresentDay.Date));
        }

        public async Task<IEnumerable<CashierBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<CashierBll>>(await _unitOfWork.CashierUtilOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.date) >= startDate.Date)
                                                                                                    && (DbFunctions.TruncateTime(x.date) <= finalDate.Date)));
        }

        #endregion

        public async Task EndDay()
        {
            var EndDay = await _incomeForTheCurrentDay.AmountOfCompletedOrders(DateTime.Now);
            var CashierEndDay = await Reports(DateTime.Now);
                        
            if(CashierEndDay.Count() != 0)
            {
                var result = CashierEndDay.FirstOrDefault(x => x.date.Day == DateTime.Now.Day);
                result.amountEndOfTheDay = EndDay.cash.Value;

                await Update(result);
            }
        }
    }
}
