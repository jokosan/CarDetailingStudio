using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices
{
    public class UtilityCostsServices : IUtilityCosts
    {
        private IUnitOfWork _unitOfWork;

        public UtilityCostsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UtilityCostsBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<UtilityCostsBll>>(await _unitOfWork.utilityCostsUnitOfWork.GetInclude("expenseCategory"));
        }

        public async Task<UtilityCostsBll> SelectId(int? elementId)
        {
            return Mapper.Map<UtilityCostsBll>(await _unitOfWork.utilityCostsUnitOfWork.GetById(elementId));
        }

        #region Insert, Update
        public async Task Insert(UtilityCostsBll element)
        {
            utilityCosts utilityCosts = Mapper.Map<UtilityCostsBll, utilityCosts>(element);

            _unitOfWork.utilityCostsUnitOfWork.Insert(utilityCosts);
            await _unitOfWork.Save();
        }

        public async Task Update(UtilityCostsBll elementToUpdate)
        {
            utilityCosts utilityCosts = Mapper.Map<UtilityCostsBll, utilityCosts>(elementToUpdate);

            _unitOfWork.utilityCostsUnitOfWork.Update(utilityCosts);
            await _unitOfWork.Save();
        }


        #endregion

      
    }
}
