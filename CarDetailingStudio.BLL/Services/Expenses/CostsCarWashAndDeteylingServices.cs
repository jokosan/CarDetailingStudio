using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
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
            return Mapper.Map<IEnumerable<CostsCarWashAndDeteylingBll>>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetInclude("expenseCategory"));
        }

        public async Task Insert(CostsCarWashAndDeteylingBll element)
        {
            costsCarWashAndDeteyling costsCarWashAndDeteylings = Mapper.Map<CostsCarWashAndDeteylingBll, costsCarWashAndDeteyling>(element);

            _unitOfWork.costsCarWashAndDeteylingUnitOfWork.Insert(costsCarWashAndDeteylings);
            await _unitOfWork.Save();
        }

        public async Task<CostsCarWashAndDeteylingBll> SelectId(int? elementId)
        {
            return Mapper.Map<CostsCarWashAndDeteylingBll>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetById(elementId));
        }

        public async Task<IEnumerable<CostsCarWashAndDeteylingBll>> MonthlyReport(DateTime date)
        {
            return Mapper.Map<IEnumerable<CostsCarWashAndDeteylingBll>>(await _unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetWhere(x => x.dateExpenses.Value.Month == date.Month));
        }

        public async Task Update(CostsCarWashAndDeteylingBll elementToUpdate)
        {
            costsCarWashAndDeteyling costsCarWashAndDeteylings = Mapper.Map<CostsCarWashAndDeteylingBll, costsCarWashAndDeteyling>(elementToUpdate);

            _unitOfWork.costsCarWashAndDeteylingUnitOfWork.Update(costsCarWashAndDeteylings);
            await _unitOfWork.Save();
        }
    }
}
