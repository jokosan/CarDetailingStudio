using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services
{
    public class PaymentStateServices : IPaymentState
    {
        private IUnitOfWork _unitOfWork;

        public PaymentStateServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentStateBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<PaymentStateBll>>(await _unitOfWork.PaymentStateUnitOfWork.Get());
        }

        public Task<PaymentStateBll> SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }
    }
}
