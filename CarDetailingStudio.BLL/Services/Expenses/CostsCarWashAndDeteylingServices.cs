using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class CostsCarWashAndDeteylingServices : ICostsCarWashAndDeteyling
    {
        private IUnitOfWork _unitOfWork;

        public CostsCarWashAndDeteylingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CostsCarWashAndDeteylingBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<CostsCarWashAndDeteylingBll>>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetInclude("expenseCategory", "TypeServices"));
        }

        public async Task<CostsCarWashAndDeteylingBll> SelectId(int? elementId)
        {
            return Mapper.Map<CostsCarWashAndDeteylingBll>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetById(elementId));
        }

        #region Отчеты за день и месяц
        public async Task<IEnumerable<CostsCarWashAndDeteylingBll>> Reports(DateTime datepresentDay)
        {
            return Mapper.Map<IEnumerable<CostsCarWashAndDeteylingBll>>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.QueryObjectGraph(x => DbFunctions.TruncateTime(x.dateExpenses.Value) == datepresentDay.Date, 
                                                                                                                                               "TypeServices"));
        }

        public async Task<IEnumerable<CostsCarWashAndDeteylingBll>> Reports(DateTime startDate, DateTime finalDate)
        {
            return Mapper.Map<IEnumerable<CostsCarWashAndDeteylingBll>>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.QueryObjectGraph(x => (DbFunctions.TruncateTime(x.dateExpenses.Value) >= startDate.Date)
                                                                                                                                              && (DbFunctions.TruncateTime(x.dateExpenses.Value) <= finalDate.Date),
                                                                                                                                              "TypeServices"));
        }
        #endregion

        #region Insert, Update
        public async Task Insert(CostsCarWashAndDeteylingBll element)
        {
            costsCarWashAndDeteyling costsCarWashAndDeteylings = Mapper.Map<CostsCarWashAndDeteylingBll, costsCarWashAndDeteyling>(element);

            _unitOfWork.costsCarWashAndDeteylingUnitOfWork.Insert(costsCarWashAndDeteylings);
            await _unitOfWork.Save();
        }

        public async Task Update(CostsCarWashAndDeteylingBll elementToUpdate)
        {
            costsCarWashAndDeteyling costsCarWashAndDeteylings = Mapper.Map<CostsCarWashAndDeteylingBll, costsCarWashAndDeteyling>(elementToUpdate);

            _unitOfWork.costsCarWashAndDeteylingUnitOfWork.Update(costsCarWashAndDeteylings);
            await _unitOfWork.Save();
        }
        #endregion
    }
}
