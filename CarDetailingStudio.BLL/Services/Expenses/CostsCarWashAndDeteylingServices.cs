using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class CostsCarWashAndDeteylingServices : ICostsCarWashAndDeteyling
    {
        private IUnitOfWork _unitOfWork;

        public CostsCarWashAndDeteylingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CostsCarWashAndDeteylingBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<CostsCarWashAndDeteylingBll>>(_unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetInclude("expenseCategory"));
        }

        public void Insert(CostsCarWashAndDeteylingBll element)
        {
            costsCarWashAndDeteyling costsCarWashAndDeteylings = Mapper.Map<CostsCarWashAndDeteylingBll, costsCarWashAndDeteyling>(element);
            
            _unitOfWork.costsCarWashAndDeteylingUnitOfWork.Insert(costsCarWashAndDeteylings);
            _unitOfWork.Save();
        }

        public CostsCarWashAndDeteylingBll SelectId(int? elementId)
        {
            return Mapper.Map<CostsCarWashAndDeteylingBll>(_unitOfWork.costsCarWashAndDeteylingUnitOfWork.GetById(elementId));
        }

        public void Update(CostsCarWashAndDeteylingBll elementToUpdate)
        {
            costsCarWashAndDeteyling costsCarWashAndDeteylings = Mapper.Map<CostsCarWashAndDeteylingBll, costsCarWashAndDeteyling>(elementToUpdate);

            _unitOfWork.costsCarWashAndDeteylingUnitOfWork.Update(costsCarWashAndDeteylings);
            _unitOfWork.Save();
        }
    }
}
