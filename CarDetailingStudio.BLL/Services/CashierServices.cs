using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class CashierServices : ICashier
    {
        private IUnitOfWork _unitOfWork;

        public CashierServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
